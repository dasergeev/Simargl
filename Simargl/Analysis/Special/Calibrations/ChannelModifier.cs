namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет модификатор канала.
/// </summary>
public abstract class ChannelModifier
{

}

/// <summary>
/// Представляет модификатор канала.
/// </summary>
public sealed class SingleChannelModifier :
    ChannelModifier
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sourceName">
    /// Имя исходного канала.
    /// </param>
    /// <param name="targetName">
    /// Имя целевого канала.
    /// </param>
    /// <param name="transformation">
    /// Преобразование канала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sourceName"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="targetName"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="transformation"/> передана пустая ссылка.
    /// </exception>
    public SingleChannelModifier(string sourceName, string targetName, Func<double, double> transformation)
    {
        //  Установка имени исходного канала.
        SourceName = IsNotNull(sourceName, nameof(sourceName));

        //  Установка имени целевого канала.
        TargetName = IsNotNull(targetName, nameof(targetName));

        //  Установка преобразования.
        Transformation = IsNotNull(transformation, nameof(transformation));
    }

    /// <summary>
    /// Возвращает имя исходного канала.
    /// </summary>
    public string SourceName { get; }

    /// <summary>
    /// Возвращает имя целевого канала.
    /// </summary>
    public string TargetName { get; }

    /// <summary>
    /// Возвращает преобразование канала.
    /// </summary>
    public Func<double, double> Transformation { get; }
}

/// <summary>
/// Представляет модификатор канала.
/// </summary>
public sealed class DoubleChannelModifier :
    ChannelModifier
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="firstSourceName">
    /// Имя первого исходного канала.
    /// </param>
    /// <param name="secondSourceName">
    /// Имя второго исходного канала.
    /// </param>
    /// <param name="targetName">
    /// Имя целевого канала.
    /// </param>
    /// <param name="transformation">
    /// Преобразование канала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="firstSourceName"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="targetName"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="transformation"/> передана пустая ссылка.
    /// </exception>
    public DoubleChannelModifier(string firstSourceName, string secondSourceName, string targetName, Func<double, double, double> transformation)
    {
        //  Установка имени первого исходного канала.
        FirstSourceName = IsNotNull(firstSourceName, nameof(firstSourceName));

        //  Установка имени второго исходного канала.
        SecondSourceName = IsNotNull(secondSourceName, nameof(secondSourceName));

        //  Установка имени целевого канала.
        TargetName = IsNotNull(targetName, nameof(targetName));

        //  Установка преобразования.
        Transformation = IsNotNull(transformation, nameof(transformation));
    }

    /// <summary>
    /// Возвращает имя первого исходного канала.
    /// </summary>
    public string FirstSourceName { get; }

    /// <summary>
    /// Возвращает имя второго исходного канала.
    /// </summary>
    public string SecondSourceName { get; }

    /// <summary>
    /// Возвращает имя целевого канала.
    /// </summary>
    public string TargetName { get; }

    /// <summary>
    /// Возвращает преобразование канала.
    /// </summary>
    public Func<double, double, double> Transformation { get; }
}
