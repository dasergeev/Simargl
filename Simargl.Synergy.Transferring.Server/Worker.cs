using Simargl.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Simargl.Synergy.Transferring.Server;

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

        //  Создание средства прослушивания сети.
        TcpListener listener = new(IPAddress.Any, _Tunings.Port);

        //  Блок с гарантированным завершением.
        try
        {
            //  Запуск средства прослушивания сети.
            listener.Start();

            //  Вывод информации в журнал.
            _Logger.LogInformation("Средство прослушивания сети запущено.");

            //  Основной цикл работы.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Ожидание подключения клиента.
                TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                //  Запуск асинхронной задачи.
                _ = Task.Run(async delegate
                {
                    //  Работа с клиентом.
                    await ClientAsync(client, cancellationToken).ConfigureAwait(false);
                }, CancellationToken.None);
            }
        }
        finally
        {
            try
            {
                //  Остановка средства прослушивания сети.
                listener.Stop();
            }
            catch { }
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с клиентом.
    /// </summary>
    /// <param name="client">
    /// Клиент.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с клиентом.
    /// </returns>
    private async Task ClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        //  Захват клиента.
        using (client)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Вывод информации в журнал.
            _Logger.LogInformation("Новое входящее подключение {point}", client.Client.RemoteEndPoint);

            //  Получение потока.
            using NetworkStream stream = client.GetStream();

            //  Получение запроса на подключение.
            if (await Message.LoadAsync(stream, Timeout.Infinite, cancellationToken).ConfigureAwait(false)
                is not ConnectionRequest connectionRequest)
            {
                throw new InvalidDataException("Получено неожиданное сообщение: ожидался запрос на подключение.");
            }

            //  Поиск путей для сохранения данных.
            List<string>? paths = _Tunings.Clients.FirstOrDefault(x => x.Identifier == connectionRequest.Identifier)?.Paths;

            //  Проверка путей.
            if (paths is null || paths.Count == 0)
            {
                throw new InvalidDataException("Произошла попытка потключения неизвестного клиента.");
            }

            //  Создание построителя текста.
            StringBuilder builder = new($"Подключился клиент: {connectionRequest.Identifier}");
            builder.AppendLine();

            //  Перебор путей.
            foreach (string path in paths)
            {
                //  Проверка пути.
                if (!Directory.Exists(path))
                {
                    //  Создание пути.
                    Directory.CreateDirectory(path);
                }

                //  Добавление информации о пути.
                builder.AppendLine($"    {path}");
            }

            //  Вывод информации в журнал.
            _Logger.LogInformation("{message}", builder);

            //  Создание ответа.
            ConnectionConfirmation connectionConfirmation = new();

            //  Отправка ответа.
            await connectionConfirmation.SaveAsync(stream, Timeout.Infinite, cancellationToken).ConfigureAwait(false);

            //  Основной цикл работы.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение запроса на подключение.
                if (await Message.LoadAsync(stream, Timeout.Infinite, cancellationToken).ConfigureAwait(false)
                    is not FileData fileData)
                {
                    throw new InvalidDataException("Получено неожиданное сообщение: ожидались данные файла.");
                }

                //  Перебор путей.
                foreach (string path in paths)
                {
                    //  Получение пути к файлу.
                    string filePath = PathBuilder.Combine(path, fileData.Path);

                    //  Получение пути каталога.
                    string directoryPath = new FileInfo(filePath).Directory!.FullName;

                    //  Проверка существования каталога.
                    if (!Directory.Exists(directoryPath))
                    {
                        //  Создание каталога.
                        Directory.CreateDirectory(directoryPath);
                    }

                    //  Сохранение данных в файл.
                    await File.WriteAllBytesAsync(filePath, fileData.Data, cancellationToken).ConfigureAwait(false);

                    //  Установка времён.
                    File.SetCreationTimeUtc(filePath, fileData.CreationTimeUtc);
                    File.SetLastWriteTimeUtc(filePath, fileData.LastWriteTimeUtc);
                    File.SetLastAccessTimeUtc(filePath, fileData.LastAccessTimeUtc);
                }

                //  Создание ответа.
                FileDataConfirmation fileDataConfirmation = new(fileData.Path);

                //  Отправка ответа.
                await fileDataConfirmation.SaveAsync(stream, Timeout.Infinite, cancellationToken).ConfigureAwait(false);

                //  Вывод информации в журнал.
                _Logger.LogInformation("Получены данные файла: {path}", fileData.Path);
            }
        }
    }
}
