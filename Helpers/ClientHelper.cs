using Discord;
using Discord.WebSocket;

namespace TestBot.Handlers;

static class ClientHelper
{
    public static async Task StartClient(DiscordSocketClient client, string token)
    {
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
    }

    public static DiscordSocketClient CreateClient(Func<LogMessage, Task>? logger = null)
    {
        var client = new DiscordSocketClient();

        if (logger is not null)
            client.Log += logger;

        return client;
    }

    public static DiscordSocketClient CreateClient(IEnumerable<Func<LogMessage, Task>> loggers)
    {
        var client = new DiscordSocketClient();

        foreach (var logger in loggers)
        {
            client.Log += logger;
        }

        return client;
    }
}
