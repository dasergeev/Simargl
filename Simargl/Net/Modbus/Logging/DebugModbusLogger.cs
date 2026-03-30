using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Logging;

/// <summary>
/// Writes using Debug.WriteLine().
/// </summary>
public class DebugModbusLogger : ModbusLogger
{
    private const int LevelColumnSize = 15;
    private static readonly string BlankHeader = Environment.NewLine + new string(' ', LevelColumnSize);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minimumLoggingLevel"></param>
    public DebugModbusLogger(LoggingLevel minimumLoggingLevel = LoggingLevel.Debug) 
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

        Debug.WriteLine($"[{level}]".PadRight(LevelColumnSize) + message);
    }
}
