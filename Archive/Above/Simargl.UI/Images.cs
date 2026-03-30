using System.Windows;
using System.Windows.Media;

namespace Simargl.UI;

/// <summary>
/// Предоставляет изображения.
/// </summary>
public static class Images
{
    /// <summary>
    /// Поле для хранения изображения "Пауза".
    /// </summary>
    private static ImageSource? _Pause;

    /// <summary>
    /// Поле для хранения изображения "Группа".
    /// </summary>
    private static ImageSource? _Group;

    /// <summary>
    /// Возвращает изображение "Пауза".
    /// </summary>
    public static ImageSource Pause => GetImageSource(ref _Pause, CreatePause);

    /// <summary>
    /// Возвращает изображение "Группа".
    /// </summary>
    public static ImageSource Group => GetImageSource(ref _Group, CreateGroup);

    /// <summary>
    /// Возвращает источник изображения.
    /// </summary>
    /// <param name="refImageSource">
    /// Ссылка на поле, содержащее источник изображения.
    /// </param>
    /// <param name="creator">
    /// Действие, создающее изображение.
    /// </param>
    /// <returns>
    /// Источник изображения.
    /// </returns>
    private static ImageSource GetImageSource(ref ImageSource? refImageSource, Action<DrawingContext> creator)
    {
        //  Проверка изображения.
        if (Volatile.Read(ref refImageSource) is not ImageSource imageSource)
        {
            // Создание DrawingGroup для векторной графики
            DrawingGroup drawingGroup = new();

            // Добавление векторных элементов в DrawingGroup
            using (DrawingContext drawingContext = drawingGroup.Open())
            {
                creator(drawingContext);
            }

            //  Создание источника изображения.
            imageSource = new DrawingImage(drawingGroup);

            //  Установка изображения.
            Interlocked.Exchange(ref refImageSource, imageSource);
        }

        //  Возврат изображения.
        return imageSource;
    }

    /// <summary>
    /// Поле для хранения прозрачного пера.
    /// </summary>
    private static readonly Pen _TransparentPen = new(Brushes.Transparent, 1);

    /// <summary>
    /// Создаёт изображение "Пауза".
    /// </summary>
    /// <param name="context">
    /// Контекст рисования.
    /// </param>
    private static void CreatePause(DrawingContext context)
    {
        //  Ограничивающий прямоугольник.
        context.DrawRectangle(
            Brushes.Transparent,
            _TransparentPen,
            new Rect(0, 0, 15, 15));

        context.DrawRectangle(
            Brushes.LightBlue,
            new Pen(Brushes.DarkBlue, 1),
            new Rect(4, 4, 2, 8));

        context.DrawRectangle(
            Brushes.LightBlue,
            new Pen(Brushes.DarkBlue, 1),
            new Rect(8, 4, 2, 8));
    }

    /// <summary>
    /// Создаёт изображение "Группа".
    /// </summary>
    /// <param name="context">
    /// Контекст рисования.
    /// </param>
    private static void CreateGroup(DrawingContext context)
    {
        //  Ограничивающий прямоугольник.
        context.DrawRectangle(
            Brushes.Transparent,
            new Pen(Brushes.Transparent, 1),
            new Rect(0, 0, 15, 15));

        //  Получение кисти для фона.
        Brush background = Brushes.DarkKhaki;

        //  Создание пера для контура.
        Pen outline = new(Brushes.LightGoldenrodYellow, 1);

        //  Общий фон.
        context.DrawRectangle(background, outline, new Rect(1, 4, 13, 9));

        //  Верхний прямоугольник.
        context.DrawRectangle(background, outline, new Rect(1, 3, 5, 2));
    }
}
