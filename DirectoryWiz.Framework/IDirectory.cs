namespace DirectoryWiz.Framework
{
    public interface IDirectory
    {
        string[] GetFiles(string folder);
        string[] GetFolders(string folder);
        void DeleteFile(string file);
        void DeleteFolder(string folder);
        void CreateFolder(string newFolderPath);
        void CopyFile(string file, string replace);
    }
}