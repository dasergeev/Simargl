using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.IO;
using Simargl.Net.Modbus.Extensions;
using Simargl.Net.Modbus.Logging;
using Simargl.Net.Modbus.Device.MessageHandlers;
using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Device;

namespace Simargl.Net.Modbus;

/// <summary>
/// 
/// </summary>
public class ModbusFactory : IModbusFactory
{
    /// <summary>
    /// The "built-in" message handlers.
    /// </summary>
    private static readonly IModbusFunctionService[] BuiltInFunctionServices = 
    {
        new ReadCoilsService(),
        new ReadInputsService(),
        new ReadHoldingRegistersService(),
        new ReadInputRegistersService(),
        new DiagnosticsService(),
        new WriteSingleCoilService(),
        new WriteSingleRegisterService(),
        new WriteMultipleCoilsService(),
        new WriteMultipleRegistersService(),
        new WriteFileRecordService(),
        new ReadWriteMultipleRegistersService(),
    };

    private readonly IDictionary<byte, IModbusFunctionService> _functionServices;

    /// <summary>
    /// Create a factory which uses the built in standard slave function handlers.
    /// </summary>
    public ModbusFactory()
    {
        _functionServices = BuiltInFunctionServices.ToDictionary(s => s.FunctionCode, s => s);

        Logger = NullModbusLogger.Instance;
    }

    /// <summary>
    /// Create a factory which optionally uses the built in function services and allows custom services to be added.
    /// </summary>
    /// <param name="functionServices">User provided function services.</param>
    /// <param name="includeBuiltIn">If true, the built in function services are included. Otherwise, all function services will come from the functionService parameter.</param>
    /// <param name="logger">Logger</param>
    [CLSCompliant(false)]
    public ModbusFactory(
        IEnumerable<IModbusFunctionService> functionServices = null!, 
        bool includeBuiltIn = true, 
        IModbusLogger logger = null!)
    {
        Logger = logger ?? NullModbusLogger.Instance;

        //Determine if we're including the built in services
        if (includeBuiltIn)
        {
            //Make a dictionary out of the built in services
            _functionServices = BuiltInFunctionServices
                .ToDictionary(s => s.FunctionCode, s => s);
        }
        else
        {
            //Create an empty dictionary
            _functionServices = new Dictionary<byte, IModbusFunctionService>();
        }

        if (functionServices != null)
        {
            //Add and replace the provided function services as necessary.
            foreach (IModbusFunctionService service in functionServices)
            {
                //This will add or replace the service.
                _functionServices[service.FunctionCode] = service;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unitId"></param>
    /// <param name="dataStore"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusSlave CreateSlave(byte unitId, ISlaveDataStore? dataStore = null)
    {
        if (dataStore == null)
            dataStore = new DefaultSlaveDataStore();

        return new ModbusSlave(unitId, dataStore, GetAllFunctionServices());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transport"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusSlaveNetwork CreateSlaveNetwork(IModbusRtuTransport transport)
    {
        return new ModbusSerialSlaveNetwork(transport, this, Logger);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transport"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusSlaveNetwork CreateSlaveNetwork(IModbusAsciiTransport transport)
    {
        return new ModbusSerialSlaveNetwork(transport, this, Logger);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tcpListener"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusTcpSlaveNetwork CreateSlaveNetwork(TcpListener tcpListener)
    {
        return new ModbusTcpSlaveNetwork(tcpListener, this, Logger);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusSlaveNetwork CreateSlaveNetwork(UdpClient client)
    {
        return new ModbusUdpSlaveNetwork(client, this, Logger);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="streamResource"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusRtuTransport CreateRtuTransport(IStreamResource streamResource)
    {
        return new ModbusRtuTransport(streamResource, this, Logger);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="streamResource"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusAsciiTransport CreateAsciiTransport(IStreamResource streamResource)
    {
        return new ModbusAsciiTransport(streamResource, this, Logger);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="streamResource"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusTransport CreateIpTransport(IStreamResource streamResource)
    {
        return new ModbusIpTransport(streamResource, this, Logger);
    }

    /// <summary>
    /// 
    /// </summary>
    public IModbusLogger Logger { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusFunctionService[] GetAllFunctionServices()
    {
        return _functionServices
            .Values
            .ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transport"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusSerialMaster CreateMaster(IModbusSerialTransport transport)
    {
        return new ModbusSerialMaster(transport);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusMaster CreateMaster(UdpClient client)
    {
        var adapter = new UdpClientAdapter(client);

        var transport = new ModbusIpTransport(adapter, this, Logger);

        return new ModbusIpMaster(transport);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusMaster CreateMaster(TcpClient client)
    {
        var adapter = new TcpClientAdapter(client);

        var transport = new ModbusIpTransport(adapter, this, Logger);

        return new ModbusIpMaster(transport);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusMaster CreateMaster(Socket client)
    {
        var adapter = new SocketAdapter(client);

        var transport = new ModbusRtuTransport(adapter, this, Logger);

        return new ModbusSerialMaster(transport);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="functionCode"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public IModbusFunctionService GetFunctionService(byte functionCode)
    {
        return _functionServices.GetValueOrDefault(functionCode);
    }
}
