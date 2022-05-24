using Discord.Commands;
using Discord.WebSocket;

namespace TestBot.Events;

static class MessageRecieved
{
    public const string DefaultPrefix = "!test ";

    public static string[] Prefixes = new string[] { DefaultPrefix };
    
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
}
