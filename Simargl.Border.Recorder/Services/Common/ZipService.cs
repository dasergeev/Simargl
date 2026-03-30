using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simargl.Border.Storage;
using Simargl.Border.Storage.Entities;
using System.IO;
using System.IO.Compression;

namespace Simargl.Border.Recorder.Services.Common;

/// <summary>
/// Представляет службу сжатия файлов.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class ZipService(Program program, ILogger<ZipService> logger) :
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
        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Создание контекста для работы с базой данных.
            await using BorderStorageContext context = new(BasisConstants.Storage);

            //  Запрос необработанных данных.
            PassageData? passage = await context.Passages
                .Where(x => x.State == PassageState.Processed)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Проверка данных.
            if (passage is not null)
            {
                //  Определение каталога исходных данных.
                string sourcePath = Path.Combine(BasisConstants.ProcessedFramesPath, $"0x{passage.Key:X16}");

                //  Определение целевого каталога.
                string targetPath = Path.Combine(BasisConstants.ZipFramesPath, $"0x{passage.Key:X16}");

                //  Создание каталога.
                Directory.CreateDirectory(targetPath);

                //  Начало транзакции.
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                //  Блок перехвата всех исключений.
                try
                {
                    //  Перебор файлов.
                    foreach (FileInfo file in new DirectoryInfo(sourcePath).GetFiles())
                    {
                        //  Получение имени файла.
                        string targetFile = Path.Combine(targetPath, file.Name + ".zip");

                        //  Открытие потока для записи.
                        using FileStream zipToOpen = new(targetFile, FileMode.Create);

                        //  Создание архива.
                        using ZipArchive archive = new(zipToOpen, ZipArchiveMode.Create);

                        //  Упаковка файлов.
                        archive.CreateEntryFromFile(
                            file.FullName,
                            file.Name,
                            CompressionLevel.SmallestSize);
                    }

                    //  Установка флага обработки.
                    passage.State = PassageState.Zip;

                    //  Сохранение изменений.
                    await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                    //  Применение транзакции.
                    await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
                }
                catch
                {
                    //  Отмена транзакции.
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

                    //  Повторный выброс исключения.
                    throw;
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(BasisConstants.MediumServicePeriod, cancellationToken).ConfigureAwait(false);
        }
    }
}
