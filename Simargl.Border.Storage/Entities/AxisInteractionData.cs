namespace Simargl.Border.Storage.Entities;

/// <summary>
/// Представляет данные взаимодействия оси.
/// </summary>
public sealed class AxisInteractionData :
    EntityData
{
    /// <summary>
    /// Возвращает или задаёт сечение.
    /// </summary>
    public int Section { get; set; }

    /// <summary>
    /// Возвращает или задаёт положение сечения.
    /// </summary>
    public double Position { get; set; }

    /// <summary>
    /// Возвращает или задаёт время нажима.
    /// </summary>
    public double Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Возвращает или задаёт длину данных.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму скоростей.
    /// </summary>
    public double SpeedSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов скоростей.
    /// </summary>
    public double SpeedSquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднюю скорость.
    /// </summary>
    public double SpeedAverage { get; set; }

    /// <summary>
    /// Возвращает или задаёт СКО скорости.
    /// </summary>
    public double SpeedDeviation { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумуу левых значений.
    /// </summary>
    public double LeftSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов левых значений.
    /// </summary>
    public double LeftSquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднее левое значение.
    /// </summary>
    public double LeftAverage { get; set; }

    /// <summary>
    /// Возвращает или задаёт СКО левых значений.
    /// </summary>
    public double LeftDeviation { get; set; }

    /// <summary>
    /// Возвращает или задаёт максимальное левое значение.
    /// </summary>
    public double LeftMax { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму правых значений.
    /// </summary>
    public double RightSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов правых значений.
    /// </summary>
    public double RightSquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт максимальное правое значение.
    /// </summary>
    public double RightMax { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднее правое значение.
    /// </summary>
    public double RightAverage { get; set; }

    /// <summary>
    /// Возвращает или задаёт СКО правых значений.
    /// </summary>
    public double RightDeviation { get; set; }

    /// <summary>
    /// Возвращает или задаёт ключ оси.
    /// </summary>
    public long AxisKey { get; set; }

    /// <summary>
    /// Возвращает или задаёт ось.
    /// </summary>
    public AxisData Axis { get; set; } = null!;

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<AxisInteractionData> typeBuilder)
    {
        //  Настройка базовой сущности.
        EntityData.BuildAction(typeBuilder);

        //  Настройка часового каталога.
        typeBuilder.HasOne(x => x.Axis)
            .WithMany(x => x.Interactions)
            .HasForeignKey(x => x.AxisKey)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Axes_Interactions");
    }
}
