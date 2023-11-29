using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.FoodDTO
{
    public class MenuItemAddOnDTO : CreateMenuItemAddOnDTO
    {
        [Required]
        public int Id { get; set; }

        public MenuItemDTO MenuItem { get; set; }

        public AddOnDTO AddOn { get; set; }
    }
    public class CreateMenuItemAddOnDTO
    {
        [Required]
        public int AddOnID { get; set; }


        [Required]
        public int MenuItemID { get; set; }
    }
}
