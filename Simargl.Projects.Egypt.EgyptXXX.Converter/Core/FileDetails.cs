using Simargl.Payload;
using System.IO;

namespace Simargl.Projects.Egypt.EgyptXXX.Converter.Core;

/// <summary>
/// Представляет ифнормацию о файле.
/// </summary>
public class FileDetails
{
    /// <summary>
    /// Поле для хранения пакетов данных.
    /// </summary>
    private DataPackage[] _DataPackages = [];

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат данных файла.
    /// </param>
    /// <param name="fileInfo">
    /// Информация о файле.
    /// </param>
    /// <param name="source">
    /// Источник данных файла.
    /// </param>
    /// <param name="time">
    /// Время файла.
    /// </param>
    private FileDetails(FileFormat format, FileInfo fileInfo, string source, DateTime time)
    {
        //  Установка значений основных свойств.
        Format = format;
        FileInfo = fileInfo;
        Source = source;
        Time = time;
    }

    /// <summary>
    /// Возвращает значение, определяющее формат данных файла.
    /// </summary>
    public FileFormat Format { get; }

    /// <summary>
    /// Возвращает информацию о файле.
    /// </summary>
    public FileInfo FileInfo { get; }

    /// <summary>
    /// Возвращает источник данных файла.
    /// </summary>
    public string Source { get; }

    /// <summary>
    /// Возвращает время файла.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Возвращает данные пакетов.
    /// </summary>
    public IEnumerable<DataPackage> DataPackages => _DataPackages;

    /// <summary>
    /// Асинхронно создаёт информацию о файле.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат данных файла.
    /// </param>
    /// <param name="fileInfo">
    /// Информация о файле.
    /// </param>
    /// <param name="source">
    /// Источник данных файла.
    /// </param>
    /// <param name="time">
    /// Время файла.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая информацию о файле.
    /// </returns>
    public static async Task<FileDetails> CreateAsync(
        FileFormat format, FileInfo fileInfo, string source, DateTime time,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Ожидание завершённой задачи.
        await Task.CompletedTask.ConfigureAwait(false);

        //  Создание информации о файле.
        FileDetails details = new(format, fileInfo, source, time);

        //  Возврат информации о файле.
        return details;
    }

    /// <summary>
    /// Асинхронно заргужает данные.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные.
    /// </returns>
    public async Task LoadAsync(CancellationToken cancellationToken)
    {
        //  Создание списка пакетов.
        List<DataPackage> dataPackages = [];

        //  Открытие потока.
        await using FileStream stream = new(FileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);

        //  Блок перехвата исключений.
        try
        {
            //  Основной цикл чтения.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Чтение пакета.
                dataPackages.Add(await DataPackage.LoadAsync(stream, cancellationToken).ConfigureAwait(false));
            }
        }
        catch { }

        //  Установка пакетов.
        _DataPackages = [.. dataPackages];
    }
}
