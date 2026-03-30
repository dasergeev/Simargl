using System.Net.Sockets;
using System.Net;
using System.Text;
using Apeiron.IO;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO;
using Apeiron.ParserSensorsFiles;
using Apeiron.Recording.Adxl357;
using System.Linq;

namespace PointerDataViewer;

/// <summary>
/// Представляет основной класс задач получения данных от датчиков.
/// </summary>
internal sealed class SensorsTcpReceivers
{
    /// <summary>
    /// Представляет ссылку на задачу подключения клиентов.
    /// </summary>
    private Task? _ServerTask;

    /// <summary>
    /// Представляет токен отмены задачи.
    /// </summary>
    private readonly CancellationToken _StoppingToken;

    /// <summary>
    /// Представляет событие ошибки задач.
    /// </summary>
    internal event EventHandler<StringEventArgs>? Error;

    /// <summary>
    /// Представляет событие получения данных от MTP датчиков.
    /// </summary>
    internal event EventHandler<DataEventArgs>? MtpData;
    
    /// <summary>
    /// Представляет событие получения данных от Adxl датчиков.
    /// </summary>
    internal event EventHandler<DataEventArgs>? AdxlData;

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    internal SensorsTcpReceivers([ParameterNoChecks]CancellationToken stoppingToken)
    {
        // Инициализация токена
        _StoppingToken = stoppingToken;

        //  Для анализатора.
        _ = _ServerTask;
    }

    internal void Start()
    { 
        //  Проверка задачи.
        _ServerTask ??= Task.Run(async () => await Invoker.KeepAsync(InvokeAsync, _StoppingToken).ConfigureAwait(false));
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
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка конфигурации.
        Options configuration = Check.IsNotNull(MainWindow.Option,nameof(MainWindow.Option));

        //  Порт для прослушивания датчиков Adxl.
        int adxlPort = configuration.AdxlStreamPort;

        //  Порт для прослушивания датчиков Adxl.
        int mtpPort = configuration.MtpStreamPort;

        //  Создание средства прослушивания TCP-соединений датчиков Adxl.
        TcpListener adxlListener = new(IPAddress.Any, adxlPort);

        //  Запуск средства прослушивания TCP-соединений датчиков Adxl.
        adxlListener.Start();


        //  Создание средства прослушивания TCP-соединений датчиков MTP.
        TcpListener mtpListener = new(IPAddress.Any, mtpPort);

        //  Запуск средства прослушивания TCP-соединений датчиков MTP.
        mtpListener.Start();

        try
        {
            //  Основной цикл.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Проверка подключений.
                while (adxlListener.Pending())
                {
                    //  Получение клиента.
                    TcpClient client = await adxlListener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                    //  Запуск задачи для работы с датчиком.
                    _ = Task.Run(async delegate
                    {
                        //  Вызов метода для работы с датчиком.
                        await WorkAdxlClientAsync(client, cancellationToken);

                    }, cancellationToken);
                }

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
            //  Остановка прослушивания соедиений Adxl.
            adxlListener.Stop();

            //  Остановка прослушивания соедиений MTP.
            mtpListener.Stop();
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с подключением датчика Adxl.
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
    private async Task WorkAdxlClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
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

                    //  Чтение данных.
                    byte[] buffer = await spreader.ReadBytesAsync(Settings.AdxlPackageSize, cancellationToken).ConfigureAwait(false);

                    //  Проверка количества прочитанных данных.
                    if (buffer.Length > 0)
                    {
                        //  Установка времени получения последнего пакета.
                        lastTime = DateTime.Now;
                    }

                    //  Уменьшение количество доступных данных
                    size -= Settings.AdxlPackageSize;

                    //  Создание потока памяти
                    using MemoryStream memory = new(buffer);

                    //  Создание читателя
                    using BinaryReader parser = new (memory,Encoding.UTF8,true);

                    //   Разбор пакета.
                    Adxl357DataPackage package = Adxl357DataPackage.Load(memory);// parser.ReadObject<Adxl357DataPackage>();

                    //  Инициализация массива.
                    float[][] data = new float[package.Signals.Count][];

                    //  Инициализация индекса.
                    int index = 0;

                    //  Цикл по всем сигналам.
                    foreach(var one in package.Signals)
                    {
                        //  Инициализация массива
                        data[index++] = (float[])one.ToArray().Clone();
                    }

                    //  Вызов события.
                    AdxlData?.Invoke(this, new(point.Address, data));

                    //  Проверка что есть еще данные на чтение
                    if (size > 0)
                    {
                        //  Переход на новую итерацию цикла 
                        continue;
                    }
                }

                //  Проверка времени получения последних данных.
                if ((DateTime.Now - lastTime).TotalMilliseconds > Settings.AdxlWaitingTimeout)
                {
                    //  Превышено время ожидания.
                    throw new TimeoutException($"Превышено время ожидания данных от датчика Adxl {number}.");
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
    private async Task WorkMtpClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        //  Клиент для работы с датчиком.
        using TcpClient client = tcpClient;

        //  Проверка соединения.
        if (client.Client.RemoteEndPoint is not IPEndPoint point)
        {
            //  Вывод информации в журнал.
            OnError($"Подключилось неизвестное устройство на порт MTP {client.Client.RemoteEndPoint}.");

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

        try
        {
            //  Поток для чтения данных.
            using NetworkStream stream = client.GetStream();

            //  Время получения последнего пакета.
            DateTime lastTime = DateTime.Now;

            //  Инициализация размера данных
            int dataLength = 0;

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

                    //  Чтение количества каналов (value = количество установленное в настройках * 2)
                    ushort channelCount = await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);

                    //  Чтение длины пакета в наборах данных по каждому каналу.
                    ushort pointsCount = await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);

                    //  Расчет размера данных
                    dataLength = (channelCount * pointsCount);

                    //  Чтение данных.
                    byte[] buffer = await spreader.ReadBytesAsync(dataLength,cancellationToken).ConfigureAwait(false);

                    //  Проверка количества прочитанных данных.
                    if (buffer.Length > 0)
                    {
                        //  Установка времени получения последнего пакета.
                        lastTime = DateTime.Now;
                    }

                    //  Уменьшение количество доступных данных
                    size -= dataLength;

                    //  Преобразование данных.
                    var data = MtpParser.LoadFromArray(buffer);

                    //  Вызов события.
                    MtpData?.Invoke(this, new(point.Address, data));

                    //  Проверка что есть еще данные на чтение
                    if (size > 0)
                    {
                        //  Переход на новую итерацию цикла 
                        continue;
                    }

                }

                //  Проверка времени получения последних данных.
                if ((DateTime.Now - lastTime).TotalMilliseconds > Settings.MtpWaitingTimeout)
                {
                    //  Превышено время ожидания.
                    throw new TimeoutException($"Превышено время ожидания данных от датчика MTP {number}.");
                }

                //  Передача ресурсов задачи.
                await Task.Delay(5, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            //  Вывод сообщения об ошибке в журнал.
            OnError(ex.Message);

            //  
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
