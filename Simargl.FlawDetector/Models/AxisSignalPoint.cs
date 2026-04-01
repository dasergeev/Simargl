namespace Simargl.FlawDetector.Models; // Определяет пространство имён точек сигнала.

/// <summary> // Описывает назначение record-типа.
/// Представляет одну точку сигнала акселерометра по времени. // Уточняет роль точки на графике.
/// </summary> // Завершает XML-документацию типа.
/// <param name="TimeSeconds">Момент времени в секундах.</param> // Документирует временную координату.
/// <param name="Acceleration">Ускорение в единицах g.</param> // Документирует величину ускорения.
internal sealed record AxisSignalPoint( // Объявляет тип точки сигнала.
    double TimeSeconds, // Хранит время точки.
    double Acceleration); // Хранит ускорение точки.

