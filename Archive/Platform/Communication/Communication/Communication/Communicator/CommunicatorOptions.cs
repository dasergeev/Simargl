using Apeiron.Platform.Communication.Remoting;

namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет параметры коммуникатора.
/// </summary>
public sealed class CommunicatorOptions
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="connectionOptions">
    /// Параметры подключения к серверу.
    /// </param>
    /// <param name="remoteInvokeOptions">
    /// Параметры удалённого вызова.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="connectionOptions"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="remoteInvokeOptions"/> передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </exception>
    public CommunicatorOptions(
        ConnectionOptions connectionOptions,
        RemoteInvokeOptions remoteInvokeOptions)
    {
        //  Установка параметров подключения к серверу.
        ConnectionOptions = IsNotNull(connectionOptions, nameof(connectionOptions));

        //  Проверка параметров удалённого вызова.
        if (remoteInvokeOptions.IsEmpty)
        {
            //  Передано значение, которое не соответствует допустимому диапазону значений.
            throw Exceptions.ArgumentOutOfRange(nameof(remoteInvokeOptions));
        }

        //  Установка параметров удалённого вызова.
        RemoteInvokeOptions = remoteInvokeOptions;
    }

    /// <summary>
    /// Возвращает параметры подключения к серверу.
    /// </summary>
    public ConnectionOptions ConnectionOptions { get; }

    /// <summary>
    /// Возвращает параметры удалённого вызова.
    /// </summary>
    public RemoteInvokeOptions RemoteInvokeOptions { get; }

    /// <summary>
    /// Выполняет нормализацию параметров удалённого вызова.
    /// </summary>
    /// <param name="options">
    /// Параметры удалённого вызова, которые необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованные параметры удалённого вызова.
    /// </returns>
    internal RemoteInvokeOptions NormalizationRemoteInvokeOptions(RemoteInvokeOptions options)
    {
        //  Проверка пустых параметров.
        if (options.IsEmpty)
        {
            //  Установка параметров по умолчанию.
            options = RemoteInvokeOptions;
        }

        //  Возврат нормализованных параметров.
        return options;
    }
}
