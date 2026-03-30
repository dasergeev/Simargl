namespace Simargl.Net.Modbus.Interfaces;

/// <summary>
///     Modbus TCP slave device.
/// </summary>
[CLSCompliant(false)]
public interface IModbusTcpSlaveNetwork :
    IModbusSlaveNetwork
{
    /// <summary>
    ///     Gets the Modbus TCP Masters connected to this Modbus TCP Slave.
    /// </summary>
    ReadOnlyCollection<TcpClient> Masters { get; }
}
