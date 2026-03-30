using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace Simargl.Engineering.Utilities.StampWriter;

/// <summary>
/// Представляет сердце приложения.
/// </summary>
public sealed class Heart :
    INotifyPropertyChanged,
    IDisposable
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения метода, выполняющего действие в основном потоке.
    /// </summary>
    private readonly Func<Action, DispatcherOperation> _Invoker;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private readonly CancellationTokenSource _CancellationTokenSource;

    /// <summary>
    /// Поле для хранения флага запуска.
    /// </summary>
    private bool _IsStarted = false;

    /// <summary>
    /// Поле для хранения флага остановки.
    /// </summary>
    private bool _IsStoped = true;

    /// <summary>
    /// Поле для хранения флага отображения штампа.
    /// </summary>
    private Visibility _StampViewVisibility = Visibility.Visible;

    /// <summary>
    /// Поле для хранения флага отображения журнала.
    /// </summary>
    private Visibility _JournalViewVisibility = Visibility.Collapsed;

    /// <summary>
    /// Поле для хранения очереди сообщений.
    /// </summary>
    private readonly ConcurrentQueue<string> _Queue;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="invoker">
    /// Метод, выполняющей действие в основном потоке.
    /// </param>
    public Heart(Func<Action, DispatcherOperation> invoker)
    {
        //  Установка метода, выполняющего действие в основном потоке.
        _Invoker = invoker;

        //  Создание источника токена отмены.
        _CancellationTokenSource = new();

        //  Установка токена отмеы.
        CancellationToken = _CancellationTokenSource.Token;

        //  Создание очереди сообщений.
        _Queue = [];

        //  Создание коллекции сообщений.
        Messages = [];

        //  Запуск асинхронной работы с сообщениями.
        _ = Task.Run(MessageAsync);
    }

    /// <summary>
    /// Возвращает токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Возвращает коллекцию сообщений.
    /// </summary>
    public ObservableCollection<string> Messages { get; }

    /// <summary>
    /// Возвращает или задаёт флаг запуска.
    /// </summary>
    public bool IsStarted
    {
        get => _IsStarted;
        set
        {
            //  Проверка изменения значения.
            if (_IsStarted != value)
            {
                //  Установка нового значения.
                _IsStarted = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(IsStarted)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт флаг остановки.
    /// </summary>
    public bool IsStoped
    {
        get => _IsStoped;
        set
        {
            //  Проверка изменения значения.
            if (_IsStoped != value)
            {
                //  Установка нового значения.
                _IsStoped = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(IsStoped)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт флаг отображения штампа.
    /// </summary>
    public Visibility StampViewVisibility
    {
        get => _StampViewVisibility;
        set
        {
            //  Проверка изменения значения.
            if (_StampViewVisibility != value)
            {
                //  Установка нового значения.
                _StampViewVisibility = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(StampViewVisibility)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт флаг отображения журнала.
    /// </summary>
    public Visibility JournalViewVisibility
    {
        get => _JournalViewVisibility;
        set
        {
            //  Проверка изменения значения.
            if (_JournalViewVisibility != value)
            {
                //  Установка нового значения.
                _JournalViewVisibility = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(JournalViewVisibility)));
            }
        }
    }

    /// <summary>
    /// Добавляет сообщение в очередь.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо добавить в очередь.
    /// </param>
    public void Log(string message)
    {
        //  Добавление сообщения в очередь.
        _Queue.Enqueue(message);
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    void IDisposable.Dispose()
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Отправка запроса на отмену.
            _CancellationTokenSource.Cancel();
        }
        catch { }

        //  Блок перехвата всех исключений.
        try
        {
            //  Разрушение источника токена отмены.
            _CancellationTokenSource.Dispose();
        }
        catch { }
    }

    /// <summary>
    /// Выполняет асинхронную работу с сообщениями.
    /// </summary>
    /// <returns>
    /// Асинхронная работа с сообщениями.
    /// </returns>
    private async Task MessageAsync()
    {
        //  Получение токена отмены.
        CancellationToken cancellationToken = CancellationToken;

        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Извлечение сообщений из очереди.
                while (
                    _Queue.TryDequeue(out string? message) &&
                    !cancellationToken.IsCancellationRequested)
                {
                    //  Проверка сообщения.
                    if (message is not null)
                    {
                        //  Блок перехвата всех исключений.
                        try
                        {
                            //  Выполнение в основном потоке.
                            await InvokeAsync(delegate
                            {
                                //  Добавление сообщения в коллекцию.
                                Messages.Insert(0, message);
                            }, cancellationToken).ConfigureAwait(false);
                        }
                        catch { }
                    }
                }
            }
            catch { }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет действие в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие в основном потоке.
    /// </returns>
    public async Task InvokeAsync(Action action, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        CancellationToken.ThrowIfCancellationRequested();

        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Исключение.
        Exception? exception = null;

        //  Выполнение с основном потоке.
        await _Invoker(delegate
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение действия.
                action();
            }
            catch (Exception ex)
            {
                //  Установка исключения.
                exception = ex;
            }
        });

        //  Проверка исключения.
        if (exception is not null)
        {
            //  Выброс исключения.
            throw exception;
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref PropertyChanged)?.Invoke(this, e);
    }
}
