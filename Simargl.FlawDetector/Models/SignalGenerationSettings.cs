using Simargl.FlawDetector.Models.Sources; // Подключает общий интерфейс источников сигнала.

namespace Simargl.FlawDetector.Models; // Определяет пространство имён настроек генерации сигнала.

/// <summary> // Описывает назначение record-типа.
/// Содержит полный набор параметров для расчёта трёхкоординатного вибросигнала. // Уточняет область применения настроек.
/// </summary> // Завершает XML-документацию типа.
/// <param name="WagonSpeedKilometersPerHour">Скорость вагона в километрах в час.</param> // Документирует скорость вагона.
/// <param name="WheelDiameterMillimeters">Диаметр колеса в миллиметрах.</param> // Документирует геометрию колеса.
/// <param name="AxleLoadKilonewtons">Нагрузка на ось в килоньютонах.</param> // Документирует нагрузку на узел.
/// <param name="BearingPitchDiameterMillimeters">Средний диаметр дорожки качения подшипника в миллиметрах.</param> // Документирует диаметр подшипника.
/// <param name="RollingElementDiameterMillimeters">Диаметр тела качения в миллиметрах.</param> // Документирует диаметр ролика.
/// <param name="RollingElementCount">Количество тел качения.</param> // Документирует количество тел качения.
/// <param name="ContactAngleDegrees">Контактный угол в градусах.</param> // Документирует контактный угол.
/// <param name="SampleRate">Частота дискретизации сигнала в герцах.</param> // Документирует частоту дискретизации.
/// <param name="DurationSeconds">Длительность сигнала в секундах.</param> // Документирует длительность сигнала.
/// <param name="Sources">Коллекция источников сигнала.</param> // Документирует список источников сигнала.
/// <param name="Faults">Коллекция активных и неактивных дефектов.</param> // Документирует список дефектов.
internal sealed record SignalGenerationSettings( // Объявляет набор параметров генерации.
    double WagonSpeedKilometersPerHour, // Хранит скорость вагона.
    double WheelDiameterMillimeters, // Хранит диаметр колеса.
    double AxleLoadKilonewtons, // Хранит осевую нагрузку.
    double BearingPitchDiameterMillimeters, // Хранит средний диаметр подшипника.
    double RollingElementDiameterMillimeters, // Хранит диаметр тела качения.
    int RollingElementCount, // Хранит число тел качения.
    double ContactAngleDegrees, // Хранит контактный угол.
    int SampleRate, // Хранит частоту дискретизации.
    double DurationSeconds, // Хранит длительность сигнала.
    IReadOnlyList<ISignalSource> Sources, // Хранит набор источников сигнала.
    IReadOnlyList<FaultDefinition> Faults); // Хранит набор дефектов.
