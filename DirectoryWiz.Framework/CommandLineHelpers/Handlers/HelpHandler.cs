namespace DirectoryWiz.Framework.CommandLineHelpers.Handlers
{
    public class HelpHandler : BaseHandler, ICommandLineHandler
    {
        public void HandleRequest(string[] args, IDivLogger logger)
        {
            _logger = logger;
            _logger.Log(CommandLineUsage.Usage, LogSeverity.Lowest);
        }
    }
}