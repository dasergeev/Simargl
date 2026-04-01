using System;

namespace Simargl.Office;

/// <summary>
/// Представляет приложение Office.
/// </summary>
/// <typeparam name="TApplication">
/// Тип приложения Office.
/// </typeparam>
public abstract class OfficeApplication<TApplication> :
    ComWrapper
    where TApplication : OfficeApplication<TApplication>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="progID">
    /// Идентификатор типа создаваемого объекта.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="progID"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ContextMarshalException">
    /// Объект не найден.
    /// </exception>
    protected OfficeApplication(string progID) :
        base(CreateObjectFromProgID(progID))
    {

    }
}
