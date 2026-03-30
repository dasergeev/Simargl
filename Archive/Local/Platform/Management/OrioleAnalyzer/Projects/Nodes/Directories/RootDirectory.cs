using System.IO;
using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет корневой каталог проекта.
/// </summary>
public sealed class RootDirectory :
    Directory<Project>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер.
    /// </param>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="path">
    /// Путь к каталогу.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="parent"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public RootDirectory(Dispatcher dispatcher, Project parent, string path) :
        base(dispatcher, "Исходные данные", parent, path)
    {

    }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {
        //  Получение подкаталогов.
        var subDirectories = new DirectoryInfo(Path).GetDirectories()
            .Select(directory => new 
            {
                Path = directory.FullName,
                IsValid = OriolePathBuilder.TryParseDirectoryTime(directory.Name, out DateTime time),
                Time = time,
            })
            .Where(entity => entity.IsValid)
            .OrderBy(entity => entity.Time)
            .Select(entity => new TimeDirectory(Dispatcher, this, entity.Path, entity.Time));

        //  Добавление подкаталогов.
        NodesProvider.AddRange(subDirectories);
    }
}
