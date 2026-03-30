namespace Simargl.Hardware.TuningsEditor;

/// <summary>
/// Представляет ядро приложения.
/// </summary>
public sealed class Kernel :
    ObservableObject,
    IAsyncDisposable
{
    /// <summary>
    /// Останавливает работу ядра приложения.
    /// </summary>
    public void Stop()
    {
        _ = this;
    }

    /// <summary>
    /// Асинхронно разрушает объект.
    /// </summary>
    /// <returns>
    /// Задача, разрушающая объект.
    /// </returns>
    public async ValueTask DisposeAsync()
    {
        

        //  Ожидание завершённой задачи.
        await ValueTask.CompletedTask.ConfigureAwait(false);
    }
}
