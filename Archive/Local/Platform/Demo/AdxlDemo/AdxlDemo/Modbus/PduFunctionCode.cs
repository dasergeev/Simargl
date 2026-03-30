namespace Apeiron.Platform.Demo.AdxlDemo.Modbus;

/// <summary>
/// Представляет значение, определяющее код функции.
/// </summary>
public enum PduFunctionCode :
    byte
{
    /// <summary>
    /// Чтение регистров хранения.
    /// </summary>
    ReadHoldings = 3,

    /// <summary>
    /// Запись регистров хранения.
    /// </summary>
    WriteHoldings = 0x10
}
