using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.DAL.Models
{
    public class OrderDAL : Entity
    {
        //public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ShopAssistantId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public virtual ICollection<ProductDAL> Products { get; set; }
    }
}
