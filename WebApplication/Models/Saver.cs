using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.DTO;

namespace WebApplication.Models
{
    public class Saver
    {
        private Service _service;
        private Parser _parser;

        public Saver()
        {
            _service = new Service();
            _parser = new Parser();
        }

        public void SaveRecords(string path)
        {
            IEnumerable<OrderDTO> data = _parser.ParseData(path);
            foreach (var tape in data)
            {
                _service.AddOrder(tape);
            }
        }
    }
}
