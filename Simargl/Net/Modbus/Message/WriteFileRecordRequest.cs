using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Message;

/// <summary>
/// 
/// </summary>
public class WriteFileRecordRequest :
    AbstractModbusMessageWithData<FileRecordCollection>,
    IModbusRequest
{
    /// <summary>
    /// 
    /// </summary>
    public WriteFileRecordRequest()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAdress"></param>
    /// <param name="data"></param>
    public WriteFileRecordRequest(byte slaveAdress, FileRecordCollection data)
        : base(slaveAdress, ModbusFunctionCodes.WriteFileRecord)
    {
        Data = data;
        ByteCount = data.ByteCount;
    }

    /// <summary>
    /// 
    /// </summary>
    public override int MinimumFrameSize => 10;

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
    /// <param name="response"></param>
    /// <exception cref="IOException"></exception>
    [CLSCompliant(false)]
    public void ValidateResponse(IModbusMessage response)
    {
        var typedResponse = (WriteFileRecordResponse)response;
        
        if (Data.FileNumber != typedResponse.Data.FileNumber)
        {
            string msg = $"Unexpected file number in response. Expected {Data.FileNumber}, received {typedResponse.Data.FileNumber}.";
            throw new IOException(msg);
        }

        if (Data.StartingAddress != typedResponse.Data.StartingAddress)
        {
            string msg = $"Unexpected starting address in response. Expected {Data.StartingAddress}, received {typedResponse.Data.StartingAddress}.";
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
        if (frame.Length < frame[2])
        {
            throw new FormatException("Message frame does not contain enough bytes.");
        }

        ByteCount = frame[2];
        Data = new FileRecordCollection(frame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string msg = $"Write {Data.DataBytes.Count} bytes for file {Data.FileNumber} starting at address {Data.StartingAddress}.";
        return msg;
    }
}
