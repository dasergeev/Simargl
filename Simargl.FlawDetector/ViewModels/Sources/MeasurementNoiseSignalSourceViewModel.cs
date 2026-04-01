using Simargl.FlawDetector.Models; // Подключает перечисление типов источников сигнала.
using Simargl.FlawDetector.Models.Sources; // Подключает модели конкретных источников сигнала.

namespace Simargl.FlawDetector.ViewModels.Sources; // Определяет пространство имён view model измерительного шума.

/// <summary> // Описывает назначение класса.
/// Представляет настройки источника измерительного шума в пользовательском интерфейсе. // Уточняет роль типа в интерфейсе.
/// </summary> // Завершает XML-документацию класса.
internal sealed class MeasurementNoiseSignalSourceViewModel : SignalSourceViewModelBase // Объявляет view model измерительного шума.
{ // Начинает тело класса.
    private int spectrumMode; // Хранит режим спектра шума.
    private double noiseLevel; // Хранит уровень шума.
    private bool packetMode; // Хранит пакетный режим шума.

    /// <summary> // Документирует доступные типы спектра.
    /// Получает список спектральных режимов шума. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public IReadOnlyList<string> SpectrumModes { get; } = ["Белый шум", "Розовый шум", "Полосовой шум"]; // Хранит список спектральных режимов.

    /// <summary> // Документирует конструктор.
    /// Инициализирует новую view model измерительного шума. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public MeasurementNoiseSignalSourceViewModel() // Объявляет конструктор view model.
        : base(SignalSourceType.MeasurementNoise, "Измерительный шум датчика") // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
    } // Завершает тело конструктора.

    /// <summary> // Документирует режим спектра.
    /// Получает или задаёт режим спектра измерительного шума. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int SpectrumMode // Объявляет свойство режима спектра.
    { // Начинает тело свойства.
        get => this.spectrumMode; // Возвращает текущий режим спектра.
        set => SetProperty(ref this.spectrumMode, Math.Clamp(value, 0, SpectrumModes.Count - 1)); // Обновляет режим спектра.
    } // Завершает тело свойства.

    /// <summary> // Документирует уровень шума.
    /// Получает или задаёт уровень измерительного шума. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double NoiseLevel // Объявляет свойство уровня шума.
    { // Начинает тело свойства.
        get => this.noiseLevel; // Возвращает текущий уровень шума.
        set => SetProperty(ref this.noiseLevel, Math.Clamp(value, 0.1d, 4d)); // Обновляет уровень шума.
    } // Завершает тело свойства.

    /// <summary> // Документирует пакетный режим.
    /// Получает или задаёт признак пакетного режима шума. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool PacketMode // Объявляет свойство пакетного режима.
    { // Начинает тело свойства.
        get => this.packetMode; // Возвращает текущее состояние пакетного режима.
        set => SetProperty(ref this.packetMode, value); // Обновляет состояние пакетного режима.
    } // Завершает тело свойства.

    /// <inheritdoc /> // Наследует документацию метода.
    public override ISignalSource ToModel() // Создаёт модель источника измерительного шума.
    { // Начинает тело метода.
        return new MeasurementNoiseSignalSource(IsEnabled, SpectrumMode, NoiseLevel, PacketMode); // Возвращает модель измерительного шума.
    } // Завершает метод.
}
