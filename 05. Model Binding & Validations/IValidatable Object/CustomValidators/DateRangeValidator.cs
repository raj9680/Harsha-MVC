using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IValidatable_Object.CustomValidators
{
    public class DateRangeValidator : ValidationAttribute
    {
        public string otherPropertyName { get; set; }
        public DateRangeValidator(string _otherPropertyName)
        {
            otherPropertyName = _otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // get ToDate
                DateTime to_date = Convert.ToDateTime(value);

                // get FromDate
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(otherPropertyName);

                if (otherProperty != null)
                {
                    DateTime from_date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));

                    if (from_date > to_date)
                    {
                        return new ValidationResult(ErrorMessage, new string[] { otherPropertyName, validationContext.MemberName });
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            return null;
        }
    }
}
