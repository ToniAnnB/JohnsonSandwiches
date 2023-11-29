using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.SpecialFeaturesDTO
{
    public class RatingDTO
    {
        [Required]
        public int Id { get; set; }


        [Required]
        [Range(1, 5)]
        public double StarCount { get; set; }


        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
    }
}
