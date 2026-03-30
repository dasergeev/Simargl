namespace Simargl.Web.Site.Core;

/// <summary>
/// Представляет позицию услуг.
/// </summary>
public class ServicePosition
{
    /// <summary>
    /// Возвращает или инициализирует ключ позиции.
    /// </summary>
    public int Key { get; init; }

    /// <summary>
    /// Возвращает или инициализирует имя позиции.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Возвращает или инициализирует путь к изображению позиции.
    /// </summary>
    public string ImageUrl { get; init; } = string.Empty;

    /// <summary>
    /// Возвращает или инициализирует описание.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Возвращает или инициализирует содержимое страницы.
    /// </summary>
    public string HtmlContent { get; set; } = string.Empty;
}
