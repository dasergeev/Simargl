namespace Simargl.Border.Processing.Core;

/// <summary>
/// Представляет карту нажимов.
/// </summary>
/// <param name="processor">
/// Устройство обработки.
/// </param>
public sealed class PressureMap(Processor processor) :
    ProcessorUnit(processor)
{
    /// <summary>
    /// Поле для хранения карты коллекций нажимов.
    /// </summary>
    private readonly PressureCollection[] _Map = [.. Enumerable
        .Range(0, BasisConstants.Preprocessor.SectionCount).Select(x => new PressureCollection())];

    /// <summary>
    /// Возвращает количество осей.
    /// </summary>
    public int AxesCount { get; private set; }

    /// <summary>
    /// Возвращает количество фиксаций осей.
    /// </summary>
    public int AxesCommits { get; private set; }

    /// <summary>
    /// Возвращает значение, определяющее является ли результат надёжным.
    /// </summary>
    public bool IsReliable { get; private set; }

    /// <summary>
    /// Возвращает коллекцию нажимов.
    /// </summary>
    /// <param name="section">
    /// Сечение.
    /// </param>
    /// <returns>
    /// Коллекция нажимов.
    /// </returns>
    public PressureCollection GetPressures(int section)
    {
        return _Map[section - 1];
    }

    /// <summary>
    /// Асинхронно выполняет анализ.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая анализ.
    /// </returns>
    public async Task AnalysisAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Блок перехвата всех исключений.
        try
        {
            //  Определение количества осей.
            var result = _Map.Select(x => x.Count)
                .Where(x => x > 0)
                .GroupBy(x => x)
                .Select(x => new
                {
                    Axes = x.Key,
                    Commits = x.Count(),
                })
                .OrderByDescending(x => x.Commits)
                .First();

            //  Установка результата.
            AxesCount = result.Axes;
            AxesCommits = result.Commits;

            //  Установка значения, определяющего является ли результат надёжным.
            IsReliable = AxesCommits >= preprocessor.MinCommitsCount;
        }
        catch { }
    }
}
