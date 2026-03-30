using System;
using System.Net;

namespace PointerDataViewer;

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
    internal DataEventArgs([ParameterNoChecks] uint serialNumber, [ParameterNoChecks] float[][] data)
    {
        //  Инициализация адреса.
        SerialNumber = serialNumber;

        //  Инциализация данных.
        Data = (float[][])data.Clone();
    }

}
