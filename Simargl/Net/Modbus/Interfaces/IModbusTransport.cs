using Simargl.Net.Modbus.IO;

namespace Simargl.Net.Modbus.Interfaces;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public interface IModbusTransport :
    IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    int Retries { get; set; }

    /// <summary>
    /// 
    /// </summary>
    uint RetryOnOldResponseThreshold { get; set; }

    /// <summary>
    /// 
    /// </summary>
    bool SlaveBusyUsesRetryCount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int WaitToRetryMilliseconds { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int ReadTimeout { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int WriteTimeout { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <returns></returns>
    T UnicastMessage<T>(IModbusMessage message) where T : IModbusMessage, new();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    byte[] ReadRequest();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    byte[] BuildMessageFrame(IModbusMessage message);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    void Write(IModbusMessage message);

    /// <summary>
    /// 
    /// </summary>
    IStreamResource StreamResource { get; }
}
