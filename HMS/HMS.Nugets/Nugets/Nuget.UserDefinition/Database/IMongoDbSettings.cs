namespace Nuget.Settings.Database
{
    public interface IMongoDbSettings
    {
        public string ConnectionString { get; set; }
    }
}
