using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.DTO;
using Task_4.DAL;
using Task_4.DAL.Models;

namespace WebApplication.Models
{
    public class Service : IService
    {
        private IUnitOfWork database;
        private object _locker = new object();

        public Service()
        {
            database = new EFUnitOfWork();
        }

        public void AddOrder(OrderDTO orderDTO)
        {
            lock (_locker)
            {
                var ShopAssistant = new ShopAssistantDAL() { ShopAssistantName = orderDTO.ShopAssistant };
                var customer = new CustomerDAL() { CustomerName = orderDTO.Customer };
                //var orderDate = new OrderDAL() { OrderDate = orderDTO.OrderDate };
                var product = new ProductDAL()
                {
                    ProductName = orderDTO.Product,
                //        if (orderDto.Amount == 0)
                //{
                //    throw new DivideByZeroException();
                //}
                ProductPrice = orderDTO.Price / orderDTO.Amount
                };

                var ShopAssistantId = database.ShopAssistants.GetId(ShopAssistant);
                if (ShopAssistantId == null)
                {
                    database.ShopAssistants.Insert(ShopAssistant);
                    database.Save();
                    ShopAssistantId = database.ShopAssistants.GetId(ShopAssistant);
                }

                var CustomerId = database.Customers.GetId(customer);
                if (CustomerId == null)
                {
                    database.Customers.Insert(customer);
                    database.Save();
                    CustomerId = database.Customers.GetId(customer);
                }

                var productId = database.Products.GetId(product);
                if (productId == null)
                {
                    database.Products.Insert(product);
                    database.Save();
                    productId = database.Products.GetId(product);
                }

                if (orderDTO.OrderDate.Year == 1)
                {
                    orderDTO.OrderDate = DateTime.Now;
                }

                if (orderDTO.Id == 0)
                {
                    var last = database.Orders.GetAll().Last();
                    orderDTO.Id = last.Id + 1;
                }

                OrderDAL order = new OrderDAL
                {
                    Id = orderDTO.Id,
                    OrderDate = orderDTO.OrderDate,
                    ShopAssistantId = ShopAssistantId.Value,
                    CustomerId = CustomerId.Value,
                    ProductId = productId.Value,
                    Price = orderDTO.Price,
                    Amount = orderDTO.Amount
                };

                database.Orders.Insert(order);
                database.Save();
            }
        }

        public ShopAssistantDTO GetShopAssistant(int id)
        {
            var ShopAssistant = database.ShopAssistants.GetById(id/*.Value*/);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShopAssistantDAL, ShopAssistantDTO>()).CreateMapper();
            return mapper.Map<ShopAssistantDAL, ShopAssistantDTO>(ShopAssistant);
        }

        public IEnumerable<ShopAssistantDTO> GetShopAssistants()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShopAssistantDAL, ShopAssistantDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ShopAssistantDAL>, List<ShopAssistantDTO>>(database.ShopAssistants.GetAll());
            
        }
        
        public CustomerDTO GetCustomer(int id)
        {
            var Customer = database.Customers.GetById(id/*.Value*/);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDAL, CustomerDTO>()).CreateMapper();
            return mapper.Map<CustomerDAL, CustomerDTO>(Customer);
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDAL, CustomerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<CustomerDAL>, List<CustomerDTO>>(database.Customers.GetAll());
        }

        public ProductDTO GetProduct(int id)
        {
            var product = database.Products.GetById(id/*.Value*/);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDAL, ProductDTO>()).CreateMapper();
            return mapper.Map<ProductDAL, ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var list = new List<ProductDTO>();

            //return database.Products.GetAll() as IEnumerable<ProductDTO>;

            foreach (var product in database.Products.GetAll())
                list.Add(new ProductDTO
                {
                    Id = GetProduct(product.Id).Id,
                    ProductName = GetProduct(product.Id).ProductName,
                    ProductPrice = GetProduct(product.Id).ProductPrice
                });
            return list.AsEnumerable();

            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDAL, ProductDTO>()).CreateMapper();
            //return Mapper.Map<IEnumerable<ProductDAL>, List<ProductDTO>>(product);
        }
        
        public OrderDTO GetOrder(int id)
        {
            var order = database.Orders.GetById(id/*.Value*/);
            var product = database.Products.GetById(order.ProductId);
            var customer = database.Customers.GetById(order.CustomerId);
            var shopAssistant = database.ShopAssistants.GetById(order.ShopAssistantId);

            return new OrderDTO()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                ShopAssistant = shopAssistant.ShopAssistantName,
                Customer = customer.CustomerName,
                Product = product.ProductName,
                Amount = order.Amount,
                Price = order.Price
            };
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var list = new List<OrderDTO>();

            //int n = database.Orders.GetAll().Count();
            foreach (var order in database.Orders.GetAll())
            {
                list.Add(new OrderDTO
                {
                    OrderDate = GetOrder(order.Id).OrderDate,
                    Id = GetOrder(order.Id).Id,
                    Amount = GetOrder(order.Id).Amount,
                    Customer = GetOrder(order.Id).Customer,
                    Price = GetOrder(order.Id).Price,
                    Product = GetOrder(order.Id).Product,
                    ShopAssistant = GetOrder(order.Id).ShopAssistant
                });
            }
            //for (int i = 1; i <= database.Orders.GetAll().Count(); i++)
            //    list.Add(new OrderDTO
            //    {
            //        OrderDate = GetOrder(i).OrderDate,
            //        Id = GetOrder(i).Id,
            //        Amount = GetOrder(i).Amount,
            //        Customer = GetOrder(i).Customer,
            //        Price = GetOrder(i).Price,
            //        Product = GetOrder(i).Product,
            //        ShopAssistant = GetOrder(i).ShopAssistant
            //    });
            return list.AsEnumerable();
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
