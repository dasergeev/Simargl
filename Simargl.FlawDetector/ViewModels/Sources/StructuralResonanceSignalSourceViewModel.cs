using Simargl.FlawDetector.Models; // Подключает перечисление типов источников сигнала.
using Simargl.FlawDetector.Models.Sources; // Подключает модели конкретных источников сигнала.

namespace Simargl.FlawDetector.ViewModels.Sources; // Определяет пространство имён view model структурного резонанса.

/// <summary> // Описывает назначение класса.
/// Представляет настройки источника структурного резонанса в пользовательском интерфейсе. // Уточняет роль типа в интерфейсе.
/// </summary> // Завершает XML-документацию класса.
internal sealed class StructuralResonanceSignalSourceViewModel : SignalSourceViewModelBase // Объявляет view model структурного резонанса.
{ // Начинает тело класса.
    private int resonanceMode; // Хранит выбранный резонансный режим.
    private double dampingScale; // Хранит демпфирование.
    private bool emphasizeVerticalAxis; // Хранит признак усиления вертикальной оси.

    /// <summary> // Документирует доступные контуры резонанса.
    /// Получает список вариантов структурного резонанса. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public IReadOnlyList<string> ResonanceModes { get; } = ["Рама тележки", "Буксовый кронштейн", "Корпус датчика"]; // Хранит список резонансных контуров.

    /// <summary> // Документирует конструктор.
    /// Инициализирует новую view model структурного резонанса. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public StructuralResonanceSignalSourceViewModel() // Объявляет конструктор view model.
        : base(SignalSourceType.StructuralResonance, "Структурный резонанс тележки") // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
    } // Завершает тело конструктора.

    /// <summary> // Документирует режим резонанса.
    /// Получает или задаёт режим структурного резонанса. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int ResonanceMode // Объявляет свойство режима резонанса.
    { // Начинает тело свойства.
        get => this.resonanceMode; // Возвращает текущий режим резонанса.
        set => SetProperty(ref this.resonanceMode, Math.Clamp(value, 0, ResonanceModes.Count - 1)); // Обновляет режим резонанса.
    } // Завершает тело свойства.

    /// <summary> // Документирует демпфирование.
    /// Получает или задаёт уровень демпфирования резонансного отклика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double DampingScale // Объявляет свойство демпфирования.
    { // Начинает тело свойства.
        get => this.dampingScale; // Возвращает текущее демпфирование.
        set => SetProperty(ref this.dampingScale, Math.Clamp(value, 0.1d, 4d)); // Обновляет демпфирование.
    } // Завершает тело свойства.

    /// <summary> // Документирует вертикальное усиление.
    /// Получает или задаёт признак усиления вклада по оси Z. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool EmphasizeVerticalAxis // Объявляет свойство усиления вертикальной оси.
    { // Начинает тело свойства.
        get => this.emphasizeVerticalAxis; // Возвращает текущее состояние усиления оси Z.
        set => SetProperty(ref this.emphasizeVerticalAxis, value); // Обновляет состояние усиления оси Z.
    } // Завершает тело свойства.

    /// <inheritdoc /> // Наследует документацию метода.
    public override ISignalSource ToModel() // Создаёт модель источника структурного резонанса.
    { // Начинает тело метода.
        return new StructuralResonanceSignalSource(IsEnabled, ResonanceMode, DampingScale, EmphasizeVerticalAxis); // Возвращает модель источника структурного резонанса.
    } // Завершает метод.
}
