namespace Simargl.UI.Nodes;

/// <summary>
/// Представляет элемент управления.
/// </summary>
public abstract class Control :
    UI.Control
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    internal Control()
    {
        //  Проверка режима разработки.
        if (IsInDesignMode)
        {
            //  Установка заглушек.
            Root = null!;

            //  Завершение инициализации.
            return;
        }

        //  Получение приложения.
        if (System.Windows.Application.Current is not Application application)
            throw new InvalidOperationException("Не удалось получить приложение.");

        //  Установка корневого узла.
        Root = application.Root;
    }

    /// <summary>
    /// Возвращает корневой узел.
    /// </summary>
    protected RootNode Root { get; }
}
