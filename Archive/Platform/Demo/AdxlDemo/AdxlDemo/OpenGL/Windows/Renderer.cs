namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;

/// <summary>
/// Представляет средство рендеринга OpenGL.
/// </summary>
public sealed class Renderer
{
    /// <summary>
    /// Поле для хранения средства рендеринга текста.
    /// </summary>
    private readonly TextRenderer _TextRenderer;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="textRenderer">
    /// Средство рендеринга текста.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="textRenderer"/> передана пустая ссылка.
    /// </exception>
    public Renderer(TextRenderer textRenderer)
    {
        //  Установка средства рендеринга текста.
        _TextRenderer = IsNotNull(textRenderer, nameof(textRenderer));
    }

    /// <summary>
    /// Выполняет рендеринг текста.
    /// </summary>
    /// <param name="x">
    /// Координата положения точки вывода по оси Ox.
    /// </param>
    /// <param name="y">
    /// Координата положения точки вывода по оси Oy.
    /// </param>
    /// <param name="text">
    /// Текст, который необходимо растрировать.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="text"/> передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public void Text(double x, double y, string text)
    {
        //  Рендеринг текста.
        _TextRenderer.Rasterize(x, y, text);
    }

    /// <summary>
    /// Устанавливает текущий цвет.
    /// </summary>
    /// <param name="color">
    /// Цвет, который необходимо установить текущим.
    /// </param>
    public void Color(System.Drawing.Color color)
    {
        //  Для анализатора.
        _ = this;

        //  Установка текущего цвета.
        Original.Color(color.R, color.G, color.B, color.A);
    }
}
