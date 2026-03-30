using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device.MessageHandlers;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class WriteFileRecordService :
    ModbusFunctionServiceBase<WriteFileRecordRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public WriteFileRecordService() :
        base(ModbusFunctionCodes.WriteFileRecord)
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
        return CreateModbusMessage<WriteFileRecordRequest>(frame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    public override int GetRtuRequestBytesToRead(byte[] frameStart)
    {
        return frameStart[2] + 1;
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
    /// <exception cref="NotImplementedException"></exception>
    [CLSCompliant(false)]
    protected override IModbusMessage Handle(WriteFileRecordRequest request, ISlaveDataStore dataStore)
    {
        throw new NotImplementedException("WriteFileRecordService::Handle");
    }
}
