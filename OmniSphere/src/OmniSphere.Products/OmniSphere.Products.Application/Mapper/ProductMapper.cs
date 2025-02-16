using OmniSphere.Products.Application.DTOs;
using OmniSphere.Products.Core.Entity;

namespace OmniSphere.Products.Application.Mapper;

public class ProductMapper
{
    internal Product Map(ProductDTO product)
    {
        return new()
        {
            CategoryName = product.CategoryName,
            ProductDescription = product.ProductDescription,
            ProductName = product.ProductName,
            ProductPrice = product.ProductPrice,
            ProductEanCode = product.ProductEanCode,
            Quantity = product.Quantity,
        };
    }
    internal ProductDTO Map(Product product)
    {
        return new()
        {
            CategoryName = product.CategoryName,
            ProductDescription = product.ProductDescription,
            ProductName = product.ProductName,
            ProductPrice = product.ProductPrice,
            ProductEanCode = product.ProductEanCode,
            Quantity = product.Quantity
        };
    }

    internal IEnumerable<ProductDTO> Map(IEnumerable<Product> products) => products.Select(Map);
    
}