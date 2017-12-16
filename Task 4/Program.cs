using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL;
using Task_4.Models;

namespace Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UsersContext db = new UsersContext())
            {
                Customer user1 = new Customer { CustomerName = "Tomas" };
                ShopAssistant ShopAssistant1 = new ShopAssistant { ShopAssistantName = "Samson" };

                db.Customers.Add(user1);
                db.ShopAssistants.Add(ShopAssistant1);
                db.SaveChanges();
            }
        }
    }
}
