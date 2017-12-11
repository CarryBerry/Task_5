using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        
        [Column("CustomerName")]
        [Display(Name = "Customer Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string CustomerName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Company Name")]
        [DisplayFormat(NullDisplayText = "No company")]
        public string CompanyName { get; set; }
        
        public ICollection<Order> Orders { get; set; }
    }
}
