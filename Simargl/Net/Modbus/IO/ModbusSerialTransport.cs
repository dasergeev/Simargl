using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Logging;

namespace Simargl.Net.Modbus.IO;

/// <summary>
///     Transport for Serial protocols.
///     Refined Abstraction - http://en.wikipedia.org/wiki/Bridge_Pattern
/// </summary>
[CLSCompliant(false)]
public abstract class ModbusSerialTransport :
    ModbusTransport,
    IModbusSerialTransport
{
    private bool _checkFrame = true;

    internal ModbusSerialTransport(IStreamResource streamResource, IModbusFactory modbusFactory, IModbusLogger logger)
        : base(streamResource, modbusFactory, logger)
    {
        Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");
    }

    /// <summary>
    /// Gets or sets a value indicating whether LRC/CRC frame checking is performed on messages.
    /// </summary>
    public bool CheckFrame
    {
        get => _checkFrame;
        set => _checkFrame = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void DiscardInBuffer()
    {
        StreamResource.DiscardInBuffer();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    [CLSCompliant(false)]
    public override void Write(IModbusMessage message)
    {
        DiscardInBuffer();

        byte[] frame = BuildMessageFrame(message);

        Logger.LogFrameTx(frame);
        
        StreamResource.Write(frame, 0, frame.Length);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="frame"></param>
    /// <returns></returns>
    /// <exception cref="IOException"></exception>
    [CLSCompliant(false)]
    public override IModbusMessage CreateResponse<T>(byte[] frame)
    {
        IModbusMessage response = base.CreateResponse<T>(frame);

        // compare checksum
        if (CheckFrame && !ChecksumsMatch(response, frame))
        {
            string msg = $"Checksums failed to match {string.Join(", ", response.MessageFrame)} != {string.Join(", ", frame)}";
            Logger.Warning(msg);
            throw new IOException(msg);
        }

        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract void IgnoreResponse();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messageFrame"></param>
    /// <returns></returns>
    public abstract bool ChecksumsMatch(IModbusMessage message, byte[] messageFrame);

    internal override void OnValidateResponse(IModbusMessage request, IModbusMessage response)
    {
        // no-op
    }
}
