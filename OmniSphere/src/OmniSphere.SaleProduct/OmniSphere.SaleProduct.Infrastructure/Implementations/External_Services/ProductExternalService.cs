using Grpc.Net.Client;
using OmniSphere.Products.Grpc;
using OmniSphere.SaleProduct.Core.Interfaces.External_Services;

namespace OmniSphere.SaleProduct.Infrastructure.Implementations.External_Services;

public class ProductExternalService : IProductExternalService
{
    public ProductExternalService()
    {
        
    }
    public async Task<(double Price, int Quantity)?> GetProductPriceById(string productId)
    {
        Console.WriteLine("Chegou aqui");
        try
        {
            double price = default;
            int quantity = default;
            await ExecuteRequestAsync(async (client) =>
            {
                var response = await client.GetProductByIdAsync(new() { Id = productId });
                Console.WriteLine(response.Quantity);
                Console.WriteLine(response.ProductPrice + " Price");
                price = response.ProductPrice != default ?  response.ProductPrice : throw new InvalidDataException("Price is invalid");
                quantity = response.Quantity != default ? response.Quantity : throw new InvalidDataException("Quantity is invalid");
            });
            return new () { Price = price, Quantity = quantity };
        }
        catch
        {
            throw;
            return null;
        }
    }

    public async Task RemoveProductAsync(string productId, int quantity)
    {
        try
        {
            await ExecuteRequestAsync(async (client) =>
            {
                var data = await client.GetProductByIdAsync(new() { Id = productId });
                data.Quantity = (data.Quantity - quantity) >= 0 
                    ? data.Quantity - quantity : throw new InvalidDataException("This quantity isn't available");
                await client.UpdateProductAsync(data);
            });
        }
        catch
        {
            throw new OperationCanceledException("The operation was canceled because an exception was thrown.");
        }
    }

    private async Task ExecuteRequestAsync(Func<GrpcProductServices.GrpcProductServicesClient, Task> requestFunc)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5100");
        try
        {
            var client = new GrpcProductServices.GrpcProductServicesClient(channel);
            await requestFunc(client);
        }
        finally
        {
            channel.Dispose();
        }
    }
}