namespace Simargl.FlawDetector.Models.Sources; // Определяет пространство имён источника возбуждения от пути.

/// <summary> // Описывает назначение класса.
/// Представляет источник низкочастотных возмущений от профиля пути и стыков рельсов. // Уточняет физическую роль источника.
/// </summary> // Завершает XML-документацию класса.
internal sealed class TrackExcitationSignalSource : SignalSourceBase // Объявляет источник возбуждения от пути.
{ // Начинает тело класса.
    /// <summary> // Документирует конструктор.
    /// Инициализирует новый источник возбуждения от пути. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public TrackExcitationSignalSource(bool isEnabled, int profileMode, double intensityScale, bool includeJointImpacts) // Объявляет конструктор источника.
        : base(SignalSourceType.TrackExcitation, "Возбуждение от пути", isEnabled) // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
        ProfileMode = profileMode; // Сохраняет режим профиля пути.
        IntensityScale = intensityScale; // Сохраняет интенсивность воздействия пути.
        IncludeJointImpacts = includeJointImpacts; // Сохраняет признак ударов от стыков.
    } // Завершает тело конструктора.

    /// <summary> // Документирует режим профиля.
    /// Получает режим профиля пути для данного источника. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int ProfileMode { get; } // Хранит режим профиля пути.

    /// <summary> // Документирует интенсивность.
    /// Получает интенсивность воздействия пути. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double IntensityScale { get; } // Хранит интенсивность воздействия пути.

    /// <summary> // Документирует удары от стыков.
    /// Получает признак добавления ударов от стыков рельсов. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool IncludeJointImpacts { get; } // Хранит признак ударов от стыков.

    /// <inheritdoc /> // Наследует документацию метода.
    public override void Accumulate(SignalSourceContext context, IReadOnlyList<FaultDefinition> faults, ref double xSignal, ref double ySignal, ref double zSignal) // Добавляет вклад возбуждения от пути.
    { // Начинает тело метода.
        double trackProfilePhase = 2d * Math.PI * 1.8d * context.TimeSeconds; // Вычисляет фазу воздействия профиля пути.
        double trackExcitation = IntensityScale * 0.1d * context.LoadScale * Math.Sin(trackProfilePhase); // Формирует базовое возбуждение от пути.
        if (ProfileMode == 0) // Проверяет режим стыков пути.
        { // Начинает ветку стыков.
            trackExcitation += 0.08d * Math.Pow(Math.Max(0d, Math.Sin(trackProfilePhase)), 8d); // Добавляет импульсы от стыков.
        } // Завершает ветку стыков.
        else if (ProfileMode == 1) // Проверяет режим гофры пути.
        { // Начинает ветку гофры.
            trackExcitation += 0.05d * Math.Sin(6d * trackProfilePhase + 0.2d); // Добавляет высокочастотную гофру пути.
        } // Завершает ветку гофры.
        else // Обрабатывает случайный профиль пути.
        { // Начинает ветку случайного профиля.
            trackExcitation += SignalSourceMath.GetPseudoNoise(context.TimeSeconds, 0.04d * IntensityScale, 0.37d); // Добавляет случайный профиль пути.
        } // Завершает ветку случайного профиля.
        if (IncludeJointImpacts) // Проверяет необходимость ударов от стыков.
        { // Начинает ветку ударов от стыков.
            trackExcitation += 0.03d * IntensityScale * Math.Pow(Math.Max(0d, Math.Cos(0.5d * trackProfilePhase)), 6d); // Добавляет редкие удары от стыков.
        } // Завершает ветку ударов от стыков.
        xSignal += 0.62d * trackExcitation; // Добавляет вклад по оси X.
        ySignal += 0.45d * trackExcitation; // Добавляет вклад по оси Y.
        zSignal += 0.98d * trackExcitation; // Добавляет вклад по оси Z.
    } // Завершает метод накопления вклада.
}
