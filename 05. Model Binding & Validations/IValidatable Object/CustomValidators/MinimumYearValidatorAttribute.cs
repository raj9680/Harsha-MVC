using System.ComponentModel.DataAnnotations;

namespace IValidatable_Object.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        public int minimumYear { get; set; } = 2000;
        public string DefaultErrorMessage { get; set; } = "Year shouldnot be less than {0}";
        public MinimumYearValidatorAttribute()
        {

        }

        public MinimumYearValidatorAttribute(int _minimumYear)
        {
            minimumYear = _minimumYear;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year < minimumYear)
                {
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
