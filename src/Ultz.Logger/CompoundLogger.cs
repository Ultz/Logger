using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Ultz.Logger
{
    public class CompoundLogger : ILogger
    {
        public List<ILogger> OtherLoggers { get; set; } = new List<ILogger>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            OtherLoggers.ForEach(x => Log<TState>(logLevel, eventId, state, exception, formatter));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return OtherLoggers.Any(x => x.IsEnabled(logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new CompoundDisposable(){OtherDisposables = OtherLoggers.Select(x => x.BeginScope(state)).ToList()};
        }
    }
}
