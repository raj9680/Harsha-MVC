namespace ViewComponentsWithViewData.Models
{
    public class PersonGridModel
    {
        public string GridTitle { get; set; } = string.Empty;
        public List<PersonModel> Persons { get; set; } = new List<PersonModel>();
    }
}
