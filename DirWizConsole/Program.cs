using System;
using DirectoryWiz.Framework;
using DirectoryWiz.Framework.Api;
using DirectoryWiz.Framework.CommandLineHelpers;

namespace DirWizConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            CommandLineHandler handler = new CommandLineHandler();

            handler.Handle(args, new ConsoleLogger());

            Console.ReadLine();
        }
    }

    public class ConsoleLogger : IDivLogger
    {
        public void Log(string message)
        {
            Console.Write(message);
            Console.WriteLine();
        }
    }
}
