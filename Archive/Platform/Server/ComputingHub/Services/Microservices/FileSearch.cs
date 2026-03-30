using Apeiron.Platform.Databases.CentralDatabase;
using Apeiron.Platform.Databases.CentralDatabase.Entities;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу, выполняющую поиск файлов.
/// </summary>
public sealed class FileSearch :
    ServerMicroservice<FileSearch>
{
    /// <summary>
    /// Поле для хранения списка путей к файлам.
    /// </summary>
    private readonly List<string> _FilePaths;

    /// <summary>
    /// Поле для хранения генератора случайных чисел.
    /// </summary>
    private readonly Random _Random;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public FileSearch(ILogger<FileSearch> logger) :
        base(logger)
    {
        //  Создание списка путей к файлам.
        _FilePaths = new();

        //  Создание генератора случайных чисел.
        _Random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));
    }

    /// <summary>
    /// Асинхронно выполняет шаг работы микрослужбы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая шаг работы микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override sealed async ValueTask MakeStepAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка списка путей к файлам.
        if (_FilePaths.Count > 0)
        {
            //  Определение индекса следующего файла.
            int index = _Random.Next() % _FilePaths.Count;

            //  Получение пути к файлу.
            string filePath = _FilePaths[index];

            //  Проверка пути к файлу.
            if (File.Exists(filePath))
            {
                //  Регистрация файла.
                await CentralDatabaseAgent.FileSystem.FileRegistrationAsync(
                    filePath, DateTime.Now, cancellationToken)
                    .ConfigureAwait(false);
            }

            //  Удаление файла из списка.
            _FilePaths.RemoveAt(index);

            //  Ввод ифнормации в журнал.
            Logger.LogInformation(
                "Файлов: {count}, обработан файл {path}",
                _FilePaths.Count, filePath);
        }
        else
        {
            //  Получение списка общих каталогов.
            GeneralDirectory[] generalDirectories = await CentralDatabaseAgent.RequestAsync(
                async (session, cancellationToken) => await session.GeneralDirectories
                    .ToArrayAsync(cancellationToken).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение всех путей общих каталогов.
            IEnumerable<DirectoryInfo> generalDirectoryInfos = generalDirectories
                .Select(directory => directory.GetAbsolutePaths()
                    .FirstOrDefault(path => System.IO.Directory.Exists(path)))
                .Where(path => path is not null)
                .Select(path => new DirectoryInfo(path!));

            //  Параллельный перебор всех путей общих каталогов.
            await Parallel.ForEachAsync(
                generalDirectoryInfos,
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                },
                LoadFilePathsAsync).ConfigureAwait(false);

            //  Вывод информации в журнал.
            Logger.LogInformation("Найдено файлов: {count}", _FilePaths.Count);
        }
    }

    /// <summary>
    /// Асинхронно загружает пути к файлам.
    /// </summary>
    /// <param name="directoryInfo">
    /// Информация о каталоге.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, асинхронно загружающая пути к файлам.
    /// </returns>
    private async ValueTask LoadFilePathsAsync(DirectoryInfo directoryInfo, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Параллельный перебор вложенных каталогов.
        await Parallel.ForEachAsync(
            directoryInfo.GetDirectories(),
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            LoadFilePathsAsync).ConfigureAwait(false);

        //  Получение массива путей к файлам.
        string[] filePaths = directoryInfo.GetFiles().Select(fileInfo => fileInfo.FullName).ToArray();

        //  Блокировка критического объекта
        lock (_FilePaths)
        {
            //  Добавление файлов в общий список.
            _FilePaths.AddRange(filePaths);
        }
    }
}
