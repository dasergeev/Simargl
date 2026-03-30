namespace Simargl.Embedded.Modbus;

/// <summary>
/// Представляет значение, определяющее код функции.
/// </summary>
public enum PduFunctionCode :
    byte
{
    /// <summary>
    /// Чтение регистров хранения.
    /// </summary>
    ReadHoldings = 0x03,

    /// <summary>
    /// Запись регистров хранения.
    /// </summary>
    WriteHoldings = 0x10
}
