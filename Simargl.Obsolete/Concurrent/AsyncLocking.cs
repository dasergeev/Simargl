namespace Simargl.Concurrent;

/// <summary>
/// Представляет блокировку объекта <see cref="AsyncLock"/>.
/// </summary>
public sealed class AsyncLocking :
    IDisposable,
    IAsyncDisposable
{
    /// <summary>
    /// Поле для хранения действия, которое необходимо выполнить для снятия блокировки объекта.
    /// </summary>
    readonly Action _Unlock;

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
    /// <param name="unlock">
    /// Действие, которое необходимо выполнить для снятия блокировки объекта.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="unlock"/> передана пустая ссылка.
    /// </exception>
    internal AsyncLocking(Action unlock)
    {
        //  Установка действия.
        _Unlock = unlock;

        //  Создание объекта, с помощью которого синхронизируется операция разрушения.
        _SyncDispose = new();

        //  Установка значения, определяющего был ли разрушен объект.
        _IsDisposed = false;
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
                //  Снятие блокировки объекта.
                _Unlock();

                //  Установка значения, определяющего был ли разрушен объект.
                _IsDisposed = true;
            }

            //  Сообщение среде CLR, что она на не должна вызывать метод завершения для текущего экземпляра объекта.
            GC.SuppressFinalize(this);
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await ValueTask.CompletedTask.ConfigureAwait(false);
        Dispose();
    }
}












//using System;

//namespace Ably.Concurrent;

///// <summary>
///// Представляет блокировку объекта <see cref="AsyncLock"/>.
///// </summary>
//public sealed class AsyncLocking :
//    IDisposable
//{
//    /// <summary>
//    /// Поле для хранения действия, которое необходимо выполнить для снятия блокировки объекта.
//    /// </summary>
//    readonly Action _Unlock;

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
//    /// <param name="unlock">
//    /// Действие, которое необходимо выполнить для снятия блокировки объекта.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="unlock"/> передана пустая ссылка.
//    /// </exception>
//    internal AsyncLocking(Action unlock)
//    {
//        //  Установка действия.
//        _Unlock = unlock;

//        //  Создание объекта, с помощью которого синхронизируется операция разрушения.
//        _SyncDispose = new();

//        //  Установка значения, определяющего был ли разрушен объект.
//        _IsDisposed = false;
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
//                //  Снятие блокировки объекта.
//                _Unlock();

//                //  Установка значения, определяющего был ли разрушен объект.
//                _IsDisposed = true;
//            }

//            //  Сообщение среде CLR, что она на не должна вызывать метод завершения для текущего экземпляра объекта.
//            GC.SuppressFinalize(this);
//        }
//    }
//}








//using System;

//namespace Ably.Marketing;

///// <summary>
///// Представляет блокировку объекта <see cref="AsyncLock"/>.
///// </summary>
//public sealed class AsyncLocking :
//    IDisposable
//{
//    /// <summary>
//    /// Поле для хранения действия, которое необходимо выполнить для снятия блокировки объекта.
//    /// </summary>
//    readonly Action _Unlock;

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
//    /// <param name="unlock">
//    /// Действие, которое необходимо выполнить для снятия блокировки объекта.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="unlock"/> передана пустая ссылка.
//    /// </exception>
//    internal AsyncLocking(Action unlock)
//    {
//        //  Установка действия.
//        _Unlock = unlock;

//        //  Создание объекта, с помощью которого синхронизируется операция разрушения.
//        _SyncDispose = new();

//        //  Установка значения, определяющего был ли разрушен объект.
//        _IsDisposed = false;
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
//                //  Снятие блокировки объекта.
//                _Unlock();

//                //  Установка значения, определяющего был ли разрушен объект.
//                _IsDisposed = true;
//            }

//            //  Сообщение среде CLR, что она на не должна вызывать метод завершения для текущего экземпляра объекта.
//            GC.SuppressFinalize(this);
//        }
//    }
//}










//namespace Aekmot.Concurrent;

///// <summary>
///// Представляет блокировку объекта <see cref="AsyncLock"/>.
///// </summary>
//public sealed class AsyncLocking :
//    IDisposable
//{
//    /// <summary>
//    /// Поле для хранения действия, которое необходимо выполнить для снятия блокировки объекта.
//    /// </summary>
//    readonly Action _Unlock;

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
//    /// <param name="unlock">
//    /// Действие, которое необходимо выполнить для снятия блокировки объекта.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="unlock"/> передана пустая ссылка.
//    /// </exception>
//    internal AsyncLocking(Action unlock)
//    {
//        //  Установка действия.
//        _Unlock = unlock;

//        //  Создание объекта, с помощью которого синхронизируется операция разрушения.
//        _SyncDispose = new();

//        //  Установка значения, определяющего был ли разрушен объект.
//        _IsDisposed = false;
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
//                //  Снятие блокировки объекта.
//                _Unlock();

//                //  Установка значения, определяющего был ли разрушен объект.
//                _IsDisposed = true;
//            }

//            //  Сообщение среде CLR, что она на не должна вызывать метод завершения для текущего экземпляра объекта.
//            GC.SuppressFinalize(this);
//        }
//    }
//}


