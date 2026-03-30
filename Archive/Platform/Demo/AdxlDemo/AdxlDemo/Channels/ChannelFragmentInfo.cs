using Apeiron.Platform.Demo.AdxlDemo.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет информацию о фрагменте канала.
/// </summary>
public sealed class ChannelFragmentInfo :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор канала.
    /// </summary>
    public long ChannelInfoID { get; set; }

    /// <summary>
    /// Возвращает или задаёт информацию о канале.
    /// </summary>
    public ChannelInfo ChannelInfo { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт путь к файлу.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public double Sampling { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала фрагмента.
    /// </summary>
    public DateTime BeginTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт время окончания фрагмента.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт минимальное значение.
    /// </summary>
    public double MinValue { get; set; }

    /// <summary>
    /// Возвращает или задаёт максимальное значение.
    /// </summary>
    public double MaxValue { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднее значение.
    /// </summary>
    public double Average { get; set; }

    /// <summary>
    /// Возвращает или задаёт СКО.
    /// </summary>
    public double Deviation { get; set; }

    /// <summary>
    /// Возвращает или задаёт количество значений.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму значений.
    /// </summary>
    public double Sum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов значений.
    /// </summary>
    public double SumSquares { get; set; }

    /// <summary>
    /// Формирует относительный путь к файлу с данными.
    /// </summary>
    /// <param name="channelInfoID">
    /// Идентификатор информации о канале.
    /// </param>
    /// <param name="beginTime">
    /// Время начала записи фрагмента.
    /// </param>
    /// <returns>
    /// Относительный путь к каталогу с данными.
    /// </returns>
    public static string CreatePath(long channelInfoID, DateTime beginTime)
    {
        //  Формирование и возврат относительного пути к файлу с данными.
        return System.IO.Path.Combine(
            $"ID-{channelInfoID:0000}",
            $"{beginTime:yyyy-MM-dd}",
            $"{beginTime:HH}",
            $"{beginTime:mm-ss-ffff}.data");
    }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<ChannelFragmentInfo> typeBuilder)
    {
        //  Настройка базового класса.
        Entity.BuildAction(typeBuilder);

        //  Настройка уникальных индексов.
        typeBuilder.HasIndex(fragmentInfo => fragmentInfo.BeginTime);
        typeBuilder.HasIndex(fragmentInfo => fragmentInfo.EndTime);

        //  Настрока информации о канале.
        typeBuilder.HasOne(fragmentInfo => fragmentInfo.ChannelInfo)
            .WithMany(channelInfo => channelInfo.ChannelFragmentInfos)
            .HasForeignKey(fragmentInfo => fragmentInfo.ChannelInfoID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ChannelFragmentInfos_ChannelInfos");
    }
}
