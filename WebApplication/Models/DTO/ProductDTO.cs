using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models.DTO
{
    public class ProductDTO : DTO
    {
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}