using System.Security.Cryptography;

namespace SyncDemo.src
{
    internal class FolderSynchronization
    {
        private readonly string _sourcePath;
        private readonly string _replicaPath;
        private readonly Manager _manager;
        private readonly ILogger _logger;

        public FolderSynchronization(string sourcePath, string replicaPath, Manager manager, ILogger logger)
        {
            _sourcePath = sourcePath;
            _replicaPath = replicaPath;
            _manager = manager;
            _logger = logger;
        }

        public void SynchronizeFolders()
        {
            try
            {
                if (!Directory.Exists(_replicaPath))
                {
                    Directory.CreateDirectory(_replicaPath);
                    var replicaCreatedMessage = $"Created Replica folder at {_replicaPath}";
                    Console.WriteLine(replicaCreatedMessage);
                    _logger.Log(replicaCreatedMessage);
                }

                var sourceFiles = Directory.GetFiles(_sourcePath, "*", SearchOption.AllDirectories);
                var replicaFiles = Directory.GetFiles(_replicaPath, "*", SearchOption.AllDirectories);

                var sourceFilesSet = new HashSet<string>(sourceFiles);
                var replicaFilesSet = new HashSet<string>(replicaFiles);

                foreach (var sourceFile in sourceFiles)
                {
                    var relativePath = Path.GetRelativePath(_sourcePath, sourceFile);
                    var replicaFile = Path.Combine(_replicaPath, relativePath);
                    var replicaDirectory = Path.GetDirectoryName(replicaFile);

                    if (!Directory.Exists(replicaDirectory))
                    {
                        Directory.CreateDirectory(replicaDirectory);
                    }

                    if (!replicaFilesSet.Contains(replicaFile) || !FileEquals(sourceFile, replicaFile))
                    {
                        _manager.CopyFile(sourceFile, replicaFile);
                    }
                }

                foreach (var replicaFile in replicaFiles)
                {
                    var relativePath = Path.GetRelativePath(_replicaPath, replicaFile);
                    var correspondingSourceFile = Path.Combine(_sourcePath, relativePath);

                    if (!sourceFilesSet.Contains(correspondingSourceFile))
                    {
                        _manager.DeleteFile(replicaFile);
                    }
                }

                var syncComplete = "Synchronization complete.";
                Console.WriteLine(syncComplete);
                _logger.Log(syncComplete);
            }
            catch (Exception ex)
            {
                var syncError = $"Error during synchronization: {ex.Message}";
                Console.WriteLine(syncError);
                _logger.Log(syncError);
            }
        }

        private static bool FileEquals(string path1, string path2)
        {
            using (var md5 = MD5.Create())
            {
                var hash1 = GetFileHash(md5, path1);
                var hash2 = GetFileHash(md5, path2);
                return hash1 == hash2;
            }
        }

        private static string GetFileHash(MD5 md5, string path)
        {
            using (var stream = File.OpenRead(path))
            {
                var hashBytes = md5.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
