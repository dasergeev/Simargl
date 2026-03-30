using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет фрагмент данных.
/// </summary>
public sealed class Fragment :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор канала кадра регистрации.
    /// </summary>
    public long ChannelInfoId { get; set; }

    /// <summary>
    /// Возвращает или задаёт информацию о канале кадра регистрации.
    /// </summary>
    public ChannelInfo ChannelInfo { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт идентификатор кадра регистрации.
    /// </summary>
    public long FrameInfoId { get; set; }

    /// <summary>
    /// Возвращает или задаёт информацию об исходном кадре регистрации.
    /// </summary>
    public FrameInfo FrameInfo { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт индекс фрагмента в кадре.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Возвращает время начала записи фрагмента.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее загружены ли данные фрагмента в длинный кадр регистрации.
    /// </summary>
    public bool IsLoadedIntoLongFrame { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее являются ли данные действительными.
    /// </summary>
    public bool IsDataValid { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее ноль, определённый по скорости движения.
    /// </summary>
    public double SpeedZero { get; set; }

    /// <summary>
    /// Возвращает или задаёт количество значений.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Возвращает или задаёт минимальное значение.
    /// </summary>
    public double Min { get; set; }

    /// <summary>
    /// Возвращает или задаёт максимальное значение.
    /// </summary>
    public double Max { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднее значение.
    /// </summary>
    public double Average { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднеквадратическое отклонение.
    /// </summary>
    public double Deviation { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму значений.
    /// </summary>
    public double Sum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов значений.
    /// </summary>
    public double SquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее являются ли GPS-данные действительными.
    /// </summary>
    public bool IsGpsValid { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость в километрах в час.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Возвращает или задаёт широту в градусах.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу в градусах.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт высоту над средним уровнем моря в метрах.
    /// </summary>
    public double Altitude { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<Fragment> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);

        //  Настрока канала.
        typeBuilder.HasOne(fragment => fragment.ChannelInfo)
            .WithMany(channelInfo => channelInfo.Fragments)
            .HasForeignKey(fragment => fragment.ChannelInfoId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Fragments_ChannelInfos");

        //  Настрока кадра.
        typeBuilder.HasOne(fragment => fragment.FrameInfo)
            .WithMany(frameInfo => frameInfo.Fragments)
            .HasForeignKey(fragment => fragment.FrameInfoId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Fragments_FrameInfos");

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(entity => new
        {
            entity.ChannelInfoId,
            entity.FrameInfoId,
            entity.Index,
        });

        //  Настройка индексов.
        typeBuilder.HasIndex(entity => entity.Time);
        typeBuilder.HasIndex(entity => entity.IsDataValid);
        typeBuilder.HasIndex(entity => entity.SpeedZero);
        typeBuilder.HasIndex(entity => entity.Min);
        typeBuilder.HasIndex(entity => entity.Max);
        typeBuilder.HasIndex(entity => entity.Average);
        typeBuilder.HasIndex(entity => entity.Deviation);
        typeBuilder.HasIndex(entity => entity.Sum);
        typeBuilder.HasIndex(entity => entity.IsGpsValid);
        typeBuilder.HasIndex(entity => entity.Speed);
        typeBuilder.HasIndex(entity => entity.Latitude);
        typeBuilder.HasIndex(entity => entity.Longitude);
        typeBuilder.HasIndex(entity => entity.Altitude);
        typeBuilder.HasIndex(entity => entity.IsLoadedIntoLongFrame);
    }
}
