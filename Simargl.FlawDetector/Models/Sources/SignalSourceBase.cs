namespace Simargl.FlawDetector.Models.Sources; // Определяет пространство имён базового источника сигнала.

/// <summary> // Описывает назначение абстрактного класса.
/// Реализует общую часть моделей источников сигнала и задаёт базовый контракт наследникам. // Уточняет архитектурную роль типа.
/// </summary> // Завершает XML-документацию класса.
internal abstract class SignalSourceBase : ISignalSource // Объявляет базовый абстрактный класс источника.
{ // Начинает тело класса.
    /// <summary> // Документирует конструктор.
    /// Инициализирует базовые свойства источника сигнала. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    /// <param name="sourceType">Тип источника сигнала.</param> // Документирует тип источника.
    /// <param name="displayName">Отображаемое имя источника.</param> // Документирует отображаемое имя.
    /// <param name="isEnabled">Признак включения источника.</param> // Документирует признак включения.
    protected SignalSourceBase(SignalSourceType sourceType, string displayName, bool isEnabled) // Объявляет конструктор базового класса.
    { // Начинает тело конструктора.
        SourceType = sourceType; // Сохраняет тип источника.
        DisplayName = displayName; // Сохраняет отображаемое имя источника.
        IsEnabled = isEnabled; // Сохраняет признак включения источника.
    } // Завершает тело конструктора.

    /// <inheritdoc /> // Наследует документацию свойства.
    public SignalSourceType SourceType { get; } // Хранит тип источника сигнала.

    /// <inheritdoc /> // Наследует документацию свойства.
    public string DisplayName { get; } // Хранит отображаемое имя источника.

    /// <inheritdoc /> // Наследует документацию свойства.
    public bool IsEnabled { get; } // Хранит признак включения источника.

    /// <inheritdoc /> // Наследует документацию метода.
    public abstract void Accumulate(SignalSourceContext context, IReadOnlyList<FaultDefinition> faults, ref double xSignal, ref double ySignal, ref double zSignal); // Добавляет вклад источника в текущий отсчёт.
}
