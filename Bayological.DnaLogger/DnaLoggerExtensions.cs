using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bayological.DnaLogger
{
  public static class DnaLoggerExtensions
  {
    public static ILoggerFactory AddDnaLogger(this ILoggerFactory factory,
                                          IDnaLogApiService apiService,
                                          Func<string, LogLevel, bool> filter = null)
    {
      factory.AddProvider(new DnaLoggerProvider(filter, apiService));
      return factory;
    }

    public static ILoggerFactory AddDnaLogger(this ILoggerFactory factory, IDnaLogApiService apiService, LogLevel minLevel)
    {
      return AddDnaLogger(
          factory,
          apiService,
          (_, logLevel) => logLevel >= minLevel);
    }
  }
}
