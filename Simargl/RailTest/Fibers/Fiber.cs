using RailTest.Events;
using System;
using System.Security;
using System.Threading;

namespace RailTest.Fibers;

/// <summary>
/// Представляет точку входа для волокна.
/// </summary>
/// <param name="context">
/// Контекст волокна.
/// </param>
public delegate void FiberEntryPoint(FiberContext context);

/// <summary>
/// Представляет волокно.
/// </summary>
public class Fiber
{
    /// <summary>
    /// Поле для хранения организатора события <see cref="Started"/>.
    /// </summary>
    private readonly EventImplementer _StartedImplementer;

    /// <summary>
    /// Поле для хранения организатора события <see cref="Stopped"/>.
    /// </summary>
    private readonly EventImplementer _StoppedImplementer;

    /// <summary>
    /// Поле для хранения организатора события <see cref="Failed"/>.
    /// </summary>
    private readonly EventImplementer _FailedImplementer;

    /// <summary>
    /// Происходит при запуске волокна.
    /// </summary>
    public event EventHandler? Started
    {
        add
        {
            _StartedImplementer.AddHandler(value);
        }
        remove
        {
            _StartedImplementer.RemoveHandler(value);
        }
    }

    /// <summary>
    /// Происходит при остановке волокна.
    /// </summary>
    public event EventHandler? Stopped
    {
        add
        {
            _StoppedImplementer.AddHandler(value);
        }
        remove
        {
            _StoppedImplementer.RemoveHandler(value);
        }
    }

    /// <summary>
    /// Происходит в случае, если во время выполнения произошла ошибка.
    /// </summary>
    public event EventHandler? Failed
    {
        add
        {
            _FailedImplementer.AddHandler(value);
        }
        remove
        {
            _FailedImplementer.RemoveHandler(value);
        }
    }

    /// <summary>
    /// Поле для хранения точки входа волокна.
    /// </summary>
    private readonly FiberEntryPoint _EntryPoint;

    /// <summary>
    /// Поле для хранения контекст волокна.
    /// </summary>
    private readonly FiberContext _Context;

    /// <summary>
    /// Поле для хранения объекта, который используется для синхронизации доступа к потоку.
    /// </summary>
    private readonly object _SyncThread;

    /// <summary>
    /// Поле для хранения потока, в котором выполняется волокно.
    /// </summary>
    private Thread? _Thread;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="entryPoint">
    /// Точка входа волокна.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="entryPoint"/> передана пустая ссылка.
    /// </exception>
    public Fiber(FiberEntryPoint entryPoint) :
        this(entryPoint, string.Empty)
    {
        
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="entryPoint">
    /// Точка входа волокна.
    /// </param>
    /// <param name="name">
    /// Имя волокна.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="entryPoint"/> передана пустая ссылка.
    /// </exception>
    public Fiber(FiberEntryPoint entryPoint, string name) :
        this(entryPoint, name, ThreadPriority.Normal)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="entryPoint">
    /// Точка входа волокна.
    /// </param>
    /// <param name="name">
    /// Имя волокна.
    /// </param>
    /// <param name="priority">
    /// Значение, определяющее приоритет потока.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="entryPoint"/> передана пустая ссылка.
    /// </exception>
    public Fiber(FiberEntryPoint entryPoint, string name, ThreadPriority priority) :
        this(entryPoint, name, priority, true)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="entryPoint">
    /// Точка входа волокна.
    /// </param>
    /// <param name="name">
    /// Имя волокна.
    /// </param>
    /// <param name="priority">
    /// Значение, определяющее приоритет потока.
    /// </param>
    /// <param name="isBackground">
    /// Значение, определяющее является ли поток фоновым.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="entryPoint"/> передана пустая ссылка.
    /// </exception>
    public Fiber(FiberEntryPoint entryPoint, string name, ThreadPriority priority, bool isBackground)
    {
        if (entryPoint is null)
        {
            throw new ArgumentNullException(nameof(entryPoint), "Передана пустая ссылка.");
        }
        _EntryPoint = entryPoint;
        _Context = new FiberContext(name, priority, isBackground);
        _SyncThread = new object();
        _StartedImplementer = new EventImplementer(EventInvokeHandler);
        _StoppedImplementer = new EventImplementer(EventInvokeHandler);
        _FailedImplementer = new EventImplementer(EventInvokeHandler);
    }

    /// <summary>
    /// Возвращает значение, определяющее выполняется ли волокно.
    /// </summary>
    public bool IsWork => _Context.IsWork;

    /// <summary>
    /// Выполняет вызов обработчика события.
    /// </summary>
    /// <param name="handler">
    /// Обработчик события.
    /// </param>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void EventInvokeHandler(EventHandler handler, object? sender, EventArgs e)
    {
        try
        {
            handler(sender, e);
        }
        catch (Exception? ex)
        {
            Exception? exception = ex;
            while (ex is not null)
            {
                if (ex is ThreadAbortException)
                {
                    exception = null;
                    break;
                }
                ex = ex.InnerException;
            }
            if (exception is not null)
            {
                throw exception;
            }
        }
    }

    /// <summary>
    /// Возвращает исключение, которое произошло во время выполнения потока.
    /// </summary>
    public Exception Exception { get; private set; } = null!;

    /// <summary>
    /// Запускает работу волокна.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Волокно уже работает.
    /// </exception>
    public void Start()
    {
        lock (_SyncThread)
        {
            if (!_Context.IsWork)
            {
                _Context.IsWork = true;
                _Thread = new Thread(ThreadEntry)
                {
                    Name = _Context.Name,
                    IsBackground = _Context.IsBackground,
                    Priority = _Context.Priority
                };
                Exception = null!;
                _Context.StartTime = DateTime.Now;
                OnStarted(EventArgs.Empty);
                _Thread.Start();
            }
            else
            {
                throw new InvalidOperationException("Волокно уже работает.");
            }
        }
    }

    /// <summary>
    /// Останавливает работу волокна.
    /// </summary>
    /// <param name="timeout">
    /// Время ожидания завершения работы.
    /// </param>
    public void Stop(int timeout)
    {
        lock (_SyncThread)
        {
            if (_Context.IsWork)
            {
                _Context.IsWork = false;
                Thread? thread = _Thread;
                if (thread is not null)
                {
                    try
                    {
                        thread.Join(timeout);
                    }
                    catch (ThreadStateException)
                    {

                    }
                }
                thread = _Thread;
                if (thread is not null)
                {
                    try
                    {
#pragma warning disable SYSLIB0006 // Тип или член устарел
                        thread.Abort();
#pragma warning restore SYSLIB0006 // Тип или член устарел
                    }
                    catch (PlatformNotSupportedException)
                    {

                    }
                    catch (SecurityException)
                    {

                    }
                    catch (ThreadStateException)
                    {

                    }
                }
                _Thread = null;
            }
        }
    }

    /// <summary>
    /// Представляет точку входа приложения.
    /// </summary>
    private void ThreadEntry()
    {
        try
        {
            _EntryPoint(_Context);
        }
        catch (Exception? ex)
        {
            Exception = ex;
            while (ex is not null)
            {
                if (ex is ThreadAbortException)
                {
                    Exception = null!;
                    break;
                }
                ex = ex.InnerException;
            }
        }
        finally
        {
            _Thread = null;
            _Context.IsWork = false;
            if (Exception is null)
            {
                OnStopped(EventArgs.Empty);
            }
            else
            {
                OnFailed(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="Started"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnStarted(EventArgs e)
    {
        _StartedImplementer.RaiseEvent(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="Stopped"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnStopped(EventArgs e)
    {
        _StoppedImplementer.RaiseEvent(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="Failed"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnFailed(EventArgs e)
    {
        _FailedImplementer.RaiseEvent(this, e);
    }
}
