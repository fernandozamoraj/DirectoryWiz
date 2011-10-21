using System;
using DirectoryWiz.Framework.Api;

namespace DirectoryWiz.Framework.CommandLineHelpers.Handlers
{
    public class RemoveHandler : BaseHandler, ICommandLineHandler
    {
        public IFileRemover _fileRemover;

        public RemoveHandler(IFileRemover fileRemover)
        {
            _fileRemover = fileRemover;
        }

        public void HandleRequest(string[] args, IDivLogger logger)
        {
            _logger = logger;

            if (IsRemoveByExtensions(args))
            {
                RemoveByExtensions(GetRootDirectory(args, 2), FromOneArgumentToTokens(args, 3));
            }
            else if (IsRemoveByFileNames(args))
            {
                RemoveByFileNames(GetRootDirectory(args, 2), FromOneArgumentToTokens(args, 3));
            }
            else if (IsRemoveByFolderName(args))
            {
                RemoveByFolderName(GetRootDirectory(args, 2), FromOneArgumentToTokens(args, 3));
            }
            else if(IsRemoveByRegularExpression(args))
            {
                RemoveByRegularExpression(GetRootDirectory(args, 2), GetArgument(args, 3));
            }
            else
            {
                InvokeSuccessor(args, logger);
            }
        }

        private void RemoveByFileNames(string rootDirectory, string[] fileNames)
        {
            RemoveTemplate( x => x.RemoveFileByName(rootDirectory, fileNames));
        }

        private void RemoveByFolderName(string rootDirectory, string[] folderNames)
        {
           RemoveTemplate(x => x.RemoveFolderByName(rootDirectory, folderNames));
        }

        private void RemoveByExtensions(string rootDirectory, string[] extensions)
        {
            RemoveTemplate(x => x.RemoveFileByExtensions(rootDirectory, extensions));
        }

        private void RemoveByRegularExpression(string rootDirectory, string pattern)
        {
            RemoveTemplate(x => x.RemoveByRegularExpression(rootDirectory, pattern));
        }

        private void RemoveTemplate(Action<IFileRemover> removeAction)
        {
            //Have to be carefull with this because if the removehandler does multiple removes
            //this events will be wired multiple times
            _fileRemover.OnProgress += (sender, e) => _logger.Log(e.Message, LogSeverity.Medium);
            _fileRemover.ErrorOccurred += (sender, e) => _logger.Log(e.Message, LogSeverity.Highest);

            removeAction.Invoke(_fileRemover);

            _logger.Log("Process completed successfuly", LogSeverity.Low);
        }

        private bool IsRemoveByFileNames(string[] args)
        {
            return IsAppropriateRemove(args, _commandLiterals.FileNamesSwitch);
        }

        private bool IsRemoveByRegularExpression(string[] args)
        {
            return IsAppropriateRemove(args, _commandLiterals.RegularExpressionSwitch);
        }

        private bool IsRemoveByFolderName(string[] args)
        {
            return IsAppropriateRemove(args, _commandLiterals.FolderNamesSwitch);
        }

        private bool IsRemoveByExtensions(string[] args)
        {
            return IsAppropriateRemove(args, _commandLiterals.FileExtensionsSwitch);
        }

        private bool IsAppropriateRemove(string[] args, string commandSwitch)
        {
            if (args.Length < 2)
                return false;

            if (args[0].ToLower() == _commandLiterals.RemoveCommand &&
                args[1].ToLower() == commandSwitch)
            {
                return true;
            }

            return false;
        }
    }
}