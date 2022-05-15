using KrileBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuild = new HostBuilder()
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<DiscordClientService>();
    });

await hostBuild.RunConsoleAsync().ConfigureAwait(false);