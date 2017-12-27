using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;
using Task_4.Models;

namespace Task_4.DAL.Repositories
{
    public class OrderRepository : Repository<OrderDAL>
    {
        private UsersContext context;
        private DbSet<OrderDAL> dbSet;

        public OrderRepository(UsersContext context) /*: base(context)*/
        {
            this.context = context;
            this.dbSet = context.Set<OrderDAL>();
        }
        
        public Order ToEntity(OrderDAL source)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDAL, Order>()).CreateMapper();
            //return mapper.Map<OrderDAL, Order>(source);
            return new Order()
            {
                OrderId = source.Id,
                OrderDate = source.OrderDate,
                ShopAssistantId = source.ShopAssistantId,
                CustomerId = source.CustomerId,
                ProductId = source.ProductId,
                Amount = source.Amount,
                Price = source.Price
            };
        }

        public OrderDAL ToObject(Order source)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDAL>()).CreateMapper();
            //return mapper.Map<Order, OrderDAL>(source);
            return new OrderDAL()
            {
                Id = source.OrderId,
                OrderDate = source.OrderDate,
                ShopAssistantId = source.ShopAssistantId,
                CustomerId = source.CustomerId,
                ProductId = source.ProductId,
                Amount = source.Amount,
                Price = source.Price
            };
        }

        public IEnumerable<OrderDAL> GetAll()
        {
            return context.Orders.Select(x => new OrderDAL()
            {
                Id = x.OrderId,
                CustomerId = x.CustomerId,
                OrderDate = x.OrderDate,
                Amount = x.Amount,
                Price = x.Price,
                ProductId = x.ProductId,
                ShopAssistantId = x.ShopAssistantId
            })
            .ToArray();
        }

        public OrderDAL GetById(int Id)
        {
            return ToObject(context.Orders.FirstOrDefault(x => (x.OrderId == Id)));
        }

        public void Insert(OrderDAL item)
        {
            context.Orders.Add(ToEntity(item));
        }

        public void Update(OrderDAL item)
        {
            //context.Orders.AddOrUpdate(ToEntity(item));
            context.Entry(ToEntity(item)).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Customer item = context.Customers.Find(id);
            if (item != null)
            {
                context.Customers.Remove(item);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
