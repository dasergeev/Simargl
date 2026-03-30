using Simargl.Frames;
using System.IO;

namespace Simargl.Border.Processing.Support;

/// <summary>
/// Представляет связанный кадр.
/// </summary>
/// <param name="path">
/// Путь к каталогу.
/// </param>
/// <param name="map">
/// Крата отображения имён.
/// </param>
/// <param name="length">
/// Длина канала.
/// </param>
public sealed class LinkedFrame(string path, Func<string, int> map, int length)
{
    /// <summary>
    /// Возвращает канал с указанным именем.
    /// </summary>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <returns>
    /// Канал с указанным именем.
    /// </returns>
    public LinkedChannel this[string name]
    {
        get
        {
            //  Определение индекса имени.
            int nameIndex = map(name);

            //  Определение пути к файлу.
            string filePath = Path.Combine(path, $"Vp0_0 {name}.{nameIndex:0000}");

            //  Кадр.
            Frame frame;

            //  Проверка существования файла.
            if (File.Exists(filePath))
            {
                //  Загрузка кадра.
                frame = new(filePath);
            }
            else
            {
                //  Создание кадра.
                frame = new();

                //  Создание канала.
                Channel channel = new("channel", string.Empty, 2000, 1000, length);

                //  Добавление канала.
                frame.Channels.Add(channel);
            }

            //  Возврат связанного канала.
            return new(name, filePath, frame);
        }
    }
}
