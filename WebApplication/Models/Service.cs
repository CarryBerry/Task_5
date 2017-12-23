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
                var product = new ProductDAL()
                {
                    ProductName = orderDTO.Product,
                    //    if (orderDto.Amount == 0)
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

                OrderDAL order = new OrderDAL
                {
                    OrderDate = orderDTO.OrderDate,
                    ShopAssistantId = ShopAssistant.Id,
                    CustomerId = customer.Id,
                    ProductId = product.Id,
                    Price = orderDTO.Price,
                    Amount = orderDTO.Amount
                };

                database.Orders.Insert(order);
                database.Save();
            }
        }

        //public void UpdateOrder(OrderDTO orderDTO)
        //{
        //    var ShopAssistant = new ShopAssistantDAL() { ShopAssistantName = orderDTO.ShopAssistant };
        //    var customer = new CustomerDAL() { CustomerName = orderDTO.Customer };
        //    var product = new ProductDAL()
        //    {
        //        ProductName = orderDTO.Product,
        //        //    if (orderDto.Amount == 0)
        //        //{
        //        //    throw new DivideByZeroException();
        //        //}
        //        ProductPrice = orderDTO.Price / orderDTO.Amount
        //    };

            //var ShopAssistantId = database.ShopAssistants.GetId(ShopAssistant);
            //if (ShopAssistantId == null)
            //{
            //    database.ShopAssistants.Insert(ShopAssistant);
            //    database.Save();
            //    ShopAssistantId = database.ShopAssistants.GetId(ShopAssistant);
            //}

            //var managerId = _repositories.Managers.GetId(manager);
            //if (managerId == null)
            //{
            //    _repositories.Managers.Add(manager);
            //    _repositories.Save();
            //    managerId = _repositories.Managers.GetId(manager);
            //}

            //var clientId = _repositories.Clients.GetId(client);
            //if (clientId == null)
            //{
            //    _repositories.Clients.Add(client);
            //    _repositories.Save();
            //    clientId = _repositories.Clients.GetId(client);
            //}

            //var productId = _repositories.Products.GetId(product);
            //if (productId == null)
            //{
            //    _repositories.Products.Add(product);
            //    _repositories.Save();
            //    productId = _repositories.Products.GetId(product);
            //}

            //var saleInfo = new DAL.Models.SaleInfo()
            //{
            //    Date = orderDTO.Date,
            //    ManagerId = (int)managerId,
            //    ClientId = (int)clientId,
            //    ProductId = (int)productId,
            //    Amount = orderDTO.Amount
            //};

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

            for (int i = 1; i <= database.Products.GetAll().Count(); i++)
                list.Add(new ProductDTO
                {
                    ProductName = GetProduct(i).ProductName,
                    ProductPrice = GetProduct(i).ProductPrice
                });
            return list.AsEnumerable();

            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDAL, ProductDTO>()).CreateMapper();
            //return Mapper.Map<IEnumerable<ProductDAL>, List<ProductDTO>>(product);
        }
        
        public OrderDTO GetOrder(int id)
        {
            var order = database.Orders.GetById(id/*.Value*/);
            var product = database.Products.GetById(id);
            var customer = database.Customers.GetById(id);
            var shopAssistant = database.ShopAssistants.GetById(id);

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
            for (int i = 1; i <= database.Orders.GetAll().Count(); i++)
                list.Add(new OrderDTO
                {
                    OrderDate = GetOrder(i).OrderDate,
                    Id = GetOrder(i).Id,
                    Amount = GetOrder(i).Amount,
                    Customer = GetOrder(i).Customer,
                    Price = GetOrder(i).Price,
                    Product = GetOrder(i).Product,
                    ShopAssistant = GetOrder(i).ShopAssistant
                });
            return list.AsEnumerable();
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
