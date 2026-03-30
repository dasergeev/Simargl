using System.Runtime.CompilerServices;

namespace ApeironApeiron.Platform.Databases.Ra2Database.Entities;

/// <summary>
/// Представляет информацию о результатах обработки.
/// </summary>
public sealed class ChannelResult :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор файла.
    /// </summary>
    public long RawFileId { get; set; }

    /// <summary>
    /// Возвращает или задаёт имя канала.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public double Sampling { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff { get; set; }

    /// <summary>
    /// Возвращает или задаёт смещение значений канала.
    /// </summary>
    public double Offset { get; set; }

    /// <summary>
    /// Возвращает или задаёт масштаб значений канала.
    /// </summary>
    public double Scale { get; set; }

    /// <summary>
    /// Возвращает или задаёт длину канала.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Возвращает или задаёт Min.
    /// </summary>
    public double Min { get; set; }

    /// <summary>
    /// Возвращает или задаёт Max.
    /// </summary>
    public double Max { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму.
    /// </summary>
    public double Sum { get; set; }

    /// <summary>
    /// Возвращает или задаёт SquaresSum.
    /// </summary>
    public double SquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт информацию о файле.
    /// </summary>
    public RawFile RawFile { get; set; } = null!;

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<ChannelResult> typeBuilder)
    {
        ////  Настройка первичного ключа.
        //typeBuilder.HasKey(entity => entity.Id);
        //typeBuilder.Property(entity => entity.Id)
        //    .ValueGeneratedOnAdd();
        //typeBuilder.HasIndex(entity => entity.Id);

        // Настройка связи один ко многим.
        typeBuilder.HasOne(channelResult => channelResult.RawFile)
            .WithMany(file => file.ChannelResults)
            .HasForeignKey(channelResult => channelResult.RawFileId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ChannelResults_RawFiles_RawFileId");
    }
}
