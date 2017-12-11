using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;
using Task_4.Models;

namespace Task_4.DAL
{
    public class EFUnitOfWork : IUnitOfWork 
    {
        private readonly UsersContext _db;
        
        public EFUnitOfWork()
        {
            _db = new UsersContext();
        }

        private Repository<ShopAssistantDAL> _ShopAssistantRepository;
        private Repository<CustomerDAL> _CustomerRepository;
        private Repository<ProductDAL> _productRepository;
        private Repository<OrderDAL> _orderRepository;

        public IGenericRepository<ProductDAL> Products
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new Repository<ProductDAL>(_db);
                return _productRepository;
            }
        }

        public IGenericRepository<ShopAssistantDAL> ShopAssistants
        {
            get
            {
                if (_ShopAssistantRepository == null)
                    _ShopAssistantRepository = new Repository<ShopAssistantDAL>(_db);
                return _ShopAssistantRepository;
            }
        }

        public IGenericRepository<CustomerDAL> Customers
        {
            get
            {
                if (_CustomerRepository == null)
                    _CustomerRepository = new Repository<CustomerDAL>(_db);
                return _CustomerRepository;
            }
        }

        public IGenericRepository<OrderDAL> Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new Repository<OrderDAL>(_db);
                return _orderRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
