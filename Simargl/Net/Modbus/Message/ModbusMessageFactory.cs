using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Message;

/// <summary>
/// Modbus message factory.
/// </summary>
public static class ModbusMessageFactory
{
    /// <summary>
    /// Create a Modbus message.
    /// </summary>
    /// <typeparam name="T">Modbus message type.</typeparam>
    /// <param name="frame">Bytes of Modbus frame.</param>
    /// <returns>New Modbus message based on type and frame bytes.</returns>
    [CLSCompliant(false)]
    public static T CreateModbusMessage<T>(byte[] frame)
        where T : IModbusMessage, new()
    {
        //Create the message
        T message = new T();

        //initialize it
        message.Initialize(frame);

        //return it
        return message;
    }
}
