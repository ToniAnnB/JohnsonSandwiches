using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.FoodDTO
{
    public class MenuItemDTO : CreateMenuItemDTO
    {
        [Required]
        public int Id { get; set; }
        public ItemSubCategoryDTO? SubCategory { get; set; }

    }

    public class CreateMenuItemDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Title is too long")]
        public string Title { get; set; }


        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Description is too long")]
        public string Description { get; set; }


        [Required]
        public string ImagePath { get; set; }


        [Required]
        public decimal Price { get; set; }


        [Required]
        public int SubCategoryID { get; set; }



        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Special request is too long")]
        public string SpecialRequest { get; set; }

    }

}
