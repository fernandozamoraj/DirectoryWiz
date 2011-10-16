using System;
using System.Collections.Generic;
using DirectoryWiz.Framework;
using NUnit.Framework;

namespace DirectoryWiz.UnitTests
{

    public class FolderNavigatorTestsContext
    {
        protected FolderNavigator Navigator;
        
        [TestFixtureSetUp]
        protected virtual void Context()
        {

        }
    }

    [TestFixture]
    public class When_navigator_scans_folder_with_two_files : FolderNavigatorTestsContext
    {
        private List<string> _filesVisited;
        private int _eventHandlerCalledTimes = 0;

        protected override void  Context()
        {
 	         base.Context();

            _filesVisited = new List<string>();

            FakeDirectory fakeDirectory = new FakeDirectory();
            fakeDirectory.SetFiles(new[]{"C:\\foo.txt", "C:\\bar.txt"});

            Navigator = new FolderNavigator(fakeDirectory);
            Navigator.FileVisited += NavigatorOnFileVisited;
            Navigator.ScanFolder("C:\\");
        }

        private void NavigatorOnFileVisited(object sender, FileVisitedEventArgs e)
        {
            _eventHandlerCalledTimes++;
            _filesVisited.Add(e.FullPath);
        }

        [Test]
        public void Should_notify_listener()
        {
            Assert.IsTrue(_eventHandlerCalledTimes > 0, "Should have called event handler");
        }

        [Test]
        public void Should_have_visited_two_files()
        {
            Assert.AreEqual(2, _eventHandlerCalledTimes, "Should have been called twice");
        }

        [Test]
        public void Should_contain_the_two_files()
        {
            Assert.IsTrue(_filesVisited.Contains("C:\\foo.txt"));
            Assert.IsTrue(_filesVisited.Contains("C:\\bar.txt"));
        }

    }

    [TestFixture]
    public class When_navigator_scans_nested_folders : FolderNavigatorTestsContext
    {
        private List<string> _filesVisited;
        private int _eventHandlerCalledTimes = 0;

        protected override void Context()
        {
            base.Context();

            _filesVisited = new List<string>();

            FakeNestedDirectory fakeDirectory = new FakeNestedDirectory();
            fakeDirectory.SetFiles(new[] { "foo.txt", "bar.txt", "foobar.txt" });

            Navigator = new FolderNavigator(fakeDirectory);
            Navigator.FileVisited += NavigatorOnFileVisited;
            Navigator.ScanFolder("C:");
        }

        private void NavigatorOnFileVisited(object sender, FileVisitedEventArgs e)
        {
            _eventHandlerCalledTimes++;
            _filesVisited.Add(e.FullPath);
        }

        [Test]
        public void Should_notify_listener()
        {
            Assert.IsTrue(_eventHandlerCalledTimes > 0, "Should have called event handler");
        }

        [Test]
        public void Should_navigate_folders_recursively()
        {
            Assert.AreEqual(3, _eventHandlerCalledTimes, "Should have been called twice");
        }

        [Test]
        public void Should_contain_the_three_files_each_with_different_folder()
        {
            Assert.IsTrue(_filesVisited.Contains("C:\\foo.txt"));
            Assert.IsTrue(_filesVisited.Contains("0\\bar.txt"));
            Assert.IsTrue(_filesVisited.Contains("1\\foobar.txt"));
        }
    }

    public class FakeDirectory : IDirectory
    {
        protected string[] _files = new string[0];

        public void SetFiles(string[] files)
        {
            _files = files;
        }

        public string[] GetFiles(string folder)
        {
            return _files;
        }

        public string[] GetFolders(string folder)
        {
            return new string[0];
        }

        public void DeleteFile(string file)
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder(string folder)
        {
            throw new NotImplementedException();
        }

        public void CreateFolder(string newFolderPath)
        {
            throw new NotImplementedException();
        }

        public void CopyFile(string file, string replace)
        {
            throw new NotImplementedException();
        }
    }


    public class FakeNestedDirectory : IDirectory
    {
        protected string[] _files = new string[0];
        protected int _folderIndex = 0;

        public void SetFiles(string[] files)
        {
            _files = files;
        }

        public string[] GetFiles(string folder)
        {
            if(_folderIndex + 1 > _files.Length)
                return new string[0];

            return new string[]{string.Format("{0}\\{1}", folder,  _files[_folderIndex])};
        }

        public string[] GetFolders(string folder)
        {
            if (_folderIndex > _files.Length)
                return new string[0];

            string folderName = _folderIndex.ToString();
            _folderIndex++;
            return new[]{folderName};
        }

        public void DeleteFile(string file)
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder(string folder)
        {
            throw new NotImplementedException();
        }

        public void CreateFolder(string newFolderPath)
        {
            throw new NotImplementedException();
        }

        public void CopyFile(string file, string replace)
        {
            throw new NotImplementedException();
        }
    }
}
