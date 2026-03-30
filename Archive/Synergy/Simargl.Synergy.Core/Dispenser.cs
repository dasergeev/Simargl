using Simargl.Synergy.Core.Criticalling;
using System.Net.Security;
using System.Runtime.CompilerServices;

namespace Simargl.Synergy.Core;

/// <summary>
/// Представляет распределитель данных потока.
/// </summary>
internal sealed class Dispenser :
    Critical
{
    /// <summary>
    /// Поле для хранения семафора.
    /// </summary>
    private SemaphoreSlim _Semaphore = null!;

    /// <summary>
    /// Поле для хранения SSL-потока.
    /// </summary>
    private SslStream _SslStream = null!;

    /// <summary>
    /// Поле для хранения токена отмены операции чтения.
    /// </summary>
    private CancellationTokenSource _ReadingCancellationToken = null!;

    /// <summary>
    /// Поле для хранения общего размера записанных данных в байтах.
    /// </summary>
    private long _WriteTotalSize = 0;

    /// <summary>
    /// Поле для хранения общего размера прочитанных данных в байтах.
    /// </summary>
    private long _ReadTotalSize = 0;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    private Dispenser()
    {

    }

    /// <summary>
    /// Возвращает общий размер записанных данных в байтах.
    /// </summary>
    public long WriteTotalSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Interlocked.Read(ref _WriteTotalSize);
    }

    /// <summary>
    /// Возвращает общий размер прочитанных данных в байтах.
    /// </summary>
    public long ReadTotalSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Interlocked.Read(ref _ReadTotalSize);
    }

    /// <summary>
    /// Асинхронно сохраняет данные в поток.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, содержащий данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохранающая данные в поток.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
    {
        //  Получение источника токена отмены.
        using CancellationTokenSource tokenSource = Volatile.Read(ref _ReadingCancellationToken);

        //  Отправка запроса на отмену.
        await tokenSource.CancelAsync().ConfigureAwait(false);

        //  Ожидание семаформа.
        await _Semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        //  Блок с гарантированным завершением.
        try
        {
            //  Запись данных в поток.
            await _SslStream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);

            //  Корректировка количества прочитанных данных.
            Interlocked.Add(ref _WriteTotalSize, buffer.Length);

            //  Замена токена отмены.
            Interlocked.Exchange(ref _ReadingCancellationToken,
                CancellationTokenSource.CreateLinkedTokenSource(CancellationToken));
        }
        finally
        {
            //  Освобождение семаформа.
            _Semaphore.Release();
        }
    }

    /// <summary>
    /// Асинхронно считывает данные и сохраняет их в заданном диапазоне памяти.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, куда помещаются прочитанные данные.
    /// </param>
    /// <returns>
    /// Задача, возвращающая количество прочитанных байт.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task<int> ReadAsync(Memory<byte> buffer)
    {
        //  Получение токена отмены.
        CancellationToken cancellationToken = Volatile.Read(ref _ReadingCancellationToken).Token;

        //  Блок перехвата исключений отмены.
        try
        {
            //  Ожидание семаформа.
            await _Semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

            //  Блок с гарантированным завершением.
            try
            {
                //  Чтение данных из потока.
                int size = await _SslStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);

                //  Корректировка количества прочитанных данных.
                Interlocked.Add(ref _ReadTotalSize, size);

                //  Возврат количества прочитанных данных.
                return size;
            }
            finally
            {
                //  Освобождение семаформа.
                _Semaphore.Release();
            }
        }
        catch
        {
            //  Проверка завершения работы.
            if (CancellationToken.IsCancellationRequested)
            {
                //  Повторный выброс исключения.
                throw;
            }
        }

        //  Не удалось прочитать данные из потока.
        return 0;
    }

    /// <summary>
    /// Асинхронно создаёт распределитель данных потока.
    /// </summary>
    /// <param name="sslStream">
    /// SSL-поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая распределитель данных потока.
    /// </returns>
    public static async Task<Dispenser> CreateAsync(SslStream sslStream, CancellationToken cancellationToken)
    {
        //  Создание распределителя данных.
        Dispenser dispenser = new();

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание семафора.
            dispenser._Semaphore = new(1, 1);
            await dispenser.AttachAsync(dispenser._Semaphore).ConfigureAwait(false);

            //  Установка потока.
            dispenser._SslStream = sslStream;
            await dispenser.AttachAsync(dispenser._SslStream).ConfigureAwait(false);

            //  Создание источника токена отмены для операции чтения.
            dispenser._ReadingCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(dispenser.CancellationToken);
            await dispenser.AddDestroyerAsync(async delegate
            {
                //  Получение источника токена отмены.
                CancellationTokenSource readingCancellationToken = Interlocked.Exchange(ref dispenser._ReadingCancellationToken, null!);

                //  Блок перехвата всех исключений.
                try
                {
                    //  Отправка запроса на отмену.
                    await readingCancellationToken.CancelAsync().ConfigureAwait(false);
                }
                catch { }

                //  Блок перехвата всех исключений.
                try
                {
                    //  Разрушение источника токена отмены.
                    readingCancellationToken.Dispose();
                }
                catch { }
            }).ConfigureAwait(false);

            //  Возврат распределителя данных.
            return dispenser;
        }
        catch
        {
            //  Разрушение распределителя данных.
            await dispenser.DisposeAsync().ConfigureAwait(false);

            //  Повторный выброс исключения.
            throw;
        }
    }
}
