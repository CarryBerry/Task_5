using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.DTO;

namespace WebApplication.Models
{
    public interface IService
    {
        void AddOrder(OrderDTO orderDto);
        ShopAssistantDTO GetShopAssistant(int id);
        IEnumerable<ShopAssistantDTO> GetShopAssistants();
        CustomerDTO GetCustomer(int id);
        IEnumerable<CustomerDTO> GetCustomers();
        ProductDTO GetProduct(int id);
        IEnumerable<ProductDTO> GetProducts();
        void Dispose();
    }
}
