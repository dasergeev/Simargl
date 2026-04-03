using System.Collections.ObjectModel; // Подключает тип коллекции для привязки источников и дефектов.
using System.ComponentModel; // Подключает события изменения свойств источников.
using System.Diagnostics; // Подключает секундомер для модельного времени.
using System.Windows; // ?????????? ?????? ? ?????????? ????????????????? ??????????.
using System.Windows.Input; // Подключает интерфейс команд WPF.
using System.Windows.Threading; // Подключает диспетчерный таймер для обновления интерфейса.
using Simargl.FlawDetector.Models; // Подключает модели предметной области.
using Simargl.FlawDetector.Services; // Подключает сервис генерации сигнала.
using Simargl.FlawDetector.ViewModels.Sources; // Подключает отдельные view model источников сигнала.

namespace Simargl.FlawDetector.ViewModels; // Определяет пространство имён главной view model.

/// <summary> // Описывает назначение класса.
/// Представляет основную view model окна симулятора дефектов буксового подшипника. // Уточняет роль класса в приложении.
/// </summary> // Завершает XML-документацию класса.
internal sealed class MainWindowViewModel : ObservableObject // Объявляет главную view model окна.
{ // Начинает тело класса.
    private const int MaxLogEntries = 400; // Limits the number of stored log entries.
    private readonly BearingSignalGenerator generator; // Хранит сервис генерации вибросигнала.
    private readonly RealSensorTcpListenerService realSensorService; // Хранит сервис прослушивания реального датчика.
    private readonly DispatcherTimer realtimeTimer; // Хранит таймер потоковой симуляции.
    private readonly Dispatcher uiDispatcher; // ?????? ????????? ????????????????? ?????????? ??? ??????????? ??????????????.
    private readonly Stopwatch simulationStopwatch; // Хранит секундомер модельного времени.
    private double wagonSpeedKilometersPerHour; // Хранит скорость вагона.
    private double wheelDiameterMillimeters; // Хранит диаметр колеса.
    private double axleLoadKilonewtons; // Хранит осевую нагрузку.
    private double bearingPitchDiameterMillimeters; // Хранит средний диаметр подшипника.
    private double rollingElementDiameterMillimeters; // Хранит диаметр тела качения.
    private int rollingElementCount; // Хранит число тел качения.
    private double contactAngleDegrees; // Хранит контактный угол.
    private int sampleRate; // Хранит частоту дискретизации.
    private double durationSeconds; // Хранит длительность сигнала.
    private AxisSignalSeries? xAxis; // Хранит рассчитанный сигнал по оси X.
    private AxisSignalSeries? yAxis; // Хранит рассчитанный сигнал по оси Y.
    private AxisSignalSeries? zAxis; // Хранит рассчитанный сигнал по оси Z.
    private string summary; // Хранит диагностическую сводку.
    private string statusText; // Хранит текст состояния интерфейса.
    private bool isStreaming; // Хранит признак активной потоковой симуляции.
    private bool suppressSourceSynchronization; // Хранит признак временного подавления реакции на промежуточные изменения источников.
    private double simulationTimeSeconds; // Хранит текущее модельное время потока.
    private ISignalSourceViewModel? selectedSignalSource; // Хранит выбранный в списке источник сигнала.
    private bool isLogPaused; // Хранит признак приостановки видимого обновления журнала.
    private string logText; // Хранит видимый текст журнала.

    /// <summary> // Документирует конструктор.
    /// Инициализирует главную view model окна с параметрами симуляции по умолчанию. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public MainWindowViewModel() // Объявляет конструктор главной view model.
    { // Начинает тело конструктора.
        this.generator = new BearingSignalGenerator(); // Создаёт сервис генерации сигнала.
        this.realSensorService = new RealSensorTcpListenerService(); // Создаёт сервис прослушивания реального датчика.
        this.uiDispatcher = Application.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher; // ??????????? ????????? ????????????????? ??????????.
        this.realSensorService.LogEntryAdded += OnRealSensorLogEntryAdded; // Subscribes the shared log to sensor network events.
        this.realtimeTimer = new DispatcherTimer(DispatcherPriority.Render) // Создаёт таймер для обновления графиков в реальном времени.
        { // Начинает инициализацию таймера.
            Interval = TimeSpan.FromMilliseconds(50d), // Устанавливает интервал обновления около 20 кадров в секунду.
        }; // Завершает инициализацию таймера.
        this.realtimeTimer.Tick += OnRealtimeTimerTick; // Подписывает обработчик обновления окна.
        this.simulationStopwatch = new Stopwatch(); // Создаёт секундомер модельного времени.
        this.wagonSpeedKilometersPerHour = 78d; // Устанавливает скорость вагона по умолчанию.
        this.wheelDiameterMillimeters = 920d; // Устанавливает диаметр колеса по умолчанию.
        this.axleLoadKilonewtons = 115d; // Устанавливает осевую нагрузку по умолчанию.
        this.bearingPitchDiameterMillimeters = 168d; // Устанавливает средний диаметр подшипника по умолчанию.
        this.rollingElementDiameterMillimeters = 29d; // Устанавливает диаметр тела качения по умолчанию.
        this.rollingElementCount = 14; // Устанавливает число тел качения по умолчанию.
        this.contactAngleDegrees = 8d; // Устанавливает контактный угол по умолчанию.
        this.sampleRate = 4096; // Устанавливает частоту дискретизации по умолчанию.
        this.durationSeconds = 2.2d; // Устанавливает длительность окна по умолчанию.
        this.summary = "Сводка симуляции будет показана после запуска потока."; // Инициализирует текст сводки.
        this.statusText = string.Empty; // Инициализирует пустой текст состояния.
        this.logText = string.Empty; // Инициализирует пустой видимый журнал.
        Sources = new ObservableCollection<ISignalSourceViewModel> // Создаёт коллекцию редактируемых источников сигнала.
        { // Начинает инициализацию коллекции источников.
            new WheelRotationSignalSourceViewModel(), // Добавляет источник базовой вибрации от вращения.
            new TrackExcitationSignalSourceViewModel(), // Добавляет источник низкочастотного возбуждения от пути.
            new StructuralResonanceSignalSourceViewModel(), // Добавляет источник структурного резонанса.
            new DefectImpulseSignalSourceViewModel(), // Добавляет источник импульсов от дефектов.
            new MeasurementNoiseSignalSourceViewModel(), // Добавляет источник измерительного шума.
            new RealSensorSignalSourceViewModel(this.realSensorService), // Добавляет источник реального TCP-датчика.
        }; // Завершает инициализацию коллекции источников.
        Faults = new ObservableCollection<FaultDefinitionViewModel> // Создаёт коллекцию редактируемых дефектов.
        { // Начинает инициализацию коллекции дефектов.
            new FaultDefinitionViewModel(BearingFaultType.OuterRace, "Дефект наружного кольца", 0.65d, 1.75d), // Добавляет дефект наружного кольца.
            new FaultDefinitionViewModel(BearingFaultType.InnerRace, "Дефект внутреннего кольца", 0.55d, 1.6d), // Добавляет дефект внутреннего кольца.
            new FaultDefinitionViewModel(BearingFaultType.RollingElement, "Дефект тела качения", 0.48d, 1.35d), // Добавляет дефект ролика.
            new FaultDefinitionViewModel(BearingFaultType.Cage, "Дефект сепаратора", 0.35d, 0.85d), // Добавляет дефект сепаратора.
            new FaultDefinitionViewModel(BearingFaultType.Unbalance, "Дисбаланс узла", 0.42d, 0.95d), // Добавляет дисбаланс.
            new FaultDefinitionViewModel(BearingFaultType.Misalignment, "Несоосность узла", 0.38d, 0.9d), // Добавляет несоосность.
        }; // Завершает инициализацию коллекции дефектов.
        this.selectedSignalSource = Sources.FirstOrDefault(); // Выбирает первый источник сигнала по умолчанию.

        foreach (ISignalSourceViewModel source in Sources) // Перебирает все источники для подписки на их изменения.
        { // Начинает цикл подписки на изменения источников.
            if (source is ObservableObject observableSource) // Проверяет поддержку уведомлений об изменении свойств.
            { // Начинает ветку подписки на уведомления.
                observableSource.PropertyChanged += OnSignalSourcePropertyChanged; // Подписывает обработчик изменений источников.
            } // Завершает ветку подписки на уведомления.
        } // Завершает цикл подписки на изменения источников.

        RestartCommand = new RelayCommand(RestartSimulation); // Создаёт команду перезапуска потока.
        ToggleStreamingCommand = new RelayCommand(ToggleStreaming); // Создаёт команду запуска и остановки потока.
        ToggleLogPauseCommand = new RelayCommand(ToggleLogPause); // Создаёт команду приостановки обновления журнала.
        ResetCommand = new RelayCommand(ResetDefaults); // Создаёт команду сброса настроек.
        ResetDefaults(); // Устанавливает стартовый сценарий и запускает поток.
    } // Завершает тело конструктора.

    /// <summary> // Документирует коллекцию источников.
    /// Получает набор источников сигнала, доступных для редактирования в интерфейсе. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public ObservableCollection<ISignalSourceViewModel> Sources { get; } // Хранит редактируемые источники сигнала.

    /// <summary> // Documents the shared text log.
    /// Gets the bounded log of actions, errors, and network events. // Clarifies the property meaning.
    /// </summary> // Ends XML documentation.
    public ObservableCollection<string> LogEntries { get; } = []; // Stores a bounded UI log.

    /// <summary> // Документирует состояние паузы журнала.
    /// Получает признак приостановки обновления видимого текста журнала. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool IsLogPaused // Объявляет свойство паузы журнала.
    { // Начинает тело свойства паузы журнала.
        get => this.isLogPaused; // Возвращает текущий признак паузы журнала.
        private set // Объявляет сеттер свойства паузы журнала.
        { // Начинает тело сеттера свойства паузы журнала.
            if (SetProperty(ref this.isLogPaused, value)) // Проверяет факт изменения состояния паузы журнала.
            { // Начинает ветку обновления зависимых свойств журнала.
                OnPropertyChanged(nameof(LogPauseButtonText)); // Обновляет подпись кнопки управления журналом.
            } // Завершает ветку обновления зависимых свойств журнала.
        } // Завершает тело сеттера свойства паузы журнала.
    } // Завершает тело свойства паузы журнала.

    /// <summary> // Документирует текст журнала.
    /// Получает текст, отображаемый в области журнала. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public string LogText // Объявляет свойство видимого текста журнала.
    { // Начинает тело свойства видимого текста журнала.
        get => this.logText; // Возвращает текущий видимый текст журнала.
        private set => SetProperty(ref this.logText, value); // Обновляет текущий видимый текст журнала.
    } // Завершает тело свойства видимого текста журнала.

    /// <summary> // Документирует выбранный источник сигнала.
    /// Получает или задаёт источник сигнала, выбранный в отдельном списке параметров. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public ISignalSourceViewModel? SelectedSignalSource // Объявляет свойство выбранного источника сигнала.
    { // Начинает тело свойства выбранного источника.
        get => this.selectedSignalSource; // Возвращает текущий выбранный источник.
        set => SetProperty(ref this.selectedSignalSource, value); // Обновляет выбранный источник.
    } // Завершает тело свойства выбранного источника.

    /// <summary> // Документирует коллекцию дефектов.
    /// Получает набор дефектов, доступных для редактирования в интерфейсе. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public ObservableCollection<FaultDefinitionViewModel> Faults { get; } // Хранит редактируемые дефекты.

    /// <summary> // Документирует скорость вагона.
    /// Получает или задаёт скорость движения вагона в километрах в час. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double WagonSpeedKilometersPerHour // Объявляет свойство скорости вагона.
    { // Начинает тело свойства скорости.
        get => this.wagonSpeedKilometersPerHour; // Возвращает текущую скорость.
        set => SetProperty(ref this.wagonSpeedKilometersPerHour, value); // Обновляет скорость вагона.
    } // Завершает тело свойства скорости.

    /// <summary> // Документирует диаметр колеса.
    /// Получает или задаёт диаметр колеса в миллиметрах. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double WheelDiameterMillimeters // Объявляет свойство диаметра колеса.
    { // Начинает тело свойства диаметра колеса.
        get => this.wheelDiameterMillimeters; // Возвращает текущий диаметр колеса.
        set => SetProperty(ref this.wheelDiameterMillimeters, value); // Обновляет диаметр колеса.
    } // Завершает тело свойства диаметра колеса.

    /// <summary> // Документирует осевую нагрузку.
    /// Получает или задаёт нагрузку на ось в килоньютонах. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double AxleLoadKilonewtons // Объявляет свойство осевой нагрузки.
    { // Начинает тело свойства осевой нагрузки.
        get => this.axleLoadKilonewtons; // Возвращает текущую осевую нагрузку.
        set => SetProperty(ref this.axleLoadKilonewtons, value); // Обновляет осевую нагрузку.
    } // Завершает тело свойства осевой нагрузки.

    /// <summary> // Документирует средний диаметр подшипника.
    /// Получает или задаёт средний диаметр дорожки качения подшипника. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double BearingPitchDiameterMillimeters // Объявляет свойство среднего диаметра подшипника.
    { // Начинает тело свойства среднего диаметра.
        get => this.bearingPitchDiameterMillimeters; // Возвращает текущий средний диаметр подшипника.
        set => SetProperty(ref this.bearingPitchDiameterMillimeters, value); // Обновляет средний диаметр подшипника.
    } // Завершает тело свойства среднего диаметра.

    /// <summary> // Документирует диаметр тела качения.
    /// Получает или задаёт диаметр тела качения в миллиметрах. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double RollingElementDiameterMillimeters // Объявляет свойство диаметра тела качения.
    { // Начинает тело свойства диаметра тела качения.
        get => this.rollingElementDiameterMillimeters; // Возвращает текущий диаметр тела качения.
        set => SetProperty(ref this.rollingElementDiameterMillimeters, value); // Обновляет диаметр тела качения.
    } // Завершает тело свойства диаметра тела качения.

    /// <summary> // Документирует количество тел качения.
    /// Получает или задаёт количество тел качения. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int RollingElementCount // Объявляет свойство количества тел качения.
    { // Начинает тело свойства количества тел качения.
        get => this.rollingElementCount; // Возвращает текущее количество тел качения.
        set => SetProperty(ref this.rollingElementCount, value); // Обновляет количество тел качения.
    } // Завершает тело свойства количества тел качения.

    /// <summary> // Документирует контактный угол.
    /// Получает или задаёт контактный угол подшипника в градусах. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double ContactAngleDegrees // Объявляет свойство контактного угла.
    { // Начинает тело свойства контактного угла.
        get => this.contactAngleDegrees; // Возвращает текущий контактный угол.
        set => SetProperty(ref this.contactAngleDegrees, value); // Обновляет контактный угол.
    } // Завершает тело свойства контактного угла.

    /// <summary> // Документирует частоту дискретизации.
    /// Получает или задаёт частоту дискретизации сигнала в герцах. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int SampleRate // Объявляет свойство частоты дискретизации.
    { // Начинает тело свойства частоты дискретизации.
        get => this.sampleRate; // Возвращает текущую частоту дискретизации.
        set => SetProperty(ref this.sampleRate, value); // Обновляет частоту дискретизации.
    } // Завершает тело свойства частоты дискретизации.

    /// <summary> // Документирует длительность окна.
    /// Получает или задаёт ширину отображаемого окна сигнала в секундах. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double DurationSeconds // Объявляет свойство длительности окна.
    { // Начинает тело свойства длительности окна.
        get => this.durationSeconds; // Возвращает текущую длительность окна.
        set => SetProperty(ref this.durationSeconds, value); // Обновляет длительность окна.
    } // Завершает тело свойства длительности окна.

    /// <summary> // Документирует график оси X.
    /// Получает или задаёт временной ряд по оси X. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public AxisSignalSeries? XAxis // Объявляет свойство оси X.
    { // Начинает тело свойства оси X.
        get => this.xAxis; // Возвращает текущий ряд оси X.
        private set => SetProperty(ref this.xAxis, value); // Обновляет ряд оси X.
    } // Завершает тело свойства оси X.

    /// <summary> // Документирует график оси Y.
    /// Получает или задаёт временной ряд по оси Y. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public AxisSignalSeries? YAxis // Объявляет свойство оси Y.
    { // Начинает тело свойства оси Y.
        get => this.yAxis; // Возвращает текущий ряд оси Y.
        private set => SetProperty(ref this.yAxis, value); // Обновляет ряд оси Y.
    } // Завершает тело свойства оси Y.

    /// <summary> // Документирует график оси Z.
    /// Получает или задаёт временной ряд по оси Z. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public AxisSignalSeries? ZAxis // Объявляет свойство оси Z.
    { // Начинает тело свойства оси Z.
        get => this.zAxis; // Возвращает текущий ряд оси Z.
        private set => SetProperty(ref this.zAxis, value); // Обновляет ряд оси Z.
    } // Завершает тело свойства оси Z.

    /// <summary> // Документирует сводку.
    /// Получает или задаёт диагностическую сводку по текущему окну сигнала. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public string Summary // Объявляет свойство сводки.
    { // Начинает тело свойства сводки.
        get => this.summary; // Возвращает текущую сводку.
        private set => SetProperty(ref this.summary, value); // Обновляет сводку.
    } // Завершает тело свойства сводки.

    /// <summary> // Документирует строку состояния.
    /// Получает или задаёт диагностическое сообщение интерфейса. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public string StatusText // Объявляет свойство строки состояния.
    { // Начинает тело свойства строки состояния.
        get => this.statusText; // Возвращает текущий текст состояния.
        private set => SetProperty(ref this.statusText, value); // Обновляет текст состояния.
    } // Завершает тело свойства строки состояния.

    /// <summary> // Документирует состояние потока.
    /// Получает или задаёт признак активной потоковой симуляции. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool IsStreaming // Объявляет свойство состояния потока.
    { // Начинает тело свойства состояния потока.
        get => this.isStreaming; // Возвращает текущее состояние потока.
        private set // Объявляет закрытый сеттер состояния потока.
        { // Начинает тело сеттера.
            if (SetProperty(ref this.isStreaming, value)) // Проверяет факт изменения состояния потока.
            { // Начинает ветку обновления зависимых свойств.
                OnPropertyChanged(nameof(ToggleStreamingButtonText)); // Обновляет подпись кнопки запуска потока.
            } // Завершает ветку обновления зависимых свойств.
        } // Завершает тело сеттера.
    } // Завершает тело свойства состояния потока.

    /// <summary> // Документирует модельное время.
    /// Получает или задаёт текущее модельное время потоковой симуляции в секундах. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double SimulationTimeSeconds // Объявляет свойство модельного времени.
    { // Начинает тело свойства модельного времени.
        get => this.simulationTimeSeconds; // Возвращает текущее модельное время.
        private set => SetProperty(ref this.simulationTimeSeconds, value); // Обновляет текущее модельное время.
    } // Завершает тело свойства модельного времени.

    /// <summary> // Документирует подпись кнопки потока.
    /// Получает текст кнопки запуска или остановки потоковой симуляции. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public string ToggleStreamingButtonText => IsStreaming ? "Остановить поток" : "Запустить поток"; // Возвращает подпись кнопки потока.

    /// <summary> // Документирует подпись кнопки журнала.
    /// Получает текст кнопки приостановки или возобновления обновления журнала. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public string LogPauseButtonText => IsLogPaused ? "Продолжить" : "Пауза"; // Возвращает подпись кнопки управления журналом.

    /// <summary> // Документирует команду перезапуска.
    /// Получает команду перезапуска потоковой симуляции с текущими настройками. // Уточняет назначение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public ICommand RestartCommand { get; } // Хранит команду перезапуска потока.

    /// <summary> // Документирует команду запуска и остановки.
    /// Получает команду управления потоковой симуляцией в реальном времени. // Уточняет назначение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public ICommand ToggleStreamingCommand { get; } // Хранит команду запуска и остановки потока.

    /// <summary> // Документирует команду сброса.
    /// Получает команду возврата настроек к типовым значениям. // Уточняет назначение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public ICommand ResetCommand { get; } // Хранит команду сброса настроек.

    /// <summary> // Документирует команду управления журналом.
    /// Получает команду приостановки или возобновления обновления журнала. // Уточняет назначение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public ICommand ToggleLogPauseCommand { get; } // Хранит команду управления журналом.

    /// <summary> // Документирует переключение потока.
    /// Запускает или останавливает потоковую симуляцию в зависимости от текущего состояния. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void ToggleLogPause() // Объявляет метод переключения паузы журнала.
    { // Начинает тело метода переключения паузы журнала.
        IsLogPaused = !IsLogPaused; // Переключает текущее состояние паузы журнала.

        if (IsLogPaused) // Проверяет, был ли журнал переведён в режим паузы.
        { // Начинает ветку включения паузы журнала.
            AppendLog("Обновление журнала приостановлено."); // Добавляет запись о включении паузы журнала.
        } // Завершает ветку включения паузы журнала.
        else // Обрабатывает возобновление обновления журнала.
        { // Начинает ветку выключения паузы журнала.
            RefreshVisibleLog(); // Восстанавливает полный видимый текст журнала после паузы.
            AppendLog("Обновление журнала возобновлено."); // Добавляет запись о возобновлении журнала.
        } // Завершает ветку выключения паузы журнала.
    } // Завершает метод переключения паузы журнала.

    /// <summary> // Документирует переключение потока.
    /// Запускает или останавливает потоковую симуляцию в зависимости от текущего состояния. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void ToggleStreaming() // Объявляет метод переключения потока.
    { // Начинает тело метода переключения потока.
        if (IsStreaming) // Проверяет, работает ли потоковая симуляция.
        { // Начинает ветку остановки потока.
            StopStreaming(); // Останавливает потоковую симуляцию.
        } // Завершает ветку остановки потока.
        else // Обрабатывает запуск потока.
        { // Начинает ветку запуска потока.
            StartStreaming(); // Запускает потоковую симуляцию.
        } // Завершает ветку запуска потока.
    } // Завершает метод переключения потока.

    /// <summary> // Документирует перезапуск потока.
    /// Пересоздаёт потоковую симуляцию с текущими параметрами и мгновенно обновляет графики. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void RestartSimulation() // Объявляет метод перезапуска симуляции.
    { // Начинает тело метода перезапуска симуляции.
        try // Начинает обработку возможных ошибок перезапуска.
        { // Начинает блок перезапуска.
            SimulationTimeSeconds = Math.Max(DurationSeconds, 0.1d); // Возвращает модельное время к начальному окну.
            this.simulationStopwatch.Restart(); // Перезапускает секундомер модельного времени.
            RenderCurrentWindow(); // Немедленно обновляет графики текущим окном.
            StatusText = string.Empty; // Очищает строку состояния после успешного перезапуска.
            AppendLog("Перезапущено обновление сигнала с текущими параметрами."); // Writes a restart message to the log.
        } // Завершает блок перезапуска.
        catch (Exception exception) // Перехватывает ошибки перезапуска.
        { // Начинает блок обработки ошибки.
            StopStreaming(); // Останавливает поток при ошибке конфигурации.
            StatusText = $"Ошибка перезапуска: {exception.Message}"; // Сообщает пользователю об ошибке.
            AppendLog($"Ошибка перезапуска потока: {exception.Message}"); // Writes a restart error to the log.
        } // Завершает блок обработки ошибки.
    } // Завершает метод перезапуска симуляции.

    /// <summary> // Документирует запуск потока.
    /// Запускает обновление сигнала в реальном времени. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void StartStreaming() // Объявляет метод запуска потока.
    { // Начинает тело метода запуска потока.
        RestartSimulation(); // Перезапускает симуляцию перед запуском потока.
        this.realtimeTimer.Start(); // Запускает таймер потоковых обновлений.
        IsStreaming = true; // Переводит интерфейс в состояние активного потока.
        AppendLog("Запущено потоковое обновление графиков."); // Writes a streaming-start message to the log.
        StatusText = string.Empty; // Очищает строку состояния после запуска потока.
    } // Завершает метод запуска потока.

    /// <summary> // Документирует остановку потока.
    /// Останавливает обновление сигнала в реальном времени. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void StopStreaming() // Объявляет метод остановки потока.
    { // Начинает тело метода остановки потока.
        this.realtimeTimer.Stop(); // Останавливает таймер потоковых обновлений.
        this.simulationStopwatch.Stop(); // Останавливает секундомер модельного времени.
        AppendLog("Остановлено потоковое обновление графиков."); // Writes a streaming-stop message to the log.
        IsStreaming = false; // Переводит интерфейс в состояние остановленного потока.
        StatusText = string.Empty; // Очищает строку состояния после остановки потока.
    } // Завершает метод остановки потока.

    /// <summary> // Документирует обработчик таймера.
    /// Продвигает модельное время и обновляет текущее окно сигнала. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="sender">Источник события таймера.</param> // Документирует источник события.
    /// <param name="eventArgs">Аргументы события таймера.</param> // Документирует аргументы события.
    private void OnRealtimeTimerTick(object? sender, EventArgs eventArgs) // Объявляет обработчик тика таймера.
    { // Начинает тело обработчика тика таймера.
        try // Начинает обработку возможных ошибок обновления потока.
        { // Начинает блок обновления потока.
            SimulationTimeSeconds = Math.Max(DurationSeconds, this.simulationStopwatch.Elapsed.TotalSeconds + DurationSeconds); // Продвигает модельное время по ходу работы таймера.
            RenderCurrentWindow(); // Обновляет графики для нового окна времени.
        } // Завершает блок обновления потока.
        catch (Exception exception) // Перехватывает ошибки обновления потока.
        { // Начинает блок обработки ошибки потока.
            StopStreaming(); // Останавливает поток после ошибки.
            AppendLog($"Ошибка потоковой симуляции: {exception.Message}"); // Writes a streaming error to the log.
            StatusText = $"Ошибка потоковой симуляции: {exception.Message}"; // Сообщает пользователю об ошибке.
        } // Завершает блок обработки ошибки потока.
    } // Завершает обработчик тика таймера.

    /// <summary> // Документирует отрисовку окна.
    /// Пересчитывает и отображает текущее скользящее окно сигнала. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void RenderCurrentWindow() // Объявляет метод отрисовки текущего окна.
    { // Начинает тело метода отрисовки окна.
        SignalGenerationSettings settings = CreateSettingsSnapshot(); // ??????? ?????? ??????? ???????? ?????????.
        bool realSensorEnabled = Sources.OfType<RealSensorSignalSourceViewModel>().Any(static source => source.IsEnabled); // ?????????, ???????????? ?? ???????? ?????? ? ??????? ????????????.
        double windowEndTimeSeconds = SimulationTimeSeconds; // ?????????????? ????? ????? ???? ??????? ????????? ????????.
        if (realSensorEnabled && this.realSensorService.LatestSampleTimeSeconds > 0d) // ????????? ??????? ?????????? ?????? ????????? ???????.
        { // ???????? ????? ????????????? ???? ?? ????????? ???????.
            windowEndTimeSeconds = Math.Max(settings.DurationSeconds, this.realSensorService.LatestSampleTimeSeconds); // ??????????? ????? ???? ? ?????????? ??????? ????????? ???????.
            SimulationTimeSeconds = windowEndTimeSeconds; // ?????????????? ????????? ????? ? ????????? ???? ????????? ???????.
        } // ????????? ????? ????????????? ???? ?? ????????? ???????.

        SimulationResult result = this.generator.GenerateWindow(settings, windowEndTimeSeconds); // ????????? ?????? ???????? ?????????? ????.
        XAxis = result.XAxis; // Обновляет график оси X.
        YAxis = result.YAxis; // Обновляет график оси Y.
        ZAxis = result.ZAxis; // Обновляет график оси Z.
        Summary = result.Summary; // Обновляет диагностическую сводку.
    } // Завершает метод отрисовки окна.

    /// <summary> // Документирует создание снимка настроек.
    /// Формирует неизменяемый снимок текущих параметров интерфейса для сервиса генерации. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <returns>Снимок настроек генерации сигнала.</returns> // Документирует возвращаемое значение.
    private SignalGenerationSettings CreateSettingsSnapshot() // Объявляет метод создания снимка настроек.
    { // Начинает тело метода создания снимка.
        return new SignalGenerationSettings( // Создаёт снимок текущих настроек симуляции.
            WagonSpeedKilometersPerHour, // Передаёт скорость вагона.
            WheelDiameterMillimeters, // Передаёт диаметр колеса.
            AxleLoadKilonewtons, // Передаёт осевую нагрузку.
            BearingPitchDiameterMillimeters, // Передаёт средний диаметр подшипника.
            RollingElementDiameterMillimeters, // Передаёт диаметр тела качения.
            RollingElementCount, // Передаёт количество тел качения.
            ContactAngleDegrees, // Передаёт контактный угол.
            SampleRate, // Передаёт частоту дискретизации.
            DurationSeconds, // Передаёт длительность окна.
            Sources.Select(static source => source.ToModel()).ToArray(), // Передаёт текущие источники сигнала.
            Faults.Select(static fault => fault.ToModel()).ToArray()); // Передаёт текущие дефекты.
    } // Завершает метод создания снимка.

    /// <summary> // Документирует сброс параметров.
    /// Возвращает интерфейс к типовым значениям буксового узла грузового вагона и автоматически запускает поток. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void ResetDefaults() // Объявляет метод сброса параметров.
    { // Начинает тело метода сброса.
        this.suppressSourceSynchronization = true; // Временно отключает реакцию на промежуточные изменения источников.
        LogEntries.Clear(); // Clears old log entries before applying defaults.
        IsLogPaused = false; // Возобновляет обновление журнала после сброса.
        LogText = string.Empty; // Очищает видимый текст журнала перед новым заполнением.
        AppendLog("Выполняется сброс модели к типовым значениям."); // Writes a reset-start message to the log.
        WagonSpeedKilometersPerHour = 78d; // Возвращает типовую скорость вагона.
        WheelDiameterMillimeters = 920d; // Возвращает типовой диаметр колеса.
        AxleLoadKilonewtons = 115d; // Возвращает типовую осевую нагрузку.
        BearingPitchDiameterMillimeters = 168d; // Возвращает типовой средний диаметр подшипника.
        RollingElementDiameterMillimeters = 29d; // Возвращает типовой диаметр ролика.
        RollingElementCount = 14; // Возвращает типовое число тел качения.
        ContactAngleDegrees = 8d; // Возвращает типовой контактный угол.
        SampleRate = 4096; // Возвращает типовую частоту дискретизации.
        DurationSeconds = 2.2d; // Возвращает типовую ширину окна сигнала.

        foreach (ISignalSourceViewModel source in Sources) // Перебирает все источники сигнала.
        { // Начинает цикл сброса источников.
            source.IsEnabled = source.SourceType is not SignalSourceType.StructuralResonance; // Включает все базовые источники, кроме структурного резонанса.
            switch (source) // Выбирает тип отдельной view model источника.
            { // Начинает ветвление по типу источника.
                case WheelRotationSignalSourceViewModel wheelRotationSource: // Обрабатывает настройки вращательной вибрации.
                    wheelRotationSource.AmplitudeScale = 1d; // Возвращает типовой уровень вращательной вибрации.
                    wheelRotationSource.HarmonicContent = 1.4d; // Возвращает типовую насыщенность гармониками.
                    wheelRotationSource.IncludeEccentricity = true; // Возвращает типовое состояние эксцентриситета.
                    break; // Завершает настройку вращательного источника.
                case TrackExcitationSignalSourceViewModel trackExcitationSource: // Обрабатывает настройки возбуждения от пути.
                    trackExcitationSource.IntensityScale = 1d; // Возвращает типовую интенсивность воздействия пути.
                    trackExcitationSource.ProfileMode = 0; // Возвращает типовой режим профиля пути.
                    trackExcitationSource.IncludeJointImpacts = true; // Возвращает включение ударов стыков.
                    break; // Завершает настройку источника пути.
                case StructuralResonanceSignalSourceViewModel structuralResonanceSource: // Обрабатывает настройки структурного резонанса.
                    structuralResonanceSource.IsEnabled = false; // Оставляет структурный резонанс выключенным по умолчанию.
                    structuralResonanceSource.ResonanceMode = 0; // Возвращает типовой резонансный режим.
                    structuralResonanceSource.DampingScale = 0.7d; // Возвращает типовое демпфирование.
                    structuralResonanceSource.EmphasizeVerticalAxis = true; // Возвращает усиление вертикальной оси.
                    break; // Завершает настройку резонансного источника.
                case DefectImpulseSignalSourceViewModel defectImpulseSource: // Обрабатывает настройки дефектных импульсов.
                    defectImpulseSource.ResponseMode = 1; // Возвращает типовой резонансный режим дефектов.
                    defectImpulseSource.EnergyScale = 1d; // Возвращает типовую энергию импульсов.
                    defectImpulseSource.SharpnessScale = 1.8d; // Возвращает типовую остроту импульсов.
                    defectImpulseSource.IncludeSidebands = true; // Возвращает включение боковых полос.
                    break; // Завершает настройку дефектного источника.
                case MeasurementNoiseSignalSourceViewModel measurementNoiseSource: // Обрабатывает настройки измерительного шума.
                    measurementNoiseSource.IsEnabled = false; // Оставляет измерительный шум выключенным по умолчанию.
                    measurementNoiseSource.SpectrumMode = 0; // Возвращает типовой спектральный режим.
                    measurementNoiseSource.NoiseLevel = 0.6d; // Возвращает типовой уровень шума.
                    measurementNoiseSource.PacketMode = false; // Возвращает отключённый пакетный режим.
                    break; // Завершает настройку шумового источника.
                case RealSensorSignalSourceViewModel realSensorSource: // Обрабатывает настройки реального датчика.
                    realSensorSource.IsEnabled = false; // Оставляет реальный датчик выключенным по умолчанию.
                    realSensorSource.BufferDurationSeconds = 15d; // Возвращает типовую длину буфера реальных данных.
                    realSensorSource.ValidateChecksum = true; // Возвращает проверку CRC32 по умолчанию.
                    realSensorSource.AmplitudeScale = 1d; // Возвращает типовой масштаб реального сигнала.
                    break; // Завершает настройку реального датчика.
            } // Завершает ветвление по типу источника.
        } // Завершает цикл сброса источников.

        SelectedSignalSource ??= Sources.FirstOrDefault(); // Гарантирует наличие выбранного источника после сброса.

        foreach (FaultDefinitionViewModel fault in Faults) // Перебирает все дефекты интерфейса.
        { // Начинает цикл сброса дефектов.
            fault.IsEnabled = fault.FaultType is BearingFaultType.OuterRace or BearingFaultType.Unbalance; // Включает дефекты, которые полезны для стартового сценария.
        } // Завершает цикл сброса дефектов.

        this.suppressSourceSynchronization = false; // Возвращает обычную синхронизацию сервисов после пакетной настройки.
        ApplyRealSensorSourceState(); // Применяет итоговое состояние реального датчика после полной инициализации.
        AppendLog("Сброс модели завершён, применено стартовое состояние источников."); // Writes a reset-complete message to the log.
        StartStreaming(); // Запускает потоковую симуляцию после сброса параметров.
    } // Завершает метод сброса параметров.

    /// <summary> // Документирует обработчик изменений источников.
    /// Синхронизирует вспомогательные сервисы с изменениями параметров источников сигнала. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="sender">Источник события изменения свойств.</param> // Документирует источник события.
    /// <param name="eventArgs">Аргументы события изменения свойства.</param> // Документирует аргументы события.
    private void OnSignalSourcePropertyChanged(object? sender, PropertyChangedEventArgs eventArgs) // Объявляет обработчик изменений источников.
    { // Начинает тело обработчика изменений источников.
        if (this.suppressSourceSynchronization) // Проверяет, выполняется ли пакетная инициализация источников.
        { // Начинает ветку подавления синхронизации.
            return; // Пропускает промежуточную синхронизацию сервисов во время пакетной настройки.
        } // Завершает ветку подавления синхронизации.

        if (sender is ISignalSourceViewModel signalSource && eventArgs.PropertyName == nameof(ISignalSourceViewModel.IsEnabled)) // Checks a source activation change.
        { // Begins the source activation log branch.
            AppendLog($"Источник '{signalSource.DisplayName}' {(signalSource.IsEnabled ? "включён" : "выключен")}."); // Writes a source activation message to the log.
        } // Ends the source activation log branch.

        if (sender is RealSensorSignalSourceViewModel realSensorSource) // Checks whether the changed source is the real TCP sensor.
        { // Begins the real sensor branch.
            if (eventArgs.PropertyName is nameof(RealSensorSignalSourceViewModel.BufferDurationSeconds) or nameof(RealSensorSignalSourceViewModel.ValidateChecksum)) // Checks whether real sensor configuration changed.
            { // Begins the real sensor configuration log branch.
                AppendLog($"Изменены параметры реального датчика: буфер {realSensorSource.BufferDurationSeconds:F1} c, CRC {(realSensorSource.ValidateChecksum ? "включена" : "выключена")}."); // Writes a real sensor configuration message to the log.
            } // Ends the real sensor configuration log branch.

            if (eventArgs.PropertyName is nameof(RealSensorSignalSourceViewModel.IsEnabled) or nameof(RealSensorSignalSourceViewModel.BufferDurationSeconds) or nameof(RealSensorSignalSourceViewModel.ValidateChecksum)) // ?????????, ????????? ?? ????????? ? ???????????? ????????? TCP-???????.
            { // ???????? ????? ????????????? TCP-???????.
                ApplyRealSensorSourceState(); // ?????????????? ????????? TCP-??????? ?????? ? ????????????????? ??????????? ?????????.
            } // ????????? ????? ????????????? TCP-???????.
        } // ????????? ????? ????????? ???????.
    } // Завершает обработчик изменений источников.

    /// <summary> // Документирует применение состояния TCP-источника.
    /// Включает или выключает сервис реального датчика в соответствии с текущими настройками источника. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void ApplyRealSensorSourceState() // Объявляет метод синхронизации TCP-источника.
    { // Начинает тело метода синхронизации TCP-источника.
        if (Sources.OfType<RealSensorSignalSourceViewModel>().FirstOrDefault() is not { } realSensorSource) // Пытается получить view model реального датчика.
        { // Начинает ветку отсутствия view model реального датчика.
            this.realSensorService.Stop(); // Останавливает сервис при отсутствии настроек источника.
            return; // Завершает метод при отсутствии источника.
        } // Завершает ветку отсутствия view model реального датчика.

        this.realSensorService.UpdateActivation(realSensorSource.IsEnabled, realSensorSource.BufferDurationSeconds, realSensorSource.ValidateChecksum); // Применяет текущие настройки реального датчика к TCP-сервису.
    } // Завершает метод синхронизации TCP-источника.

    /// <summary> // Documents the sensor log relay handler.
    /// Transfers sensor network log messages into the shared window log. // Clarifies the method purpose.
    /// </summary> // Ends XML documentation.
    /// <param name="message">The sensor log message.</param> // Documents the incoming message.
    private void OnRealSensorLogEntryAdded(string message) // Handles sensor log messages.
    { // Begins the handler body.
        AppendLog(message); // Adds the sensor message to the shared log.
    } // Ends the handler body.

    /// <summary> // Документирует обновление видимого текста журнала.
    /// Пересобирает текст журнала из ограниченной коллекции записей. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    private void RefreshVisibleLog() // Объявляет метод обновления видимого текста журнала.
    { // Начинает тело метода обновления видимого текста журнала.
        LogText = string.Join(Environment.NewLine, LogEntries); // Пересобирает видимый текст журнала из текущих записей.
    } // Завершает метод обновления видимого текста журнала.

    /// <summary> // Documents log appending.
    /// Adds a new line to the shared log and keeps only the latest entries. // Clarifies the method purpose.
    /// </summary> // Ends XML documentation.
    /// <param name="message">The short event text.</param> // Documents the log text.
    private void AppendLog(string message) // Adds a new line to the shared log.
    { // Begins the method body.
        if (!this.uiDispatcher.CheckAccess()) // Проверяет, выполняется ли обновление журнала в UI-потоке.
        { // Начинает ветку переноса обновления журнала в UI-поток.
            _ = this.uiDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => AppendLog(message))); // Переносит обновление журнала в UI-поток.
            return; // Завершает метод до выполнения в UI-потоке.
        } // Завершает ветку переноса обновления журнала в UI-поток.

        string timestampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}"; // Добавляет локальную отметку времени к сообщению журнала.
        LogEntries.Add(timestampedMessage); // Добавляет новую запись в ограниченную коллекцию журнала.

        while (LogEntries.Count > MaxLogEntries) // Проверяет, превышен ли допустимый размер журнала.
        { // Начинает цикл ограничения размера журнала.
            LogEntries.RemoveAt(0); // Удаляет самую старую запись из журнала.
        } // Завершает цикл ограничения размера журнала.

        if (!IsLogPaused) // Проверяет, разрешено ли обновление видимого текста журнала.
        { // Начинает ветку активного обновления видимого журнала.
            RefreshVisibleLog(); // Обновляет текст журнала в интерфейсе.
        } // Завершает ветку активного обновления видимого журнала.
    } // Ends the method body.
} // Завершает тело главной view model.










