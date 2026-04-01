namespace Simargl.Com;

/// <summary>
/// Представляет действие с COM-объектом.
/// </summary>
internal sealed class ComAction
{
    /// <summary>
    /// Поле для хранения действия.
    /// </summary>
    private Action? _Action;

    /// <summary>
    /// Поле для хранения источника токена завершения задачи.
    /// </summary>
    private readonly TaskCompletionSource _TaskCompletionSource;

    /// <summary>
    /// Поле для хранения делегата обратного вызова.
    /// </summary>
    private readonly CancellationTokenRegistration _CancellationTokenRegistration;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="action">
    /// Базовое действие.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public ComAction(Action action, CancellationToken cancellationToken)
    {
        //  Установка действия.
        _Action = IsNotNull(action);

        //  Создание источника токена завершения задачи.
        _TaskCompletionSource = new();

        //  Регистрация метода отмены задачи.
        _CancellationTokenRegistration = cancellationToken.Register(Cancel);
    }

    /// <summary>
    /// Возвращает задачу.
    /// </summary>
    public Task Task => _TaskCompletionSource.Task;

    /// <summary>
    /// Выполняет действие.
    /// </summary>
    public void Invoke()
    {
        //  Извлечение действия.
        if (Interlocked.Exchange(ref _Action, null) is Action action)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение действия.
                action();

                //  Установка состояния.
                DefyCritical(_TaskCompletionSource.SetResult);
            }
            catch (Exception ex)
            {
                //  Установка исключения.
                DefyCritical(() => _TaskCompletionSource.SetException(ex));
            }

            //  Отмена регистрации метода отмены.
            DefyCritical(_CancellationTokenRegistration.Dispose);
        }
    }

    /// <summary>
    /// Отменяет задачу.
    /// </summary>
    private void Cancel()
    {
        //  Сброс действия.
        if (Interlocked.Exchange(ref _Action, null) is not null)
        {
            //  Отмена выполнения.
            DefyCritical(_TaskCompletionSource.SetCanceled);

            //  Отмена регистрации метода отмены.
            DefyCritical(_CancellationTokenRegistration.Dispose);
        }
    }
}
