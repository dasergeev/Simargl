using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет данные канала RawFrame.
/// </summary>
public class RawFrameChannelData
{
    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public long Timestamp { get; set; }

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
        EntityTypeBuilder<RawFrameChannelData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => new
        {
            x.Timestamp,
            x.Index,
            x.Frequency,
        });
    }
}
