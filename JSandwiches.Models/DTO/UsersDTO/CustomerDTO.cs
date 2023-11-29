using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.UsersDTO
{
    public class CustomerDTO : CreateCustomerDTO
    {
        [Required]
        public int Id { get; set; }

    }

    public class CreateCustomerDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "First Name is too long")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Last Name is too long")]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 11, ErrorMessage = "Phone Number is too long")]
        public string Phone { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Email Address is too long")]
        public string Email { get; set; }
    }
}
