using System.ComponentModel.DataAnnotations;

namespace OmniSphere.SaleProduct.Core.Entity;

public class ProductSale
{
    [MinLength(1)]
    public required string UserId { get; set; }
    [MinLength(1)]
    public required string ProductId { get; set; }
    [DeniedValues(0)]
    public int Quantity { get; set; }
}