using JSandwiches.Models.Food;
using JSandwiches.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.SpecialFeatures
{
    public class MenuItemRating
    {
        [Key]
        public int Id { get; set; }



        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }



        public int RatingID { get; set; }

        [ForeignKey("RatingID")]
        public virtual Rating Rating { get; set; }


        public int MenuItemID { get; set; }

        [ForeignKey("MenuItemID")]
        public virtual MenuItem MenuItem { get; set; }



        public DateTime DateAllocated { get; set; }
    }
}
