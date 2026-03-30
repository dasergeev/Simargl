using Simargl.Journaling;
using System.ComponentModel;

namespace Simargl.Engine;

/// <summary>
/// Представляет базовый элемент приложения.
/// </summary>
public abstract class Something
{
    /// <summary>
    /// Поле для хранения входа в приложение.
    /// </summary>
    internal static volatile Entry _Entry = null!;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка создания элемента приложения до инициализации входа в приложение.
    /// </exception>
    public Something()
    {
        //  Получение входа в приложение.
        Entry = Entry.Unique ?? throw new InvalidOperationException(
            "Произошла попытка создания элемента приложения до инициализации входа в приложение.");
    }

    /// <summary>
    /// Возвращает вход в приложение.
    /// </summary>
    [Browsable(false)]
    protected Entry Entry { get; }

    /// <summary>
    /// Возвращает журнал.
    /// </summary>
    protected Journal Journal => Entry.Journal;
}
