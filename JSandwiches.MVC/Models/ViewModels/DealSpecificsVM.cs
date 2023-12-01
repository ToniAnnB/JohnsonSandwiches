using JSandwiches.Models.SpecialFeaturesDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Models.ViewModels
{
    public class DealSpecificsVM
    {
        public DealSpecificsDTO DealSpecifics { get; set; }
        public IFormFile? DealImagePath { get; set; }
        public IEnumerable<SelectListItem> ddlDeals { get; set; }
    }
}
