using System.Windows.Controls.Ribbon;

namespace Simargl.UI.Nodes;

/// <summary>
/// Представляет элемент управления, отображающий вкладку узла на ленте.
/// </summary>
public class NodeTab :
    RibbonTab
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public NodeTab()
    {
        //  Настройка заголовка.
        Header = "Узел";
    }

    /// <summary>
    /// Устанавливает активный узел.
    /// </summary>
    /// <param name="node">
    /// Активный узел.
    /// </param>
    public virtual void SetActiveNode(Node? node)
    {

    }
}
