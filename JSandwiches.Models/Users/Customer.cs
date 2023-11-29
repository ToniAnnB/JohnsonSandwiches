using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Users
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }


        [Column(TypeName = "varchar(11)")]
        public string Phone { get; set; }


        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }
    }
}
