namespace Simargl.FlawDetector.Models.Sources; // Определяет пространство имён математических вспомогательных функций источников.

/// <summary> // Описывает назначение вспомогательного класса.
/// Предоставляет общие математические операции для расчёта вкладов источников сигнала. // Уточняет роль класса в генерации.
/// </summary> // Завершает XML-документацию класса.
internal static class SignalSourceMath // Объявляет статический вспомогательный класс.
{ // Начинает тело класса.
    /// <summary> // Документирует вычисление частоты дефекта.
    /// Возвращает характеристическую частоту для указанного типа дефекта. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="faultType">Тип дефекта подшипника.</param> // Документирует тип дефекта.
    /// <param name="context">Контекст текущего отсчёта.</param> // Документирует расчётный контекст.
    /// <returns>Характеристическая частота дефекта.</returns> // Документирует возвращаемое значение.
    public static double GetFaultFrequency(BearingFaultType faultType, SignalSourceContext context) // Объявляет метод вычисления частоты дефекта.
    { // Начинает тело метода.
        return faultType switch // Выбирает частоту по типу дефекта.
        { // Начинает switch-выражение.
            BearingFaultType.OuterRace => context.Bpfo, // Возвращает частоту наружного кольца.
            BearingFaultType.InnerRace => context.Bpfi, // Возвращает частоту внутреннего кольца.
            BearingFaultType.RollingElement => context.Bsf, // Возвращает частоту тела качения.
            BearingFaultType.Cage => context.Ftf, // Возвращает частоту сепаратора.
            BearingFaultType.Unbalance => context.WheelRotationFrequency, // Возвращает частоту дисбаланса.
            BearingFaultType.Misalignment => 2d * context.WheelRotationFrequency, // Возвращает удвоенную частоту для несоосности.
            _ => context.WheelRotationFrequency, // Возвращает резервную частоту.
        }; // Завершает switch-выражение.
    } // Завершает метод вычисления частоты дефекта.

    /// <summary> // Документирует вычисление псевдошума.
    /// Возвращает гладкий детерминированный шумовой компонент. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="timeSeconds">Абсолютное модельное время.</param> // Документирует время сигнала.
    /// <param name="amplitude">Амплитуда шумовой компоненты.</param> // Документирует амплитуду шума.
    /// <param name="phaseSeed">Фазовый сдвиг для развязки каналов.</param> // Документирует фазовый сдвиг.
    /// <returns>Значение шумовой компоненты.</returns> // Документирует возвращаемое значение.
    public static double GetPseudoNoise(double timeSeconds, double amplitude, double phaseSeed) // Объявляет метод вычисления псевдошума.
    { // Начинает тело метода.
        double harmonicA = Math.Sin((2d * Math.PI * 113d * timeSeconds) + phaseSeed); // Вычисляет первую гармонику шума.
        double harmonicB = Math.Sin((2d * Math.PI * 173d * timeSeconds) + (phaseSeed * 1.7d)); // Вычисляет вторую гармонику шума.
        double harmonicC = Math.Cos((2d * Math.PI * 251d * timeSeconds) + (phaseSeed * 2.3d)); // Вычисляет третью гармонику шума.
        return amplitude * ((0.55d * harmonicA) + (0.3d * harmonicB) + (0.15d * harmonicC)); // Возвращает суммарную шумовую компоненту.
    } // Завершает метод вычисления псевдошума.
}
