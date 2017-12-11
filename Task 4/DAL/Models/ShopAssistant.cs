using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.DAL.Models
{
    public class ShopAssistantDAL : Entity
    {
        //public int ShopAssistantId { get; set; }

        public string ShopAssistantName { get; set; }

        public DateTime? HireDate { get; set; }

        public ICollection<OrderDAL> Orders { get; set; }
    }
}
