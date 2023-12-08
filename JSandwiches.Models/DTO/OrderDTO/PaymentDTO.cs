namespace JSandwiches.Models.DTO.OrderDTO
{
    public class PaymentDTO : CreateOrderDTO
    {
        public int Id { get; set; }

        public OrderDTO? Order { get; set; }


    }
    public class CreatePaymentDTO
    {
        public int OrderID { get; set; }

        public decimal TotalCost { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
