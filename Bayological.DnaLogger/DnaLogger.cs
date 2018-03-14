using Microsoft.Extensions.Logging;
using System;

namespace Bayological.DnaLogger
{
  public class DnaLogger : ILogger
  {
    private string _categoryName;
    private Func<string, LogLevel, bool> _filter;
    private IDnaLogApiService _logApiService;

    public DnaLogger(string categoryName, Func<string, LogLevel, bool> filter, IDnaLogApiService logApiService)
    {
      _categoryName = categoryName;
      _filter = filter;
      _logApiService = logApiService;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
      return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return (_filter == null || _filter(_categoryName, logLevel));
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
      if (!IsEnabled(logLevel))
      {
        return;
      }

      if (formatter == null)
      {
        throw new ArgumentNullException(nameof(formatter));
      }

      var message = formatter(state, exception);

      if (string.IsNullOrEmpty(message))
      {
        return;
      }

      message = $@"Level: {logLevel} {message}";

      if (exception != null)
      {
        message += Environment.NewLine + Environment.NewLine + exception.ToString();
      }

      var line = new Line
      {
        Lines = new LogLine[]
        {
          new LogLine()
          {
            App = "Launchly",
            Env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            Level = logLevel.ToString(),
            Line = message
          }
        }
      };
      _logApiService.IngestLog(line);
    }
  }
}
