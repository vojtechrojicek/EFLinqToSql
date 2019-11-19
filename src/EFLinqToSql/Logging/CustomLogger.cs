using Microsoft.Extensions.Logging;
using System;

namespace EFLinqToSql.Logging
{
    internal class CustomLogger : ILogger
    {
        private readonly CustomLogMessage _message;

        public CustomLogger(CustomLogMessage message)
        {
            _message = message;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            _message.Information += $"{formatter(state, exception)}{Environment.NewLine}";
            _message.Information += $"------------------------------------------------------------------------------------------------------------{Environment.NewLine}";
            _message.Information += $"------------------------------------------------------------------------------------------------------------{Environment.NewLine}";
        }
    }
}
