using System;

namespace DirectoryWiz.Framework.CommandLineHelpers.Handlers
{
    public class BaseHandler
    {
        protected IDivLogger _logger;
        protected CommandLineLiterals _commandLiterals = new CommandLineLiterals();

        protected string[] GetExtensions(string[] args, int indexOfArg)
        {
            if (args.Length <= indexOfArg)
                return new string[0];

            string[] extensions = args[indexOfArg].Split(' ');

            return extensions;
        }

        protected string GetRootDirectory(string[] args, int indexOfArg)
        {
            if (args.Length < indexOfArg)
                return string.Empty;

            return args[indexOfArg];
        }

        public ICommandLineHandler Successor { get; set; }

        protected void InvokeSuccessor(string[] args, IDivLogger divLogger)
        {
            if(Successor != null)
            {
                Successor.HandleRequest(args, divLogger);
            }
        }
    }
}