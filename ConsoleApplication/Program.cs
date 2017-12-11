using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper cs = new Helper();
            cs.OnStart();
            //string sourceFolder = ConfigurationShopAssistant.AppSettings["CSVSourceFolder"];

            //CSVController controller = new CSVController(sourceFolder);
            ////controller.GetAllTables();
            //controller.Run();

            //Console.WriteLine("\nTo stop watching and exit press '0'");
            //bool flag = true;
            //do
            //{
            //    var k = Console.ReadKey(true);
            //    if (k.KeyChar == '0')
            //    {
            //        flag = false;
            //        controller.Stop();
            //    }
            //} while (flag);

            //Console.WriteLine("\nPress any key to exit...");
            //Console.ReadKey();
        }
    }
}
