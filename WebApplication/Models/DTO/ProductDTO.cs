using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DTO
{
    public class ProductDTO : DTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between {1} and {2} characters.")]
        public string ProductName { get; set; }

        [Required]
        [Range(0.1, 10000, ErrorMessage = "Price must be from {1} to {2}.")]
        public double ProductPrice { get; set; }
    }
}