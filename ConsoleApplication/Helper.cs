using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BusinessLayer;

namespace ConsoleApplication
{
    public class Helper
    {
        private Task_4.BusinessLayer.Monitor _logger;

        public void OnStart()
        {
            _logger = new Task_4.BusinessLayer.Monitor();
            Thread loggerThread = new Thread(_logger.Start);
            loggerThread.Start();
        }
        public void OnStop()
        {
            _logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
