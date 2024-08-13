using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Model_Validations_2.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "{0} can't be empty or null")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters long")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphabets, space and dot (.)")]
        public string? PersonName { get; set; }


        [EmailAddress(ErrorMessage = "{0} is not valid")]
        public string? Email { get; set; }


        [Phone(ErrorMessage = "{0} is not valid")]
        [ValidateNever]
        public string? Phone { get; set; }


        [Required(ErrorMessage = "{0} can't be blank")]
        public string? Password { get; set; }


        [Required(ErrorMessage = "{0} can't be blank")]
        [Compare("Password", ErrorMessage = "{0} and {1} do not match")]
        [Display(Name="Re-enter password")]
        public string? ConfirmPassword { get; set; }

        [Range(0, 999.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public string? Price { get; set; }


        public override string ToString()
        {
            return $"Person Name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }
    }
}
