using Simargl.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net.Sockets;

namespace Simargl.Synergy.Transferring.Client;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
public class Worker :
    BackgroundService
{
    /// <summary>
    /// Поле для хранения средства ведения журнала.
    /// </summary>
    private readonly ILogger<Worker> _Logger;

    /// <summary>
    /// Поле для хранения настроек.
    /// </summary>
    private readonly Tunings _Tunings;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="tunings">
    /// Настройки.
    /// </param>
    public Worker(ILogger<Worker> logger, Tunings tunings)
    {
        //  Для анализатора.
        _ = this;

        //  Установка средства ведения журнала.
        _Logger = logger;

        //  Установка настроек.
        _Tunings = tunings;
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Завершение работы.
                    return;
                }

                //  Вывод информации в журнал.
                _Logger.LogError("Произошло исключение: {ex}", ex);
            }

            //  Ожидание перед следующей попыткой.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Ожидание для инициализации консоли.
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        _Logger.LogInformation("Запуск службы.");
        _Logger.LogInformation("Номер порта для подключения к серверу: {port}", _Tunings.Port);

        //  Создание клиента для подключения к серверу.
        using TcpClient client = new();

        //  Подключение к серверу.
        await client.ConnectAsync(_Tunings.Server, _Tunings.Port, cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        _Logger.LogInformation("Выполнено подключение к серверу: {server}", _Tunings.Server);

        //  Получение потока.
        using NetworkStream stream = client.GetStream();

        //  Создание запроса на подключение.
        ConnectionRequest connectionRequest = new(_Tunings.Identifier);

        //  Отправка запроса на подключение.
        await connectionRequest.SaveAsync(stream, _Tunings.ConnectionTimeout * 1000, cancellationToken).ConfigureAwait(false);

        //  Ожидание ответа.
        if (await Message.LoadAsync(stream, _Tunings.ConnectionTimeout * 1000, cancellationToken).ConfigureAwait(false)
            is not ConnectionConfirmation)
        {
            throw new InvalidDataException("Получено неожиданное сообщение: ожидалось подтверждение подключения.");
        }

        //  Вывод информации в журнал.
        _Logger.LogInformation("Получено подтверждение от сервера.");

        //  Получение корневого каталога.
        DirectoryInfo directory = new(_Tunings.Path);

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
            if (duration < _Tunings.MinDuration) return;

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
            await fileData.SaveAsync(stream, _Tunings.DataTimeout * 1000, cancellationToken).ConfigureAwait(false);

            //  Ожидание ответа.
            if (await Message.LoadAsync(stream, _Tunings.DataTimeout * 1000, cancellationToken).ConfigureAwait(false)
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

            //  Вывод информации в журнал.
            _Logger.LogInformation("Отправлены данные файла: {path}", localPath);
        }
    }
}
