using Microsoft.Extensions.Hosting;

namespace Simargl.Services;

/// <summary>
/// Представляет службу.
/// </summary>
public abstract class Service :
    IHostedService
{
    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private volatile CancellationTokenSource? _CancellationTokenSource;

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
    /// Асинхронно запускает службу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, запускающая службу.
    /// </returns>
    Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        if (cancellationToken.IsCancellationRequested)
        {
            //  Задача отменена.
            return Task.FromCanceled(cancellationToken);
        }

        //  Создание источникат токена отмены.
        CancellationTokenSource tokenSource = new();

        //  Получение токена отмены.
        CancellationToken token = tokenSource.Token;

        //  Установка источника токена отмены.
        if (Interlocked.CompareExchange(ref _CancellationTokenSource, tokenSource, null) is not null)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение источника токена отмены.
                tokenSource.Dispose();
            }
            catch { }

            //  Выброс исключения.
            return Task.FromException(new InvalidOperationException("Произошла попытка запуска работающей службы."));
        }

        //  Запуск асинхронной задачи.
        _ = Task.Run(async delegate
        {
            //  Выполнение основной работы.
            await InvokeAsync(cancellationToken).ConfigureAwait(false);
        }, token).ConfigureAwait(false);

        //  Возврат завершённой задачи.
        return Task.CompletedTask;
    }

    /// <summary>
    /// Асинхронно запускает службу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, запускающая службу.
    /// </returns>
    async Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
        
    }
}
