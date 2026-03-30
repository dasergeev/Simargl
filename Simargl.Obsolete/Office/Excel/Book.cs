namespace Simargl.Office.Excel;

/// <summary>
/// Представляет рабочую книгу.
/// </summary>
public sealed class Book :
    OfficeObject<Application>
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
    internal Book(Application application, object obj) :
        base(application, obj)
    {
        //  Создание коллекции рабочих листов.
        Sheets = new(Application, Invoke(comObject => comObject.Worksheets));

        //  Добавление в коллекцию вложенных оболочек.
        NestedWrappers.AddRange(new ComWrapper[]
        {
            Sheets
        });
    }

    /// <summary>
    /// Возвращает коллекцию рабочих листов.
    /// </summary>
    public SheetCollection Sheets { get; }

    /// <summary>
    /// Сохраняет книгу в файл.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    public void Save(string path)
    {
        //  Проверка ссылки на путь.
        IsNotNull(path, nameof(path));

        //  Сохранение.
        Invoke(comObject => comObject.SaveAs(path));
    }
}
