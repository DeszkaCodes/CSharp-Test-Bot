using Discord.Commands;

namespace TestBot.Commands;

public class InfoModule : ModuleBase<SocketCommandContext>
{
    [Command("ping", RunMode = RunMode.Async)]
    [Summary("Returns the latency of the bot")]
    [RequireContext(ContextType.Guild)]
    public async Task Ping()
    {
        await ReplyAsync($"Latency is: {Context.Client.Latency}ms");
    }
}
