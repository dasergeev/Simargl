using Simargl.IO;
using Microsoft.Extensions.Logging;
using Simargl.Hardware.Recorder.Core;
using Simargl.Synergy.Transferring;
using System.IO;
using System.Net.Sockets;

namespace Simargl.Hardware.Recorder.Services.Common;

/// <summary>
/// Представляет службу передачи файлов.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public class TransferringService(
    ILogger<TransferringService> logger, Heart heart) :
    Service<TransferringService>(logger, heart)
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
        //  Вывод информации в журнал.
        Logger.LogInformation("Запуск передачи данных.");

        //  Создание клиента для подключения к серверу.
        using TcpClient client = new();

        //  Подключение к серверу.
        await client.ConnectAsync(
            Heart.Options.TransferringServer,
            Heart.Options.TransferringPort,
            cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        Logger.LogInformation("Выполнено подключение к серверу: {server}", Heart.Options.TransferringServer);

        //  Получение потока.
        using NetworkStream stream = client.GetStream();

        //  Создание запроса на подключение.
        ConnectionRequest connectionRequest = new(Heart.Options.TransferringIdentifier);

        //  Отправка запроса на подключение.
        await connectionRequest.SaveAsync(stream, Heart.Options.TransferringConnectionTimeout * 1000, cancellationToken).ConfigureAwait(false);

        //  Ожидание ответа.
        if (await Message.LoadAsync(stream, Heart.Options.TransferringConnectionTimeout * 1000, cancellationToken).ConfigureAwait(false)
            is not ConnectionConfirmation)
        {
            throw new InvalidDataException("Получено неожиданное сообщение: ожидалось подтверждение подключения.");
        }

        //  Вывод информации в журнал.
        Logger.LogInformation("Получено подтверждение от сервера.");

        //  Получение корневого каталога.
        DirectoryInfo directory = new(Heart.Options.TransferringPath);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Выполнение работы в корневом каталоге.
            await directoryAsync(directory, string.Empty).ConfigureAwait(false);

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }

        //  Асинхронно выполняет работу в каталоге.
        async Task directoryAsync(DirectoryInfo directory, string localPath)
        {
            //  Перебор всех файлов.
            foreach (FileInfo file in directory.GetFiles("*", SearchOption.TopDirectoryOnly))
            {
                //  Асинхронная работа с файлом.
                await fileAsync(file, localPath).ConfigureAwait(false);
            }

            //  Перебор всех подкаталогов.
            foreach (DirectoryInfo subDirectory in directory.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                //  Асинхронная работа в подкаталоге.
                await directoryAsync(subDirectory, string.IsNullOrEmpty(localPath) ? subDirectory.Name : PathBuilder.Combine(localPath, subDirectory.Name));
            }
        }

        //  Асинхронно выполняет работу с файлом.
        async Task fileAsync(FileInfo file, string localPath)
        {
            //  Получение информации о файле.
            DateTime creationTimeUtc = file.CreationTimeUtc;
            DateTime lastWriteTimeUtc = file.LastWriteTimeUtc;
            DateTime lastAccessTimeUtc = file.LastAccessTimeUtc;

            //  Получение текущего времени.
            DateTime nowTimeUtc = DateTime.UtcNow;

            //  Определение длительности хранения.
            double duration = Math.Min(
                (nowTimeUtc - creationTimeUtc).TotalSeconds,
                Math.Min(
                (nowTimeUtc - lastWriteTimeUtc).TotalSeconds,
                (nowTimeUtc - lastAccessTimeUtc).TotalSeconds));

            //  Проверка длительности.
            if (duration < Heart.Options.TransferringMinDuration) return;

            //  Данные файла.
            byte[]? data = null;

            //  Блок перехвата всех исключений.
            try
            {
                //  Чтение данных.
                data = await File.ReadAllBytesAsync(file.FullName, cancellationToken).ConfigureAwait(false);
            }
            catch { }

            //  Проверка данных файла.
            if (data is null) return;

            //  Корректировка локального пути.
            localPath = string.IsNullOrEmpty(localPath) ? file.Name : PathBuilder.Combine(localPath, file.Name);

            //  Создание данных файла.
            FileData fileData = new(localPath, data, creationTimeUtc, lastWriteTimeUtc, lastAccessTimeUtc);

            //  Отправка данных.
            await fileData.SaveAsync(stream, Heart.Options.TransferringDataTimeout * 1000, cancellationToken).ConfigureAwait(false);

            //  Ожидание ответа.
            if (await Message.LoadAsync(stream, Heart.Options.TransferringDataTimeout * 1000, cancellationToken).ConfigureAwait(false)
                is not FileDataConfirmation fileDataConfirmation)
            {
                throw new InvalidDataException("Получено неожиданное сообщение: ожидалось подтверждение получения данных файла.");
            }

            //  Проверка пути.
            if (fileDataConfirmation.Path != fileData.Path)
            {
                throw new InvalidDataException("Получены неожиданные данные: подтверждение получения данных файла содержит неизвестный путь.");
            }

            //  Блок перехвата всех исключений.
            try
            {
                //  Удаление файла.
                File.Delete(file.FullName);
            }
            catch { }
        }
    }
}
