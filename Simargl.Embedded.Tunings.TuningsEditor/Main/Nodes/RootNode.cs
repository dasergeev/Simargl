using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Errors;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;
using System.IO;
using System.Text.Json;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

/// <summary>
/// Представляет родительский узел.
/// </summary>
public sealed class RootNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public RootNode(Project project) :
        base(NodeFormat.Root, project)
    {
        //  Создание узлов.
        SpecNode = new(project);
        ErrorNode = new(project);
        GeneratorNode = new(project);

        //  Добавление узлов.
        Nodes.Add(SpecNode);
        Nodes.Add(ErrorNode);
        Nodes.Add(GeneratorNode);
    }

    /// <summary>
    /// Возвращает узел спецификации.
    /// </summary>
    public SpecNode SpecNode { get; }

    /// <summary>
    /// Возвращает узел ошибок.
    /// </summary>
    public ErrorNode ErrorNode { get; }

    /// <summary>
    /// Возвращает узел генерации кода.
    /// </summary>
    public GeneratorNode GeneratorNode { get; }

    /// <summary>
    /// Сохраняет данные в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток данных.
    /// </param>
    public void Save(FileStream stream)
    {
        //  Сброс данных в файле.
        stream.SetLength(0);

        //  Создание настроек средства записи.
        JsonWriterOptions options = new()
        {
            //  Использовать отступы.
            Indented = true,
        };

        //  Создание средства записи.
        using Utf8JsonWriter writer = new(stream, options);

        //  Начало объекта.
        writer.WriteStartObject();

        //  Сохранение узла спецификации.
        writer.WriteStartObject("spec");
        SpecNode.Save(writer);
        writer.WriteEndObject();

        //  Сохранение узла ошибок.
        writer.WriteStartObject("error");
        ErrorNode.Save(writer);
        writer.WriteEndObject();

        //  Сохранение узла генерации.
        writer.WriteStartObject("generator");
        GeneratorNode.Save(writer);
        writer.WriteEndObject();

        //  Завершение объекта.
        writer.WriteEndObject();

        //  Сброс данных в поток.
        writer.Flush();

        //  Сброс данных на диск.
        stream.Flush();
    }

    /// <summary>
    /// Загружает данные из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток данных.
    /// </param>
    public void Load(FileStream stream)
    {
        //  Установка нотока на начальную позицию.
        stream.Position = 0;

        //  Чтение документа.
        using JsonDocument document = JsonDocument.Parse(stream);

        //  Чтение корневого объекта.
        JsonElement element = document.RootElement;

        //  Чтение узла спецификации.
        SpecNode.Load(element.GetProperty("spec"));

        //  Чтение узла ошибок.
        ErrorNode.Load(element.GetProperty("error"));

        //  Чтение узла генерации кода.
        GeneratorNode.Load(element.GetProperty("generator"));
    }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => Nodes.Sum(x => x.GetDataSize());
}
