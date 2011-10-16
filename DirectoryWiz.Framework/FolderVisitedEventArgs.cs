using System;

namespace DirectoryWiz.Framework
{
    public class FolderVisitedEventArgs : EventArgs
    {
        public string FullFolderPath
        {
            get; set;
        }
    }
}