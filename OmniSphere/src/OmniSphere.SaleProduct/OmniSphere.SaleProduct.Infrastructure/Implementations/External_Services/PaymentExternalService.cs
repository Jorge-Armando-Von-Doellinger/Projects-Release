using Grpc.Net.Client;
using OmniSphere.Payments.gRpc;
using OmniSphere.SaleProduct.Core.Interfaces.External_Services;

namespace OmniSphere.SaleProduct.Infrastructure.Implementations.External_Services;

public class PaymentExternalService : IPaymentExternalService
{
    public async Task<bool> ExecutePaymentAsync(string userId, double amount, string? message = null)
    {
        try
        {
            await ExecuteRequestAsync(async (client) =>
            {
                Console.WriteLine("Calling payment");
                var response = await client.AddPaymentAsync(new() { Amount = amount, AccountId = userId, Message = "message"}); 
            });
            return true;
        }
        catch
        {
            throw;
            return false;
        }
    }

    private async Task ExecuteRequestAsync(Func<PaymentProtoService.PaymentProtoServiceClient, Task> action)
    {
        using (var channel = GrpcChannel.ForAddress("http://localhost:5154"))
        {
            var client = new PaymentProtoService.PaymentProtoServiceClient(channel);
            await action(client);
        }
    }
}   