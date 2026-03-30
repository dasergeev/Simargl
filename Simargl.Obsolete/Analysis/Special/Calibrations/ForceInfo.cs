namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет информацию о силе.
/// </summary>
public sealed class ForceInfo
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="shortName">
    /// Короткое имя.
    /// </param>
    /// <param name="fullName">
    /// Полное имя.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="shortName"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="fullName"/> передана пустая ссылка.
    /// </exception>
    public ForceInfo(string shortName, string fullName)
    {
        //  Установка короткого имени.
        ShortName = IsNotNull(shortName, nameof(shortName));

        //  Установка полного имени.
        FullName = IsNotNull(fullName, nameof(fullName));
    }

    /// <summary>
    /// Возвращает короткое имя силы.
    /// </summary>
    public string ShortName { get; }

    /// <summary>
    /// Возвращает полное имя силы.
    /// </summary>
    public string FullName { get; }
}
