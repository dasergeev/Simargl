using Apeiron.Platform.Demo.AdxlDemo.Adxl;
using Apeiron.Platform.Demo.AdxlDemo.Database;
using Apeiron.Platform.Demo.AdxlDemo.Logging;
using Apeiron.Threading;
using System.Collections.Concurrent;

namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет основной активный объект.
/// </summary>
public sealed class Engine
{
    /// <summary>
    /// Постоянная, определяющая задержку перед следующим шагом цикла основной задачи.
    /// </summary>
    private const int _MainTaskDelay = 100;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private readonly CancellationTokenSource _CancellationTokenSource;

    /// <summary>
    /// Поле для хранения основной задачи.
    /// </summary>
    private Task? _MainTask;

    /// <summary>
    /// Поле для хранения очереди асинхронных действий.
    /// </summary>
    private readonly ConcurrentQueue<AsyncAction> _AsyncActions;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="application">
    /// Текущее приложение.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="application"/> передана пустая ссылка.
    /// </exception>
    public Engine(App application)
    {
        //  Установка текущего приложения.
        Application = IsNotNull(application, nameof(application));

        //  Создание источника токена отмены.
        _CancellationTokenSource = new();

        //  Установка токена отмены.
        CancellationToken = _CancellationTokenSource.Token;

        //  Установка основной задачи.
        _MainTask = null;

        //  Создание очереди асинхронных действий.
        _AsyncActions = new();

        //  Создание средства вызова методов.
        Invoker = new(this);

        //  Создание синхронизатора.
        Synchronizer = new(this);

        //  Создание управляющего контекстом базы данных.
        ContextManager = new();

        //  Создание средства ведения журнала.
        Logger = new(this, application.UserInterfaceData.LogMessages);

        //  Создание корневого узла приложения.
        Root = new(this);

        //  Создание приёмника пакетов датчиков ADXL357.
        Receiver = new(this);

        //  Создание диспетчера пакетов.
        PackageManager = new(this);

        //  Добавление обработчика запуска приложения.
        Application.Startup += (sender, e) =>
        {
            //  Запуск основной задачи.
            _MainTask = Task.Run(async delegate
            {
                //  Асинхронное выполнение основной задачи.
                await InvokeAsync(_CancellationTokenSource.Token);
            });

            //  Запуск синхронизатора.
            Synchronizer.Start();
        };

        //  Добавление обработчика события завершения приложения.
        Application.Exit += (sender, e) =>
        {
            //  Остановка синхронизатора.
            Synchronizer.Stop();

            //  Активация токена отмены.
            _CancellationTokenSource.Cancel();

            //  Ожидание завершения основной задачи.
            try
            {
                _MainTask?.Wait();
            }
            catch
            {

            }

            //  Разрушение управляющего контекстом базы данных.
            ContextManager.Dispose();
        };
    }

    /// <summary>
    /// Возвращает токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Возвращает синхронизатор.
    /// </summary>
    public Synchronizer Synchronizer { get; }

    /// <summary>
    /// Возвращает текущее приложение.
    /// </summary>
    public App Application { get; }

    /// <summary>
    /// Возвращает общие настройки приложения.
    /// </summary>
    public Settings Settings => Application.Settings;

    /// <summary>
    /// Возвращает средство вызова методов.
    /// </summary>
    public Invoker Invoker { get; }

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    public Logger Logger { get; }

    /// <summary>
    /// Возвращает корневой узел приложения.
    /// </summary>
    public Root Root { get; }

    /// <summary>
    /// Возвращает приёмник пакетов датчиков ADXL357.
    /// </summary>
    public AdxlReceiver Receiver { get; }

    /// <summary>
    /// Возвращает диспетчера пакетов.
    /// </summary>
    public AdxlPackageManager PackageManager { get; }

    /// <summary>
    /// Возвращает управляющего контекстом базы данных.
    /// </summary>
    public ContextManager ContextManager { get; }

    /// <summary>
    /// Прикрепление асинхронного действия.
    /// </summary>
    /// <param name="asyncAction">
    /// Асинхронное действие.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="asyncAction"/> передана пустая ссылка.
    /// </exception>
    public void Attach(AsyncAction asyncAction)
    {
        //  Проверка ссылки на действие.
        IsNotNull(asyncAction, nameof(asyncAction));

        //  Добавление действия в очередь.
        _AsyncActions.Enqueue(asyncAction);
    }

    /// <summary>
    /// Асинхронно выполняет основную задачу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Основная задача.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Безопасное выполнение.
            await Invoker.CriticalAsync(async (cancellationToken) =>
            {
                //  Вывод сообщения в журнал.
                await Logger.LogAsync("Запуск основной задачи.", cancellationToken).ConfigureAwait(false);

                //  Основной цикл работы.
                while (!cancellationToken.IsCancellationRequested)
                {
                    //  Извлечение асинхронных действий.
                    while (_AsyncActions.TryDequeue(out AsyncAction? asyncAction) && !cancellationToken.IsCancellationRequested)
                    {
                        //  Запуск действия.
                        _ = Task.Run(async delegate
                        {
                            //  Проверка токена отмены.
                            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                            //  Запуск действия.
                            await asyncAction(cancellationToken).ConfigureAwait(false);
                        }, cancellationToken).ConfigureAwait(false);
                    }

                    //  Ожидание перед следующим шагом.
                    await Task.Delay(_MainTaskDelay, cancellationToken);
                }
            }, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед следующим шагом.
            await Task.Delay(_MainTaskDelay, cancellationToken);
        }
    }
}
