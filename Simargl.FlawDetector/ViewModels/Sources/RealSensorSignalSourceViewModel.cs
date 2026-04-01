using Simargl.FlawDetector.Models; // Подключает перечисление типов источников сигнала.
using Simargl.FlawDetector.Models.Sources; // Подключает модели конкретных источников сигнала.
using Simargl.FlawDetector.Services; // Подключает сервис реального датчика.

namespace Simargl.FlawDetector.ViewModels.Sources; // Определяет пространство имён view model реального датчика.

/// <summary> // Описывает назначение класса.
/// Представляет настройки источника сигнала от реального TCP-датчика в пользовательском интерфейсе. // Уточняет роль типа в интерфейсе.
/// </summary> // Завершает XML-документацию класса.
internal sealed class RealSensorSignalSourceViewModel : SignalSourceViewModelBase // Объявляет view model реального датчика.
{ // Начинает тело класса.
    private readonly RealSensorTcpListenerService sensorService; // Хранит сервис прослушивания реального датчика.
    private double bufferDurationSeconds; // Хранит длительность буфера данных.
    private bool validateChecksum; // Хранит признак проверки CRC32.
    private double amplitudeScale; // Хранит масштаб вклада реального сигнала.

    /// <summary> // Документирует конструктор.
    /// Инициализирует новую view model реального TCP-датчика. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    /// <param name="sensorService">Сервис прослушивания реального датчика.</param> // Документирует сервис реального датчика.
    /// <exception cref="ArgumentNullException">Возникает, если сервис датчика не передан.</exception> // Документирует ошибку отсутствия сервиса.
    public RealSensorSignalSourceViewModel(RealSensorTcpListenerService sensorService) // Объявляет конструктор view model.
        : base(SignalSourceType.RealSensor, "Реальный датчик TCP") // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
        this.sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService)); // Сохраняет сервис прослушивания датчика.
        this.bufferDurationSeconds = 15d; // Инициализирует длину буфера реальных данных по умолчанию.
        this.validateChecksum = true; // Инициализирует включённую проверку CRC32 по умолчанию.
        this.amplitudeScale = 1d; // Инициализирует типовой масштаб вклада реального сигнала.
        this.sensorService.PropertyChanged += OnSensorServicePropertyChanged; // Подписывается на обновления состояния сервиса датчика.
    } // Завершает тело конструктора.


    /// <summary> // Документирует длительность буфера.
    /// Получает или задаёт длину буфера принятых данных в секундах. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double BufferDurationSeconds // Объявляет свойство длины буфера.
    { // Начинает тело свойства.
        get => this.bufferDurationSeconds; // Возвращает текущую длину буфера.
        set => SetProperty(ref this.bufferDurationSeconds, Math.Clamp(value, 0.1d, 300d)); // Ограничивает и обновляет длину буфера.
    } // Завершает тело свойства.

    /// <summary> // Документирует проверку CRC.
    /// Получает или задаёт признак проверки контрольной суммы входящих кадров. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool ValidateChecksum // Объявляет свойство проверки CRC.
    { // Начинает тело свойства.
        get => this.validateChecksum; // Возвращает текущий признак проверки CRC.
        set => SetProperty(ref this.validateChecksum, value); // Обновляет текущий признак проверки CRC.
    } // Завершает тело свойства.

    /// <summary> // Документирует масштаб сигнала.
    /// Получает или задаёт масштаб вклада реального сигнала в итоговую модель. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double AmplitudeScale // Объявляет свойство масштаба реального сигнала.
    { // Начинает тело свойства.
        get => this.amplitudeScale; // Возвращает текущий масштаб сигнала.
        set => SetProperty(ref this.amplitudeScale, Math.Clamp(value, 0.1d, 8d)); // Ограничивает и обновляет масштаб сигнала.
    } // Завершает тело свойства.

    /// <summary> // Документирует состояние сервиса.
    /// Получает текстовое описание состояния прослушивания TCP-датчика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public string ConnectionStatus => this.sensorService.ConnectionStatus; // Возвращает состояние прослушивания датчика.

    /// <summary> // Документирует адрес клиента.
    /// Получает конечную точку текущего активного клиента датчика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public string ActiveClientEndpoint => this.sensorService.ActiveClientEndpoint; // Возвращает адрес активного клиента датчика.

    /// <summary> // Документирует счётчик кадров.
    /// Получает количество успешно разобранных кадров реального датчика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public long FramesReceived => this.sensorService.FramesReceived; // Возвращает количество принятых кадров.

    /// <summary> // Документирует счётчик отсчётов.
    /// Получает количество принятых трёхкоординатных отсчётов реального датчика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public long SamplesReceived => this.sensorService.SamplesReceived; // Возвращает количество принятых отсчётов.

    /// <inheritdoc /> // Наследует документацию метода.
    public override ISignalSource ToModel() // Создаёт модель источника реального датчика.
    { // Начинает тело метода.
        return new RealSensorSignalSource(IsEnabled, this.sensorService, AmplitudeScale); // Возвращает модель источника реального датчика.
    } // Завершает метод.

    /// <summary> // Документирует обработчик обновления сервиса.
    /// Передаёт изменения состояния TCP-сервиса в свойства пользовательского интерфейса. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="sender">Источник события изменения свойств.</param> // Документирует источник события.
    /// <param name="eventArgs">Аргументы события изменения свойства.</param> // Документирует аргументы события.
    private void OnSensorServicePropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs eventArgs) // Объявляет обработчик изменений сервиса.
    { // Начинает тело обработчика изменений сервиса.
        OnPropertyChanged(nameof(ConnectionStatus)); // Обновляет отображаемый статус соединения.
        OnPropertyChanged(nameof(ActiveClientEndpoint)); // Обновляет отображаемый адрес клиента.
        OnPropertyChanged(nameof(FramesReceived)); // Обновляет количество принятых кадров.
        OnPropertyChanged(nameof(SamplesReceived)); // Обновляет количество принятых отсчётов.
    } // Завершает обработчик изменений сервиса.
} // Завершает тело класса.


