using Simargl.Border.Hardware;
using Simargl.Border.Processing;

namespace Simargl.Border.Channels;

/// <summary>
/// Представляет построитель каналов.
/// </summary>
public sealed class ChannelBuilder :
    ProcessorUnit
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="processor">
    /// Устройство обработки.
    /// </param>
    internal ChannelBuilder(Processor processor) :
        base(processor)
    {

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

        //  Построение групп каналов.
        await Processor.SectionGroups.BuildAsync(synchromarker, cancellationToken).ConfigureAwait(false);
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

        //  Обновление групп каналов.
        await Processor.SectionGroups.UpdateAsync(cancellationToken).ConfigureAwait(false);
    }
}



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

