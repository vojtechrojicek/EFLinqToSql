using Microsoft.Extensions.Logging;

namespace EFLinqToSql.Logging
{
    internal class CustomLoggerProvider : ILoggerProvider
    {
        private ILogger _logger;
        private readonly CustomLogMessage _message;

        public CustomLoggerProvider(CustomLogMessage message)
        {
            _message = message;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (_logger == null)
            {
                _logger = new CustomLogger(_message);
            }
            return _logger;
        }

        public void Dispose()
        {
            return;
        }
    }
}
