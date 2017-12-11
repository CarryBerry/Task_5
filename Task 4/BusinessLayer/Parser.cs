using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task_4.BusinessLayer.DTO;

namespace Task_4.BusinessLayer
{
    public class Parser
    {
        public IEnumerable<OrderDTO> ParseData(string path)
        {
            //Ivanov_19112012.csv
            string shopAssistantName = Path.GetFileName(path).Split('_').First();
            IList<OrderDTO> history = new List<OrderDTO>();

            string[] tapes;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        tapes = sr.ReadLine().Split(',');//Дата,Клиент,Товар,Сумма
                                                         //OrderDTO order = new OrderDTO() { }
                        history.Add(new OrderDTO()
                        {
                            OrderDate = DateTime.Parse(tapes[0]),
                            ShopAssistant = shopAssistantName,
                            Customer = tapes[1],
                            Product = tapes[2],
                            Price = double.Parse(tapes[3])
                        });
                    }
                }
            }

            catch (Exception e)
            {
                throw new Exception();
            }

            return history;
            }
    }
}
