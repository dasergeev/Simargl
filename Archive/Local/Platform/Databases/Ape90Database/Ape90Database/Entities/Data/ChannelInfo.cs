using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет информацию о канале кадра регистрации.
/// </summary>
public sealed class ChannelInfo :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт имя канала.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт размерность канала.
    /// </summary>
    public string Unit { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public double Sampling { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff { get; set; }

    /// <summary>
    /// Возвращает или задаёт описание канала.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию фрагментов данных.
    /// </summary>
    public HashSet<Fragment> Fragments { get; set; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<ChannelInfo> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(entity => entity.Name);
    }
}
