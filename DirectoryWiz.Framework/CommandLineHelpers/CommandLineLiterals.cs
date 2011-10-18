namespace DirectoryWiz.Framework.CommandLineHelpers
{
    public class CommandLineLiterals
    {
        public string RemoveCommand{
            get { return "--remove"; }
        }

        public string HelpCommand
        {
            get { return "--help"; }
        }

        public string CopyCommand
        {
            get { return "--copy"; }
        }

        public string FileExtensionsSwitch
        {
            get { return "-e"; }
        }

        public string FileNamesSwitch
        {
            get { return "-n"; }
        }
    }
}