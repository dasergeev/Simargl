using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Message;

/// <summary>
/// 
/// </summary>
public class ReadHoldingInputRegisters32Request :
    ReadHoldingInputRegistersRequest
{
    /// <summary>
    /// 
    /// </summary>
    public ReadHoldingInputRegisters32Request()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="functionCode"></param>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="numberOfPoints"></param>
    [CLSCompliant(false)]
    public ReadHoldingInputRegisters32Request(byte functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        : base(functionCode, slaveAddress, startAddress, numberOfPoints)
    {
        StartAddress = startAddress;
        NumberOfPoints = numberOfPoints;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <exception cref="IOException"></exception>
    [CLSCompliant(false)]
    public override void ValidateResponse(IModbusMessage response)
    {
        var typedResponse = response as ReadHoldingInputRegistersResponse;
        Debug.Assert(typedResponse != null, "Argument response should be of type ReadHoldingInputRegistersResponse.");
        var expectedByteCount = NumberOfPoints * 4;

        if (expectedByteCount != typedResponse.ByteCount)
        {
            string msg = $"Unexpected byte count. Expected {expectedByteCount}, received {typedResponse.ByteCount}.";
            throw new IOException(msg);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string msg = $"Read {NumberOfPoints} {(FunctionCode == ModbusFunctionCodes.ReadHoldingRegisters ? "holding" : "input")} registers starting at address {StartAddress}.";
        return msg;
    }
}
