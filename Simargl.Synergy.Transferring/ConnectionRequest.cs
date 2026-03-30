using Simargl.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Synergy.Transferring;

/// <summary>
/// Представляет запрос на подключение.
/// </summary>
public sealed class ConnectionRequest :
    Message
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="identifier">
    /// Идентификатор клиента.
    /// </param>
    public ConnectionRequest(string identifier) :
        base(MessageFormat.ConnectionRequest)
    {
        //  Установка значений свойств.
        Identifier = IsNotNull(identifier);

        //  Для анализатора.
        _ = this;
    }

    /// <summary>
    /// Возвращает идентификатор клиента.
    /// </summary>
    public string Identifier { get; }

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
        //  Сохранение идентификатора.
        await spreader.WriteStringAsync(Identifier, cancellationToken).ConfigureAwait(false);
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
    internal static async Task<ConnectionRequest> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Чтение идентификатора.
        string identifier = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат сообщения.
        return new(identifier);
    }
}
