namespace Simargl.FlawDetector.Models.Sources; // Определяет пространство имён источника измерительного шума.

/// <summary> // Описывает назначение класса.
/// Представляет источник измерительного шума акселерометра и тракта регистрации. // Уточняет физическую роль источника.
/// </summary> // Завершает XML-документацию класса.
internal sealed class MeasurementNoiseSignalSource : SignalSourceBase // Объявляет источник измерительного шума.
{ // Начинает тело класса.
    /// <summary> // Документирует конструктор.
    /// Инициализирует новый источник измерительного шума. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public MeasurementNoiseSignalSource(bool isEnabled, int spectrumMode, double noiseLevel, bool packetMode) // Объявляет конструктор источника.
        : base(SignalSourceType.MeasurementNoise, "Измерительный шум датчика", isEnabled) // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
        SpectrumMode = spectrumMode; // Сохраняет тип спектра шума.
        NoiseLevel = noiseLevel; // Сохраняет уровень шумовой компоненты.
        PacketMode = packetMode; // Сохраняет признак пакетного режима.
    } // Завершает тело конструктора.

    /// <summary> // Документирует тип спектра.
    /// Получает тип спектра шумовой составляющей. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int SpectrumMode { get; } // Хранит тип спектра шума.

    /// <summary> // Документирует уровень шума.
    /// Получает амплитудный уровень шумовой составляющей. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double NoiseLevel { get; } // Хранит уровень шумовой составляющей.

    /// <summary> // Документирует пакетный режим.
    /// Получает признак пакетной модуляции шума. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool PacketMode { get; } // Хранит признак пакетного режима шума.

    /// <inheritdoc /> // Наследует документацию метода.
    public override void Accumulate(SignalSourceContext context, IReadOnlyList<FaultDefinition> faults, ref double xSignal, ref double ySignal, ref double zSignal) // Добавляет вклад шумовой составляющей.
    { // Начинает тело метода.
        double noiseScale = SpectrumMode switch // Выбирает масштаб спектра шума.
        { // Начинает switch-выражение.
            1 => 0.6d, // Возвращает масштаб для розового шума.
            2 => 1.6d, // Возвращает масштаб для полосового шума.
            _ => 1d, // Возвращает масштаб для белого шума.
        }; // Завершает switch-выражение.
        double burstGate = PacketMode ? 0.45d + (0.55d * Math.Pow(Math.Max(0d, Math.Sin((2d * Math.PI * 0.9d * context.TimeSeconds) + 0.4d)), 6d)) : 1d; // Формирует пакетную модуляцию шума.
        double amplitude = NoiseLevel * noiseScale * burstGate; // Вычисляет итоговую амплитуду шумовой составляющей.
        xSignal += SignalSourceMath.GetPseudoNoise(context.TimeSeconds, amplitude * 0.031d * context.LoadScale, 0.73d); // Добавляет шум по оси X.
        ySignal += SignalSourceMath.GetPseudoNoise(context.TimeSeconds, amplitude * 0.028d * context.LoadScale, 1.91d); // Добавляет шум по оси Y.
        zSignal += SignalSourceMath.GetPseudoNoise(context.TimeSeconds, amplitude * 0.035d * context.LoadScale, 2.57d); // Добавляет шум по оси Z.
    } // Завершает метод накопления вклада.
}
