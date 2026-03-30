using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device.MessageHandlers;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class WriteSingleCoilService :
    ModbusFunctionServiceBase<WriteSingleCoilRequestResponse>
{
    /// <summary>
    /// 
    /// </summary>
    public WriteSingleCoilService() :
        base(ModbusFunctionCodes.WriteSingleCoil)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    public override IModbusMessage CreateRequest(byte[] frame)
    {
        return CreateModbusMessage<WriteSingleCoilRequestResponse>(frame);
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
        return 4;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="dataStore"></param>
    /// <returns></returns>
    protected override IModbusMessage Handle(WriteSingleCoilRequestResponse request, ISlaveDataStore dataStore)
    {
        bool[] values = new bool[]
        {
            request.Data[0] == Modbus.CoilOn
        };

         dataStore.CoilDiscretes.WritePoints(request.StartAddress, values);

        return request;
    }
}
