using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Order
{
    public class Payment
    {
        public int Id { get; set; }


        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }


        public decimal TotalCost { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
