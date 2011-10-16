using System;
using System.Collections.Generic;

namespace DirectoryWiz.Framework
{
    public class DirectoryViewer
    {
        private IFolderNavigator _navigator;

        public List<string> Files { get; set; }
        public List<string> Folders{ get; set;} 

        public DirectoryViewer(IFolderNavigator folderNavigator)
        {
            _navigator = folderNavigator;
            _navigator.FileVisited += NavigatorFileVisited;
            _navigator.FolderVisited += NavigatorFolderVisited;
        }

        public void Scan(string root)
        {
            ClearTheFileAndFolderLists();

            _navigator.ScanFolder(root);

            PreventActingOnParentFoldersBeforeChildFolders();
        }

        private void ClearTheFileAndFolderLists()
        {
            Files = new List<string>();
            Folders = new List<string>();
        }

        private void PreventActingOnParentFoldersBeforeChildFolders()
        {
            Folders.Sort(new DescendingStringComparer());
            Files.Sort(new DescendingStringComparer());
        }

        void NavigatorFolderVisited(object sender, FolderVisitedEventArgs e)
        {
            Folders.Add(e.FullFolderPath);
        }

        void NavigatorFileVisited(object sender, FileVisitedEventArgs e)
        {
            Files.Add(e.FullPath);
        }
    }
}
