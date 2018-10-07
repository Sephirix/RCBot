using System;

using System.Linq;
using Discord.Rest;
using RCBot.Logging;

using RCBot.Services;
using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace RCBot
{

    public sealed class Program
    {
        public static void Main(string[] args)
            => new Program().ConfigureAsync().GetAwaiter().GetResult();

        private async Task ConfigureAsync()
        {

            ConfigService.SetOrCreateConfig();
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            await provider.GetRequiredService<DiscordService>().InitializeAsync(provider);
            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(x => x.AddProvider(new LoggerProvider()));
            foreach (var custom in CustomServices)
                services.AddSingleton(custom);
            services.AddSingleton<CommandService>();
            services.AddSingleton<DiscordSocketClient>();

        }

        private IEnumerable<Type> CustomServices
            => Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ICustomService)));
    }
}