using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Unme.Common;

namespace Simargl.Net.Modbus.Device;

/// <summary>
///     Modbus device.
/// </summary>
public abstract class ModbusDevice :
    IDisposable
{
    private IModbusTransport _transport;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transport"></param>
    [CLSCompliant(false)]
    protected ModbusDevice(IModbusTransport transport)
    {
        _transport = transport;
    }

    /// <summary>
    ///     Gets the Modbus Transport.
    /// </summary>
    [CLSCompliant(false)]
    public IModbusTransport Transport => _transport;

    /// <summary>
    ///     Releases unmanaged and - optionally - managed resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    ///     <c>true</c> to release both managed and unmanaged resources;
    ///     <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DisposableUtility.Dispose(ref _transport);
        }
    }
}
