using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.UsersDTO
{
    public class AddressDTO : CreateAddressDTO
    {
        [Required]
        public int Id { get; set; }

        public ParishDTO? Parish { get; set; }
    }

    public class CreateAddressDTO
    {

        [Required]
        [DisplayName("Line 1")]
        [StringLength(maximumLength: 50, ErrorMessage = "FAddress Line 1 is too long")]
        public string Ln1 { get; set; }


        [Required]
        [DisplayName("Line 2")]
        [StringLength(maximumLength: 50, ErrorMessage = "FAddress Line 2 is too long")]
        public string Ln2 { get; set; }


        [Required]
        [DisplayName("Parish")]
        public int ParishID { get; set; }
    }
}
