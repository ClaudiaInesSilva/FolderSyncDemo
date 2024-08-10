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
        }

        private void CallSync(object? state)
        {
            _folderSynchronization.SynchronizeFolders();
        }
    }
}
