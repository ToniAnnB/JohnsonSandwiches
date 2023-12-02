using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.OrderDTO
{
    public class OrderStatusDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Title is too long")]
        public string Title { get; set; }
    }
}
