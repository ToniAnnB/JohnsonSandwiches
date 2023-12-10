using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Order
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }


        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [Column (TypeName = "varchar(MAX)")]
        public string ReceiptNumber { get; set; }
         
        public decimal TotalCost { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
