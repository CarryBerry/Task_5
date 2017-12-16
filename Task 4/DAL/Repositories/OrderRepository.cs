using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public OrderRepository(UsersContext context) : base(context)
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
                OrderDate = source.OrderDate,
                ShopAssistantId = source.ShopAssistantId,
                CustomerId = source.CustomerId,
                ProductId = source.ProductId,
                Amount = source.Amount,
                Price = source.Price
            };
        }

        public new void Insert(OrderDAL item)
        {
            context.Orders.Add(ToEntity(item));
        }

        //public new IEnumerable<OrderDAL> GetAll()
        //{
        //    return context.Orders.Select(x => new OrderDAL() { Id = x.OrderId, O = x.Name }).ToArray();
        //}

        //public OrderDAL GetById(int Id)
        //{
        //    return ToObject(context.Orders.FirstOrDefault(x => (x.OrderId == Id)));
        //}

        public void Update(Order item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
