using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BusinessLayer;

namespace WindowsService
{
    public partial class WindowsService : ServiceBase
    {
        public WindowsService()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        private MessageManager _logger;

        protected override void OnStart(string[] args)
        {
            _logger = new MessageManager();
            Thread loggerThread = new Thread(_logger.Start);
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            _logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
