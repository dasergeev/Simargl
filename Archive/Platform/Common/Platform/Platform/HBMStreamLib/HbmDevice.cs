using System.Net.Sockets;
using System.Text.Json.Nodes;
using System.Text;
using Apeiron.Platform.TcpClientReceiver;
using Apeiron.EventsArgs;

namespace Apeiron.QuantumX;

/// <summary>
/// Класс обеспечивающий прием и управление потоком от устройства Quantum.
/// </summary>
public class HbmDevice
{

    /// <summary>
    /// Представляет флаг запуска выполнения задачи обмена.
    /// </summary>
    private bool _IsStarted = false;

    /// <summary>
    /// Представляет размера буфера.
    /// </summary>
    public int BufferSize { get; init; } = 10 * 1024 * 1024;

    /// <summary>
    /// Представляет буфер промежуточного копирования.
    /// </summary>
    public int PortionSize { get; init; } = 1024;

    /// <summary>
    /// Представляет время ожидания данных в милисекундах.
    /// </summary>
    public int Time { get; init; } = 2000;

    /// <summary>
    /// Представляет источник токена отмены для роутины.
    /// </summary>
    private CancellationTokenSource? _TokenSource;

    /// <summary>
    /// Представляет задачу класса.
    /// </summary>
    private Task? _RoutineTask;

    /// <summary>
    /// Представляет событие возвращающее поток сырых данных от устройства.
    /// </summary>
    public EventHandler<ByteArrayEventArgs>? RawDataCallback { get; set; }

    /// <summary>
    /// Функция разбора метаинформации.
    /// </summary>
    public EventHandler<JsonRpcEventArgs>? AlternativeMetaParserCallback { get; set; }

    /// <summary>
    /// Адрес сервера.
    /// </summary>
    public string Address { get; private set; } = string.Empty;
    /// <summary>
    /// Порт сервера для стрима.
    /// </summary>
    public string StreamPort { get; private set; } = string.Empty;

    /// <summary>
    /// Адрес для управления устройством через http.
    /// </summary>
    public string CtrlHttpPath { get; private set; } = string.Empty;

    /// <summary>
    /// Порт для управления устройством.
    /// </summary>
    public string CtrlPort { get; private set; } = string.Empty;

    /// <summary>
    /// Идентификатор потока.
    /// </summary>
    public string StreamId { get; private set; } = string.Empty;

    /// <summary>
    /// Время инициализации полученное после открытия потока обмена с устройством.
    /// </summary>
    [CLSCompliant(false)] public TimeInfo InitialTime { get; private set; }

    /// <summary>
    /// Эпоха на момент инициализации потока.
    /// </summary>
    /// <value></value>
    public string InitialTimeEpoch { get; private set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public string InitialTimeScale { get; private set; } = string.Empty;

    /// <summary>
    /// Представляет список сигналов.
    /// </summary>
    public List<HbmSignal> Signals { get; } = new();

    /// <summary>
    /// Представляет флаг парсинга данных.
    /// </summary>
    public bool ISParse { get; init; } = true;

    /// <summary>
    /// Конструктор класса StreamClient
    /// </summary>
    public HbmDevice()
    {
        InitialTime = new TimeInfo();
    }
    /// <summary>
    /// Подписаться на сигналы
    /// </summary>
    /// <param name="signalReferences">Список сигналов.</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public async Task SubscribeAsync(List<string> signalReferences,CancellationToken token)
    {
        //  Создание подписчика
        Subscriber subscriber = new(StreamId, Address, CtrlHttpPath);

        //  Подпись на каналы
        await subscriber.SubscribeAsync(signalReferences,token);
    }

    /// <summary>
    /// Отписаться от сигналов
    /// </summary>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public async Task UnsubscribeAsync(CancellationToken token)
    {
        //  Создание подписчика
        Subscriber subscriber = new(StreamId, Address, CtrlHttpPath);
        
        //  Создание списка каналов
        List<string> signalReferences = new();

        //  Цикл по всем каналам
        foreach (var one in Signals)
        {
            //  Если канал содержит строку
            if (one.SignalReference.Contains("Signal1"))
            {
                //  Добавление канал
                signalReferences.Add(one.SignalReference);
            }
        }

        //  Отписка от каналов.
        await subscriber.UnSubscribeAsync(signalReferences,token);
    }



    
    /// <summary>
    /// Представляет функцию запуска задачи.
    /// </summary>
    /// <param name="address">IP Адресс устройтсва</param>
    /// <param name="streamPort">Порт потока</param>
    /// <param name="controlPort">Порт управления</param>
    public void Start(string address, string streamPort = Common.DAQSTREAM_PORT, string controlPort = "")
    {
        //  Проверка запуска.
        if (!_IsStarted)
        {
            //  Установка IP адреса
            Address = address;

            //   Установка порта
            StreamPort = streamPort;

            //  Установка порта
            CtrlPort = controlPort;

            //  Создание источника токена.
            _TokenSource = new CancellationTokenSource();

            //  Запуск задачи получения данных
            _RoutineTask = Task.Run(async () => await StreamRoutineAsync(_TokenSource.Token));

            //  Установка флага запуска
            _IsStarted = true;
        }

    }

    /// <summary>
    /// Представляет роутину отмены задачи.
    /// </summary>
    /// <param name="token">Токен отмены</param>
    /// <returns>Возвращает задачу.</returns>
    private async Task StreamRoutineAsync(CancellationToken token)
    {
        //  Создание получателя
        TcpReceiver tcpReceiver = new(BufferSize, PortionSize, Time);

        //  Создание клиента 
        using TcpClient client = new();

        //  Подключение к устройству
        await client.ConnectAsync(Address, int.Parse(StreamPort),token).ConfigureAwait(false);

        //  Получение потока соединения
        using var stream = client.GetStream();

        //  Проверка токена
        while (!token.IsCancellationRequested)
        {
            //  Получение данных
            await tcpReceiver.ReceiveBufferAsync(stream, token).ConfigureAwait(false);

            //  Получение данных
            var data = tcpReceiver.GetData();

            //  Обработка данных.
            var readLength = ReadFromArray(data);

            //  Проверка что обработано хоть что то.
            if (readLength > 0)
            {
                //  Создание буфера обработанных данных
                byte[] parsedData = new byte[readLength];

                //  Копирование буфера
                Array.Copy(data, parsedData, readLength);

                //  Вызов события.
                RawDataCallback?.Invoke(this,new(parsedData));

                //  Цикл по всем каналам
                foreach (var one in Signals)
                {
                    //  Очистка данных после callback.
                    one.SignalValue.Clear();

                    //  Очистка временых меток
                    one.SignalNtp.Clear();
                }

                //  Завершение итерации чтения
                tcpReceiver.FinallyShift(readLength);
            }

            //  Ожидание.
            await Task.Delay(20, token).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Представляет функцию разбора данных из массива.
    /// </summary>
    /// <param name="buffer">Данные</param>
    /// <returns>Индекс прочитанных данных.</returns>
    public long ReadFromArray(byte[] buffer)
    {
        //  Инициализация счетчика прочитанных данных
        long lengthRead;

        //  Создание потока массива
        using var memory = new MemoryStream(buffer, 0, (int)buffer.Length);

        //  Создание читателя
        using var memoryReader = new BinaryReader(memory, Encoding.UTF8, true);

        //  Выполнять до исключения
        while (true)
        {
            //  Сохранение новой позиции
            lengthRead = memory.Position;

            //  Проверка есть ли заголовок.
            if (memory.Length - lengthRead < 8)
            {
                //  Выход из цикла.
                break;
            }

            //  Чтение заголовка
            HbmHeader header = HbmHeader.GetHeader(memory);

            //  Проверка есть ли тело сообщения.
            if ((memory.Length - memory.Position) < header.Size)
            {
                //  Выход из цикла.
                break;
            }

            //  Проверка типа данных
            switch (header.Type)
            {
                // Пришли данные
                case HbmHeader.MessageType.DATA:
                    {
                        //  Чтение массива данных
                        byte[] data = memoryReader.ReadBytes((int)header.Size);
                     
                        //  Проверка флага что надо обработывать сигнал.
                        if (ISParse)
                        {
                            //  Цикло по всем сигналам
                            foreach (var one in Signals)
                            {
                                //  Если найден
                                if (one.SignalNumber == header.SignalNumber)
                                {

                                    //  Обработка данных
                                    one.ProcessMeasuredData(data, header.Size);
                                    break;
                                }
                            }
                        }
                    }
                    //  Возврат из case
                    break;
                // Пришла метаинформация    
                case HbmHeader.MessageType.META:
                    {
                        // Читаем метаинформацию
                        var metaInfo = HbmMetaInfo.GetMeta(memory, header.Size);

                        if (metaInfo.Type == HbmMetaInfo.MetaInfoType.JSON)
                        {
                            // Проверка что метаинформация для всего потока
                            if (header.SignalNumber == 0)
                            {
                                //  Обработка метаинформации.
                                ProcessStreamMetaInformation(metaInfo);
                            }
                            else
                            {
                                //  Обработка метаинформации.
                                ProcessSignalMetaInformation(header.SignalNumber, metaInfo);
                            }
                        }
                    }
                    //  Возврат из case
                    break;
                default:
                    // Не понятно что пришло, поэтому ничего не делаем, возможно нужно выкинуть исключение?                       
                    break;
            }
        }
        //  Возврат индекса чтения.
        return lengthRead;
    }



    /// <summary>
    /// Остановить прием данных и отключиться от сервера
    /// </summary>
    /// <param name="token">Токен отмены.</param>
    public async Task StopAsync(CancellationToken token)
    {
        if (_IsStarted)
        {
#pragma warning disable VSTHRD103 // Call async methods when in an async method
            _TokenSource?.Cancel();
#pragma warning restore VSTHRD103 // Call async methods when in an async method
            _TokenSource?.Dispose();
            _IsStarted = false;

            if (_RoutineTask != null)
            {
                await _RoutineTask.WaitAsync(token);
            }

            await UnsubscribeAsync(token);
        }
    }
    /// <summary>
    /// Функция обработки метаинформации потока.
    /// </summary>
    /// <param name="metaInfo">Поступившая метаинформация</param>
    /// <returns></returns>
    private void ProcessStreamMetaInformation(HbmMetaInfo metaInfo)
    {
        //  Получение значения метода
        string? method = metaInfo.Method?.GetValue<string>();

        //  Получение параметров.
        var parameters = metaInfo.Params;

        //  Проверка ссылок.
        if(method is null || parameters is null)
        {
            return;
        }

        //  Если мета инициализации.
        if (method == Common.META_METHOD_INIT)
        {
            //  Получение ID
            StreamId = parameters["streamId"]!.ToString();

            //  Получение командного интерфейса.
            JsonObject commandInterfaces = parameters["commandInterfaces"]!.AsObject();

            //  Если получен интерфейс
            if (commandInterfaces != null)
            {
                //  Цикл по всем интерфейсам
                foreach (var item in commandInterfaces)
                {
                    //  Получение записи.
                    var node = item.Value;

                    //  Проверка записи.
                    if (node is null)
                    {
                        //  Продолжение цикла
                        continue;
                    }

                    //  Получение http метода.
                    //string httpMethod = node!["httpMethod"]!.ToString();

                    // Получение http пути.
                    CtrlHttpPath = node["httpPath"]!.ToString();
                    
                    //  Получение порта.
                    CtrlPort = node["port"]!.ToString();
                }
            }

            return;
        }

        //  Если  мета времени
        if (method == Common.META_METHOD_TIME)
        {
            //  Установка времени.
            InitialTime.Set(metaInfo);

            //  Получение параметра 
            InitialTimeEpoch = parameters["epoch"]!.ToString();

            //  Получение параметра.
            InitialTimeScale = parameters["scale"]!.ToString();

            return;
        }

        //  Вызов внешнего парсера.
        AlternativeMetaParserCallback?.Invoke(this, new(method, parameters));
    }


    /// <summary>
    /// Обрабатывает сигнальную метаинформацию по всем подписанным сигналам
    /// </summary>
    /// <param name="signalNumber">Номер сигнала</param>
    /// <param name="metaInfo">Метаинформация сигнала</param>
    /// <returns></returns>
    private void ProcessSignalMetaInformation(int signalNumber, HbmMetaInfo metaInfo)
    { 
        //  Получение значения метода
        string? method = metaInfo.Method?.GetValue<string>();

        //  Получение параметров.
        var parameters = metaInfo.Params;

        //  Проверка ссылок.
        if (method is null || parameters is null)
        {
            return;
        }
        
        //  Проверка существует ли сигнал.
        HbmSignal? signal = null;

        //  Цикло по всем сигналам
        foreach (var one in Signals)
        {
            //  Если найден
            if (one.SignalNumber == signalNumber)
            {
                //  Установка сигнала
                signal = one;

                break;
            }
        }
        // Новый сигнал
        if (signal is null)
        {
            //  Создание сигнала 
            signal = new(signalNumber);

            //  Добавить в список.
            Signals.Add(signal);
        }

        //  Обработка мета информации.
        signal.ProcessSignalMetaInformation(metaInfo);
    }
}

