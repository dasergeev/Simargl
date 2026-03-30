using System.Buffers;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.CompilerServices;

namespace Simargl.Synergy.Core;

/// <summary>
/// Представляет блок данных.
/// </summary>
internal sealed class Block :
    IDisposable
{
    /// <summary>
    /// Поле для хранения данных размера пустых данных.
    /// </summary>
    private static readonly byte[] _SizeEmptyData;

    /// <summary>
    /// Поле для хранения пустых данных.
    /// </summary>
    private static readonly byte[] _EmptyData;

    /// <summary>
    /// Поле для хранения пустого блока данных.
    /// </summary>
    private static readonly Block _Empty;

    /// <summary>
    /// Возвращает пустой блок данных.
    /// </summary>
    public static Block Empty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _Empty;
    }

    /// <summary>
    /// Поле для хранения размера данных.
    /// </summary>
    private readonly int _Size;

    /// <summary>
    /// Поле для хранения данных размера данных.
    /// </summary>
    private byte[]? _SizeData;

    /// <summary>
    /// Поле для хранения данных.
    /// </summary>
    private byte[]? _Data;

    /// <summary>
    /// Инициализирует статические данные.
    /// </summary>
    static Block()
    {
        //  Создание данных размера пустых данных.
        _SizeEmptyData = new byte[sizeof(int)];

        //  Запись размера пустых данных в память.
        BinaryPrimitives.WriteInt32LittleEndian(_SizeEmptyData, 0);

        //  Установка пустых данных.
        _EmptyData = [];

        //  Создание пустого блока.
        _Empty = new(0, _SizeEmptyData, _EmptyData);
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="size">
    /// Размер данных.
    /// </param>
    /// <param name="sizeData">
    /// Данные размера данных.
    /// </param>
    /// <param name="data">
    /// Данные.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Block(int size, byte[] sizeData, byte[] data)
    {
        //  Установка значений полей.
        _Size = size;
        _SizeData = sizeData;
        _Data = data;
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="size">
    /// Размер блока данных.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Block(int size)
    {
        //  Проверка размера блока данных.
        if (size == 0)
        {
            //  Установка полей.
            _Size = 0;
            _SizeData = _SizeEmptyData;
            _Data = _EmptyData;
        }
        else
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Установка размера данных.
                _Size = size;

                //  Выделение памяти для размера.
                _SizeData = ArrayPool<byte>.Shared.Rent(sizeof(int));

                //  Запись размера в память.
                BinaryPrimitives.WriteInt32LittleEndian(_SizeData, size);

                //  Выделение памяти для данных.
                _Data = ArrayPool<byte>.Shared.Rent(size);
            }
            catch
            {
                //  Разрушение данных.
                Dispose();

                //  Повторный выброс исключения.
                throw;
            }
        }
    }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    public int Size
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _Size;
    }

    ///// <summary>
    ///// Записывает значение в память.
    ///// </summary>
    ///// <param name="offset">
    ///// Смещение, по которому необходимо записать данные.
    ///// </param>
    ///// <param name="value">
    ///// Значение, которое необходимо записать.
    ///// </param>
    //public void WriteInt32(int offset, int value)
    //{
    //    //  Запись данных в память.
    //    BinaryPrimitives.WriteInt32LittleEndian(_Data.AsSpan(offset), value);
    //}

    ///// <summary>
    ///// Читает значение из памяти.
    ///// </summary>
    ///// <param name="offset">
    ///// Смещение, по которому необходимо прочитать данные.
    ///// </param>
    ///// <returns>
    ///// Прочитанное значение.
    ///// </returns>
    //public int ReadInt32(int offset)
    //{
    //    //  Чтение данных из памяти.
    //    return BinaryPrimitives.ReadInt32LittleEndian(_Data.AsSpan(offset));
    //}



    /// <summary>
    /// Асинхронно сохраняет блок данных в поток.
    /// </summary>
    /// <param name="dispenser">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохраняющая данные в поток.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async ValueTask SaveAsync(Dispenser dispenser, CancellationToken cancellationToken)
    {
        //  Запись длины данных в поток.
        await dispenser.WriteAsync(_SizeData!.AsMemory(0, sizeof(int)), cancellationToken).ConfigureAwait(false);

        //  Запись данных в поток.
        await dispenser.WriteAsync(_Data!.AsMemory(0, _Size), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает блок данных из потока.
    /// </summary>
    /// <param name="dispenser">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая блок данных из потока.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async ValueTask<Block> LoadAsync(Dispenser dispenser, CancellationToken cancellationToken)
    {
        //  Память для размера.
        byte[]? sizeData = null;

        //  Память для данных.
        byte[]? data = null;

        //  Смещение для записи.
        int offset = 0;

        //  Блок перехвата всех исключений.
        try
        {
            //  Выделение памяти для размера.
            sizeData = ArrayPool<byte>.Shared.Rent(sizeof(int));

            //  Цикл чтения памяти для размера.
            while (offset < sizeof(int) && !cancellationToken.IsCancellationRequested)
            {
                //  Чтение данных.
                offset += await dispenser.ReadAsync(sizeData.AsMemory(offset, sizeof(int) - offset)).ConfigureAwait(false);
            }

            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение размера данных.
            int size = BinaryPrimitives.ReadInt32LittleEndian(sizeData);

            //  Проверка размера данных.
            if (size == 0)
            {
                //  Возвращение памяти.
                ArrayPool<byte>.Shared.Return(sizeData);

                //  Сброс ссылки.
                sizeData = null;

                //  Возврат пустого блока.
                return Empty;
            }

            //  Выделение памяти для данных.
            data = ArrayPool<byte>.Shared.Rent(size);

            //  Сброс смещения.
            offset = 0;

            //  Цикл чтения памяти для данных.
            while (offset < size && !cancellationToken.IsCancellationRequested)
            {
                //  Чтение данных.
                offset += await dispenser.ReadAsync(data.AsMemory(offset, size - offset)).ConfigureAwait(false);
            }

            //  Возврат блока данных.
            return new(size, sizeData, data);
        }
        catch
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Проверка данных.
                if (sizeData is not null)
                {
                    //  Возвращение памяти.
                    ArrayPool<byte>.Shared.Return(sizeData);
                }
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Проверка данных.
                if (data is not null)
                {
                    //  Возвращение памяти.
                    ArrayPool<byte>.Shared.Return(data);
                }
            }
            catch { }

            //  Повторный выброс исключения.
            throw;
        }
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        //  Проверка размера данных.
        if (_Size == 0)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Сброс данных.
                Interlocked.Exchange(ref _SizeData, null);
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Сброс данных.
                Interlocked.Exchange(ref _Data, null);
            }
            catch { }
        }
        else
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Сброс данных.
                byte[]? sizeData = Interlocked.Exchange(ref _SizeData, null);

                //  Проверка данных.
                if (sizeData is not null)
                {
                    //  Возвращение памяти.
                    ArrayPool<byte>.Shared.Return(sizeData);
                }
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Сброс данных.
                byte[]? data = Interlocked.Exchange(ref _Data, null);

                //  Проверка данных.
                if (data is not null)
                {
                    //  Возвращение памяти.
                    ArrayPool<byte>.Shared.Return(data);
                }
            }
            catch { }
        }
    }
}
