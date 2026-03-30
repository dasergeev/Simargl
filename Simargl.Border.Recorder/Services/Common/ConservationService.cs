using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simargl.Border.Recorder.Configuring;
using Simargl.Border.Storage;
using Simargl.Border.Storage.Entities;
using System.IO;

namespace Simargl.Border.Recorder.Services.Common;

/// <summary>
/// Представляет службу сохранения файлов.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class ConservationService(Program program, ILogger<ConservationService> logger) :
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
            //  Создание контекста для работы с базой данных.
            await using BorderStorageContext context = new(BasisConstants.Storage);

            //  Запрос необработанных данных.
            PassageData? passage = await context.Passages
                .Where(x => x.State == PassageState.Zip)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Проверка данных.
            if (passage is not null)
            {
                //  Определение каталога исходных данных.
                string sourcePath = Path.Combine(BasisConstants.ZipFramesPath, $"0x{passage.Key:X16}");

                //  Определение целевых каталогов.
                string dataPath = Path.Combine(configuration.DataPath, $"0x{passage.Key:X16}");
                string transferringPath = Path.Combine(configuration.TransferringPath, $"0x{passage.Key:X16}");

                //  Создание каталогов.
                Directory.CreateDirectory(dataPath);
                Directory.CreateDirectory(transferringPath);

                //  Начало транзакции.
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                //  Блок перехвата всех исключений.
                try
                {
                    //  Перебор файлов.
                    foreach (FileInfo file in new DirectoryInfo(sourcePath).GetFiles())
                    {
                        //  Копирование файла.
                        File.Copy(file.FullName, Path.Combine(dataPath, file.Name), true);
                        File.Copy(file.FullName, Path.Combine(transferringPath, file.Name), true);

                        //  Удаление исходного файла.
                        File.Delete(file.FullName);
                    }

                    //  Установка флага обработки.
                    passage.State = PassageState.Conservated;

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
