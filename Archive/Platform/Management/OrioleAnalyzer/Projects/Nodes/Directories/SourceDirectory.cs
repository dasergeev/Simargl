using System.IO;
using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет каталог исходных данных.
/// </summary>
public class SourceDirectory :
    Directory<TimeDirectory>
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
    /// <param name="format">
    /// Формат данных каталога.
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="format"/> передано значение,
    /// которое не содержится в перечислении <see cref="DataFormat"/>.
    /// </exception>
    public SourceDirectory(Dispatcher dispatcher, TimeDirectory parent, string path, DataFormat format) :
        base(dispatcher, new DirectoryInfo(path).Name, parent, path)
    {
        //  Установка формата данных каталога.
        Format = Check.IsDefined(format, nameof(format));
    }

    /// <summary>
    /// Возвращает формат данных каталога.
    /// </summary>
    public DataFormat Format { get; }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {
        //  Получение файлов.
        var paths = new DirectoryInfo(Path).GetFiles()
            .Select(file => new
            {
                Path = file.FullName,
                IsValid = OriolePathBuilder.TryParseFileName(file.Name, out DateTime time),
                Time = time,
            })
            .Where(entity => entity.IsValid)
            .OrderBy(entity => entity.Time);

        if (Format == DataFormat.Geolocation)
        {
            //  Добавление файлов.
            NodesProvider.AddRange(paths.Select(
                entity => new GeolocationFile(Dispatcher, this, entity.Path, entity.Time)));
        }
        else
        {
            //  Добавление файлов.
            NodesProvider.AddRange(paths.Select(
                entity => new PackageFile(Dispatcher, this, entity.Path, entity.Time)));
        }
    }
}
