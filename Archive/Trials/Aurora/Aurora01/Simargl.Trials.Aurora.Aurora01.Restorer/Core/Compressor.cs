using Simargl.Analysis.Transforms;
using Simargl.Frames;
using System.IO;
using System.Linq;

namespace Simargl.Trials.Aurora.Aurora01.Restorer.Core;

/// <summary>
/// Предоставляет методы сжатия кадров.
/// </summary>
public static class Compressor
{
    /// <summary>
    /// Асинхронно выполняет сжатие кадров.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сжатие кадров.
    /// </returns>
    public static async Task CompressionAsync(CancellationToken cancellationToken)
    {
        //  Поиск последовательностей файлов.
        (int Begin, int End)[] fragments = await Explorer.SearchAsync(cancellationToken).ConfigureAwait(false);

        //  Индекс кадра.
        int index = 0;

        //  Перебор последовательностей.
        foreach ((int begin, int end) in fragments)
        {
            //  Сжатие кадра.
            await CompressionAsync(index++, begin, end, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет сжатие кадра.
    /// </summary>
    /// <param name="index">
    /// Индекс кадра.
    /// </param>
    /// <param name="begin">
    /// Начальный индекс фрагмента.
    /// </param>
    /// <param name="end">
    /// Конечный индекс фрагмента.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сжатие кадра.
    /// </returns>
    private static async Task CompressionAsync(int index, int begin, int end, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение кадров.
        Frame[] frames = (await Explorer.GetFilesAsync(cancellationToken).ConfigureAwait(false))
            .Where(x => begin <= x.Index && x.Index < end)
            .OrderBy(x => x.Index)
            .Select(x => new Frame(x.Info.FullName))
            .ToArray();

        //  Проверка количества загруженных кадров.
        if (frames.Length != end - begin)
            throw new InvalidDataException("Не найдены все кадры.");

        //  Настройка общего кадра.
        Frame common = frames[0];

        //  Создание фильтра.
        ButterworthFilter filter = new(RestorerTunnings.FilterCutoff, 4);

        //  Перебор каналов.
        for (int channelIndex = 0; channelIndex < common.Channels.Count; channelIndex++)
        {
            //  Получение целевого канала.
            Channel target = common.Channels[channelIndex];

            //  Определение длины целевого канала.
            int targetLength = target.Length * frames.Length;

            //  Изменение длины канала.
            target.Length = targetLength;

            //  Перебор кадров.
            for (int frameIndex = 1; frameIndex < frames.Length; frameIndex++)
            {
                //  Получение исходного канала.
                Channel source = frames[frameIndex].Channels[channelIndex];

                //  Копированеи данных.
                Array.Copy(source.Items, 0, target.Items, frameIndex * source.Length, source.Length);
            }

            //  Проверка частоты дискретизации.
            if (target.Sampling > RestorerTunnings.Sampling)
            {
                //  Увеличение размера канала для зеркального отображения.
                target.Length = 2 * targetLength;

                //  Получение массива значений.
                double[] items = target.Items;

                //  Создание зеркальной копии канала.
                for (int i = 0; i < targetLength; i++)
                {
                    //  Установка зеркального значения.
                    items[2 * targetLength - 1 - i] = items[i];
                }

                //  Фильтрация канала.
                filter.Invoke(target.Signal);

                //  Определение длины канала.
                int length = RestorerTunnings.Sampling * 60 * frames.Length;

                //  Перебор значений канала.
                for (int i = 0; i < length; i++)
                {
                    //  Определение исходного индекса.
                    int sourceIndex = (int)Math.Floor(i * target.Sampling / RestorerTunnings.Sampling);

                    //  Установка значения.
                    items[i] = items[sourceIndex];
                }

                //  Изменение параметров канала.
                target.Sampling = RestorerTunnings.Sampling;
                target.Cutoff = RestorerTunnings.FilterCutoff;
                target.Length = length;
            }
        }

        //  Формирование пути к кадру.
        string path = Path.Combine(
            RestorerTunnings.RestoredPath,
            RestorerTunnings.Date.ToString("yyyy-MM-dd"),
            RestorerTunnings.CompressedLabel);

        //  Проверка каталога.
        Directory.CreateDirectory(path);

        //  Настройка имени файла.
        path = Path.Combine(path, $"Vp000_0.{index:0000}");

        //  Сохранение кадра.
        common.Save(path, StorageFormat.TestLab);

        //  Вывод в консоль.
        Console.WriteLine($"Выполнена сборка кадра: {index:0000}");
    }
}
