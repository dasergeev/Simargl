using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Journaling;
using System.Net;
using System.Reflection;

namespace Simargl.Hardware.Strain.Demo.Main.Properties;

/// <summary>
/// Представляет коллекцию свойств датчика.
/// </summary>
public sealed class SensorPropertyCollection :
    Anything,
    IEnumerable<SensorProperty>
{
    /// <summary>
    /// Поле для хранения метаданных.
    /// </summary>
    private static readonly IEnumerable<(PropertyInfo Info, SensorPropertyAttribute Attribute)> _Metadata = typeof(SensorPropertyCollection)
        .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(x => x.GetCustomAttribute<SensorPropertyAttribute>() != null && x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(SensorProperty<>))
        .Select(x => new
        {
            Info = x,
            Attribute = x.GetCustomAttribute<SensorPropertyAttribute>()!,
        })
        .Where(x => x.Attribute is not null)
        .OrderBy(x => x.Attribute.Number)
        .Select(x => (x.Info, x.Attribute));

    /// <summary>
    /// Поле для хранения списка свойств.
    /// </summary>
    private readonly List<SensorProperty> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="sensor">
    /// Датчик.
    /// </param>
    /// <param name="connection">
    /// Соединение.
    /// </param>
    public SensorPropertyCollection(Sensor sensor, ModbusConnection connection)
    {
        //  Обращение к объекту.
        Lay();

        Journal journal = ((App)System.Windows.Application.Current).Journal;

        //  Создание списка.
        _Items = [];

        //  Перебор метаданных.
        foreach ((PropertyInfo info, SensorPropertyAttribute attribute) in _Metadata)
        {
            //  Создание контекста.
            SensorPropertyContext context = new(
                sensor, connection,
                attribute.Format, attribute.Start, attribute.Count, attribute.Name, attribute.Description);

            //  Создание свойства.
            if (Activator.CreateInstance(info.PropertyType,
                [context]) is not SensorProperty property)
            {
                //  Переход к следующему свойству.
                continue;
            }

            //  Установка значения свойства.
            info.SetValue(this, property);

            //  Добавление свойства в список.
            _Items.Add(property);
        }
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Count;

    /// <summary>
    /// Возвращает элемент с указанным именем.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным именем.
    /// </returns>
    public SensorProperty this[int index] => _Items[index];

    /// <summary>
    /// Возвращает тип датчика.
    /// </summary>
    [SensorProperty(1, SensorPropertyFormat.ReadOnly, 40001, 8, "Тип датчика", "")]
    public SensorProperty<string> SensorType { get; private set; } = null!;

    /// <summary>
    /// Возвращает версию прошивки.
    /// </summary>
    [SensorProperty(2, SensorPropertyFormat.ReadOnly, 40009, 6, "Версия прошивки", "")]
    public SensorProperty<string> FirmwareVersion { get; private set; } = null!;

    /// <summary>
    /// Возвращает дату изготовления прошивки.
    /// </summary>
    [SensorProperty(3, SensorPropertyFormat.ReadOnly, 40015, 5, "Дата изготовления прошивки", "")]
    public SensorProperty<string> FirmwareDate { get; private set; } = null!;

    /// <summary>
    /// Возвращает серийный номер.
    /// </summary>
    [SensorProperty(4, SensorPropertyFormat.ReadOnly, 40020, 4, "Серийный номер", "")]
    public SensorProperty<string> SerialNumber { get; private set; } = null!;

    /// <summary>
    /// Возвращает UDP порт для запроса идентификации.
    /// </summary>
    [SensorProperty(5, SensorPropertyFormat.ReadWrite, 40024, 1, "UDP порт для запроса идентификации", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> UdpPortIdentification { get; private set; } = null!;

    /// <summary>
    /// Возвращает UDP порт для сервиса логгирования.
    /// </summary>
    [SensorProperty(6, SensorPropertyFormat.ReadWrite, 40025, 1, "UDP порт для сервиса логгирования", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> UdpPortLogging { get; private set; } = null!;

    /// <summary>
    /// Возвращает TCP порт для подключения к серверу.
    /// </summary>
    [SensorProperty(7, SensorPropertyFormat.ReadWrite, 40026, 1, "TCP порт для подключения к серверу", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> TCPServerPort { get; private set; } = null!;

    /// <summary>
    /// Возвращает IP адрес сети.
    /// </summary>
    [SensorProperty(8, SensorPropertyFormat.ReadWrite, 40027, 2, "IP адрес сети", "")]
    public SensorProperty<IPAddress> NetworkAddress { get; private set; } = null!;

    /// <summary>
    /// Возвращает маску подсети.
    /// </summary>
    [SensorProperty(9, SensorPropertyFormat.ReadWrite, 40029, 2, "Маска подсети", "")]
    public SensorProperty<IPAddress> SubnetMask { get; private set; } = null!;

    /// <summary>
    /// Возвращает IP адрес сервера.
    /// </summary>
    [SensorProperty(10, SensorPropertyFormat.ReadWrite, 40031, 2, "IP адрес сервера", "")]
    public SensorProperty<IPAddress> ServerAddress { get; private set; } = null!;

    /// <summary>
    /// Возвращает максимальное значение температуры.
    /// </summary>
    [SensorProperty(11, SensorPropertyFormat.Reset, 40033, 2, "Максимальное значение температуры", "")]
    public SensorProperty<float> MaxTemperature { get; private set; } = null!;

    /// <summary>
    /// Возвращает минимальное значение температуры.
    /// </summary>
    [SensorProperty(12, SensorPropertyFormat.Reset, 40035, 2, "Минимальное значение температуры", "")]
    public SensorProperty<float> MinTemperature { get; private set; } = null!;

    /// <summary>
    /// Возвращает максимальное значение напряжения питания.
    /// </summary>
    [SensorProperty(13, SensorPropertyFormat.Reset, 40037, 2, "Максимальное значение напряжения питания", "")]
    public SensorProperty<float> MaxVoltage { get; private set; } = null!;

    /// <summary>
    /// Возвращает минимальное значение напряжения питания.
    /// </summary>
    [SensorProperty(14, SensorPropertyFormat.Reset, 40039, 2, "Минимальное значение напряжения питания", "")]
    public SensorProperty<float> MinVoltage { get; private set; } = null!;

    /// <summary>
    /// Возвращает смещение времени датчика в мс отн. NTP сервера.
    /// </summary>
    [SensorProperty(15, SensorPropertyFormat.ReadOnly, 40041, 2, "Смещение времени датчика в мс отн. NTP сервера", "")]
    public SensorProperty<float> TimeOffset { get; private set; } = null!;

    /// <summary>
    /// Возвращает желаемую частоту дискретизации, Гц.
    /// </summary>
    [SensorProperty(16, SensorPropertyFormat.ReadWrite, 40043, 1, "Желаемая частота дискретизации, Гц", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> DesiredSampling { get; private set; } = null!;

    /// <summary>
    /// Возвращает режим фильтрации.
    /// </summary>
    [SensorProperty(17, SensorPropertyFormat.ReadWrite, 40044, 1, "Режим фильтрации", "1 - медленно/0 - быстро меняющиеся процессы\r\nмаксимальная частота дискретизации 1.365 кГц / 7.6 кГц")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> FilteringMode { get; private set; } = null!;

    /// <summary>
    /// Возвращает режим записи нулей.
    /// </summary>
    [SensorProperty(18, SensorPropertyFormat.ReadWrite, 40045, 1, "Режим записи нулей", "1 - запись нулей при старте")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> AutoZeroMode { get; private set; } = null!;

    /// <summary>
    /// Возвращает реальную частоту дискретизации, Гц.
    /// </summary>
    [SensorProperty(19, SensorPropertyFormat.ReadOnly, 40046, 2, "Реальная частота дискретизации, Гц", "")]
    public SensorProperty<float> RealSampling { get; private set; } = null!;

    /// <summary>
    /// Возвращает реальную полосу пропускания, Гц.
    /// </summary>
    [SensorProperty(20, SensorPropertyFormat.ReadOnly, 40048, 2, "Реальная полоса пропускания, Гц", "")]
    public SensorProperty<float> RealBandwidth { get; private set; } = null!;

    /// <summary>
    /// Возвращает число каналов AD7730.
    /// </summary>
    [SensorProperty(21, SensorPropertyFormat.ReadOnly, 40050, 1, "Число каналов AD7730", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> ChannelsCount { get; private set; } = null!;

    /// <summary>
    /// Возвращает коды ошибок чип AD7730#0.
    /// </summary>
    [SensorProperty(22, SensorPropertyFormat.Reset, 40051, 1, "Коды ошибок чипа AD7730#0", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> Chip0ErrorСodes { get; private set; } = null!;

    /// <summary>
    /// Возвращает коды ошибок чип AD7730#1.
    /// </summary>
    [SensorProperty(23, SensorPropertyFormat.Reset, 40052, 1, "Коды ошибок чипа AD7730#1", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> Chip1ErrorСodes { get; private set; } = null!;

    /// <summary>
    /// Возвращает коды ошибок чип AD7730#2.
    /// </summary>
    [SensorProperty(24, SensorPropertyFormat.Reset, 40053, 1, "Коды ошибок чипа AD7730#2", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> Chip2ErrorСodes { get; private set; } = null!;

    /// <summary>
    /// Возвращает коды ошибок чип AD7730#3.
    /// </summary>
    [SensorProperty(25, SensorPropertyFormat.Reset, 40054, 1, "Коды ошибок чипа AD7730#3", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> Chip3ErrorСodes { get; private set; } = null!;

    /// <summary>
    /// Возвращает состояние датчика.
    /// </summary>
    [SensorProperty(26, SensorPropertyFormat.Service, 40055, 1, "Состояние датчика", "")]
    [CLSCompliant(false)]
    public SensorProperty<ushort> Status { get; private set; } = null!;

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<SensorProperty> GetEnumerator()
    {
        //  Возврат перечислителя списка.
        return ((IEnumerable<SensorProperty>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка.
        return _Items.GetEnumerator();
    }
}
