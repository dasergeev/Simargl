using Apeiron.Services.GlobalIdentity.Tunings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Apeiron.Services.GlobalIdentity.Workers;

/// <summary>
/// Представляет фоновый процесс службы глобальной идентификации.
/// </summary>
/// <typeparam name="TWorker">
/// Тип фонового процесса службы глобальной идентификации.
/// </typeparam>
/// <typeparam name="TTuning">
/// Тип настроек.
/// </typeparam>
public abstract class Worker<TWorker, TTuning> :
    BackgroundService
    where TWorker : Worker<TWorker, TTuning>
    where TTuning : Tuning
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="context">
    /// Контекст фонового процесса службы глобальной идентификации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="context"/> передана пустая ссылка.
    /// </exception>
    public Worker(WorkerContext<TWorker, TTuning> context)
    {
        //  Установка контекста фонового процесса службы глобальной идентификации.
        Context = Check.IsNotNull(context, nameof(context));
    }

    /// <summary>
    /// Возвращает контекст фонового процесса службы глобальной идентификации.
    /// </summary>
    protected WorkerContext<TWorker, TTuning> Context { get; }

    /// <summary>
    /// Возвращает настройки.
    /// </summary>
    protected TTuning Tuning => Context.Tuning;

    /// <summary>
    /// Возвращает средство записи в журнал службы.
    /// </summary>
    protected ILogger<TWorker> Logger => Context.Logger;

    /// <summary>
    /// Ассинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected abstract Task InvokeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Ассинхронно выполняет фоновую работу с поддержкой.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу с поддержкой.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override sealed async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Задержка для инициализации консоли и выдачи служебных сообщений.
        await Task.Delay(50, cancellationToken).ConfigureAwait(false);

        //  Основной цикл поддержки выполнения.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех некритических исключений.
            try
            {
                //  Выполнение фоновой работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод сообщения об ошибке в журнал.
                    Logger.LogError("{exception}", ex);
                }

                //  Проверка критического исключения.
                if (ex.IsCritical())
                {
                    //  Повторный выброс исключения.
                    throw;
                }
            }
        }
    }
}
