using Discord;
using Discord.Commands;
using RCBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RCBot.Modules
{
    [Group("Profile")]
   public class ProfileModule : CustomBase
    {
        DbHelper db = new DbHelper();
        [Command, Priority(0), Summary("Display user Trade Profile.")]
        public Task Profile(IUser user = null)
        {
            
            ulong searchUser = (user == null) ? Context.User.Id : user.Id;
            var member = db.search(searchUser);
            if(member == null)
            {
                 ReplyAsync("No Trade Profile Found.");
                return Task.CompletedTask;
                
            }
            var link = (member.inviteID == "n/a" && member.inviteID == "" || member.inviteID.Length > 8) ? "No invite link available" : $"Click this [Link to Add this Person](https://game.streets.cafe/?from=" + $"{member.inviteID})";
            var need = (member.forTrade != "" || member.forTrade != null) ? member.forTrade.ToString().Replace(",", "\n") : "empty";
       
            var have = (member.stock != "" && member.forTrade != null) ? member.stock.ToString().Replace(",", "\n") : "empty";
            var name = (member.inGameName.ToString() != "") ? member.inGameName : "empty";
            EmbedBuilder embed = new EmbedBuilder();
            embed.AddField("In-Game-Name: ", name);
            embed.AddField("Invite Link: ", link);
            embed.AddField(addField("Need:", need));
            embed.AddField(addField("Stock:", have));
            return ReplyAsync("", false, embed.Build());
        }

        [Command("Create"),Alias("update"), Summary("Create user Trade Profile.")]
        public Task create( string Id="", [Remainder]string InGameName="")
        {

            db.updateProfile(Context.User.Id, InGameName, Id);
            
            return Profile();
        }

        [Command("Reset"), Summary("Reset user Trade Profile.")]
        public Task reset()
        {
            var result = db.reset(Context.User.Id);
            if (!result.Result) return Task.CompletedTask;

            return Profile();
        }

        public EmbedFieldBuilder addField(string title="", string desc ="")
        {
            var exampleField = new EmbedFieldBuilder()
                    .WithName(title)
                    .WithValue(desc)
                    .WithIsInline(true);


                    return exampleField;

          
        }
    }
}
