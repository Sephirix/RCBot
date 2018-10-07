using System;
using Microsoft.Extensions.Logging;

namespace RCBot.Logging
{
    public sealed class ConsoleLogger : ILogger
    {
        private readonly string Name;

        public ConsoleLogger(string categoryName)
            => Name = categoryName;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            Console.ForegroundColor = GetLogLevelColor(logLevel);
            var logString = $"[{GetLogLevelString(logLevel)}] ";
            Console.Write(logString);
            Console.ResetColor();
            Console.Write($"{Name}:\n");
            var form = formatter(state, exception);
            Console.Write($"{form}".PadLeft(10 - logString.Length));
            Console.Write(Environment.NewLine);
        }

        public bool IsEnabled(LogLevel logLevel)
            => true;

        public IDisposable BeginScope<TState>(TState state)
            => null;

        private ConsoleColor GetLogLevelColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return ConsoleColor.Red;
                case LogLevel.Error:
                    return ConsoleColor.DarkRed;
                case LogLevel.Warning:
                    return ConsoleColor.Yellow;
                case LogLevel.Information:
                    return ConsoleColor.Green;
                case LogLevel.Debug:
                    return ConsoleColor.DarkMagenta;
                case LogLevel.Trace:
                    return ConsoleColor.DarkMagenta;
                default:
                    return ConsoleColor.Gray;
            }
        }

        private string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "TRCE";
                case LogLevel.Debug:
                    return "DBUG";
                case LogLevel.Information:
                    return "INFO";
                case LogLevel.Warning:
                    return "WARN";
                case LogLevel.Error:
                    return "FAIL";
                case LogLevel.Critical:
                    return "CRIT";
                case LogLevel.None:
                    return "NONE";
                default:
                    return "UKNW";
            }
        }
    }
}