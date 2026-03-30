using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет исходный файл геолокационных данных.
/// </summary>
public sealed class RawGeolocation :
    RawFile
{
    /// <summary>
    /// Возвращает или задаёт значение, определяющее загружен ли файл.
    /// </summary>
    public bool IsLoaded { get; set; }

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащее данные местоположения.
    /// </summary>
    public HashSet<GgaMessage> GgaMessages { get; set; } = new();

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащее минимальный рекомендованный набор данных.
    /// </summary>
    public HashSet<RmcMessage> RmcMessages { get; set; } = new();

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащее данные о наземном курсе и скорости.
    /// </summary>
    public HashSet<VtgMessage> VtgMessages { get; set; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<RawGeolocation> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);

        //  Настрока каталога исходных данных.
        typeBuilder.HasOne(rawGeolocation => rawGeolocation.RawDirectory)
            .WithMany(rawGeolocation => rawGeolocation.RawGeolocations)
            .HasForeignKey(rawGeolocation => rawGeolocation.RawDirectoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RawGeolocations_RawDirectories");

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(entity => entity.Path);
    }
}
