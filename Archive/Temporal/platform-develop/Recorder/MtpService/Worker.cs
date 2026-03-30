using Apeiron.Support;
using System.Net;
using System.Net.Sockets;
using Apeiron.IO;
using System.Text;
using Apeiron.Platform.Transmitters;
using Apeiron.Platform.Journals;

namespace Apeiron.Mtp;

public class Worker : BackgroundService
{
    /// <summary>
    /// Представляет интерфейс логирования.
    /// </summary>
    private readonly Journal _Journal;

    /// <summary>
    /// Устанавливает и возвращает конфигурацию службы.
    /// </summary>
    public static Options? Configuration { get; set; }

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<Worker> _Logger;


    /// <summary>
    /// Предатавляет передачик данных датчиков.
    /// </summary>
    private BinaryTransmitter? Transmitter;


    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="logger"></param>
    public Worker(ILogger<Worker> logger)
    {
        //  Установка значения системы логирования по умолчанию
        _Logger = logger;

        //  Создание системы логирования.
        _Journal = Journal.FromLogger(_Logger);
    }

    /// <summary>
    /// Выполняет основную работу службы.
    /// </summary>
    /// <param name="stoppingToken">Токен остановки.</param>
    /// <returns>Задача</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //  Выполнение основной работы c cлужбы с перезапуском в случае ошибок
        await Invoker.KeepAsync(InvokeAsync, stoppingToken).ConfigureAwait(false);
    }
    /// <summary>
    /// Асинхронно выполняет основную работу службы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу службы.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <see cref="Configuration"/> передана пустая ссылка.
    /// </exception>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {

        //  Проверка конфигурации.
        Options options = Check.IsNotNull(Configuration, nameof(Configuration));

        //  Проверка конфигурации.
        if (options.TransmitterConfig is not null)
        {
            //  Создание передатчика.
            Transmitter = new BinaryTransmitter(_Journal, options.TransmitterConfig);
        }

        //  Порт для прослушивания датчиков Adxl.
        int mtpPort = options.MtpStreamPort;

        //  Создание средства прослушивания TCP-соединений датчиков MTP.
        TcpListener mtpListener = new(IPAddress.Any, mtpPort);

        //  Запуск средства прослушивания TCP-соединений датчиков MTP.
        mtpListener.Start();

        //  Корректировка времени получения пакета.
        DateTime time = DateTime.Now;

        //  Вывод информации об исключении.
        _Logger.LogInformation("Запущен сервер");
        try
        {
            //  Основной цикл.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Проверка подключений.
                while (mtpListener.Pending())
                {
                    //  Получение клиента.
                    TcpClient client = await mtpListener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                    //  Запуск задачи для работы с датчиком.
                    _ = Task.Run(async delegate
                    {
                        //  Вызов метода для работы с датчиком.
                        await WorkMtpClientAsync(client, cancellationToken);

                    }, cancellationToken);
                }
                
                //  Ожидaние.
                await Task.Delay(options.ListeningTimeout, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            //  Вывод информации об исключении.
            _Logger.LogError("{exception}", ex);
        }
        finally
        {
            //  Остановка прослушивания соедиений MTP.
            mtpListener.Stop();
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с подключением датчика MTP.
    /// </summary>
    /// <param name="tcpClient">
    /// TCP-клиент датчика.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с подключением датчика.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <see cref="Configuration"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <see cref="Transmitter"/> передана пустая ссылка.
    /// </exception>
    private async Task WorkMtpClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        //  Проверка конфигурации.
        Options options = Check.IsNotNull(Configuration, nameof(Configuration));

        //  Проверка передатчика.
        var transmitter = Check.IsNotNull(Transmitter, nameof(Transmitter));

        //  Клиент для работы с датчиком.
        using TcpClient client = tcpClient;

        //  Проверка соединения.
        if (client.Client.RemoteEndPoint is not IPEndPoint point)
        {
            //  Вывод информации в журнал.
            _Logger.LogError("Подключилось неизвестное устройство на порт MTP {point}.", client.Client.RemoteEndPoint);

            //  Выброс исключения.
            throw new IOException();
        }

        //  Получение массива байт.
        IPAddress address = point.Address;

        //  Получение идентификатора.
        long identifier = BitConverter.ToInt32(address.GetAddressBytes());

        //  Вывод информации в журнал.
        _Logger.LogInformation("Подключен датчик MTP: ({point}).", client.Client.RemoteEndPoint);

        try
        {
            //  Поток для чтения данных.
            using NetworkStream stream = client.GetStream();

            //  Время получения последнего пакета.
            DateTime lastTime = DateTime.Now;

            //  Инициализация размера данных
            int dataLength = 0;

            //  Фиксация количества каналов.
            ushort fixChannelCounter = 0;

            //  Основной цикл для работы с датчиком.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Количество данных, доступных для чтения.
                int size = client.Available;

                //  Проверка данных для чтения.
                if (size >=4)
                {
                    //  Сохранение нового времени.
                    lastTime = DateTime.Now;

                    //  Создание читателя.
                    Spreader spreader = new(stream, Encoding.UTF8);

                    //  Чтение количества каналов (value = количество установленное в настройках * 2)
                    ushort channelCount = await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);

                    //  Чтение длины пакета в наборах данных по каждому каналу.
                    ushort pointsCount = await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);
                    
                    //  Проверка фиксированова количества канала
                    if (fixChannelCounter == 0)
                    {
                        //  Установка значения.
                        fixChannelCounter = channelCount;
                    }

                    //  Проверка фиксированого количества каналов.
                    if(fixChannelCounter != channelCount)
                    {
                        //  Выброс исключения.
                        throw new FormatException();
                    }

                    //  Создание потока.
                    using var memory = new MemoryStream();

                    //  Создание писателя.
                    using BinaryWriter writer = new(memory);

                    //  Запись количества каналов в пакете (*2)
                    writer.Write(channelCount);

                    //  Запись количества точек данных в пакете 
                    writer.Write(pointsCount);

                    //  Расчет размера данных
                    dataLength = (channelCount * pointsCount);

                    //  Чтение данных.
                    var array = await spreader.ReadBytesAsync(dataLength, cancellationToken).ConfigureAwait(false);

                    //  Сохранение данных
                    writer.Write(array);

                    //  Передача данных
                    await transmitter.SendTransparentAsync(memory.ToArray(), cancellationToken).ConfigureAwait(false);

                    //  Уменьшение количество доступных данных
                    size -= dataLength + 4;

                    //  Проверка что есть еще данные на чтение
                    if (size > 0)
                    {
                        //  Переход на новую итерацию цикла 
                        continue;
                    }
                }

                //  Проверка времени получения последних данных.
                if ((DateTime.Now - lastTime).TotalMilliseconds > options.MtpWaitingTimeout)
                {
                    //  Превышено время ожидания.
                    throw new TimeoutException($"Превышено время ожидания данных от датчика MTP: {client.Client.RemoteEndPoint}.");
                }

                //  Передача ресурсов задачи.
                await Task.Delay(5, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception e)
        {
            //  Вывод сообщения об ошибке в журнал.
            _Logger.LogError("{message}", e.Message);
        }
    }
}
