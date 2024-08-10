namespace SyncDemo.src
{
    internal class SyncTimer
    {
        private readonly FolderSynchronization _folderSynchronization;
        private readonly Timer _timer;

        public SyncTimer(FolderSynchronization folderSynchronization, int syncInterval)
        {
            _folderSynchronization = folderSynchronization;
            _timer = new Timer(CallSync, null, TimeSpan.Zero, TimeSpan.FromSeconds(syncInterval));
            Console.WriteLine($"Syncronization timer started with an interval of {syncInterval}");
        }

        private void CallSync(object? state)
        {
            _folderSynchronization.SynchronizeFolders();
        }

    }
}
