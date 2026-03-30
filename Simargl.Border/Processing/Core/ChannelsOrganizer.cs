using Simargl.Border.Geometry;
using Simargl.Frames;
using System.IO;

namespace Simargl.Border.Processing.Core;

/// <summary>
/// Представляет организатор каналов.
/// </summary>
/// <param name="processor">
/// Устройство обработки.
/// </param>
/// <param name="sourcePath">
/// Путь к исходным каналам.
/// </param>
/// <param name="targetPath">
/// Путь к целевым каналам.
/// </param>
public sealed class ChannelsOrganizer(Processor processor, string sourcePath, string targetPath) :
    ProcessorUnit(processor)
{
    /// <summary>
    /// Возвращает целевой канал.
    /// </summary>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <returns>
    /// Целевой канал.
    /// </returns>
    public Channel GetTarget(string name)
    {
        return GetTargetFrame(name).Channels[0];
    }

    /// <summary>
    /// Возвращает целевой кадр.
    /// </summary>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <returns>
    /// Целевой кадр.
    /// </returns>
    public Frame GetTargetFrame(string name)
    {
        //  Определение индекса имени.
        int nameIndex = BasisConstants.TargetNames[name];

        //  Определение пути к файлу.
        string path = Path.Combine(targetPath, $"Vp0_0 {name}.{nameIndex:0000}");

        //  Открытие кадра.
        return new(path);
    }

    /// <summary>
    /// Поле для хранения карты исходных файлов.
    /// </summary>
    private readonly Dictionary<string, string> _SourceMap = Enumerable
        .Range(0, processor.SectionGroups.Sources.Count)
        .Select(x => new
        {
            Index = x,
            processor.SectionGroups.Sources[x].Name
        })
        .Select(x => new
        {
            x.Name,
            Path = Path.Combine(sourcePath, $"Vp0_0 {x.Name}.{x.Index:0000}")
        })
        .ToDictionary(x => x.Name, x => x.Path);

    /// <summary>
    /// Возвращает исходный канал.
    /// </summary>
    /// <param name="section">
    /// Сечение.
    /// </param>
    /// <param name="rail">
    /// Рельс.
    /// </param>
    /// <param name="side">
    /// Сторона.
    /// </param>
    /// <param name="index">
    /// Индекс.
    /// </param>
    /// <returns>
    /// Исходный канал.
    /// </returns>
    public Channel GetSource(int section, Rail rail, Side side, int index)
    {
        //  Определение имени канала.
        string name = $"S{(rail == Rail.Left ? "L" : "R")}{(side == Side.External ? "e" : "i")}{section:00}_{index}";

        //  Получение пути к кадру.
        string path = _SourceMap[name];

        //  Загрузка кадра.
        Frame frame = new(path);

        //  Возврат канала.
        return frame.Channels[0];
    }
}
