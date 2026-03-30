using Microsoft.Extensions.Logging;
using Simargl.Border.Recorder.Configuring;
using Simargl.Border.Storage;
using Simargl.Border.Storage.Entities;
using System.IO;

namespace Simargl.Border.Recorder.Services.Common;

/// <summary>
/// Представляет службу очистки файлов.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class CleaningService(Program program, ILogger<CleaningService> logger) :
    Service(program, logger)
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Получение конфигурации.
        Configuration configuration = await Program.GetConfigurationAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Очистка каталога сырых данных.
            await ClearAsync(
                BasisConstants.RawFramesPath, PassageState.Processed,
                false, cancellationToken).ConfigureAwait(false);

            //  Очистка каталога обработки.
            await ClearAsync(
                BasisConstants.ProcessedFramesPath, PassageState.Zip,
                false, cancellationToken).ConfigureAwait(false);

            //  Очистка каталога с сжатыми файлами.
            await ClearAsync(
                BasisConstants.ZipFramesPath, PassageState.Conservated,
                false, cancellationToken).ConfigureAwait(false);

            //  Очистка каталога с данными.
            await ClearAsync(
                configuration.DataPath, PassageState.Converted,
                false, cancellationToken).ConfigureAwait(false);

            //  Очистка каталога с передаваемыми файлами.
            await ClearAsync(
                configuration.TransferringPath, PassageState.Conservated,
                true, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед следующим проходом.
            await Task.Delay(BasisConstants.MediumServicePeriod, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет очистку каталога.
    /// </summary>
    /// <param name="path">
    /// Путь к каталогу.
    /// </param>
    /// <param name="minState">
    /// Минимальное состояние.
    /// </param>
    /// <param name="isEmptyOnly">
    /// Значение, определяющее следует ли удалять только пустые каталоги.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая очистку каталога.
    /// </returns>
    private static async Task ClearAsync(string path, PassageState minState, bool isEmptyOnly, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken);

        //  Создание контекста для работы с базой данных.
        await using BorderStorageContext context = new(BasisConstants.Storage);

        //  Перебор ключей.
        foreach (long key in LoadKeys(path))
        {
            //  Поиск данных.
            PassageData? passage = context.Passages.FirstOrDefault(x => x.Key == key);

            //  Проверка данных.
            if (passage is not null && passage.State >= minState)
            {
                //  Путь к каталогу.
                string directory = Path.Combine(path, $"0x{key:X16}");

                //  Проверка флага.
                if (!isEmptyOnly || (
                    Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0))
                {
                    //  Удаление каталога.
                    Directory.Delete(directory, true);
                }
            }
        }
    }

    /// <summary>
    /// Выполняет загрузку ключей.
    /// </summary>
    /// <param name="path">
    /// Путь к каталогу.
    /// </param>
    /// <returns>
    /// Коллекция ключей.
    /// </returns>
    private static IEnumerable<long> LoadKeys(string path)
    {
        //  Получение информации о каталоге.
        DirectoryInfo directory = new(path);

        //  Создание списка ключей.
        List<long> keys = [];

        //  Перебор подкаталогов.
        foreach (DirectoryInfo subDirectory in directory.GetDirectories("*", SearchOption.TopDirectoryOnly))
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Добавление ключа.
                keys.Add(Convert.ToInt64(subDirectory.Name, 16));
            }
            catch { }
        }

        //  Возврат коллекции.
        return [.. keys];
    }
}
