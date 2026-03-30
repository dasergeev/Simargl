namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет активный объект.
/// </summary>
public abstract class Active :
    Element
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public Active(Engine engine) :
        base(engine)
    {
        //  Добавление нового асинхронного действия.
        Engine.Attach(InvokeAsync);
    }

    /// <summary>
    /// Асинхронно выполняет основную задачу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Основная задача.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected virtual async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Ожидание завершённой задачи.
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
