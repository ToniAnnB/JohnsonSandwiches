using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Users
{
    public class Address
    {
        [Key]
        public int Id { get; set; }




        [Column(TypeName = "varchar(50)")]
        public string Ln1 { get; set; }



        [Column(TypeName = "varchar(50)")]
        public string Ln2 { get; set; }



        public int ParishID { get; set; }

        [ForeignKey ("ParishID")]
        public virtual Parish Parish { get; set; }
    }
}
