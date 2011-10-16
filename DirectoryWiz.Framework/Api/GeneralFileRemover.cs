namespace DirectoryWiz.Framework.Api
{
    public class GeneralFileRemover: FileRemover
    {
        public GeneralFileRemover():
            base(new FolderNavigator(new DirectoryImpl()), new DirectoryImpl())
        {
        }
    }
}