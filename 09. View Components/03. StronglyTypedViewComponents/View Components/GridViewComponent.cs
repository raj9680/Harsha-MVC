using Microsoft.AspNetCore.Mvc;
using ViewComponentsWithViewData.Models;

namespace ViewComponentsExample.View_Components
{
    public class GridViewComponent: ViewComponent
    {
        private readonly PersonGridModel model;
        public GridViewComponent()
        {
            var personModels = new List<PersonModel>
            {
                new PersonModel() { PersonName = "John", JobTitle = "A" },
                new PersonModel() { PersonName = "Adam", JobTitle = "B" },
                new PersonModel() { PersonName = "Della", JobTitle = "C" },
                new PersonModel() { PersonName = "Steve", JobTitle = "D" }
            };

            model = new PersonGridModel()
            {
                GridTitle = "Persons List",
                Persons = personModels
            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(model);
        }
    }
}
