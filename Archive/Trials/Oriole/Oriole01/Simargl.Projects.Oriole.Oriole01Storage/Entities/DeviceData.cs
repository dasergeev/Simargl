using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет данные устройства.
/// </summary>
public sealed class DeviceData
{
    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<DeviceData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);

        //  Настройка индекса серийного номера.
        typeBuilder.HasIndex(x => x.Serial).IsUnique();

        //  Настройка индекса IP-адреса.
        typeBuilder.HasIndex(x => x.IPAddress).IsUnique();
    }

    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Возвращает или задаёт серийный номер датчика.
    /// </summary>
    public string Serial { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт IP-адрес датчика.
    /// </summary>
    public string IPAddress { get; set; } = string.Empty;
}
