using Simargl.Trials.Aurora.Aurora01.Storage.Entities;
using System.Collections.Generic;

namespace Simargl.Trials.Aurora.Aurora01.Analyzer;

/// <summary>
/// Представляет фрагмент данных.
/// </summary>
public sealed class NmeaFragment
{
    /// <summary>
    /// Поле для хранения первой метки времени.
    /// </summary>
    private readonly long _FirstTimestamp;

    /// <summary>
    /// Поле для хранения последней метки времени.
    /// </summary>
    private readonly long _LastTimestamp;

    /// <summary>
    /// Поле для хранения данные.
    /// </summary>
    private readonly NmeaData[] _Data;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    private NmeaFragment(long firstTimestamp, long lastTimestamp, NmeaData[] nmeas)
    {
        //  Установка полей.
        _FirstTimestamp = firstTimestamp;
        _LastTimestamp = lastTimestamp;
        _Data = nmeas;

        //  Установка свойств.
        FirstTime = NmeaData.ToTime(_FirstTimestamp);
        LastTime = NmeaData.ToTime(_LastTimestamp);

        //  Для анализатора.
        _ = _Data;
    }

    /// <summary>
    /// Возвращает первое время.
    /// </summary>
    public DateTime FirstTime { get; }

    /// <summary>
    /// Возвращает последнее время.
    /// </summary>
    public DateTime LastTime { get; }

    /// <summary>
    /// Возвращает данные с указанным временем.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <returns>
    /// Данные.
    /// </returns>
    public NmeaData this[DateTime time]
    {
        get
        {
            //  Получение метки времени.
            long timestamp = NmeaData.FromTime(time);

            //  Получение индекса.
            int index = (int)(timestamp - _FirstTimestamp);

            //  Возврат данных.
            return _Data[index];
        }
    }

    /// <summary>
    /// Возвращает диапазон значений.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <param name="duration">
    /// Длительность.
    /// </param>
    /// <returns>
    /// Диапазон значений.
    /// </returns>
    public NmeaData[] GetRange(DateTime time, TimeSpan duration)
    {
        //  Создание списка.
        List<NmeaData> nmeas = [];

        //  Определение конечного значения.
        DateTime endTime = time + duration;

        //  Формирование значений.
        for (; time < endTime; time = time.AddSeconds(1))
        {
            //  Добавление значения.
            nmeas.Add(this[time]);
        }

        //  Возврат массива значений.
        return [.. nmeas];
    }

    /// <summary>
    /// Асинхронно загружает фрагмент.
    /// </summary>
    /// <param name="data">
    /// Данные фрагмента.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая фрагмент.
    /// </returns>
    public static async Task<NmeaFragment> LoadAsync(List<NmeaData> data, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Определение меток времени.
        long firstTimestamp = data[0].Timestamp;
        long lastTimestamp = data[^1].Timestamp;

        //  Определение длительности.
        int duration = 1 + (int)(lastTimestamp - firstTimestamp);

        //  Создание массива данных.
        NmeaData[] nmeas = new NmeaData[duration];

        //  Перебор исходных данных.
        for (int dataIndex = 1; dataIndex < data.Count; dataIndex++)
        {
            //  Получение данных.
            NmeaData first = data[dataIndex - 1];
            NmeaData second = data[dataIndex];

            //  Определение коэффициентов.
            double k = 1.0 / (-first.Timestamp + second.Timestamp);
            double b = first.Timestamp - firstTimestamp;
            double kSpeed = k * (second.Speed - first.Speed);
            double bSpeed = first.Speed - b * kSpeed;
            double kLatitude = k * (second.Latitude - first.Latitude);
            double bLatitude = first.Latitude - b * kLatitude;
            double kLongitude = k * (second.Longitude - first.Longitude);
            double bLongitude = first.Longitude - b * kLongitude;

            //  Перебор меток времени.
            for (long timestamp = first.Timestamp; timestamp <= second.Timestamp; timestamp++)
            {
                //  Получение индекса.
                int index = (int)(timestamp - firstTimestamp);

                //  Установка данных.
                nmeas[index] = new()
                {
                    Timestamp = timestamp,
                    Speed = kSpeed * index + bSpeed,
                    Latitude = kLatitude * index + bLatitude,
                    Longitude = kLongitude * index + bLongitude,
                };
            }
        }

        //  Создание фрагмента.
        return new(firstTimestamp, lastTimestamp, nmeas);
    }
}
