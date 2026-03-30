using Simargl.Concurrent;
using Simargl.Frames;
using Simargl.Recording.Geolocation;
using System.IO;

namespace Simargl.Trials.Aurora.Aurora01.Restorer.Core;

/// <summary>
/// Предоставляет методы построения каналов.
/// </summary>
public static class Builder
{
    /// <summary>
    /// Асинхронно выполняет построение.
    /// </summary>
    /// <param name="header">
    /// Заголовок действия.
    /// </param>
    /// <param name="action">
    /// Действие с кадром.
    /// </param>
    /// <param name="sourceLabel">
    /// Метка исходного каталога.
    /// </param>
    /// <param name="targetLabel">
    /// Метка целевого каталога.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение.
    /// </returns>
    public static async Task BuildAsync(string header, AsyncAction<Frame> action, string sourceLabel, string targetLabel, CancellationToken cancellationToken)
    {
        //  Вывод в консоль.
        Console.WriteLine($"Дейстсвие {header}.");
        Console.WriteLine("  Построение начато.");

        //  Формирование путей.
        string sourcePath = Path.Combine(
            RestorerTunnings.RestoredPath,
            RestorerTunnings.Date.ToString("yyyy-MM-dd"),
            sourceLabel);
        string targetPath = Path.Combine(
            RestorerTunnings.RestoredPath,
            RestorerTunnings.Date.ToString("yyyy-MM-dd"),
            targetLabel);

        //  Проверка целевого каталога.
        Directory.CreateDirectory(targetPath);

        //  Перебор кадров.
        foreach (FileInfo fileInfo in new DirectoryInfo(sourcePath).GetFiles("*", SearchOption.TopDirectoryOnly))
        {
            //  Открытие кадра.
            Frame frame = new(fileInfo.FullName);

            //  Выполнение действия.
            await action(frame, cancellationToken).ConfigureAwait(false);

            //  Сохранение кадра.
            frame.Save(Path.Combine(targetPath, fileInfo.Name), StorageFormat.TestLab);

            //  Вывод в консоль.
            Console.WriteLine($"    Кадр: {fileInfo.Name}.");
        }

        //  Вывод в консоль.
        Console.WriteLine("  Построение завершено.");
    }

    /// <summary>
    /// Асинхронно выполняет экспорт.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая экспорт.
    /// </returns>
    public static async Task ExportAsync(CancellationToken cancellationToken)
    {
        //  Вывод в консоль.
        Console.WriteLine($"Дейстсвие Экспорт.");
        Console.WriteLine("  Построение начато.");

        //  Формирование путей.
        string sourcePath = Path.Combine(
            RestorerTunnings.RestoredPath,
            RestorerTunnings.Date.ToString("yyyy-MM-dd"),
            RestorerTunnings.NormalizedLabel);
        string targetPath = Path.Combine(
            RestorerTunnings.RestoredPath,
            RestorerTunnings.Date.ToString("yyyy-MM-dd"),
            RestorerTunnings.ExportedLabel);

        //  Проверка целевого каталога.
        Directory.CreateDirectory(targetPath);

        //  Перебор кадров.
        foreach (FileInfo fileInfo in new DirectoryInfo(sourcePath).GetFiles("*", SearchOption.TopDirectoryOnly))
        {
            //  Открытие кадра.
            Frame frame = new(fileInfo.FullName);

            //  Создание построителя текста.
            StringBuilder output = new();

            //  Заполнение заголовка.
            output.Append("Index;Time");

            //  Перебор каналов.
            foreach (Channel channel in frame.Channels)
            {
                //  Вывод названия канала.
                output.Append(';');
                output.Append(channel.Name);
            }

            //  Переход на новую строку.
            output.AppendLine();

            //  Перебор значений.
            for (int i = 0; i < frame.Channels[0].Length; i++)
            {
                //  Заполнение заголовка.
                output.Append($"{i}");
                write(i / (double)RestorerTunnings.Sampling);

                //  Перебор каналов.
                foreach (Channel channel in frame.Channels)
                {
                    //  Вывод значения.
                    write(channel[i]);
                }

                //  Переход на новую строку.
                output.AppendLine();
            }

            //  Вывод в консоль.
            Console.WriteLine($"    Кадр: {fileInfo.Name}.");

            void write(double value)
            {
                output.Append(';');
                output.Append(value.ToString().Replace('.', ','));
            }

            //  Сохранение файла.
            await File.WriteAllTextAsync(
                Path.Combine(targetPath, fileInfo.Name + ".csv"),
                output.ToString(), cancellationToken).ConfigureAwait(false);
        }

        //  Вывод в консоль.
        Console.WriteLine("  Построение завершено.");
    }

    /// <summary>
    /// Асинхронно нормализует кадр.
    /// </summary>
    /// <param name="frame">
    /// Кадр, который необходимо нормализовать.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая нормализацию.
    /// </returns>
    public static async Task NormalizeAsync(Frame frame, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор ускорений.
        foreach (var x in RestorerTunnings.Accelerations)
        {
            //  Получение канала.
            Channel channel = frame.Channels[x.Name];

            //  Корректировка имени.
            channel.Name += RestorerTunnings.AccelerationPostfix;

            //  Определение масштаба.
            double scale = x.Scale;

            //  Получение массива значений.
            double[] items = channel.Items;

            //  Перебор значений.
            for (int i = 0; i < items.Length; i++)
            {
                //  Корректировка значения.
                items[i] = scale * items[i];// * 9.81;
            }
        }

        //  Получение каналов GPS.
        foreach (string name in new string[] { "V_GPS", "Lat_GPS", "Lon_GPS"})
        {
            //  Получение канала.
            Channel channel = frame.Channels[name];

            //  Копирование данных.
            double[] source = (double[])channel.Items.Clone();

            //  Изменение данных.
            Array.Resize(ref source, source.Length + 1);

            //  Установка последней точки.
            source[^1] = source[^2];

            //  Изменение параметров канала.
            channel.Length *= RestorerTunnings.Sampling;
            channel.Sampling = RestorerTunnings.Sampling;

            //  Перебор значений канала.
            for (int i = 0; i < source.Length - 1; i++)
            {
                //  Получение граничных точек.
                double begin = source[i];
                double end = source[i + 1];

                //  Определение начального индекса.
                int beginIndex = i * RestorerTunnings.Sampling;

                //  Определение коэффициентов.
                double k = (end - begin) / RestorerTunnings.Sampling;

                //  Перебор точек.
                for (int offset = 0; offset < RestorerTunnings.Sampling; offset++)
                {
                    //  Установка значения.
                    channel[beginIndex + offset] = k * offset + begin;
                }
            }
        }

        //  Получение процесса скорости.
        double[] speed = frame.Channels["V_GPS"].Items;

        //  Перебор точек.
        for (int i = 0; i < speed.Length; i++)
        {
            //  Масштабирование.
            speed[i] *= 1 / 3.6;
        }

        //  Получение каналов скоординатами.
        Channel latitude = frame.Channels["Lat_GPS"];
        Channel longitude = frame.Channels["Lon_GPS"];

        //  Создание вспомогательных каналов.
        Channel curvature = frame.Channels["V_GPS"].Clone();
        Channel normal = curvature.Clone();
        Channel tangential = curvature.Clone();

        //  Добавление вспомогательных каналов.
        frame.Channels.Insert(3, curvature);
        frame.Channels.Insert(4, normal);
        frame.Channels.Insert(5, tangential);

        //  Настройка вспомогательных каналов.
        curvature.Name = "Curv"; curvature.Unit = "1/m";
        normal.Name = "Norm"; normal.Unit = "m/c^2";
        tangential.Name = "Tang"; tangential.Unit = "m/c^2";

        //  Перебор точек.
        for (int i = 1; i < curvature.Length - 1; i++)
        {
            //  Получение координат.
            GpsPointInfo[] points =
            [
                new(latitude[i - 1], longitude[i - 1], 0),
                new(latitude[i], longitude[i], 0),
                new(latitude[i + 1], longitude[i + 1], 0),
            ];

            //  Получение изменения координат.
            double dX01 = points[0].X - points[1].X;
            double dY01 = points[0].Y - points[1].Y;
            double dZ01 = points[0].Z - points[1].Z;
            double dX12 = points[1].X - points[2].X;
            double dY12 = points[1].Y - points[2].Y;
            double dZ12 = points[1].Z - points[2].Z;
            double dX20 = points[2].X - points[0].X;
            double dY20 = points[2].Y - points[0].Y;
            double dZ20 = points[2].Z - points[0].Z;

            //  Получение сторон треугольника.
            double a = Math.Sqrt(dX01 * dX01 + dY01 * dY01 + dZ01 * dZ01);
            double b = Math.Sqrt(dX12 * dX12 + dY12 * dY12 + dZ12 * dZ12);
            double c = Math.Sqrt(dX20 * dX20 + dY20 * dY20 + dZ20 * dZ20);

            //  Получение полупериметра.
            double p = 0.5 * (a + b + c);

            //  Получение площади.
            double S = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            //  Определение произведения сторон.
            double m = a * b * c;

            //  Получение кривизны.
            curvature[i] = m > 0 ? 4 * S / m : 0;
        }

        //  Установка краевых значений.
        curvature[0] = curvature[1];
        curvature[^1] = curvature[^2];

        //  Перебор точек.
        for (int i = 1; i < curvature.Length; i++)
        {
            tangential[i] = (speed[i] - speed[i - 1]) / RestorerTunnings.Sampling;
        }


        //  Получение каналов ускорений на буксах.
        Channel[] xPoints = RestorerTunnings.Points
            .Select(x => frame.Channels[x.XName + RestorerTunnings.AccelerationPostfix])
            .ToArray();
        Channel[] yPoints = RestorerTunnings.Points
            .Select(x => frame.Channels[x.YName + RestorerTunnings.AccelerationPostfix])
            .ToArray();
        Channel[] zPoints = RestorerTunnings.Points
            .Select(x => frame.Channels[x.ZName + RestorerTunnings.AccelerationPostfix])
            .ToArray();

        //  Создание каналов ускорения центра.
        Channel xC = xPoints[0].Clone();
        Channel yC = yPoints[0].Clone();
        Channel zC = zPoints[0].Clone();
        xC.Name = "CX" + RestorerTunnings.AccelerationPostfix;
        yC.Name = "CY" + RestorerTunnings.AccelerationPostfix;
        zC.Name = "CZ" + RestorerTunnings.AccelerationPostfix;

        //  Перебор точек.
        for (int i = 0; i < xC.Length; i++)
        {
            //  Величина ускорения.
            double x = 0;
            double y = 0;
            double z = 0;

            //  Перебор точек.
            for (int j = 0; j < xPoints.Length; j++)
            {
                //  Корректировка величины ускорения.
                x += xPoints[j][i];
                y += yPoints[j][i];
                z += zPoints[j][i];
            }

            //  Корректировка величины ускорения.
            x /= xPoints.Length;
            y /= yPoints.Length;
            z /= zPoints.Length;

            //  Установка значений.
            xC[i] = x;
            yC[i] = y;
            zC[i] = z;
        }

        //  Добавление каналов.
        frame.Channels.Insert(6, xC);
        frame.Channels.Insert(7, yC);
        frame.Channels.Insert(8, zC);

        //  A -> 1 - 01 BX101
        //  B -> 2 - 01 BX201
        //  C -> 2 - 1  BX21
        //  D -> 1 - 1  BX11
    }

    /// <summary>
    /// Асинхронно выполняет интегрирование.
    /// </summary>
    /// <param name="frame">
    /// Кадр, в котором необходимо выполнить интегрирование.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая интегрирование.
    /// </returns>
    public static async Task IntegrateAsync(Frame frame, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Индекс канала.
        int vIndex = 6;

        //  Перебор всех ускорений.
        foreach (string name in new string[] { "CX", "CY", "CZ" })
        {
            //  Получение канала ускорения.
            Channel a = frame.Channels[name + RestorerTunnings.AccelerationPostfix];

            //  Создание канала скорости.
            Channel v = a.Clone();
            v.Name = name + RestorerTunnings.SpeedPostfix;

            //  Добавление канала.
            frame.Channels.Insert(vIndex++, v);

            //  Установка начального значения.
            v[0] = 0;

            //  Интегрирование.
            for (int i = 1; i < v.Length; i++)
            {
                v[i] = v[i - 1] + a[i - 1] / v.Sampling;
            }
        }
    }



    ///// <summary>
    ///// Возвращает среднее значение канала при скорости движения равной нулю.
    ///// </summary>
    ///// <param name="channel">
    ///// Целевой канал.
    ///// </param>
    ///// <param name="speed">
    ///// Канал скорости.
    ///// </param>
    ///// <returns>
    ///// Среднее значение канала при скорости движения равной нулю.
    ///// </returns>
    //private static double GetZeroAverage(Channel channel, Channel speed)
    //{
    //    //  Сумма и количество значений.
    //    double sum = 0;
    //    int count = 0;

    //    //  Получение массивов значений.
    //    double[] channelItems = channel.Items;
    //    double[] speedItems = speed.Items;

    //    //  Получение частоты дискретизации.
    //    int sampling = (int)channel.Sampling;

    //    //  Перебор значений.
    //    for (int i = 0; i < speedItems.Length; i++)
    //    {
    //        //  Проверка скорости.
    //        if (speedItems[i] == 0)
    //        {
    //            //  Определение индексов.
    //            int begin = i * sampling;
    //            int end = begin + sampling;

    //            //  Перебор целевых значений.
    //            for (int j = begin; j < end; j++)
    //            {
    //                //  Корректировка суммы.
    //                sum += channelItems[j];
    //            }

    //            //  Корректировка количества.
    //            count += sampling;
    //        }
    //    }

    //    //  Возврат среднего значения.
    //    return sum / count;
    //}
}
