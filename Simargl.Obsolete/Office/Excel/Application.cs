using System;

namespace Simargl.Office.Excel;

/// <summary>
/// Представляет приложение Excel.
/// </summary>
public sealed class Application :
    OfficeApplication<Application>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Application() :
        base(@"Excel.Application")
    {
        //  Создание коллекции рабочих книг.
        Books = new(this, Invoke(comObject => comObject.Workbooks));

        //  Добавление в коллекцию вложенных оболочек.
        NestedWrappers.AddRange(new ComWrapper[]
        {
            Books
        });
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее виден ли объект.
    /// </summary>
    public bool Visible
    {
        get => Invoke(comObject => comObject.Visible);
        set => Invoke(comObject => comObject.Visible = value);
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее отображаются ли определенные оповещения и сообщения во время выполнения макроса.
    /// </summary>
    public bool DisplayAlerts
    {
        get => Invoke(comObject => comObject.DisplayAlerts);
        set => Invoke(comObject => comObject.DisplayAlerts = value);
    }

    /// <summary>
    /// Возвращает коллекцию рабочих книг.
    /// </summary>
    public BookCollection Books { get; }

    /// <summary>
    /// Закрывает приложение.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="ContextMarshalException">
    /// Член не найден.
    /// </exception>
    public void Quit() => Invoke((comObject) => comObject.Quit());
}
