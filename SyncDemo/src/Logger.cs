﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncDemo.src
{
    public class Logger : ILogger
    {
        private readonly string _logFilePath;

        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            var logMessage = $"{DateTime.Now}: {message}\n";
            try
            {
                Console.WriteLine(logMessage);
                File.AppendAllText(_logFilePath, logMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to write to log file: {e.Message}");
            }
        }
    }
}
