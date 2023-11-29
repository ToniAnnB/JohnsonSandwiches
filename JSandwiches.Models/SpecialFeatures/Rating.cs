using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.SpecialFeatures
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public double StarCount { get; set; }


        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
    }
}
