using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.OrderDTO
{
    public class OrderDTO : CreateOrderDTO
    {
        [Required]
        public int Id { get; set; }


        public OrderStatusDTO? OrderStatus { get; set; }


    }
    public class CreateOrderDTO
    {

        [Required]
        public int Amount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Special request is too long")]
        public string? SpecialRequest { get; set; }


        [Required]
        public int OrderStatusID { get; set; }


    }
}
