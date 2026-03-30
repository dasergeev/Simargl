namespace Apeiron.Platform.Demo.AdxlDemo.Modbus;

/// <summary>
/// Представляет пакет, содержащий данные PDU.
/// </summary>
public sealed class PduPackage
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="functionCode">
    /// Значение, определяющее код функции.
    /// </param>
    /// <param name="data">
    /// Данные пакета.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="functionCode"/> передано значение,
    /// которое не содержится в перечислении <see cref="PduFunctionCode"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    public PduPackage(PduFunctionCode functionCode, byte[] data)
    {
        //  Установка кода функции.
        FunctionCode = IsDefined(functionCode, nameof(functionCode));

        //  Установка данных пакета.
        Data = IsNotNull(data, nameof(data));
    }

    /// <summary>
    /// Возвращает значение, определяющее код функции.
    /// </summary>
    public PduFunctionCode FunctionCode { get; }

    /// <summary>
    /// Возвращает данные пакета.
    /// </summary>
    public byte[] Data { get; }
}
