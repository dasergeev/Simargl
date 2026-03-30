using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OrioleKmtDatabase.Entities;

/// <summary>
/// Представляет информацию об обработанном файле.
/// </summary>
public class AnalyzedFrame : Entity
{
    /// <summary>
    /// Возвращает или задаёт полный путь к файлам.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт широту.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт минимальную кривизну.
    /// </summary>
    public double CurvatureMin { get; set; }

    /// <summary>
    /// Возвращает или задаёт максимальную кривизну.
    /// </summary>
    public double CurvatureMax { get; set; }

    /// <summary>
    /// Возвращает или задаёт средниюю кривизну.
    /// </summary>
    public double CurvatureAverage { get; set; }

    /// <summary>
    /// Возвращает или задаёт длительность остановки.
    /// </summary>
    public double DurationParking { get; set; }

    /// <summary>
    /// Возвращает или задаёт длительность тяги.
    /// </summary>
    public double DurationTraction { get; set; }

    /// <summary>
    /// Возвращает или задаёт длительность торможения.
    /// </summary>
    public double DurationBraking { get; set; }

    /// <summary>
    /// Возвращает или задаёт длительность выбега.
    /// </summary>
    public double DurationRunout { get; set; }

    /// <summary>
    /// Возращает или задаёт минимальное тяговое усилие.
    /// </summary>
    public double TractionEffortMin { get; set; }

    /// <summary>
    /// Возращает или задаёт максимальное тяговое усилие.
    /// </summary>
    public double TractionEffortMax { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму тягового усилия.
    /// </summary>
    public double TractionEffortSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт квадрат суммы тягового усилия.
    /// </summary>
    public double TractionEffortSquaredSum { get; set; }

    /// <summary>
    /// Количество тяговых усилий.
    /// </summary>
    public int TractionEffortCount { get; set; }

    /// <summary>
    /// Устанавливает флаг обработан или нет кадр.
    /// </summary>
    public bool IsPocessed { get; set; }


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<AnalyzedFrame> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        //  Настройка индексов и ключей
        typeBuilder.Property(entity => entity.FilePath);
        typeBuilder.HasAlternateKey(entity => entity.FilePath);
        typeBuilder.HasIndex(entity => entity.FilePath);

        typeBuilder.Property(entity => entity.IsPocessed);
        typeBuilder.HasIndex(entity => entity.IsPocessed);
    }
}