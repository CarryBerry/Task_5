using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_4.BusinessLayer
{
    public class Logger
    {
        private readonly FileSystemWatcher _watcher;
        private readonly Saver _saver;
        private Task _task;
        private readonly object _obj = new object(); 
        private bool actionStopper = true;

        public Logger()
        {
            _saver = new Saver();
            _watcher = new FileSystemWatcher();
            _watcher.Path = ConfigurationManager.AppSettings["Path"];
            _watcher.Filter = "*.csv";
            _watcher.NotifyFilter = NotifyFilters.FileName;
        
            _watcher.Created += OnChanged;
            _watcher.Deleted += Watcher_Deleted;
            _watcher.Created += Watcher_Created;
            _watcher.Changed += Watcher_Changed;
            _watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            while (actionStopper)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            actionStopper = false;
        }

        // переименование файлов
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "renamed to " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }

        // изменение файлов
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "updated";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        // создание файлов
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "created";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        // удаление файлов
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "deleted";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (_obj)
            {
                string path = ConfigurationManager.AppSettings["MessageFile"];
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine($"{DateTime.Now:dd/MM/yyyy hh:mm:ss} file {filePath} was {fileEvent}");
                    writer.Flush();
                }
            }
        }

        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            _task = new Task
                (() => CallParse(sender, e));
            _task.Start();
        }

        public void CallParse(object sender, FileSystemEventArgs e)
        {
            var path = e.FullPath;
            _saver.SaveRecords(path);
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}
