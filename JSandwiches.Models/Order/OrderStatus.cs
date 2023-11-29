using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Order
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }
    }
}
