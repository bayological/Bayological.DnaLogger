using Microsoft.Extensions.Logging;
using System;

namespace Bayological.DnaLogger
{
  public class DnaLoggerProvider : ILoggerProvider
  {
    private readonly Func<string, LogLevel, bool> _filter;
    private readonly IDnaLogApiService _logApiService;

    public DnaLoggerProvider(Func<string, LogLevel, bool> filter, IDnaLogApiService logApiService)
    {
      _logApiService = logApiService;
      _filter = filter;
    }

    public ILogger CreateLogger(string categoryName)
    {
      return new DnaLogger(categoryName, _filter, _logApiService);
    }
    
    public void Dispose()
    {
    }
  }
}
