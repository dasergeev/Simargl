using Apeiron.Gps;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет сообщение GPS, содержащее данные о наземном курсе и скорости.
/// </summary>
public class VtgMessage :
    NmeaMessage
{
    /// <summary>
    /// Возвращает или задаёт курс на истинный полюс в градусах.
    /// </summary>
    public double? PoleCourse { get; set; }

    /// <summary>
    /// Возвращает или задаёт курс на магнитный полюс в градусах.
    /// </summary>
    public double? MagneticCourse { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость в узлах.
    /// </summary>
    public double? Knots { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость в километрах в час.
    /// </summary>
    public double? Speed { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее режим системы позиционирования.
    /// </summary>
    public GpsMode? Mode { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<VtgMessage> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);

        //  Настрока каталога исходных данных.
        typeBuilder.HasOne(message => message.RawGeolocation)
            .WithMany(rawGeolocation => rawGeolocation.VtgMessages)
            .HasForeignKey(message => message.RawGeolocationId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_VtgMessages_RawGeolocations");

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(entity => new
        {
            entity.RawGeolocationId,
            entity.Index,
        });
    }
}
