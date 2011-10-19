using DirectoryWiz.Framework.Api;

namespace DirectoryWiz.Framework.CommandLineHelpers.Handlers
{
    public class RemoveHandler : BaseHandler, ICommandLineHandler
    {
        public void HandleRequest(string[] args, IDivLogger logger)
        {
            _logger = logger;

            if (IsRemoveByExtensions(args))
            {
                RemoveByExtensions(GetRootDirectory(args, 2), GetExtensions(args, 3));
            }
            else if (IsRemoveByFileNames(args))
            {
                RemoveByFileNames(GetRootDirectory(args, 2), GetExtensions(args, 3));
            }
            else if (IsRemoveByFolderName(args))
            {
                RemoveByFolderName(GetRootDirectory(args, 2), GetExtensions(args, 3));
            }
            else
            {
                InvokeSuccessor(args, logger);
            }
        }

        private void RemoveByFileNames(string rootDirectory, string[] fileNames)
        {
            GeneralFileRemover fileRemover = new GeneralFileRemover();
            fileRemover.OnProgress += (sender, e) => _logger.Log(e.Message, LogSeverity.Medium);
            fileRemover.ErrorOccurred += (sender, e) => _logger.Log(e.Message, LogSeverity.Highest);
            fileRemover.RemoveFileByName(rootDirectory, fileNames);
            _logger.Log("Process completed successfuly", LogSeverity.Low);
        }

        private void RemoveByFolderName(string rootDirectory, string[] folderNames)
        {
            GeneralFileRemover fileRemover = new GeneralFileRemover();
            fileRemover.OnProgress += (sender, e) => _logger.Log(e.Message, LogSeverity.Medium);
            fileRemover.ErrorOccurred += (sender, e) => _logger.Log(e.Message, LogSeverity.Highest);
            fileRemover.RemoveFolderByName(rootDirectory, folderNames);
            _logger.Log("Process completed successfuly", LogSeverity.Low);
        }

        private bool IsRemoveByFileNames(string[] args)
        {
            if (args.Length < 2)
                return false;

            if (args[0].ToLower() == _commandLiterals.RemoveCommand &&
                args[1].ToLower() == _commandLiterals.FileNamesSwitch)
            {
                return true;
            }

            return false;
        }

        private bool IsRemoveByFolderName(string[] args)
        {
            if (args.Length < 2)
                return false;

            if (args[0].ToLower() == _commandLiterals.RemoveCommand &&
                args[1].ToLower() == _commandLiterals.FolderNamesSwitch)
            {
                return true;
            }

            return false;
        }

        private void RemoveByExtensions(string rootDirectory, string[] extensions)
        {
            GeneralFileRemover fileRemover = new GeneralFileRemover();
            fileRemover.OnProgress += (sender, e) => _logger.Log(e.Message, LogSeverity.Medium);
            fileRemover.ErrorOccurred += (sender, e) => _logger.Log(e.Message, LogSeverity.Highest);
            fileRemover.RemoveFileByExtensions(rootDirectory, extensions);
            _logger.Log("Process completed successfuly", LogSeverity.Low);
        }

        private bool IsRemoveByExtensions(string[] args)
        {
            if (args[0].ToLower() == _commandLiterals.RemoveCommand &&
                args[1].ToLower() == _commandLiterals.FileExtensionsSwitch)
            {
                return true;
            }

            return false;
        }

        private bool IsRemove(string[] args)
        {
            if (args.Length < 1)
                return false;

            if (args[0].ToLower() == _commandLiterals.RemoveCommand)
            {
                return true;
            }

            return false;
        }
    }
}