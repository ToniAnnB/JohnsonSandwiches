using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.SpecialFeaturesDTO
{
    public class LoyaltyPointDTO : CreateLoyaltyPointDTO
    {
        [Required]
        public int Id { get; set; }

    }

    public class CreateLoyaltyPointDTO
    {
        [Required]
        public int Amount { get; set; }


        [StringLength(maximumLength: 250, ErrorMessage = "Description is too long")]
        public string Description { get; set; }

    }
}
