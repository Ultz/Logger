using Microsoft.Extensions.Logging;

namespace Ultz.Logger
{
    public class UltzLoggerProvider : ILoggerProvider
    {
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            var logger = new UltzLogger();
            logger.Format = categoryName + " " + logger.Format;
            return logger;
        }
    }
}
