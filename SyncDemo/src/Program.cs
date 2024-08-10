
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

                if (!Directory.Exists(replicaPath))
                {
                    Directory.CreateDirectory(replicaPath);
                    var replicaCreatedMessage = "$Created replica folder at {replicaPath}";
                    Console.WriteLine(replicaCreatedMessage);
                    logger.Log(replicaCreatedMessage);
                }


                var manager = new Manager(logger);
                var synchronizer = new FolderSynchronization(commandLine.SourcePath, commandLine.ReplicaPath, manager, logger);
                var syncTimer = new SyncTimer(synchronizer, commandLine.SyncInterval);

                var message = "Synchronization has started. Press any key to exit.";
                Console.WriteLine(message);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Application error: {e.Message}");
            }

        }


    }
}
