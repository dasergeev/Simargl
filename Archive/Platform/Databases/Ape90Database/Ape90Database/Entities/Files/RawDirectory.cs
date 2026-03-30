using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет каталог исходных данных.
/// </summary>
public sealed class RawDirectory :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт путь к каталогу.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт длительность кадров в секундах.
    /// </summary>
    public double FrameDuration { get; set; }

    /// <summary>
    /// Возвращает время начала записей.
    /// </summary>
    public DateTime BeginTime { get; set; }

    /// <summary>
    /// Возвращает время окончания записей.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Возвращает коллекцию исходных кадров.
    /// </summary>
    public HashSet<RawFrame> RawFrames { get; set; } = new();

    /// <summary>
    /// Возвращает коллекцию исходных файлов геолокационных данных.
    /// </summary>
    public HashSet<RawGeolocation> RawGeolocations { get; set; } = new();

    /// <summary>
    /// Возвращает коллекцию исходных файлов данных о питании.
    /// </summary>
    public HashSet<RawPower> RawPowers { get; set; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<RawDirectory> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
    }
}
