using Simargl.Hardware.Recorder.Services;
using System.Collections.Concurrent;

namespace Simargl.Hardware.Recorder.Core.Logging;

/// <summary>
/// Представляет журнал.
/// </summary>
/// <param name="heart">
/// Сердце приложения.
/// </param>
/// <param name="serviceKey">
/// Значение, определяющее ключ службы.
/// </param>
public sealed class Logger(Heart heart, ServiceKey serviceKey)
{
    /// <summary>
    /// Возвращает очередь сообщений.
    /// </summary>
    private ConcurrentQueue<string> Messages { get; } = [];

    /// <summary>
    /// Возвращает значение, определяющее ключ приложения.
    /// </summary>
    public ServiceKey ServiceKey { get; } = serviceKey;

    /// <summary>
    /// Асинхронно выполняет работу журнала.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу журнала.
    /// </returns>
    public async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Ожидание завершённой задачи.
        await Task.CompletedTask.ConfigureAwait(false);

        //  Очистка очереди сообщений.
        while (Messages.Count > heart.Options.LogMaxLength &&
            !cancellationToken.IsCancellationRequested)
        {
            //  Извлечение сообщения из очереди.

            
        }
    }
}
