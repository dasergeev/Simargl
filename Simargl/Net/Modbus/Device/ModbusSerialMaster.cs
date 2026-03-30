using Simargl.Net.Modbus.Data;
using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Message;

namespace Simargl.Net.Modbus.Device;

/// <summary>
///     Modbus serial master device.
/// </summary>
public class ModbusSerialMaster :
    ModbusMaster,
    IModbusSerialMaster
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="transport"></param>
    [CLSCompliant(false)]
    public ModbusSerialMaster(IModbusSerialTransport transport) :
        base(transport)
    {
    }

    /// <summary>
    ///     Gets the Modbus Transport.
    /// </summary>
    IModbusSerialTransport IModbusSerialMaster.Transport => (IModbusSerialTransport)Transport;

    /// <summary>
    ///     Serial Line only.
    ///     Diagnostic function which loops back the original data.
    ///     NModbus only supports looping back one ushort value, this is a limitation of the "Best Effort" implementation of
    ///     the RTU protocol.
    /// </summary>
    /// <param name="slaveAddress">Address of device to test.</param>
    /// <param name="data">Data to return.</param>
    /// <returns>Return true if slave device echoed data.</returns>
    [CLSCompliant(false)]
    public bool ReturnQueryData(byte slaveAddress, ushort data)
    {
        DiagnosticsRequestResponse request = new DiagnosticsRequestResponse(
            ModbusFunctionCodes.DiagnosticsReturnQueryData,
            slaveAddress,
            new RegisterCollection(data));

        DiagnosticsRequestResponse response = Transport.UnicastMessage<DiagnosticsRequestResponse>(request);

        return response.Data[0] == data;
    }
}
