using Apeiron.IO;
using System.Text;

namespace Apeiron.Platform.Specialized.FileTransfer;

/// <summary>
/// Представляет пакет данных.
/// </summary>
public sealed class Parcel
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Parcel() :
        this(new(), Array.Empty<byte>())
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="relation">
    /// Описание пакета данных.
    /// </param>
    /// <param name="data">
    /// Массив данных.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="relation"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    public Parcel(Relation relation, byte[] data)
    {
        //  Установка описания пакета данных.
        Relation = Check.IsNotNull(relation, nameof(relation));

        //  Установка массива данных.
        Data = Check.IsNotNull(data, nameof(data));
    }

    /// <summary>
    /// Возвращает описание пакета данных.
    /// </summary>
    public Relation Relation { get; private set; }

    /// <summary>
    /// Возвращает массив данных.
    /// </summary>
    public byte[] Data { get; private set; }

    ///// <summary>
    ///// Возвращает размер объекта в байтах.
    ///// </summary>
    //public int Size =>
    //    SizeDeterminer.SizeOf(Relation) +
    //    Data.Length;

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
    public async Task SaveAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        Check.IsWritable(stream, nameof(stream));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства записи в поток.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Запись описания пакета данных.
        await Relation.SaveAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Запись длины массива данных.
        await spreader.WriteInt32Async(Data.Length, cancellationToken).ConfigureAwait(false);

        //  Запись массива данных.
        await spreader.WriteBytesAsync(Data, cancellationToken).ConfigureAwait(false);
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
    public async Task<Parcel> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        Check.IsReadable(stream, nameof(stream));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства чтения из потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Чтение описания пакета данных.
        Relation = await new Relation().LoadAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Чтение длины массива данных.
        int length = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Чтение массива данных.
        Data = await spreader.ReadBytesAsync(length, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного объекта.
        return this;
    }
}
