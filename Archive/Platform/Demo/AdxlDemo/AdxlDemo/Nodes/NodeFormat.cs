namespace Apeiron.Platform.Demo.AdxlDemo.Nodes;

/// <summary>
/// Возвращает значение, определяющее формат узла.
/// </summary>
public enum NodeFormat
{
    /// <summary>
    /// Корневой узел приложения.
    /// </summary>
    Root,

    /// <summary>
    /// Узел, представляющий сеть.
    /// </summary>
    Network,

    /// <summary>
    /// Узел, представляющий коллекцию сетей.
    /// </summary>
    NetworkCollection,

    /// <summary>
    /// Узел, представляющий датчик.
    /// </summary>
    AdxlDevice,

    /// <summary>
    /// Узел, представляющий коллекцию датчиков.
    /// </summary>
    AdxlDeviceCollection,

    /// <summary>
    /// Узел, представляющий организатор канала.
    /// </summary>
    ChannelOrganizer,

    /// <summary>
    /// Узел, представляющий коллекцию организаторов каналов.
    /// </summary>
    ChannelOrganizerCollection,
}
