using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Models.DTO
{
    public class CustomerDTO : DTO
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between {1} and {2} characters.")]
        public string CustomerName { get; set; }
    }
}