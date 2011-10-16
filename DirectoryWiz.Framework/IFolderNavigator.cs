using System;

namespace DirectoryWiz.Framework
{
    public interface IFolderNavigator
    {
        event EventHandler<FileVisitedEventArgs> FileVisited;
        event EventHandler<FolderVisitedEventArgs> FolderVisited;
        void ScanFolder(string root);
    }
}