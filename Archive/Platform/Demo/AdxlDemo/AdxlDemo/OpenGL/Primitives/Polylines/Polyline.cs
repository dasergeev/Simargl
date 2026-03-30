using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;
using System.Numerics;
using System.Windows.Media;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;

/// <summary>
/// Представляет ломаную линию.
/// </summary>
public sealed class Polyline :
    Primitive
{
    /// <summary>
    /// Поле для хранения массива вершин.
    /// </summary>
    private readonly Vector2[] _Vertexes;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Polyline()
    {
        //  Установка значений для пустой линии.
        _Vertexes = Array.Empty<Vector2>();
        IsEmpty = true;
        Color = default;
        Width = default;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sequence">
    /// Последовательность значений.
    /// </param>
    /// <param name="sampling">
    /// Частота дискретизации.
    /// </param>
    /// <param name="xBegin">
    /// Начальное значение по оси Ox.
    /// </param>
    /// <param name="color">
    /// Цвет линии.
    /// </param>
    /// <param name="width">
    /// Ширина линии.
    /// </param>
    public Polyline(double[] sequence, double sampling, double xBegin, System.Drawing.Color color, double width)
    {
        //  Проверка пустой линии.
        if (sequence is null || sequence.Length < 2)
        {
            _Vertexes = Array.Empty<Vector2>();
            IsEmpty = true;
        }
        else
        {
            IsEmpty = false;

            //  Создание массива вершин.
            _Vertexes = new Vector2[sequence.Length];

            //  Определение шага по оси Ox.
            double xStep = 1 / sampling;

            //  Установка предельных значений.
            XMin = xBegin;
            XMax = xBegin + (sequence.Length - 1) * xStep;
            YMin = double.MaxValue;
            YMax = double.MinValue;

            //  Заполнение массива вершин.
            for (int i = 0; i < sequence.Length; i++)
            {
                //  Определение текущего значения.
                double value = sequence[i];

                //  Коректировка предельных значений.
                if (value < YMin) YMin = value;
                if (value > YMax) YMax = value;

                //  Определение очередной вершины.
                _Vertexes[i] = new((float)(xBegin + i * xStep), (float)value);
            }
        }

        //  Установка значений свойств отображения.
        Color = color;
        Width = width;
    }

    /// <summary>
    /// Возвращает значение, определяющее, является ли линия пустой.
    /// </summary>
    public bool IsEmpty { get; }

    /// <summary>
    /// Возвращает цвет линии.
    /// </summary>
    public System.Drawing.Color Color { get; }

    /// <summary>
    /// Возвращает ширину линии.
    /// </summary>
    public double Width { get; }

    /// <summary>
    /// Возвращает минимальное значение по оси Ox.
    /// </summary>
    public double XMin { get; }

    /// <summary>
    /// Возвращает максимального значение по оси Ox.
    /// </summary>
    public double XMax { get; }

    /// <summary>
    /// Возвращает минимальное значение по оси Oy.
    /// </summary>
    public double YMin { get; }

    /// <summary>
    /// Возвращает максимального значение по оси Oy.
    /// </summary>
    public double YMax { get; }

    /// <summary>
    /// Выполняет рендеринг.
    /// </summary>
    /// <param name="renderer">
    /// Средство рендеринга.
    /// </param>
    public override void Render(Renderer renderer)
    {
        //  Проверка необходимости отображения.
        if (!IsEmpty)
        {
            //  Установка цвета линии.
            Original.Color(Color.R, Color.G, Color.B);

            //  Установка ширины линии.
            Original.LineWidth((float)Width);

            //  Начало отображение ломаной.
            Original.Begin(BeginMode.LineStrip);

            //  Перебор вершин.
            foreach (Vector2 vertex in _Vertexes)
            {
                //  Добавление вершины.
                Original.Vertex(vertex.X, vertex.Y);
            }

            //  Завершение отображения ломаной.
            Original.End();
        }
    }
}
