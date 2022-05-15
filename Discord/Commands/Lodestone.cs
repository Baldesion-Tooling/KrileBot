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
    public async Task GetProfile(string characterId)
    {
        var character = await _service.GetCharacter(characterId);
        await RespondAsync(character.Name);
    }
}