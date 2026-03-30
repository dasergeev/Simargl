namespace Simargl.Embedded.Modbus;

/// <summary>
/// Представляет пакет, содержащий данные PDU.
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
public sealed class PduPackage(PduFunctionCode functionCode, byte[] data)
{
    /// <summary>
    /// Возвращает значение, определяющее код функции.
    /// </summary>
    public PduFunctionCode FunctionCode { get; } = IsDefined(functionCode, nameof(functionCode));

    /// <summary>
    /// Возвращает данные пакета.
    /// </summary>
    public byte[] Data { get; } = IsNotNull(data, nameof(data));
}
