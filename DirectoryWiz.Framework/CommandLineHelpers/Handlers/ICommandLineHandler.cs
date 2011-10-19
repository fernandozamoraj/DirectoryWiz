namespace DirectoryWiz.Framework.CommandLineHelpers.Handlers
{
    public interface ICommandLineHandler
    {
        void HandleRequest(string[] args, IDivLogger logger);
        ICommandLineHandler Successor { get; set; }
        
    }
}