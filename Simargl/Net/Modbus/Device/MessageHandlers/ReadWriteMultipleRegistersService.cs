using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device.MessageHandlers;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class ReadWriteMultipleRegistersService :
    ModbusFunctionServiceBase<ReadWriteMultipleRegistersRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public ReadWriteMultipleRegistersService() 
        : base(ModbusFunctionCodes.ReadWriteMultipleRegisters)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    public override IModbusMessage CreateRequest(byte[] frame)
    {
        return CreateModbusMessage<ReadWriteMultipleRegistersRequest>(frame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public override int GetRtuRequestBytesToRead(byte[] frameStart)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public override int GetRtuResponseBytesToRead(byte[] frameStart)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="dataStore"></param>
    /// <returns></returns>
    protected override IModbusMessage Handle(ReadWriteMultipleRegistersRequest request, ISlaveDataStore dataStore)
    {
        ushort[] pointsToWrite = request.WriteRequest.Data
            .ToArray();

        dataStore.HoldingRegisters.WritePoints(request.WriteRequest.StartAddress, pointsToWrite);

        ushort[] readPoints = dataStore.HoldingRegisters.ReadPoints(request.ReadRequest.StartAddress,
            request.ReadRequest.NumberOfPoints);

        RegisterCollection data = new RegisterCollection(readPoints);

        return new ReadHoldingInputRegistersResponse(
            request.FunctionCode,
            request.SlaveAddress,
            data);
    }
}
