using JSandwiches.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.SpecialFeatures
{
    public class CustomerLoyaltyPoint
    {
        [Key]
        public int Id { get; set; }


        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }



        public int LoyalPointsID { get; set; }
        [ForeignKey("LoyalPointsID")]
        public virtual LoyaltyPoint LoyaltyPoints { get; set; }



        public DateTime DateAllocated { get; set; }
    }
}
