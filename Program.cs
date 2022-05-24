using System.Collections.ObjectModel;
using Discord.WebSocket;
using Discord;
using TestBot.Loggers;
using TestBot.Events;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using TestBot.Handlers;
using System.Reflection;

namespace TestBot;

class Program
{
    public static DiscordSocketClient? Client
    {
        get;
        private set;
    }

    public static CommandService Commands
    {
        get;
        private set;
    } = new CommandService();

    public static ServiceProvider? Services
    {
        get;
        private set;
    }

    private static Task Main(string[] args) => new Program().MainAsync(args);

    public async Task MainAsync(string[] args)
    {
        var tokens = GetTokens();

        Client = ClientHelper.CreateClient(BasicLogger.LogToConsole);

        Client.MessageReceived += MessageRecieved.CheckForCommand;

        Services = new ServiceCollection()
            .AddSingleton(this)
            .AddSingleton(Client)
            .AddSingleton(Commands)
            .BuildServiceProvider();

        await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);

        await ClientHelper.StartClient(Client, tokens["main"]);
        
        await Task.Delay(-1);
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
