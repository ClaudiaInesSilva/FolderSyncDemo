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
                _logger.Log($"COPY - Copied file from {sourcePath} to {destinationPath}");
            }
            catch (Exception e)
            {
                _logger.Log($"ERROR - Failed to copy file {sourcePath} to {destinationPath}: {e.Message}");
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                _logger.Log($"DELETE - Deleted file {path}");
            }
            catch (Exception e)
            {
                _logger.Log($"ERROR - Failed to delete file {path}: {e.Message}");
            }
        }
    }
}
