namespace Simargl.FlawDetector.Models.Sources; // Определяет пространство имён источника структурного резонанса.

/// <summary> // Описывает назначение класса.
/// Представляет источник структурного резонанса буксы, рамы и тележки. // Уточняет физическую роль источника.
/// </summary> // Завершает XML-документацию класса.
internal sealed class StructuralResonanceSignalSource : SignalSourceBase // Объявляет источник структурного резонанса.
{ // Начинает тело класса.
    /// <summary> // Документирует конструктор.
    /// Инициализирует новый источник структурного резонанса. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public StructuralResonanceSignalSource(bool isEnabled, int resonanceMode, double dampingScale, bool emphasizeVerticalAxis) // Объявляет конструктор источника.
        : base(SignalSourceType.StructuralResonance, "Структурный резонанс тележки", isEnabled) // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
        ResonanceMode = resonanceMode; // Сохраняет режим резонансного контура.
        DampingScale = dampingScale; // Сохраняет демпфирование резонанса.
        EmphasizeVerticalAxis = emphasizeVerticalAxis; // Сохраняет признак доминирования вертикальной оси.
    } // Завершает тело конструктора.

    /// <summary> // Документирует режим резонанса.
    /// Получает выбранный резонансный контур конструкции. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int ResonanceMode { get; } // Хранит режим резонансного контура.

    /// <summary> // Документирует демпфирование.
    /// Получает коэффициент демпфирования резонансного отклика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double DampingScale { get; } // Хранит коэффициент демпфирования резонанса.

    /// <summary> // Документирует доминирование вертикальной оси.
    /// Получает признак усиления отклика по оси Z. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool EmphasizeVerticalAxis { get; } // Хранит признак доминирования вертикальной оси.

    /// <inheritdoc /> // Наследует документацию метода.
    public override void Accumulate(SignalSourceContext context, IReadOnlyList<FaultDefinition> faults, ref double xSignal, ref double ySignal, ref double zSignal) // Добавляет вклад структурного резонанса.
    { // Начинает тело метода.
        double resonanceBaseFrequency = ResonanceMode switch // Выбирает базовую частоту резонанса.
        { // Начинает switch-выражение.
            1 => 26d, // Возвращает частоту резонанса буксы.
            2 => 44d, // Возвращает частоту резонанса тележки.
            _ => 38d, // Возвращает частоту резонанса рамы.
        }; // Завершает switch-выражение.
        double dampingFactor = 1d / (0.4d + DampingScale); // Вычисляет демпфирование резонансного контура.
        double structuralResonance = 0.09d * context.LoadScale * Math.Sin((2d * Math.PI * resonanceBaseFrequency * context.TimeSeconds) + 0.7d); // Формирует базовый резонансный отклик.
        structuralResonance *= 1d + (0.15d * dampingFactor * Math.Sin(0.5d * context.RotationPhase)); // Добавляет дыхание резонансной моды.
        xSignal += 0.32d * structuralResonance; // Добавляет вклад по оси X.
        ySignal += 0.24d * structuralResonance; // Добавляет вклад по оси Y.
        zSignal += (EmphasizeVerticalAxis ? 1.05d : 0.74d) * structuralResonance; // Добавляет вклад по оси Z.
    } // Завершает метод накопления вклада.
}
