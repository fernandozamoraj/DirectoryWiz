using System;

namespace DirectoryWiz.Framework
{
    public class FileVisitedEventArgs : EventArgs
    {
        public string FullPath { get; set; }
    }
}