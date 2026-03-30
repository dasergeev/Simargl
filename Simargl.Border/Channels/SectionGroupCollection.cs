using Simargl.Analysis;
using Simargl.Border.Hardware;
using Simargl.Border.Processing;
using Simargl.Frames;
using System.IO;

namespace Simargl.Border.Channels;

/// <summary>
/// Представляет коллекцию групп каналов.
/// </summary>
public sealed class SectionGroupCollection :
    ProcessorUnit,
    IEnumerable<SectionGroup>
{
    //        /// <summary>
    //        /// Постоянная, определяющая размер блока данных.
    //        /// </summary>
    //        public const int BlockSize = SignalBuilder.BlockSize;

    //        /// <summary>
    //        /// Постоянная, определяющая количество блоков.
    //        /// </summary>
    //        public const int BlockCount = SignalBuilder.BlockCount;

    /// <summary>
    /// Поле для хранения массива элементов коллекции.
    /// </summary>
    private readonly SectionGroup[] _Items;

    /// <summary>
    /// Поле для хранения первого синхромаркера.
    /// </summary>
    private Synchromarker? _FirstSynchromarker;

    /// <summary>
    /// Поле для хранения последнего синхромаркера.
    /// </summary>
    private Synchromarker? _LastSynchromarker;

    /// <summary>
    /// Поле для хранения очереди кадров.
    /// </summary>
    private readonly Queue<Frame> _FrameQueue = [];

    /// <summary>
    /// Поле для хранения времени начала обработки.
    /// </summary>
    private DateTime? _ProcessingStart;

    /// <summary>
    /// Поле для хранения времени конца обработки.
    /// </summary>
    private DateTime? _ProcessingEnd;

    /// <summary>
    /// Поле для хранения ключа обработки.
    /// </summary>
    private long _ProcessingKey;

    /// <summary>
    /// Поле для хранения текущего номера кадра.
    /// </summary>
    private int _FrameNumber;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="processor">
    /// Устройство обработки.
    /// </param>
    internal SectionGroupCollection(Processor processor) :
        base(processor)
    {
        //  Создание массива групп.
        _Items = new SectionGroup[BasisConstants.SectionGroupCount];

        //  Создание списка источников каналов.
        List<ChannelSource> sources = [];

        //  Перебор групп.
        for (int i = 0; i != _Items.Length; ++i)
        {
            //  Создание группы.
            SectionGroup group = new(processor, i + 1);

            //  Установка группы.
            _Items[i] = group;

            //  Добавление сигналов группы.
            sources.AddRange(group.Sources);
        }

        //            Frame = new Frame();
        //            foreach (var signal in Signals)
        //            {
        //                Frame.Channels.Add(signal.Channel);
        //            }

        //  Установка коллекции всех источников каналов.
        Sources = sources.AsReadOnly();


        //            FrameNumber = 0;
        //            while (File.Exists(GetFileName(FrameNumber)))
        //            {
        //                ++FrameNumber;
        //            }

        //            BlockIndex = 0;
    }

    /// <summary>
    /// Асинхронно выполняет построение.
    /// </summary>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение.
    /// </returns>
    public async Task BuildAsync(Synchromarker synchromarker, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор элементов.
        foreach (SectionGroup group in _Items)
        {
            //  Построение группы.
            await group.BuildAsync(synchromarker, cancellationToken).ConfigureAwait(false);
        }

        //  Установка последнего синхромаркера.
        _LastSynchromarker = synchromarker;
    }

    /// <summary>
    /// Асинхронно обновляет данные.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, обновляющая данные.
    /// </returns>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор каналов.
        foreach (ChannelSource source in Sources)
        {
            //  Обновление канала.
            await source.UpdateAsync(cancellationToken).ConfigureAwait(false);
        }

        //  Поле для хранения флага завершения обработки.
        bool isCompletion = false;

        //  Поле для хранения счётчика порогов.
        int thresholdCount = 0;

        //  Перебор элементов.
        foreach (SectionGroup group in _Items)
        {
            //  Построение группы.
            await group.UpdateAsync(cancellationToken).ConfigureAwait(false);

            //  Перебор каналов.
            foreach (ChannelSource source in new ChannelSource[]
            {
                group.Left.Internal.Source0,
                group.Left.External.Source0,
                group.Right.Internal.Source0,
                group.Right.External.Source0,
            })
            {
                //  Проверка порога.
                if (source.IsLoaded && source.Deviation > BasisConstants.SignalDeviationThreshold)
                {
                    //  Увеличение счётчика порогов.
                    ++thresholdCount;
                }
            }
        }

        //  Определение текущего времени.
        DateTime now = DateTime.Now;

        //  Проверка счётчика.
        if (thresholdCount > BasisConstants.ThresholdCountMinCount)
        {
            //  Проверка запуска обработки.
            if (!_ProcessingStart.HasValue)
            {
                //  Настройка начала обработки.
                _ProcessingStart = now;
                ++_ProcessingKey;
                _FrameNumber = 1;
            }

            //  Сброс завершения обработки.
            _ProcessingEnd = null;
        }
        else
        {
            //  Проверка запуска обработки.
            if (_ProcessingStart.HasValue)
            {
                //  Проверка времени завершения обработки.
                if (!_ProcessingEnd.HasValue)
                {
                    //  Установка времени завершения обработки.
                    _ProcessingEnd = now;
                }
                else
                {
                    //  Проверка длительности завершения.
                    if (now - _ProcessingEnd.Value > BasisConstants.ProcessingCompletionDuration)
                    {
                        //  Установка флага завершения обработки.
                        isCompletion = true;
                    }
                }
            }
        }

        //  Проверка последнего синхромаркера.
        if (_LastSynchromarker.HasValue)
        {
            //  Проверка первого синхромаркера.
            if (_FirstSynchromarker.HasValue)
            {
                //  Проверка необходимости формирования кадра.
                if (_LastSynchromarker.Value - _FirstSynchromarker.Value > BasisConstants.ChannelSourceSaveSize)
                {
                    //  Создание кадра регистрации.
                    Frame frame = new();

                    //  Перебор каналов.
                    foreach (ChannelSource source in Sources)
                    {
                        //  Создание канала.
                        Channel channel = new(
                            source.Name, source.Unit, 2000, 1000,
                            BasisConstants.SignalFragmentLength * BasisConstants.ChannelSourceSaveSize);

                        //  Получение массива значений канала.
                        double[] channelItems = channel.Items;

                        //  Индекс значения.
                        int index = 0;

                        //  Перебор сигналов.
                        for (int signalIndex = 0; signalIndex < BasisConstants.ChannelSourceSaveSize; ++signalIndex)
                        {
                            //  Определение синхромаркера.
                            Synchromarker synchromarker = _FirstSynchromarker.Value + signalIndex;

                            //  Получение сигнала.
                            if (source[synchromarker] is Signal signal)
                            {
                                //  Получение массива значений сигнала.
                                double[] signalItems = signal.Items;

                                //  Перебор значений сигнала.
                                for (int i = 0; i < BasisConstants.SignalFragmentLength; i++)
                                {
                                    //  Запись значения.
                                    channelItems[index] = signalItems[i];

                                    //  Смещение индекса.
                                    ++index;
                                }
                            }
                            else
                            {
                                //  Смещение индекса.
                                index += BasisConstants.SignalFragmentLength;
                            }
                        }

                        //  Добавление канала в кадр.
                        frame.Channels.Add(channel);
                    }

                    //  Добавление кадра в очередь.
                    _FrameQueue.Enqueue(frame);

                    //  Смещение первого синхромаркера.
                    _FirstSynchromarker = _FirstSynchromarker.Value + BasisConstants.ChannelSourceSaveSize;
                }
            }
            else
            {
                //  Установка первого синхромаркера.
                _FirstSynchromarker = _LastSynchromarker.Value;
            }

            //  Блок перехвата всех исключений.
            try
            {
                //  Проверка режима сохранения.
                if (_ProcessingStart.HasValue)
                {
                    //  Извлечение кадров из очереди.
                    while (_FrameQueue.TryDequeue(out Frame? frame))
                    {
                        //  Проверка кадра.
                        if (frame is not null)
                        {
                            //  Создание каталога.
                            Directory.CreateDirectory(BasisConstants.FrameQueuePath);

                            //  Определение пути к кадру.
                            string path = Path.Combine(
                                BasisConstants.FrameQueuePath,
                                $"Vp0_0 {_ProcessingKey:X16}.{_FrameNumber:0000}");

                            //  Сохранение кадра.
                            frame.Save(path, StorageFormat.TestLab);

                            //  Увеличение номера кадра.
                            ++_FrameNumber;
                        }
                    }
                }
            }
            catch
            {
                //  Сброс режима обработки.
                _ProcessingStart = null;
                _ProcessingEnd = null;

                //  Повторный выброс исключения.
                throw;
            }

            //  Проверка флага завершения обработки.
            if (isCompletion)
            {
                //  Проверка параметров.
                if (_ProcessingStart.HasValue && _ProcessingEnd.HasValue)
                {
                    //  Запуск обработки.
                    await Processor.ProcessAsync(
                        _ProcessingStart.Value,
                        _ProcessingEnd.Value,
                        _ProcessingKey,
                        _FrameNumber - 1,
                        cancellationToken).ConfigureAwait(false);
                }

                //  Сброс режима обработки.
                _ProcessingStart = null;
                _ProcessingEnd = null;
            }

            //  Нормализация очереди кадров.
            while (_FrameQueue.Count > BasisConstants.FrameQueueLength)
            {
                //  Удаление кадра.
                _FrameQueue.Dequeue();
            }
        }
    }



    //        /// <summary>
    //        /// Возвращает текущего номер кадра.
    //        /// </summary>
    //        internal int FrameNumber { get; private set; }

    //        /// <summary>
    //        /// Возвращает имя файла.
    //        /// </summary>
    //        /// <param name="index">
    //        /// Индекс файла.
    //        /// </param>
    //        /// <returns>
    //        /// Имя файла.
    //        /// </returns>
    //        private static string GetFileName(int index)
    //        {
    //            return $"C:\\Source\\Vp0_0.{index:0000}";
    //        }

    //        /// <summary>
    //        /// Возвращает кадр.
    //        /// </summary>
    //        internal Frame Frame { get; }

    //        /// <summary>
    //        /// Возвращает последний записанный индекс.
    //        /// </summary>
    //        public int BlockIndex { get; internal set; }

    //        /// <summary>
    //        /// Сохраняет текущее состояние.
    //        /// </summary>
    //        internal void Save()
    //        {
    //            Frame.Save(GetFileName(FrameNumber), StorageFormat.TestLab);
    //            ++FrameNumber;
    //        }

    /// <summary>
    /// Возвращает количество групп в коллекции.
    /// </summary>
    public int Count => _Items.Length;


    //        /// <summary>
    //        /// Возвращает объект, который может быть использован для синхронизации доступа.
    //        /// </summary>
    //        public object SyncRoot
    //        {
    //            get
    //            {
    //                return this;
    //            }
    //        }

    /// <summary>
    /// Возвращает группу с указанным индексом.
    /// </summary>
    /// <param name="section">
    /// Индекс группы.
    /// </param>
    /// <returns>
    /// Группа.
    /// </returns>
    public SectionGroup this[int section] => _Items[section - 1];

    ///// <summary>
    ///// Возвращает группу, которая принадлежит заданному сечению.
    ///// </summary>
    ///// <param name="section">
    ///// Номер сечения.
    ///// </param>
    ///// <returns>
    ///// Группа.
    ///// </returns>
    //public SectionGroup BySection(int section)
    //{
    //    return this[section - 1];
    //}

    /// <summary>
    /// Возвращает коллекцию всех источников каналов.
    /// </summary>
    public IReadOnlyList<ChannelSource> Sources { get; }

    ///// <summary>
    ///// Возвращает группу каналов, принадлежащую модулю с указанным индексом.
    ///// </summary>
    ///// <param name="index">
    ///// Индекс модуля.
    ///// </param>
    ///// <returns>
    ///// Группа.
    ///// </returns>
    //internal SideGroup ByModule(int index)
    //{
    //    int position = index % 4;
    //    SectionGroup sectionGroup = _Groups[(index - position) / 4];
    //    return position switch
    //    {
    //        0 => sectionGroup.Right.Internal,
    //        1 => sectionGroup.Right.External,
    //        2 => sectionGroup.Left.Internal,
    //        _ => sectionGroup.Left.External,
    //    };
    //}

    //        /// <summary>
    //        /// Обновляет данные.
    //        /// </summary>
    //        /// <param name="blockIndex">
    //        /// Индекс чтения.
    //        /// </param>
    //        internal void Update(int blockIndex)
    //        {
    //            foreach (var group in this)
    //            {
    //                group.Update(blockIndex);
    //            }
    //        }

    //        /// <summary>
    //        /// Выставляет ноль.
    //        /// </summary>
    //        public void Zero()
    //        {
    //            lock (SyncRoot)
    //            {
    //                foreach (var group in this)
    //                {
    //                    group.Zero();
    //                }
    //            }
    //        }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<SectionGroup> GetEnumerator()
    {
        return ((IEnumerable<SectionGroup>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<SectionGroup>)_Items).GetEnumerator();
    }
}
