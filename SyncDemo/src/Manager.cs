namespace SyncDemo.src
{
    public class Manager
    {
        private readonly ILogger _logger;

        public Manager(ILogger logger)
        {
            _logger = logger;
        }

        public void CopyFile(string sourcePath, string destinationPath)
        {
            try
            {
                File.Copy(sourcePath, destinationPath, true);
                var message = $"COPY - Copied file from {sourcePath} to {destinationPath}";
                Console.WriteLine(message);
                _logger.Log(message);
            }
            catch (Exception e)
            {
                var errorMessage = $"ERROR - Failed to copy file {sourcePath} to {destinationPath}: {e.Message}";
                Console.WriteLine($"{e.Message}");
                _logger.Log(errorMessage);
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                var message = $"DELETE - Deleted file {path}";
                Console.WriteLine(message);
                _logger.Log(message);
            }
            catch (Exception e)
            {
                var errorMessage = $"ERROR - Failed to delete file {path}: {e.Message}";
                Console.WriteLine(errorMessage);
                _logger.Log(errorMessage);
            }
        }
    }
}
