using Microsoft.Extensions.Logging;
using Simargl.Hardware.Recorder.Core;

namespace Simargl.Hardware.Recorder.Services;

/// <summary>
/// Представляет службу.
/// </summary>
/// <typeparam name="TService">
/// Тип службы.
/// </typeparam>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public abstract class Service<TService>(ILogger<TService> logger, Heart heart) :
    BackgroundService
    where TService : Service<TService>
{
    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    protected ILogger<TService> Logger { get; } = logger;

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    protected Heart Heart { get; } = heart;

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
                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);

                //  Завершение работы.
                return;
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод информации в журнал.
                    Logger.LogError("Произошла ошибка: {ex}", ex);
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }
}
