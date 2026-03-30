using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Logging;

/// <summary>
/// 
/// </summary>
public class TraceModbusLogger : ModbusLogger
{
    private const int LevelColumnSize = 15;
    private static readonly string BlankHeader = Environment.NewLine + new string(' ', LevelColumnSize);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minimumLoggingLevel"></param>
    public TraceModbusLogger(LoggingLevel minimumLoggingLevel = LoggingLevel.Debug)
        : base(minimumLoggingLevel)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    protected override void LogCore(LoggingLevel level, string message)
    {
        message = message?.Replace(Environment.NewLine, BlankHeader)!;

        Trace.WriteLine($"[{level}]".PadRight(LevelColumnSize) + message);
    }
}
