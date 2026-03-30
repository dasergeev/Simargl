using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device.MessageHandlers;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class WriteMultipleRegistersService :
    ModbusFunctionServiceBase<WriteMultipleRegistersRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public WriteMultipleRegistersService() :
        base(ModbusFunctionCodes.WriteMultipleRegisters)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    public override IModbusMessage CreateRequest(byte[] frame)
    {
        return CreateModbusMessage<WriteMultipleRegistersRequest>(frame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    public override int GetRtuRequestBytesToRead(byte[] frameStart)
    {
        return frameStart[6] + 2;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    public override int GetRtuResponseBytesToRead(byte[] frameStart)
    {
        return 4;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="dataStore"></param>
    /// <returns></returns>
    protected override IModbusMessage Handle(WriteMultipleRegistersRequest request, ISlaveDataStore dataStore)
    {
        ushort[] registers = request.Data.ToArray();

        dataStore.HoldingRegisters.WritePoints(request.StartAddress, registers);

        return new WriteMultipleRegistersResponse(
            request.SlaveAddress,
            request.StartAddress,
            request.NumberOfPoints);
    }
}
