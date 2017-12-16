using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.BusinessLayer.DTO;
using Task_4.DAL;
using Task_4.DAL.Models;

namespace Task_4.BusinessLayer
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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDAL, ProductDTO>()).CreateMapper();
            return Mapper.Map<IEnumerable<ProductDAL>, List<ProductDTO>>(database.Products.GetAll());
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
