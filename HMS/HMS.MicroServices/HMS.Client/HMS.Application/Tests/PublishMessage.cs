using HMS.Core.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Client.Input;
using Nuget.MessagingUtilities;
using System.Text.Json;

namespace HMS.Application.Tests
{
    public class PublishMessage
    {
        internal async Task teste(IServiceScope scope)
        {
            try
            {

                var inputmodel = new InputModel
                {
                    CPF = 123,
                    DateBirth = DateTime.UtcNow,
                    EmailAdress = "teste",
                    Name = "BANANAAAAAAAAAAAAAAA",
                    PhoneNumber = "1234567890",
                    RG = 123321,
                    YearsOld = 19
                };
                var response = new Message
                {
                    Content = inputmodel,
                    Destination = "clients.add",
                    ID = new Guid(),
                    MessageFlow = new List<string>() { "clients.add" },
                    Origin = "clients.add"
                };
                var _messagePublisher = scope.ServiceProvider.GetRequiredService<IMessagePublisher>();
                await _messagePublisher.PublishMessage(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
