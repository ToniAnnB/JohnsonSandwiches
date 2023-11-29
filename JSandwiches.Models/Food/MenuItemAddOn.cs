using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Food
{
    public class MenuItemAddOn
    {
        [Key]
        public int Id { get; set; }



        public int MenuItemID { get; set; }

        [ForeignKey("MenuItemID")]

        public virtual MenuItem MenuItem { get; set; }



        public int AddOnID { get; set; }

        [ForeignKey("AddOnID")]

        public virtual AddOn AddOn { get; set; }
    }
}
