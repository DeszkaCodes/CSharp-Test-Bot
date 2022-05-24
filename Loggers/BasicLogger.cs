using Discord;
using System.Text;

namespace TestBot.Loggers;

static class BasicLogger
{
    public static Task LogToConsole(LogMessage log)
    {
        var builder = new StringBuilder("[");
        builder.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"));
        builder.Append("] ");
        builder.Append(log.Source);
        builder.Append(": ");
        builder.Append(log.Message);

        switch (log.Severity)
        {
            case LogSeverity.Critical:
            case LogSeverity.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case LogSeverity.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case LogSeverity.Info:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case LogSeverity.Verbose:
            case LogSeverity.Debug:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
        }

        Console.WriteLine(builder.ToString());

        Console.ResetColor();

        return Task.CompletedTask;
    }
}