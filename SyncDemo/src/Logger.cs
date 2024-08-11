namespace SyncDemo.src
{
    public class Logger : ILogger
    {
        private readonly string _logFilePath;
        private readonly object _lock = new object();

        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {

            lock (_lock)
            {
                var logMessage = $"[{DateTime.Now}] : {message}\n";
                try
                {
                    Console.WriteLine(logMessage);
                    File.AppendAllText(_logFilePath, logMessage);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"LOG FILE ERROR - Failed to write to log file: {e.Message}");
                }
            }
        }
    }
}
