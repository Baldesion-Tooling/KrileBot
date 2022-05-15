using NetStone;
using NetStone.Model.Parseables.Character;

namespace KrileBot.Services;

public class LodestoneService
{
    private static string? ApiKey => Environment.GetEnvironmentVariable("XIV_API_KEY");
    private static LodestoneClient? _client;

    public static async Task SetupService()
    {
        if (_client is null)
        {
            _client = await LodestoneClient.GetClientAsync(lodestoneBaseAddress: "https://na.finalfantasyxiv.com", privateKey: ApiKey);
            
        }
    }

    public async Task<LodestoneCharacter> GetCharacter(string characterId) => await _client!.GetCharacter(characterId);
}