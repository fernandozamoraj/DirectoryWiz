using System;

namespace DirectoryWiz.Framework.CommandLineHelpers
{
    public class CommandLineEntryException : ApplicationException
    {
        public CommandLineEntryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}