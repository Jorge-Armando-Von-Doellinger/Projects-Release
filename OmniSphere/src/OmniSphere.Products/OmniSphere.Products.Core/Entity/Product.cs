using System.ComponentModel.DataAnnotations;
using OmniSphere.Products.Core.Attributes;

namespace OmniSphere.Products.Core.Entity;

public class Product : BaseEntity
{
    public required string ProductName { get; set; } // Ex: Fode bluetooth
    [EanCodeLength]
    public required string ProductEanCode { get; set; } // EAN Code (ex: 7890000000000)
    public required string ProductDescription { get; set; } // Portatil, otima qualidade, etc...
    public required double ProductPrice { get; set; }  // R$ 22,59
    public required string CategoryName { get; set; } // Ex: Audio, Video-Game, etc
    [DeniedValues(0)]
    public required int Quantity { get; set; }
}