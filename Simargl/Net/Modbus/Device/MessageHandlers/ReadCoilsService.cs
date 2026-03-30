using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device.MessageHandlers;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class ReadCoilsService :
    ModbusFunctionServiceBase<ReadCoilsInputsRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public ReadCoilsService() :
        base(ModbusFunctionCodes.ReadCoils)
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
        return CreateModbusMessage<ReadCoilsInputsRequest>(frame);
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
    protected override IModbusMessage Handle(ReadCoilsInputsRequest request, ISlaveDataStore dataStore)
    {
        bool[] discretes = dataStore.CoilDiscretes.ReadPoints(request.StartAddress, request.NumberOfPoints);

        DiscreteCollection data = new DiscreteCollection(discretes);

        return new ReadCoilsInputsResponse(
            request.FunctionCode, 
            request.SlaveAddress, 
            data.ByteCount, data);
    }
}
