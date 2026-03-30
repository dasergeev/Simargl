namespace Apeiron.Services.GlobalIdentity;

/// <summary>
/// Класс описывающий выборку данных для дальнейшего отображения в DataGrid.
/// </summary>
public class EntityRowGrid : ViewModel
{
    private string? _Name;
    private long _Value;
    private DateTime _ReceiptTime;
    private DateTime _PacketTime;
    private string? _LastAddress;
    private int _LastPort;
    private int _LastVersion;

    /// <summary>
    /// Возвращает имя идентифицируемой системы.
    /// </summary>
    public string Name 
    { 
        get => _Name ?? string.Empty;
        set => Set(ref _Name, value);
        //{
        //    if (_Name == value)
        //        return;
        //    _Name = value;
        //    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Name)));
        //}
    }

    /// <summary>
    /// Возвращает Идентификатор системы.
    /// </summary>
    public long Value
    {
        get => _Value;
        set => Set(ref _Value, value);
    }

    /// <summary>
    /// Возвращает или задаёт время получения сообщения на сервер.
    /// </summary>
    public DateTime ReceiptTime 
    { 
        get => _ReceiptTime;
        set => Set(ref _ReceiptTime, value);
    }

    /// <summary>
    /// Возвращает или задаёт время создания пакета.
    /// </summary>
    public DateTime PacketTime
    {
        get => _PacketTime;
        set => Set(ref _PacketTime, value);
    }

    /// <summary>
    /// Возвращает значение последнего IP адреса с которого получен идентификационный пакет от системы.
    /// </summary>
    public string LastAddress
    {
        get => _LastAddress ?? string.Empty;
        set => Set(ref _LastAddress, value);
    }

    /// <summary>
    /// Возвращает значение последнего порта UDP с которого получен идентификационный пакет от системы.
    /// </summary>
    public int LastPort
    {
        get => _LastPort;
        set => Set(ref _LastPort, value);
    }

    /// <summary>
    /// Возвращает значение последнего порта UDP с которого получен идентификационный пакет от системы.
    /// </summary>
    public int LastVersion
    {
        get => _LastVersion;
        set => Set(ref _LastVersion, value);
    }
}

