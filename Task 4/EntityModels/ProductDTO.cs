using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Product's Name")]
        public string ProductName { get; set; }

        [Range(0.1, 1000)]
        public double ProductPrice { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
