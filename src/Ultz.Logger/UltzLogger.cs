using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Xsl;
using Microsoft.Extensions.Logging;

namespace Ultz.Logger
{
    public class UltzLogger : ILogger
    {
        public string Format { get; set; } = "[HH:MI:SS LVL]: EVENT_ID MSG";
        public List<TextWriter> Writers { get; set; } = new List<TextWriter>();
        public List<LogLevel> LogLevels { get; set; } = new List<LogLevel>();

        public void Log<TState>
        (
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter
        )
        {
            var msg = Format.Replace("DD", DateTime.Now.ToString("dd"))
                .Replace("MO", DateTime.Now.ToString("MM"))
                .Replace("YYYY", DateTime.Now.ToString("yyyy"))
                .Replace("YY", DateTime.Now.ToString("yy"))
                .Replace("HH", DateTime.Now.ToString("hh"))
                .Replace("MI", DateTime.Now.ToString("mm"))
                .Replace("SS", DateTime.Now.ToString("ss"))
                .Replace("EVENT_ID", $"[{eventId.Name}/{eventId.Id}]")
                .Replace("MSG", formatter(state, exception))
                .Replace("LVL", GetLevel(logLevel));
            Writers.ForEach(x => x.WriteLine(msg));
        }

        private string GetLevel(LogLevel lvl)
        {
            switch (lvl)
            {
                case LogLevel.Trace:
                    return "TRACE";
                case LogLevel.Debug:
                    return "DEBUG";
                case LogLevel.Information:
                    return "INFO";
                case LogLevel.Warning:
                    return "WARN";
                case LogLevel.Error:
                    return "ERROR";
                case LogLevel.Critical:
                    return "SEVERE";
                case LogLevel.None:
                    return "NONE";
                default:
                    throw new ArgumentOutOfRangeException(nameof(lvl), lvl, null);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return LogLevels.Contains(logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new EmptyDisposable();
        }
    }
}
