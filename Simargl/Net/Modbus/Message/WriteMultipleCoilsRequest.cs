using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Unme.Common;

namespace Simargl.Net.Modbus.Message;

/// <summary>
///     Write Multiple Coils request.
/// </summary>
public class WriteMultipleCoilsRequest : AbstractModbusMessageWithData<DiscreteCollection>, IModbusRequest
{
    /// <summary>
    ///     Write Multiple Coils request.
    /// </summary>
    public WriteMultipleCoilsRequest()
    {
    }

    /// <summary>
    ///     Write Multiple Coils request.
    /// </summary>
    [CLSCompliant(false)]
    public WriteMultipleCoilsRequest(byte slaveAddress, ushort startAddress, DiscreteCollection data)
        : base(slaveAddress, ModbusFunctionCodes.WriteMultipleCoils)
    {
        StartAddress = startAddress;
        NumberOfPoints = (ushort)data.Count;
        ByteCount = (byte)((data.Count + 7) / 8);
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
    [CLSCompliant(false)]
    public ushort NumberOfPoints
    {
        get => MessageImpl.NumberOfPoints!.Value;

        set
        {
            if (value > Modbus.MaximumDiscreteRequestResponseSize)
            {
                string msg = $"Maximum amount of data {Modbus.MaximumDiscreteRequestResponseSize} coils.";
                throw new ArgumentOutOfRangeException("NumberOfPoints", msg);
            }

            MessageImpl.NumberOfPoints = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public ushort StartAddress
    {
        get => MessageImpl.StartAddress!.Value;
        set => MessageImpl.StartAddress = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public override int MinimumFrameSize => 7;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string msg = $"Write {NumberOfPoints} coils starting at address {StartAddress}.";
        return msg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <exception cref="IOException"></exception>
    [CLSCompliant(false)]
    public void ValidateResponse(IModbusMessage response)
    {
        var typedResponse = (WriteMultipleCoilsResponse)response;

        if (StartAddress != typedResponse.StartAddress)
        {
            string msg = $"Unexpected start address in response. Expected {StartAddress}, received {typedResponse.StartAddress}.";
            throw new IOException(msg);
        }

        if (NumberOfPoints != typedResponse.NumberOfPoints)
        {
            string msg = $"Unexpected number of points in response. Expected {NumberOfPoints}, received {typedResponse.NumberOfPoints}.";
            throw new IOException(msg);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <exception cref="FormatException"></exception>
    protected override void InitializeUnique(byte[] frame)
    {
        if (frame.Length < MinimumFrameSize + frame[6])
        {
            throw new FormatException("Message frame does not contain enough bytes.");
        }

        StartAddress = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 2));
        NumberOfPoints = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 4));
        ByteCount = frame[6];
        Data = new DiscreteCollection(frame.Slice(7, ByteCount).ToArray());
    }
}
