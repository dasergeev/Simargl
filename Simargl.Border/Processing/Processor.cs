using Simargl.Border.Channels;
using Simargl.Border.Hardware;
using Simargl.Border.Schematic;
using Simargl.Border.Storage;
using Simargl.Border.Storage.Entities;
using Simargl.Frames;
using System.IO;

namespace Simargl.Border.Processing;

/// <summary>
/// Представляет устройство обработки.
/// </summary>
public sealed class Processor
{
    /// <summary>
    /// Поле для хранения последнего синхромаркера.
    /// </summary>
    private Synchromarker? _LastSynchromarker;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="scheme">
    /// Схема.
    /// </param>
    private Processor(Scheme scheme)
    {
        //  Установка схемы.
        Scheme = scheme;

        //  Создание коллекции устройств.
        Devices = new(this);

        //  Создание синхронизатора.
        Synchronizer = new();

        //  Создание коллекции групп каналов.
        SectionGroups = new(this);

        //  Создание построителя каналов.
        ChannelBuilder = new(this);
    }

    /// <summary>
    /// Возвращает схему.
    /// </summary>
    public Scheme Scheme { get; }

    /// <summary>
    /// Возвращает коллекцию устройств.
    /// </summary>
    public DeviceCollection Devices { get; }

    /// <summary>
    /// Возвращает синхронизатор модулей.
    /// </summary>
    public Synchronizer Synchronizer { get; }

    /// <summary>
    /// Возвращает коллекцию групп каналов.
    /// </summary>
    public SectionGroupCollection SectionGroups { get; }

    /// <summary>
    /// Возвращает построитель каналов.
    /// </summary>
    public ChannelBuilder ChannelBuilder { get; }

    /// <summary>
    /// Асинхронно создаёт устройство обработки.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая устройство обработки.
    /// </returns>
    public static async Task<Processor> CreateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание схемы.
        Scheme scheme = await Scheme.CreateAsync(cancellationToken).ConfigureAwait(false);

        //  Создание устройства обработки.
        Processor processor = new(scheme);

        //  Возврат устройства обработки.
        return processor;
    }

    /// <summary>
    /// Асинхронно выполняет синхронизацию данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая синхронизацию данных.
    /// </returns>
    public async Task SynchronizationAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка синхронизатора.
        if (!Synchronizer.IsEnable)
        {
            //  Завершение работы.
            return;
        }

        //  Получение конечного синхромаркера.
        Synchromarker end = Synchronizer.LastSynchromarker + BasisConstants.SynchronizationOffset;

        //  Проверка начального синхромаркера.
        if (_LastSynchromarker.HasValue)
        {
            //  Получение начального синхромаркера.
            Synchromarker begin = _LastSynchromarker.Value;

            //  Перебор синхромаркеров.
            for (Synchromarker synchromarker = begin; end - synchromarker > 0; synchromarker += 1)
            {
                //  Построение каналов.
                await ChannelBuilder.BuildAsync(synchromarker, cancellationToken).ConfigureAwait(false);
            }
        }

        //  Установка последнего синхромаркера.
        _LastSynchromarker = end;

        //  Обновление данных.
        await ChannelBuilder.UpdateAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет обработку.
    /// </summary>
    /// <param name="start">
    /// Время начала обработки.
    /// </param>
    /// <param name="end">
    /// Время завершения обработки.
    /// </param>
    /// <param name="key">
    /// Ключ обработки.
    /// </param>
    /// <param name="count">
    /// Количество кадров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обработку.
    /// </returns>
    public async Task ProcessAsync(
        DateTime start, DateTime end, long key, int count,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Смещение по времени.
        TimeSpan timeOffset = TimeSpan.FromSeconds(- BasisConstants.SynchronizationOffset / 40.0);

        //  Корректировка времени.
        start += timeOffset;
        end += timeOffset;

        //  Запуск асинхронной задачи.
        _ = Task.Run(async delegate
        {
            //  Постоянная определяющая длину фрагмента.
            const int fragmentLength = BasisConstants.SignalFragmentLength * BasisConstants.ChannelSourceSaveSize;

            //  Создание списка исходных файлов.
            string[] paths = [.. Enumerable.Range(1, count)
                .Select(x => Path.Combine(
                    BasisConstants.FrameQueuePath,
                    $"Vp0_0 {key:X16}.{x:0000}"))];

            //  Создание контекста для работы с базой данных.
            await using BorderStorageContext context = new(BasisConstants.Storage);

            //  Начало транзакции.
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

            //  Блок перехвата всех исключений.
            try
            {
                //  Создание данных о проезде.
                PassageData passage = new()
                {
                    StartTimestamp = start.Ticks,
                    EndTimestamp = end.Ticks,
                    State = PassageState.Registered,
                };

                //  Добавление в базу данных.
                await context.Passages.AddAsync(passage, cancellationToken).ConfigureAwait(false);

                //  Сохранение изменений.
                await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                //  Перебор каналов.
                for (int channelIndex = 0; channelIndex < SectionGroups.Sources.Count; channelIndex++)
                {
                    //  Получение источника канала.
                    ChannelSource channelSource = SectionGroups.Sources[channelIndex];

                    //  Создание кадра.
                    Frame frame = new();

                    //  Создание канала.
                    Channel targetChannel = new(
                        "channel", channelSource.Unit, 2000, 1000,
                        paths.Length * fragmentLength);

                    //  Добавление канала.
                    frame.Channels.Add(targetChannel);

                    //  Перебор исходных кадров.
                    for (int frameIndex = 0; frameIndex < count; frameIndex++)
                    {
                        //  Загрузка кадра.
                        Frame fragmentFrame = new(paths[frameIndex]);

                        //  Получение исходного канала.
                        Channel fragmentChannel = fragmentFrame.Channels[channelSource.Name];

                        //  Получение исходных данных.
                        double[] source = fragmentChannel.Items;

                        //  Получение данных полного канала.
                        double[] target = targetChannel.Items;

                        //  Копирование данных.
                        Array.Copy(source, 0, target, frameIndex * fragmentLength, fragmentLength);
                    }

                    //  Определение каталога.
                    string directory = Path.Combine(BasisConstants.RawFramesPath, $"0x{passage.Key:X16}");

                    //  Создание каталога.
                    Directory.CreateDirectory(directory);

                    //  Сохранение кадра.
                    frame.Save(Path.Combine(directory, $"Vp0_0 {channelSource.Name}.{channelIndex:0000}"), StorageFormat.TestLab);
                }

                //  Применение транзакции.
                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                //  Отмена транзакции.
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

                //  Повторный выброс исключения.
                throw;
            }

            //  Перебор исходных файлов.
            foreach (string path in paths)
            {
                //  Блок перехвата всхе исключений.
                try
                {
                    //  Удаление файла.
                    File.Delete(path);
                }
                catch { }
            }
        }, cancellationToken);
    }
}
