using Simargl.Frames;

namespace Simargl.Border.Processing.Support;

/// <summary>
/// Представляет связанный канал.
/// </summary>
/// <param name="name">
/// Имя канала.
/// </param>
/// <param name="path">
/// Путь к каналу.
/// </param>
/// <param name="frame">
/// Кадр.
/// </param>
public sealed class LinkedChannel(string name, string path, Frame frame)
{
    /// <summary>
    /// Возвращает имя канала.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Возвращает массив значений.
    /// </summary>
    public double[] Items { get; } = frame.Channels[0].Items;

    /// <summary>
    /// Сохраняет данные.
    /// </summary>
    public void Save()
    {
        //  Сохранение кадра.
        frame.Save(path, StorageFormat.TestLab);
    }
}
