﻿using System;

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

        public string FolderNamesSwitch
        {
            get { return "-fn"; }
        }

        public string RegularExpressionSwitch
        {
            get { return "-rx"; }
        }
    }
}