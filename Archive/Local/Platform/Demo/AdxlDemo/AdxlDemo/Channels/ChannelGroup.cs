using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет группу каналов.
/// </summary>
public sealed class ChannelGroup :
    Element,
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения серийного номера датчика.
    /// </summary>
    private long _SerialNumber;

    /// <summary>
    /// Поле для хранения массива организаторов канала.
    /// </summary>
    private readonly ChannelOrganizer[] _Channels;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="number">
    /// Номер группы каналов.
    /// </param>
    /// <param name="serialNumber">
    /// Серийный номер датчика.
    /// </param>
    /// <param name="channels">
    /// Массив организаторов каналов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="channels"/> передана пустая ссылка.
    /// </exception>
    public ChannelGroup(Engine engine, int number, long serialNumber, ChannelOrganizer[] channels) :
        base(engine)
    {
        //  Установка полей класса.
        Number = number;
        _SerialNumber = serialNumber;
        _Channels = IsNotNull(channels, nameof(channels));
    }

    /// <summary>
    /// Возвращает номер группы каналов.
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// Возвращает или задаёт серийный номер датчика.
    /// </summary>
    public long SerialNumber
    {
        get => _SerialNumber;
        set
        {
            //  Выполнение в основном потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_SerialNumber != value)
                {
                    //  Перебор организаторов каналов.
                    foreach (ChannelOrganizer channel in _Channels)
                    {
                        //  Установка серийного.
                        channel.SetSerialNumber(value);
                    }

                    //  Установка нового значения.
                    _SerialNumber = value;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(SerialNumber)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает коллекцию организаторов каналов.
    /// </summary>
    public IEnumerable<ChannelOrganizer> Channels => _Channels;

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        PropertyChanged?.Invoke(this, e);
    }
}
