using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.SpecialFeaturesDTO
{
    public class DealSpecificsDTO : CreateDealSpecificsDTO
    {
        [Required]
        public int Id { get; set; }

        public DealDTO? Deal { get; set; }
    }
    public class CreateDealSpecificsDTO
    {
        [Required]
        public DateTime StartDate { get; set; }


        [Required]
        public DateTime EndDate { get; set; }


        [Required]
        public decimal Percentage { get; set; }


        [Required]
        public string ImagePath { get; set; }


        [Required]
        public int DealID { get; set; }
    }
}
