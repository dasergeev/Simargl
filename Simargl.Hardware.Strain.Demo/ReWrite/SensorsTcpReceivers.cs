using System.Net.Sockets;
using System.Net;
using System.IO;
using Simargl.IO;

namespace Simargl.Hardware.Strain.Demo.ReWrite;

/// <summary>
/// Представляет основной класс задач получения данных от датчиков.
/// </summary>
internal sealed class SensorsTcpReceivers
{
    ///// <summary>
    ///// Представляет ссылку на задачу подключения клиентов.
    ///// </summary>
    //private Task? _ServerTask;

    ///// <summary>
    ///// Представляет токен отмены задачи.
    ///// </summary>
    //private readonly CancellationToken _StoppingToken;

    /// <summary>
    /// Представляет событие ошибки задач.
    /// </summary>
    internal event EventHandler<StringEventArgs>? Error;
    
    /// <summary>
    /// Представляет событие получения данных от Tenso датчиков.
    /// </summary>
    internal event EventHandler<DataEventArgs>? TensoData;

    /// <summary>
    /// Представляет событие получения первого пакета от Tenso датчиков.
    /// </summary>
    internal event EventHandler<DataEventArgs>? Connected;

    ///// <summary>
    ///// Инициализирует объект.
    ///// </summary>
    //internal SensorsTcpReceivers([NoVerify]CancellationToken stoppingToken)
    //{
    //    // Инициализация токена
    //    _StoppingToken = stoppingToken;

    //    ////  Для анализатора.
    //    //_ = _ServerTask;
    //}

    //internal void Start()
    //{ 
    //    //  Проверка задачи.
    //    _ServerTask ??= Task.Run(async () => await Invoker.KeepAsync(InvokeAsync, _StoppingToken).ConfigureAwait(false));
    //}

    /// <summary>
    /// Асинхронно выполняет основную работу службы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу службы.
    /// </returns>
    public async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Порт для прослушивания датчиков Tenso.
        int tensoPort = 49003;

        //  Создание средства прослушивания TCP-соединений датчиков Tenso.
        TcpListener tensoListener = new(IPAddress.Any, tensoPort);

        //  Запуск средства прослушивания TCP-соединений датчиков Tenso.
        tensoListener.Start();

        try
        {
            //  Основной цикл.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Проверка подключений.
                while (tensoListener.Pending())
                {
                    //  Получение клиента.
                    TcpClient client = await tensoListener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                    //  Запуск задачи для работы с датчиком.
                    _ = Task.Run(async delegate
                    {
                        //  Вызов метода для работы с датчиком.
                        await WorkTensoClientAsync(client, cancellationToken);

                    }, cancellationToken);
                }

                //  Ожидaние.
                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
                
            }
        }
        catch (Exception ex)
        {
            //  Вывод сообщения об ошибке в журнал.
            OnError(ex.Message);
        }
        finally
        {
            //  Остановка прослушивания соедиений Tenso.
            tensoListener.Stop();
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с подключением датчика Tenso.
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
    private async Task WorkTensoClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        //  Клиент для работы с датчиком.
        using TcpClient client = tcpClient;

        //  Проверка соединения.
        if (client.Client.RemoteEndPoint is not IPEndPoint point)
        {
            //  Выброс исключения.
            throw new IOException();
        }

        //  Номер датчика.
        int number = 0;

        //  Получение номера датчика.
        {
            //  Получение массива байт.
            byte[] bytes = point.Address.GetAddressBytes();

            //  Проверка длины массива байт.
            if (bytes.Length != 4)
            {
                //  Выброс исключения.
                throw new IOException();
            }

            //  Получение номера датчика.
            number = bytes[3];
        }

        //  Время получения первого пакета в файле.
        DateTime beginTime = DateTime.Now;

        try
        {
            //  Поток для чтения данных.
            using NetworkStream stream = client.GetStream();

            //  Время получения последнего пакета.
            DateTime lastTime = DateTime.Now;

            //  Создание флага
            bool isFirst = true;

            //  Основной цикл для работы с датчиком.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Количество данных, доступных для чтения.
                int size = client.Available;

                //  Проверка данных для чтения.
                if (size > 0)
                {
                    //  Создание читателя.
                    Spreader spreader = new(stream, Encoding.UTF8);

                    //  Чтение префикса.
                    uint prefix = spreader.ReadUInt32();

                    //  Проверка префикса
                    if (prefix != 0x6E727041)
                    {
                        //  Выброс исключения
                        throw new FormatException();
                    }

                    //  Получение формата.
                    uint format = spreader.ReadUInt32();

                    //  Проверка формата.
                    if(format != 5)
                    {
                        //  Выброс исключения
                        throw new FormatException();
                    }

                    //  Чтение длинны пакета
                    ulong packageSize = spreader.ReadUInt64();

                    //  Получение данных
                    byte[] array = spreader.ReadBytes((int)packageSize);

                    //  Создание потока.
                    using MemoryStream memory = new(array);

                    //   Создание читателя.
                    using var reader = new BinaryReader(memory, Encoding.UTF8, true);

                    //  Чтение серийного номера
                    uint serialNumber  = reader.ReadUInt32();

                    //  Чтение поля количества каналов и длины данных
                    uint nLength = reader.ReadUInt32();

                    //  Получение количества каналов
                    byte channelCount =(byte)(nLength & 0xFF);

                    //  Получение количества точек данных
                    uint pointCount = nLength >> 8 & 0xFFFFFF;

                    //  Получение флага времени
                    byte syncFlag = reader.ReadByte();

                    //  Получение времени в секундах
                    ulong timeUnix = reader.ReadUInt64();

                    //  Получение младшей части времени
                    uint timeNano = reader.ReadUInt32();

                    //  Получение младшей части времени
                    //uint temp  = reader.ReadUInt32();


                    //  Получение температуры
                    float cpuTemp = reader.ReadSingle();

                    //  Получение температуры
                    float sensorTemp = reader.ReadSingle();

                    //  Получение напряжение питания
                    float cpuPower = reader.ReadSingle();

                    //  Получение данных
                    //byte[] array = spreader.ReadBytes((int)(packageSize - 33));



                    //  Инициализация первого измерения массива.
                    float[][] data = new float[channelCount][];

                    //  Цикл по всем каналам.
                    for (int i = 0; i < channelCount; i++)
                    {
                        //  Инициализация массива каналов.
                        data[i] = new float[pointCount];
                    }

                    //  Цикл по всему файлу.
                    for (long j = 0; j < pointCount; j++)
                    {
                        //  Цикл по всем каналам
                        for (int i = 0; i < channelCount; i++)
                        {
                            //  Конвертация и сохранение данных из файла.
                            data[i][j] = reader.ReadSingle();
                        }
                    }

                    if (isFirst)
                    {
                        isFirst = false;
                        Connected?.Invoke(this,new(serialNumber, data));
                    }
                    else
                    {
                        TensoData?.Invoke(this, new(serialNumber, data));
                    }

                    lastTime = DateTime.Now;
                }

                //  Проверка времени получения последних данных.
                if ((DateTime.Now - lastTime).TotalMilliseconds > 5000)
                {
                    //  Превышено время ожидания.
                    throw new TimeoutException($"Превышено время ожидания данных от датчика Tenso {number}.");
                }

                //  Передача ресурсов задачи.
                await Task.Delay(5, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            //  Вывод сообщения об ошибке в журнал.
            OnError(ex.Message);
        }
    }


    /// <summary>
    /// Функция вызова события <see cref="Error"/>
    /// </summary>
    /// <param name="message">
    /// Сообщение.
    /// </param>
    private void OnError(string message)
    {
        //  Вызов события
        Error?.Invoke(this, new(message));
    }
}
