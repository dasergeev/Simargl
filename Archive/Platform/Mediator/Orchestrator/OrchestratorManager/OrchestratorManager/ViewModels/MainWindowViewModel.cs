using Apeiron.Platform.MediatorLibrary;
using Apeiron.Platform.MediatorLibrary.Responce;
using Apeiron.Platform.OrchestratorManager.Commands;
using Apeiron.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Apeiron.Platform.OrchestratorManager.ViewModels;

/// <summary>
/// Модель представление для окна MainWindow.
/// </summary>
public sealed class MainWindowViewModel : ViewModel, IDisposable
{
    // Поддерживающие поля для свойств.
    private string? _Title;
    private ObservableCollection<HostEntityRowGrid> _HostCollection;
    private string? _CommandsLog;
    private string? _ResultsLog;
    private HostEntityRowGrid? _HostRow;

    // Точка подключения к серверу.
    private readonly IPEndPoint _IPEndPoint;

    /// <summary>
    /// Содержит источник для токена отмены текущего окна.
    /// </summary>
    private readonly CancellationTokenSource _LocalCancellationTokenSource;

    /// <summary>
    /// Содержит заголовок окна.
    /// </summary>
    public string Title
    {
        get => _Title ?? $"{Assembly.GetExecutingAssembly().GetName().Name}";
        set => Set(ref _Title, value);
    }

    /// <summary>
    /// Содержит команды строку лог команд.
    /// </summary>
    public string CommandsLog
    {
        get => _CommandsLog ?? string.Empty;
        set => Set(ref _CommandsLog, value);
    }

    /// <summary>
    /// Содержит лог результатов выполнения.
    /// </summary>
    public string ResultsLog
    {
        get => _ResultsLog ?? string.Empty;
        set => Set(ref _ResultsLog, value);
    }

    /// <summary>
    /// Представляет коллекцию данных для DataGrid.
    /// </summary>
    public ObservableCollection<HostEntityRowGrid> HostCollection
    {
        get => _HostCollection;
        set => Set(ref _HostCollection, value);
    }

    /// <summary>
    /// Содержит команды строку лог команд.
    /// </summary>
    public HostEntityRowGrid HostRow
    {
        get => _HostRow!;
        set => Set(ref _HostRow, value);
    }

    /// <summary>
    /// Свойство возвращающее команду закрытия окна.
    /// </summary>
    public ICommand CloseApplicationCommand { get; }

    /// <summary>
    /// Свойство возвращающее команду получения информации о хосте.
    /// </summary>
    public ICommand GetHostInfo { get; }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public MainWindowViewModel()
    {
        // Создание команд.
        CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        GetHostInfo = new RelayCommand(OnGetHostInfoExecute, CanGetHostInfoExecute);


        // Создаёт локальный токен отмены.
        _LocalCancellationTokenSource = new CancellationTokenSource();
        // Инициализирует коллекцию.
        _HostCollection = new ObservableCollection<HostEntityRowGrid>();
        // Инициализирует настройки подключения к серверу Mediator.
        _IPEndPoint = new(MediatorSettings.MediatorServerAddress, MediatorSettings.MediatorServerPort);


        //Включает синхронизированный доступ к коллекции, используемой в нескольких потоках.
        var lockObject = new object();
        BindingOperations.EnableCollectionSynchronization(HostCollection, lockObject);

        // Запуск задачи обновления данных хостов в DataGrid.
        Task LoopUpdateHostCollection = Task.Run(async () => await LoopUpdateHostViewModelCollectionAsync(_LocalCancellationTokenSource.Token));
    }


    /// <summary>
    /// Запускает переодическое обновление таблицы с хостами.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    private async Task LoopUpdateHostViewModelCollectionAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // Очистка коллекции.
            HostCollection.Clear();

            // Вывод лога команд в текстовое поле.
            CommandsLog = CommandsLog + nameof(MediatorMethodId.GetHostListFromMediatorServer) + "\r";

            List<HostInfo>? result = await MediatorTcpClient.RemoteMethodCallAsync(_IPEndPoint,
            MediatorMethodId.GetHostListFromMediatorServer,
            async (writer, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                int count = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);
                List<HostInfo> hostList = new();
                for (int i = 0; i < count; i++)
                {
                    hostList.Add(await new HostInfo().LoadAsync(spreader.Stream, cancellationToken).ConfigureAwait(false));
                }

                return hostList;
            }, cancellationToken);

            // Вывод результатов в окно вывода.
            if (result is not null)
            {
                foreach (var item in result)
                {
                    HostCollection.Add(new HostEntityRowGrid()
                    {
                        Hostname = item.Hostname,
                        RegTime = item.RequestTime
                    });

                    // Вывод результатов выполнения команд в текстовое поле.
                    ResultsLog = ResultsLog + item.Hostname + " : " + item.RequestTime.ToString() + "\r";
                }
            }



            // Задержка перед следующей итерацией цикла.
            await Task.Delay(6000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Возможность выполнения команды.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    /// <returns>Возвращает доступность выполнения команды - True или False.</returns>
    private bool CanGetHostInfoExecute(object? parameter) => true;

    /// <summary>
    /// Выполняет запрос и получение результатов выполнения команды получения данных о хосте.
    /// </summary>
    /// <param name="parameter">Параметр</param>
#pragma warning disable VSTHRD100 // Avoid async void methods
    private async void OnGetHostInfoExecute(object? parameter)
#pragma warning restore VSTHRD100 // Avoid async void methods
    {
        // Вывод лога команд в текстовое поле.
        CommandsLog = CommandsLog + nameof(MediatorMethodId.GetHostInfo) + "\r";

        var result = await MediatorTcpClient.RemoteMethodCallAsync(_IPEndPoint,
            MediatorMethodId.GetHostInfo,
            async (spreader, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);
                await spreader.WriteStringAsync(HostRow.Hostname, cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                // Токен отмены
                CancellationTokenSource timeOutCommandCancellationTokenSource = new();

                // Соединяет локальный токен отмены с токеном отмены уровня приложения.
                CancellationTokenSource combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(timeOutCommandCancellationTokenSource.Token, cancellationToken);

                // Отмена по таймеру
                timeOutCommandCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(3));


                HostInfo hostInfo = await new HostInfo().LoadAsync(spreader.Stream, combinedTokenSource.Token).ConfigureAwait(false);
                // await reader.ReadObjectAsync<HostInfo>(combinedTokenSource.Token).ConfigureAwait(false);


                return hostInfo;
            }, _LocalCancellationTokenSource.Token);

    }


    /// <summary>
    /// Возможность выполнения команды закрытия окна.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    /// <returns>Возвращает доступность выполнения команды - True или False.</returns>
    private bool CanCloseApplicationCommandExecute(object? parameter) => true;

    /// <summary>
    /// Выполняет команду закрытия окна.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    private void OnCloseApplicationCommandExecuted(object? parameter) => Application.Current.Shutdown();

    /// <summary>
    /// Деструктор класса.
    /// </summary>
    public void Dispose()
    {
        _LocalCancellationTokenSource?.Dispose();
    }
}
