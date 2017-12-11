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

        public Service()
        {
            database = new EFUnitOfWork();
        }

        public void AddOrder(OrderDTO orderDto)
        {

            var ShopAssistant = new ShopAssistantDAL() { ShopAssistantName = orderDto.ShopAssistant };
            var customer = new CustomerDAL() { CustomerName = orderDto.Customer };
            var product = new ProductDAL() { ProductName = orderDto.Product };

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
                OrderDate = DateTime.Now,
                ShopAssistantId = ShopAssistant.Id,
                CustomerId = customer.Id,
                ProductId = product.Id
            };
            database.Orders.Insert(order);
            database.Save();
        }

        public ShopAssistantDTO GetShopAssistant(int? id)
        {
            var ShopAssistant = database.ShopAssistants.GetById(id.Value);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShopAssistantDAL, ShopAssistantDTO>()).CreateMapper();
            return mapper.Map<ShopAssistantDAL, ShopAssistantDTO>(ShopAssistant);
        }

        public IEnumerable<ShopAssistantDTO> GetShopAssistants()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShopAssistantDAL, ShopAssistantDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ShopAssistantDAL>, List<ShopAssistantDTO>>(database.ShopAssistants.GetAll());
            
        }
        
        public CustomerDTO GetCustomer(int? id)
        {
            var Customer = database.Customers.GetById(id.Value);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDAL, CustomerDTO>()).CreateMapper();
            return mapper.Map<CustomerDAL, CustomerDTO>(Customer);
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDAL, CustomerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<CustomerDAL>, List<CustomerDTO>>(database.Customers.GetAll());
        }

        public ProductDTO GetProduct(int? id)
        {
            var product = database.Products.GetById(id.Value);
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
