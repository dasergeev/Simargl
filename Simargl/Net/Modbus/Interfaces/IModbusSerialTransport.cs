namespace Simargl.Net.Modbus.Interfaces;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public interface IModbusSerialTransport :
    IModbusTransport
{
    /// <summary>
    /// 
    /// </summary>
    void DiscardInBuffer();

    /// <summary>
    /// 
    /// </summary>
    bool CheckFrame { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messageFrame"></param>
    /// <returns></returns>
    bool ChecksumsMatch(IModbusMessage message, byte[] messageFrame);

    /// <summary>
    /// 
    /// </summary>
    void IgnoreResponse();
}