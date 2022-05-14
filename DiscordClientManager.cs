using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace KrileDotNet;

public class DiscordClientManager
{
    private static DiscordSocketClient? _client;
    private static InteractionService? _interactionService;
    private static CommandService? _commandService;
    private static IServiceProvider? _serviceProvider;
    public DiscordSocketClient Client => _client!;

    public DiscordClientManager()
    {
        if (_client is null)
        {
            _client = new DiscordSocketClient();
            _client.Log += LogDiscordMessage;
            _client.Ready += ClientReady;
        }

        if (_interactionService is null)
        {
            _interactionService = new InteractionService(_client.Rest);
        }

        if (_commandService is null)
        {
            _commandService = new CommandService();
        }
        
        _serviceProvider = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commandService)
            .AddSingleton(_interactionService)
            .BuildServiceProvider();
    }

    private static async Task ClientReady()
    {
        try
        {
            await _interactionService!.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
            var guildId = Environment.GetEnvironmentVariable("GUILD_ID");
            if (guildId is not null)
            {
                await LogDiscordMessage(new LogMessage(LogSeverity.Info, "Startup", "Initializing Guild Commands"));
                await _interactionService!.RegisterCommandsToGuildAsync(Convert.ToUInt64(guildId), true);
            }
            else
            {
                await LogDiscordMessage(new LogMessage(LogSeverity.Info, "Startup", "Initializing Global Commands"));
                await _interactionService!.RegisterCommandsGloballyAsync(true);
            }
        }
        catch (HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }

        _client!.SlashCommandExecuted += async interaction =>
        {
            var scope = _serviceProvider!.CreateScope();
            var ctx = new SocketInteractionContext(_client, interaction);
            await _interactionService!.ExecuteCommandAsync(ctx, scope.ServiceProvider);
        };
    }

    private static Task LogDiscordMessage(LogMessage message)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}