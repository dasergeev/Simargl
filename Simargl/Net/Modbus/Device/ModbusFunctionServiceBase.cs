using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Device;

/// <summary>
/// Base class for 
/// </summary>
/// <typeparam name="TRequest">The type of request to handle.</typeparam>
[CLSCompliant(false)]
public abstract class ModbusFunctionServiceBase<TRequest> :
    IModbusFunctionService
    where TRequest : class
{
    private readonly byte _functionCode;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="functionCode"></param>
    protected ModbusFunctionServiceBase(byte functionCode)
    {
        _functionCode = functionCode;
    }

    /// <summary>
    /// 
    /// </summary>
    public byte FunctionCode => _functionCode;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    public abstract IModbusMessage CreateRequest(byte[] frame);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="dataStore"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public IModbusMessage HandleSlaveRequest(IModbusMessage request, ISlaveDataStore dataStore)
    {
        //Attempt to cast the message
        TRequest typedRequest = (request as TRequest)!;

        if (typedRequest == null)
            throw new InvalidOperationException($"Unable to cast request of type '{request.GetType().Name}' to type '{typeof(TRequest).Name}");

        //Do the implementation specific logic
        return Handle(typedRequest, dataStore);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    public abstract int GetRtuRequestBytesToRead(byte[] frameStart);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frameStart"></param>
    /// <returns></returns>
    public abstract int GetRtuResponseBytesToRead(byte[] frameStart);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="dataStore"></param>
    /// <returns></returns>
    protected abstract IModbusMessage Handle(TRequest request, ISlaveDataStore dataStore);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="frame"></param>
    /// <returns></returns>
    protected static T CreateModbusMessage<T>(byte[] frame)
        where T : IModbusMessage, new()
    {
        IModbusMessage message = new T();
        message.Initialize(frame);

        return (T)message;
    }
}
