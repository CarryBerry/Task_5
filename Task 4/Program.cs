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
            //using (UsersContext db = new UsersContext())
            //{
            //    Customer user1 = new Customer { CustomerName = "Tomas", CustomerId = 1};
            //    ShopAssistant ShopAssistant1 = new ShopAssistant { ShopAssistantName = "Samson", ShopAssistantId = 1, HireDate = DateTime.Now };
            //    Product product1 = new Product { ProductName = "Mozarella", ProductId = 1, ProductPrice = 50 };
            //    Order order1 = new Order { OrderDate = DateTime.Now, ProductId = 1, Amount = 1, CustomerId = 1, OrderId = 1, Price = 50, ShopAssistantId = 1 };

            //    db.Customers.Add(user1);
            //    db.ShopAssistants.Add(ShopAssistant1);
            //    db.Products.Add(product1);
            //    db.Orders.Add(order1);
            //    db.SaveChanges();
            //}
            EFUnitOfWork database = new EFUnitOfWork();

            foreach (var product in database.Products.GetAll())
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.ProductName, product.ProductPrice, database.Products.GetAll().Count());
            }

            Console.ReadKey();
        }
    }
}
