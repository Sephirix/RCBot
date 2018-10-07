using Discord;
using System;
using Discord.Commands;
using Discord.WebSocket;

using System.Threading.Tasks;
using Discord.Rest;


namespace RCBot.Modules
{
    public class CustomBase : ModuleBase<CustomContext>
    {

        private Random Random => new Random();
 

      
        protected EmbedBuilder BuildEmbed()
            => new EmbedBuilder()
                .WithColor(new Color(Random.Next(255), Random.Next(255), Random.Next(255)))
                .WithAuthor(Context.Guild.CurrentUser.Username, Context.Guild.CurrentUser.GetAvatarUrl());

      

        protected Task ThumbUp
            => Context.Message.AddReactionAsync(Emote.Parse("<:ThumbUp:496687272624914432>"));

        protected Task ThumbDown
            => Context.Message.AddReactionAsync(Emote.Parse("<:ThumbDown:496690573118275624>"));
    }

    public class CustomContext : SocketCommandContext
    {
        public new SocketGuildUser User { get; }
     

        public CustomContext(DiscordSocketClient client, SocketUserMessage msg) : base(
            client, msg)
        {
            
            User = msg.Author as SocketGuildUser;
        }
    }
}