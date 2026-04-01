using System;

namespace Simargl.Office.Excel;

/// <summary>
/// Представляет коллекцию рабочих книг.
/// </summary>
public sealed class BookCollection :
    OfficeObjectCollection<Application, Book>
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
    internal BookCollection(Application application, object obj) :
        base(application, obj, (app, comObject) => new Book(app, comObject))
    {

    }

    /// <summary>
    /// Добавляет новый элемент.
    /// </summary>
    /// <returns>
    /// Новый элемент.
    /// </returns>
    public Book Add()
    {
        //  Создание нового документа.
        Book book = new(Application, Invoke(comObject => comObject.Add()));

        //  Добавление в коллекцию вложенных оболочек.
        NestedWrappers.Add(book);

        //  Возврат оболочки.
        return book;
    }
}
