using Microsoft.Extensions.Logging;

namespace RCBot.Logging
{
    public sealed class LoggerProvider : ILoggerProvider
    {
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
            => new ConsoleLogger(categoryName);
    }
}