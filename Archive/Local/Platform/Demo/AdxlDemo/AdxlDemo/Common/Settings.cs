namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет общие настройки приложения.
/// </summary>
public sealed class Settings
{
    /// <summary>
    /// Возвращает путь к файлу базы данных, используемый для отладки.
    /// </summary>
    public const string DebugDatabasePath = @"D:\Publish\AdxlDemo\AdxlDemo.db";

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="application">
    /// Текущее приложение.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="application"/> передана пустая ссылка.
    /// </exception>
    public Settings(App application)
    {
        //  Проверка ссылки на приложение.
        IsNotNull(application, nameof(application));

        //  Установка шага синхронизатора в милисекундах.
        SynchronizerStep = 2;

        //  Установка пути к файлу базы данных.
        DatabasePath = @"D:\Publish\AdxlDemo\AdxlDemo.db";

        //  Установка пути к каталогу с данными.
        DataPath = @"D:\Publish\AdxlDemo\Data";

        //  Установка значения, определяющего, следует ли захватывать датчики.
        IsDeviceCapture = true;

        //  Установка длительности фрагмента в секундах.
        FragmentDuration = 30;

        //  Установка интервала таймера средства вызова методов.
        InvokerInterval = TimeSpan.FromMilliseconds(100);

        //  Установка размера кэша канала.
        ChannelCacheSize = 1000;
    }

    /// <summary>
    /// Возвращает шаг синхронизатора в милисекундах.
    /// </summary>
    public int SynchronizerStep { get; }

    /// <summary>
    /// Возвращает путь к файлу базы данных.
    /// </summary>
    public string DatabasePath { get; }

    /// <summary>
    /// Возвращает путь к каталогу с данными.
    /// </summary>
    public string DataPath { get; }

    /// <summary>
    /// Возвращает значение, определяющее, следует ли захватывать датчики.
    /// </summary>
    public bool IsDeviceCapture { get; }

    /// <summary>
    /// Возвращает длительность фрагментов в секунадх.
    /// </summary>
    public int FragmentDuration { get; }

    /// <summary>
    /// Возвращает интервал таймера средства вызова методов.
    /// </summary>
    public TimeSpan InvokerInterval { get; }

    /// <summary>
    /// Возвращает размер кэша канала.
    /// </summary>
    public int ChannelCacheSize { get; }
}
