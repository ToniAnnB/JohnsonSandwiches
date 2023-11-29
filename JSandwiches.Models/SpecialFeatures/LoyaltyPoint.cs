using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.SpecialFeatures
{
    public class LoyaltyPoint
    {
        [Key]
        public int Id { get; set; }


        public int Amount { get; set; }



        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
    }
}
