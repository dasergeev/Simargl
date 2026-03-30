using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет проект.
/// </summary>
public class Project :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер.
    /// </param>
    /// <param name="rootPath">
    /// Путь к корневому каталогу.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="rootPath"/> передана пустая ссылка.
    /// </exception>
    public Project(Dispatcher dispatcher, string rootPath) :
        base(dispatcher, "Проект")
    {
        //  Установка пути к корневому каталогу.
        RootPath = Check.IsNotNull(rootPath, nameof(rootPath));

        //  Установка значения, определяющего требует ли проект сохранения.
        IsNeedSave = true;

        //  Загрузка узла.
        Load();
    }

    /// <summary>
    /// Возвращает путь к корневому каталогу.
    /// </summary>
    public string RootPath { get; }

    /// <summary>
    /// Возвращает значение, определяющее требует ли проект сохранения.
    /// </summary>
    public bool IsNeedSave { get; }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {
        //  Добавление корневого каталога.
        NodesProvider.Add(new RootDirectory(Dispatcher, this, RootPath));
    }
}
