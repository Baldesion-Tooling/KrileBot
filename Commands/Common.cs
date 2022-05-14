using Discord.Interactions;

namespace KrileDotNet.Commands;

public class Common : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("echo", "Echo an input")]
    public async Task Echo(string input)
    {
        await RespondAsync(input);
    }
    
    [SlashCommand("hello", "say hello back")]
    public async Task Hello()
    {
        await RespondAsync("hi there!");
    }
    
    
}