using JSandwiches.Models.DTO.FoodDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Models.ViewModels
{
    public class SubCategoryVM
    {
        public ItemSubCategoryDTO SubCategory { get; set; }
        public IEnumerable<SelectListItem> ddlCategory { get; set; }
    }
}
