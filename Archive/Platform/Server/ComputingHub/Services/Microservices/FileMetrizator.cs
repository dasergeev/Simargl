using Apeiron.Platform.Databases.CentralDatabase;
using Apeiron.Platform.Databases.CentralDatabase.Entities;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу, выполняющую расчёт метрик файлов.
/// </summary>
public sealed class FileMetrizator :
    ServerMicroservice<FileMetrizator>
{
    /// <summary>
    /// Поле для хранения списка идентификаторов файлов.
    /// </summary>
    private readonly List<long> _FileIds;

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
    public FileMetrizator(ILogger<FileMetrizator> logger) :
        base(logger)
    {
        //  Создание списка идентификаторов файлов.
        _FileIds = new();

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

        //  Проверка списка идентификаторов файлов.
        if (_FileIds.Count > 0)
        {
            //  Определение индекса следующего файла.
            int index = _Random.Next() % _FileIds.Count;

            //  Получение идентификатора файла.
            long fileId = _FileIds[index];

            //  Удаление файла из списка.
            _FileIds.RemoveAt(index);

            //  Загрузка файла.
            InternalFile? internalFile = await CentralDatabaseAgent.RequestAsync(
                async (session, cancellationToken) => await session.InternalFiles
                    .FirstOrDefaultAsync(
                        internalFile => internalFile.Id == fileId,
                        cancellationToken)
                    .ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

            //  Проверка файла.
            if (internalFile is not null)
            {
                //  Обновление метрики файла.
                await CentralDatabaseAgent.FileSystem.UpdateFileMetricAsync(
                    internalFile, cancellationToken).ConfigureAwait(false);
            }

            //  Ввод информации в журнал.
            Logger.LogInformation(
                "Файлов: {count}, обработан файл {id}",
                _FileIds.Count, fileId);
        }
        else
        {
            //  Получение идентификаторов файлов.
            long[] ids = await CentralDatabaseAgent.RequestAsync(
                async (session, cancellationToken) => await session.InternalFiles
                    .Select(internalFile => internalFile.Id)
                    .ToArrayAsync(cancellationToken).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

            //  Добавление идентификаторов файлов.
            _FileIds.AddRange(ids);
        }
    }
}
