using Microsoft.AspNetCore.Mvc;

namespace ViewComponentsExample.View_Components
{
    // [ViewComponent]
    public class GridViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(); // invoke partial view i/e Views/Shared/Components/Grid/Default.cshtml

            // return View("Sample"); // invoke partial view i/e Views/Shared/Components/Grid/Sample.cshtml
        }
    }
}
