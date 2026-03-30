using Apeiron.EventsArgs;
using Apeiron.Platform.Journals;
using Apeiron.Platform.Transmitters;
using Apeiron.Support;
using System.Text.Json.Nodes;

namespace Apeiron.QuantumX;

/// <summary>
/// Представляет класс управляющего коллекцией данных QuantumX
/// </summary>
internal class QuantumXManager
{
    /// <summary>
    /// Предствляет конфигурацию.
    /// </summary>
    private readonly QuantumXOptions _Configuratrion;

    /// <summary>
    /// Представляет интерфейс логирования.
    /// </summary>
    private readonly Journal _Journal;

    /// <summary>
    /// Представляет интерфейс подключения к Quantum.
    /// </summary>
    private HbmDevice Devices { get; }

    /// <summary>
    /// Представляет класс передатчика.
    /// </summary>
    private BinaryTransmitter Transmitter { get; }

    /// <summary>
    /// Возвращает время получения последних данных.
    /// </summary>
    internal DateTime LastReceiveDataTime { get; private set; } = DateTime.Now;

    /// <summary>
    /// Инициализирует объект класса.
    /// </summary>
    /// <param name="logger">Логгер.</param>
    /// <param name="journal">Журнал</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <param name="isRaw">Флаг формата данных.</param>
    internal QuantumXManager(Journal journal, QuantumXOptions configuration)
    {
        //  Установка и проверка конфигурации
        _Configuratrion = Check.IsNotNull(configuration, nameof(configuration));

        //  Установка и проверка конфигурации
        var tranmiterSensorOptions = Check.IsNotNull(_Configuratrion.TransmitterSensorOptions, nameof(_Configuratrion.TransmitterSensorOptions));

        //  Установка и проверка поставщика журнала.
        _Journal = Check.IsNotNull(journal, nameof(journal));

        //  Инициализация передатчика.
        Transmitter = new BinaryTransmitter(_Journal, Check.IsNotNull(tranmiterSensorOptions.TranmitterConfig, nameof(tranmiterSensorOptions.TranmitterConfig)));

        //  Инициализация устройства.
        Devices = new()
        {
            //  Установка обработчика сырых данных.
            RawDataCallback = OnNewRawData,
            AlternativeMetaParserCallback = OnMetaInfo,
            BufferSize = _Configuratrion.TcpReceiverBufferSize,
            PortionSize = _Configuratrion.TcpReceiverPortionSize,
            Time = _Configuratrion.TcpReceiverTimeSpan,
            ISParse = false,
        };
    }

    internal void Start()
    {
        //  Сброс времени.
        LastReceiveDataTime = DateTime.Now;

        //  Установка и проверка конфигурации
        var tranmiterSensorOptions = Check.IsNotNull(_Configuratrion.TransmitterSensorOptions, nameof(_Configuratrion.TransmitterSensorOptions));

        //  Запуск устройства.
        Devices.Start(Check.IsNotNull(tranmiterSensorOptions.SensorIP, nameof(tranmiterSensorOptions.SensorIP)));
    }

    internal async Task StopAsync(CancellationToken token)
    {
        //  Запуск устройства.
        await Devices.StopAsync(token);
    }

    private void OnNewRawData(object? obj,ByteArrayEventArgs e)
    {
        //  Проверка размера массива.
        if (e.Data.Length > 0)
        { 
            //  Передача вектора.
            Transmitter.SendTransparent(e.Data);

            //  Установка времени получения данных
            LastReceiveDataTime = DateTime.Now;
        }
    }
    /// <summary>
    /// Представляет функцию подписи на каналы.
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <param name="method">Метод</param>
    /// <param name="prm">Данные.</param>
    public void OnMetaInfo(object? obj,JsonRpcEventArgs e)
    {
        // Подписываемся на все имеющиеся сигналы
        if (e.Method == Common.META_METHOD_AVAILABLE)
        {
            //  Создание списка.
            List<string> signalReferences = new();

            //  Получение массива.
            JsonArray signals = e.Params.AsArray();

            //  Цикл по всему массиву
            foreach (var one in signals)
            {
                //  Проверка ссылки единицы массива
                if (one is not null)
                {
                    //  Получение имени канала
                    string name = one.ToString();

                    //  Проверка что содержит суффикс.
                    if (name.Contains("Signal1"))
                    {
                        //  Добавление в список единицы массива.
                        signalReferences.Add(name);
                    }
                }
            }

            //  Подпись на сигналы.
            _ = Task.Run(async () =>
            {
                //  Подписывание на каналы.
                await Devices.SubscribeAsync(signalReferences,default);
            });

        }
        else if (e.Method == Common.META_METHOD_UNAVAILABLE)
        {

        }
        else if (e.Method == Common.META_METHOD_ALIVE)
        {

        }
        else if (e.Method == Common.META_METHOD_FILL)
        {

        }
    }
}
