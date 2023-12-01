using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.UsersDTO;
using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.SpecialFeaturesDTO
{
    public class MenuItemRatingDTO : CreateMenuItemRatingDTO
    {
        [Required]
        public int Id { get; set; }

        public CustomerDTO? Customer { get; set; }

        public RatingDTO? Rating { get; set; }

        public MenuItemDTO? MenuItem { get; set; }

    }
    public class CreateMenuItemRatingDTO
    {
        [Required]
        public int CustomerID { get; set; }


        [Required]
        public int RatingID { get; set; }


        [Required]
        public int MenuItemID { get; set; }


        [Required]
        public DateTime DateAllocated { get; set; }
    }
}
