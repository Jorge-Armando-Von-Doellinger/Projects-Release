namespace OmniSphere.Products.Infrastructure.Exceptions;

public class ProductAlreadyExistsException : Exception
{
    public ProductAlreadyExistsException()
    {
        
    }

    public ProductAlreadyExistsException(string message) : base(message)
    {
        
    }
}