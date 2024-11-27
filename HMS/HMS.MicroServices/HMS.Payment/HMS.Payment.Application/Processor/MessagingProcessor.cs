using HMS.Payments.Application.Exceptions;
using HMS.Payments.Core.Interfaces.Processor;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;
using HMS.Payments.Application.Internal_Services;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.External_Services;
using HMS.Payments.Core.Interfaces.Messaging;

namespace HMS.Payments.Application.Processor;

public sealed class MessagingProcessor : IMessageProcessor
{
    private readonly HandlerService _handlerService;
    private readonly IMessagePublisher _publisher;
    private readonly INotificationExternalService _notificationExternalService;

    public MessagingProcessor(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateAsyncScope().ServiceProvider;
        _handlerService = scope.GetRequiredService<HandlerService>();
        _publisher = serviceProvider.CreateAsyncScope().ServiceProvider.GetRequiredService<IMessagePublisher>();
        _notificationExternalService = scope.CreateAsyncScope().ServiceProvider.GetRequiredService<INotificationExternalService>();
    }


    public async Task BatchProcess(byte[] bytes)
    {
        try
        {
            var json = Encoding.UTF8.GetString(bytes);
            json = json.ToLower();
            var handle = await _handlerService.GetHandler(json)
                         ?? throw new InvalidMessageException("Mensagem inválida!");
            await handle.HandleAsync(json);
        }
        catch (Exception ex)
        {
            JsonSerializer.Serialize(ex);
            Console.WriteLine(ex + " B");
            await _publisher.ToRetryQueue(new Message { Content = bytes, Attempts = 1 });
        }
    }

    public async Task BatchProcess(List<byte[]> bytes)
    {
        var messages = new List<Message>();
        await Parallel.ForEachAsync(bytes, async (bytes, _) =>
        {
            short attempts = 0;
            try
            {
                var json = Encoding.UTF8.GetString(bytes);
                var handle = await _handlerService.GetHandler(json)
                             ?? throw new InvalidMessageException("Mensagem inválida!");
                await handle.HandleAsync(json);
            }
            catch
            {
                messages.Add(new Message { Content = bytes, Attempts = 1 });
            }
        });
        if (messages.Count < 1) return; // Se não houver mensagens não processadas, ele retorna,
        await Parallel.ForEachAsync(messages,
            async (message, cancelationToken) => { await _publisher.ToRetryQueue(message); });
    }


    public async Task ReProcess(Message message) // Tenta reprocessar uma mensagem. Se não conseguir, joga na fila dnv
    {
        try
        {
            var json = Encoding.UTF8.GetString(message.Content);
            var handle = await _handlerService.GetHandler(json)
                         ?? throw new InvalidMessageException("Mensagem inválida!");
            await handle.HandleAsync(json);
        }
        catch
        {
            message.Attempts++;
            await _publisher.ToRetryQueue(message);
        }
    }
}