using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Apeiron.Oriole.Server.Workers.Analysis;

/// <summary>
/// Представляет фоновый процесс, выполняющий спектральный анализ.
/// </summary>
public class SpectrumAnalysis :
    Worker<SpectrumAnalysis>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public SpectrumAnalysis(ILogger<SpectrumAnalysis> logger) :
        base(logger)
    {

    }

    /// <summary>
    /// Асинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Выполнение работы с базой данных.
            await WorkDatabaseAsync(cancellationToken);

            //  Ожидание перед следующим поиском.
            await Task.Delay(60000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с базой данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task WorkDatabaseAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос информации о кадрах.
        Tuple<int, long>[] frames = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.Frames
                .AsNoTracking()
                .Where(frame => !frame.IsSpectrum)
                .Select(frame => new Tuple<int, long>(frame.RegistrarId, frame.Timestamp))
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Асинхронная работа с кадрами.
        await Parallel.ForEachAsync(
            frames,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            WorkInFrameAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с кадром.
    /// </summary>
    /// <param name="frameKey">
    /// Ключ кадра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkInFrameAsync(Tuple<int, long> frameKey, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение идентификатора регистратора.
        int registrarId = frameKey.Item1;

        //  Получение метки времени получения данных.
        long timestamp = frameKey.Item2;

        //  Начало транзакции.
        await OrioleDatabaseManager.TryTransactionAsync(async (database, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Блок перехвата исключений для записи ошибки в журнал.
            try
            {
                //  Получение кадра.
                Frame? dbFrame = await database.Frames
                    .Where(frame => frame.RegistrarId == registrarId &&
                        frame.Timestamp == timestamp &&
                        !frame.IsSpectrum)
                    .Include(frame => frame.Registrar)
                    .ThenInclude(registrar => registrar.Channels)
                    .Include(frame => frame.Registrar)
                    .ThenInclude(registrar => registrar.RecordDirectories)
                    .Include(frame => frame.Spectrums)
                    .FirstAsync(cancellationToken)
                    .ConfigureAwait(false);

                //  Проверка полученного кадра.
                if (dbFrame is null)
                {
                    //  Завершение анализа.
                    return;
                }

                //  Удаление информации о текущих спектрах.
                dbFrame.Spectrums.Clear();

                //  Установка флага обработки.
                dbFrame.IsSpectrum = true;

                //  Получение пути к файлу.
                string path = dbFrame.GetPath();

                //  Открытие кадра.
                Frames.Frame frame = new(path);

                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Перебор каналов в кадре.
                foreach (Channel dbChannel in dbFrame.Registrar.Channels)
                {
                    //  Получение канала.
                    Frames.Channel channel = frame.Channels[dbChannel.Name];

                    //  Проверка пустых значений.
                    if (channel.Where(value => value == 0).Count() >= 10)
                    {
                        //  Переход к следующему каналу.
                        continue;
                    }

                    //  Определение шага по длине канала.
                    int step = (int)channel.Sampling;

                    //  Постоянная, определяющая количество амплитуд в спектре.
                    const int amplitudeCount = 251;

                    //  Создание массива амплитуд.
                    Complex[] amplitudes = new Complex[amplitudeCount];

                    //  Определение количества фрагментов.
                    int count = channel.Length / step;

                    //  Перебор фрагментов.
                    for (int fragmentIndex = 0; fragmentIndex < count; fragmentIndex++)
                    {
                        //  Построение спектра подвектора.
                        Apeiron.Analysis.Spectrum fragmentSpectrum = new(new Apeiron.Analysis.Signal(1, channel.Vector.Subvector(fragmentIndex * step, step)));

                        //  Перебор значений в спектре.
                        for (int i = 0; i < amplitudeCount; i++)
                        {
                            //  Добавление амплитуды.
                            amplitudes[i] += fragmentSpectrum[i];
                        }
                    }

                    //  Усреднение всех амплитуд.
                    for (int i = 0; i < amplitudeCount; i++)
                    {
                        //  Добавление амплитуды.
                        amplitudes[i] /= count;
                    }

                    //  Создание спектра.
                    Spectrum spectrum = new()
                    {
                        ChannelId = dbChannel.Id,
                        RegistrarId = dbFrame.RegistrarId,
                        Timestamp = dbFrame.Timestamp,
                    };

                    //  Добавление спектра.
                    dbFrame.Spectrums.Add(spectrum);

                    //  Перебор амплитуд спектра.
                    for (int i = 0; i < amplitudeCount; i++)
                    {
                        //  Получение амплитуды.
                        Complex amplitude = amplitudes[i];

                        //  Добавление амплитуды.
                        spectrum.Amplitudes.Add(new Amplitude
                        {
                            Frequency = i,
                            Real = amplitude.Real,
                            Imaginary = amplitude.Imaginary,
                            Magnitude = amplitude.Magnitude,
                            Phase = amplitude.Phase,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод информации об ошибке в журнал.
                    Logger.LogError("{exception}", ex);
                }

                //  Повторный выброс исключения.
                throw;
            }
        },
        cancellationToken).ConfigureAwait(false);
    }
}
