using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;

namespace Task_4.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ProductDAL> Products { get; }
        IGenericRepository<OrderDAL> Orders { get; }
        IGenericRepository<ShopAssistantDAL> ShopAssistants { get; }
        IGenericRepository<CustomerDAL> Customers { get; }
        void Save();
    }
}
