using Simargl.AccelEth3T.AccelEth3TViewer.Nodes;

namespace Simargl.AccelEth3T.AccelEth3TViewer.UI;

/// <summary>
/// Представляет локальный элемент управления.
/// </summary>
public class LocalControl :
    Simargl.UI.Control
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public LocalControl()
    {
        //  Проверка режима разработки.
        if (IsInDesignMode)
        {
            //  Установка заглушек.
            Root = null!;

            //  Завершение инициализации.
            return;
        }

        ////  Получение приложения.
        //if (Application.Current is not App application)
        //    throw new InvalidOperationException("Не удалось получить приложение.");

        //  Установка корневого узла.
        Root = Program.Root;
    }

    /// <summary>
    /// Возвращает корневой узел.
    /// </summary>
    protected RootNode Root { get; }
}
