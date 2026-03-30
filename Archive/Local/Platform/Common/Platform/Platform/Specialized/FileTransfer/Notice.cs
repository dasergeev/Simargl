namespace Apeiron.Platform.Specialized.FileTransfer;

/// <summary>
/// Представляет уведомление о получении пакета данных.
/// </summary>
public sealed class Notice
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Notice() :
        this(new())
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="relation">
    /// Описание пакета данных.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="relation"/> передана пустая ссылка.
    /// </exception>
    public Notice(Relation relation)
    {
        //  Установка описания пакета данных.
        Relation = Check.IsNotNull(relation, nameof(relation));
    }

    /// <summary>
    /// Возвращает описание пакета данных.
    /// </summary>
    public Relation Relation { get; private set; }

    ///// <summary>
    ///// Возвращает размер объекта в байтах.
    ///// </summary>
    //int ISizeable.Size =>
    //    SizeDeterminer.SizeOf(Relation);

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

        //  Запись описания пакета данных.
        await Relation.SaveAsync(stream, cancellationToken).ConfigureAwait(false);
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
    public async Task<Notice> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        Check.IsReadable(stream, nameof(stream));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение описания пакета данных.
        Relation = await new Relation().LoadAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного объекта.
        return this;
    }
}
