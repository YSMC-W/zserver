using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ZMap;

public static class Log
{
    public static ILogger Logger = NullLogger.Instance;
}