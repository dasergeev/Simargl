using System.Net;

namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет параметры подключения к серверу.
/// </summary>
public sealed class ConnectionOptions
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="hostName">
    /// Имя хоста сервера.
    /// </param>
    /// <param name="port">
    /// Порт для подключения к серверу.
    /// </param>
    /// <param name="login">
    /// Логин для подключения к серверу.
    /// </param>
    /// <param name="password">
    /// Пароль для подключения к серверу.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="hostName"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="hostName"/> передана пустая строка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="port"/> передано значение,
    /// которое меньше значения <see cref="IPEndPoint.MinPort"/>
    /// или больше значения <see cref="IPEndPoint.MaxPort"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="login"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="login"/> передана пустая строка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="password"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="password"/> передана пустая строка.
    /// </exception>
    public ConnectionOptions(string hostName, int port, string login, string password)
    {
        //  Установка имени хоста.
        HostName = IsNotEmpty(hostName, nameof(hostName));

        //  Установка порта для подключения к серверу.
        Port = IsInRange(port, IPEndPoint.MinPort, IPEndPoint.MaxPort, nameof(port));

        //  Установка логина для подключения к серверу.
        Login = IsNotEmpty(login, nameof(login));

        //  Установка пароля для подключения к серверу.
        Password = IsNotEmpty(password, nameof(password));
    }

    /// <summary>
    /// Возвращает имя хоста сервера.
    /// </summary>
    public string HostName { get; }

    /// <summary>
    /// Возвращает порт для подключения к серверу.
    /// </summary>
    public int Port { get; }

    /// <summary>
    /// Возвращает логин для подключения к серверу.
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Возвращает пароль для подключения к серверу.
    /// </summary>
    public string Password { get; }
}
