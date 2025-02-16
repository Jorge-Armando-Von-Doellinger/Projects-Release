using System.ComponentModel.DataAnnotations;

namespace OmniSphere.Products.Core.Attributes;

public class EanCodeLengthAttribute : ValidationAttribute
{
    public EanCodeLengthAttribute() : base("Ean code length is invalid!")
    { }

    public EanCodeLengthAttribute(string errorMessage) : base(errorMessage)
    {  }
    public override bool IsValid(object? value)
    {
        if (value is not string code) return false;
        return code.Length == 8 || code.Length == 13;
    }
}