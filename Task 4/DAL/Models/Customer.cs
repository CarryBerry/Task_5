using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.DAL.Models
{
    public class CustomerDAL : Entity
    {
        //public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CompanyName { get; set; }

        public string CustomerIdentification
        {
            get
            {
                if (CompanyName == "")
                {
                    return CustomerName;
                }
                else
                {
                    return CustomerName + ", " + CompanyName;
                }
            }
        }

        public ICollection<OrderDAL> Orders { get; set; }
    }
}
