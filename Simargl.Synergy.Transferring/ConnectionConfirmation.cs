using Simargl.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Synergy.Transferring;

/// <summary>
/// Представляет подтверждение подключения.
/// </summary>
public sealed class ConnectionConfirmation :
    Message
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public ConnectionConfirmation() :
        base(MessageFormat.ConnectionConfirmation)
    {

    }

    /// <summary>
    /// Асинхронно сохраняет сообщение в поток.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохраняющая данные в поток.
    /// </returns>
    protected override sealed async Task SaveAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        await Task.CompletedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает сообщение из потока.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные из потока.
    /// </returns>
    internal static async Task<ConnectionConfirmation> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        await Task.CompletedTask.ConfigureAwait(false);

        //  Возврат сообщения.
        return new();
    }
}
