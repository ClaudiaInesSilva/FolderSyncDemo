namespace SyncDemo.src
{
    internal class CommandLine
    {
        public string SourcePath { get; private set; }
        public string ReplicaPath { get; private set; }
        public string LogFilePath { get; private set; }
        public int SyncInterval { get; private set; }

        public CommandLine(string[] args)
        {
            const int MinSyncInterval = 10;
            const int MaxSyncInterval = 3600;

            if (args.Length != 4)
            {
                throw new ArgumentException("4 arguments expected: <sourcePath> <replicaPath> <logFilePath> <syncInterval>");
            }

            SourcePath = args[0];
            ReplicaPath = args[1];
            LogFilePath = args[2];

            ValidatePaths(SourcePath, ReplicaPath, LogFilePath);

            if (!int.TryParse(args[3], out int interval) || interval < MinSyncInterval || interval > MaxSyncInterval)
            {
                throw new ArgumentException($"The period of synchronization (<syncInterval>) must be between {MinSyncInterval} seconds and {MaxSyncInterval} seconds.");
            }

            SyncInterval = interval;
        }

        private static void ValidatePaths(string sourcePath, string replicaPath, string logFilePath)
        {
            if (!Directory.Exists(sourcePath))
            {
                throw new ArgumentException($"The source path '{sourcePath}' does not exist or is not accessible.");
            }

            if (!Directory.Exists(replicaPath))
            {
                throw new ArgumentException($"The replica path '{replicaPath}' does not exist or is not accessible.");
            }

            string? logDirectory = Path.GetDirectoryName(logFilePath);

            if (string.IsNullOrEmpty(logDirectory) || !Directory.Exists(logDirectory))
            {
                throw new ArgumentException($"The directory for the log file '{logFilePath}' does not exist or is not accessible.");
            }
        }
    }
}