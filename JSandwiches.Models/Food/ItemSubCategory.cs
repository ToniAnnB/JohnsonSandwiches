using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.Food
{
    public class ItemSubCategory
    {
        [Key]
        public int Id { get; set; }




        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }




        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }



        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ItemCategory Category { get; set; }
    }
}
