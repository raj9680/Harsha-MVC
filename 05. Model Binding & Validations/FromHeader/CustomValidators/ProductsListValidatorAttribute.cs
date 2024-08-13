using FromHeader.Models;
using System.ComponentModel.DataAnnotations;

namespace FromHeader.CustomValidators
{
    public class ProductsListValidatorAttribute: ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Order should have atleast one product.";

        public ProductsListValidatorAttribute()
        {
            
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null) 
            {
                List<Product> products = (List<Product>)value;

                //if no products
                if(products.Count == 0)
                {
                    // return validation error
                    return new ValidationResult(DefaultErrorMessage, new string[] { nameof(ValidationContext.MemberName) });
                }
                // no validation error
                return ValidationResult.Success;
            }
            return null;
        }
    }
}
