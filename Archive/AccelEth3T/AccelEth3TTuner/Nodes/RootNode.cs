namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет корневой узел.
/// </summary>
public sealed class RootNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public RootNode() :
        base("Root")
    {
        //  Добавление узлов в коллекцию.
        Nodes.Add(NetNode);
    }

    /// <summary>
    /// Возвращает сетевой узел.
    /// </summary>
    public NetNode NetNode { get; } = new();
}
