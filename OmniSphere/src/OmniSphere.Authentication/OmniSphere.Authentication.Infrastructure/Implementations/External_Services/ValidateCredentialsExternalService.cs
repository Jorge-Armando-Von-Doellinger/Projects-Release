using Grpc.Net.Client;
using OmniSphere.Authentication.Core.Interfaces.External_Services;
using OmniSphere.Users.Grpc;

namespace OmniSphere.Authentication.Infrastructure.Implementations.External_Services;

public class ValidateCredentialsExternalService : ILoginExternalService
{
    public ValidateCredentialsExternalService()
    {
        
    }

    public async Task<bool> ValidateCredentialsAsync(string email, string password)
    {
        var valid = false;
        await ExecuteUserRequest(async (client) =>
        {
            var response = await client.FindUserAsync(new() { Email = email, Password = password });
            valid = (string.IsNullOrEmpty(response.Id) || response.Id.Length == 0);
        });
        return valid;
    }

    public async Task<string> GetUserIdByCredentialsAsync(string email, string password)
    {
        try
        {
            var userId = string.Empty;
            await ExecuteUserRequest(async (client) =>
            {
                try
                {
                    Console.WriteLine($"{email} and {password}");
                    var response = await client.FindUserAsync(new() { Email = email, Password = password });
                    userId = response.Id;

                }
                catch (Exception ex)
                {
                    userId = string.Empty;
                    Console.WriteLine(ex.Message + " HEREEEEEEE");
                    throw;
                }
            });

            return !(string.IsNullOrEmpty(userId) ||
                userId.Length == 0) // If false, returns userId, else, returns am empty string
                ? userId : string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + " GHereee::");
            throw new Exception("Batata");
        }
    }

    private async Task ExecuteUserRequest(Func<UsersProtoService.UsersProtoServiceClient,Task> action)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5204");
        var client = new UsersProtoService.UsersProtoServiceClient(channel);
        try
        {
            await action(client);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + " Her?");
        }
        finally
        {
            channel.Dispose();
        }
    }
}   