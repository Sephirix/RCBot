using System;
using Discord;
using System.Linq;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using RCBot.Modules;

namespace RCBot.Modules
{
    public class GeneralModule : CustomBase
    {
        public CommandService CommandService { get; set; }

        //[Command("Ping")]
        //public async Task PingAsync()
        //{
        //    var old = Environment.TickCount;
        //    var message = await ReplyAsync("*Hold your horses ...*");
        //    await message.ModifyAsync(x =>
        //        x.Content = $"💓 Latency: {Context.Client.Latency} ms\n⌛ Response: {Environment.TickCount - old} ms");
        //}

        //[Command("Avatar")]
        //public Task Avatar(IUser user = null) =>
        //    ReplyAsync((user ?? Context.User).GetAvatarUrl(size: 2048));

        [Command("Commands"), Alias("Cmds", "Cmd") , Summary("List of Commands")]
        public Task Commands()
        {
            var builder = new StringBuilder();
            builder.AppendLine("```css");

            foreach (var module in CommandService.Modules.OrderBy(x => x.Name))
            {
                module.Name.Replace("CustomBase", "");
                var moduleName = module.Name.Replace("Module", "");
                var padLength = CommandService.Modules.Max(x => x.Name.Length);
                var colon = ":".PadLeft(padLength - moduleName.Length + 2);
                var commands = string.Join("\n", module.Commands.Select(x => $"{x.Name} - {x.Summary}"));
                builder.AppendLine($"**{moduleName}**{colon}\n{commands}");
            }

            builder.AppendLine("```");
            return ReplyAsync($"{builder}");
        }
    }
}