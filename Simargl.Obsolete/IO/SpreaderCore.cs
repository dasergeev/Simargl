using Simargl;
using System.IO;
using System.Runtime.CompilerServices;

namespace Simargl.IO;

/// <summary>
/// Представляет ядро распределителя данных потока.
/// </summary>
/// <param name="stream">
/// Поток, данные которого необходимо распределять.
/// </param>
internal sealed class SpreaderCore(Stream stream) :
    Anything
{
    /// <summary>
    /// Поле для хранения потока, данные которого необходимо распределять.
    /// </summary>
    private readonly Stream _Stream = IsNotNull(stream, nameof(stream));

    /// <summary>
    /// Возвращает текущую позицию чтения.
    /// </summary>
    public int ReadPosition
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private set;
    } = 0;

    /// <summary>
    /// Возвращает текущую позицию записи.
    /// </summary>
    public int WritePosition
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private set;
    } = 0;

    /// <summary>
    /// Считывает указанное количество байтов из потока, начиная с заданной точки в массиве байтов.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, в который должны считываться данные.
    /// </param>
    /// <param name="index">
    /// Стартовая точка в буфере, начиная с которой считываемые данные записываются в буфер.
    /// </param>
    /// <param name="count">
    /// Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ReadBytes(byte[] buffer, int index, int count)
    {
        //  Проверка буфера.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Смещение позиции чтения.
        int offset = 0;

        //  Цикл чтения.
        while (offset < count)
        {
            //  Чтение из потока.
            int readBytes = _Stream.Read(buffer, index + offset, count - offset);

            //  Корректировка позиции чтения.
            ReadPosition += readBytes;

            //  Проверка количества полученных байт.
            if (readBytes == 0)
            {
                //  Достигнут конец потока.
                throw new EndOfStreamException();
            }

            //  Смещение позиции чтения.
            offset += readBytes;
        }
    }

    /// <summary>
    /// Асинхронно считывает указанное количество байтов из потока, начиная с заданной точки в массиве байтов.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, в который должны считываться данные.
    /// </param>
    /// <param name="index">
    /// Стартовая точка в буфере, начиная с которой считываемые данные записываются в буфер.
    /// </param>
    /// <param name="count">
    /// Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task ReadBytesAsync(byte[] buffer, int index, int count, CancellationToken cancellationToken)
    {
        //  Проверка буфера.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Смещение позиции чтения.
        int offset = 0;

        //  Цикл чтения.
        while (offset < count)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Чтение из потока.
            int readBytes = await _Stream.ReadAsync(
                buffer.AsMemory(index + offset, count - offset), cancellationToken).ConfigureAwait(false);

            //  Корректировка позиции на чтение.
            ReadPosition += readBytes;

            //  Проверка количества полученных байт.
            if (readBytes == 0)
            {
                //  Достигнут конец потока.
                throw new EndOfStreamException();
            }

            //  Смещение позиции чтения.
            offset += readBytes;
        }
    }

    /// <summary>
    /// Выполняет запись части массива байтов в текущий поток.
    /// </summary>
    /// <param name="buffer">
    /// Массив байтов, содержащий записываемые в поток данные.
    /// </param>
    /// <param name="index">
    /// Индекс первого байта для чтения из <paramref name="buffer"/> и для записи в поток.
    /// </param>
    /// <param name="count">
    /// Количество байтов для чтения из <paramref name="buffer"/> и для записи в поток.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteBytes(byte[] buffer, int index, int count)
    {
        //  Проверка буфера.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Запись в поток.
        _Stream.Write(buffer, index, count);

        //  Корректировка положения для записи.
        WritePosition += count;
    }

    /// <summary>
    /// Асинхронно выполняет запись части массива байтов в текущий поток.
    /// </summary>
    /// <param name="buffer">
    /// Массив байтов, содержащий записываемые в поток данные.
    /// </param>
    /// <param name="index">
    /// Индекс первого байта для чтения из <paramref name="buffer"/> и для записи в поток.
    /// </param>
    /// <param name="count">
    /// Количество байтов для чтения из <paramref name="buffer"/> и для записи в поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task WriteBytesAsync(byte[] buffer, int index, int count, CancellationToken cancellationToken)
    {
        //  Проверка буфера.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Запись в поток.
        await _Stream.WriteAsync(buffer.AsMemory(index, count), cancellationToken).ConfigureAwait(false);

        //  Корректировка позиции на запись.
        WritePosition += count;
    }
}
