//using RailTest.Memory;
//using System.Threading;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет построитель сигналов.
//    /// </summary>
//    internal class SignalBuilder
//    {
//        /// <summary>
//        /// Постоянная, определяющая размер блока данных.
//        /// </summary>
//        public const int BlockSize = Receiver.ReadSize;

//        /// <summary>
//        /// Постоянная, определяющая количество блоков.
//        /// </summary>
//        public const int BlockCount = 1000;

//        /// <summary>
//        /// Постоянная, определяющая общий размер данных.
//        /// </summary>
//        public const int DataSize = 128000;

//        /// <summary>
//        /// Поле для хранения потока, в котором выполняется работа.
//        /// </summary>
//        private readonly Thread _Thread;

//        /// <summary>
//        /// Поле для хранения значения, определяющего выполняется ли работа.
//        /// </summary>
//        private bool _IsWork;

//        /// <summary>
//        /// Постоянная, определяющая количество исходных сигналов.
//        /// </summary>
//        public const int SourceSignalCount = Receiver.ClientsCount * 3;

//        /// <summary>
//        /// Поле для хранения массива исходных сигналов.
//        /// </summary>
//        private readonly Signal[] _SourceSignals;
        
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="receiver">
//        /// Приёмник данных.
//        /// </param>
//        /// <param name="groups">
//        /// Коллекция групп сигналов.
//        /// </param>
//        public SignalBuilder(Receiver receiver, SectionGroupCollection groups)
//        {
//            Receiver = receiver;
//            Groups = groups;

//            _SourceSignals = new Signal[SourceSignalCount];
//            for (int i = 0; i != Receiver.ClientsCount; ++i)
//            {
//                SideGroup group = Groups.ByModule(i);
//                _SourceSignals[i * 3 + 0] = group.Signal0;
//                _SourceSignals[i * 3 + 1] = group.Signal1;
//                _SourceSignals[i * 3 + 2] = group.Signal2;
//            }

//            _Thread = new Thread(ThreadEntry);
//            _IsWork = true;
//            _Thread.IsBackground = true;
//            _Thread.Priority = ThreadPriority.Highest;
//            _Thread.Start();
//        }

//        /// <summary>
//        /// Возвращает приёмник данных.
//        /// </summary>
//        public Receiver Receiver { get; }

//        /// <summary>
//        /// Возвращает коллекцию групп сигналов.
//        /// </summary>
//        public SectionGroupCollection Groups { get; }

//        /// <summary>
//        /// Представляет точку входа рабочего потока.
//        /// </summary>
//        private unsafe void ThreadEntry()
//        {
//            int receiverIndex = 0;
//            double* data = (double*)MemoryManager.Alloc(BlockSize * sizeof(double));
//            try
//            {
//                while (_IsWork)
//                {
//                    int index = Receiver.ReadIndex;
//                    if (receiverIndex != index)
//                    {
//                        while (receiverIndex != index && _IsWork)
//                        {
//                            receiverIndex = (receiverIndex + 1) % Receiver.ReadCount;
//                            Groups.BlockIndex = (Groups.BlockIndex + 1) % BlockCount;
//                            int blockIndex = Groups.BlockIndex;
//                            lock (Groups.SyncRoot)
//                            {
//                                for (int i = 0; i != SourceSignalCount; ++i)
//                                {
//                                    Receiver.Read(receiverIndex, i, data);
//                                    _SourceSignals[i].Write(blockIndex, data);
//                                }

//                                //  Обработка
//                                Groups.Update(blockIndex);


//                                if (blockIndex == BlockCount - 1)
//                                {
//                                    Groups.Save();
//                                }
//                            }
//                        }
//                    }
//                    else
//                    {
//                        Thread.Sleep(10);
//                    }
//                }
//            }
//            catch
//            {

//            }
//            finally
//            {
//                MemoryManager.Free(data);
//            }
//            if (_IsWork)
//            {
//                ThreadEntry();
//            }
//        }

//        /// <summary>
//        /// Останавливает работу.
//        /// </summary>
//        public void Stop()
//        {
//            _IsWork = false;
//            bool result = false;
//            try
//            {
//                result = _Thread.Join(1000);
//            }
//            catch (ThreadStateException)
//            {

//            }
//            if (!result)
//            {
//                try
//                {
//                    _Thread.Abort();
//                }
//                catch (ThreadStateException)
//                {

//                }
//            }
//        }
//    }
//}
