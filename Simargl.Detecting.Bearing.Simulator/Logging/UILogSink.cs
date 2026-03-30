using System.ComponentModel;

namespace Simargl.Detecting.Bearing.Simulator.Logging;

/// <summary>
/// Представляет хранилище сообщений журнала, используемое для отображения UI.
/// </summary>
public sealed class UILogSink :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения аргументов события изменения значения свойства <see cref="Text"/>.
    /// </summary>
    private static readonly PropertyChangedEventArgs _TextChangedEventArgs = new(nameof(Text));

    /// <summary>
    /// Поле для хранения диспетчера приложения.
    /// </summary>
    private readonly Dispatcher _Dispatcher;

    /// <summary>
    /// Поле для хранения токена отмены.
    /// </summary>
    private readonly CancellationToken _CancellationToken;

    /// <summary>
    /// Поле для хранения очереди сообщений.
    /// </summary>
    private readonly ConcurrentQueue<string> _Queue;

    /// <summary>
    /// Поле для хранения списка сообщений.
    /// </summary>
    private readonly List<string> _Messages;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public UILogSink()
    {
        //  Получение приложения.
        App app = (App)Application.Current;

        //  Получение диспетчера приложения.
        _Dispatcher = app.Dispatcher;

        //  Получение токена отмены.
        _CancellationToken = app.CancellationToken;

        //  Создание очереди сообщений.
        _Queue = [];

        //  Создание списка сообщений.
        _Messages = [];

        //  Установка текста по умолчанию.
        Text = string.Empty;

        //  Запуск асинхронной задачи.
        _ = Task.Run(InvokeAsync, _CancellationToken);
    }

    /// <summary>
    /// Возвращает текущий текст.
    /// </summary>
    public string Text { get; private set; }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    private async Task InvokeAsync()
    {
        //  Получение токена отмены.
        CancellationToken cancellationToken = _CancellationToken;

        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Основной цикл работы.
                while (!cancellationToken.IsCancellationRequested)
                {
                    //  Флаг изменения.
                    bool isChanged = false;

                    //  Извлечение сообщений из очереди.
                    while (
                        _Queue.TryDequeue(out string? message) &&
                        !cancellationToken.IsCancellationRequested)
                    {
                        //  Проверка сообщения.
                        if (message is not null)
                        {
                            //  Добавление сообщения в список.
                            _Messages.Add(message);

                            //  Установка флага изменений.
                            isChanged = true;
                        }
                    }

                    // Проверка превышения размера списка.
                    if (_Messages.Count > 512)
                    {
                        // Удаление всех элементов сверх допустимого размера.
                        _Messages.RemoveRange(0, _Messages.Count - 512);
                    }

                    //  Проверка флага изменений.
                    if (isChanged)
                    {
                        //  Создание построителя строки.
                        StringBuilder builder = new(_Messages.Count * 64);

                        //  Перебор сообщений.
                        for (int i = _Messages.Count - 1; i >= 0; i--)
                        {
                            //  Добавление сообщения.
                            builder.AppendLine(_Messages[i]);
                        }

                        //  Изменение текста.
                        Text = builder.ToString();

                        //  Выполение в основном потоке.
                        await _Dispatcher.InvokeAsync(delegate
                        {
                            //  Вызов собятия.
                            Volatile.Read(ref PropertyChanged)?.Invoke(this, _TextChangedEventArgs);
                        }, DispatcherPriority.DataBind, cancellationToken);
                    }

                    //  Ожидание перед следующим проходом.
                    await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
                }
            }
            catch
            {
                //  Проверка токена отмены.
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Повторный выброс исключения.
                    throw;
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Добавляет сообщение в журнал.
    /// </summary>
    public void Add(string message)
    {
        //  Добавление сообщения в очередь.
        _Queue.Enqueue(message);
    }
}
