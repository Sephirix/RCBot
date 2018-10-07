
using System;
using System.IO;
using Newtonsoft.Json;

using System.Collections.Generic;
using RCBot.Entities;

namespace RCBot.Services
{
    public sealed class ConfigService
    {
        public static Config Config { get; set; }
        private static readonly JsonSerializer Serializer = new JsonSerializer();
        private static string DIR => $"{Path.Combine(Environment.CurrentDirectory, "Config.json")}";

        public static void SetOrCreateConfig()
        {
            if (File.Exists(DIR))
            {
                using (var stream = new StreamReader(DIR))
                using (var reader = new JsonTextReader(stream))
                    Config = Serializer.Deserialize<Config>(reader);
                return;
            }

            using (var stringWriter = new StringWriter())
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.WriteStartObject();
                var config = new Config();

                Console.WriteLine("Please enter prefix: ");
                writer.WritePropertyName("prefix");
                config.Prefix = Console.ReadLine();
                writer.WriteValue(config.Prefix);

                Console.WriteLine("Please enter token: ");
                writer.WritePropertyName("token");
                config.Token = Console.ReadLine();
                writer.WriteValue(config.Token);
              
                writer.WriteEndObject();

                Config = config;
                
                File.WriteAllText(DIR, $"{stringWriter}");
            }
        }
    }
}