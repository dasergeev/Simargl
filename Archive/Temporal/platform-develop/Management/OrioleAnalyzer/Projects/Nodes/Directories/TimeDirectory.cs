using System.IO;
using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет каталог исходных данных сгруппированных по времени.
/// </summary>
public sealed class TimeDirectory :
    Directory<RootDirectory>
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
    /// <param name="time">
    /// Время каталога.
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
    public TimeDirectory(Dispatcher dispatcher, RootDirectory parent, string path, DateTime time) :
        base(dispatcher, new DirectoryInfo(path).Name, parent, path)
    {
        //  Установка времени каталога.
        Time = time;
    }

    /// <summary>
    /// Возвращает время каталога.
    /// </summary>
    public DateTime Time { get; }

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
                IsValid = OriolePathBuilder.TryParseDirectoryFormat(directory.Name, out DataFormat format),
                Format = format,
            })
            .Where(entity => entity.IsValid)
            .OrderBy(entity => entity.Format)
            .Select(entity => new SourceDirectory(Dispatcher, this, entity.Path, entity.Format));

        //  Добавление подкаталогов.
        NodesProvider.AddRange(subDirectories);
    }
}
