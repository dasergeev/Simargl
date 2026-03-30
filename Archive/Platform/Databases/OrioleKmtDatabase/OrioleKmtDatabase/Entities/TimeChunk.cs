using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OrioleKmtDatabase.Entities;

/// <summary>
/// Представляет информацию о части времени работы с определёнными именами каналов.
/// </summary>
public  class TimeChunk : Entity
{
    /// <summary>
    /// Время начала работы.
    /// </summary>
    public DateTime BeginTime { get; set; }

    /// <summary>
    /// Время завершения работы.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Возвращает коллекцию информации об именах каналов.
    /// </summary>
    public ICollection<ChannelName> ChannelNames { get; } = new HashSet<ChannelName>();


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<TimeChunk> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        typeBuilder.Property(entity => entity.BeginTime);
        typeBuilder.HasIndex(entity => entity.BeginTime);

        typeBuilder.Property(entity => entity.EndTime);
        typeBuilder.HasIndex(entity => entity.EndTime);
    }
}
