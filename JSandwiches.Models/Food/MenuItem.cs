using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Food
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }



        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }



        [Column(TypeName = "varchar(MAX)")]
        public string ImagePath { get; set; }



        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        public int SubCategoryID { get; set; }
        [ForeignKey("SubCategoryID")]
        public virtual ItemSubCategory SubCategory { get; set; }



        [Column(TypeName = "varchar(250)")]
        public string SpecialRequest { get; set; }
    }
}
