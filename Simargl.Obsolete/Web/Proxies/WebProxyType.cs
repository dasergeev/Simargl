namespace Simargl.Web.Proxies;

/// <summary>
/// Представляет значение, определяющее тип прокси сервера.
/// </summary>
public enum WebProxyType
{
    /// <summary>
    /// HTTP-прокси
    /// </summary>
    Http = 0,

    /// <summary>
    /// HTTPS-прокси.
    /// </summary>
    Https = 1,

    /// <summary>
    /// SOCKS4-прокси.
    /// </summary>
    Socks4 = 2,

    /// <summary>
    /// SOCKS5-прокси.
    /// </summary>
    Socks5 = 3,
}
