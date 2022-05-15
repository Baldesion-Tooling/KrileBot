using Discord.Interactions;
using KrileBot.Services;

namespace KrileBot.Commands;

[Group("lodestone", "Access player data from lodestone")]
public class Lodestone : InteractionModuleBase<SocketInteractionContext>
{
    private LodestoneService _service;

    public Lodestone(LodestoneService service)
    {
        _service = service;
    }

    [SlashCommand("player", "Gets basic player data from lodestone")]
    public async Task Get(string characterId)
    {
        var temp = await _service.GetCharacterPortrait(characterId);
        await RespondAsync(temp);
    }
}