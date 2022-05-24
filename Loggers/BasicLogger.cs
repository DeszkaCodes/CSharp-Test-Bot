using Discord;
using System.Text;

namespace TestBot.Loggers;

static class BasicLogger
{
    public static string LogToConsole(LogMessage log)
    {
        var builder = new StringBuilder("[");
        builder.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"));
        builder.Append("] ");
        builder.Append(log.Source);
        builder.Append(": ");
        builder.AppendLine(log.Message);

        return builder.ToString();
    }
}