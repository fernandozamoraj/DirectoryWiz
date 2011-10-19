namespace DirectoryWiz.Framework.CommandLineHelpers.Handlers
{
    public class CopyHandler : BaseHandler, ICommandLineHandler
    {
        private bool IsCopy(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.ToLower() == _commandLiterals.CopyCommand)
                {
                    return true;
                }
            }

            return false;
        }

        public void HandleRequest(string[] args, IDivLogger logger)
        {
            _logger = logger;

            if(IsCopy(args))
            {
                _logger.Log("Not yet implemented better luck next time", LogSeverity.Medium);
            }
            else
            {
                InvokeSuccessor(args, logger);
            }
        }
    }
}