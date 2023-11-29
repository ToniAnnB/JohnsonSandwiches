using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Users
{
    public class Parish
    {
        [Key]
        public int Id { get; set; }



        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}
