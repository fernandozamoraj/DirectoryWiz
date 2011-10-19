using System;
using DirectoryWiz.Framework.CommandLineHelpers;
using DirectoryWiz.Framework.CommandLineHelpers.Handlers;

namespace DirWizConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            CommandLineHandler handler = new CommandLineHandler();

            handler.HandleRequest(args, new ConsoleLogger());
        }
    }

    public class ConsoleLogger : IDivLogger
    {
        public void Log(string message, LogSeverity logSeverity)
        {
            Console.ForegroundColor = DetermineColor(logSeverity);
            Console.Write(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        private ConsoleColor DetermineColor(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Lowest:
                    return ConsoleColor.Gray;
                case LogSeverity.Low:
                    return ConsoleColor.White;
                case LogSeverity.Medium:
                    return ConsoleColor.Green;
                case LogSeverity.High:
                    return ConsoleColor.Yellow;
                case LogSeverity.Highest:
                    return ConsoleColor.Red;
            }

            return ConsoleColor.White;
        }
    }
}
