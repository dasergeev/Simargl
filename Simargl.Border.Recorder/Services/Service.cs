using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Simargl.Border.Recorder.Services;

/// <summary>
/// Представляет службу.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public abstract class Service(Program program, ILogger logger) :
    BackgroundService
{
    /// <summary>
    /// Возвращает программу.
    /// </summary>
    protected Program Program { get; } = program;

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected abstract Task InvokeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно выполняет поддержку выполнения службы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поддержку 
    /// </returns>
    protected override sealed async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Запись в журнал.
                Logger.LogInformation("Запуск службы.");

                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);

                //  Запись в журнал.
                Logger.LogInformation("Остановка службы.");

                //  Завершение работы.
                return;
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод информации в журнал.
                    Logger.LogError(ex, "Остановка службы.");
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(BasisConstants.ServiceRecoveryTimeout, cancellationToken).ConfigureAwait(false);
        }
    }
}
