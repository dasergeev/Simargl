using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Simargl.Hardware.Receiving.Net;
using Simargl.Hardware.Recorder.Core;
using Simargl.Recording.Geolocation.Nmea;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;

namespace Simargl.Hardware.Recorder.Measurement;

/// <summary>
/// Представляет измеритель.
/// </summary>
public sealed class Measurer
{
    /// <summary>
    /// Поле для хранения настроек.
    /// </summary>
    private readonly MeasurementOptions _Options = new();

    /// <summary>
    /// Поле для хранения коллекции индикаторов.
    /// </summary>
    private MeasurementIndicatorCollection _Indicators = null!;

    /// <summary>
    /// Поле для хранения очереди данных от датчиков ускорения.
    /// </summary>
    private readonly ConcurrentQueue<TcpDataReceiveResult> _AdxlQueue = [];

    /// <summary>
    /// Поле для хранения очереди данных от тензомодулей.
    /// </summary>
    private readonly ConcurrentQueue<TcpDataReceiveResult> _StrainQueue = [];

    /// <summary>
    /// Поле для хранения очереди данных от геолокации.
    /// </summary>
    private readonly ConcurrentQueue<UdpDataReceiveResult> _GeolocationQueue = [];

    /// <summary>
    /// Поле для хранения очереди данных от геолокации.
    /// </summary>
    private readonly ConcurrentQueue<UdpDataReceiveResult> _RS485Queue = [];

    /// <summary>
    /// Поле для хранения управляющего буферами данных тензомодулей.
    /// </summary>
    private readonly StrainBufferManager _StrainBufferManager = new();

    /// <summary>
    /// Возвращает коллекцию индикаторов.
    /// </summary>
    public MeasurementIndicatorCollection Indicators => _Indicators;

    /// <summary>
    /// Возвращает индикатор геолокации.
    /// </summary>
    public GeolocationIndicator GeolocationIndicator { get; } = new();

    /// <summary>
    /// 
    /// </summary>
    public MeasurementIndicator[] RSIndicators { get; }

    /// <summary>
    /// 
    /// </summary>
    public Measurer()
    {
        RSIndicators = [
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[16]", 0, "UxBuf1", "mm/mm", 1),
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[11]", 0, "UxBuf2", "mm/mm", 1),
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[15]", 0, "UzBux1", "mm/mm", 1),
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[17]", 0, "UzDV1", "mm/mm", 1),
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[14]", 0, "UyDG2", "mm/mm", 1),
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[19]", 0, "UzDV2", "mm/mm", 1),
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[13]", 0, "UzBux2", "mm/mm", 1),
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[12]", 0, "Alfa", "mm/mm", 1),
            new MeasurementIndicator(84, "RS485", string.Empty, "RS[21]", 0, "PTrC", "MPa", 1),
            ];
    }

    /// <summary>
    /// Асинхронно выполняет работу с данными датчиков ускорения.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с данными датчиков ускорения.
    /// </returns>
    public async Task AdxlInvokeAsync(ILogger logger, CancellationToken cancellationToken)
    {
        //  Вывод информации в журнал.
        logger.LogInformation("Запуск работы с данными датчиков ускорения.");

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Извлечение данных из очереди.
            while (_AdxlQueue.TryDequeue(out TcpDataReceiveResult? tcpData) &&
                !cancellationToken.IsCancellationRequested)
            {
                //  Проверка данных.
                if (tcpData is not null)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Создание потока.
                        using MemoryStream stream = new(tcpData.Data);

                        //  Создание средства чтения двоичных данных.
                        using BinaryReader reader = new(stream);

                        //  Чтение сигнатуры.
                        long signature = reader.ReadInt64();    //  8

                        //  Проверка сигнатуры.
                        if (signature != 0x545345544C494152)
                        {
                            //  Неверная сигнатура пакета данных Adxl.
                            throw new InvalidDataException("Неверная сигнатура пакета данных Adxl.");
                        }

                        //  Загрузка описания данных.
                        uint description = reader.ReadUInt32(); //  4

                        //  Определение количества асинхронных значений.
                        int countAsyncValues = (int)(description >> 24);

                        //  Определение длины блока передаваемых данных.
                        int dataSize = (int)(0x00FFFFFF & description);

                        //  Расчёт количества синхронных значений.
                        int length = ((dataSize >> 2) - countAsyncValues) / 3;

                        //  Проверка количества синхронных значений.
                        if (dataSize != length * 3 + countAsyncValues << 2 ||
                            countAsyncValues != 3 ||
                            length != 50)
                        {
                            //  Неверный формат пакета данных Adxl.
                            throw new InvalidDataException("Неверный формат пакета данных Adxl.");
                        }

                        //  Загрузка синхромаркера.
                        long synchromarker = reader.ReadInt64();    //  8

                        //  Создание массива асинхронных данных.
                        float[] asyncValues = new float[countAsyncValues];

                        //  Чтение массива асинхронных данных.
                        for (int i = 0; i < countAsyncValues; i++)
                        {
                            //  Чтение асинхронного значения.
                            asyncValues[i] = reader.ReadSingle();   //  3 * 4
                        }

                        //  Создание массивов синхронных данных.
                        float[] xSignal = new float[length];
                        float[] ySignal = new float[length];
                        float[] zSignal = new float[length];

                        //  Чтение синхронных данных.
                        for (int i = 0; i < length; i++)
                        {
                            xSignal[i] = reader.ReadSingle();   //  4 * 50
                            ySignal[i] = reader.ReadSingle();   //  4 * 50
                            zSignal[i] = reader.ReadSingle();   //  4 * 50
                        }

                        //  Добавление данных.
                        Heart.Unique.Measurer.Indicators.TryAddAdxl(
                            tcpData.EndPoint.Address, [xSignal, ySignal, zSignal]);
                    }
                    catch (Exception ex)
                    {
                        //  Вывод информации в журнал.
                        logger.LogError("Произошла ошибка при разборе данных: {ex}", ex);

                        //  Добавление в очередь плохих потоков.
                        Heart.Unique.BadAdxlKeys.Enqueue(tcpData.ConnectionKey);
                    }
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с данными тензомодулей.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с данными тензомодулей.
    /// </returns>
    public async Task StrainInvokeAsync(ILogger logger, CancellationToken cancellationToken)
    {
        //  Вывод информации в журнал.
        logger.LogInformation("Запуск работы с данными тензомодулей.");

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Извлечение данных из очереди.
            while (_StrainQueue.TryDequeue(out TcpDataReceiveResult? receiveData) &&
                !cancellationToken.IsCancellationRequested)
            {
                //  Проверка данных.
                if (receiveData is not null)
                {
                    //  Добавление данных в буфер.
                    await _StrainBufferManager.AddAsync(receiveData, cancellationToken).ConfigureAwait(false);
                }
            }

            //  Нормализация данных.
            await _StrainBufferManager.NormalizationAsync(cancellationToken).ConfigureAwait(false);

            //  Чтение пакетов.
            while (await _StrainBufferManager.TryReadAsync(logger, cancellationToken))
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с данными RS485.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с данными RS485.
    /// </returns>
    public async Task RS485InvokeAsync(ILogger logger, CancellationToken cancellationToken)
    {
        //  Вывод информации в журнал.
        logger.LogInformation("Запуск работы с данными RS485.");

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Извлечение данных из очереди.
            while (_RS485Queue.TryDequeue(out UdpDataReceiveResult? updData) &&
                !cancellationToken.IsCancellationRequested)
            {
                //  Проверка данных.
                if (updData is not null)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Создание потока.
                        using MemoryStream stream = new(updData.Result.Buffer);

                        //  Создание средства чтения двоичных данных.
                        using StreamReader reader = new(stream, Encoding.ASCII);

                        //  Чтение текста.
                        string text = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);

                        //  Получение строк.
                        string[] lines = [.. text.Split('\n').Select(x => x.Trim()).Where(x => x.Length > 0)];

                        //  Значения.
                        double UxBuf1 = double.NaN; //  16, 
                        double UxBuf2 = double.NaN; //  11, 
                        double UzBux1 = double.NaN; //  15, 
                        double UzDV1 = double.NaN; //  17, 
                        double UyDG2 = double.NaN; //  14, 
                        double UzDV2 = double.NaN; //  19, 
                        double UzBux2 = double.NaN; //  13, 
                        double Alfa = double.NaN; //  12, 
                        double PTrC = double.NaN; //  21

                        //  Перебор строк.
                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i] == "[11,12,13,14,15,16,17,19]" &&
                                i + 8 < lines.Length)
                            {
                                UxBuf2 = getValue(lines[i + 1]); //  11, 
                                Alfa = getValue(lines[i + 2]); //  12, 
                                UzBux2 = getValue(lines[i + 3]); //  13, 
                                UyDG2 = getValue(lines[i + 4]); //  14, 
                                UzBux1 = getValue(lines[i + 5]); //  15, 
                                UxBuf1 = getValue(lines[i + 6]); //  16, 
                                UzDV1 = getValue(lines[i + 7]); //  17, 
                                UzDV2 = getValue(lines[i + 8]); //  19, 
                                i += 8;
                            }
                            else if (lines[i] == "[21]" &&
                                i + 1 < lines.Length)
                            {
                                PTrC = getValue(lines[i + 1]);
                                ++i;
                            }
                        }

                        setValue(0, UxBuf1);
                        setValue(1, UxBuf2);
                        setValue(2, UzBux1);
                        setValue(3, UzDV1);
                        setValue(4, UyDG2);
                        setValue(5, UzDV2);
                        setValue(6, UzBux2);
                        setValue(7, Alfa);
                        setValue(8, PTrC);

                        void setValue(int index, double value)
                        {
                            if (!double.IsNaN(value))
                            {
                                RSIndicators[index].AddValues([(float)value]);
                            }
                        }

                        /*

                                                
                        [8]:         0.0419639


                            private static int[] _RS485Ids = [];
    private static string[] _RS485Names = ["UxBuf1", "UxBuf2", "UzBux1", "UzDV1", "UyDG2", "UzDV2", "UzBux2", "Alfa", "PTrC"];

                        */



                        /*

                        
                        [257]:         40517 (-25019)
                        [257]:         8797
                        [257]:         34225 (-31311)
                        [257]:         15942
                        [257]:         46364 (-19172)
                        [257]:         41423 (-24113)
                        [257]:         19002
                        [257]:         26017
                        [11,12,13,14,15,16,17,19]
                        [257]:         40514 (-25022)
                        [257]:         8798
                        [257]:         34227 (-31309)
                        [257]:         15963
                        [257]:         46436 (-19100)
                        [257]:         41423 (-24113)
                        [257]:         19003
                        [257]:         26018
                        [11,12,13,14,15,16,17,19]
                        [257]:         40516 (-25020)
                        [257]:         8798
                        [257]:         34224 (-31312)
                        [257]:         15964
                        [257]:         46427 (-19109)
                        [257]:         41424 (-24112)
                        [257]:         19001
                        [257]:         26015
                        [11,12,13,14,15,16,17,19]
                        [257]:         40515 (-25021)
                        [257]:         8798
                        [257]:         34222 (-31314)
                        [257]:         15964
                        [257]:         46426 (-19110)
                        [257]:         41423 (-24113)
                        [257]:         19001
                        [257]:         26015
                        [11,12,13,14,15,16,17,19]
                        [257]:         40517 (-25019)
                        [257]:         8798
                        [257]:         34227 (-31309)
                        [257]:         15964
                        [257]:         46431 (-19105)
                        [257]:         41367 (-24169)
                        [257]:         19002
                        [257]:         26014
                        [11,12,13,14,15,16,17,19]
                        [257]:         40516 (-25020)
                        [257]:         8797
                        [257]:         34232 (-31304)
                        [257]:         15963
                        [257]:         46429 (-19107)
                        [257]:         41335 (-24201)
                        [257]:         19002
                        [257]:         26017
                        [11,12,13,14,15,16,17,19]
                        [257]:         40515 (-25021)
                        [257]:         8797
                        [257]:         34227 (-31309)
                        [257]:         15964
                        [257]:         46429 (-19107)
                        [257]:         41422 (-24114)
                        [257]:         19002
                        [257]:         26016
                        [11,12,13,14,15,16,17,19]
                        [257]:         40515 (-25021)
                        [257]:         8797
                        [257]:         34225 (-31311)
                        [257]:         15964
                        [257]:         46425 (-19111)
                        [257]:         41423 (-24113)
                        [257]:         19001
                        [257]:         26014
                        [11,12,13,14,15,16,17,19]
                        [257]:         40516 (-25020)
                        [257]:         8798
                        [257]:         34225 (-31311)
                        [257]:         15962
                        [257]:         46429 (-19107)
                        [257]:         41423 (-24113)
                        [257]:         19001
                        [257]:         25963
                        [11,12,13,14,15,16,17,19]
                        [257]:         40515 (-25021)
                        [257]:         8798
                        [257]:         34226 (-31310)
                        [257]:         15963
                        [257]:         46432 (-19104)
                        [257]:         41423 (-24113)
                        [257]:         18965
                        [257]:         26015
*/


                        static double getValue(string line)
                        {
                            try
                            {
                                double factor = 1.0;
                                if (line.StartsWith("[257]:"))
                                {
                                    factor = 1 / 50000.0;
                                    line = line[6..].Trim();
                                }
                                else if (line.StartsWith("[8]:"))
                                {
                                    line = line[4..].Trim();
                                }
                                else
                                {
                                    return double.NaN;
                                }
                                if (double.TryParse(line.Split(' ')[0], CultureInfo.InvariantCulture, out double value))
                                {
                                    return value * factor;
                                }
                            }
                            catch
                            {

                            }
                            return double.NaN;
                        }



                        ////  Получение сообщений.
                        //IEnumerable<NmeaMessage?> messages = text
                        //    .Split('\n', '\r')
                        //    .Select(x => x.Trim())
                        //    .Where(x => x.Length > 0)
                        //    .Select(x =>
                        //    {
                        //        try
                        //        {
                        //            return NmeaMessage.Parse(x);
                        //        }
                        //        catch { }

                        //        return null;
                        //    });

                        //double? latitude = null;
                        //double? longitude = null;
                        //double? speed = null!;

                        ////  Разбор сообщений.
                        //foreach (NmeaMessage? message in messages)
                        //{
                        //    //  Разбор сообщения.
                        //    if (message is NmeaGgaMessage ggaMessage)
                        //    {
                        //        if (ggaMessage.Latitude.HasValue)
                        //            latitude = ggaMessage.Latitude.Value;
                        //        if (ggaMessage.Longitude.HasValue)
                        //            longitude = ggaMessage.Longitude.Value;
                        //    }

                        //    //  Разбор сообщения.
                        //    if (message is NmeaRmcMessage rmcMessage)
                        //    {
                        //        if (rmcMessage.Latitude.HasValue)
                        //            latitude = rmcMessage.Latitude.Value;
                        //        if (rmcMessage.Longitude.HasValue)
                        //            longitude = rmcMessage.Longitude.Value;
                        //        if (rmcMessage.Speed.HasValue)
                        //            speed = rmcMessage.Speed.Value;
                        //    }

                        //    //  Разбор сообщения.
                        //    if (message is NmeaVtgMessage vtgMessage)
                        //    {
                        //        if (vtgMessage.Speed.HasValue)
                        //            speed = vtgMessage.Speed.Value;
                        //    }
                        //}

                        ////  Добавление данных.
                        //GeolocationIndicator.Update(latitude, longitude, speed);
                    }
                    catch (Exception ex)
                    {
                        //  Вывод информации в журнал.
                        logger.LogError("Произошла ошибка при разборе данных: {ex}", ex);
                    }
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с данными геолокации.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с данными геолокации.
    /// </returns>
    public async Task GeolocationInvokeAsync(ILogger logger, CancellationToken cancellationToken)
    {
        //  Вывод информации в журнал.
        logger.LogInformation("Запуск работы с данными геолокации.");

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Извлечение данных из очереди.
            while (_GeolocationQueue.TryDequeue(out UdpDataReceiveResult? updData) &&
                !cancellationToken.IsCancellationRequested)
            {
                //  Проверка данных.
                if (updData is not null)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Создание потока.
                        using MemoryStream stream = new (updData.Result.Buffer);

                        //  Создание средства чтения двоичных данных.
                        using StreamReader reader = new(stream, Encoding.ASCII);

                        //  Чтение текста.
                        string text = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);

                        //  Получение сообщений.
                        IEnumerable<NmeaMessage?> messages = text
                            .Split('\n', '\r')
                            .Select(x => x.Trim())
                            .Where(x => x.Length > 0)
                            .Select(x =>
                            {
                                try
                                {
                                    return NmeaMessage.Parse(x);
                                }
                                catch { }

                                return null;
                            });

                        double? latitude = null;
                        double? longitude = null;
                        double? speed = null!;

                        //  Разбор сообщений.
                        foreach (NmeaMessage? message in messages)
                        {
                            //  Разбор сообщения.
                            if (message is NmeaGgaMessage ggaMessage)
                            {
                                if (ggaMessage.Latitude.HasValue)
                                    latitude = ggaMessage.Latitude.Value;
                                if (ggaMessage.Longitude.HasValue)
                                    longitude = ggaMessage.Longitude.Value;
                            }

                            //  Разбор сообщения.
                            if (message is NmeaRmcMessage rmcMessage)
                            {
                                if (rmcMessage.Latitude.HasValue)
                                    latitude = rmcMessage.Latitude.Value;
                                if (rmcMessage.Longitude.HasValue)
                                    longitude = rmcMessage.Longitude.Value;
                                if (rmcMessage.Speed.HasValue)
                                    speed = rmcMessage.Speed.Value;
                            }

                            //  Разбор сообщения.
                            if (message is NmeaVtgMessage vtgMessage)
                            {
                                if (vtgMessage.Speed.HasValue)
                                    speed = vtgMessage.Speed.Value;
                            }
                        }

                        //  Добавление данных.
                        GeolocationIndicator.Update(latitude, longitude, speed);
                    }
                    catch (Exception ex)
                    {
                        //  Вывод информации в журнал.
                        logger.LogError("Произошла ошибка при разборе данных: {ex}", ex);
                    }
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Присоединяет настройки.
    /// </summary>
    /// <param name="configuration">
    /// Конфигурация.
    /// </param>
    public void Bind(ConfigurationManager configuration)
    {
        //  Чтение настроек.
        configuration.GetSection("Measurement").Bind(_Options);

        //  Создание коллекции индикаторов.
        _Indicators = new(_Options);
    }

    /// <summary>
    /// Добавляет данные от датчика ускорения.
    /// </summary>
    /// <param name="data">
    /// Данные от датчика ускорения.
    /// </param>
    public void AddAdxlData(TcpDataReceiveResult data)
    {
        //  Добавление данных в очередь.
        _AdxlQueue.Enqueue(data);
    }

    /// <summary>
    /// Добавляет данные от тензомодуля.
    /// </summary>
    /// <param name="data">
    /// Данные от тензомодуля.
    /// </param>
    public void AddStrainData(TcpDataReceiveResult data)
    {
        //  Добавление данных в очередь.
        _StrainQueue.Enqueue(data);
    }

    /// <summary>
    /// Добавляет данные геолокации.
    /// </summary>
    /// <param name="data">
    /// Данные геолокации.
    /// </param>
    public void AddGeolocationData(UdpDataReceiveResult data)
    {
        //  Добавление данных в очередь.
        _GeolocationQueue.Enqueue(data);
    }

    /// <summary>
    /// Добавляет данные геолокации.
    /// </summary>
    /// <param name="data">
    /// Данные геолокации.
    /// </param>
    public void AddRS485Data(UdpDataReceiveResult data)
    {
        //  Добавление данных в очередь.
        _RS485Queue.Enqueue(data);
    }
}
