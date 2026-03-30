namespace Simargl.Hardware.Strain.Demo.ReWrite;

/// <summary>
/// Представляет класс, предоставляющий аргумент события новых данных датчика.
/// </summary>
internal sealed class DataEventArgs :
    EventArgs
{
    /// <summary>
    /// Представляет серийный номер датчика.
    /// </summary>
    internal uint SerialNumber { get; init; }

    /// <summary>
    /// Поле данных события.
    /// </summary>
    internal float[][] Data { get; init; }

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="serialNumber">
    /// Cерийный номер.
    /// </param>
    /// <param name="data">
    /// Новые данные.
    /// </param>
    internal DataEventArgs([NoVerify] uint serialNumber, [NoVerify] float[][] data)
    {
        //  Инициализация адреса.
        SerialNumber = serialNumber;

        //  Инциализация данных.
        Data = (float[][])data.Clone();
    }

}
