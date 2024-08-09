using System;

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
            if (args.Length != 4)
            {
                throw new ArgumentException("4 arguments expected: <sourcePath> <replicaPath> <logFilePath> <syncInterval>");
            }

            SourcePath = args[0];
            ReplicaPath = args[1];
            LogFilePath = args[2];

            if (!int.TryParse(args[3], out int interval) || interval <= 0)
            {
                throw new ArgumentException("The period of synchronization (<syncInterval>) is not a valid number.");
            }

            SyncInterval = interval;
        }
    }
}

