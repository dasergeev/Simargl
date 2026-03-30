using Simargl.Designing.Utilities;
using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing;

/// <summary>
/// Представляет предписание для веб-страницы.
/// </summary>
public abstract class WebPageOrder
{
    /// <summary>
    /// Поле для хранения тайм-аута.
    /// </summary>
    private readonly int _Timeout = System.Threading.Timeout.Infinite;

    /// <summary>
    /// Возвращает или инициализирует тайм-аут.
    /// </summary>
    public TimeSpan Timeout
    {
        get => _Timeout >= 0 ? TimeSpan.FromMilliseconds(_Timeout) : System.Threading.Timeout.InfiniteTimeSpan;
        init => _Timeout = IsTimeout(value, nameof(Timeout));
    }

    /// <summary>
    /// Выполняет попытку установить исключение.
    /// </summary>
    /// <param name="exception">
    /// Исключение, которое необходимо установить.
    /// </param>
    internal abstract void TrySetException(Exception exception);

    /// <summary>
    /// Асинхронно выполняет предписание.
    /// </summary>
    /// <param name="controller">
    /// Контроллер веб-страницы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая предписание.
    /// </returns>
    internal abstract Task ExecutionCoreAsync(WebPageController controller, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно выполняет предписание.
    /// </summary>
    /// <param name="controller">
    /// Контроллер веб-страницы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая предписание.
    /// </returns>
    internal async Task ExecutionAsync(WebPageController controller, CancellationToken cancellationToken)
    {
        //  Источник токена отмены по тайм-ауту.
        CancellationTokenSource? timeoutTokenSource = null;

        //  Токен отмены по тайм-ауту.
        CancellationToken timeoutToken = CancellationToken.None;

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Операция отменена.
                TrySetException(ExceptionsCreator.Cancelled());

                //  Завершение выполнения предписания.
                return;
            }

            //  Проверка тайм-аута.
            if (_Timeout != System.Threading.Timeout.Infinite)
            {
                //  Создание источника токена отмены по тайм-ауту.
                timeoutTokenSource = new();

                //  Установка времени ожидания.
                timeoutTokenSource.CancelAfter(_Timeout);

                //  Установка токена отмены по тайм-ауту.
                timeoutToken = timeoutTokenSource.Token;
            }

            //  Создание источника связанного токена отмены.
            using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                timeoutToken, cancellationToken);

            //  Получение связанного токена отмены.
            CancellationToken linkedToken = linkedTokenSource.Token;

            //  Выполнение предписания.
            await ExecutionCoreAsync(controller, linkedToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Подавление всех некритических исключений.
            DefyCritical(delegate
            {
                //  Проверка основного токена отмены.
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Операция отменена.
                    TrySetException(ExceptionsCreator.Cancelled());
                }
                //  Проверка токена отмены по тайм-ауту.
                else if (timeoutToken.IsCancellationRequested)
                {
                    //  Время ожидания операции истекло.
                    TrySetException(ExceptionsCreator.OperationTimeout());
                }
                else
                {
                    //  Произошла ошибка.
                    TrySetException(ex);
                }
            });
        }

        //  Проверка источника токена отмены по тайм-ауту.
        if (timeoutTokenSource is not null)
        {
            //  Разрушение источника токена отмены по тайм-ауту.
            DefyCritical(timeoutTokenSource.Dispose);
        }

        //  Подавление всех некритических исключений.
        DefyCritical(delegate
        {
            //  Контрольная попытка установить исключение.
            TrySetException(ExceptionsCreator.OperationInvalid());
        });
    }
}
