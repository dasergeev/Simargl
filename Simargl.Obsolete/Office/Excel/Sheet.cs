using System;
using System.Runtime.InteropServices;

namespace Simargl.Office.Excel;

/// <summary>
/// Представляет рабочий лист.
/// </summary>
public sealed class Sheet :
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
    internal Sheet(Application application, object obj) :
        base(application, obj)
    {

    }

    /// <summary>
    /// Возвращает или задаёт имя.
    /// </summary>
    public string Name
    {
        get => Invoke(comObject => comObject.Name);
        set => Invoke(comObject => comObject.Name = value);
    }

    /// <summary>
    /// Возвращает или задаёт значение ячейки.
    /// </summary>
    /// <param name="row">
    /// Индекс строки.
    /// </param>
    /// <param name="column">
    /// Индекс столбца.
    /// </param>
    /// <returns>
    /// Значение ячейки.
    /// </returns>
    public dynamic this[int row, int column]
    {
        get
        {
            //  Получение ячеек.
            dynamic cells = Invoke(comObject => comObject.Cells);

            try
            {
                //  Возврат значения.
                return cells[row + 1, column + 1];
            }
            finally
            {
                //  Проверка платформы.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    //  Проверка необходимости освобождения COM-объекта.
                    if (Marshal.IsComObject(cells))
                    {
                        //  Освобождение объекта.
                        Marshal.ReleaseComObject(cells);
                    }
                }
            }
        }
        set
        {
            //  Получение ячеек.
            dynamic cells = Invoke(comObject => comObject.Cells);

            try
            {
                //  Установка значения.
                cells[row + 1, column + 1] = value;
            }
            finally
            {
                //  Проверка платформы.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    //  Проверка необходимости освобождения COM-объекта.
                    if (Marshal.IsComObject(cells))
                    {
                        //  Освобождение объекта.
                        Marshal.ReleaseComObject(cells);
                    }
                }
            }
        }
    }
}
