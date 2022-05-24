using Discord.Commands;
using Discord.WebSocket;
using TestBot.Data;
using TestBot.Models;
using TestBot.Services;

namespace TestBot.Events;

public class MessageRecieved
{
    public const string DefaultPrefix = "!test ";

    public static string[] Prefixes = new string[] { DefaultPrefix };

    private const byte MaxExp = 10;
    private const byte MinExp = 5;

    private static Random RandomGenerator = new Random();

    public static async Task CheckForCommand(SocketMessage rawMessage)
    {
        var message = rawMessage as SocketUserMessage;

        if (message is null)
            return;

        if (Prefixes.Length == 0 || message.Content.Length == 0)
            return;
        
        if (message.Author.IsBot)
            return;

        int argPos = 0;

        if (!Prefixes.Any(prefix => message.HasStringPrefix(prefix, ref argPos))
            && !message.HasMentionPrefix(Program.Client?.CurrentUser, ref argPos))
            return;

        var context = new SocketCommandContext(Program.Client, message);
        var result = await Program.Commands.ExecuteAsync(context, argPos, Program.Services);
        
        if(!result.IsSuccess)
            await context.Channel.SendMessageAsync(result.ErrorReason);
    }
    
    public static async Task AddExperience(SocketMessage rawMessage)
    {
        var service = Program.Services?.GetService(typeof(UserService)) as UserService;
        var message = rawMessage as SocketUserMessage;

        if (message is null || service is null)
            return;

        byte exp = (byte)RandomGenerator.Next(MinExp, MaxExp);

        var user = await service.GetById(message.Author.Id);

        if (user is null)
            user = await service.AddUser(message.Author.Id);

        await service.AddExperience(user.Id, exp);
    }

    private static int ExperienceToLevel(uint exp)
    {
        return (int)Math.Pow(exp / 42, 0.55);
    }
}
