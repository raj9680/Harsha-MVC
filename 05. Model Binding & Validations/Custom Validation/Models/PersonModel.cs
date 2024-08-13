using Custom_Validation.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace Custom_Validation.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "{0} can't be empty or null")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters long")]
        public string? PersonName { get; set; }


        //[MinimumYearValidator(2005, ErrorMessage ="Min Year required is{0} - custom message")] // custom validator class
        [MinimumYearValidator(2005)] // custom validator class
        public DateTime DateOfBirth { get; set; }


        public string? Email { get; set; }


        public string? Phone { get; set; }


        public string? Password { get; set; }


        public string? ConfirmPassword { get; set; }


        [Range(0, 999.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public string? Price { get; set; }


        public DateTime? FromDate { get; set; }

        //[DateRangeValidator("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To Date'")]
        [DateRangeValidator("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To Date'")]
        public DateTime? ToDate { get; set; }


        public override string ToString()
        {
            return $"Person Name: {PersonName}, Email: {Email}, DOB: {DateOfBirth}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }
    }
}
