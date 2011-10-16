using System;

namespace DirectoryWiz.Framework.Api
{
    public class FileActionBase 
    {
        //Public API
        public event EventHandler<FileActionEventArgs> OnProgress;
        public event EventHandler<FileActionEventArgs> ErrorOccurred;

        protected void InvokeErrorOccurred(string fileOrFolder, string message, out bool cancel)
        {
            cancel = false;

            if (ErrorOccurred != null)
            {
                FileActionEventArgs fileActionEventArgs = new FileActionEventArgs { FileOrFolder = fileOrFolder, Message = message };
                ErrorOccurred(this, fileActionEventArgs);

                cancel = fileActionEventArgs.CancelProcess;
            }
        }

        protected void InvokeOnProgress(string fileOrFolder, string message)
        {
            if (OnProgress != null)
            {
                FileActionEventArgs fileActionEventArgs = new FileActionEventArgs { FileOrFolder = fileOrFolder, Message = message };
                OnProgress(this, fileActionEventArgs);
            }
        }

        protected void HandleFileSystemAcion(Action action, string fileOrFolder, string errorMessage, string successMessage, out bool cancel)
        {
            cancel = false;

            try
            {
                action.Invoke();
                InvokeOnProgress(fileOrFolder, successMessage);
            }
            catch (Exception exception)
            {
                InvokeErrorOccurred(fileOrFolder, errorMessage + ". Additional information: " + exception.Message, out cancel);
            }

            if (cancel)
            {
                InvokeOnProgress(fileOrFolder,
                                 "Action cancelled by calling program after exception with " + fileOrFolder);
            }
        }
    }
}