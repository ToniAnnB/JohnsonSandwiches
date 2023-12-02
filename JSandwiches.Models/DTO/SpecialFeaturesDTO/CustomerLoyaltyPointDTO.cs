using JSandwiches.Models.DTO.UsersDTO;
using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.SpecialFeaturesDTO
{
    public class CustomerLoyaltyPointDTO : CreateCustomerLoyaltyPointDTO
    {
        [Required]
        public int Id { get; set; }

        public CustomerDTO? Customer { get; set; }

        public LoyaltyPointDTO? LoyaltyPoints { get; set; }
    }

    public class CreateCustomerLoyaltyPointDTO
    {
        [Required]
        public int CustomerID { get; set; }


        [Required]
        public int LoyalPointsID { get; set; }


        [Required]
        public DateTime DateAllocated { get; set; }

    }
}
