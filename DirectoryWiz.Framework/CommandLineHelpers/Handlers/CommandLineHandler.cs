using System;
using DirectoryWiz.Framework.Api;

namespace DirectoryWiz.Framework.CommandLineHelpers.Handlers
{
    public class CommandLineHandler : BaseHandler, ICommandLineHandler
    {
        public void PrepareHandler(string[] args)
        {
            
        }

        public void HandleRequest(string[] args, IDivLogger divLogger)
        {
            _logger = divLogger;

            try
            {
                //Chain of responsibility pattern
                RemoveHandler removeHandler = new RemoveHandler(new GeneralFileRemover());
                CopyHandler copyHandler = new CopyHandler();
                HelpHandler helpHandler = new HelpHandler();

                removeHandler.Successor = copyHandler;
                copyHandler.Successor = helpHandler;

                removeHandler.HandleRequest(args, _logger);

            }
            catch (CommandLineEntryException exception)
            {
                _logger.Log(exception.Message, LogSeverity.High);
                _logger.Log(CommandLineUsage.Usage, LogSeverity.Low);

            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message, LogSeverity.Highest);
            }
        }
    }
}
