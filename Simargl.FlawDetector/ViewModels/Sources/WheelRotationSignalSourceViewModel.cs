using Simargl.FlawDetector.Models; // Подключает перечисление типов источников сигнала.
using Simargl.FlawDetector.Models.Sources; // Подключает модели конкретных источников сигнала.

namespace Simargl.FlawDetector.ViewModels.Sources; // Определяет пространство имён view model вращательного источника.

/// <summary> // Описывает назначение класса.
/// Представляет настройки источника вращательной вибрации в пользовательском интерфейсе. // Уточняет роль типа в интерфейсе.
/// </summary> // Завершает XML-документацию класса.
internal sealed class WheelRotationSignalSourceViewModel : SignalSourceViewModelBase // Объявляет view model вращательного источника.
{ // Начинает тело класса.
    private double amplitudeScale; // Хранит уровень вращательной вибрации.
    private double harmonicContent; // Хранит насыщенность гармониками.
    private bool includeEccentricity; // Хранит признак эксцентриситета.

    /// <summary> // Документирует конструктор.
    /// Инициализирует новую view model вращательной вибрации. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public WheelRotationSignalSourceViewModel() // Объявляет конструктор view model.
        : base(SignalSourceType.WheelRotation, "Вращение колесной пары") // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
    } // Завершает тело конструктора.

    /// <summary> // Документирует амплитуду.
    /// Получает или задаёт уровень вращательной вибрации. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double AmplitudeScale // Объявляет свойство уровня вращения.
    { // Начинает тело свойства.
        get => this.amplitudeScale; // Возвращает текущий уровень вращения.
        set => SetProperty(ref this.amplitudeScale, Math.Clamp(value, 0.1d, 4d)); // Обновляет уровень вращения.
    } // Завершает тело свойства.

    /// <summary> // Документирует гармоники.
    /// Получает или задаёт насыщенность сигнала гармониками. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double HarmonicContent // Объявляет свойство гармоник.
    { // Начинает тело свойства.
        get => this.harmonicContent; // Возвращает текущую насыщенность гармониками.
        set => SetProperty(ref this.harmonicContent, Math.Clamp(value, 0.1d, 4d)); // Обновляет насыщенность гармониками.
    } // Завершает тело свойства.

    /// <summary> // Документирует эксцентриситет.
    /// Получает или задаёт признак добавления эксцентриситета. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool IncludeEccentricity // Объявляет свойство эксцентриситета.
    { // Начинает тело свойства.
        get => this.includeEccentricity; // Возвращает текущее состояние эксцентриситета.
        set => SetProperty(ref this.includeEccentricity, value); // Обновляет состояние эксцентриситета.
    } // Завершает тело свойства.

    /// <inheritdoc /> // Наследует документацию метода.
    public override ISignalSource ToModel() // Создаёт модель вращательного источника.
    { // Начинает тело метода.
        return new WheelRotationSignalSource(IsEnabled, AmplitudeScale, HarmonicContent, IncludeEccentricity); // Возвращает модель вращательного источника.
    } // Завершает метод.
}
