using Discord;
using KrileDotNet;

var discordClientManager = new DiscordClientManager();
var botToken = Environment.GetEnvironmentVariable("DISCORD_TOKEN");

await discordClientManager.Client.LoginAsync(TokenType.Bot, botToken);
await discordClientManager.Client.StartAsync();

await Task.Delay(-1);