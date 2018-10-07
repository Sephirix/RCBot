using Discord;
using Discord.Commands;
using RCBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RCBot.Modules
{
    [Group("Ingredients")]
    public class IngredientsModule : CustomBase
    {
        DbHelper db = new DbHelper();
        ProfileModule pf = new ProfileModule();

        [Command, Priority(0), Summary("Display user Trade Profile.")]
        public Task list(IUser user=null)
        {
            ulong searchUser = (user == null) ? Context.User.Id : user.Id;
            var member = db.search(searchUser);
            if (member == null)
            {
                ReplyAsync("No Trade Profile Found.");
                return Task.CompletedTask;

            }
            var link = (member.inviteID == "n/a" || member.inviteID == "" || member.inviteID.Length > 8 || member.inviteID.Length < 6) ? "No invite link available" : $"Click this [Link to Add this Person](https://game.streets.cafe/?from=" + $"{member.inviteID})";
            var need = (member.forTrade != "" && member.forTrade != null) ? member.forTrade.ToString().Replace(" ", "\n") : "empty";
            Console.WriteLine(need+" sss");
            var have = (member.stock != "" && member.forTrade != null) ? member.stock.ToString().Replace(" ", "\n") : "empty";
            var name = (member.inGameName.ToString() != "") ? member.inGameName : "empty";
            EmbedBuilder embed = new EmbedBuilder();
            embed.AddField("In-Game-Name: ", name);
            embed.AddField("Invite Link: ", link);
            embed.AddField(addField("Need:",  need));
            embed.AddField(addField("Stock:", have));
            return ReplyAsync("", false, embed.Build());
        }
        [Command("+need"), Summary("Add ingredient(s) to your list.")]
        public Task addToNeeds([Remainder]string ingredients)
        {
            db.addToList(Context.User.Id, "n", ingredients);
            ReplyAsync("Ingredient(s) Added");
            return list();
        }

        [Command("+have"), Summary("Add ingredient(s) to your list.")]
        public Task addToStock([Remainder]string ingredients)
        {
            db.addToList(Context.User.Id, "h", ingredients);
            ReplyAsync("Ingredient(s) Added");
            return list();
        }

        [Command("-need"), Summary("Remove an ingredient to your list.")]
        public Task removeFromNeeds(string ingredients)
        {
            db.updateIngredient(Context.User.Id, ingredients, "n");
            ReplyAsync("Ingredient Removed");
            return list();
        }

        [Command("-have"), Summary("Remove an ingredient to your list.")]
        public Task removeFromStock(string ingredients)
        {
            db.updateIngredient(Context.User.Id, ingredients, "h");
            ReplyAsync("Ingredient Removed");
            return list();
        }

        [Command("reset"), Summary("Reset your list.")]
        public Task reset()
        {
            db.resetList(Context.User.Id);
            ReplyAsync("List has been reset.");
            return list();
        }



        public EmbedFieldBuilder addField(string title = "", string desc = "")
        {
            var exampleField = new EmbedFieldBuilder()
                    .WithName(title)
                    .WithValue(desc)
                    .WithIsInline(true);


            return exampleField;


        }

    }
}
