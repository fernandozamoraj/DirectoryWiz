using System.IO;

namespace DirectoryWiz.Framework
{
    public class DirectoryImpl : IDirectory
    {
        public string[] GetFiles(string fullFolderPath)
        {
            return Directory.GetFiles(fullFolderPath);
        }

        public string[] GetFolders(string fullFolderPath)
        {
            return Directory.GetDirectories(fullFolderPath);
        }

        public void DeleteFile(string filePath)
        {
            File.SetAttributes(filePath, FileAttributes.Normal);
            File.Delete(filePath);
        }

        public void DeleteFolder(string folder)
        {
            foreach(string file in GetFiles(folder))
                DeleteFile(file);

            Directory.Delete(folder);
        }

        public void CreateFolder(string newFolderPath)
        {
            Directory.CreateDirectory(newFolderPath);
        }

        public void CopyFile(string source, string target)
        {
            string adjustedTargetFileName = target;
            int i = 1;

            //If the file exists rename the destination by appending a number
            //Loop through this section until you find a file name available
            while (File.Exists(adjustedTargetFileName))
            {
                FileInfo fileInfo = new FileInfo(source);

                adjustedTargetFileName = target.Replace(fileInfo.Extension, string.Format("({0}){1}", i, fileInfo.Extension));
                i++;
            }

            File.Copy(source, adjustedTargetFileName);
        }
    }
}