using Simargl.Net.Modbus.Extensions;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Logging;

namespace Simargl.Net.Modbus.Device;

/// <summary>
/// 
/// </summary>
public abstract class ModbusSlaveNetwork :
    ModbusDevice,
    IModbusSlaveNetwork
{
    private readonly IModbusLogger _logger;
    private readonly IDictionary<byte, IModbusSlave> _slaves = new ConcurrentDictionary<byte, IModbusSlave>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transport"></param>
    /// <param name="modbusFactory"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [CLSCompliant(false)]
    protected ModbusSlaveNetwork(IModbusTransport transport, IModbusFactory modbusFactory, IModbusLogger logger) :
        base(transport)
    {
        ModbusFactory = modbusFactory;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    protected IModbusFactory ModbusFactory { get; }

    /// <summary>
    /// 
    /// </summary>
    protected IModbusLogger Logger
    {
        get { return _logger; }
    }

    /// <summary>
    /// Start slave listening for requests.
    /// </summary>
    public abstract Task ListenAsync(CancellationToken cancellationToken = new CancellationToken());

    /// <summary>
    /// Apply the request.
    /// </summary>
    /// <param name="request"></param>
    [CLSCompliant(false)]
    protected IModbusMessage ApplyRequest(IModbusMessage request)
    {
        //Check for broadcast requests
        if (request.SlaveAddress == 0)
        {
            //Grab each slave
            foreach (var slave in _slaves.Values)
            {
                try
                {
                    //Apply the request
                    slave.ApplyRequest(request);
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error applying request to slave {slave.UnitId}: {ex.Message}");
                }
            }
        }
        else
        {
            //Attempt to find a slave for this address
            IModbusSlave slave = GetSlave(request.SlaveAddress);

            // only service requests addressed to our slaves
            if (slave == null)
            {
                Console.WriteLine($"NModbus Slave Network ignoring request intended for NModbus Slave {request.SlaveAddress}");
            }
            else
            {
                // perform action
                return slave.ApplyRequest(request);
            }
        }

        return null!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slave"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [CLSCompliant(false)]
    public void AddSlave(IModbusSlave slave)
    {
        if (slave == null) throw new ArgumentNullException(nameof(slave));

        _slaves.Add(slave.UnitId, slave);

        Logger.Information($"Slave {slave.UnitId} added to slave network.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitId"></param>
    public void RemoveSlave(byte unitId)
    {
        _slaves.Remove(unitId);

        Logger.Information($"Slave {unitId} removed from slave network.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitId"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusSlave GetSlave(byte unitId)
    {
        return _slaves.GetValueOrDefault(unitId);
    }
}
