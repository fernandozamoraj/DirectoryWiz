namespace DirectoryWiz.Framework.Api
{
    public interface IFileCopier
    {
        void CopyStructure(string sourceFolder, string targetFolder);
        void CopyFull(string sourceFolder, string targetFolder);
        void CopyFullToFlat(string sourceFolder, string targetFolder);
        void CopyFullToFlat(string sourceFolder, string targetFolder, string[] fileExtensions);
    }
}