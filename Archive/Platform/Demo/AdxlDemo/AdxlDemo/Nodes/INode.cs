using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.Nodes;

/// <summary>
/// Представляет узел проекта.
/// </summary>
public interface INode :
    INotifyPropertyChanged
{
    /// <summary>
    /// Возвращает или задаёт имя узла.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    INodeCollection Nodes { get; }

    /// <summary>
    /// Возвращает формат узла.
    /// </summary>
    NodeFormat NodeFormat { get; }
}
