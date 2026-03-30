using Apeiron.IO;
using Apeiron.Platform.MediatorLibrary.Requests;
using System.Text;

namespace Apeiron.Platform.MediatorLibrary.Responce;

/// <summary>
/// Представлет класс описывающий информацию о хосте.
/// </summary>
public class HostInfo :
    Request
{
    /// <summary>
    /// Возвращает имя хоста.
    /// </summary>
    public string Hostname { get; set; } = Environment.MachineName.ToLower();

    /// <summary>
    /// Возвразает время создания запроса на хосте.
    /// </summary>
    public DateTime RequestTime { get; set; } = DateTime.Now;

    ///// <summary>
    ///// Возвращает размер запроса.
    ///// </summary>
    //public override int Size => SizeDeterminer.SizeOf(Hostname, Encoding.UTF8)
    //                        + SizeDeterminer.SizeOf(RequestTime);

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
        await spreader.WriteDateTimeAsync(RequestTime, cancellationToken).ConfigureAwait(false);
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
    public new async Task<HostInfo> LoadAsync(Stream stream, CancellationToken cancellationToken)
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
        RequestTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);

        return this;
    }
}
