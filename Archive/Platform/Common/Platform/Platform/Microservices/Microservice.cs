using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Apeiron.Platform.Microservices;

/// <summary>
/// Представляет микрослужбу.
/// </summary>
/// <typeparam name="TMicroservice">
/// Тип микрослужбы.
/// </typeparam>
public abstract class Microservice<TMicroservice> :
    BackgroundService
    where TMicroservice : Microservice<TMicroservice>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала микрослужбы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    protected Microservice(ILogger<TMicroservice> logger)
    {
        //  Установка средства ведения журнала микрослужбы.
        Logger = Check.IsNotNull(logger, nameof(logger));

        //  Установка имени микрослужбы.
        Name = GetType().Name;

        //  Установка полного имени микрослужбы.
        FullName = GetType().FullName ?? Name;
    }

    /// <summary>
    /// Представляет средство ведения журнала микрослужбы.
    /// </summary>
    protected ILogger<TMicroservice> Logger { get; }

    /// <summary>
    /// Возвращает имя микрослужбы.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает полное имя микрослужбы.
    /// </summary>
    public string FullName { get; }

    /// <summary>
    /// Асинхронно выполняет безопасный вызов действия.
    /// </summary>
    /// <param name="actionAsync">
    /// Действие, которое необходимо выполнить безопасно.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая безопасный вызов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected async ValueTask SafeCallAsync(
        [ParameterNoChecks] Func<CancellationToken, Task> actionAsync, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блок перехвата некритических исключений.
        try
        {
            //  Асинхронный вызов действия.
            await actionAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Проверка критического исключения.
            if (ex.IsCritical())
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Запись информации об ошибке в журнал.
                Logger.LogError("{exception}", ex);
            }
        }
    }
}
