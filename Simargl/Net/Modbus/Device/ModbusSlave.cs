using Simargl.Net.Modbus.Extensions;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device;

/// <summary>
/// 
/// </summary>
public class ModbusSlave :
    IModbusSlave
{
    private readonly byte _unitId;
    private readonly ISlaveDataStore _dataStore;

    private readonly IDictionary<byte, IModbusFunctionService> _handlers;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitId"></param>
    /// <param name="dataStore"></param>
    /// <param name="handlers"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [CLSCompliant(false)]
    public ModbusSlave(byte unitId, ISlaveDataStore dataStore, IEnumerable<IModbusFunctionService> handlers)
    {
        if (dataStore == null) throw new ArgumentNullException(nameof(dataStore));
        if (handlers == null) throw new ArgumentNullException(nameof(handlers));

        _unitId = unitId;
        _dataStore = dataStore;
        _handlers = handlers.ToDictionary(h => h.FunctionCode, h => h);
    }

    /// <summary>
    /// 
    /// </summary>
    public byte UnitId => _unitId;

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public ISlaveDataStore DataStore => _dataStore;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusMessage ApplyRequest(IModbusMessage request)
    {
        IModbusMessage response;

        try
        {
            //Try to get a handler for this function.
            IModbusFunctionService handler = _handlers.GetValueOrDefault(request.FunctionCode);

            //Check to see if we found a handler for this function code.
            if (handler == null)
            {
                throw new InvalidModbusRequestException(SlaveExceptionCodes.IllegalFunction);
            }

            //Process the request
            response = handler.HandleSlaveRequest(request, DataStore);
        }
        catch (InvalidModbusRequestException ex)
        {
            // Catches the exception for an illegal function or a custom exception from the ModbusSlaveRequestReceived event.
            response = new SlaveExceptionResponse(
                request.SlaveAddress,
                (byte) (Modbus.ExceptionOffset + request.FunctionCode),
                ex.ExceptionCode);
        }
        catch (Exception)
        {
            //Okay - this is no beuno.
            response = new SlaveExceptionResponse(request.SlaveAddress,
                (byte)(Modbus.ExceptionOffset + request.FunctionCode),
                SlaveExceptionCodes.SlaveDeviceFailure);
        }

        return response;
    }
}
