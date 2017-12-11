using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ShopAssistantId { get; set; }
        public int ProductId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}
