namespace CustomModelBinder.Models
{
    public class PersonModel
    {
        public string? PersonName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Price { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        
        // [Required]
        public int? Age { get; set; }

        public List<string?> Tags { get; set; } = new List<string?>();

        public override string ToString()
        {
            return $"Person Name: {PersonName}, Email: {Email}, DOB: {DateOfBirth}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}, Tags: {Tags}";
        }
    }
}
