using Simargl.IO;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Frames.Gantner.Raw;

/// <summary>
/// Представляет средство чтения потока в формате <see cref="StorageFormat.Gantner"/>.
/// </summary>
/// <param name="stream">
/// Поток, из которого нужно прочитать данные.
/// </param>
/// <param name="encoding">
/// Кодировка символов.
/// </param>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="stream"/> передана пустая ссылка.
/// </exception>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="encoding"/> передана пустая ссылка.
/// </exception>
public sealed class RawGantnerReader(Stream stream, Encoding encoding)
{
    /// <summary>
    /// Поле для хранения распределителя данных потока.
    /// </summary>
    private readonly Spreader _Spreader = new(stream, encoding);

    /// <summary>
    /// Возвращает или задаёт значение, определяющее используется ли порядок байтов от старшего к младшему.
    /// </summary>
    public bool IsBigEndian
    {
        get => _Spreader.IsBigEndian;
        set => _Spreader.IsBigEndian = value;
    }

    /// <summary>
    /// Возвращает текущую позицию.
    /// </summary>
    public long Position => _Spreader.ReadPosition;

    /// <summary>
    /// Считывает значение типа <see cref="bool"/>
    /// из текущего потока и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public bool ReadBoolean() => _Spreader.ReadBoolean();

    /// <summary>
    /// Асинхронно считывает логическое значение.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<bool> ReadBooleanAsync(CancellationToken cancellationToken)
    {
        //  Чтение значения.
        return await _Spreader.ReadBooleanAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Считывает значение типа <see cref="ushort"/>
    /// из текущего потока и перемещает текущую позицию в потоке на два байта вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public ushort ReadUInt16() => _Spreader.ReadUInt16();

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="ushort"/>
    /// из текущего потока и перемещает текущую позицию в потоке на два байта вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public async Task<ushort> ReadUInt16Async(CancellationToken cancellationToken)
    {
        //  Чтение значения.
        return await _Spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение строки.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public string ReadString()
    {
        //  Чтение длины строки.
        ushort length = _Spreader.ReadUInt16();

        //  Чтение данных строки.
        byte[] bytes = _Spreader.ReadBytes(length);

        //  Получение строки.
        string value = _Spreader.Encoding.GetString(bytes);

        //  Обрезка завершающих нулей.
        value = value.TrimEnd('\0');

        //  Возврат прочитанного значения.
        return value;
    }

    /// <summary>
    /// Асинхронно выполняет чтение строки.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение строки.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<string> ReadStringAsync(CancellationToken cancellationToken)
    {
        //  Чтение длины строки.
        ushort length = await _Spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);

        //  Чтение данных строки.
        byte[] bytes = await _Spreader.ReadBytesAsync(length, cancellationToken).ConfigureAwait(false);

        //  Получение строки.
        string value = _Spreader.Encoding.GetString(bytes);

        //  Обрезка завершающих нулей.
        value = value.TrimEnd('\0');

        //  Возврат прочитанного значения.
        return value;
    }

    /// <summary>
    /// Пропускает указанное количество байт.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые нужно пропустить.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public void Skip(int count)
    {
        //  Чтение данных.
        _ = _Spreader.ReadBytes(count);
    }

    /// <summary>
    /// Асинхронно пропускает указанное количество байт.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые нужно пропустить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, пропускающая указанное количество байт.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task SkipAsync(int count, CancellationToken cancellationToken)
    {
        //  Чтение данных.
        _ = await _Spreader.ReadBytesAsync(count, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Считывает значение типа <see cref="double"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public double ReadFloat64() => _Spreader.ReadFloat64();

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="double"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<double> ReadFloat64Async(CancellationToken cancellationToken)
    {
        //  Чтение данных.
        return await _Spreader.ReadFloat64Async(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Считывает значение в указанном формате.
    /// </summary>
    /// <param name="format">
    /// Формат, в котором необходимо прочитать значение.
    /// </param>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Произошла попытка чтения значения в недопустимом формате.
    /// </exception>
    public double ReadData(GantnerDataFormat format)
    {
        return (double)(format switch
        {
            GantnerDataFormat.Boolean => _Spreader.ReadBoolean() ? 1 : 0,
            GantnerDataFormat.Int8 => _Spreader.ReadInt8(),
            GantnerDataFormat.UInt8 => _Spreader.ReadUInt8(),
            GantnerDataFormat.Int16 => _Spreader.ReadInt16(),
            GantnerDataFormat.UInt16 => _Spreader.ReadUInt16(),
            GantnerDataFormat.Int32 => _Spreader.ReadInt32(),
            GantnerDataFormat.UInt32 => _Spreader.ReadUInt32(),
            GantnerDataFormat.Float32 => _Spreader.ReadFloat32(),
            GantnerDataFormat.BitSet8 => _Spreader.ReadUInt8(),
            GantnerDataFormat.BitSet16 => _Spreader.ReadUInt16(),
            GantnerDataFormat.BitSet32 => _Spreader.ReadUInt32(),
            GantnerDataFormat.Float64 => _Spreader.ReadFloat64(),
            GantnerDataFormat.Int64 => _Spreader.ReadInt64(),
            GantnerDataFormat.UInt64 => _Spreader.ReadUInt64(),
            GantnerDataFormat.BitSet64 => _Spreader.ReadUInt64(),
            _ => throw new InvalidDataException(
                $"Произошла попытка чтения значения в недопустимом формате {format}."),
        });
    }

    /// <summary>
    /// Асинхронно считывает значение в указанном формате.
    /// </summary>
    /// <param name="format">
    /// Формат, в котором необходимо прочитать значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Произошла попытка чтения значения в недопустимом формате.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<double> ReadDataAsync(GantnerDataFormat format, CancellationToken cancellationToken)
    {
        return (double)(format switch
        {
            GantnerDataFormat.Boolean => await _Spreader.ReadBooleanAsync(cancellationToken).ConfigureAwait(false) ? 1 : 0,
            GantnerDataFormat.Int8 => await _Spreader.ReadInt8Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.UInt8 => await _Spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.Int16 => await _Spreader.ReadInt16Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.UInt16 => await _Spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.Int32 => await _Spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.UInt32 => await _Spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.Float32 => await _Spreader.ReadFloat32Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.BitSet8 => await _Spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.BitSet16 => await _Spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.BitSet32 => await _Spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.Float64 => await _Spreader.ReadFloat64Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.Int64 => await _Spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.UInt64 => await _Spreader.ReadUInt64Async(cancellationToken).ConfigureAwait(false),
            GantnerDataFormat.BitSet64 => await _Spreader.ReadUInt64Async(cancellationToken).ConfigureAwait(false),
            _ => throw new InvalidDataException(
                $"Произошла попытка чтения значения в недопустимом формате {format}."),
        });
    }
}
