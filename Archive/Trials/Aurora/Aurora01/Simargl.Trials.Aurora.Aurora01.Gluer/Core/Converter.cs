using Simargl.Frames;
using System.IO;
using System.Linq;
using System.Text;

namespace Simargl.Trials.Aurora.Aurora01.Gluer.Core;

/// <summary>
/// Предоставляет методы конвертации.
/// </summary>
public static class Converter
{
    /// <summary>
    /// Асинхронно выполняет конвертацию.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая конвертацию.
    /// </returns>
    public static async Task ConvertAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Предыдущий индекс.
        int previousIndex = int.MinValue;

        //  Средства записи.
        StreamWriter tensometry = null!;
        StreamWriter accelerations = null!;

        //  Индексы и времена.
        int tIndex = 0;
        int aIndex = 0;

        //  Перебор кадров.
        foreach (var x in new DirectoryInfo(Path.Combine(
                            GluerTunnings.RecordsPath,
                            $"{GluerTunnings.Date:yyyy-MM-dd}"))
            .GetFiles("*", SearchOption.TopDirectoryOnly)
            .Select(x => new
            {
                Index = int.Parse(x.Extension[1..]),
                Speed = int.Parse(x.Name.Substring(2, 3)),
                Path = x.FullName,
            })
            .OrderBy(x => x.Index))
        {
            //  Вывод в консоль.
            Console.WriteLine($"  {x.Path}");

            //  Загрузка кадра.
            Frame frame = new(x.Path);

            //  Проверка необходимости начать новый файл.
            if (previousIndex + 1 != x.Index || aIndex >= 10 * 60 * 2000)
            {
                //  Открытие нового файла.
                await openAsync(x.Index, frame).ConfigureAwait(false);
            }

            //  Корректировка предыдущего индекса.
            previousIndex = x.Index;

            //  Получение каналов геолокации.
            Channel latitude = frame.Channels["Lat_GPS"];
            Channel longitude = frame.Channels["Lon_GPS"];
            Channel speed = frame.Channels["V_GPS"];

            //  Получение ускорений.
            var a = frame.Channels.Where(x => x.Sampling == 2000);

            //  Запись ускорений.
            for (int i = 0; i < 60 * 2000; i++)
            {
                await accelerations.WriteLineAsync().ConfigureAwait(false);

                await write(accelerations, aIndex.ToString()).ConfigureAwait(false);
                await number(accelerations, aIndex / 2000.0).ConfigureAwait(false);

                int gpsIndex = i / 2000;
                await number(accelerations, latitude[gpsIndex]).ConfigureAwait(false);
                await number(accelerations, longitude[gpsIndex]).ConfigureAwait(false);
                await number(accelerations, speed[gpsIndex]).ConfigureAwait(false);

                foreach (Channel channel in a)
                {
                    await number(accelerations, channel[i]).ConfigureAwait(false);
                }

                ++aIndex;
            }

            //  Получение тензометрии.
            var t = frame.Channels.Where(x => x.Sampling == 1200);

            //  Запись ускорений.
            for (int i = 0; i < 60 * 1200; i++)
            {
                await tensometry.WriteLineAsync().ConfigureAwait(false);

                await write(tensometry, tIndex.ToString()).ConfigureAwait(false);
                await number(tensometry, tIndex / 1200.0).ConfigureAwait(false);

                int gpsIndex = i / 1200;
                await number(tensometry, latitude[gpsIndex]).ConfigureAwait(false);
                await number(tensometry, longitude[gpsIndex]).ConfigureAwait(false);
                await number(tensometry, speed[gpsIndex]).ConfigureAwait(false);

                foreach (Channel channel in t)
                {
                    await number(tensometry, channel[i]).ConfigureAwait(false);
                }

                ++tIndex;
            }
        }

        //  Закрытие файлов.
        await closeAsync().ConfigureAwait(false);

        async Task number(StreamWriter writer, double value)
        {
            string text = ";" + value.ToString("G8").Replace(',', '.');
            await write(writer, text).ConfigureAwait(false);
        }

        async Task write(StreamWriter writer, string text)
        {
            await writer.WriteAsync(text.AsMemory(), cancellationToken).ConfigureAwait(false);
        }

        async Task openAsync(int index, Frame frame)
        {
            //  Определение времени.
            DateTime time = GluerTunnings.Date.ToDateTime(default).AddMinutes(index);

            //  Закрытие файлов.
            await closeAsync().ConfigureAwait(false);

            tIndex = 0;
            aIndex = 0;

            string path = Path.Combine(
                GluerTunnings.CsvPath,
                $"{GluerTunnings.Date:yyyy-MM-dd}",
                "Tensometry",
                $"{time:yyyy-MM-dd-HH-mm}.csv");
            new FileInfo(path).Directory!.Create();

            tensometry = new(path, Encoding.UTF8, new()
            {
                Access = FileAccess.Write,
                Mode = FileMode.Create,
            });

            path = Path.Combine(
                GluerTunnings.CsvPath,
                $"{GluerTunnings.Date:yyyy-MM-dd}",
                "Accelerations",
                $"{time:yyyy-MM-dd-HH-mm}.csv");
            new FileInfo(path).Directory!.Create();

            accelerations = new(path, Encoding.UTF8, new()
            {
                Access = FileAccess.Write,
                Mode = FileMode.Create,
            });

            await write(tensometry,
                "Index;Time;Latitude;Longitude;Speed"
                ).ConfigureAwait(false);
            await write(accelerations,
                "Index;Time;Latitude;Longitude;Speed"
                ).ConfigureAwait(false);

            foreach (Channel channel in frame.Channels)
            {
                if (channel.Sampling == 2000)
                {
                    await write(accelerations,
                        $";{channel.Name}"
                        ).ConfigureAwait(false);
                }
                else if (channel.Sampling == 1200)
                {
                    await write(tensometry,
                        $";{channel.Name}"
                        ).ConfigureAwait(false);
                }
            }

            await tensometry.WriteLineAsync().ConfigureAwait(false);
            await accelerations.WriteLineAsync().ConfigureAwait(false);

            await write(tensometry,
                ";s;°;°;kmph"
                ).ConfigureAwait(false);
            await write(accelerations,
                ";s;°;°;kmph"
                ).ConfigureAwait(false);

            foreach (Channel channel in frame.Channels)
            {
                if (channel.Sampling == 2000)
                {
                    await write(accelerations,
                        $";{channel.Unit}"
                        ).ConfigureAwait(false);
                }
                else if (channel.Sampling == 1200)
                {
                    await write(tensometry,
                        $";{channel.Unit}"
                        ).ConfigureAwait(false);
                }
            }
        }

        async Task closeAsync()
        {
            if (tensometry is not null)
            {
                await tensometry.FlushAsync(cancellationToken).ConfigureAwait(false);
                await tensometry.DisposeAsync().ConfigureAwait(false);
                tensometry = null!;
            }
            if (accelerations is not null)
            {
                await accelerations.FlushAsync(cancellationToken).ConfigureAwait(false);
                await accelerations.DisposeAsync().ConfigureAwait(false);
                accelerations = null!;
            }
        }
    }
}
