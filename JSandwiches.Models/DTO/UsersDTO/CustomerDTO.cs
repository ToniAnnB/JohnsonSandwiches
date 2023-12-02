using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.UsersDTO
{
    public class CustomerDTO : CreateCustomerDTO
    {
        [Required]
        public int Id { get; set; }

    }

    public class CreateCustomerDTO
    {
        [Required]
        [DisplayName ("First Name")]
        [StringLength(maximumLength: 50, ErrorMessage = "First Name is too long")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(maximumLength: 50, ErrorMessage = "Last Name is too long")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [StringLength(maximumLength: 11, ErrorMessage = "Phone Number is too long")]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [StringLength(maximumLength: 250, ErrorMessage = "Email Address is too long")]
        public string Email { get; set; }
    }
}
