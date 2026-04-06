using Simargl.IO;
using Simargl.Hardware.Modbus.Core;
using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Main.Attributes;
using Simargl.Hardware.Strain.Demo.Main.Properties;
using Simargl.Hardware.Strain.Demo.Microservices;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace Simargl.Hardware.Strain.Demo.Main;

/// <summary>
/// Представляет датчик.
/// </summary>
public sealed partial class Sensor :
    Microservice,
    INotifyPropertyChanged
{
    /// <summary>
    /// Постоянная, определяющая номер порта по умолчанию.
    /// </summary>
    public const int DefaultPort = 502;

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Address"/>.
    /// </summary>
    public event EventHandler? AddressChanged;

    /// <summary>
    /// Происходит при изменении значения свойства <see cref="IsConnected"/>.
    /// </summary>
    public event EventHandler? IsConnectedChanged;

    /// <summary>
    /// Поле для хранения очереди транзакций.
    /// </summary>
    private readonly ConcurrentQueue<ModbusTransaction> _Transaction;

    /// <summary>
    /// Поле для хранения соединения.
    /// </summary>
    private readonly ModbusConnection _Connection;

    /// <summary>
    /// Поле для хранения списка всех атрибутов.
    /// </summary>
    private readonly List<SensorAttribute> _Attribute;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    /// <param name="serial">
    /// Серийный номер.
    /// </param>
    /// <param name="address">
    /// IP-адрес.
    /// </param>
    [CLSCompliant(false)]
    public Sensor(Heart heart, uint serial, IPAddress address) :
        base(heart)
    {
        //  Создание очереди транзакций.
        _Transaction = [];

        //  Создание соединения.
        _Connection = new(_Transaction)
        {
            Timeout = TimeSpan.FromSeconds(1),
        };

        //  Установка значений.
        Address = address;
        Serial = serial;

        //  Создание коллекции сигналов.
        Signals = [new(Serial, 0), new(Serial, 1), new(Serial, 2), new(Serial, 3)];

        //  Создание коллекции свойств датчика.
        Properties = new(this, _Connection);

        //  Создание коллекций атрибутов.
        Information = [];
        Condition = [];
        Settings = [];

        //  Создание атрибутов.
        CreateAttributes();

        //  Создание списка всех атрибутов.
        _Attribute = [];
        _Attribute.AddRange(Information);
        _Attribute.AddRange(Condition);
        _Attribute.AddRange(Settings);

        //  Добавление обработчика события.
        IsConnectedChanged += delegate (object? sender, EventArgs e)
        {
            //  Установка свойства всем атрибутам.
            _Attribute.ForEach(x => x.IsAvailable = IsConnected);
        };

        //  Связывание свойств.
        BindProperties();

        //  Добавление основной задачи в механизм поддержки.
        Keeper.Add(InvokeAsync);
    }

    /// <summary>
    /// Возвращает серийный номер.
    /// </summary>
    [CLSCompliant(false)]
    public uint Serial { get; }

    /// <summary>
    /// Возвращает коллекцию сигналов.
    /// </summary>
    public Signal[] Signals { get; }

    /// <summary>
    /// Возвращает IP-адрес.
    /// </summary>
    public IPAddress Address { get; private set; }

    /// <summary>
    /// Возвращает время обновления IP-адреса.
    /// </summary>
    public DateTime AddressUpdateTime { get; private set; } = DateTime.Now;

    /// <summary>
    /// Возвращает значение, определяющее подключен ли датчик.
    /// </summary>
    public bool IsConnected { get; private set; } = false;

    /// <summary>
    /// Возвращает длительность соединения.
    /// </summary>
    public TimeSpan ConnectionDuration { get; private set; } = TimeSpan.Zero;

    /// <summary>
    /// Возвращает коллекцию свойств датчика.
    /// </summary>
    public SensorPropertyCollection Properties { get; }

    /// <summary>
    /// Возвращает информационные атрибуты.
    /// </summary>
    public SensorAttributeCollection Information { get; }

    /// <summary>
    /// Возвращает атрибуты состояния.
    /// </summary>
    public SensorAttributeCollection Condition { get; }

    /// <summary>
    /// Возвращает атрибуты настройки.
    /// </summary>
    public SensorAttributeCollection Settings { get; }

    private float _Sampling = 0;

    /// <summary>
    /// Асинхронно добавляет пакет данных.
    /// </summary>
    /// <param name="package">
    /// Пакет данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющая пакет данных.
    /// </returns>
    public async Task AddDataPackageAsync(DataPackage package, CancellationToken cancellationToken)
    {
        //  Проверка адреса.
        if (!IPAddressHelper.Equals(Address, package.EndPoint.Address))
        {
            //  Установка состояния соединения.
            await SetIsConnectedAsync(false, cancellationToken).ConfigureAwait(false);
        }

        //  Выполнение в основном потоке.
        await Invoker.InvokeAsync(delegate
        {
            //  Обновление свойств.
            ReceivingTime = package.ReceivingTime;
            SyncFlag = package.SyncFlag;
        }, cancellationToken).ConfigureAwait(false);

        if (_Sampling != 0)
        {
            package.Sampling = _Sampling;
            foreach (var signal in Signals)
            {
                await signal.AddDataPackageAsync(package, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Асинхронно устанавливает IP-адрес.
    /// </summary>
    /// <param name="ipAddress">
    /// IP-адрес.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая IP-адрес.
    /// </returns>
    public async Task SetIPAddressAsync(IPAddress ipAddress, CancellationToken cancellationToken)
    {
        //  Выполнение в основном потоке.
        await Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения значения.
            if (!IPAddressHelper.Equals(Address, ipAddress))
            {
                //  Установка нового значения.
                Address = ipAddress;

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(Address)));
                Volatile.Read(ref AddressChanged)?.Invoke(this, EventArgs.Empty);

                //  Установка времени обновления.
                AddressUpdateTime = DateTime.Now;
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(AddressUpdateTime)));
            }
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно устанавливает значение свойства <see cref="IsConnected"/>.
    /// </summary>
    /// <param name="value">
    /// Новое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая значение свойства.
    /// </returns>
    public async Task SetIsConnectedAsync(bool value, CancellationToken cancellationToken)
    {
        //  Выполнение в основном потоке.
        await Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения значения.
            if (IsConnected != value)
            {
                //  Установка нового значения.
                IsConnected = value;

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(IsConnected)));
                Volatile.Read(ref IsConnectedChanged)?.Invoke(this, EventArgs.Empty);
            }
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно устанавливает значение свойства <see cref="ConnectionDuration"/>.
    /// </summary>
    /// <param name="value">
    /// Новое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая значение свойства.
    /// </returns>
    public async Task SetConnectionDurationAsync(TimeSpan value, CancellationToken cancellationToken)
    {
        //  Выполнение в основном потоке.
        await Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения значения.
            if (ConnectionDuration != value)
            {
                //  Установка нового значения.
                ConnectionDuration = value;

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(ConnectionDuration)));
            }
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет перезагрузку датчика.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая перезагрузку.
    /// </returns>
    public async Task RebootAsync(CancellationToken cancellationToken)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Вывод сообщения в журнал.
            Journal.Add($"[{Serial:X8}] Отправлен запрос на перезагрузку.");

            //  Отправка запроса на перезагрузку.
            await Properties.Status.WriteAsync(0xA0A0, cancellationToken).ConfigureAwait(false); ;
        }
        catch { }
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
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Добавление задач.
        Keeper.Add(AuditAsync);
        Keeper.Add(ConnectAsync);

        //foreach (var item in Properties)
        //{
        //    Journal.Add($"{item.Name}: {await item.ReadAsync(cancellationToken).ConfigureAwait(false)}");
        //}
    }

    /// <summary>
    /// Асинхронно выполняет аудит датчика.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая аудит.
    /// </returns>
    private async Task AuditAsync(CancellationToken cancellationToken)
    {
        //  Флаг инициализации.
        bool isInitialize = false;

        //  Время соединения.
        DateTime connectionTime = default;

        //  Время обновления атрибутов.
        DateTime attributeUpdateTime = DateTime.MinValue;

        //  Установка длительности соединения.
        await SetConnectionDurationAsync(TimeSpan.Zero, cancellationToken).ConfigureAwait(false);

        //  Основной цикл аудита.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Проверка необходимости обновления свойств.
                if (DateTime.Now - attributeUpdateTime > TimeSpan.FromMilliseconds(250))
                {
                    //  Запуск асинхронной задачи.
                    _ = Task.Run(async delegate
                    {
                        //  Выполнение в основном потоке.
                        await Invoker.InvokeAsync(delegate
                        {
                            //  Проверка получения данных.
                            IsRegistration = DateTime.Now - ReceivingTime < TimeSpan.FromSeconds(1);


                        }, cancellationToken).ConfigureAwait(false);
                    }, cancellationToken);
                }

                //  Проверка соединения.
                if (IsConnected)
                {
                    //  Проверка флага инициализации.
                    if (!isInitialize)
                    {
                        //  Перебор свойств датчика.
                        foreach (SensorProperty property in Properties)
                        {
                            //  Инициализация свойства.
                            await property.InitializeAsync(cancellationToken).ConfigureAwait(false);
                        }

                        //  Установка флага инициализации.
                        isInitialize = true;

                        //  Установка времени соединения.
                        connectionTime = DateTime.Now;
                    }

                    //  Чтение серийного номера.
                    string serialNumber = await Properties.SerialNumber.ReadAsync(cancellationToken).ConfigureAwait(false);

                    //  Проверка серийного номера.
                    if (!$"{Serial:X8}".Equals(serialNumber, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //  Разрыв соединения.
                        throw new InvalidOperationException("Сменился серийный номер.");
                    }

                    //  Установка длительности соединения.
                    await SetConnectionDurationAsync(DateTime.Now - connectionTime, cancellationToken).ConfigureAwait(false);

                    //  Проверка необходимости обновления атрибутов.
                    if (DateTime.Now - attributeUpdateTime > TimeSpan.FromSeconds(1))
                    {
                        //  Выполнение в основном потоке.
                        await Invoker.InvokeAsync(delegate
                        {
                            //  Проверка получения данных.
                            IsRegistration = DateTime.Now - ReceivingTime < TimeSpan.FromSeconds(1);


                        }, cancellationToken).ConfigureAwait(false);

                        //  Запуск асинхронной задачи.
                        _ = Task.Run(async delegate
                        {
                            //  Перебор атрибутов.
                            await Parallel.ForEachAsync(
                                _Attribute,
                                cancellationToken,
                                async delegate (SensorAttribute attribute, CancellationToken cancellationToken)
                                {
                                    //  Проверка необходимости обновления.
                                    if (!attribute.HasValue ||
                                        attribute.Format == SensorAttributeFormat.Readable ||
                                        attribute.Format == SensorAttributeFormat.Resettable)
                                    {
                                        //  Блок перехвата всех исключений.
                                        try
                                        {
                                            //  Обновление атрибута.
                                            await attribute.LoadAsync(cancellationToken).ConfigureAwait(false);
                                        }
                                        catch (Exception ex)
                                        {
                                            //  Вывод сообщения в журнал.
                                            Journal.Add($"Не удалось прочитать значение свойства {attribute.Name} ({ex.Message}).");
                                        }
                                    }
                                }).ConfigureAwait(false);
                        }, cancellationToken);

                        //  Установка времени обновления.
                        attributeUpdateTime = DateTime.Now;
                    }
                }
                else
                {
                    //  Сброс флага инициализации.
                    isInitialize = false;

                    //  Проверка времени соединения.
                    if (connectionTime != default)
                    {
                        //  Разрыв соединения.
                        await disconnectAsync().ConfigureAwait(false);
                    }
                }
            }
            catch
            {
                //  Разрыв соединения.
                await disconnectAsync().ConfigureAwait(false);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);

            //  Разрывает соединение.
            async Task disconnectAsync()
            {
                //  Сброс флага инициализации.
                isInitialize = false;

                //  Сброс времени соединения.
                connectionTime = default;

                //  Установка длительности соединения.
                await SetConnectionDurationAsync(TimeSpan.Zero, cancellationToken).ConfigureAwait(false);

                //  Разрыв соединения.
                await SetIsConnectedAsync(false, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Асинхронно устанавливает соедиение.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая соединение.
    /// </returns>
    private async Task ConnectAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание конечной точки.
        IPEndPoint endPoint = new(Address, DefaultPort);

        //  Создание источника токена отмены.
        using CancellationTokenSource cancellationTokenSource =
            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        //  Замена токена отмены.
        cancellationToken = cancellationTokenSource.Token;

        //  Флаг подключения.
        bool isConnected = false;

        //  Блок с гарантированным завершением.
        try
        {
            //  Добавление обработчиков событий.
            AddressChanged += addressChanged;

            //  Создание клиента
            using TcpClient tcpClient = new();

            //  Подключение по TCP/IP
            await tcpClient.ConnectAsync(endPoint, cancellationToken).ConfigureAwait(false);

            //  Получение потока.
            await using NetworkStream stream = tcpClient.GetStream();

            //  Создание распределителя данных потока.
            Spreader spreader = new(stream);

            //  Вывод сообщения в журнал.
            Journal.Add($"[{Serial:X8}] Установлено Modbus-соединение: {endPoint}");

            //  Установка флага подключения.
            isConnected = true;

            //  Установка значения, определяющего установлено ли соединение.
            await SetIsConnectedAsync(true, cancellationToken).ConfigureAwait(false);

            //  Основной цикл выполнения.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Проверка состояния соединения.
                if (!IsConnected)
                {
                    //  Остановка подключения.
                    throw new InvalidOperationException($"[{Serial:X8}] Разорвано соединение: {endPoint}");
                }

                //  Извлечение транзакций из очереди.
                while (!cancellationToken.IsCancellationRequested &&
                    _Transaction.TryDequeue(out ModbusTransaction? transaction))
                {
                    //  Проверка состояния соединения.
                    if (!IsConnected)
                    {
                        //  Остановка подключения.
                        throw new InvalidOperationException($"[{Serial:X8}] Разорвано соединение: {endPoint}");
                    }

                    //  Проверка транзакции.
                    if (transaction is not null)
                    {
                        //  Блок перехвата всех исключений.
                        try
                        {
                            //  Запись в поток.
                            await transaction.Request.SaveAsync(spreader, cancellationToken).ConfigureAwait(false);

                            //  Принудительная отправка данных.
                            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

                            //  Чтение ответа.
                            TcpAduPackage response = await TcpAduPackage.LoadAsync(spreader, cancellationToken).ConfigureAwait(false);

                            //  Завершение транзакции.
                            transaction.TaskCompletionSource.TrySetResult(response);
                        }
                        catch (Exception ex)
                        {
                            //  Завершение транзакции.
                            transaction.TaskCompletionSource.TrySetException(ex);

                            //  Повторный выброс исключения.
                            throw;
                        }
                    }
                }

                //  Ожидание перед следующим проходом.
                await Task.Delay(100, cancellationToken).ConfigureAwait(false);
            }
        }
        finally
        {
            //  Удаление обработчиков события.
            AddressChanged -= addressChanged;

            //  Проверка флага подключения.
            if (isConnected)
            {
                //  Вывод сообщения в журнал.
                Journal.Add($"[{Serial:X8}] Разорвано Modbus-соединение: {endPoint}");
            }

            //  Установка значения, определяющего установлено ли соединение.
            await SetIsConnectedAsync(false, cancellationToken).ConfigureAwait(false);
        }

        //  Обрабатывает событие изменения адреса.
        void addressChanged(object? sender, EventArgs e)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение источника токена отмены.
                cancellationTokenSource.Dispose();
            }
            catch { }
        }
    }
}
