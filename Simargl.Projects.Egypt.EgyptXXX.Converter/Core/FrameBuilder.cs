using Simargl.Frames;
using Simargl.Payload;
using Simargl.Payload.Recording;
using Simargl.Recording.AccelEth3T;
using Simargl.Recording.Geolocation.Nmea;
using System.Globalization;
using System.IO;

namespace Simargl.Projects.Egypt.EgyptXXX.Converter.Core;

/// <summary>
/// Представляет построителя кадра.
/// </summary>
/// <param name="time">
/// Время кадра.
/// </param>
/// <param name="files">
/// Исходные файлы.
/// </param>
/// <param name="options">
/// Настройки.
/// </param>
public sealed class FrameBuilder(DateTime time, List<FileDetails> files, MeasurementOptions options)
{
    /// <summary>
    /// Постоянная, определяющая частоту дискретизации для записи в файл.
    /// </summary>
    private const int _Sampling = 300;

    /// <summary>
    /// Постоянная, определяющая частоту дискретизации датчиков ускорения.
    /// </summary>
    private const double _AdxlSampling = 500;

    /// <summary>
    /// Постоянная, определяющая максимальный заполняемый соседними данными пропуск тензометрических данных.
    /// </summary>
    private const int _StrainMaxGap = 75;

    /// <summary>
    /// Постоянная, определяющая максимальный заполняемый соседними данными пропуск данных ускорений.
    /// </summary>
    private const int _AdxlMaxGap = 75;

    /// <summary>
    /// Возвращает кадр.
    /// </summary>
    public Frame Frame { get; } = new();

    /// <summary>
    /// Возвращает среднюю скорость.
    /// </summary>
    public double AverageSpeed { get; private set; }

    /// <summary>
    /// Возвращает номер кадра.
    /// </summary>
    public int Number { get; } = time.Hour * 60 + time.Minute + 1;

    /// <summary>
    /// Поле для хранения данных тензометрии.
    /// </summary>
    private Dictionary<uint, StrainData[]> _StrainData = [];

    /// <summary>
    /// Асинхронно возвращает данные для файла CSV.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая данные для файла CSV.
    /// </returns>
    public async Task<string> ToCsvAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Определение длины канала.
        int length = 60 * _Sampling;

        //  Создание построителя строки.
        StringBuilder output = new();

        //  Перебор значений.
        for (int i = 0; i < length; i++)
        {
            //  Проверка токена отмены.
            cancellationToken.ThrowIfCancellationRequested();

            //  Определение времени.
            double time = i / (double)_Sampling;

            //  Добавление времени.
            output.Append(time);

            //  Перебор каналов.
            foreach (Channel channel in Frame.Channels)
            {
                //  Запись разделителя.
                output.Append(';');

                //  Запись значения.
                output.Append(channel[i]);
            }

            //  Переход на новую строку.
            output.AppendLine();
        }

        //  Ожидание завершённой задачи.
        await Task.CompletedTask.ConfigureAwait(false);

        //  Возврат строки.
        return output.ToString();
    }

    /// <summary>
    /// Асинхронно выполняет построение кадра.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение кадра.
    /// </returns>
    public async Task BuildAsync(CancellationToken cancellationToken)
    {
        //  Очистка каналов.
        Frame.Channels.Clear();

        //  Перебор файлов.
        foreach (FileDetails file in files)
        {
            //  Загрузка данных.
            await file.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        //  Сбор данных тензометрии.
        await StrainLoadAsync(cancellationToken).ConfigureAwait(false);

        //  Получение данных геолокации.
        (Channel speed, Channel longitude, Channel latitude) = await GeolocationAsync(cancellationToken).ConfigureAwait(false);

        //  Добавление каналов.
        Frame.Channels.Add(speed);
        Frame.Channels.Add(longitude);
        Frame.Channels.Add(latitude);

        //  Перебор источников данных.
        foreach (MeasurementSource source in options.Sources)
        {
            //  Каналы.
            Channel[] channels = [];

            //  Проверка данных тензометрии.
            if (source.Type == "Strain")
            {
                //  Получение каналов тензометрии.
                channels = await BuildStrainAsync(source, cancellationToken).ConfigureAwait(false);
            }

            //  Проверка данных ускорений.
            if (source.Type == "Adxl")
            {
                //  Получение каналов ускорений.
                channels = await BuildAdxlAsync(source, cancellationToken).ConfigureAwait(false);
            }

            //  Перебор каналов.
            foreach (Channel channel in channels)
            {
                //  Проверка имени канала.
                if (channel.Name != "-")
                {
                    //  Добавление канала в кадр.
                    Frame.Channels.Add(channel);
                }
            }
        }

        //  Добавление RS-каналов.
        Frame.Channels.AddRange(await RS485Async(cancellationToken).ConfigureAwait(false));
    }

    /// <summary>
    /// Асинхронно выполняет построение каналов тензометрии.
    /// </summary>
    /// <param name="source">
    /// Информация об источнике данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение каналов тензометрии.
    /// </returns>
    private async Task<Channel[]> BuildStrainAsync(MeasurementSource source, CancellationToken cancellationToken)
    {
        //  Определение длины канала.
        int length = 60 * _Sampling;

        //  Создание массива каналов.
        Channel[] channels = new Channel[4];

        //  Создание буфера данных.
        List<float>[][] buffer = [.. 
            Enumerable.Range(0, channels.Length)
            .Select(_ => Enumerable.Range(0, length).Select(_ => new List<float>()).ToArray())];

        //  Перебор каналов.
        for (int i = 0; i < channels.Length; i++)
        {
            //  Создание канала.
            channels[i] = new(source.Names[i], source.Units[i], _Sampling, 0.5 * _Sampling, length);
        }

        //  Получение данных.
        StrainData[]? data = _StrainData
            .Where(x => x.Key.ToString("X8").Equals(source.Serial, StringComparison.CurrentCultureIgnoreCase))
            .Select(x => x.Value)
            .FirstOrDefault();

        //  Проверка данных.
        if (data is not null)
        {
            //  Перебор данных.
            foreach (StrainData item in data)
            {
                //  Проверка токена отмены.
                cancellationToken.ThrowIfCancellationRequested();

                //  Определение начального индекса.
                double beginIndex = (item.ReceiptTime - time).TotalSeconds * _Sampling;

                //  Определение множителя для индекса.
                double indexFactor = _Sampling / item.Sampling;

                //  Перебор каналов.
                for (int channelIndex = 0; channelIndex < channels.Length; channelIndex++)
                {
                    //  Получение целевого буфера.
                    List<float>[] target = buffer[channelIndex];

                    //  Получение источника.
                    float[] values = item.Data[channelIndex];

                    //  Перебор значений.
                    for (int i = 0; i < values.Length; i++)
                    {
                        //  Получение индекса.
                        int index = (int)(beginIndex + i * indexFactor);

                        //  Проверка индекса.
                        if (0 <= index && index < length)
                        {
                            //  Добавление значения.
                            target[index].Add(values[i]);
                        }
                    }
                }
            }
        }

        //  Перебор каналов.
        for (int channelIndex = 0; channelIndex < channels.Length; channelIndex++)
        {
            //  Перебор значений.
            for (int i = 0; i < length; i++)
            {
                //  Проверка данных.
                if (buffer[channelIndex][i].Count > 0)
                {
                    //  Получение значения.
                    channels[channelIndex][i] = buffer[channelIndex][i].Average();
                }
            }

            //  Нормализация значений.
            FillSmallZeroGaps(channels[channelIndex].Items, _StrainMaxGap);
        }

        //  Ожидание завершённой задачи.
        await Task.CompletedTask;

        //  Возврат каналов.
        return channels;
    }

    /// <summary>
    /// Асинхронно выполняет построение каналов ускорений.
    /// </summary>
    /// <param name="source">
    /// Информация об источнике данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение каналов ускорений.
    /// </returns>
    private async Task<Channel[]> BuildAdxlAsync(MeasurementSource source, CancellationToken cancellationToken)
    {
        //  Определение длины канала.
        int length = 60 * _Sampling;

        //  Создание массива каналов.
        Channel[] channels = new Channel[3];

        //  Создание буфера данных.
        List<double>[][] buffer = [..
            Enumerable.Range(0, channels.Length)
            .Select(_ => Enumerable.Range(0, length).Select(_ => new List<double>()).ToArray())];

        //  Перебор каналов.
        for (int i = 0; i < channels.Length; i++)
        {
            //  Создание канала.
            channels[i] = new(source.Names[i], source.Units[i], _Sampling, 0.5 * _Sampling, length);
        }

        //  Получение данных.
        IEnumerable<TcpDataBlock> data = files
            .Where(x => x.Source.Equals(source.Source, StringComparison.CurrentCultureIgnoreCase))
            .SelectMany(x => x.DataPackages)
            .Where(x => x is TcpDataBlock)
            .Select(x => (TcpDataBlock)x);

        //  Перебор данных.
        foreach (TcpDataBlock block in data)
        {
            //  Проверка токена отмены.
            cancellationToken.ThrowIfCancellationRequested();

            //  Определение начального индекса.
            double beginIndex = (block.ReceiptTime - time).TotalSeconds * _Sampling;

            //  Определение множителя для индекса.
            double indexFactor = _Sampling / _AdxlSampling;

            //  Блок перехвата всех исключений.
            try
            {
                //  Получение потока для чтения данных.
                using MemoryStream stream = new(block.Data);

                //  Чтение пакета данных.
                AccelEth3TDataPackage adxlPackage = await AccelEth3TDataPackage.LoadAsync(stream, cancellationToken).ConfigureAwait(false);

                //  Перебор каналов.
                for (int channelIndex = 0; channelIndex < channels.Length; channelIndex++)
                {
                    //  Получение целевого буфера.
                    List<double>[] target = buffer[channelIndex];

                    //  Получение источника.
                    AccelEth3TSignal values = adxlPackage.Signals[channelIndex];

                    //  Перебор значений.
                    for (int i = 0; i < values.Length; i++)
                    {
                        //  Получение индекса.
                        int index = (int)(beginIndex + i * indexFactor);

                        //  Проверка индекса.
                        if (0 <= index && index < length)
                        {
                            //  Добавление значения.
                            target[index].Add(values[i]);
                        }
                    }
                }
            }
            catch { }
        }

        //  Перебор каналов.
        for (int channelIndex = 0; channelIndex < channels.Length; channelIndex++)
        {
            //  Перебор значений.
            for (int i = 0; i < length; i++)
            {
                //  Проверка данных.
                if (buffer[channelIndex][i].Count > 0)
                {
                    //  Получение значения.
                    channels[channelIndex][i] = buffer[channelIndex][i].Average();
                }
            }

            //  Нормализация значений.
            FillSmallZeroGaps(channels[channelIndex].Items, _AdxlMaxGap);
        }

        //  Ожидание завершённой задачи.
        await Task.CompletedTask;

        //  Возврат каналов.
        return channels;
    }

    /// <summary>
    /// Асинхронно собирает данные тензометрии.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, собирающая данные тензометрии.
    /// </returns>
    private async Task StrainLoadAsync(CancellationToken cancellationToken)
    {
        //  Получение пакетов.
        IEnumerable<TcpDataBlock> packages = files
            .Where(x => x.Format == FileFormat.Strain)
            .SelectMany(x => x.DataPackages)
            .Where(x => x is TcpDataBlock)
            .Select(x => (TcpDataBlock)x)
            .OrderBy(x => x.ReceiptTime);

        //  Создание управляющего буферами.
        StrainBufferManager manager = new();

        //  Создание списка всех данных.
        List<StrainData> allData = [];

        //  Перебор пакетов.
        foreach (TcpDataBlock package in packages)
        {
            //  Добавление пакета.
            await manager.AddAsync(package, cancellationToken).ConfigureAwait(false);

            //  Цикл получения данных.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение данных.
                StrainData[] result = await manager.TryReadAsync(cancellationToken).ConfigureAwait(false);

                //  Проверка данных.
                if (result.Length == 0)
                {
                    //  Переход к следующему пакету.
                    break;
                }

                //  Перебор данных.
                foreach (StrainData data in result)
                {
                    //  Добавление данных.
                    allData.Add(data);
                }
            }
        }

        //  Группировка данных.
        var groups = allData.GroupBy(x => x.ConnectionKey)
            .Select(x => new
            {
                ConnectionKey = x.Key,
                Data = x.OrderBy(x => x.BlockIndex).ToArray(),
            });

        //  Перебор групп.
        foreach (var group in groups)
        {
            //  Определение общей длительности.
            double duration = (group.Data[^1].ReceiptTime - group.Data[0].ReceiptTime).TotalSeconds;

            //  Определение количества значений.
            int count = (group.Data[^1].BlockIndex - group.Data[0].BlockIndex) * group.Data[^1].Data[0].Length;

            //  Проверка значений.
            if (duration > 0 && count > 0)
            {
                //  Определение частоты дискретизации.
                double sampling = count / duration;

                //  Перебор данных.
                foreach (StrainData data in group.Data)
                {
                    //  Установка частоты дискретизации.
                    data.Sampling = sampling;
                }
            }
        }

        //  Подготовка данных.
        _StrainData = allData
            .Where(x => x.Sampling > 0)
            .GroupBy(x => x.SerialNumber)
            .Select(x => new
            {
                SerialNumber = x.Key,
                Data = x.OrderBy(x => x.BlockIndex).ToArray(),
            })
            .ToDictionary(x => x.SerialNumber, x => x.Data);
    }

    /// <summary>
    /// Асинхронно выполняет построение данных геолокации.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение данных геолокации.
    /// </returns>
    private async Task<(Channel Speed, Channel Longitude, Channel Latitude)> GeolocationAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Определение длины канала.
        int length = 60 * _Sampling;

        //  Создание каналов.
        Channel speed = new("Speed", "км/ч", _Sampling, 0.5 * _Sampling, length);
        Channel longitude = new("Longitude", "°", _Sampling, 0.5 * _Sampling, length);
        Channel latitude = new("Latitude", "°", _Sampling, 0.5 * _Sampling, length);

        //  Сброс средней скорости.
        AverageSpeed = 0;

        //  Количество значений скорости.
        int speedCount = 0;

        //  Получение пакетов.
        IEnumerable<DataPackage> packages = files
            .Where(x => x.Format == FileFormat.Nmea)
            .SelectMany(x => x.DataPackages);

        //  Перебор пакетов.
        foreach (DataPackage package in packages)
        {
            //  Проверка типа.
            if (package is UdpDatagram udpDatagram)
            {
                //  Получение индекса.
                int index = (int)((udpDatagram.ReceiptTime - time).TotalSeconds * _Sampling);

                //  Проверка индекса.
                if (index < 0 || index >= length)
                {
                    //  Переход к следующему пакету.
                    continue;
                }

                //  Блок перехвата всех исключений.
                try
                {
                    //  Создание потока.
                    using MemoryStream stream = new(udpDatagram.Datagram);

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

                    //  Разбор сообщений.
                    foreach (NmeaMessage? message in messages)
                    {
                        //  Разбор сообщения.
                        if (message is NmeaGgaMessage ggaMessage)
                        {
                            if (ggaMessage.Latitude.HasValue)
                                latitude[index] = ggaMessage.Latitude.Value;
                            if (ggaMessage.Longitude.HasValue)
                                longitude[index] = ggaMessage.Longitude.Value;
                        }

                        //  Разбор сообщения.
                        if (message is NmeaRmcMessage rmcMessage)
                        {
                            if (rmcMessage.Latitude.HasValue)
                                latitude[index] = rmcMessage.Latitude.Value;
                            if (rmcMessage.Longitude.HasValue)
                                longitude[index] = rmcMessage.Longitude.Value;
                            if (rmcMessage.Speed.HasValue)
                            {
                                speed[index] = rmcMessage.Speed.Value;
                                AverageSpeed += rmcMessage.Speed.Value;
                                ++speedCount;
                            }
                        }

                        //  Разбор сообщения.
                        if (message is NmeaVtgMessage vtgMessage)
                        {
                            if (vtgMessage.Speed.HasValue)
                            {
                                speed[index] = vtgMessage.Speed.Value;
                                AverageSpeed += vtgMessage.Speed.Value;
                                ++speedCount;
                            }
                        }
                    }
                }
                catch { }
            }
        }

        //  Нормализация значений.
        FillSmallZeroGaps(speed.Items, 5 * _Sampling);
        FillSmallZeroGaps(latitude.Items, 5 * _Sampling);
        FillSmallZeroGaps(longitude.Items, 5 * _Sampling);

        //  Проверка количества скоростей.
        if (speedCount > 0)
        {
            //  Корректировка средней скорости.
            AverageSpeed /= speedCount;
        }

        //  Ожидание завершённой задачи.
        await Task.CompletedTask.ConfigureAwait(false);

        //  Возврат каналов.
        return (speed, longitude, latitude);
    }

    /// <summary>
    /// Асинхронно выполняет построение данных геолокации.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение данных геолокации.
    /// </returns>
    private async Task<Channel[]> RS485Async(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Определение длины канала.
        int length = 60 * _Sampling;

        //  Создание данных каналов.
        int[] ids = [16, 11, 15, 17, 14, 19, 13, 12, 21];
        string[] names = ["UxBuf1", "UxBuf2", "UzBux1", "UzDV1", "UyDG2", "UzDV2", "UzBux2", "Alfa", "PTrC"];
        string[] units = ["mm/mm", "mm/mm", "mm/mm", "mm/mm", "mm/mm", "mm/mm", "mm/mm", "mm/mm", "MPa"];

        //  Создание каналов.
        Channel[] channels = new Channel[9];
        for (int i = 0; i < channels.Length; i++)
        {
            channels[i] = new(names[i], units[i], _Sampling, 0.5 * _Sampling, length);
        }

        //  Получение пакетов.
        IEnumerable<DataPackage> packages = files
            .Where(x => x.Format == FileFormat.RS485)
            .SelectMany(x => x.DataPackages);

        RSReader rsReader = new(packages);

        //  Перебор значений.
        for (int i = 0; i < ids.Length; i++)
        {
            int id = ids[i];
            Channel channel = channels[i];
            rsReader.Enum(id, delegate (DateTime receiptTime, double value)
            {
                //  Получение индекса.
                int index = (int)((receiptTime - time).TotalSeconds * _Sampling);

                //  Проверка индекса.
                if (index < 0 || index >= length)
                {
                    //  Переход к следующему пакету.
                    return;
                }

                //  Установка значения.
                channel[index] = value;
            });
        }

        //Console.WriteLine($"packages.Count() = {packages.Count()}");
        ////  Перебор пакетов.
        //foreach (DataPackage package in packages)
        //{
        //    //  Проверка типа.
        //    if (package is UdpDatagram udpDatagram)
        //    {
        //        //  Получение индекса.
        //        int index = (int)((udpDatagram.ReceiptTime - time).TotalSeconds * _Sampling);

        //        //  Проверка индекса.
        //        if (index < 0 || index >= length)
        //        {
        //            //  Переход к следующему пакету.
        //            continue;
        //        }

        //        //  Блок перехвата всех исключений.
        //        try
        //        {
        //            //  Создание потока.
        //            using MemoryStream stream = new(udpDatagram.Datagram);

        //            //  Создание средства чтения двоичных данных.
        //            using StreamReader reader = new(stream, Encoding.ASCII);

        //            //  Чтение текста.
        //            string text = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);

        //            //  Разбор значений.
        //            IEnumerable<string> lines = text.Split('\n')
        //                .Select(x => x.Trim())
        //                .Where(x => x.Length > 0);

        //            StringBuilder builder = new();

        //            foreach (string line in lines)
        //            {
        //                var parts = line.Split(']');
        //                if (parts.Length != 3)
        //                {
        //                    continue;
        //                }
        //                if (!parts[0].StartsWith('['))
        //                {
        //                    continue;
        //                }
        //                if (!int.TryParse(parts[0].AsSpan(1), out int id))
        //                {
        //                    continue;
        //                }
        //                int[] indexes = [.. ids.Index().Where(x => x.Item == id).Select(x => x.Index)];
        //                if (indexes.Length != 1)
        //                {
        //                    continue;
        //                }
        //                int channelIndex = indexes[0];
        //                if (!parts[1].Trim().StartsWith('['))
        //                {
        //                    continue;
        //                }
        //                if (!int.TryParse(parts[1].Trim().AsSpan(1), out int sensorType))
        //                {
        //                    continue;
        //                }
        //                if (!parts[2].StartsWith(':'))
        //                {
        //                    continue;
        //                }
        //                parts = parts[2][1..].Trim().Split(' ');
        //                if (!double.TryParse(parts[0], CultureInfo.InvariantCulture, out double value))
        //                {
        //                    continue;
        //                }
        //                if (sensorType == 257)
        //                {
        //                    value /= 50000.0;
        //                }
        //                channels[channelIndex][index] = value;

        //                builder.AppendLine($"{channelIndex}, {sensorType}, {value} : \"{line}\"");
        //            }
        //        }
        //        catch { }
        //    }
        //}

        //  Нормализация значений.
        foreach (Channel channel in channels)
        {
            FillSmallZeroGaps(channel.Items, 5 * _Sampling);
        }

        //  Ожидание завершённой задачи.
        await Task.CompletedTask.ConfigureAwait(false);

        //  Возврат каналов.
        return channels;
    }

    /// <summary>
    /// Заполняет пропущенные области.
    /// </summary>
    /// <param name="data">
    /// Массив данных.
    /// </param>
    /// <param name="maxGap">
    /// Максимальная длина области для заполнения.
    /// </param>
    private static void FillSmallZeroGaps(double[] data, int maxGap)
    {
        if (data == null || data.Length == 0)
            return;

        int i = 0;
        while (i < data.Length)
        {
            if (data[i] != 0)
            {
                i++;
                continue;
            }

            // начало нулевой области
            int start = i;
            while (i < data.Length && data[i] == 0)
                i++;

            int end = i - 1;
            int length = end - start + 1;

            if (length > maxGap)
                continue; // пропускаем — слишком длинная область

            double leftValue = (start > 0) ? data[start - 1] : double.NaN;
            double rightValue = (i < data.Length) ? data[i] : double.NaN;

            if (double.IsNaN(leftValue) && double.IsNaN(rightValue))
                continue; // массив весь из нулей, ничего не делаем

            if (double.IsNaN(leftValue))
            {
                // в начале массива — заполняем ближайшим справа
                for (int j = start; j <= end; j++)
                    data[j] = rightValue;
            }
            else if (double.IsNaN(rightValue))
            {
                // в конце массива — заполняем ближайшим слева
                for (int j = start; j <= end; j++)
                    data[j] = leftValue;
            }
            else
            {
                // линейная интерполяция между leftValue и rightValue
                double step = (rightValue - leftValue) / (length + 1);
                for (int j = 1; j <= length; j++)
                    data[start + j - 1] = leftValue + step * j;
            }
        }
    }
}
