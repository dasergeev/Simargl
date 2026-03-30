using System.Runtime.CompilerServices;
using System.Text;

namespace Apeiron.IO;

/// <summary>
/// Представляет буфер распределителя данных потока.
/// </summary>
internal sealed class SpreaderBuffer
{
    /// <summary>
    /// Постоянная, определяющая размер буфера.
    /// </summary>
    private const int _BufferSize = 16;

    /// <summary>
    /// Поле для хранения буфера.
    /// </summary>
    private readonly byte[] _Buffer;

    /// <summary>
    /// Поле для хранения исходного потока.
    /// </summary>
    private readonly Stream _SourceStream;

    /// <summary>
    /// Поле для хранения потока в памяти.
    /// </summary>
    private readonly MemoryStream _MemoryStream;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="stream">
    /// Исходный поток.
    /// </param>
    /// <param name="encoding">
    /// Кодировка, которую необходимо использовать при распределении данных потока.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="encoding"/> передана пустая ссылка.
    /// </exception>
    public SpreaderBuffer(Stream stream, Encoding encoding)
    {
        //  Установка исходного потока.
        _SourceStream = IsNotNull(stream, nameof(stream));

        //  Проверка ссылки на кодировку.
        IsNotNull(encoding, nameof(encoding));

        //  Создание буфера.
        _Buffer = new byte[_BufferSize];

        //  Создание потока в памяти.
        _MemoryStream = new(_Buffer);

        //  Создание средства для чтения двоичных данных из буфера.
        Reader = new(_MemoryStream, encoding, true);

        //  Создание средства для записи двоичных данных в буфер.
        Writer = new(_MemoryStream, encoding, true);

        //  Установка текущей позиции чтения.
        ReadPosition = 0;
    }

    /// <summary>
    /// Возвращает или задаёт текущую позицию чтения.
    /// </summary>
    public int ReadPosition { get; set; }

    /// <summary>
    /// Возвращает средство для чтения двоичных данных из буфера.
    /// </summary>
    public BinaryReader Reader { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    /// Возвращает средство для записи двоичных данных в буфер.
    /// </summary>
    public BinaryWriter Writer { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    /// Выполняет сброс буфера.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        //  Установка потока в памяти на начальную позицию.
        _MemoryStream.Position = 0;
    }

    /// <summary>
    /// Загружает данные в буфер из исходного потока.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые необходимо загрузить из исходного потока.
    /// </param>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Load([ParameterNoChecks] int count)
    {
        //  Общее количество прочитанных байт.
        int totalRead = 0;

        //  Основной цикл чтения данных.
        while (totalRead < count)
        {
            //  Чтение данных из исходного потока.
            int stepRead = _SourceStream.Read(_Buffer, totalRead, count - totalRead);

            //  Проверка количества прочитанных байт.
            if (stepRead == 0)
            {
                //  Достигнут конец потока.
                throw Exceptions.StreamEnd();
            }

            //  Корректировка позиции чтения.
            ReadPosition += stepRead;

            //  Корректировка общего числа прочитанных байт.
            totalRead += stepRead;
        }
    }

    /// <summary>
    /// Асинхронно загружает данные в буфер из исходного потока.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые необходимо загрузить из исходного потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая загрузку данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task LoadAsync([ParameterNoChecks] int count, CancellationToken cancellationToken)
    {
        //  Общее количество прочитанных байт.
        int totalRead = 0;

        //  Основной цикл чтения данных.
        while (totalRead < count)
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Чтение данных из исходного потока.
            int stepRead = await _SourceStream.ReadAsync(
                _Buffer.AsMemory(totalRead, count - totalRead), cancellationToken)
                .ConfigureAwait(false);

            //  Проверка количества прочитанных байт.
            if (stepRead == 0)
            {
                //  Достигнут конец потока.
                throw Exceptions.StreamEnd();
            }

            //  Корректировка позиции чтения.
            ReadPosition += stepRead;

            //  Корректировка общего числа прочитанных байт.
            totalRead += stepRead;
        }
    }

    /// <summary>
    /// Сохраняет данные из буфера в исходный поток.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые необходимо сохранить в исходном потоке.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Save([ParameterNoChecks] int count)
    {
        //  Сохранение данных в поток.
        _SourceStream.Write(_Buffer, 0, count);
    }

    /// <summary>
    /// Асинхронно сохраняет данные из буфера в исходный поток.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые необходимо сохранить в исходном потоке.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сохранение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task SaveAsync([ParameterNoChecks] int count, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Сохранение данных в поток.
        await _SourceStream.WriteAsync(_Buffer.AsMemory(0, count), cancellationToken).ConfigureAwait(false);
    }
}
