using Simargl.Net.Modbus.Extensions;
using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Logging;

/// <summary>
/// 
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    public static void Trace(this IModbusLogger logger, string message)
    {
        logger.Log(LoggingLevel.Trace, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    public static void Debug(this IModbusLogger logger, string message)
    {
        logger.Log(LoggingLevel.Debug, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    public static void Information(this IModbusLogger logger, string message)
    {
        logger.Log(LoggingLevel.Information, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    public static void Warning(this IModbusLogger logger, string message)
    {
        logger.Log(LoggingLevel.Warning, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    public static void Error(this IModbusLogger logger, string message)
    {
        logger.Log(LoggingLevel.Error, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    public static void Critical(this IModbusLogger logger, string message)
    {
        logger.Log(LoggingLevel.Critical, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="level"></param>
    /// <param name="messageFactory"></param>
    public static void Log(this IModbusLogger logger, LoggingLevel level, Func<string> messageFactory)
    {
        if (logger.ShouldLog(level))
        {
            string message = messageFactory();

            logger.Log(level, message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="messageFactory"></param>
    public static void Trace(this IModbusLogger logger, Func<string> messageFactory)
    {
        logger.Log(LoggingLevel.Trace, messageFactory);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="messageFactory"></param>
    public static void Debug(this IModbusLogger logger, Func<string> messageFactory)
    {
        logger.Log(LoggingLevel.Debug, messageFactory);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="messageFactory"></param>
    public static void Information(this IModbusLogger logger, Func<string> messageFactory)
    {
        logger.Log(LoggingLevel.Information, messageFactory);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="messageFactory"></param>
    public static void Warning(this IModbusLogger logger, Func<string> messageFactory)
    {
        logger.Log(LoggingLevel.Warning, messageFactory);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="messageFactory"></param>
    public static void Error(this IModbusLogger logger, Func<string> messageFactory)
    {
        logger.Log(LoggingLevel.Error, messageFactory);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="messageFactory"></param>
    public static void Critical(this IModbusLogger logger, Func<string> messageFactory)
    {
        logger.Log(LoggingLevel.Critical, messageFactory);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="validPrefix"></param>
    /// <param name="invalidPrefix"></param>
    /// <param name="frame"></param>
    private static void LogFrame(this IModbusLogger logger, string validPrefix, string invalidPrefix, byte[] frame)
    {
        if (logger.ShouldLog(LoggingLevel.Trace))
        {
            if (logger.ShouldLog(LoggingLevel.Trace))
            {
                string prefix = frame.DoesCrcMatch() ? validPrefix : invalidPrefix;

                logger.Trace($"{prefix}: {string.Join(" ", frame.Select(b => b.ToString("X2")))}");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="frame"></param>
    internal static void LogFrameTx(this IModbusLogger logger, byte[] frame)
    {
        logger.LogFrame("TX", "tx", frame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="frame"></param>
    internal static void LogFrameRx(this IModbusLogger logger, byte[] frame)
    {
        logger.LogFrame("RX", "rx", frame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="frame"></param>
    internal static void LogFrameIgnoreRx(this IModbusLogger logger, byte[] frame)
    {
        logger.LogFrame("IR", "ir", frame);
    }
}
