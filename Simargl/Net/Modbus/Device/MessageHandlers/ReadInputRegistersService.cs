using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device.MessageHandlers;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class ReadInputRegistersService :
    ModbusFunctionServiceBase<ReadHoldingInputRegistersRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public ReadInputRegistersService() :
        base(ModbusFunctionCodes.ReadInputRegisters)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public override IModbusMessage CreateRequest(byte[] frame)
    {
        return CreateModbusMessage<ReadHoldingInputRegistersRequest>(frame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    public override int GetRtuRequestBytesToRead(byte[] frameStart)
    {
        return 1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    public override int GetRtuResponseBytesToRead(byte[] frameStart)
    {
        return frameStart[2] + 1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="dataStore"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    protected override IModbusMessage Handle(ReadHoldingInputRegistersRequest request, ISlaveDataStore dataStore)
    {
        ushort[] registers = dataStore.InputRegisters.ReadPoints(request.StartAddress, request.NumberOfPoints);

        RegisterCollection regsiterCollection = new RegisterCollection(registers);

        return new ReadHoldingInputRegistersResponse(request.FunctionCode, request.SlaveAddress, regsiterCollection);
    }
}
