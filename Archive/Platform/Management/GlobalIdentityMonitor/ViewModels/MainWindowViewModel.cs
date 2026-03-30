using Apeiron.Services.GlobalIdentity.Commands;
using Apeiron.Services.GlobalIdentity.Content;
using Apeiron.Services.GlobalIdentity.Tunings;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Apeiron.Services.GlobalIdentity.ViewModels;

/// <summary>
/// Представляет модель представление для окна MainWindow.
/// </summary>
public sealed class MainWindowViewModel : ViewModel, IDisposable
{
    #region Свойства

    // Поддерживающие свойства поля.
    private string? _Title;
    private string? _ProgramVersion;
    private string? _DbName;
    private string? _DbStatusNotification;
    private Color _Color;
    private ObservableCollection<EntityRowGrid> _ViewModelCollection;

    /// <summary>
    /// Содержит источник для токена отмены текущего окна.
    /// </summary>
    private CancellationTokenSource _LocalCancellationTokenSource;

    /// <summary>
    /// Представляет токен отмены всего приложения.
    /// </summary>
    private readonly CancellationToken _GlobalToken;

    /// <summary>
    /// Содержит объект для работы с данными из БД и взаимодействия с DataContext.
    /// </summary>
    private static DataService DataService => new();

    /// <summary>
    /// Содержит настройки программы, получаемые из файла Json.
    /// </summary>
    public AppTuning AppTuning { get; }


    /// <summary>
    /// Содержит заголовок окна.
    /// </summary>
    public string Title
    {
        get => _Title ?? $"{Assembly.GetExecutingAssembly().GetName().Name}";
        set => Set(ref _Title, value);
    }

    /// <summary>
    /// Содержит версию программы.
    /// </summary>
    public string ProgramVersion
    {
        get => _ProgramVersion ?? $"Ver. {Assembly.GetExecutingAssembly().GetName().Version}";
        set => Set(ref _ProgramVersion, value);
    }

    /// <summary>
    /// Содержит имя базы данных.
    /// </summary>
    public string DbName
    {
        get => _DbName ?? string.Empty;
        set => Set(ref _DbName, value);
    }

    /// <summary>
    /// Содержит сообщение об успешном или не успешном подключении к БД в строке состоянии.
    /// </summary>
    public string DbStatusNotification
    {
        get => _DbStatusNotification ?? string.Empty;
        set => Set(ref _DbStatusNotification, value);
    }

    /// <summary>
    /// Цвет заливки.
    /// </summary>
    public Color Color
    {
        get => _Color;
        set => Set(ref _Color, value);
    }

    /// <summary>
    /// Представляет коллекцию данных для DataGrid.
    /// </summary>
    public ObservableCollection<EntityRowGrid> ViewModelCollection
    {
        get => _ViewModelCollection;
        set => Set(ref _ViewModelCollection, value);
    }

    /// <summary>
    /// Свойство возвращающее команду закрытия окна.
    /// </summary>
    public ICommand CloseApplicationCommand { get; }

    /// <summary>
    /// Свойство возвращающее команду ручного обновления данных в DataGrid из БД.
    /// </summary>
    public ICommand ManualUpdateData { get; }

    /// <summary>
    /// Отсанавливает опрос базы данных и обновления UI.
    /// </summary>
    public ICommand StopUpdateData { get; }

    /// <summary>
    /// Запускает задачу опроса базы данных и обновления UI.
    /// </summary>
    public ICommand StartUpdateData { get; }

    #endregion

    /// <summary>
    /// Инициализирует класс.
    /// </summary>
    public MainWindowViewModel()
    {      
        // Создаёт команду.
        CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        ManualUpdateData = new RelayCommand(OnManualUpdateDataCommandExecute, CanManualUpdateDataCommandExecute);
        StopUpdateData = new RelayCommand(OnStopUpdateDataCommandExecute, CanStopUpdateDataCommandExecute);
        StartUpdateData = new RelayCommand(OnStartUpdateDataCommandExecute, CanStartUpdateDataCommandExecute);
  
        // Создаёт объект в котором храняться настройки программы и загружаем настройки из файла.            
        AppTuning = AppTuning.Instance.Load();

        // Получает имя БД.
        _DbName = DataService.DbName;

        // Задаёт цвет по умолчанию.
        _Color = Colors.Transparent;

        // Инициализирует коллекцию.
        _ViewModelCollection = new();

        //Включает синхронизированный доступ к коллекции, используемой в нескольких потоках.
        var lockObject = new object();
        BindingOperations.EnableCollectionSynchronization(ViewModelCollection, lockObject);

        // Создаёт локальный токен отмены.
        _LocalCancellationTokenSource = new CancellationTokenSource();

        // Получает глобальный токен отмены.
        _GlobalToken = App.Current.Properties["GlobalToken"] is CancellationToken globalToken ? globalToken : CancellationToken.None;

        // Соединяет локальный токен отмены с токеном отмены уровня приложения.
        var combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_GlobalToken, _LocalCancellationTokenSource.Token);
        
        // Запуск задачи обновления данных.
        Task LoopGetViewTask = Task.Run(async () => await LoopGetViewModelCollectionAsync(combinedTokenSource.Token));
        // Задача разрущения токена отмены после заверщения задачи.
        Task taskDisposeToken = LoopGetViewTask.ContinueWith(_ => combinedTokenSource.Dispose(), TaskScheduler.Default);
    }

    /// <summary>
    /// Запускает переодический опрос БД и обновление коллекции для UI.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    private async Task LoopGetViewModelCollectionAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // Проверка подключения к БД.
            var dbStatus = await DataService.DbStatusDisplayAsync(cancellationToken);

            // Обновляем сообщение о статусе БД.
            DbStatusNotification = dbStatus.Item2;

            if (dbStatus.Item1)
            {
                Color = Colors.DarkGreen;
                // Обновляем коллекцию данными из БД.
                await GetViewModelCollectionAsync(cancellationToken);
            }
            else
            {
                Color = Colors.Red;
                // Очистка коллекции перед заполнением.
                ViewModelCollection.Clear();
            }

            // Задержка перед следующей итерацией цикла.
            await Task.Delay(AppTuning.MainTuning.RefreshPeriod, cancellationToken);
        }
    }

    /// <summary>
    /// Обновляет коллекцию.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    private async Task GetViewModelCollectionAsync(CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Очистка коллекции перез заполнением.
        ViewModelCollection.Clear();

        // Получение данных из БД.
        var resultQuery = await DataService.DataLoadAsync(cancellationToken);

        if (resultQuery?.Length > 0)
            foreach (var itemSystem in resultQuery)
            {
                ViewModelCollection.Add(new EntityRowGrid()
                {
                    Name = itemSystem.Name,
                    ReceiptTime = itemSystem.LastTime,
                    PacketTime = itemSystem.LastPacketTime,
                    Value = itemSystem.Value,
                    LastAddress = itemSystem.LastAddress,
                    LastPort = itemSystem.LastPort,
                    LastVersion = itemSystem.LastVersion
                });
            }
    }


    #region Команды

    /// <summary>
    /// Возможность выполнения команды остановки опроса БД и обновления UI.
    /// </summary>
    /// <param name="parameter">Параметр</param>
    /// <returns>Возвращает доступность выполнения команды - True или False.</returns>
    private bool CanStopUpdateDataCommandExecute(object? parameter) => true;

    /// <summary>
    /// Выполняет команду остановки опроса БД и обновления UI.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    private void OnStopUpdateDataCommandExecute(object? parameter)
    {
        // Отправляем Отмену локальному токену отмены для остановки выполняемых задач.
        _LocalCancellationTokenSource.Cancel();
        // Очистка коллекции перед заполнением.
        ViewModelCollection.Clear();
    }

    /// <summary>
    /// Возможность запуска команды обновления данных.
    /// </summary>
    /// <param name="parameter">Параметр</param>
    /// <returns>Возвращает доступность выполнения команды - True или False.</returns>
    private bool CanStartUpdateDataCommandExecute(object? parameter) => true;

    /// <summary>
    /// Выполняет команду запуска опроса БД и обновления UI.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    private void OnStartUpdateDataCommandExecute(object? parameter)
    {
        if (_LocalCancellationTokenSource.IsCancellationRequested)
        {
            _LocalCancellationTokenSource.Dispose(); 
            _LocalCancellationTokenSource = new();
        }

        // Соединяем локальный токен отмены с токеном отмены уровня приложения.
        var combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_GlobalToken, _LocalCancellationTokenSource.Token);
        // Запуск задачи обновления данных.
        Task loopGetViewTask = Task.Run(async () => await LoopGetViewModelCollectionAsync(combinedTokenSource.Token));

        // Задача разрущения токена отмены после заверщения задачи.
        Task taskDisposeToken = loopGetViewTask.ContinueWith(delegate
        {
            combinedTokenSource.Dispose();

        }, TaskScheduler.Default);
    }

    /// <summary>
    /// Возможность выполенения команды единоразовой подгрузки данных из БД.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    /// <returns>Возвращает доступность выполнения команды - True или False.</returns>
    private bool CanManualUpdateDataCommandExecute(object? parameter) => parameter == null;

    /// <summary>
    /// Выполняет команды единоразовой подгрузки данных из БД.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    private void OnManualUpdateDataCommandExecute(object? parameter)
    {
        if (_LocalCancellationTokenSource.IsCancellationRequested)
        {
            _LocalCancellationTokenSource.Dispose();
            _LocalCancellationTokenSource = new();
        }

        // Соединяем локальный токен отмены с токеном отмены уровня приложения.
        var combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_GlobalToken, _LocalCancellationTokenSource.Token);
        Task getViewtask = Task.Run(async () => await GetViewModelCollectionAsync(combinedTokenSource.Token));

        // Задача разрущения токена отмены после заверщения задачи.
        Task taskDisposeToken = getViewtask.ContinueWith(_ => combinedTokenSource.Dispose(), TaskScheduler.Default);
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

    #endregion
}

