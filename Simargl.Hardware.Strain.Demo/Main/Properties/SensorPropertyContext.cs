using Simargl.Hardware.Strain.Demo.Core;

namespace Simargl.Hardware.Strain.Demo.Main.Properties;

/// <summary>
/// Представляет контекст свойства датчика.
/// </summary>
public sealed class SensorPropertyContext :
    Anything
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="sensor">
    /// Датчик.
    /// </param>
    /// <param name="connection">
    /// Соединение.
    /// </param>
    /// <param name="format">
    /// Значение, определяющее формат свойства датчика.
    /// </param>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="count">
    /// Количество регистров.
    /// </param>
    /// <param name="name">
    /// Имя свойства.
    /// </param>
    /// <param name="description">
    /// Описание свойства.
    /// </param>
    public SensorPropertyContext(
        Sensor sensor, ModbusConnection connection,
        SensorPropertyFormat format,
        int start, int count,
        string name, string description)
    {
        //  Обращение к объекту.
        Lay();

        //  Установка значений свойств.
        Sensor = sensor;
        Connection = connection;
        Format = format;
        Start = start;
        Count = count;
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Возвращает датчик.
    /// </summary>
    public Sensor Sensor { get; }

    /// <summary>
    /// Возвращает соединение.
    /// </summary>
    public ModbusConnection Connection { get; }

    /// <summary>
    /// Возвращает значение, определяющее формат свойства датчика.
    /// </summary>
    public SensorPropertyFormat Format { get; }

    /// <summary>
    /// Возвращает номер первого регистра.
    /// </summary>
    public int Start { get; }

    /// <summary>
    /// Возвращает количество регистров.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Возвращает имя свойства.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает описание свойства.
    /// </summary>
    public string Description { get; }
}
