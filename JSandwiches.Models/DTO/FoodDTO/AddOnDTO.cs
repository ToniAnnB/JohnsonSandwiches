using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.FoodDTO
{
    public class AddOnDTO : CreateAddOnDTO
    {
        [Required]
        public int Id { get; set; }

    }
    public class CreateAddOnDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Title is too long")]
        public string Title { get; set; }


        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Description is too long")]
        public string Description { get; set; }


        [Required]
        public decimal Price { get; set; }
    }
}
