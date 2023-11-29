using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.SpecialFeatures
{
    public class DealSpecifics
    {
        [Key]
        public int Id { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }



        [Column(TypeName = "decimal(18,2)")]
        public decimal Percentage { get; set; }



        [Column(TypeName = "varchar(MAX)")]
        public string ImagePath { get; set; }


        public int DealID { get; set; }

        [ForeignKey("DealID")]
        public virtual Deal Deal { get; set; }

    }
}
