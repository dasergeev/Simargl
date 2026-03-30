using System.Runtime.CompilerServices;

namespace Simargl.Concurrent;

/// <summary>
/// Представляет асинхронный примитив взаимноисключающей блокировки.
/// </summary>
public sealed class AsyncLock :
    IDisposable,
    IAsyncDisposable
{
    /// <summary>
    /// Поле для хранения упрощенного семафора.
    /// </summary>
    readonly SemaphoreSlim _Semaphore;

    /// <summary>
    /// Поле для хранения объекта, с помощью которого синхронизируется операция разрушения.
    /// </summary>
    readonly object _SyncDispose;

    /// <summary>
    /// Поле для хранения значения, определяющего был ли разрушен объект.
    /// </summary>
    volatile bool _IsDisposed;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public AsyncLock()
    {
        //  Создание упрощённого семафора.
        _Semaphore = new(1, 1);

        //  Создание объекта, с помощью которого синхронизируется операция разрушения.
        _SyncDispose = new();

        //  Установка значения, определяющего был ли разрушен объект.
        _IsDisposed = false;
    }

    /// <summary>
    /// Асинхронно выполняет операцию блокировки.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая операцию блокировки.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public async Task<AsyncLocking> LockAsync() => await LockAsync(Timeout.Infinite, CancellationToken.None);

    /// <summary>
    /// Асинхронно выполняет операцию блокировки.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая операцию блокировки.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public async Task<AsyncLocking> LockAsync(CancellationToken cancellationToken) => await LockAsync(Timeout.Infinite, cancellationToken);

    /// <summary>
    /// Асинхронно выполняет операцию блокировки.
    /// </summary>
    /// <param name="timeout">
    /// Количество миллисекунд ожидания, <see cref="Timeout.Infinite"/> для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    /// Задача, выполняющая операцию блокировки.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передано
    /// значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Время ожидания операции истекло.
    /// </exception>
    public async Task<AsyncLocking> LockAsync(int timeout) => await LockAsync(timeout, CancellationToken.None);

    /// <summary>
    /// Асинхронно выполняет операцию блокировки.
    /// </summary>
    /// <param name="timeout">
    /// Время ожидания, <see cref="Timeout.InfiniteTimeSpan"/> для неограниченного времени ожидания.
    /// </param>
    /// <returns>
    /// Задача, выполняющая операцию блокировки.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передано
    /// значение в миллисекундах меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Время ожидания операции истекло.
    /// </exception>
    public async Task<AsyncLocking> LockAsync(TimeSpan timeout) => await LockAsync(timeout, CancellationToken.None);

    /// <summary>
    /// Асинхронно выполняет операцию блокировки.
    /// </summary>
    /// <param name="timeout">
    /// Время ожидания, <see cref="Timeout.InfiniteTimeSpan"/> для неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая операцию блокировки.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передано
    /// значение в миллисекундах меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Время ожидания операции истекло.
    /// </exception>
    public async Task<AsyncLocking> LockAsync(TimeSpan timeout, CancellationToken cancellationToken) =>
        await LockAsync(IsTimeout(timeout), cancellationToken);

    /// <summary>
    /// Асинхронно выполняет операцию блокировки.
    /// </summary>
    /// <param name="timeout">
    /// Количество миллисекунд ожидания, <see cref="Timeout.Infinite"/> для неограниченного времени ожидания.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая операцию блокировки.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передано
    /// значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Время ожидания операции истекло.
    /// </exception>
    public async Task<AsyncLocking> LockAsync(int timeout, CancellationToken cancellationToken)
    {
        //  Проверка текущего объекта.
        CheckDisposeCore();

        //  Ожидание входа в семафор.
        bool isLock = await _Semaphore.WaitAsync(timeout, cancellationToken).ConfigureAwait(false);

        //  Проверка входа в семафор.
        if (!isLock)
        {
            //  Проверка токена отмены.
            cancellationToken.ThrowIfCancellationRequested();

            //  Время ожидания операции истекло.
            throw new TimeoutException();
        }

        //  Возврат блокировки.
        return new AsyncLocking(() => _Semaphore.Release());
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {
        //  Блокировка критического объекта.
        lock (_SyncDispose)
        {
            //  Проверка значения, определяющего был ли разрушен объект.
            if (!_IsDisposed)
            {
                //  Разрушение упрощенного семафора.
                ((IDisposable)_Semaphore).Dispose();

                //  Установка значения, определяющего был ли разрушен объект.
                _IsDisposed = true;
            }

            //  Сообщение среде CLR, что она на не должна вызывать метод завершения для текущего экземпляра объекта.
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Выполняет проверку разрушения объекта.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckDisposeCore()
    {
        //  Блокировка критического объекта.
        lock (_SyncDispose)
        {
            //  Проверка значения, определяющего был ли разрушен объект.
            ObjectDisposedException.ThrowIf(_IsDisposed, nameof(AsyncLock));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await ValueTask.CompletedTask;
        Dispose();
    }
}




//using System;
//using System.Runtime.CompilerServices;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Ably.Concurrent;

///// <summary>
///// Представляет асинхронный примитив взаимноисключающей блокировки.
///// </summary>
//public sealed class AsyncLock :
//    IDisposable
//{
//    /// <summary>
//    /// Поле для хранения упрощенного семафора.
//    /// </summary>
//    readonly SemaphoreSlim _Semaphore;

//    /// <summary>
//    /// Поле для хранения объекта, с помощью которого синхронизируется операция разрушения.
//    /// </summary>
//    readonly object _SyncDispose;

//    /// <summary>
//    /// Поле для хранения значения, определяющего был ли разрушен объект.
//    /// </summary>
//    volatile bool _IsDisposed;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    public AsyncLock()
//    {
//        //  Создание упрощённого семафора.
//        _Semaphore = new(1, 1);

//        //  Создание объекта, с помощью которого синхронизируется операция разрушения.
//        _SyncDispose = new();

//        //  Установка значения, определяющего был ли разрушен объект.
//        _IsDisposed = false;
//    }

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync() => await LockAsync(Timeout.Infinite, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(CancellationToken cancellationToken) => await LockAsync(Timeout.Infinite, cancellationToken);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Количество миллисекунд ожидания, <see cref="Timeout.Infinite"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(int timeout) => await LockAsync(timeout, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Время ожидания, <see cref="Timeout.InfiniteTimeSpan"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение в миллисекундах меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(TimeSpan timeout) => await LockAsync(timeout, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Время ожидания, <see cref="Timeout.InfiniteTimeSpan"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение в миллисекундах меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(TimeSpan timeout, CancellationToken cancellationToken) =>
//        await LockAsync(timeout, cancellationToken);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Количество миллисекунд ожидания, <see cref="Timeout.Infinite"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(int timeout, CancellationToken cancellationToken)
//    {
//        //  Проверка текущего объекта.
//        CheckDisposeCore();

//        //  Ожидание входа в семафор.
//        bool isLock = await _Semaphore.WaitAsync(timeout, cancellationToken).ConfigureAwait(false);

//        //  Проверка входа в семафор.
//        if (!isLock)
//        {
//            //  Проверка токена отмены.
//            cancellationToken.ThrowIfCancellationRequested();

//            //  Время ожидания операции истекло.
//            throw new TimeoutException();
//        }

//        //  Возврат блокировки.
//        return new AsyncLocking(() => _Semaphore.Release());
//    }

//    /// <summary>
//    /// Разрушает объект.
//    /// </summary>
//    public void Dispose()
//    {
//        //  Блокировка критического объекта.
//        lock (_SyncDispose)
//        {
//            //  Проверка значения, определяющего был ли разрушен объект.
//            if (!_IsDisposed)
//            {
//                //  Разрушение упрощенного семафора.
//                ((IDisposable)_Semaphore).Dispose();

//                //  Установка значения, определяющего был ли разрушен объект.
//                _IsDisposed = true;
//            }

//            //  Сообщение среде CLR, что она на не должна вызывать метод завершения для текущего экземпляра объекта.
//            GC.SuppressFinalize(this);
//        }
//    }

//    /// <summary>
//    /// Выполняет проверку разрушения объекта.
//    /// </summary>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private void CheckDisposeCore()
//    {
//        //  Блокировка критического объекта.
//        lock (_SyncDispose)
//        {
//            //  Проверка значения, определяющего был ли разрушен объект.
//            ObjectDisposedException.ThrowIf(_IsDisposed, nameof(AsyncLock));
//        }
//    }
//}



//using System;
//using System.Runtime.CompilerServices;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Ably.Marketing;

///// <summary>
///// Представляет асинхронный примитив взаимноисключающей блокировки.
///// </summary>
//public sealed class AsyncLock :
//    IDisposable
//{
//    /// <summary>
//    /// Поле для хранения упрощенного семафора.
//    /// </summary>
//    readonly SemaphoreSlim _Semaphore;

//    /// <summary>
//    /// Поле для хранения объекта, с помощью которого синхронизируется операция разрушения.
//    /// </summary>
//    readonly object _SyncDispose;

//    /// <summary>
//    /// Поле для хранения значения, определяющего был ли разрушен объект.
//    /// </summary>
//    volatile bool _IsDisposed;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    public AsyncLock()
//    {
//        //  Создание упрощённого семафора.
//        _Semaphore = new(1, 1);

//        //  Создание объекта, с помощью которого синхронизируется операция разрушения.
//        _SyncDispose = new();

//        //  Установка значения, определяющего был ли разрушен объект.
//        _IsDisposed = false;
//    }

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync() => await LockAsync(Timeout.Infinite, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(CancellationToken cancellationToken) => await LockAsync(Timeout.Infinite, cancellationToken);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Количество миллисекунд ожидания, <see cref="Timeout.Infinite"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(int timeout) => await LockAsync(timeout, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Время ожидания, <see cref="Timeout.InfiniteTimeSpan"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение в миллисекундах меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(TimeSpan timeout) => await LockAsync(timeout, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Время ожидания, <see cref="Timeout.InfiniteTimeSpan"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение в миллисекундах меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(TimeSpan timeout, CancellationToken cancellationToken) =>
//        await LockAsync(timeout, cancellationToken);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Количество миллисекунд ожидания, <see cref="Timeout.Infinite"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(int timeout, CancellationToken cancellationToken)
//    {
//        //  Проверка текущего объекта.
//        CheckDisposeCore();

//        //  Ожидание входа в семафор.
//        bool isLock = await _Semaphore.WaitAsync(timeout, cancellationToken).ConfigureAwait(false);

//        //  Проверка входа в семафор.
//        if (!isLock)
//        {
//            //  Проверка токена отмены.
//            cancellationToken.ThrowIfCancellationRequested();

//            //  Время ожидания операции истекло.
//            throw new TimeoutException();
//        }

//        //  Возврат блокировки.
//        return new AsyncLocking(() => _Semaphore.Release());
//    }

//    /// <summary>
//    /// Разрушает объект.
//    /// </summary>
//    public void Dispose()
//    {
//        //  Блокировка критического объекта.
//        lock (_SyncDispose)
//        {
//            //  Проверка значения, определяющего был ли разрушен объект.
//            if (!_IsDisposed)
//            {
//                //  Разрушение упрощенного семафора.
//                ((IDisposable)_Semaphore).Dispose();

//                //  Установка значения, определяющего был ли разрушен объект.
//                _IsDisposed = true;
//            }

//            //  Сообщение среде CLR, что она на не должна вызывать метод завершения для текущего экземпляра объекта.
//            GC.SuppressFinalize(this);
//        }
//    }

//    /// <summary>
//    /// Выполняет проверку разрушения объекта.
//    /// </summary>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private void CheckDisposeCore()
//    {
//        //  Блокировка критического объекта.
//        lock (_SyncDispose)
//        {
//            //  Проверка значения, определяющего был ли разрушен объект.
//            ObjectDisposedException.ThrowIf(_IsDisposed, nameof(AsyncLock));
//        }
//    }
//}










//using System.Runtime.CompilerServices;

//namespace Aekmot.Concurrent;

///// <summary>
///// Представляет асинхронный примитив взаимноисключающей блокировки.
///// </summary>
//public sealed class AsyncLock :
//    IDisposable
//{
//    /// <summary>
//    /// Поле для хранения упрощенного семафора.
//    /// </summary>
//    readonly SemaphoreSlim _Semaphore;

//    /// <summary>
//    /// Поле для хранения объекта, с помощью которого синхронизируется операция разрушения.
//    /// </summary>
//    private readonly Lock _SyncDispose;

//    /// <summary>
//    /// Поле для хранения значения, определяющего был ли разрушен объект.
//    /// </summary>
//    volatile bool _IsDisposed;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    public AsyncLock()
//    {
//        //  Создание упрощённого семафора.
//        _Semaphore = new(1, 1);

//        //  Создание объекта, с помощью которого синхронизируется операция разрушения.
//        _SyncDispose = new();

//        //  Установка значения, определяющего был ли разрушен объект.
//        _IsDisposed = false;
//    }

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync() => await LockAsync(Timeout.Infinite, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(CancellationToken cancellationToken) => await LockAsync(Timeout.Infinite, cancellationToken);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Количество миллисекунд ожидания, <see cref="Timeout.Infinite"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(int timeout) => await LockAsync(timeout, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Время ожидания, <see cref="Timeout.InfiniteTimeSpan"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение в миллисекундах меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(TimeSpan timeout) => await LockAsync(timeout, CancellationToken.None);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Время ожидания, <see cref="Timeout.InfiniteTimeSpan"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение в миллисекундах меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(TimeSpan timeout, CancellationToken cancellationToken) =>
//        await LockAsync(timeout.Microseconds, cancellationToken);

//    /// <summary>
//    /// Асинхронно выполняет операцию блокировки.
//    /// </summary>
//    /// <param name="timeout">
//    /// Количество миллисекунд ожидания, <see cref="Timeout.Infinite"/> для неограниченного времени ожидания.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая операцию блокировки.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="timeout"/> передано
//    /// значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
//    /// </exception>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Время ожидания операции истекло.
//    /// </exception>
//    public async Task<AsyncLocking> LockAsync(int timeout, CancellationToken cancellationToken)
//    {
//        //  Проверка текущего объекта.
//        CheckDisposeCore();

//        //  Ожидание входа в семафор.
//        bool isLock = await _Semaphore.WaitAsync(timeout, cancellationToken).ConfigureAwait(false);

//        //  Проверка входа в семафор.
//        if (!isLock)
//        {
//            //  Проверка токена отмены.
//            cancellationToken.ThrowIfCancellationRequested();

//            //  Время ожидания операции истекло.
//            throw new TimeoutException();
//        }

//        //  Возврат блокировки.
//        return new AsyncLocking(() => _Semaphore.Release());
//    }

//    /// <summary>
//    /// Разрушает объект.
//    /// </summary>
//    public void Dispose()
//    {
//        //  Блокировка критического объекта.
//        lock (_SyncDispose)
//        {
//            //  Проверка значения, определяющего был ли разрушен объект.
//            if (!_IsDisposed)
//            {
//                //  Разрушение упрощенного семафора.
//                ((IDisposable)_Semaphore).Dispose();

//                //  Установка значения, определяющего был ли разрушен объект.
//                _IsDisposed = true;
//            }

//            //  Сообщение среде CLR, что она на не должна вызывать метод завершения для текущего экземпляра объекта.
//            GC.SuppressFinalize(this);
//        }
//    }

//    /// <summary>
//    /// Выполняет проверку разрушения объекта.
//    /// </summary>
//    /// <exception cref="ObjectDisposedException">
//    /// В результате операции произошло обращение к разрушенному объекту.
//    /// </exception>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private void CheckDisposeCore()
//    {
//        //  Блокировка критического объекта.
//        lock (_SyncDispose)
//        {
//            //  Проверка значения, определяющего был ли разрушен объект.
//            ObjectDisposedException.ThrowIf(_IsDisposed, nameof(AsyncLock));
//        }
//    }
//}



