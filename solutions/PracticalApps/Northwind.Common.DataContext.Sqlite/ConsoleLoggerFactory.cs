using Microsoft.Extensions.Logging;

namespace Packt.Shared;

public class ConsoleLoggerFactory : ILoggerFactory
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new ConosleLogger();
    }

    public void AddProvider(ILoggerProvider provider)
    {
        
    }
}