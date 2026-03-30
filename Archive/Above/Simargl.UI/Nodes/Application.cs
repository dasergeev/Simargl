namespace Simargl.UI.Nodes;

/// <summary>
/// Представляет приложение узлов.
/// </summary>
public class Application :
    UI.Application
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Application()
    {
        //  Создание корневого узла.
        Root = new();
    }

    /// <summary>
    /// Возвращает корневой узел.
    /// </summary>
    public RootNode Root { get; }
}
