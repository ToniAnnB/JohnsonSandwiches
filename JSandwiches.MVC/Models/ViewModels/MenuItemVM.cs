using JSandwiches.Models.DTO.FoodDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Models.ViewModels
{
    public class MenuItemVM
    {
        public MenuItemDTO MenuItem { get; set; }
        public IFormFile MenuItemImagePath { get; set; }
        public IEnumerable<SelectListItem> ddlSubCategory { get; set; }
    }
}
