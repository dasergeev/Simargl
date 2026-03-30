using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет данные имени канала.
/// </summary>
public sealed class ChannelNameData
{
    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<ChannelNameData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);

        //  Настройка индекса имени.
        typeBuilder.HasIndex(x => x.Name).IsUnique();

        //  Настройка альтернативного ключа.
        typeBuilder.HasAlternateKey(x => new
        {
            x.Serial,
            x.Axis,
        });
    }

    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Возвращает или задаёт имя канала.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт серийный номер датчика.
    /// </summary>
    public string Serial { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт номер оси датчика.
    /// </summary>
    public int Axis { get; set; }

    /// <summary>
    /// Возвращает или задаёт направление оси.
    /// </summary>
    public double Direct { get; set; }
}
