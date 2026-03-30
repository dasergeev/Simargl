using Simargl.Recording.Geolocation;
using Simargl.Trials.Aurora.Aurora01.Storage.Entities;
using System.Collections.Generic;

namespace Simargl.Trials.Aurora.Aurora01.TrackBuilder;

partial class Program
{
    /// <summary>
    /// Асинхронно выполняет связывание данных.
    /// </summary>
    /// <param name="nmeas">
    /// Массив всех данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задча, выполняющая связывание данных.
    /// </returns>
    private static async Task RelationAsync(NmeaData[] nmeas, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);
        _ = nmeas;

        ////  Проверка размера данных.
        //if (nmeas.Length < 3) return;

        ////  Перебор всех данных.
        //foreach (var item in nmeas)
        //{
        //    //  Сброс индекса фрагмента.
        //    item.FragmentIndex = -1;
        //}

        ////  Предыдущие и текущие данные.
        //NmeaData nmeaPrevious = nmeas[0];
        //NmeaData nmeaCurrent = nmeas[1];
        //GpsPointInfo pointPrevious = new(nmeaPrevious.Latitude, nmeaPrevious.Longitude, 0);
        //GpsPointInfo pointCurrent = new(nmeaCurrent.Latitude, nmeaCurrent.Longitude, 0);
        //double distancePrevious = getDistance(pointPrevious, pointCurrent);

        //int stepInfo = nmeas.Length / 10000;

        ////  Текущий фрагмент.
        //List<NmeaData> fragment = [];

        ////  Индекс фрагмента.
        //int fragmentIndex = 0;

        ////  Перебор данных.
        //for (int i = 2; i < nmeas.Length; i++)
        //{
        //    if ((i % stepInfo) == 0)
        //    {
        //        Console.WriteLine($"Relation: {i * 100.0 / nmeas.Length:0.00}%");
        //    }

        //    //  Получение следующих данных.
        //    NmeaData nmeaNext = nmeas[i];
        //    GpsPointInfo pointNext = new(nmeaNext.Latitude, nmeaNext.Longitude, 0);
        //    double distanceNext = getDistance(pointCurrent, pointNext);

        //    //  Проверка длительности.
        //    if (nmeaCurrent.Timestamp == nmeaPrevious.Timestamp + 1 &&
        //        nmeaNext.Timestamp == nmeaCurrent.Timestamp + 1)
        //    {
        //        //  Общее расстояние.
        //        double distance = distancePrevious + distanceNext;

        //        //  Определение скорости.
        //        nmeaCurrent.GpsSpeed = 0.5 * distance * 3.6;

        //        //  Настройка поколения.
        //        nmeaCurrent.Generation = NmeaGeneration.Related;

        //        //  Добавление данных во фрагмент.
        //        fragment.Add(nmeaCurrent);
        //    }
        //    else
        //    {
        //        //  Настройка поколения.
        //        nmeaCurrent.Generation = NmeaGeneration.Registered;

        //        //  Настройка индекса фрагмента.
        //        nmeaCurrent.FragmentIndex = -1;

        //        //  Проверка фрагмента.
        //        if (fragment.Count > 0)
        //        {
        //            //  Перебор данных фрагмента.
        //            foreach (var item in fragment)
        //            {
        //                //  Установка индекса фрагмента.
        //                item.FragmentIndex = fragmentIndex;
        //            }

        //            //  Увеличение индекса фрагмента.
        //            ++fragmentIndex;

        //            //  Очистка текущего фрагмента.
        //            fragment.Clear();
        //        }
        //    }

        //    //  Смена данных.
        //    nmeaPrevious = nmeaCurrent;
        //    nmeaCurrent = nmeaNext;
        //    //pointPrevious = pointCurrent;
        //    pointCurrent = pointNext;
        //    distancePrevious = distanceNext;
        //}

        ////  Возвращает расстояние в метрах.
        //static double getDistance(GpsPointInfo first, GpsPointInfo second)
        //{
        //    double dX = first.X - second.X;
        //    double dY = first.Y - second.Y;
        //    double dZ = first.Z - second.Z;
        //    return Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
        //}
    }
}
