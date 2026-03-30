using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет узел дерева проекта, представляющий файл.
/// </summary>
/// <typeparam name="TParentNode">
/// Тип родительского узла.
/// </typeparam>
public abstract class File<TParentNode> :
    ChildNode<TParentNode>
    where TParentNode : Node
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="path">
    /// Путь к каталогу.
    /// </param>
    /// <param name="time">
    /// Время начала записи в файл.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="parent"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public File(Dispatcher dispatcher, string name, TParentNode parent, string path, DateTime time) :
        base(dispatcher, name, parent)
    {
        //  Установка пути к каталогу.
        Path = Check.IsNotNull(path, nameof(path));

        //  Установка времени начала записи в файл.
        Time = time;
    }

    /// <summary>
    /// Возвращает путь к файлу.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Возвращает время начала записи в файл.
    /// </summary>
    public DateTime Time { get; }
}
