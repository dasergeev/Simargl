namespace Simargl.Frames.Mera;

/// <summary>
/// Представляет заголовок кадра в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
public sealed class MeraFrameHeader() :
    FrameHeader(StorageFormat.Mera)
{
    /// <summary>
    /// Поле для хранения названия испытаний.
    /// </summary>
    private string _Title = string.Empty;

    /// <summary>
    /// Поле для хранения названия изделия.
    /// </summary>
    private string _Product = string.Empty;

    /// <summary>
    /// Поле для хранения названия приложения, в котором записывались данные.
    /// </summary>
    private string _DataSourceApplication = "MERA Recorder";

    /// <summary>
    /// Поле для хранения версии источника данных.
    /// </summary>
    private string _DataSourceVersion = "3.4.0.16";

    /// <summary>
    /// Возвращает или задаёт название испытаний.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Title
    {
        get => _Title;
        set => _Title = IsNotNull(value, nameof(Title));
    }

    /// <summary>
    /// Возвращает или задаёт название изделия.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Product
    {
        get => _Product;
        set => _Product = IsNotNull(value, nameof(Product));
    }

    /// <summary>
    /// Возвращает или задаёт время проведения испытаний.
    /// </summary>
    public DateTime Time { get; set; } = default;

    /// <summary>
    /// Возвращает или задаёт название приложения, в котором записывались данные.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string DataSourceApplication
    {
        get => _DataSourceApplication;
        set => _DataSourceApplication = IsNotNull(value, nameof(DataSourceApplication));
    }

    /// <summary>
    /// Возвращает или задаёт версию источника данных.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string DataSourceVersion
    {
        get => _DataSourceVersion;
        set => _DataSourceVersion = IsNotNull(value, nameof(DataSourceVersion));
    }

    /// <summary>
    /// Возвращает копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public override FrameHeader Clone()
    {
        //  Создание и возврат копии объекта.
        return new MeraFrameHeader()
        {
            _Title = _Title,
            _Product = _Product,
            Time = Time,
            _DataSourceApplication = _DataSourceApplication,
            _DataSourceVersion = _DataSourceVersion,
        };
    }
}
