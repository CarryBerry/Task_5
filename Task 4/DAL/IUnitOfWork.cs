using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;
using Task_4.DAL.Repositories;

namespace Task_4.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ProductRepository Products { get; }
        //IGenericRepository<OrderDAL> Orders { get; }
        OrderRepository Orders { get; }
        ShopAssistantRepository ShopAssistants { get; }
        CustomerRepository Customers { get; }
        void Save();
    }
}
