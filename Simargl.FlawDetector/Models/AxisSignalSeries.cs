namespace Simargl.FlawDetector.Models; // Определяет пространство имён серий сигнала.

/// <summary> // Описывает назначение record-типа.
/// Представляет временной ряд по одной оси акселерометра. // Уточняет роль серии сигнала.
/// </summary> // Завершает XML-документацию типа.
/// <param name="AxisName">Имя оси датчика.</param> // Документирует имя оси.
/// <param name="Stroke">Цвет отрисовки серии.</param> // Документирует цвет линии.
/// <param name="Points">Набор точек сигнала.</param> // Документирует набор точек графика.
/// <param name="PeakAcceleration">Пиковое ускорение по модулю.</param> // Документирует пиковое значение.
/// <param name="RootMeanSquare">Среднеквадратичное значение сигнала.</param> // Документирует RMS.
internal sealed record AxisSignalSeries( // Объявляет тип временного ряда.
    string AxisName, // Хранит имя оси.
    Color Stroke, // Хранит цвет линии графика.
    IReadOnlyList<AxisSignalPoint> Points, // Хранит точки графика.
    double PeakAcceleration, // Хранит пиковое ускорение.
    double RootMeanSquare); // Хранит RMS сигнала.

