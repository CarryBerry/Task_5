using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.Models
{
    public class ShopAssistant 
    {
        [Key]
        public int ShopAssistantId { get; set; }
        
        [Column("ShopAssistantName")]
        [Display(Name = "Shop Assistant Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string ShopAssistantName { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime? HireDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
