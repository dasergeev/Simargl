using Simargl.AccelEth3T.AccelEth3TViewer.Nodes;

namespace Simargl.AccelEth3T.AccelEth3TViewer.Targets.File;

/// <summary>
/// Представляет узел, отображающий файл.
/// </summary>
public class FileNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    public FileNode(Node? parent, string name, string path) :
        base(parent, name)
    {
        //  Установка пути к файлу.
        Path = path;

        _ = this;
    }

    /// <summary>
    /// Возвращает путь к файлу.
    /// </summary>
    public string Path { get; }
}
