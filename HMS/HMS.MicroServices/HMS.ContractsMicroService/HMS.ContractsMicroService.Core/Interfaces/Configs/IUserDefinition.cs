namespace HMS.ContractsMicroService.Core.Interfaces.Configs
{
    public interface IUserDefinition
    {
        void Register<T>(T settings);
        void Override<T>(T settings);
        void Put<T>(T settings);
    }
}
