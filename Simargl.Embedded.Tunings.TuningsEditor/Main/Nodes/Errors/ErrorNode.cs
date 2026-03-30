using Simargl.Embedded.Tunings.TuningsEditor.Core;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Errors;

/// <summary>
/// Представляет узел ошибок.
/// </summary>
public sealed class ErrorNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public ErrorNode(Project project) :
        base(NodeFormat.Error, project)
    {
        //  Установка имени.
        Name.Value = "Коды ошибок";

        //  Создание команд.
        AddValueCommand = new(true, delegate
        {
            //  Добавление параметра.
            Nodes.Add(new ErrorValueNode(project));
        });
    }

    /// <summary>
    /// Возвращает команду добавления значения.
    /// </summary>
    public Command AddValueCommand { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => 0;
}
