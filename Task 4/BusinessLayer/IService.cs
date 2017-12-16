using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.BusinessLayer.DTO;

namespace Task_4.BusinessLayer
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
