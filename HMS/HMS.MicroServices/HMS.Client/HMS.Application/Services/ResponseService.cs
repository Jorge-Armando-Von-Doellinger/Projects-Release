using Nuget.MessagingUtilities;
using System.Text.Json;

namespace HMS.Application.Services
{
    public class ResponseService
    {
        public static Message CreateMessageResponse(Message messageRecieved, bool success, string? message = null)
        {
            string successMessage = "Operação realizada com sucesso!";
            string errorMessage = "Houve erros durante a operação!";
            string content = success ? successMessage : errorMessage;
            content = message != null ? message : content;
            return new Message()
            {
                Content = content,
                Destination = messageRecieved.Destination,
                ID = messageRecieved.ID,
                MessageFlow = messageRecieved.MessageFlow,
                Origin = messageRecieved.Origin
            };
        }
    }
}
