using System;
using System.ComponentModel.DataAnnotations;

namespace CakesManagement.Models.Category
{
    public class Create
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
