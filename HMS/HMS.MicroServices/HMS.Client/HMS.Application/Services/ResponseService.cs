using Nuget.MessageObject;

namespace HMS.Application.Services
{
    public static class ResponseService
    {
        public Message SuccessMessage(Message messageRecieved)
        {
            return new Message
            {
                Content = "Operação realizada com sucesso!",
                Destination = messageRecieved.Destination,
                ID = messageRecieved.ID,
                Origin = 
            }
        }
    }
}
