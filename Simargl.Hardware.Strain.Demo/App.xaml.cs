using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Journaling;
using System.Collections.Concurrent;

namespace Simargl.Hardware.Strain.Demo;

/// <summary>
/// Представляет приложение.
/// </summary>
partial class App
{
    /// <summary>
    /// Поле для хранения очереди действий.
    /// </summary>
    private readonly ConcurrentQueue<Action> _Actions;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public App()
    {
        ThreadPool.SetMinThreads(1000, 1000);

        //  Создание источника токена отмены.
        CancellationTokenSource cancellationTokenSource = new();

        //  Блок перехвата всех исключений.
        try
        {
            //  Установка главного источника токена отмены.
            CancellationToken = cancellationTokenSource.Token;

            //  Добавление обработчика события завершения приложения.
            Exit += delegate (object sender, System.Windows.ExitEventArgs e)
            {
                //  Разрушение источника токена отмены.
                cancellationTokenSource.Dispose();
            };

            //  Создание очереди действий.
            _Actions = [];

            //  Создание таймера.
            System.Windows.Threading.DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromMilliseconds(100),
            };

            //  Добавление обработчика события таймера.
            timer.Tick += Timer_Tick;

            //  Запуск таймера.
            timer.Start();

            //  Добавление обработчика события завершения приложения.
            Exit += delegate (object sender, System.Windows.ExitEventArgs e)
            {
                //  Остановка таймера.
                DefyCritical(timer.Stop);
            };

            //  Создание очереди сообщений журнала.
            JournalRecords = [];

            //  Создание журнала.
            Journal = new(JournalRecords);

            //  Создание средства вызова методов в основном потоке.
            Invoker = new(_Actions.Enqueue, CancellationToken);

            //  Создание механизме поддержки.
            Keeper = new(CancellationToken);

            //  Инициализация основных компонентов.
            InitializeComponent();

            //  Запись в журнал.
            Journal.Add("Начало работы.");
        }
        catch
        {
            //  Разрушение источника токена отмены.
            cancellationTokenSource.Dispose();

            //  Повторный выброс исключения.
            throw;
        }
    }

    /// <summary>
    /// Возвращает главный источник токена отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Возвращает очередь сообщений журнала.
    /// </summary>
    public ConcurrentQueue<JournalRecord> JournalRecords { get; }

    /// <summary>
    /// Возвращает журнал.
    /// </summary>
    public Journal Journal { get; }

    /// <summary>
    /// Возвращает средство вызова методов в основном потоке.
    /// </summary>
    public Invoker Invoker { get; }

    /// <summary>
    /// Возвращает механизм поддержки.
    /// </summary>
    public Keeper Keeper { get; }

    /// <summary>
    /// Обрабатывает событие таймера.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Timer_Tick(object? sender, EventArgs e)
    {
        //  Разбор очереди действий.
        while (!CancellationToken.IsCancellationRequested &&
            _Actions.TryDequeue(out Action? action))
        {
            //  Проверка действия.
            if (action is not null)
            {
                //  Выполнение действия.
                DefyCritical(action);
            }
        }
    }
}
