using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет данные канала.
/// </summary>
public class ChannelData
{
    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт ключ датчика.
    /// </summary>
    public long AdxlKey { get; set; }

    /// <summary>
    /// Возвращает или задаёт датчик.
    /// </summary>
    public AdxlData Adxl { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт индекс канала.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту фильтрации.
    /// </summary>
    public double Frequency { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму значений.
    /// </summary>
    public double Sum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов значений.
    /// </summary>
    public double SquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт минимальное значение.
    /// </summary>
    public double Min { get; set; }

    /// <summary>
    /// Возвращает или задаёт максимальное значение.
    /// </summary>
    public double Max { get; set; }

    /// <summary>
    /// Возвращает или задаёт количество значений.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<ChannelData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(x => new
        {
            x.Timestamp,
            x.AdxlKey,
            x.Index,
            x.Frequency,
        });

        //  Настройка датичка.
        typeBuilder.HasOne(x => x.Adxl)
            .WithMany(x => x.Channels)
            .HasForeignKey(x => x.AdxlKey)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Adxls_Channels");
    }
}
