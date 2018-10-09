
using System;
using Discord.Commands;
using Discord.WebSocket;



namespace RCBot.Modules
{
    public class CustomBase : ModuleBase<CustomContext>
    {
        private Random Random => new Random();
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