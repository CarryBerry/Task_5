using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BusinessLayer.DTO
{
    public class OrderDTO : DTO
    {
        public DateTime OrderDate { get; set; }
        public string ShopAssistant { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
