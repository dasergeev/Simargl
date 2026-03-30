using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет данные канала Adxl.
/// </summary>
public class AdxlChannelData
{
    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт ключ датчика.
    /// </summary>
    public int AdxlAddress { get; set; }

    /// <summary>
    /// Возвращает или задаёт датчик.
    /// </summary>
    public AdxlData Adxl { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт индекс канала.
    /// </summary>
    public byte Index { get; set; }

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
        EntityTypeBuilder<AdxlChannelData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => new
        {
            x.Timestamp,
            x.AdxlAddress,
            x.Index,
            x.Frequency,
        });

        //  Настройка датичка.
        typeBuilder.HasOne(x => x.Adxl)
            .WithMany(x => x.AdxlChannels)
            .HasForeignKey(x => x.AdxlAddress)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Adxls_AdxlChannels");
    }
}
