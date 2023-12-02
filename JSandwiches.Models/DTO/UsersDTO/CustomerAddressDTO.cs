using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.UsersDTO
{
    public class CustomerAddressDTO : CreateCustomerAddressDTO
    {
        [Required]
        public int Id { get; set; }

        public CustomerDTO? Customer { get; set; }

        public AddressDTO? Address { get; set; }
    }

    public class CreateCustomerAddressDTO
    {
        [Required]
        public int CustomerID { get; set; }


        [Required]
        public int AddressID { get; set; }
    }
}
