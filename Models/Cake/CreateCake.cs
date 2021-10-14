using System;
using System.ComponentModel.DataAnnotations;

namespace CakesManagement.Models.Cake
{
    public class CreateCake
    {
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
        public int CategoryId { get; set; }
    }
}
