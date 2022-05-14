using Discord;
using KrileDotNet;
using KrileDotNet.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuild = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<DiscordClientService>();
    });

await hostBuild.RunConsoleAsync().ConfigureAwait(false);