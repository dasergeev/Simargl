using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Unme.Common;

namespace Simargl.Net.Modbus.Message;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class ReadWriteMultipleRegistersRequest :
    AbstractModbusMessageWithData<RegisterCollection>,
    IModbusRequest
{
    private ReadHoldingInputRegistersRequest _readRequest;
    private WriteMultipleRegistersRequest _writeRequest;

    /// <summary>
    /// 
    /// </summary>
    public ReadWriteMultipleRegistersRequest()
    {
        _readRequest = null!;
        _writeRequest = null!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startReadAddress"></param>
    /// <param name="numberOfPointsToRead"></param>
    /// <param name="startWriteAddress"></param>
    /// <param name="writeData"></param>
    public ReadWriteMultipleRegistersRequest(
        byte slaveAddress,
        ushort startReadAddress,
        ushort numberOfPointsToRead,
        ushort startWriteAddress,
        RegisterCollection writeData)
        : base(slaveAddress, ModbusFunctionCodes.ReadWriteMultipleRegisters)
    {
        _readRequest = new ReadHoldingInputRegistersRequest(
            ModbusFunctionCodes.ReadHoldingRegisters,
            slaveAddress,
            startReadAddress,
            numberOfPointsToRead);

        _writeRequest = new WriteMultipleRegistersRequest(
            slaveAddress,
            startWriteAddress,
            writeData);

        // TODO: ugly hack for all ModbusSerialTransport-inheritances (ModbusIpTransport would not need this, as it implements complete different BuildMessageFrame)

        // fake ByteCount, Data can hold only even number of bytes
        ByteCount = (ProtocolDataUnit[1]);

        // fake Data, as this modbusmessage does not fit ModbusMessageImpl
        Data = new RegisterCollection(ProtocolDataUnit.Slice(2, ProtocolDataUnit.Length - 2).ToArray());
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
    public override byte[] ProtocolDataUnit
    {
        get
        {
            byte[] readPdu = _readRequest.ProtocolDataUnit;
            byte[] writePdu = _writeRequest.ProtocolDataUnit;

            using (var stream = new MemoryStream(readPdu.Length + writePdu.Length))
            {
                stream.WriteByte(FunctionCode);

                // read and write PDUs without function codes
                stream.Write(readPdu, 1, readPdu.Length - 1);
                stream.Write(writePdu, 1, writePdu.Length - 1);

                return stream.ToArray();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public ReadHoldingInputRegistersRequest ReadRequest => _readRequest;

    /// <summary>
    /// 
    /// </summary>
    public WriteMultipleRegistersRequest WriteRequest => _writeRequest;

    /// <summary>
    /// 
    /// </summary>
    public override int MinimumFrameSize => 11;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string msg = $"Write {_writeRequest.NumberOfPoints} holding registers starting at address {_writeRequest.StartAddress}, and read {_readRequest.NumberOfPoints} registers starting at address {_readRequest.StartAddress}.";
        return msg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <exception cref="IOException"></exception>
    public void ValidateResponse(IModbusMessage response)
    {
        var typedResponse = (ReadHoldingInputRegistersResponse)response;
        var expectedByteCount = ReadRequest.NumberOfPoints * 2;

        if (expectedByteCount != typedResponse.ByteCount)
        {
            string msg = $"Unexpected byte count in response. Expected {expectedByteCount}, received {typedResponse.ByteCount}.";
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
        if (frame.Length < MinimumFrameSize + frame[10])
        {
            throw new FormatException("Message frame does not contain enough bytes.");
        }

        byte[] readFrame = new byte[2 + 4];
        byte[] writeFrame = new byte[frame.Length - 6 + 2];

        readFrame[0] = writeFrame[0] = SlaveAddress;
        readFrame[1] = writeFrame[1] = FunctionCode;

        Buffer.BlockCopy(frame, 2, readFrame, 2, 4);
        Buffer.BlockCopy(frame, 6, writeFrame, 2, frame.Length - 6);

        _readRequest = ModbusMessageFactory.CreateModbusMessage<ReadHoldingInputRegistersRequest>(readFrame);
        _writeRequest = ModbusMessageFactory.CreateModbusMessage<WriteMultipleRegistersRequest>(writeFrame);

        // TODO: ugly hack for all ModbusSerialTransport-inheritances (ModbusIpTransport would not need this, as it implements complete different BuildMessageFrame)

        // fake ByteCount, Data can hold only even number of bytes
        ByteCount = (ProtocolDataUnit[1]);

        // fake Data, as this modbusmessage does not fit ModbusMessageImpl
        Data = new RegisterCollection(ProtocolDataUnit.Slice(2, ProtocolDataUnit.Length - 2).ToArray());
    }
}
