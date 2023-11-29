using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Order
{
    public class Reciept
    {
        [Key]
        public int Id { get; set; }



        [Column(TypeName = "varchar(MAX)")]
        public string RecieptNumber {  get; set; }



        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }



        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }



        public int? DealSpecificsID { get; set; }

        [ForeignKey("DealSpecificsID")]
        public virtual DealSpecifics DealSpecifics { get; set; }
    }
}
