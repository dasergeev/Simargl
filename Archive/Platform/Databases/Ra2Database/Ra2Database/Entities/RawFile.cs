using System.Runtime.CompilerServices;

namespace ApeironApeiron.Platform.Databases.Ra2Database.Entities;

/// <summary>
/// Представляет информацию о файлах
/// </summary>
public sealed class RawFile :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт имя компьютера.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт флаг обработан или не обработан файл.
    /// </summary>
    public bool IsAnalyzed { get; set; } = false;

    /// <summary>
    /// Возвращает или задаёт флаг пустого кадра
    /// </summary>
    public bool IsEmpty { get; set; }

    /// <summary>
    /// Возвращает или задаёт время записи кадра.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднюю скорость.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт широту.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Возвращает коллекцию информации о каналах.
    /// </summary>
    public EntityCollection<ChannelResult> ChannelResults { get; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<RawFile> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id)
            .ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        //  Настройка индексов и ключей
        typeBuilder.Property(service => service.FilePath);
        typeBuilder.HasAlternateKey(service => service.FilePath);
        typeBuilder.HasIndex(service => service.FilePath);

        typeBuilder.Property(service => service.IsAnalyzed);
        typeBuilder.HasIndex(service => service.IsAnalyzed);
    }
}
