using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.MVC.ViewComponents
{
    [ViewComponent(Name = "SearchBar")]

    public class SearchBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
