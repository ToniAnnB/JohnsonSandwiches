using JSandwiches.Models.Food;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Order
{
    public class Order
    {
        [Key]
        public int Id { get; set; }



        public int MenuItemAddOnID { get; set; }
        [ForeignKey("MenuItemAddOnID")]
        public virtual MenuItemAddOn MenuItemAddOn { get; set; }


        public int Amount { get; set; }



        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }



        public int OrderStatusID { get; set; }
        [ForeignKey("OrderStausID")]
        public virtual OrderStatus OrderStatus { get; set; }


        public void CalcuPrice(decimal itemPrice)
        {
            Price = Amount * itemPrice;
        }
    }
}
