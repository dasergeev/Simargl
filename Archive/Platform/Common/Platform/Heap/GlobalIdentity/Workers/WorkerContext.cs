using Apeiron.Services.GlobalIdentity.Tunings;
using Microsoft.Extensions.Logging;

namespace Apeiron.Services.GlobalIdentity.Workers;

/// <summary>
/// Представляет контекст фонового процесса службы глобальной идентификации.
/// </summary>
/// <typeparam name="TWorker">
/// Тип фонового процесса службы глобальной идентификации.
/// </typeparam>
/// <typeparam name="TTuning">
/// Тип настроек.
/// </typeparam>
public sealed class WorkerContext<TWorker, TTuning>
    where TWorker : Worker<TWorker, TTuning>
    where TTuning : Tuning
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="tuning">
    /// Настройки.
    /// </param>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="tuning"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public WorkerContext(TTuning tuning, ILogger<TWorker> logger)
    {
        //  Установка настроек.
        Tuning = Check.IsNotNull(tuning, nameof(tuning));

        //  Установка средства записи в журнал службы.
        Logger = Check.IsNotNull(logger, nameof(logger));
    }

    /// <summary>
    /// Возвращает настройки.
    /// </summary>
    public TTuning Tuning { get; }

    /// <summary>
    /// Возвращает средство записи в журнал службы.
    /// </summary>
    public ILogger<TWorker> Logger { get; }
}
