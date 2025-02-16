using OmniSphere.Products.Application.DTOs;
using OmniSphere.Products.Core.Entity;

namespace OmniSphere.Products.Grpc.ProtoMapper;

public class ProductProtoMapper
{
    internal ProductDTO MapToProductDTO(ProductProtoModel product)
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

    internal ProductProtoModel MapToProductProtoModel(ProductDTO product)
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

    internal GetListProductProtoModel MapToListProductProtoModels(IEnumerable<ProductDTO> products)
    {
        var productList = new GetListProductProtoModel();
        productList.ProductModel.AddRange(products.Select(MapToProductProtoModel));
        return productList;
    }
    
}