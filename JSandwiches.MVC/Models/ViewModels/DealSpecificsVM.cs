using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Models.ViewModels
{
    public class DealSpecificsVM
    {
        public DealSpecificsDTO DealSpecificsDTO { get; set; }
        public IFormFile? DealImagePath { get; set; }
        public IEnumerable<SelectListItem> ddlDeals { get; set; }
    }
}
