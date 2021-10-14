using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CakesManagement.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(200)]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection<Cake> Cakes { get; set; }
    }
}
