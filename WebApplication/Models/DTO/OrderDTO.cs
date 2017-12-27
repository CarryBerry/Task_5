using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DTO
{
    public class OrderDTO : DTO
    {
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between {1} and {2} characters.")]
        public string ShopAssistant { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between {1} and {2} characters.")]
        public string Customer { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The {0} name must be between {1} and {2} characters.")]
        public string Product { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "The {0} must be from {1} to {2}.")]
        public int Amount { get; set; }

        [Required]
        [Range(0.1, 1000000, ErrorMessage = "The {0} must be from {1} to {2}.")]
        public double Price { get; set; }
    }
}