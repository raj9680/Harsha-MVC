using IValidatable_Object.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace IValidatable_Object.Models
{
    public class PersonModel: IValidatableObject
    {
        [Required(ErrorMessage = "{0} can't be empty or null")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters long")]
        public string? PersonName { get; set; }


        [MinimumYearValidator(2005)] // custom validator class
        public DateTime DateOfBirth { get; set; }

        [BindNever] // - value wont be picked or received
        public string? Email { get; set; }


        public string? Phone { get; set; }


        public string? Password { get; set; }


        public string? ConfirmPassword { get; set; }


        [Range(0, 999.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public string? Price { get; set; }


        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To Date'")]
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }


        public override string ToString()
        {
            return $"Person Name: {PersonName}, Email: {Email}, DOB: {DateOfBirth}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Convert.ToString(DateOfBirth) == null || Age.HasValue == false)
            {
                yield return new ValidationResult("Either of Date of Birth or Age must be supplied", new[] { nameof(Age) });
            }
        }
    }
}
