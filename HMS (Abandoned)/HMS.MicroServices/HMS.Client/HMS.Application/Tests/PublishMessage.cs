using HMS.Core.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Client.Input;
using Nuget.Client.MessagingSettings;
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
                var addkey = new ClientMessagingSettings().AddKey;
                var response = new Message();
                response.Configure(inputmodel, 
                    addkey, 
                    addkey, 
                    addkey);
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
