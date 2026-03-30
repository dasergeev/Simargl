using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device.MessageHandlers;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class WriteMultipleCoilsService :
    ModbusFunctionServiceBase<WriteMultipleCoilsRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public WriteMultipleCoilsService() :
        base(ModbusFunctionCodes.WriteMultipleCoils)
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
        return CreateModbusMessage<WriteMultipleCoilsRequest>(frame);
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
    [CLSCompliant(false)]
    protected override IModbusMessage Handle(WriteMultipleCoilsRequest request, ISlaveDataStore dataStore)
    {
        bool[] points = request.Data
            .Take(request.NumberOfPoints)
            .ToArray();

        dataStore.CoilDiscretes.WritePoints(request.StartAddress, points);

        return new WriteMultipleCoilsResponse(
           request.SlaveAddress,
           request.StartAddress,
           request.NumberOfPoints);
    }
}
