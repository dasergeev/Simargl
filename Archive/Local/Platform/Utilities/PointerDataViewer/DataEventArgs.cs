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
    /// Представляет IP адрес датчика.
    /// </summary>
    internal IPAddress SensorAddress { get; init; }

    /// <summary>
    /// Поле данных события.
    /// </summary>
    internal float[][] Data { get; init; }

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="address">
    /// IP адресс датчика.
    /// </param>
    /// <param name="data">
    /// Новые данные.
    /// </param>
    internal DataEventArgs([ParameterNoChecks]IPAddress address, [ParameterNoChecks] float[][] data)
    {
        //  Инициализация адреса.
        SensorAddress = address;

        //  Инциализация данных.
        Data = (float[][])data.Clone();
    }

}
