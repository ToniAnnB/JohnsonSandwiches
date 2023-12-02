using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.UsersDTO
{
    public class ParishDTO
    {
        [Required]
        public int Id { get; set; }


        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
    }
}
