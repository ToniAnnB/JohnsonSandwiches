using JSandwiches.Models.SpecialFeaturesDTO;
using JSandwiches.Models.UsersDTO;
using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.OrderDTO
{
    public class ReceiptDTO : CreateReceiptDTO
    {
        [Required]
        public int Id { get; set; }

        public OrderDTO Order { get; set; }


        public CustomerDTO Customer { get; set; }


        public DealSpecificsDTO DealSpecifics { get; set; }
    }

    public class CreateReceiptDTO
    {
        [Required]
        public string ReceiptNumber { get; set; }


        [Required]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public int? DealSpecificsID { get; set; }
    }
}
