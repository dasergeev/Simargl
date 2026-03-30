using Apeiron.IO;
using Apeiron.Platform.Specialized.FileTransfer;
using Apeiron.Support;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Apeiron.Services.FileTransfer;

/// <summary>
/// Представляет правило, выполняющееся на клиенте.
/// </summary>
public class ClientRule
{
    /// <summary>
    /// Возвращает имя правила.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Возвращает корневой путь к файлам.
    /// </summary>
    public string? Path { get; init; }

    /// <summary>
    /// Возвращает время задержки в секундах перед отправкой файла.
    /// </summary>
    public int? Delay { get; init; }

    /// <summary>
    /// Возвращает фильтры файлов.
    /// </summary>
    public List<string>? Filters { get; init; }

    /// <summary>
    /// Асинхронно выполняет работу правила.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <param name="logic">
    /// Логика работы службы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу правила.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logic"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task InvokeAsync(ILogger logger, ClientLogic logic, CancellationToken cancellationToken)
    {
        //  Проверка средства записи в журнал службы.
        logger = Check.IsNotNull(logger, nameof(logger));

        //  Проверка ссылки на логику.
        logic = Check.IsNotNull(logic, nameof(logic));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение информации о подключении.
        ClientConnection connection = Check.IsNotNull(logic.Connection, nameof(connection));

        //  Получение имени домена для подключения.
        string domain = Check.IsNotNull(connection.Domain, nameof(domain));

        //  Получение порта для подключения.
        int port = Check.IsHasValue(connection.Port, nameof(port));

        //  Получение идентификатора.
        int identifier = Check.IsHasValue(connection.Identifier, nameof(identifier));

        //  Получение имени.
        string name = Check.IsNotNull(Name, nameof(name));

        //  Получение пути.
        string path = PathBuilder.Normalize(Check.IsNotNull(Path, nameof(path)));

        //  Основной цикл правила.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Получение IP-адресов.
            IPAddress[] addresses = await Dns.GetHostAddressesAsync(domain, cancellationToken).ConfigureAwait(false);

            //  Создание задач.
            List<Task> tasks = new();

            //  Перебор всех адресов.
            foreach (IPAddress address in addresses)
            {
                //  Создание новой задачи.
                tasks.Add(Task.Run(
                    async delegate
                    {
                        //  Прехват всех некритических исключений.
                        await Invoker.SafeNotCriticalAsync(
                            async cancellationToken =>
                            {
                                //  Вывод информации в журнал о попытке подключения к адресу.
                                logger.LogInformation("[{name}] Попытка подключения к {address}", Name, address);

                                //  Создание подключения.
                                using TcpClient client = new();

                                //  Подключение.
                                await client.ConnectAsync(address, port, cancellationToken).ConfigureAwait(false);

                                //  Вывод информации в журнал о подключении к адресу.
                                logger.LogInformation("[{name}] Подключен к {address}", Name, address);

                                //  Получение потока.
                                using NetworkStream stream = client.GetStream();

                                //  Создание запроса на подключение.
                                Request request = new(identifier, name);

                                {
                                    ////  Создание средства записи в поток.
                                    //using AsyncBinaryWriter writer = new(stream, Encoding.UTF8, true);

                                    //  Запись в поток.
                                    await request.SaveAsync(stream, cancellationToken).ConfigureAwait(false);
                                    //await writer.WriteAsync(request, cancellationToken).ConfigureAwait(false);

                                    //  Сброс всех данных в поток.
                                    await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
                                }

                                //  Ответ на запрос.
                                Response response;

                                //  Ожидание соединения.
                                {
                                    ////  Создание средства чтения из потока.
                                    //using AsyncBinaryReader reader = new(stream, Encoding.UTF8, true);

                                    //  Создание источника токена отмены с таймаутом.
                                    using CancellationTokenSource timeoutSource = new();

                                    //  Установка времени ожидания.
                                    timeoutSource.CancelAfter(StaticSettings.ResponseTimeout);

                                    //  Создание связанного токена отмены.
                                    using CancellationTokenSource linkedSource =
                                        CancellationTokenSource.CreateLinkedTokenSource(
                                            cancellationToken, timeoutSource.Token);

                                    //  Чтение ответа.
                                    response = await new Response().LoadAsync(stream, linkedSource.Token).ConfigureAwait(false);
                                    //response = await reader.ReadObjectAsync<Response>(linkedSource.Token).ConfigureAwait(false);
                                }

                                //  Вывод информации о подключении к серверу.
                                logger.LogInformation("[{name}] Подключен к серверу.", Name);

                                //  Выполнение работы с каталогом.
                                await DirectoryInvokeAsync(response, stream,
                                    new(path), cancellationToken).ConfigureAwait(false);

                            }, cancellationToken).ConfigureAwait(false);
                    }, cancellationToken));
            }

            //  Ожидание задач.
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с каталогом.
    /// </summary>
    /// <param name="response">
    /// Ответ на запрос о подключении.
    /// </param>
    /// <param name="stream">
    /// Поток для обмена данными.
    /// </param>
    /// <param name="directory">
    /// Каталог, с которым необходимо выполнить работу.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с каталогом.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="response"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="directory"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task DirectoryInvokeAsync(Response response,
        Stream stream, DirectoryInfo directory, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на ответ.
        response = Check.IsNotNull(response, nameof(response));

        //  Проверка ссылки на поток.
        stream = Check.IsNotNull(stream, nameof(stream));

        //  Проверка ссылки на каталог.
        directory = Check.IsNotNull(directory, nameof(directory));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор всех файлов.
        foreach (FileInfo file in directory.GetFiles())
        {
            //  Работа с файлом.
            await FileInvokeAsync(response, stream, file, cancellationToken).ConfigureAwait(false);
        }

        //  Перебор всех подкаталогов.
        foreach (DirectoryInfo subDirectory in directory.GetDirectories())
        {
            //  Работа с подкаталогом.
            await DirectoryInvokeAsync(response, stream, subDirectory, cancellationToken).ConfigureAwait(false);
        }

        //  Передача ресурсов другим потокам.
        await Task.Delay(1,cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с файлом.
    /// </summary>
    /// <param name="response">
    /// Ответ на запрос о подключении.
    /// </param>
    /// <param name="stream">
    /// Поток для обмена данными.
    /// </param>
    /// <param name="file">
    /// Файл, с которым необходимо выполнить работу.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с файлом.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="response"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="file"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task FileInvokeAsync(Response response,
        Stream stream, FileInfo file, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на ответ.
        response = Check.IsNotNull(response, nameof(response));

        //  Проверка ссылки на поток.
        stream = Check.IsNotNull(stream, nameof(stream));

        //  Проверка ссылки на файл.
        file = Check.IsNotNull(file, nameof(file));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение имени файла.
        string fileName = file.Name;

        //  Получение задержки.
        int delay = Check.IsHasValue(Delay, nameof(delay));

        //  Проверка фильтров.
        if (Filters is List<string> filters && filters.Count > 0)
        {
            //  Значение, определяющее необходимоть обработки файла.
            bool isWork = false;

            //  Перебор всех фильтров.
            foreach (var filter in filters)
            {
                //  Проверка фильтра.
                if (Regex.IsMatch(fileName, filter))
                {
                    //  Необходимо обработать файл.
                    isWork = true;

                    //  Выход из цикла.
                    break;
                }
            }

            //  Проверка необходимости обработки файла.
            if (!isWork)
            {
                //  Завершение работы с файлом.
                return;
            }
        }

        //  Проверка задержки.
        if (delay > 0)
        {
            //  Определение текущего времени.
            DateTime currentTime = DateTime.Now;

            if (
                (currentTime - file.CreationTime).TotalSeconds < Delay ||
                (currentTime - file.LastWriteTime).TotalSeconds < Delay ||
                (currentTime - file.LastAccessTime).TotalSeconds < Delay)
            {
                //  Завершение работы с файлом.
                return;
            }
        }

        //  Получение корневого пути.
        string rootPath = PathBuilder.Normalize(Check.IsNotNull(Path, nameof(Path)));

        //  Получение полного пути.
        string fullPath = PathBuilder.Normalize(file.FullName);

        //  Относительный путь.
        string relativePath;

        //  Получение относительного пути.
        {
            //  Получение длины корневого пути.
            int length = rootPath.Length;

            //  Проверка исходного пути.
            if (fullPath.Length >= length && fullPath[..length] == rootPath)
            {
                //  Получение относительного пути.
                relativePath = fullPath[length..];
            }
            else
            {
                //  Завершение работы с файлом.
                return;
            }
        }

        //  Получение размера файла.
        int fileSize = checked((int)file.Length);

        //  Создание описания пакета данных.
        Relation relation = new(response, relativePath, file.Length, fullPath,
            file.CreationTime, file.LastAccessTime, file.LastWriteTime);

        //  Данные файла.
        byte[] data = new byte[fileSize];

        //  Создание потока для чтения файла.
        using (FileStream fileStream = new(fullPath, FileMode.Open, FileAccess.Read, FileShare.None))
        {
            //  Чтение данных из файла.
            if (fileSize != fileStream.Read(data))
            {
                //  Недопустимая операция.
                throw Exceptions.OperationInvalid();
            }
        }

        //  Создание пакета данных.
        Parcel parcel = new(relation, data);

        //  Отправка пакета данных.
        //using (AsyncBinaryWriter writer = new(stream, Encoding.UTF8, true))
        {
            //  Запись в поток.
            await parcel.SaveAsync(stream, cancellationToken).ConfigureAwait(false);
            //await writer.WriteAsync(parcel, cancellationToken).ConfigureAwait(false);

            //  Сброс всех данных в поток.
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }

        //  Уведомление о получении.
        Notice notice;

        //  Создание средства чтения из потока.
        //using (AsyncBinaryReader reader = new(stream, Encoding.UTF8, true))
        {
            //  Создание источника токена отмены с таймаутом.
            using CancellationTokenSource timeoutSource = new();

            //  Установка времени ожидания.
            timeoutSource.CancelAfter(StaticSettings.NoticeTimeout);

            //  Создание связанного токена отмены.
            using CancellationTokenSource linkedSource =
                CancellationTokenSource.CreateLinkedTokenSource(
                    cancellationToken, timeoutSource.Token);

            //  Чтение ответа.
            notice = await new Notice().LoadAsync(stream, linkedSource.Token).ConfigureAwait(false);
        }

        //  Проверка ответа.
        if (notice.Relation != relation)
        {
            //  Завершение работы с файлом.
            return;
        }

        //  Удаление файла.
        file.Delete();
    }

    /// <summary>
    /// Асинхронно выполняет проверку данных.
    /// </summary>
    /// <param name="writer">
    /// Средство записи текстовой информации.
    /// </param>
    /// <param name="level">
    /// Уровень вложенности.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая проверку данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="writer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="level"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Недопустимый формат данных.
    /// </exception>
    public async Task CheckValidityAsync(TextWriter writer, int level, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на средство записи в журнал.
        writer = Check.IsNotNull(writer, nameof(writer));

        //  Проверка уровня вложенности.
        level = Check.IsNotNegative(level, nameof(level));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Формирование отступа.
        string indent = new(' ', 2 * level);

        //  Проверка установки имени.
        if (Name is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не задано имя правила.");
        }

        //  Вывод информации о проверке имени.
        await writer.WriteAsync($"{indent}Имя: {Name}\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Проверка установки корневого пути к файлам.
        if (Path is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не задан корневой путь правила.");
        }

        //  Проверка существования корневого пути к файлам.
        if (!Directory.Exists(Path))
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не существует корневой путь правила.");
        }

        //  Вывод информации о корневом пути.
        await writer.WriteAsync($"{indent}Путь: \"{Path}\"\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Проверка установки времени задаржки перед отправкой файла.
        if (Delay is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не задано время задаржки перед отправкой файла.");
        }

        //  Проверка времени задаржки перед отправкой файла.
        if (Delay < 0)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Отрицательное время задаржки перед отправкой файла.");
        }

        //  Вывод информации о корневом пути.
        await writer.WriteAsync($"{indent}Время задержки: {Delay}\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Проверка установки фильтров.
        if (Filters is null)
        {
            //  Вывод информации о об отсутствии фильтров.
            await writer.WriteAsync($"{indent}Фильтры: не заданы\n".AsMemory(), cancellationToken).ConfigureAwait(false);
        }
        else
        {
            //  Вывод информации о фильтрах.
            await writer.WriteAsync($"{indent}Фильтры:\n".AsMemory(), cancellationToken).ConfigureAwait(false);

            //  Перебор всех фильтров.
            for (int i = 0; i < Filters.Count; i++)
            {
                //  Получение фильтра.
                string filter = Filters[i];

                //  Проверка фильтра.
                try
                {
                    //  Создание регулярного выражения.
                    Regex regex = new(filter);
                }
                catch (ArgumentException ex)
                {
                    //  Недопустимый формат данных.
                    throw new InvalidDataException($"Неправильный формат фильтра {filter}: {ex.Message}.");
                }

                //  Вывод информации о фильтре.
                await writer.WriteAsync($"{indent}  Фильтр[{i}]: \"{filter}\"\n".AsMemory(), cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
