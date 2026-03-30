using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Unme.Common;

namespace Simargl.Net.Modbus.Message;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class ReadHoldingInputRegistersResponse :
    AbstractModbusMessageWithData<RegisterCollection>,
    IModbusMessage
{
    /// <summary>
    /// 
    /// </summary>
    public ReadHoldingInputRegistersResponse()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="functionCode"></param>
    /// <param name="slaveAddress"></param>
    /// <param name="data"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ReadHoldingInputRegistersResponse(byte functionCode, byte slaveAddress, RegisterCollection data)
        : base(slaveAddress, functionCode)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        ByteCount = data.ByteCount;
        Data = data;
    }

    /// <summary>
    /// 
    /// </summary>
    public byte ByteCount
    {
        get => MessageImpl.ByteCount!.Value;
        set => MessageImpl.ByteCount = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public override int MinimumFrameSize => 3;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string msg = $"Read {Data.Count} {(FunctionCode == ModbusFunctionCodes.ReadHoldingRegisters ? "holding" : "input")} registers.";
        return msg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <exception cref="FormatException"></exception>
    protected override void InitializeUnique(byte[] frame)
    {
        if (frame.Length < MinimumFrameSize + frame[2])
        {
            throw new FormatException("Message frame does not contain enough bytes.");
        }

        ByteCount = frame[2];
        Data = new RegisterCollection(frame.Slice(3, ByteCount).ToArray());
    }
}
