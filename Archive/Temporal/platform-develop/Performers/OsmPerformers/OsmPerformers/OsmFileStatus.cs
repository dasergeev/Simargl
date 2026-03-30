namespace Apeiron.Platform.Performers.OsmPerformers;

/// <summary>
/// Содержит статусы обработки и загрузки файлов Osm.
/// </summary>
public enum OsmFileStatus : byte
{
    /// <summary>
    /// Точки Node отсутствуют в файле.
    /// </summary>
    IsNodesEmpty,

    /// <summary>
    /// Точки Node загружены из файла в БД.
    /// </summary>
    IsNodesLoad,

    /// <summary>
    /// Tag описания точек Node загружены в БД.
    /// </summary>
    IsNodeTagsLoad,

    /// <summary>
    /// Линии Ways отсутствуют в файле.
    /// </summary>
    IsWaysEmpty,

    /// <summary>
    /// Линии Ways загружены из файла в БД.
    /// </summary>
    IsWaysLoad,

    /// <summary>
    /// Tag описания линий Way загружены в БД.
    /// </summary>
    IsWayTagsLoad
}
