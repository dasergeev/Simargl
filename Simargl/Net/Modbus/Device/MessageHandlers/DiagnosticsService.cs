using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device.MessageHandlers;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class DiagnosticsService :
    ModbusFunctionServiceBase<IModbusMessage>
{
    /// <summary>
    /// 
    /// </summary>
    public DiagnosticsService() :
        base(ModbusFunctionCodes.Diagnostics)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    public override IModbusMessage CreateRequest(byte[] frame)
    {
        return CreateModbusMessage<DiagnosticsRequestResponse>(frame);
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
    protected override IModbusMessage Handle(IModbusMessage request, ISlaveDataStore dataStore)
    {
        return request;
    }
}
