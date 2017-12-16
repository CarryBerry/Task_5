using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base()
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShopAssistant> ShopAssistants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
