namespace DirectoryWiz.Framework.Api
{
    /// <summary>
    /// Purpose of this class is to eliminate the hassle of dependency overhead in constructor
    /// </summary>
    public class GeneralFileCopier : FileCopier
    {
        public GeneralFileCopier(): base(new FolderNavigator(new DirectoryImpl()), new DirectoryImpl() )
        {
            
        }
    }
}