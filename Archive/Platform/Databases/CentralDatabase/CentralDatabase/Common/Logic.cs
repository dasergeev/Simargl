namespace Apeiron.Platform.Databases.CentralDatabase;

/// <summary>
/// Представляет логику взаимодействия с центральной базой данных.
/// </summary>
public sealed class Logic
{
    /// <summary>
    /// Поле для хранения времени ожидания команд, выполняемых с базой данных.
    /// </summary>
    private TimeSpan _CommandTimeout;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal Logic()
    {
        //  Создание объекта, с помощью которого можно синхронизировать доступ.
        SyncRoot = new();

        //  Создание информации о подключении к базе данных.
        Connection = new(SyncRoot);
    }

    /// <summary>
    /// Возвращает объект, с помощью которого можно синхронизировать доступ.
    /// </summary>
    public object SyncRoot { get; }

    /// <summary>
    /// Возвращает информацию о подключении к базе данных.
    /// </summary>
    public Connection Connection { get; }

    /// <summary>
    /// Возвращает или задаёт время ожидания команд, выполняемых с базой данных.
    /// </summary>
    public TimeSpan CommandTimeout
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _CommandTimeout;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _CommandTimeout = value;
            }
        }
    }
}
