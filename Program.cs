using System.Collections.ObjectModel;
using Discord.WebSocket;
using Discord;
using TestBot.Loggers;

namespace TestBot;

class Program
{
    private static Task Main(string[] args) => new Program().MainAsync(args);

    public async Task MainAsync(string[] args)
    {
        var tokens = GetTokens();

        var client = await CreateClient(tokens["main"], BasicLogger.LogToConsole);

        await Task.Delay(-1);
    }

    private async Task<DiscordSocketClient> CreateClient(string token, Func<LogMessage, Task>? logger = null)
    {
        var client = new DiscordSocketClient();

        if (logger is not null)
            client.Log += logger;

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        return client;
    }

    private ReadOnlyDictionary<string, string> GetTokens(string tokenEnvPath = "./tokens.env")
    {
        var environement = File.ReadAllLines(tokenEnvPath);
        
        var tokens = new Dictionary<string, string>(environement.Length);

        foreach(var line in environement)
        {
            var split = line.Split('=');
            var name = split[0];
            var data = split[1];

            tokens.Add(name, data);
        }

        return new ReadOnlyDictionary<string, string>(tokens);
    }
}
