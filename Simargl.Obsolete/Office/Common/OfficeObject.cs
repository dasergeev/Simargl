namespace Simargl.Office;

/// <summary>
/// Представляет объект Office.
/// </summary>
/// <typeparam name="TApplication">
/// Тип приложения Office.
/// </typeparam>
public abstract class OfficeObject<TApplication> :
    ComWrapper
    where TApplication : OfficeApplication<TApplication>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="application">
    /// Приложение.
    /// </param>
    /// <param name="obj">
    /// COM-объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="application"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="obj"/> передана пустая ссылка.
    /// </exception>
    internal OfficeObject(TApplication application, object obj) :
        base(obj)
    {
        //  Установка приложения.
        Application = IsNotNull(application, nameof(application));
    }

    /// <summary>
    /// Возвращает приложение.
    /// </summary>
    public TApplication Application { get; }
}
