using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Food
{
    public class ItemCategory
    {
        [Key]
        public int Id { get; set; }



        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }



        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
    }
}
