namespace DirectoryWiz.Framework.CommandLineHelpers
{
    public interface IDivLogger
    {
        void Log(string message, LogSeverity logSeverity);
    }

    public enum LogSeverity
    {
        Lowest,
        Low,
        Medium,
        High,
        Highest
    }
}