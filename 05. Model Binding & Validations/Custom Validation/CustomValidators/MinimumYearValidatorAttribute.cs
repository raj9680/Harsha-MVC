using System.ComponentModel.DataAnnotations;

namespace Custom_Validation.CustomValidators
{
    public class MinimumYearValidatorAttribute: ValidationAttribute
    {
        public int minimumYear { get; set; } = 2000;
        public string DefaultErrorMessage { get; set; } = "Year shouldnot be less than {0}";
        // default constructor
        public MinimumYearValidatorAttribute()
        {
            
        }

        // parameterized constructor
        public MinimumYearValidatorAttribute(int _minimumYear)
        {
            minimumYear = _minimumYear;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                DateTime date = (DateTime)value;
                if(date.Year < minimumYear)
                {
                    //return new ValidationResult("Minimum year allowed is 2000 from custom validation");
                    //OR
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, minimumYear));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}
