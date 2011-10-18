using System;
using DirectoryWiz.Framework.Api;

namespace DirectoryWiz.Framework.CommandLineHelpers
{
    public class CommandLineHandler
    {
        private Action _handler;
        private IDivLogger _logger;
        private CommandLineLiterals _commandLiterals = new CommandLineLiterals();

        public void PrepareHandler(string[] args)
        {
            try
            {
                if(IsHelp(args))
                {
                    throw new CommandLineEntryException("Help Information", null);
                }

                if (IsRemove(args))
                {
                    if (IsRemoveByExtensions(args))
                    {
                        RemoveByExtensions(GetRootDirectory(args, 2), GetExtensions(args, 3));
                    }
                    else if (IsRemoveByFileNames(args))
                    {
                        RemoveByFileNames(GetRootDirectory(args, 2), GetExtensions(args, 3));
                    }
                    else
                    {
                        throw new CommandLineEntryException("Remove command has some invalid entries", null);
                    }
                }
                else if (IsCopy(args))
                {
                    throw new CommandLineEntryException("Copy command has is not yet available", null);
                }
                else
                {
                    throw new CommandLineEntryException("Usage", null);
                }

            }
            catch(CommandLineEntryException exception)
            {
                this._handler = () =>
                                    {
                                        _logger.Log(exception.Message);
                                        _logger.Log(CommandLineUsage.Usage);
                                    };
            }
            catch (Exception exception)
            {
                this._handler = () =>
                {
                    _logger.Log(exception.Message);
                };
            }
        }

        public void Handle(string[] args, IDivLogger divLogger)
        {
            _logger = divLogger;

            try
            {
                PrepareHandler(args);
                _handler.Invoke();
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
            }
        }

        private bool IsHelp(string[] args)
        {
            foreach(var arg in args)
            {
                if(arg == _commandLiterals.HelpCommand)
                    return true;
            }

            return false;
        }

        private bool IsCopy(string[] args)
        {
            foreach(string arg in args)
            {
                if(arg.ToLower() == _commandLiterals.CopyCommand)
                {
                    return true;
                }
            }

            return false;
        }

        private string[] GetExtensions(string[] args, int indexOfArg)
        {
            if(args.Length < indexOfArg)
                return new string[0];

            string[] extensions = args[indexOfArg].Split(' ');

            return extensions;
        }

        private string GetRootDirectory(string[] args, int indexOfArg)
        {
            if (args.Length < indexOfArg)
                return string.Empty;

            return args[indexOfArg];
        }

        private void RemoveByFileNames(string rootDirectory, string[] fileNames)
        {
            _handler = () =>
            {

                GeneralFileRemover fileRemover = new GeneralFileRemover();
                fileRemover.OnProgress += (sender, e) => _logger.Log(e.Message);
                fileRemover.ErrorOccurred += (sender, e) => _logger.Log(e.Message);
                fileRemover.RemoveFileByName(rootDirectory, fileNames);
                _logger.Log("Process completed successfuly");
            };
        }

        private bool IsRemoveByFileNames(string[] args)
        {
            if (args.Length < 2)
                return false;

            if(args[0].ToLower() == _commandLiterals.RemoveCommand &&
               args[1].ToLower() == _commandLiterals.FileNamesSwitch)
            {
                return true;
            }

            return false;
        }

        private void RemoveByExtensions(string rootDirectory, string[] extensions)
        {
            _handler = () =>
                               {

                                   GeneralFileRemover fileRemover = new GeneralFileRemover();
                                   fileRemover.OnProgress += (sender, e) => _logger.Log(e.Message);
                                   fileRemover.ErrorOccurred += (sender, e) => _logger.Log(e.Message);
                                   fileRemover.RemoveFileByExtensions(rootDirectory, extensions);
                                   _logger.Log("Process completed successfuly");

                               };
        }

        private bool IsRemoveByExtensions(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.ToLower() == _commandLiterals.FileExtensionsSwitch)
                {
                    return true;
                }
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
