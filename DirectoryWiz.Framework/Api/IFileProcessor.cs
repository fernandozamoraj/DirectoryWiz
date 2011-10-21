using System;

namespace DirectoryWiz.Framework.Api
{
    public interface IFileProcessor
    {
        event EventHandler<FileActionEventArgs> OnProgress;
        event EventHandler<FileActionEventArgs> ErrorOccurred;
    }
}