using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.DAL.Models
{
    public class ProductDAL : Entity
    {
        //public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public ICollection<OrderDAL> Orders { get; set; }
    }
}
