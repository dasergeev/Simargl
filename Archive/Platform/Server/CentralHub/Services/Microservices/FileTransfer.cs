using Apeiron.Platform.Server.Microservices;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу, выполняющую приём файлов от удалённых устройств.
/// </summary>
public sealed class FileTransfer :
    ServerMicroservice<FileTransfer>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public FileTransfer(ILogger<FileTransfer> logger) :
        base(logger)
    {

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
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        ////  Получение информации о микрослужбе.
        //MicroserviceInfo microservice = await GetMicroserviceInfoAsync(cancellationToken).ConfigureAwait(false);

        //  Получение номера прослушиваемого порта.
        int port = 7012;// await microservice.GetInt32SettingAsync("Port", cancellationToken).ConfigureAwait(false);

        //  Получение коллекции правил.
        ServerRuleCollection rules = new(Logger, microservice);

        //  Инициализация правил.
        await rules.InitializeAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл приложения.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Средство прослушивания сети.
            TcpListener? listener = null;

            //  Блок перехвата критических исключений.
            try
            {
                //  Создание средства прослушивания сети.
                listener = new(IPAddress.Any, port);

                //  Запуск средства прослушивания сети.
                listener.Start();

                //  Вывод записи в журнал.
                Logger.LogInformation("Запуск прослушивания сети {endpoint}", listener.LocalEndpoint);

                //  Цикл ожидания входящих подключений.
                while (!cancellationToken.IsCancellationRequested)
                {
                    //  Получение входящего подключения.
                    TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                    //  Вывод записи в журнал.
                    Logger.LogInformation("Новое входящее подключение {endPoint}", client.Client.RemoteEndPoint);

                    //  Запуск задачи, выполняющей работу с подключением.
                    _ = Task.Run(async delegate
                    {
                        //  Использование для корректного освобождения ресурсов.
                        using TcpClient tcpClient = client;

                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Выполнение работы с клиентом.
                        await rules.InvokeAsync(client, cancellationToken).ConfigureAwait(false);

                    }, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                //  Асинхронный вывод информации в журнал.
                await Invoker.SafeNotCriticalAsync(
                    async cancellationToken =>
                    {
                        //  Асинхронное выполнение задачи.
                        await Task.Run(delegate
                        {
                            //  Запись информации об исключении в журнал.
                            Logger.LogCritical("{exception}", ex);
                        }, cancellationToken).ConfigureAwait(false);
                    }, default).ConfigureAwait(false);

                //  Проверка критического исключения.
                if (ex.IsCritical())
                {
                    //  Повторный выброс исключения.
                    throw;
                }
            }
            finally
            {
                //  Остановка средства прослушивания сети.
                await Invoker.SafeNotCriticalAsync(
                    async cancellationToken =>
                    {
                        //  Асинхронное выполнение задачи.
                        await Task.Run(delegate
                        {
                            //  Остановка средства прослушивания сети.
                            listener?.Stop();
                        }, cancellationToken).ConfigureAwait(false);
                    }, default).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Представляет коллекцию правил, выполняющихся на сервере.
    /// </summary>
    private class ServerRuleCollection :
        IEnumerable<ServerRule>
    {
        /// <summary>
        /// Поле для хранения списка элементов коллекции.
        /// </summary>
        private readonly List<ServerRule> _Items;

        /// <summary>
        /// Поле для хранения средства записи в журнал службы.
        /// </summary>
        private readonly ILogger _Logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="logger">
        /// Средство записи в журнал.
        /// </param>
        /// <param name="microservice">
        /// Информация о микрослужбе.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="logger"/> передана пустая ссылка.
        /// </exception>
        public ServerRuleCollection(ILogger logger, MicroserviceInfo microservice)
        {
            //  Установка средства записи в журнал.
            _Logger = Check.IsNotNull(logger, nameof(logger));

            //  Создание списка элементов коллекции.
            _Items = new();

            //  Загрузка всех регистраторов.
            Recorder[] recorders = CentralDatabaseAgent.Request(session => session.Recorders.ToArray());

            Microservice = microservice;

            RequestTimeout = microservice.GetInt32Setting("RequestTimeout");

            //  Перебор всех настроек.
            foreach (var recorder in recorders)
            {
                //  Создание правила.
                _Items.Add(new(
                    _Logger,
                    recorder.Name,
                    recorder.TransferIdentificator,
                    recorder.TransferDirectory.GetAbsolutePaths().First(),
                    microservice));
            }
        }

        /// <summary>
        /// Возвращает информацию о микрослужбе.
        /// </summary>
        public MicroserviceInfo Microservice { get; }

        /// <summary>
        /// Возвращает время запроса на соединение в миллисекундах.
        /// </summary>
        public int RequestTimeout { get; }

        /// <summary>
        /// Асинхронно выполняет инициализацию всех правил.
        /// </summary>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая инициализацию всех правил.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Перебор всех элементов коллекции.
            foreach (var rule in this)
            {
                //  Инициализация правила.
                await rule.InitializeAsync(cancellationToken).ConfigureAwait(false);
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
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="client"/> передана пустая ссылка.
        /// </exception>
        public async Task InvokeAsync(TcpClient client, CancellationToken cancellationToken)
        {
            //  Проверка ссылки на клиента.
            client = Check.IsNotNull(client, nameof(client));

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание потока.
            using NetworkStream stream = client.GetStream();

            //  Запрос.
            Request request = new();

            //  Установка соединения.
            {
                //  Создание источника токена отмены с таймаутом.
                using CancellationTokenSource timeoutSource = new();

                //  Установка времени ожидания.
                timeoutSource.CancelAfter(RequestTimeout);

                //  Создание связанного токена отмены.
                using CancellationTokenSource linkedSource =
                    CancellationTokenSource.CreateLinkedTokenSource(
                        cancellationToken, timeoutSource.Token);

                ////  Создание средства чтения двоичной информации.
                //using AsyncBinaryReader reader = new(stream, Encoding.UTF8, true);

                //  Чтение запроса.
                await request.LoadAsync(stream, linkedSource.Token).ConfigureAwait(false);

                //request = await reader.ReadObjectAsync<Request>(linkedSource.Token).ConfigureAwait(false);
            }

            //  Вывод информации о клиенте.
            _Logger.LogInformation("Подключился клиент: ID = {identifier}, Name = \"{name}\"",
                request.Identifier, request.Name);

            //  Поиск правила.
            if (FromIdentifierCore(request.Identifier) is not ServerRule rule)
            {
                //  Неверный идентификатор клиента.
                _Logger.LogInformation(
                    "Неверный идентификатор клиента: ID = {identifier}, Name = \"{name}\"",
                    request.Identifier, request.Name);

                //  Произошла попытка выполнить недопустимую операцию.
                throw Exceptions.OperationInvalid();
            }

            //  Создание ответа.
            Response response = new(request);

            //  Отправка ответа.
            {
                ////  Создание средства записи двоичной информации.
                //using AsyncBinaryWriter writer = new(stream, Encoding.UTF8, true);

                //  Запись ответа.
                await response.SaveAsync(stream, cancellationToken).ConfigureAwait(false);

                //  Сброс данных в поток.
                await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            }

            //  Запуск правила.
            await rule.InvokeAsync(stream, response, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Выполняет поиск правила по идентификатору.
        /// </summary>
        /// <param name="identifier">
        /// Идентификатор для поиска.
        /// </param>
        /// <returns>
        /// Найденное правило или пустая ссылка.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ServerRule? FromIdentifierCore(int identifier)
        {
            //  Перебор всех правил.
            foreach (ServerRule rule in this)
            {
                //  Проверка идентификатора.
                if (rule.Identifier == identifier)
                {
                    //  Правило найдено.
                    return rule;
                }
            }

            //  Правило не найдено.
            return null;
        }

        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель коллекции.
        /// </returns>
        public IEnumerator<ServerRule> GetEnumerator()
        {
            //  Возврат перечислителя списка элементов коллекции.
            return ((IEnumerable<ServerRule>)_Items).GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            //  Возврат перечислителя списка элементов коллекции.
            return ((IEnumerable)_Items).GetEnumerator();
        }
    }

    /// <summary>
    /// Представляет правило, выполняющееся на сервере.
    /// </summary>
    private class ServerRule
    {
        /// <summary>
        /// Поле для хранения средства записи в журнал службы.
        /// </summary>
        private readonly ILogger _Logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя правила.
        /// </param>
        /// <param name="logger">
        /// Средство записи в журнал.
        /// </param>
        /// <param name="identifier">
        /// Идентификатор клиента.
        /// </param>
        /// <param name="path">
        /// Путь к корневому каталогу.
        /// </param>
        /// <param name="microservice">
        /// Информация о микрослужбе.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="logger"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="name"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="identifier"/> передано отрицательное значение.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="path"/> передана пустая ссылка.
        /// </exception>
        public ServerRule(ILogger logger, string name, int identifier, string path, MicroserviceInfo microservice)
        {
            //  Установка средства записи в журнал.
            _Logger = Check.IsNotNull(logger, nameof(logger));

            //  Установка имени правила.
            Name = Check.IsNotNull(name, nameof(name));

            //  Установка идентификатора.
            Identifier = Check.IsNotNegative(identifier, nameof(identifier));

            //  Установка корневого каталога.
            Directory = new(PathBuilder.Normalize(Check.IsNotNull(path, nameof(path))));

            Microservice = microservice;

            ParcelTimeout = microservice.GetInt32Setting("ParcelTimeout");
        }

        /// <summary>
        /// Возвращает имя правила.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает идентификатор клиента.
        /// </summary>
        public int Identifier { get; }

        /// <summary>
        /// Возвращает корневой каталог.
        /// </summary>
        public DirectoryInfo Directory { get; }

        /// <summary>
        /// Возвращает информацию о микрослужбе.
        /// </summary>
        public MicroserviceInfo Microservice { get; }

        /// <summary>
        /// Возвращает время ожидания пакета данных.
        /// </summary>
        public int ParcelTimeout { get; }

        /// <summary>
        /// Асинхронно выполняет инициализацию правила.
        /// </summary>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая инициализацию правила.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Инициализация корневого каталога.
            await InitializeDirectoryAsync(Directory, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Асинхронно выполняет работу правила.
        /// </summary>
        /// <param name="stream">
        /// Поток для работы с клиентом.
        /// </param>
        /// <param name="response">
        /// Ответ на запрос о соединении.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая работу правила.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="stream"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="response"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public async Task InvokeAsync(Stream stream, Response response, CancellationToken cancellationToken)
        {
            //  Проверка ссылки на поток.
            stream = Check.IsNotNull(stream, nameof(stream));

            //  Проверка ссылки на ответ на запрос о соединении.
            response = Check.IsNotNull(response, nameof(response));

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Цикл чтения.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Создание источника токена отмены с таймаутом.
                using CancellationTokenSource timeoutSource = new();

                //  Установка времени ожидания.
                timeoutSource.CancelAfter(ParcelTimeout);

                //  Создание связанного токена отмены.
                using CancellationTokenSource linkedSource =
                    CancellationTokenSource.CreateLinkedTokenSource(
                        cancellationToken, timeoutSource.Token);

                //  Чтение пакета данных.
                Parcel parcel = await new Parcel().LoadAsync(stream, linkedSource.Token).ConfigureAwait(false);

                //  Получение описания пакета данных.
                Relation relation = parcel.Relation;

                //  Проверка пакета.
                if (relation.Response != response)
                {
                    //  Недопустимая операция.
                    throw Exceptions.OperationInvalid();
                }

                //  Получение целевого пути.
                string path = Path.Combine(PathBuilder.Normalize(Directory.FullName), PathBuilder.RelativeNormalize(parcel.Relation.Path));

                //  Инициализация целевого каталога.
                await InitializeDirectoryAsync(
                    Check.IsNotNull(new FileInfo(path).Directory, nameof(Directory)),
                    cancellationToken).ConfigureAwait(false);

                //  Блок перехвата исключений для удаления файла.
                try
                {
                    //  Получение массива данных.
                    byte[] data = parcel.Data;

                    //  Определение длины массива данных.
                    int length = data.Length;

                    //  Запись данных в файл.
                    await File.WriteAllBytesAsync(path, data, cancellationToken).ConfigureAwait(false);

                    //  Чтение данных из файла.
                    byte[] buffer = await File.ReadAllBytesAsync(path, cancellationToken).ConfigureAwait(false);

                    //  Проверка прочитанных данных.
                    if (buffer.Length != length)
                    {
                        //  Недопустимая операция.
                        throw Exceptions.OperationInvalid();
                    }

                    //  Проверка прочитанных данных.
                    for (int i = 0; i < length; i++)
                    {
                        //  Проверка значения.
                        if (buffer[i] != data[i])
                        {
                            //  Недопустимая операция.
                            throw Exceptions.OperationInvalid();
                        }
                    }

                    //  Получение информации о файле.
                    FileInfo file = new(path);

                    //  Проверка размера файла.
                    if (file.Length != relation.Size)
                    {
                        //  Недопустимая операция.
                        throw Exceptions.OperationInvalid();
                    }

                    //  Установка времён.
                    File.SetCreationTime(path, relation.CreationTime);
                    File.SetLastAccessTime(path, relation.LastAccessTime);
                    File.SetLastWriteTime(path, relation.LastWriteTime);

                    //  Проверка врёмен.
                    if (File.GetCreationTime(path) != relation.CreationTime ||
                        File.GetLastAccessTime(path) != relation.LastAccessTime ||
                        File.GetLastWriteTime(path) != relation.LastWriteTime)
                    {
                        //  Недопустимая операция.
                        throw Exceptions.OperationInvalid();
                    }

                    //  Регистрация в базе данных.
                    InternalFile internalFile = await CentralDatabaseAgent.FileSystem.FileRegistrationAsync(
                        path, DateTime.Now, cancellationToken).ConfigureAwait(false);

                    //  Вывод информации о получении файла.
                    _Logger.LogInformation("[{name}] \"{path}\"", Name, internalFile.Path);
                }
                catch (Exception ex)
                {
                    //  Вывод информации об ошибке.
                    _Logger.LogInformation("[{name}] {exception}", Name, ex);

                    //  Удаление файла.
                    File.Delete(path);

                    //  Повторный выброс исключения.
                    throw;
                }

                //  Создание уведомления о получении данных.
                Notice notice = new(relation);

                //  Запись уведомления о получении данных.
                await notice.SaveAsync(stream, cancellationToken).ConfigureAwait(false);

                //  Сброс данных в поток.
                await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Асинхронно выполняет инициализацию каталога.
        /// </summary>
        /// <param name="directory">
        /// Каталог.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая инициализацию каталога.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="directory"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        private static async Task InitializeDirectoryAsync(DirectoryInfo directory, CancellationToken cancellationToken)
        {
            //  Проверка ссылки на путь.
            directory = Check.IsNotNull(directory, nameof(directory));

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Асинхронное выполенение задачи.
            await Task.Run(async delegate
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Проверка родительского каталога.
                if (directory.Parent is DirectoryInfo parent)
                {
                    //  Подготовка родительского каталога.
                    await InitializeDirectoryAsync(parent, cancellationToken).ConfigureAwait(false);
                }

                //  Проверка существования каталога.
                if (!directory.Exists)
                {
                    //  Создание каталога.
                    directory.Create();
                }
            }, cancellationToken).ConfigureAwait(false);
        }
    }
}
