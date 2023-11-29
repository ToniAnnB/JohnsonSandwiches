﻿using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.DTO.FoodDTO
{
    public class ItemCategoryDTO : CreateItemCategoryDTO
    {
        [Required]
        public int Id { get; set; }

    }
    public class CreateItemCategoryDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Title is too long")]
        public string Title { get; set; }


        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
    }
}
