using Nuget.Settings.Messaging;

namespace HMS.ContractsMicroService.Application.Settings.Messaging
{
    public class MessagingSettings : IRabbitMqSettings
    {
        public string HostName { get; protected set; }

        public int Port { get; protected set; }
        public void SetHostName(string hostName) => HostName = hostName;
        public void SetPort(int port) => Port = port;
    }
}
