using Simargl.FlawDetector.Services; // Подключает сервис доступа к буферу реального датчика.

namespace Simargl.FlawDetector.Models.Sources; // Определяет пространство имён источника реального датчика.

/// <summary> // Описывает назначение класса.
/// Представляет источник сигнала, использующий реальные трёхкоординатные отсчёты из TCP-потока датчика. // Уточняет физическую роль источника.
/// </summary> // Завершает XML-документацию класса.
internal sealed class RealSensorSignalSource : SignalSourceBase // Объявляет источник реального датчика.
{ // Начинает тело класса.
    private readonly RealSensorTcpListenerService sensorService; // Хранит сервис доступа к буферу реального датчика.
    private readonly double amplitudeScale; // Хранит масштаб подключаемого реального сигнала.

    /// <summary> // Документирует конструктор.
    /// Инициализирует новый источник сигнала на основе реального датчика. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    /// <param name="isEnabled">Признак включения источника.</param> // Документирует признак включения источника.
    /// <param name="sensorService">Сервис прослушивания TCP-потока реального датчика.</param> // Документирует сервис датчика.
    /// <param name="amplitudeScale">Масштаб вклада реального сигнала в итоговый трёхосевой ряд.</param> // Документирует масштаб сигнала.
    /// <exception cref="ArgumentNullException">Возникает, если сервис датчика не передан.</exception> // Документирует ошибку отсутствия сервиса.
    public RealSensorSignalSource(bool isEnabled, RealSensorTcpListenerService sensorService, double amplitudeScale) // Объявляет конструктор источника.
        : base(SignalSourceType.RealSensor, "Реальный датчик TCP", isEnabled) // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
        this.sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService)); // Сохраняет сервис реального датчика.
        this.amplitudeScale = Math.Clamp(amplitudeScale, 0.1d, 8d); // Ограничивает и сохраняет масштаб реального сигнала.
    } // Завершает тело конструктора.

    /// <summary> // Документирует масштаб сигнала.
    /// Получает масштаб вклада реального сигнала датчика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double AmplitudeScale => this.amplitudeScale; // Возвращает масштаб вклада реального сигнала.

    /// <inheritdoc /> // Наследует документацию метода.
    public override void Accumulate(SignalSourceContext context, IReadOnlyList<FaultDefinition> faults, ref double xSignal, ref double ySignal, ref double zSignal) // Добавляет вклад реального датчика.
    { // Начинает тело метода.
        if (!this.sensorService.TryGetSample(context.TimeSeconds, out RealSensorVector sensorVector)) // Проверяет наличие отсчёта реального датчика для текущего времени.
        { // Начинает ветку отсутствия данных датчика.
            return; // Пропускает добавление вклада при отсутствии данных.
        } // Завершает ветку отсутствия данных датчика.
        xSignal += sensorVector.X * AmplitudeScale; // Добавляет реальный сигнал по оси X.
        ySignal += sensorVector.Y * AmplitudeScale; // Добавляет реальный сигнал по оси Y.
        zSignal += sensorVector.Z * AmplitudeScale; // Добавляет реальный сигнал по оси Z.
    } // Завершает метод накопления вклада.
} // Завершает тело класса.
