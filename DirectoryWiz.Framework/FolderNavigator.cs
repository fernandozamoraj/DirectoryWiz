using System;

namespace DirectoryWiz.Framework
{
    public class FolderNavigator : IFolderNavigator
    {
        private IDirectory _directory;

        public event EventHandler<FileVisitedEventArgs> FileVisited;
        public event EventHandler<FolderVisitedEventArgs> FolderVisited;
        
        public FolderNavigator(IDirectory directory)
        {
            _directory = directory;
        }

        public void ScanFolder(string root)
        {
            InvokeFolderVisited(root);

            foreach(string file in _directory.GetFiles(root))
            {
                InvokeFileVisited(file);
            }

            foreach(string folder in _directory.GetFolders(root))
            {
                ScanFolder(folder);
            }
        }

        private void InvokeFolderVisited(string folder)
        {
            if(FolderVisited != null)
            {
                FolderVisited(this, new FolderVisitedEventArgs{FullFolderPath = folder});
            }
        }

        private void InvokeFileVisited(string file)
        {
            if (FileVisited != null)
            {
                FileVisited(this, new FileVisitedEventArgs { FullPath = file });
            }
        }
    }
}
