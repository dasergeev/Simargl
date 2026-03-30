using Simargl.Net.Modbus.Device;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.IO;

namespace Simargl.Net.Modbus;

/// <summary>
/// Extension methods for the IModbusFactory interface.
/// </summary>
public static class FactoryExtensions
{
    /// <summary>
    /// Creates an RTU master.
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="streamResource"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public static IModbusSerialMaster CreateRtuMaster(this IModbusFactory factory, IStreamResource streamResource)
    {
        IModbusRtuTransport transport = factory.CreateRtuTransport(streamResource);

        return new ModbusSerialMaster(transport);
    }

    /// <summary>
    /// Creates an ASCII master.
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="streamResource"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public static IModbusSerialMaster CreateAsciiMaster(this IModbusFactory factory, IStreamResource streamResource)
    {
        IModbusAsciiTransport transport = factory.CreateAsciiTransport(streamResource);

        return new ModbusSerialMaster(transport);
    }

    /// <summary>
    /// Creates a TCP or UDP master.
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="streamResource"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public static IModbusMaster CreateIpMaster(this IModbusFactory factory, IStreamResource streamResource)
    {
        IModbusTransport transport = factory.CreateIpTransport(streamResource);
        return new ModbusIpMaster(transport);
    }

    /// <summary>
    /// Creates an RTU slave network.
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="streamResource"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public static IModbusSlaveNetwork CreateRtuSlaveNetwork(this IModbusFactory factory,
        IStreamResource streamResource)
    {
        IModbusRtuTransport transport = factory.CreateRtuTransport(streamResource);

        return factory.CreateSlaveNetwork(transport);
    }

    /// <summary>
    /// Creates an ASCII slave network.
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="streamResource"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public static IModbusSlaveNetwork CreateAsciiSlaveNetwork(this IModbusFactory factory,
        IStreamResource streamResource)
    {
        IModbusAsciiTransport transport = factory.CreateAsciiTransport(streamResource);

        return factory.CreateSlaveNetwork(transport);
    }
}
