using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;
using Task_4.DAL.Repositories;
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

        private ShopAssistantRepository _ShopAssistantRepository;
        private CustomerRepository _CustomerRepository;
        private ProductRepository _productRepository;
        //private Repository<OrderDAL> _orderRepository;
        private OrderRepository _orderRepository;

        public ProductRepository Products
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_db);
                return _productRepository;
            }
        }

        public ShopAssistantRepository ShopAssistants
        {
            get
            {
                if (_ShopAssistantRepository == null)
                    _ShopAssistantRepository = new ShopAssistantRepository(_db);
                return _ShopAssistantRepository;
            }
        }

        public CustomerRepository Customers
        {
            get
            {
                if (_CustomerRepository == null)
                    _CustomerRepository = new Repositories.CustomerRepository(_db);
                return _CustomerRepository;
            }
        }

        //public IGenericRepository<OrderDAL> Orders
        //{
        //    get
        //    {
        //        if (_orderRepository == null)
        //            _orderRepository = new Repository<OrderDAL>(_db);
        //        return _orderRepository;
        //    }
        //}

        public OrderRepository Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_db);
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
