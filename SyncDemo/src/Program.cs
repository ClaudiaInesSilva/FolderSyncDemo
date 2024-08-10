using SyncDemo.src;

namespace SyncDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var commandLine = new CommandLine(args);
                var logger = new Logger(commandLine.LogFilePath);

                var replicaFolderName = $"{Path.GetFileName(commandLine.SourcePath)}_replica";
                var replicaPath = Path.Combine(commandLine.ReplicaPath, replicaFolderName);

                var manager = new Manager(logger);
                var synchronizer = new FolderSynchronization(commandLine.SourcePath, replicaPath, manager, logger);
                var syncTimer = new SyncTimer(synchronizer, commandLine.SyncInterval);

                var message = "INFO - Synchronization has started. Press CTRL+C to exit.";
                Console.WriteLine(message);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Application Error: {e.Message}");
            }
        }
    }
}
