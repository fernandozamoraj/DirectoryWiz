using DirectoryWiz.Framework.MatchEvaluators;

namespace DirectoryWiz.Framework.Api
{
    public class FileRemover : FileActionBase, IFileRemover
    {
        private DirectoryViewer _directoryViewer;
        private IDirectory _directory;

        public FileRemover(IFolderNavigator navigator, IDirectory directory)
        {
            _directoryViewer = new DirectoryViewer(navigator);
            _directory = directory;
        }

        public void RemoveFileByExtensions(string root, string[] extensions)
        {
            MatchByFileExtensionEvaluator evaluator = new MatchByFileExtensionEvaluator(extensions);

            RemoveFilesByCustomEvaluator(root, evaluator);
        }

        public void RemoveFileByName(string root, string[] names)
        {
            MatchFileByNameEvaluator evaluator = new MatchFileByNameEvaluator(names);

            RemoveFilesByCustomEvaluator(root, evaluator);
        }

        public void RemoveFolderByName(string root, string[] names)
        {
            MatchFolderByNameEvaluator evaluator = new MatchFolderByNameEvaluator(names);

            RemoveFoldersByCustomEvaluator(root, evaluator);
        }

        public void RemoveByRegularExpression(string root, string pattern)
        {
            MatchFolderByRegularExpressionEvaluator evaluator = new MatchFolderByRegularExpressionEvaluator(pattern);

            RemoveFoldersByCustomEvaluator(root, evaluator);
        }

        public void RemoveFilesByCustomEvaluator(string rootFolder, IMatchFileByEvaluator evaluator)
        {
            _directoryViewer.Scan(rootFolder);

            foreach(string file in _directoryViewer.Files)
            {
                if(evaluator.IsMatch(file))
                {
                    bool cancel = false;

                    HandleFileSystemAcion(()=> _directory.DeleteFile(file), 
                        file,
                        "Failed to delete file " + file,
                        "Deleted file " + file,
                        out cancel);
                    
                    if(cancel)
                        break;
                }
            }
        }

        public void RemoveFoldersByCustomEvaluator(string rootFolder, IMatchFileByEvaluator evaluator)
        {
            _directoryViewer.Scan(rootFolder);

            foreach (string folder in _directoryViewer.Folders)
            {
                if (evaluator.IsMatch(folder))
                {
                    bool cancel = false;

                    foreach (string file in _directory.GetFiles(folder))
                    {
                        HandleFileSystemAcion(() => _directory.DeleteFile(file),
                                              folder,
                                              "Failed to delete file " + file,
                                              "Deleted file " + file,
                                              out cancel);

                        if(cancel)
                            break;
                    }

                    if(cancel)
                        break;

                    HandleFileSystemAcion(() => _directory.DeleteFolder(folder),
                                          folder,
                                          "Failed to delete folder " + folder,
                                          "Deleted folder " + folder,
                                          out cancel);
                    if(cancel)
                        break;
                }
            }
        }
    }
}