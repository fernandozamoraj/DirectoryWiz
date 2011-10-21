using System;
using System.IO;
using DirectoryWiz.Framework.MatchEvaluators;

namespace DirectoryWiz.Framework.Api
{
    public class FileCopier : FileActionBase, IFileCopier
    {
        public FileCopier(IFolderNavigator navigator, IDirectory directory)
        {
            _directoryViewer = new DirectoryViewer(navigator);
            _directory = directory;
        }

        public void CopyStructure(string sourceFolder, string targetFolder)
        {
            _directoryViewer.Scan(sourceFolder);

            foreach (string folder in _directoryViewer.Folders)
            {
                bool cancel = false;

                HandleFileSystemAcion(() => _directory.CreateFolder(folder.Replace(sourceFolder, targetFolder)),
                                       folder,
                                       "Error occured while copying folder " + folder,
                                       "Created folder " + folder,
                                       out cancel
                                      );

                if(cancel)
                    break;
            }
        }

        public void CopyFull(string sourceFolder, string targetFolder)
        {
            CopyStructure(sourceFolder, targetFolder);

            foreach (string file in _directoryViewer.Files)
            {
                bool cancel = false;

                HandleFileSystemAcion(() => _directory.CopyFile(file, file.Replace(sourceFolder, targetFolder)),
                                       file,
                                       "Error occured while copying file " + file,
                                       "Copied file " + file,
                                       out cancel
                                       );

                if(cancel)
                    break;
            }
        }
        
        public void CopyFullToFlat(string sourceFolder, string targetFolder)
        {
            _directory.CreateFolder(targetFolder);

            foreach (string file in _directoryViewer.Files)
            {
                bool cancel = false;

                FileInfo fileInfo = new FileInfo(file);

                HandleFileSystemAcion(() => _directory.CopyFile(file, targetFolder + "\\" + fileInfo.Name),
                       file,
                       "Error occured while copying file " + file,
                       "Copied file " + file,
                       out cancel
                      );

                if (cancel)
                    break;
            }
        }

        public void CopyFullToFlat(string sourceFolder, string targetFolder, string[] fileExtensions)
        {
            _directory.CreateFolder(targetFolder);

            MatchEvaluators.MatchByFileExtensionEvaluator evaluator = new MatchByFileExtensionEvaluator(fileExtensions);

            foreach (string file in _directoryViewer.Files)
            {
                if (evaluator.IsMatch(file))
                {
                    bool cancel = false;
                    FileInfo fileInfo = new FileInfo(file);

                    HandleFileSystemAcion(() => _directory.CopyFile(file, targetFolder + "\\" + fileInfo.Name),
                       file,
                       "Error occured while copying file " + file,
                       "Copied file " + file,
                       out cancel
                      );

                    if(cancel)
                        break;
                }
            }
        }

        private DirectoryViewer _directoryViewer;
        private IDirectory _directory;
    }

    public class FileActionEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string FileOrFolder { get; set; }
        public bool CancelProcess { get; set; }
    }
}