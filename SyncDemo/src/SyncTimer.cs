namespace SyncDemo.src
{
    internal class SyncTimer : IDisposable
    {
        private readonly FolderSynchronization _folderSynchronization;
        private readonly Timer _timer;
        private bool _disposed = false;

        public SyncTimer(FolderSynchronization folderSynchronization, int syncInterval)
        {
            _folderSynchronization = folderSynchronization;
            _timer = new Timer(CallSync, null, TimeSpan.Zero, TimeSpan.FromSeconds(syncInterval));
        }

        private void CallSync(object? state)
        {
            _folderSynchronization.SynchronizeFolders();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                _timer.Dispose();
                _disposed = true;
            }
        }
    }
}
