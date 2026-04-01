namespace Simargl.FlawDetector.Models.Sources; // Определяет пространство имён вращательного источника сигнала.

/// <summary> // Описывает назначение класса.
/// Представляет источник базовой вращательной вибрации колёсной пары и буксового узла. // Уточняет физическую роль источника.
/// </summary> // Завершает XML-документацию класса.
internal sealed class WheelRotationSignalSource : SignalSourceBase // Объявляет источник вращательной вибрации.
{ // Начинает тело класса.
    /// <summary> // Документирует конструктор.
    /// Инициализирует новый источник вращательной вибрации. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public WheelRotationSignalSource(bool isEnabled, double amplitudeScale, double harmonicContent, bool includeEccentricity) // Объявляет конструктор источника.
        : base(SignalSourceType.WheelRotation, "Вращение колесной пары", isEnabled) // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
        AmplitudeScale = amplitudeScale; // Сохраняет масштаб амплитуды вращения.
        HarmonicContent = harmonicContent; // Сохраняет насыщенность гармониками.
        IncludeEccentricity = includeEccentricity; // Сохраняет признак эксцентриситета.
    } // Завершает тело конструктора.

    /// <summary> // Документирует масштаб амплитуды.
    /// Получает масштаб амплитуды вращательной вибрации. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double AmplitudeScale { get; } // Хранит масштаб амплитуды вращения.

    /// <summary> // Документирует насыщенность гармониками.
    /// Получает степень насыщения вращательного сигнала дополнительными гармониками. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double HarmonicContent { get; } // Хранит насыщенность гармониками.

    /// <summary> // Документирует эксцентриситет.
    /// Получает признак добавления эксцентриситета к вращательному сигналу. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool IncludeEccentricity { get; } // Хранит признак эксцентриситета.

    /// <inheritdoc /> // Наследует документацию метода.
    public override void Accumulate(SignalSourceContext context, IReadOnlyList<FaultDefinition> faults, ref double xSignal, ref double ySignal, ref double zSignal) // Добавляет вклад вращательной вибрации.
    { // Начинает тело метода.
        double wheelUnroundness = AmplitudeScale * 0.16d * context.LoadScale * Math.Sin(context.RotationPhase); // Формирует основную гармонику вращения.
        wheelUnroundness += AmplitudeScale * 0.05d * HarmonicContent * Math.Sin(2d * context.RotationPhase); // Добавляет вторую гармонику вращения.
        wheelUnroundness += AmplitudeScale * 0.03d * HarmonicContent * Math.Cos(3d * context.RotationPhase); // Добавляет третью гармонику вращения.
        if (IncludeEccentricity) // Проверяет необходимость добавления эксцентриситета.
        { // Начинает ветку эксцентриситета.
            wheelUnroundness += AmplitudeScale * 0.04d * HarmonicContent * Math.Cos(context.RotationPhase + 0.35d); // Добавляет вклад эксцентриситета.
        } // Завершает ветку эксцентриситета.
        xSignal += 0.55d * wheelUnroundness; // Добавляет вклад по оси X.
        ySignal += 0.75d * wheelUnroundness; // Добавляет вклад по оси Y.
        zSignal += 0.35d * wheelUnroundness; // Добавляет вклад по оси Z.
    } // Завершает метод накопления вклада.
}
