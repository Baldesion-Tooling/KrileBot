using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace KrileBot.Commands;

public class Activities : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("activities", "Starts an activity in a voice channel")]
    public async Task StartActivity(ActivityList activity, [ChannelTypes(ChannelType.Voice)] IChannel channel)
    {
        try
        {
            var commandInteraction = (SocketSlashCommand)Context.Interaction;
            var activitySelection = commandInteraction.Data.Options.First(c => c.Name == "activity").Value;

            var voiceChannel = (SocketVoiceChannel)Context.Guild.GetChannel(channel.Id);
            var invite = await voiceChannel.CreateInviteToApplicationAsync((ulong)activity, 7200);

            var joinButton = new ButtonBuilder()
                .WithStyle(ButtonStyle.Link)
                .WithUrl(invite.Url)
                .WithLabel("Join Activity");
            var response = new ComponentBuilder().WithButton(joinButton);
            await RespondAsync($"And we are good to go! To join the `{Helpers.FormatStringWithSpaces(activitySelection)}` activity in <#{voiceChannel.Id}>, please press the button below. Enjoy!", components: response.Build());
        }
        catch (Exception e)
        {
            Console.WriteLine(new LogMessage(LogSeverity.Error, "Commands", e.Message));
            await RespondAsync("Unable to start activity. Please ensure I have permission to create invites.", ephemeral: true);
        }
    }

    public enum ActivityList : ulong
    {
        [ChoiceDisplay("Watch Together (YouTube)")]
        WatchTogether = DefaultApplications.Youtube,
        [ChoiceDisplay("Letter League (Scrabble)")]
        LetterLeague = DefaultApplications.LetterTile,
        [ChoiceDisplay("Sketch Heads (formally Doodle Crew)")]
        DoodleCrew = DefaultApplications.DoodleCrew,
        [ChoiceDisplay("Poker Night")]
        PokerNight = DefaultApplications.Poker,
        [ChoiceDisplay("Chess in the Park")]
        ChessInThePark = DefaultApplications.Chess,
        [ChoiceDisplay("Checkers in the Park")]
        CheckersInThePark = DefaultApplications.Checkers,
        [ChoiceDisplay("Blaze 8s (formally Ocho)")]
        Blaze8 = 832025144389533716,
    }
}