using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Simargl.Embedded.Tunings.TuningsEditor.Images;

/// <summary>
/// Предоставляет доступ к изображениям.
/// </summary>
public static class ImageMap
{
    /// <summary>
    /// Поле для хранения карты изображений.
    /// </summary>
    private static readonly Dictionary<string, ImageSource> _ImageMap;

    /// <summary>
    /// Инициализирует статические данные.
    /// </summary>
    static ImageMap()
    {
        //  Создание карты изображений.
        _ImageMap = [];
    }

    /// <summary>
    /// Возвращает изображение по умолчанию.
    /// </summary>
    /// <returns>
    /// Изображение по умолчанию.
    /// </returns>
    public static ImageSource GetDefault()
    {
        //  Возврат изображения по умолчанию.
        return GetImage("Default.ico");
    }

    /// <summary>
    /// Возвращает изображение.
    /// </summary>
    /// <param name="path">
    /// Путь к изображению.
    /// </param>
    /// <returns>
    /// Изображение.
    /// </returns>
    public static ImageSource GetImage(string path)
    {
        //  Поиск изображения в карте.
        if (!_ImageMap.TryGetValue(path, out ImageSource? imageSource))
        {
            //  Получение изображения.
            imageSource = new BitmapImage(new Uri($"pack://application:,,,/Images/Data/{path}", UriKind.Absolute));

            //  Добавление изображения в карту.
            _ImageMap.Add(path, imageSource);
        }

        //  Возврат изображения.
        return imageSource;
    }

    /// <summary>
    /// Возвращает изображение.
    /// </summary>
    /// <param name="format">
    /// Формат узла.
    /// </param>
    /// <returns>
    /// Изображение.
    /// </returns>
    public static ImageSource GetImage(NodeFormat format)
    {
        //  Возврат изображения.
        return GetImage(NodeHelper.GetImageName(format));
    }
}
