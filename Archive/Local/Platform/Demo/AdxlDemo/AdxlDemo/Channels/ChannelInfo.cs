using Apeiron.Platform.Demo.AdxlDemo.Database;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет информацию о канале.
/// </summary>
public sealed class ChannelInfo :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт имя канала.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Возвращает или задаёт серийный номер датчика.
    /// </summary>
    public long SerialNumber { get; set; }

    /// <summary>
    /// Возвращает или задаёт тип канала.
    /// </summary>
    public ChannelType ChannelType { get; init; }

    /// <summary>
    /// Возвращает или задаёт номер сигнала.
    /// </summary>
    public int SignalNumber { get; init; }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public double Sampling { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff { get; set; }

    /// <summary>
    /// Возвращает или задаёт цвет.
    /// </summary>
    public long Color { get; set; }

    /// <summary>
    /// Возвращает коллекцию информации о фрагментах канала.
    /// </summary>
    public HashSet<ChannelFragmentInfo> ChannelFragmentInfos { get; set; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<ChannelInfo> typeBuilder)
    {
        //  Настройка базового класса.
        Entity.BuildAction(typeBuilder);

        //  Настройка уникальных индексов.
        typeBuilder.HasIndex(channelInfo => channelInfo.Name).IsUnique();
    }
}
