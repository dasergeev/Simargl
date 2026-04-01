using Simargl.FlawDetector.Models; // Подключает перечисление типов источников сигнала.
using Simargl.FlawDetector.Models.Sources; // Подключает модели конкретных источников сигнала.

namespace Simargl.FlawDetector.ViewModels.Sources; // Определяет пространство имён view model источника пути.

/// <summary> // Описывает назначение класса.
/// Представляет настройки источника возбуждения от пути в пользовательском интерфейсе. // Уточняет роль типа в интерфейсе.
/// </summary> // Завершает XML-документацию класса.
internal sealed class TrackExcitationSignalSourceViewModel : SignalSourceViewModelBase // Объявляет view model источника пути.
{ // Начинает тело класса.
    private int profileMode; // Хранит режим профиля пути.
    private double intensityScale; // Хранит интенсивность воздействия.
    private bool includeJointImpacts; // Хранит признак ударов стыков.

    /// <summary> // Документирует доступные режимы профиля.
    /// Получает список режимов возбуждения от пути. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public IReadOnlyList<string> ProfileModes { get; } = ["Стыки и неровности", "Волнообразный профиль", "Случайная колея"]; // Хранит список режимов профиля пути.

    /// <summary> // Документирует конструктор.
    /// Инициализирует новую view model источника возбуждения от пути. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public TrackExcitationSignalSourceViewModel() // Объявляет конструктор view model.
        : base(SignalSourceType.TrackExcitation, "Возбуждение от пути") // Инициализирует базовые свойства источника.
    { // Начинает тело конструктора.
    } // Завершает тело конструктора.

    /// <summary> // Документирует режим профиля.
    /// Получает или задаёт режим профиля пути. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public int ProfileMode // Объявляет свойство режима профиля.
    { // Начинает тело свойства.
        get => this.profileMode; // Возвращает текущий режим профиля.
        set => SetProperty(ref this.profileMode, Math.Clamp(value, 0, ProfileModes.Count - 1)); // Обновляет режим профиля.
    } // Завершает тело свойства.

    /// <summary> // Документирует интенсивность.
    /// Получает или задаёт интенсивность воздействия от пути. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public double IntensityScale // Объявляет свойство интенсивности.
    { // Начинает тело свойства.
        get => this.intensityScale; // Возвращает текущую интенсивность.
        set => SetProperty(ref this.intensityScale, Math.Clamp(value, 0.1d, 4d)); // Обновляет интенсивность.
    } // Завершает тело свойства.

    /// <summary> // Документирует удары стыков.
    /// Получает или задаёт признак включения ударов от стыков рельса. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public bool IncludeJointImpacts // Объявляет свойство ударов стыков.
    { // Начинает тело свойства.
        get => this.includeJointImpacts; // Возвращает текущее состояние ударов стыков.
        set => SetProperty(ref this.includeJointImpacts, value); // Обновляет состояние ударов стыков.
    } // Завершает тело свойства.

    /// <inheritdoc /> // Наследует документацию метода.
    public override ISignalSource ToModel() // Создаёт модель источника возбуждения от пути.
    { // Начинает тело метода.
        return new TrackExcitationSignalSource(IsEnabled, ProfileMode, IntensityScale, IncludeJointImpacts); // Возвращает модель источника пути.
    } // Завершает метод.
}
