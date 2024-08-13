using Microsoft.AspNetCore.Mvc;
using ViewComponentsWithViewData.Models;

namespace ViewComponentsExample.View_Components
{
    public class GridViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PersonGridModel gridParam)
        {
            return View(gridParam);
        }
    }
}
