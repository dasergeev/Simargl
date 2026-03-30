using Simargl.Designing;

namespace Simargl.Mathematics.Projects.Border;

/// <summary>
/// Структура, содержащая результат оценки временного ряда на наличие полезного сигнала.
/// </summary>
public readonly struct TrendNoiseEstimationOutputData
{
    /// <summary>
    /// Поле для хранения оценки уровня нуля.
    /// </summary>
    private readonly TrendNoiseEstimationOutputDataCharacter _IsNoise;

    /// <summary>
    /// Возвращает информацию о характере сигнала.
    /// </summary>
    public TrendNoiseEstimationOutputDataCharacter IsNoise
    {
        get
        {
            // ...



            return _IsNoise;
        }
        init    // Раскрывается в init(TrendNoiseEstimationOutputDataCharacter value)
        {
            

            _ = value;  //  value - это ключевое слово, обозначет переменную, в которой хранится новое значение

            //  ...



            _IsNoise = IsDefined(value, nameof(IsNoise));
        }
    }

    /// <summary>
    /// Поле для хранения оценки уровня нуля.
    /// </summary>
    public double NullLevel { get; init; }

    /// <summary>
    /// Поле для хранения индексов моментов наезда колеса на сечение.
    /// </summary>
    public int[] IndEnter { get; init; }

    /// <summary>
    /// Поле для хранения индексов моментов съезда колеса с сечения.
    /// </summary>
    public int[] IndExit { get; init; }

    /// <summary>
    /// Поле для хранения числа временных интервалов полных проездов по сечению.
    /// </summary>
    public int NumberIntervals { get; init; }

    /// <summary>
    /// Поле для хранения индексов моментов наезда/съезда колеса на сечение для полных интервалов проездов по сечению.
    /// </summary>
    public int[,] IndIntervals { get; init; }

    /// <summary>
    /// Поле для хранения значений минимумов процесса для всех полных интервалов проездов по сечению.
    /// </summary>
    public double[] Minimums { get; init; }

    /// <summary>
    /// Поле для хранения значений максимумов процесса для всех полных интервалов проездов по сечению.
    /// </summary>
    public double[] Maximums { get; init; }

    /// <summary>
    /// Поле для хранения средних значений процесса для всех полных интервалов проездов по сечению.
    /// </summary>
    public double[] Averages { get; init; }

    /// <summary>
    /// Поле для хранения значений СКО процесса для всех полных интервалов проездов по сечению.
    /// </summary>
    public double[] Deviations { get; init; }

    /// <summary>
    /// Конструктор: инициализирует новый экземпляр структуры.
    /// </summary>
    /// <param name="isNoise">
    /// Информация о характере сигнала: 2 - шум; 1 - возможно, это шум; 0 - есть полезный сигнал.
    /// </param>
    /// <param name="nullLevel">
    /// Оценка уровня нуля.
    /// </param>
    /// <param name="indEnter">
    /// Индексы моментов наезда колеса на сечение.
    /// </param>
    /// <param name="indExit">
    /// Индексы моментов съезда колеса с сечения.
    /// </param>
    /// <param name="numberIntervals">
    /// Количество временных интервалов полных проездов по сечению.
    /// </param>
    /// <param name="indIntervals">
    /// Индексы моментов наезда/съезда колеса на сечение для полных интервалов проездов по сечению.
    /// </param>
    /// <param name="minimums">
    /// Значения минимумов процесса для всех полных интервалов проездов по сечению.
    /// </param>
    /// <param name="maximums">
    /// Значения максимумов процесса для всех полных интервалов проездов по сечению.
    /// </param>
    /// <param name="averages">
    /// Средние значения процесса для всех полных интервалов проездов по сечению.
    /// </param>
    /// <param name="deviations">
    /// Значения СКО процесса для всех полных интервалов проездов по сечению.
    /// </param>
    public TrendNoiseEstimationOutputData(TrendNoiseEstimationOutputDataCharacter isNoise, double nullLevel, int[] indEnter, int[] indExit, int numberIntervals, int[,] indIntervals, double[] minimums, double[] maximums, double[] averages, double[] deviations)
    {
        _IsNoise = IsDefined(isNoise, nameof(isNoise));
        NullLevel = nullLevel;
        IndEnter = indEnter;
        IndExit = indExit;
        NumberIntervals = numberIntervals;
        IndIntervals = indIntervals;
        Minimums = minimums;
        Maximums = maximums;
        Averages = averages;
        Deviations = deviations;
    }
}
