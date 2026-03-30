using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Message;

/// <summary>
///     Methods specific to a modbus request message.
/// </summary>
[CLSCompliant(false)]
public interface IModbusRequest :
    IModbusMessage
{
    /// <summary>
    ///     Validate the specified response against the current request.
    /// </summary>
    void ValidateResponse(IModbusMessage response);
}
