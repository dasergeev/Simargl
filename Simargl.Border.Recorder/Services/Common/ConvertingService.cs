using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simargl.Border.Recorder.Configuring;
using Simargl.Border.Storage;
using Simargl.Border.Storage.Entities;
using Simargl.Frames;
using System.Globalization;
using System.IO;
using System.IO.Compression;

namespace Simargl.Border.Recorder.Services.Common;

/// <summary>
/// Представляет службу конвертации.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class ConvertingService(Program program, ILogger<ConvertingService> logger) :
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
                .Where(x => x.State == PassageState.Conservated)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Проверка данных.
            if (passage is not null)
            {
                //  Определение каталога исходных данных.
                string sourcePath = Path.Combine(configuration.DataPath, $"0x{passage.Key:X16}");

                //  Определение целевого каталога.
                string targetPath = Path.Combine(configuration.ConvertedPath, $"0x{passage.Key:X16}");

                //  Удаление каталога.
                if (Directory.Exists(targetPath)) Directory.Delete(targetPath, true);

                //  Создание каталога.
                Directory.CreateDirectory(targetPath);

                //  Получение всех каналов.
                var channels = new DirectoryInfo(sourcePath)
                    .GetFiles("*.zip", SearchOption.TopDirectoryOnly)
                    .Select(x =>
                    {
                        //  Разбор имени файла.
                        if (!TryParseFileName(x.Name, out var name, out var index) || name is null)
                            return null;

                        //  Возврат данных.
                        return new
                        {
                            Path = x.FullName,
                            EntryName = x.Name[..^4],
                            Name = name!,
                            Index = index,
                        };
                    })
                    .Where(x => x is not null)
                    .Select(x => x!)
                    .OrderBy(x => x.Index)
                    .ToArray();

                //  Создание файла с заголовками.
                await using (FileStream stream = new(
                    Path.Combine(targetPath, $"0x{passage.Key:X16}.mera"),
                    FileMode.Create, FileAccess.Write))
                {
                    //  Создание средства записи текста.
                    await using StreamWriter writer = new(stream, Encoding.UTF8, 65536, true);

                    //  Определение текущего времени.
                    DateTime now = DateTime.Now;

                    //  Запись заголовка.
                    writer.WriteLine("[MERA]");
                    writer.WriteLine("DataSourceApp=Simargl.Border.Recorder");
                    writer.WriteLine($"DataSourceVer={Program.Verion}");
                    writer.WriteLine($"Time={now:HH:mm:ss.fff}");
                    writer.WriteLine($"Date={now:dd.MM.yyyy}");
                    writer.WriteLine("Test=Test");
                    writer.WriteLine("Prod=Prod");

                    //  Запись каналов.
                    int index = 1;
                    foreach (var channel in channels)
                    {
                        writer.WriteLine();
                        writer.WriteLine($"[{channel.Name}]");
                        writer.WriteLine("Freq=2000.000000");
                        writer.WriteLine("XUnits=сек.");
                        writer.WriteLine("Comment=Comment");
                        writer.WriteLine($"Address = m11 - 1 - {index++}");

                        writer.WriteLine("ModSN=00000002");
                        writer.WriteLine("ModName=MIC1100");
                        writer.WriteLine($"YUnits = -");
                        writer.WriteLine("Start=0.0000000000");
                        writer.WriteLine("YFormat=R4");
                    }

                    //  Сброс данных потока.
                    await writer.FlushAsync(cancellationToken).ConfigureAwait(false);
                    await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
                }

                //  Перебор каналов.
                foreach (var info in channels)
                {
                    //  Открытие потока.
                    await using FileStream zipStream = new(info.Path, FileMode.Open, FileAccess.Read, FileShare.Read);

                    //  Получение архива.
                    await using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);

                    //  Чтение данных.
                    ZipArchiveEntry entry = archive.GetEntry(info.EntryName)
                        ?? throw new FileNotFoundException("Файл в архиве не найден", info.EntryName);

                    //  Открытие потока.
                    await using Stream entryStream = entry.Open();

                    //  Открытие кадра.
                    Frame frame = new(entryStream);

                    //  Получение канала.
                    Channel channel = frame.Channels[0];

                    //  Открытие потока для записи.
                    await using FileStream stream = new(
                        Path.Combine(targetPath, info.Name + ".dat"),
                        FileMode.Create, FileAccess.Write);

                    //  Получение средства записи двоичных данных.
                    await using BinaryWriter writer = new(stream, Encoding.UTF8, true);

                    //  Перебор данных.
                    for (int i = 0; i < channel.Length; ++i)
                    {
                        //  Запись данных.
                        writer.Write((float)channel[i]);
                    }
                }

                //  Установка флага обработки.
                passage.State = PassageState.Converted;

                //  Сохранение изменений.
                await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(BasisConstants.MediumServicePeriod, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Выполняет рабор имени файла.
    /// </summary>
    /// <param name="fileName">
    /// Имя файла.
    /// </param>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <param name="index">
    /// Индекс канала.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    private static bool TryParseFileName(string fileName, out string? name, out int index)
    {
        //  Установка выходных значений по умолчанию.
        name = null;
        index = -1;

        //  Проверка имени.
        if (!fileName.StartsWith("Vp0_0 ") || !fileName.EndsWith(".zip"))
            return false;

        //  Выделение основной части.
        string core = fileName.Substring(6, fileName.Length - 6 - 4); // убрали "Vp0_0 " и ".zip"

        //  Определение положения последней точки.
        int lastDot = core.LastIndexOf('.');

        //  Проверка последней точки.
        if (lastDot < 0 || core.Length - lastDot - 1 != 4)
            return false;

        //  Определение имени.
        name = core[..lastDot];

        //  Определение части, содержащей индекс.
        string indexPart = core[(lastDot + 1)..];

        //  Определение индекса.
        if (!int.TryParse(indexPart, NumberStyles.None, CultureInfo.InvariantCulture, out index))
            return false;

        //  Разбор прошёл успешно.
        return true;
    }
}
