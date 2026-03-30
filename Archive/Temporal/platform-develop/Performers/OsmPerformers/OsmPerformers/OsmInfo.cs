using System.Collections.Concurrent;

namespace Apeiron.Platform.Performers.OsmPerformers;

/// <summary>
/// Класс представляет инофрмацию полученную из файла Osm.
/// </summary>
internal sealed class OsmInfo
{
    /// <summary>
    /// Содержит коллекцию узлов Node.
    /// </summary>
    internal BlockingCollection<OsmNode>? OsmNodeCollection { get; init; }

    /// <summary>
    /// Содержит коллекцию меток Tag.
    /// </summary>
    internal BlockingCollection<OsmNodeTag>? OsmTagCollection { get; init; }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public OsmInfo()
    {
        OsmNodeCollection = new BlockingCollection<OsmNode>();
        OsmTagCollection = new BlockingCollection<OsmNodeTag>();
    }
}
