using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет фильтр.
/// </summary>
public class Filter :
    Entity
{
    /// <summary>
    /// Поле для хранения частоты среза фильтра.
    /// </summary>
    private double _Cutoff;

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff
    {
        get => _Cutoff;
        set => SetProperty(nameof(Cutoff), ref _Cutoff, value);
    }

    /// <summary>
    /// Возвращает коллекцию статистических данных.
    /// </summary>
    public ObservableCollection<Statistic> Statistics { get; } = new();

    /// <summary>
    /// Возвращает коллекцию экстремальных данных.
    /// </summary>
    public ObservableCollection<Extremum> Extremums { get; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <typeparam name="TFilter">
    /// Тип сущности.
    /// </typeparam>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static new void BuildAction<TFilter>(EntityTypeBuilder<TFilter> typeBuilder)
        where TFilter : Filter
    {
        //  Базовая настройка.
        Entity.BuildAction(typeBuilder);

        //  Настройка частоты среза фильтра.
        typeBuilder.Property(filter => filter.Cutoff);
        typeBuilder.HasIndex(filter => filter.Cutoff);

        //  Настройка альтернативного ключа.
        typeBuilder.HasAlternateKey(filter => filter.Cutoff);
    }
}
