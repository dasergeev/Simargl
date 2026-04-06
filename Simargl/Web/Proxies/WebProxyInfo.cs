using System.Net;
using System.Runtime.CompilerServices;

namespace Simargl.Web.Proxies;

/// <summary>
/// Представляет информацию о подключении к прокси-серверу.
/// </summary>
public sealed class WebProxyInfo :
    IEquatable<WebProxyInfo>
{
    /// <summary>
    /// Поле для хранения хэш-кода.
    /// </summary>
    private readonly int _HashCode;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="type">
    /// Значение, определяющее тип прокси-сервера.
    /// </param>
    /// <param name="address">
    /// Адрес прокси-сервера.
    /// </param>
    /// <param name="port">
    /// Порт для подключения к прокси-серверу.
    /// </param>
    public WebProxyInfo(WebProxyType type, string address, int port) :
        this(type, address, port, string.Empty, string.Empty)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="type">
    /// Значение, определяющее тип прокси-сервера.
    /// </param>
    /// <param name="address">
    /// Адрес прокси-сервера.
    /// </param>
    /// <param name="port">
    /// Порт для подключения к прокси-серверу.
    /// </param>
    public WebProxyInfo(WebProxyType type, IPAddress address, int port) :
        this(type, address, port, string.Empty, string.Empty)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="type">
    /// Значение, определяющее тип прокси-сервера.
    /// </param>
    /// <param name="address">
    /// Адрес прокси-сервера.
    /// </param>
    /// <param name="port">
    /// Порт для подключения к прокси-серверу.
    /// </param>
    /// <param name="username">
    /// Имя пользователя.
    /// </param>
    /// <param name="password">
    /// Пароль.
    /// </param>
    public WebProxyInfo(WebProxyType type, string address, int port, string username, string password) :
        this(type, IPAddress.Parse(address), port, username, password)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="type">
    /// Значение, определяющее тип прокси-сервера.
    /// </param>
    /// <param name="address">
    /// Адрес прокси-сервера.
    /// </param>
    /// <param name="port">
    /// Порт для подключения к прокси-серверу.
    /// </param>
    /// <param name="username">
    /// Имя пользователя.
    /// </param>
    /// <param name="password">
    /// Пароль.
    /// </param>
    public WebProxyInfo(WebProxyType type, IPAddress address, int port, string username, string password)
    {
        //  Установка значений свойств.
        Type = IsDefined(type);
        Address = IsNotNull(address);
        Port = IsNotLarger(IsNotLess(port, IPEndPoint.MinPort), IPEndPoint.MaxPort);
        Username = IsNotNull(username);
        Password = IsNotNull(password);

        //  Получение хэш-кода.
        _HashCode = HashCode.Combine(Address, Port, Type, Username, Password);

        //  Получение уникального ключа.
        Key = string.Join(string.Empty, Address, Port, Type, Username, Password);
    }

    /// <summary>
    /// Возвращает значение, определяющее тип прокси-сервера.
    /// </summary>
    public WebProxyType Type { get; }

    /// <summary>
    /// Возвращает адрес прокси-сервера.
    /// </summary>
    public IPAddress Address { get; }

    /// <summary>
    /// Возвращает порт для подключения к прокси-серверу.
    /// </summary>
    public int Port { get; }

    /// <summary>
    /// Возвращает имя пользователя.
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// Возвращает пароль.
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// Возвращает уникальный ключ.
    /// </summary>
    internal string Key { get; }

    /// <summary>
    /// Выполняет операцию проверки на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator ==(WebProxyInfo? left, WebProxyInfo? right)
    {
        //  Проверка ссылок.
        if (ReferenceEquals(left, right)) return true;

        //  Проверка пустых ссылок.
        if (left is null || right is null) return false;

        //  Сравнение объектов.
        return EqualsCore(left, right);
    }

    /// <summary>
    /// Выполняет операцию проверки на неравенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator !=(WebProxyInfo? left, WebProxyInfo? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Выполняет проверку равенства объектов.
    /// </summary>
    /// <param name="other">
    /// Экземпляр для сравнения.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    bool IEquatable<WebProxyInfo>.Equals(WebProxyInfo? other)
    {
        //  Проверка ссылки.
        if (other is null) return false;

        //  Сравнение объектов.
        return EqualsCore(this, other);
    }

    /// <summary>
    /// Выполняет проверку равенства объектов.
    /// </summary>
    /// <param name="obj">
    /// Экземпляр для сравнения.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public override bool Equals(object? obj)
    {
        //  Проверка типа.
        if (obj is not WebProxyInfo other) return false;

        //  Сравнение объектов.
        return EqualsCore(this, other);
    }

    /// <summary>
    /// Возвращает хэш-код.
    /// </summary>
    /// <returns>
    /// Хэш-код.
    /// </returns>
    public override int GetHashCode()
    {
        return _HashCode;
    }

    /// <summary>
    /// Выполняет операцию проверки на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool EqualsCore(WebProxyInfo left, WebProxyInfo right)
    {
        //  Проверка хэш-кодов.
        if (left._HashCode != right._HashCode) return false;

        //  Сравнение ключей.
        return left.Key == right.Key;
    }
}
