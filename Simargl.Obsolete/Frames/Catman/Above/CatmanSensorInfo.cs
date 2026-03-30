namespace Simargl.Frames.Catman;

/// <summary>
/// Представляет информацию о сенсоре.
/// </summary>
public sealed class CatmanSensorInfo
{
    /// <summary>
    /// Поле для хранения значения, определяющего учитываются ли свойства датчика в процессе инициализации.
    /// </summary>
    private bool _InUse;

    /// <summary>
    /// Поле для хранения описания.
    /// </summary>
    private string _Description;

    /// <summary>
    /// Поле для хранения идентификатора.
    /// </summary>
    private string _Tid;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public CatmanSensorInfo() :
        this(false, string.Empty, string.Empty)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="inUse">
    /// Значение, определяющее учитываются ли свойства датчика в процессе инициализации.
    /// </param>
    /// <param name="description">
    /// Описание.
    /// </param>
    /// <param name="tid">
    /// Идентификатор.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="description"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="tid"/> передана пустая ссылка.
    /// </exception>
    public CatmanSensorInfo(bool inUse, string description, string tid)
    {
        _InUse = inUse;
        _Description = IsNotNull(description, nameof(description));
        _Tid = IsNotNull(tid, nameof(tid));
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее учитываются ли свойства датчика в процессе инициализации.
    /// </summary>
    public bool InUse
    {
        get => _InUse;
        set => _InUse = value;
    }

    /// <summary>
    /// Возвращает или задаёт описание.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Description
    {
        get => _Description;
        set => _Description = IsNotNull(value, nameof(Description));
    }

    /// <summary>
    /// Возвращает или задаёт идентификатор.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Tid
    {
        get => _Tid;
        set => _Tid = IsNotNull(value, nameof(Tid));
    }
}
