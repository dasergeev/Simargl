using System.Threading;
using System.Threading.Tasks;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет объект, выполняющий работу ядра.
/// </summary>
public abstract class Worker
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="core">
    /// Ядро.
    /// </param>
    public Worker(Core core)
    {
        //  Установка ядра.
        Core = core;

        //  Запуск основной задачи.
        _ = Task.Run(async delegate
        {
            //  Выполнение основной работы.
            await InvokeAsync(core.MainCancellationToken).ConfigureAwait(false);
        }, core.MainCancellationToken);
    }

    /// <summary>
    /// Возвращает ядро.
    /// </summary>
    public Core Core { get; }

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    public Journal Journal => Core.Journal;

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
}
