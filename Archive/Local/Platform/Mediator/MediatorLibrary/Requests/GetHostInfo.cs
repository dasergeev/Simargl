using Apeiron.IO;
using System.Text;

namespace Apeiron.Platform.MediatorLibrary.Requests;

/// <summary>
/// Запрос на получение информации о службе.
/// </summary>
public class GetHostInfo : Request
{
    /// <summary>
    /// Имя узла.
    /// </summary>
    public string Hostname { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор метода.
    /// </summary>
    public MediatorMethodId MediatorMethodId { get; set; }

    ///// <summary>
    ///// Возвращает размер запроса.
    ///// </summary>
    //public override int Size => SizeDeterminer.SizeOf(Hostname, Encoding.UTF8)
    //                        + SizeDeterminer.SizeOf((int)MediatorMethodId);

    /// <summary>
    /// Асинхронно сохраняет данные объекта в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию записи.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Поток не поддерживает запись.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public new async Task SaveAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        Check.IsWritable(stream, nameof(stream));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства записи в поток.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Запись имени хоста.
        await spreader.WriteStringAsync(Hostname, cancellationToken).ConfigureAwait(false);

        //  Запись времени создания сообщения.
        await spreader.WriteInt32Async((int)MediatorMethodId, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает данные объекта из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию чтения.
    /// </returns>
    /// <remarks>
    /// Задача, должна возвращать объект, к которому обращаются.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public new async Task<GetHostInfo> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        Check.IsReadable(stream, nameof(stream));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства чтения из потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Считывает имя хоста.
        Hostname = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

        //  Считывает время.
        MediatorMethodId = (MediatorMethodId)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        return this;
    }
}
