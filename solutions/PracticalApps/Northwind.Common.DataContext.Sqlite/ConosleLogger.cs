using System.Text;
using Microsoft.Extensions.Logging;

namespace Packt.Shared;

public class ConosleLogger : ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (eventId.Id != 20100) // execute SQL statement
        {
            return;
        }

        StringBuilder line = new();
        line.Append($"Level: {logLevel}, Event Id: {eventId.Id}");

        // only output the state or exception if it exists
        if (state != null)
        {
            line.Append($", State: {state}");
        }

        if (exception != null)
        {
            line.Append($", Exception: {exception.Message}");
        }
        Console.WriteLine(line.ToString());
    }

    public bool IsEnabled(LogLevel logLevel)
        => logLevel switch
        {
            LogLevel.Trace
                or LogLevel.Information
                or  LogLevel.None 
                    => false,
            LogLevel.Debug
                or LogLevel.Warning
                or LogLevel.Error
                or LogLevel.Critical 
                or _
                    => true
        };

    public IDisposable BeginScope<TState>(TState state)
        => null;
}