using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplication.Models.DTO;

namespace WebApplication.Models
{
    public class Parser
    {
        public IEnumerable<OrderDTO> ParseData(string path)
        {
            string shopAssistantName = Path.GetFileName(path).Split('_').First();
            //DateTime dateOfOrder = Convert.ToDateTime(Path.GetFileName(path).Split('_').Last());
            IList<OrderDTO> history = new List<OrderDTO>();

            string[] tapes;

            //try
            //{
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        tapes = sr.ReadLine().Split(',');
                                                         
                        history.Add(new OrderDTO() // OrderDate, CustomerName, ProductName, Price, AmountOfProduct
                        {
                            OrderDate = /*dateOfOrder*/Convert.ToDateTime(tapes[0]),
                            ShopAssistant = shopAssistantName,
                            Customer = tapes[1],
                            Product = tapes[2],
                            Price = Convert.ToDouble(tapes[3]),
                            Amount = Convert.ToInt32(tapes[4])
                        });
                    }
                }
            //}

            //catch (Exception e)
            //{
            //    throw new Exception();
            //}

            return history;
            }
    }
}
