using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakesManagement.Entities
{
    public class Cake
    {
        [Key]
        public int CakeId { get; set; }
        [Required]
        [StringLength(300)]
        public string CakeName { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool Status { get; set; }
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
