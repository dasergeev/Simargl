namespace Simargl.FlawDetector.Models.Sources; // Определяет пространство имён источника дефектных импульсов.

/// <summary> // Описывает назначение класса.
/// Представляет источник дефектных импульсов подшипника и их высокочастотного резонансного отклика. // Уточняет физическую роль источника.
/// </summary> // Завершает XML-документацию класса.
internal sealed class DefectImpulseSignalSource : SignalSourceBase // Объявляет источник дефектных импульсов.
{ // Начинает тело класса.
    /// <summary> // Документирует конструктор.
    /// Инициализирует новый источник дефектных импульсов. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public DefectImpulseSignalSource(bool isEnabled, int responseMode, double energyScale, double sharpnessScale, bool includeSidebands) // Объявляет конструктор источника.
        : base(SignalSourceType.DefectImpulses, "Импульсы дефектов подшипника", isEnabled) // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
        ResponseMode = responseMode; // Сохраняет режим дефектного отклика.
        EnergyScale = energyScale; // Сохраняет энергию импульсов.
        SharpnessScale = sharpnessScale; // Сохраняет остроту импульсов.
        IncludeSidebands = includeSidebands; // Сохраняет признак добавления боковых полос.
    } // Завершает тело конструктора.

    /// <summary> // Документирует режим отклика.
    /// Получает выбранный режим дефектного отклика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int ResponseMode { get; } // Хранит режим дефектного отклика.

    /// <summary> // Документирует энергию импульсов.
    /// Получает масштаб энергии импульсного отклика. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double EnergyScale { get; } // Хранит энергию импульсов.

    /// <summary> // Документирует остроту импульсов.
    /// Получает остроту формируемых дефектных импульсов. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double SharpnessScale { get; } // Хранит остроту импульсов.

    /// <summary> // Документирует боковые полосы.
    /// Получает признак добавления боковых полос модуляции. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool IncludeSidebands { get; } // Хранит признак боковых полос модуляции.

    /// <inheritdoc /> // Наследует документацию метода.
    public override void Accumulate(SignalSourceContext context, IReadOnlyList<FaultDefinition> faults, ref double xSignal, ref double ySignal, ref double zSignal) // Добавляет вклад дефектных импульсов.
    { // Начинает тело метода.
        foreach (FaultDefinition fault in faults.Where(static fault => fault.IsEnabled && fault.Severity > 0d)) // Перебирает активные дефекты.
        { // Начинает обработку дефекта.
            double excitationFrequency = SignalSourceMath.GetFaultFrequency(fault.FaultType, context); // Получает характеристическую частоту дефекта.
            double excitationPhase = 2d * Math.PI * excitationFrequency * context.TimeSeconds; // Вычисляет фазу повторения ударов дефекта.
            double impulsePower = 4d + (2d * SharpnessScale); // Определяет остроту ударного импульса.
            double impulseEnvelope = Math.Pow(Math.Max(0d, Math.Sin(excitationPhase)), impulsePower); // Формирует ударный импульс.
            double carrierFrequency = ResponseMode switch // Выбирает частоту носителя резонанса.
            { // Начинает switch-выражение.
                1 => 620d, // Возвращает частоту резонансного режима.
                2 => 760d, // Возвращает частоту модулированного режима.
                _ => 420d, // Возвращает частоту широкополосного режима.
            }; // Завершает switch-выражение.
            double resonanceCarrier = Math.Sin((2d * Math.PI * carrierFrequency * context.TimeSeconds) + (0.35d * excitationFrequency)); // Формирует высокочастотный носитель.
            double modulation = 1d + (0.18d * Math.Sin(context.RotationPhase + (0.8d * fault.Severity))); // Добавляет вращательную модуляцию.
            if (IncludeSidebands) // Проверяет необходимость боковых полос модуляции.
            { // Начинает ветку боковых полос.
                modulation += 0.12d * Math.Cos((0.5d * excitationPhase) + 0.25d); // Добавляет боковые полосы модуляции.
            } // Завершает ветку боковых полос.
            double faultAmplitude = EnergyScale * fault.ImpactFactor * fault.Severity * context.LoadScale * modulation; // Вычисляет интенсивность отклика дефекта.
            switch (fault.FaultType) // Выбирает модель пространственного отклика по типу дефекта.
            { // Начинает ветвление по типу дефекта.
                case BearingFaultType.OuterRace: // Обрабатывает дефект наружного кольца.
                    xSignal += 0.55d * faultAmplitude * impulseEnvelope * resonanceCarrier; // Добавляет продольный отклик наружного кольца.
                    ySignal += 0.42d * faultAmplitude * impulseEnvelope * Math.Cos((2d * Math.PI * 540d * context.TimeSeconds) + 0.4d); // Добавляет поперечный отклик наружного кольца.
                    zSignal += 1.15d * faultAmplitude * impulseEnvelope * Math.Sin((2d * Math.PI * 700d * context.TimeSeconds) + 0.2d); // Добавляет вертикальный отклик наружного кольца.
                    break; // Завершает обработку дефекта наружного кольца.
                case BearingFaultType.InnerRace: // Обрабатывает дефект внутреннего кольца.
                    xSignal += 0.95d * faultAmplitude * impulseEnvelope * Math.Sin((2d * Math.PI * 650d * context.TimeSeconds) + context.RotationPhase); // Добавляет продольный отклик внутреннего кольца.
                    ySignal += 0.75d * faultAmplitude * impulseEnvelope * Math.Cos((2d * Math.PI * 500d * context.TimeSeconds) + (0.3d * context.RotationPhase)); // Добавляет поперечный отклик внутреннего кольца.
                    zSignal += 0.9d * faultAmplitude * impulseEnvelope * resonanceCarrier; // Добавляет вертикальный отклик внутреннего кольца.
                    break; // Завершает обработку дефекта внутреннего кольца.
                case BearingFaultType.RollingElement: // Обрабатывает дефект тела качения.
                    xSignal += 0.48d * faultAmplitude * impulseEnvelope * Math.Sin((2d * Math.PI * 780d * context.TimeSeconds) + 0.5d); // Добавляет продольный отклик ролика.
                    ySignal += 0.68d * faultAmplitude * impulseEnvelope * Math.Cos((2d * Math.PI * 810d * context.TimeSeconds) + context.RotationPhase); // Добавляет поперечный отклик ролика.
                    zSignal += 0.88d * faultAmplitude * impulseEnvelope * Math.Sin((2d * Math.PI * 760d * context.TimeSeconds) + 0.15d); // Добавляет вертикальный отклик ролика.
                    break; // Завершает обработку дефекта ролика.
                case BearingFaultType.Cage: // Обрабатывает дефект сепаратора.
                    xSignal += 0.25d * faultAmplitude * Math.Sin(excitationPhase) * (1d + (0.1d * resonanceCarrier)); // Добавляет продольный отклик сепаратора.
                    ySignal += 0.34d * faultAmplitude * Math.Sin(excitationPhase + 0.45d); // Добавляет поперечный отклик сепаратора.
                    zSignal += 0.52d * faultAmplitude * Math.Sin(excitationPhase + 0.9d); // Добавляет вертикальный отклик сепаратора.
                    break; // Завершает обработку дефекта сепаратора.
                case BearingFaultType.Unbalance: // Обрабатывает дисбаланс узла.
                    xSignal += 0.85d * faultAmplitude * Math.Sin(context.RotationPhase + 0.12d); // Добавляет продольный отклик дисбаланса.
                    ySignal += 1.1d * faultAmplitude * Math.Cos(context.RotationPhase + 0.3d); // Добавляет поперечный отклик дисбаланса.
                    zSignal += 0.3d * faultAmplitude * Math.Sin((2d * context.RotationPhase) + 0.15d); // Добавляет вертикальный отклик дисбаланса.
                    break; // Завершает обработку дисбаланса.
                case BearingFaultType.Misalignment: // Обрабатывает несоосность узла.
                    xSignal += 0.72d * faultAmplitude * Math.Sin((2d * context.RotationPhase) + 0.15d); // Добавляет продольный отклик несоосности.
                    ySignal += 0.63d * faultAmplitude * Math.Cos((2d * context.RotationPhase) + 0.1d); // Добавляет поперечный отклик несоосности.
                    zSignal += 0.44d * faultAmplitude * Math.Sin(context.RotationPhase + 0.45d); // Добавляет вертикальный отклик несоосности.
                    break; // Завершает обработку несоосности.
            } // Завершает ветвление по типу дефекта.
        } // Завершает обработку активных дефектов.
    } // Завершает метод накопления вклада.
}
