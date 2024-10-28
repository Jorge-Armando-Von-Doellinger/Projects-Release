namespace HMS.Payments.Core.Data
{
    public sealed class ServiceData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public CheckData Check { get; set; }
    }

    public class CheckData
    {
        public string HTTP { get; set; }
        public TimeSpan Interval { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}

