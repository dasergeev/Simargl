using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет исходный файл данных о питании.
/// </summary>
public sealed class RawPower :
    RawFile
{
    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<RawPower> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);

        //  Настрока каталога исходных данных.
        typeBuilder.HasOne(rawGeolocation => rawGeolocation.RawDirectory)
            .WithMany(rawGeolocation => rawGeolocation.RawPowers)
            .HasForeignKey(rawGeolocation => rawGeolocation.RawDirectoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RawPowers_RawDirectories");

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(entity => entity.Path);
    }
}
