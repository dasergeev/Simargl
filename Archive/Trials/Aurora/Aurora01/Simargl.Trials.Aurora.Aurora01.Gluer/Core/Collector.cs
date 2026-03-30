using Simargl.Frames;
using Simargl.Frames.OldStyle;
using System.Linq;

namespace Simargl.Trials.Aurora.Aurora01.Gluer.Core;

/// <summary>
/// представляет сборщик кадров.
/// </summary>
/// <param name="geolocation">
/// Данные геолокации.
/// </param>
/// <param name="fragmentMap">
/// Карта фрагментов.
/// </param>
public sealed class Collector(Geolocation geolocation, FragmentMap fragmentMap)
{
    /// <summary>
    /// Асинхронно собирает кадр.
    /// </summary>
    /// <param name="minute">
    /// Индекс кадра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, собирающая кадр.
    /// </returns>
    public async Task<Frame?> CollectAsync(int minute, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание каналов геолокации.
        Channel speed = new("V_GPS", "kmph", 1, 1, 60);
        Channel latitude = new("Lat_GPS", "°", 1, 1, 60);
        Channel longitude = new("Lon_GPS", "°", 1, 1, 60);

        //  Проверка данных геолокации.
        for (int i = minute * 60; i < (minute + 1) * 60; i++)
        {
            //  Проверка данных.
            if (geolocation.Latitude[i] == 0) return null;

            //  Индекс значения в канале.
            int index = i - minute * 60;

            //  Установка значений канала.
            speed[index] = geolocation.Speed[i];
            latitude[index] = geolocation.Latitude[i];
            longitude[index] = geolocation.Longitude[i];
        }

        //  Определение времени начала и окончания кадра.
        DateTime beginTime = GluerTunnings.Date.ToDateTime(default).AddMinutes(minute);
        DateTime endTime = beginTime.AddMinutes(1);

        //  Фрагменты исходных данных.
        Fragment? rawFrame = null;
        Fragment?[] rawAdxl = new Fragment?[fragmentMap.Adxl.Length];

        //  Перебор сырых кадров.
        foreach (Fragment fragment in fragmentMap.Frame)
        {
            //  Проверка вхождения во фрагмент.
            if (fragment.BeginTime <= beginTime && endTime <= fragment.EndTime)
            {
                //  Установка фрагмента.
                rawFrame = fragment;

                //  Завершение поиска.
                break;
            }
        }

        //  Проверка фрагмента.
        if (rawFrame is null) return null;

        //  Перебор датчиков Adxl.
        for (int i = 0; i < fragmentMap.Adxl.Length; i++)
        {
            //  Перебор фрагментов.
            foreach (Fragment fragment in fragmentMap.Adxl[i])
            {
                //  Проверка вхождения во фрагмент.
                if (fragment.BeginTime <= beginTime && endTime <= fragment.EndTime)
                {
                    //  Установка фрагмента.
                    rawAdxl[i] = fragment;

                    //  Завершение поиска.
                    break;
                }
            }

            //  Проверка фрагмента.
            if (rawAdxl[i] is null) return null;
        }

        //  Получение тензоментрических каналов.
        Channel[]? tenso = await rawFrame.LoadFrameAsync(minute, cancellationToken).ConfigureAwait(false);

        //  Проверка тензометрических каналов.
        if (tenso is null) return null;

        //  Массив каналов ускорений.
        Channel[][] accel = new Channel[GluerTunnings.AdxlAddresses.Length][];

        //  Перебор датчиков ускорений.
        for (int i = 0; i < GluerTunnings.AdxlAddresses.Length; i++)
        {
            //  Получение фрагмента.
            Fragment? fragment = rawAdxl[i];

            //  Проверка фрагмента.
            if (fragment is null) return null;

            //  Получение каналов.
            accel[i] = (await fragment.LoadAdxlAsync(minute, cancellationToken).ConfigureAwait(false))!;

            //  Проверка каналов.
            if (accel[i] is null) return null;
        }

        //  Создание кадра.
        Frame frame = new();

        //  Добавление каналов геолокации.
        frame.Channels.Add(speed);
        frame.Channels.Add(latitude);
        frame.Channels.Add(longitude);

        //  Добавление каналов ускорений.
        foreach (var order in GluerTunnings.AdxlData
            .Select(x =>
            {
                int index = Array.IndexOf(GluerTunnings.AdxlAddresses, x.Address0);
                if (index >= 0)
                {
                    return new
                    {
                        x.Name,
                        AddressIndex = index,
                        AdxlIndex = x.AdxlIndex0,
                        Scale = x.Scale0,
                        ChannelIndex = x.Index,
                    };
                }
                index = Array.IndexOf(GluerTunnings.AdxlAddresses, x.Address1);
                return new
                {
                    x.Name,
                    AddressIndex = index,
                    AdxlIndex = x.AdxlIndex1,
                    Scale = x.Scale1,
                    ChannelIndex = x.Index,
                };
            })
            .Select(x => new
            {
                x.Name,
                x.Scale,
                x.ChannelIndex,
                Channel = accel[x.AddressIndex][x.AdxlIndex],
            })
            .OrderBy(x => x.ChannelIndex))
        {
            //  Установка имени канала.
            order.Channel.Name = order.Name;

            //  Проверка масштаба.
            if (order.Scale != 1)
            {
                //  Масштабирование канала.
                order.Channel.Scale(order.Scale);
            }

            //  Добавление канала.
            frame.Channels.Add(order.Channel);
        }

        //  Добавление тензометрических каналов.
        foreach (var order in Enumerable.Range(0, GluerTunnings.RawChannels.Length)
            .Select(x => new
            {
                TargetIndex = GluerTunnings.RawChannels[x].Index,
                SourceIndex = x,
                GluerTunnings.RawChannels[x].Scale,
                Channel = tenso[x],
            })
            .OrderBy(x => x.TargetIndex))
        {
            //  Масштабирование канала.
            order.Channel.Scale(order.Scale);

            //  Добавление канала.
            frame.Channels.Add(order.Channel);
        }

        //  Возврат кадра.
        return frame;
    }
}
