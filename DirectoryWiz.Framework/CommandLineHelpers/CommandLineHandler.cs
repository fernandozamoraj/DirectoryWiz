using System;
using DirectoryWiz.Framework.Api;

namespace DirectoryWiz.Framework.CommandLineHelpers
{
    public class CommandLineHandler
    {
        private Action _handler;
        private IDivLogger _logger;

        public bool CanHandle(string[] args)
        {
            try
            {
                if (IsRemove(args))
                {
                    if (IsRemoveByExtensions(args))
                    {
                        RemoveByExtensions(GetRootDirectory(args), GetExtensions(args));
                    }
                    else if (IsRemoveByFileNames(args))
                    {
                        RemoveByFileNames(GetRootDirectory(args), GetExtensions(args));
                    }
                    else
                    {
                        throw new Exception("Cannot handle removal");
                    }
                }
                else if (IsCopy(args))
                {

                }

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void Handle(IDivLogger divLogger)
        {
            _logger = divLogger;
            _handler.Invoke();
        }

        private bool IsCopy(string[] args)
        {
            foreach(string arg in args)
            {
                if(arg.ToLower() == "--copy")
                {
                    return true;
                }
            }

            return false;
        }

        private string[] GetExtensions(string[] args)
        {
            return new string[3];
        }

        private string GetRootDirectory(string[] args)
        {
            throw new NotImplementedException();
        }

        private void RemoveByFileNames(object getRootDirectory, object getExtensions)
        {
            throw new NotImplementedException();
        }

        private bool IsRemoveByFileNames(string[] args)
        {
            throw new NotImplementedException();
        }

        private void RemoveByExtensions(string rootDirectory, string[] extensions)
        {
            _handler = () =>
                               {

                                   GeneralFileRemover fileRemover = new GeneralFileRemover();
                                   fileRemover.OnProgress += (sender, e) => _logger.Log(e.Message);
                                   fileRemover.ErrorOccurred += (sender, e) => _logger.Log(e.Message);
                                   fileRemover.RemoveFileByExtensions(rootDirectory, extensions); 
                               };
        }

        private bool IsRemoveByExtensions(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.ToLower() == "-e")
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsRemove(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.ToLower() == "--remove")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
