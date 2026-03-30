using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OrioleKmtDatabase.Entities;

/// <summary>
/// Представляет информацию об именах каналов.
/// </summary>
public class ChannelName : Entity
{
    /// <summary>
    /// Представляет внешний ключ. ИД части времени.
    /// </summary>
    public long TimeChunkId { get; set; }

    /// <summary>
    /// Индекс канала.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Имя канала.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт информацию о части времени.
    /// </summary>
    public TimeChunk TimeChunk { get; set; } = null!;


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<ChannelName> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        // Настройка связи один ко многим.
        typeBuilder.HasOne(channelName => channelName.TimeChunk)
            .WithMany(timeChunk => timeChunk.ChannelNames)
            .HasForeignKey(channelName => channelName.TimeChunkId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ChannelNames_TimeChunk");

        // Настройка индекса канала
        typeBuilder.Property(entity => entity.Index);
        typeBuilder.HasIndex(entity => entity.Index);

        // Настройка имени канала.
        typeBuilder.Property(entity => entity.Name);
        typeBuilder.HasIndex(entity => entity.Name);
    }

}