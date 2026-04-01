using System;

namespace Simargl.Office.Excel;

/// <summary>
/// Представляет коллекцию рабочих листов.
/// </summary>
public sealed class SheetCollection :
    OfficeObjectCollection<Application, Sheet>
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
    internal SheetCollection(Application application, object obj) :
        base(application, obj, (app, comObject) => new Sheet(app, comObject))
    {

    }

    /// <summary>
    /// Добавляет новый элемент.
    /// </summary>
    /// <returns>
    /// Новый элемент.
    /// </returns>
    public Sheet Add()
    {
        //  Создание нового документа.
        Sheet sheet = new(Application, Invoke(comObject => comObject.Add()));

        //  Добавление в коллекцию вложенных оболочек.
        NestedWrappers.Add(sheet);

        //  Возврат оболочки.
        return sheet;
    }
}
