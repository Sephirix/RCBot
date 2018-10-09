using System;
using Discord;
using System.Threading;
using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Rest;
using Microsoft.Extensions.Logging;
using RCBot.Services;
using RCBot.Modules;

namespace RCBot.Services
{
    public sealed class DiscordService : ICustomService
    {

        private readonly ILogger Logger;
        private IServiceProvider Provider;
        private readonly DiscordSocketClient Client;
        private readonly CommandService Commands;

        public DiscordService(ILoggerFactory factory, DiscordSocketClient client, CommandService command)
        {

            Client = client;
            Commands = command;
            Logger = factory.CreateLogger("Discord");
        }

        public async Task InitializeAsync(IServiceProvider provider)
        {
            Provider = provider;
            Client.Log += OnLog;
            Client.Ready += OnReady;

            Client.MessageReceived += OnMessageReceived;

            await Commands.AddModulesAsync(Assembly.GetExecutingAssembly(), provider);

            await Client.LoginAsync(TokenType.Bot, ConfigService.Config.Token);
            await Client.StartAsync();
        }

        private async Task OnReady()
        {
            //if (Client.GetChannel(496033392106668047) is IMessageChannel channel)
            //    await channel.SendMessageAsync("Hi, I'm up!" + ConfigService.Config.Prefix);


        }



        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            //if (BlockMessages) return;
            if (!(socketMessage is SocketUserMessage message) || message.Author.IsBot ||
                  message.Author.IsWebhook) return;


            var argPos = 0;
            if (!(message.HasStringPrefix(ConfigService.Config.Prefix, ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos))) {
                if (message.Channel.Id == 498413664584466443)
                {
                    await message.DeleteAsync();

                }
                return;
            }

            if (message.Channel.Id == 498413664584466443)
            {

            }
             else if(message.Channel.Id == 498440748287524875 )
            {
                Console.Write(1);
                if (!message.Content.Contains(ConfigService.Config.Prefix + "profile")) return;
                Console.WriteLine(2);
            }
            else
            {
                Console.WriteLine(3);
                return;
            }
            Console.WriteLine(ConfigService.Config.Prefix + "profile");
            var context = new CustomContext(Client, message);
            
            var Result = await Commands.ExecuteAsync(context, argPos, Provider, MultiMatchHandling.Best); 
            if (!Result.IsSuccess)
            {
                Console.WriteLine($"[{DateTime.Now} at Commands] Something went wrong with the executing a command. " +
                    $"Text: {context.Message.Content} | Error: {Result.ErrorReason}");

            }
        }

        private async Task OnLog(LogMessage log)
        {
           Logger.Log((LogLevel)Math.Abs((int)log.Severity - 5), 0, log.Message, log.Exception);
            Console.WriteLine($"{DateTime.Now} at {log.Source} {log.Message}");
        }
    }
}