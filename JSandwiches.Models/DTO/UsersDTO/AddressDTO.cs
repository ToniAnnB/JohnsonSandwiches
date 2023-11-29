using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.UsersDTO
{
    public class AddressDTO : CreateAddressDTO
    {
        [Required]
        public int Id { get; set; }

        public ParishDTO Parish { get; set; }
    }

    public class CreateAddressDTO
    {

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "FAddress Line 1 is too long")]
        public string Ln1 { get; set; }


        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "FAddress Line 2 is too long")]
        public string Ln2 { get; set; }


        [Required]
        public int ParishID { get; set; }
    }
}
