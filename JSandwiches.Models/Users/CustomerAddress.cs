using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSandwiches.Models.Users
{
    public class CustomerAddress
    {
        [Key]
        public int Id { get; set; }


        public int CustomerID { get; set; }

        [ForeignKey ("CustomerID")]
        public virtual Customer Customer { get; set; }



        public int AddressID { get; set; }

        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
    }
}
