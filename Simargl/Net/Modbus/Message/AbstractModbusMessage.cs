namespace Simargl.Net.Modbus.Message;

/// <summary>
///     Abstract Modbus message.
/// </summary>
public abstract class AbstractModbusMessage
{
    private readonly ModbusMessageImpl _messageImpl;

    /// <summary>
    ///     Abstract Modbus message.
    /// </summary>
    internal AbstractModbusMessage()
    {
        _messageImpl = new ModbusMessageImpl();
    }

    /// <summary>
    ///     Abstract Modbus message.
    /// </summary>
    internal AbstractModbusMessage(byte slaveAddress, byte functionCode)
    {
        _messageImpl = new ModbusMessageImpl(slaveAddress, functionCode);
    }

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public ushort TransactionId
    {
        get => _messageImpl.TransactionId;
        set => _messageImpl.TransactionId = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public byte FunctionCode
    {
        get => _messageImpl.FunctionCode;
        set => _messageImpl.FunctionCode = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public byte SlaveAddress
    {
        get => _messageImpl.SlaveAddress;
        set => _messageImpl.SlaveAddress = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public byte[] MessageFrame => _messageImpl.MessageFrame;

    /// <summary>
    /// 
    /// </summary>
    public virtual byte[] ProtocolDataUnit => _messageImpl.ProtocolDataUnit;

    /// <summary>
    /// 
    /// </summary>
    public abstract int MinimumFrameSize { get; }

    internal ModbusMessageImpl MessageImpl => _messageImpl;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <exception cref="FormatException"></exception>
    public void Initialize(byte[] frame)
    {
        if (frame.Length < MinimumFrameSize)
        {
            string msg = $"Message frame must contain at least {MinimumFrameSize} bytes of data.";
            throw new FormatException(msg);
        }

        _messageImpl.Initialize(frame);
        InitializeUnique(frame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    protected abstract void InitializeUnique(byte[] frame);
}
