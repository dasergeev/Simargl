using Simargl.FlawDetector.Models; // Подключает перечисление типов источников сигнала.
using Simargl.FlawDetector.Models.Sources; // Подключает общий интерфейс моделей источников.

namespace Simargl.FlawDetector.ViewModels.Sources; // Определяет пространство имён базовой view model источника.

/// <summary> // Описывает назначение класса.
/// Реализует общую часть view model источников сигнала и задаёт общий абстрактный интерфейс. // Уточняет архитектурную роль типа.
/// </summary> // Завершает XML-документацию класса.
internal abstract class SignalSourceViewModelBase : ObservableObject, ISignalSourceViewModel // Объявляет базовый абстрактный класс view model источника.
{ // Начинает тело класса.
    private bool isEnabled; // Хранит признак включения источника.

    /// <summary> // Документирует конструктор.
    /// Инициализирует общие свойства view model источника сигнала. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    /// <param name="sourceType">Тип источника сигнала.</param> // Документирует тип источника.
    /// <param name="displayName">Отображаемое имя источника.</param> // Документирует отображаемое имя.
    protected SignalSourceViewModelBase(SignalSourceType sourceType, string displayName) // Объявляет конструктор базовой view model.
    { // Начинает тело конструктора.
        SourceType = sourceType; // Сохраняет тип источника.
        DisplayName = displayName; // Сохраняет отображаемое имя.
    } // Завершает тело конструктора.

    /// <inheritdoc /> // Наследует документацию свойства.
    public SignalSourceType SourceType { get; } // Хранит тип источника сигнала.

    /// <inheritdoc /> // Наследует документацию свойства.
    public string DisplayName { get; } // Хранит отображаемое имя источника.

    /// <inheritdoc /> // Наследует документацию свойства.
    public bool IsEnabled // Объявляет признак включения источника.
    { // Начинает тело свойства.
        get => this.isEnabled; // Возвращает текущее состояние включения источника.
        set => SetProperty(ref this.isEnabled, value); // Обновляет состояние включения источника.
    } // Завершает тело свойства.

    /// <inheritdoc /> // Наследует документацию метода.
    public abstract ISignalSource ToModel(); // Создаёт модель источника сигнала.
}
