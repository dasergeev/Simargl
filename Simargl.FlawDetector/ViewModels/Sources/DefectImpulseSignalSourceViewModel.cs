using Simargl.FlawDetector.Models; // Подключает перечисление типов источников сигнала.
using Simargl.FlawDetector.Models.Sources; // Подключает модели конкретных источников сигнала.

namespace Simargl.FlawDetector.ViewModels.Sources; // Определяет пространство имён view model дефектных импульсов.

/// <summary> // Описывает назначение класса.
/// Представляет настройки источника дефектных импульсов в пользовательском интерфейсе. // Уточняет роль типа в интерфейсе.
/// </summary> // Завершает XML-документацию класса.
internal sealed class DefectImpulseSignalSourceViewModel : SignalSourceViewModelBase // Объявляет view model дефектных импульсов.
{ // Начинает тело класса.
    private int responseMode; // Хранит режим дефектного отклика.
    private double energyScale; // Хранит энергию импульсов.
    private double sharpnessScale; // Хранит остроту импульсов.
    private bool includeSidebands; // Хранит признак боковых полос.

    /// <summary> // Документирует режимы отклика.
    /// Получает список режимов дефектного отклика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public IReadOnlyList<string> ResponseModes { get; } = ["Широкополосный", "Резонансный", "Модулированный"]; // Хранит список режимов дефектного отклика.

    /// <summary> // Документирует конструктор.
    /// Инициализирует новую view model дефектных импульсов. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public DefectImpulseSignalSourceViewModel() // Объявляет конструктор view model.
        : base(SignalSourceType.DefectImpulses, "Импульсы дефектов подшипника") // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
    } // Завершает тело конструктора.

    /// <summary> // Документирует режим отклика.
    /// Получает или задаёт режим дефектного отклика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int ResponseMode // Объявляет свойство режима отклика.
    { // Начинает тело свойства.
        get => this.responseMode; // Возвращает текущий режим отклика.
        set => SetProperty(ref this.responseMode, Math.Clamp(value, 0, ResponseModes.Count - 1)); // Обновляет режим отклика.
    } // Завершает тело свойства.

    /// <summary> // Документирует энергию импульсов.
    /// Получает или задаёт энергию дефектных импульсов. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double EnergyScale // Объявляет свойство энергии импульсов.
    { // Начинает тело свойства.
        get => this.energyScale; // Возвращает текущую энергию импульсов.
        set => SetProperty(ref this.energyScale, Math.Clamp(value, 0.1d, 4d)); // Обновляет энергию импульсов.
    } // Завершает тело свойства.

    /// <summary> // Документирует остроту импульсов.
    /// Получает или задаёт остроту дефектных импульсов. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double SharpnessScale // Объявляет свойство остроты импульсов.
    { // Начинает тело свойства.
        get => this.sharpnessScale; // Возвращает текущую остроту импульсов.
        set => SetProperty(ref this.sharpnessScale, Math.Clamp(value, 0.1d, 4d)); // Обновляет остроту импульсов.
    } // Завершает тело свойства.

    /// <summary> // Документирует боковые полосы.
    /// Получает или задаёт признак добавления боковых полос модуляции. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool IncludeSidebands // Объявляет свойство боковых полос.
    { // Начинает тело свойства.
        get => this.includeSidebands; // Возвращает текущее состояние боковых полос.
        set => SetProperty(ref this.includeSidebands, value); // Обновляет состояние боковых полос.
    } // Завершает тело свойства.

    /// <inheritdoc /> // Наследует документацию метода.
    public override ISignalSource ToModel() // Создаёт модель дефектных импульсов.
    { // Начинает тело метода.
        return new DefectImpulseSignalSource(IsEnabled, ResponseMode, EnergyScale, SharpnessScale, IncludeSidebands); // Возвращает модель дефектных импульсов.
    } // Завершает метод.
}
