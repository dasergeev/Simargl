//using System.Threading;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет объект, который выполняет обработку.
//    /// </summary>
//    public class Processing
//    {
//        /// <summary>
//        /// Поле для хранения потока, в котором происходит обработка.
//        /// </summary>
//        private Thread _Thread;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="groups">
//        /// Коллекция групп сечений.
//        /// </param>
//        internal Processing(SectionGroupCollection groups)
//        {
//            Groups = groups;
//            _FirstBlockIndex = new int[groups.Count];
//            _AxisCounters = new int[groups.Count];
//            IsWork = false;
//            Axes = new AxisCollection(groups);
//        }

//        /// <summary>
//        /// Возвращает коллекцию осей.
//        /// </summary>
//        public AxisCollection Axes { get; }

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

//        /// <summary>
//        /// Возвращает значение, определяющее выполняется ли обработка.
//        /// </summary>
//        public bool IsWork { get; private set; }

//        /// <summary>
//        /// Возвращает коллекцию групп сечений.
//        /// </summary>
//        internal SectionGroupCollection Groups { get; }

//        /// <summary>
//        /// Запускает обработку.
//        /// </summary>
//        public void Start()
//        {
//            lock (SyncRoot)
//            {
//                if (!IsWork)
//                {
//                    IsWork = true;
//                    _Thread = new Thread(ThreadEntry)
//                    {
//                        IsBackground = true,
//                        Priority = ThreadPriority.Highest
//                    };
//                    _Thread.Start();
//                }
//            }
//        }

//        /// <summary>
//        /// Останавливает обработку.
//        /// </summary>
//        public void Stop()
//        {
//            lock (SyncRoot)
//            {
//                if (_Thread is object)
//                {
//                    IsWork = false;
//                    bool result = false;
//                    try
//                    {
//                        result = _Thread.Join(1000);
//                    }
//                    catch (ThreadStateException)
//                    {

//                    }
//                    if (!result)
//                    {
//                        try
//                        {
//                            _Thread.Abort();
//                        }
//                        catch (ThreadStateException)
//                        {

//                        }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Поле для хранения массива индексов блоков, в которые началось прохождение оси сечения.
//        /// </summary>
//        private readonly int[] _FirstBlockIndex;

//        /// <summary>
//        /// Поле для хранения счётчиков осей.
//        /// </summary>
//        private readonly int[] _AxisCounters;

//        /// <summary>
//        /// Выполняет подсчёт осей.
//        /// </summary>
//        /// <param name="sectionIndex">
//        /// Индекс сечения.
//        /// </param>
//        /// <param name="frameNumber">
//        /// Номер кадра.
//        /// </param>
//        /// <param name="blockIndex">
//        /// Индекс блока.
//        /// </param>
//        /// <param name="existence">
//        /// Наличие оси.
//        /// </param>
//        private void AxisCounting(int sectionIndex, int frameNumber, int blockIndex, bool existence)
//        {
//            if (existence)
//            {
//                if (_FirstBlockIndex[sectionIndex] == -1)
//                {
//                    _FirstBlockIndex[sectionIndex] = blockIndex;
//                }
//            }
//            else
//            {
//                if (_FirstBlockIndex[sectionIndex] != -1)
//                {
//                    AxisRegistration(sectionIndex, frameNumber, _FirstBlockIndex[sectionIndex], blockIndex, _AxisCounters[sectionIndex]);
//                    ++_AxisCounters[sectionIndex];
//                    _FirstBlockIndex[sectionIndex] = -1;
//                }
//            }
//        }

//        /// <summary>
//        /// Выполняет регистрацию оси на сечении.
//        /// </summary>
//        /// <param name="sectionIndex">
//        /// Индекс сечения.
//        /// </param>
//        /// <param name="frameNumber">
//        /// Номер кадра.
//        /// </param>
//        /// <param name="beginBlockIndex">
//        /// Индекс начального блока.
//        /// </param>
//        /// <param name="endBlockIndex">
//        /// Индекс конечного блока.
//        /// </param>
//        /// <param name="axisNumber">
//        /// Номер оси.
//        /// </param>
//        private void AxisRegistration(int sectionIndex, int frameNumber, int beginBlockIndex, int endBlockIndex, int axisNumber)
//        {
//            lock (Axes)
//            {
//                Axes[axisNumber].Registration(sectionIndex, frameNumber, beginBlockIndex, endBlockIndex);
//            }
//        }

//        /// <summary>
//        /// Представляет точку входа потока, который выполняет обработку.
//        /// </summary>
//        private void ThreadEntry()
//        {
//            int lastBlockIndex = 0;
//            lock (SyncRoot)
//            {
//                if (IsWork)
//                {
//                    Groups.Zero();
//                    Axes.Clear();
//                    lastBlockIndex = (Groups.BlockIndex + SignalBuilder.BlockCount - 10) % SignalBuilder.BlockCount;
//                    for (int i = 0; i != _FirstBlockIndex.Length; ++i)
//                    {
//                        _FirstBlockIndex[i] = -1;
//                        _AxisCounters[i] = 0;
//                    }
//                }
//            }
//            while (IsWork)
//            {
//                lock (Groups.SyncRoot)
//                {
//                    int frameNumber = 0;
//                    int blockIndex = (Groups.BlockIndex + SignalBuilder.BlockCount - 10) % SignalBuilder.BlockCount;
//                    while (lastBlockIndex != blockIndex && IsWork)
//                    {
//                        lastBlockIndex = (lastBlockIndex + 1) % SignalBuilder.BlockCount;
//                        if (lastBlockIndex <= (blockIndex + 10) % SignalBuilder.BlockCount)
//                        {
//                            frameNumber = Groups.FrameNumber;
//                        }
//                        else
//                        {
//                            frameNumber = Groups.FrameNumber - 1;
//                        }

//                        for (int i = 0; i != Groups.Count; ++i)
//                        {
//                            AxisCounting(i, frameNumber, lastBlockIndex, Groups[i].Counter.ReadFirst(lastBlockIndex) > 0.5);
//                        }
//                    }
//                }
//                Thread.Sleep(50);
//                lock (Axes)
//                {
//                    foreach (var number in Axes.Numbers)
//                    {
//                        if (!IsWork)
//                        {
//                            break;
//                        }
//                        var axis = Axes[number];
//                        if (!axis.Worked)
//                        {
//                            axis.Work();
//                        }
//                    }
//                }
//                Thread.Sleep(50);
//            }
//        }
//    }
//}
