using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SyncDemo.src
{
    internal class FolderSynchronization
    {
        private readonly string _sourcePath;
        private readonly string _replicaPath;
        private readonly Manager _manager;
        private readonly ILogger _logger;


        public FolderSynchronization(string sourcePath,string replicaPath, Manager manager, ILogger logger)
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
                    var destinationFile = Path.Combine(_replicaPath, relativePath);

                    if (!replicaFilesSet.Contains(destinationFile) || !FileEquals(sourceFile, destinationFile))
                    {
                        _manager.CopyFile(sourceFile, destinationFile);
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

                var syncComplete = "Synchronization completed.";
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
            using (var sha = File.OpenRead(path))
            {
                var hashBytes = md5.ComputeHash(sha);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
