using Simargl.Analysis;
using Simargl.Border.Hardware;
using Simargl.Border.Processing;

namespace Simargl.Border.Channels;

/// <summary>
/// Представляет источник канала.
/// </summary>
public sealed class ChannelSource :
    ProcessorUnit
{
    /// <summary>
    /// Поле для хранения буфера сигналов.
    /// </summary>
    private readonly CircularBuffer<Signal> _Buffer;

    //        /// <summary>
    //        /// Поле для хранения смещения.
    //        /// </summary>
    //        private double _Offset;

    //        /// <summary>
    //        /// Поле для хранения следующего смещения.
    //        /// </summary>
    //        private readonly double[] _NextOffset;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="processor">
    /// Устройство обработки.
    /// </param>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <param name="unit">
    /// Единица измерения.
    /// </param>
    ///// <param name="virtual">
    ///// Значение, определяющее, является ли канал виртуальным.
    ///// </param>
    internal ChannelSource(Processor processor, string name, string unit/*, bool @virtual*/) :
        base(processor)
    {
        //  Установка значений основных свойств.
        Name = name;
        Unit = unit;
        //Virtual = @virtual;

        //  Создание буфера сигналов.
        _Buffer = new(BasisConstants.ChannelSourceBufferSize);

        //            _Offset = 0;
        //            if (!Virtual)
        //            {
        //                _NextOffset = new double[40];
        //                for (int i = 0; i != _NextOffset.Length; ++i)
        //                {
        //                    _NextOffset[i] = 0;
        //                }
        //            }
        //            LastData = new RealVector(SignalBuilder.BlockSize);
        //            Channel = new Channel(name, unit, 2000, 1000, SignalBuilder.DataSize);

    }

    /// <summary>
    /// Возвращает имя сигнала.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает единицу измерения сигнала.
    /// </summary>
    public string Unit { get; }

    ///// <summary>
    ///// Возвращает значение, определяющее, является ли сигнал виртуальным.
    ///// </summary>
    //public bool Virtual { get; }

    /// <summary>
    /// Возвращает значение, определяющее загружены ли данные канала.
    /// </summary>
    public bool IsLoaded { get; private set; }

    /// <summary>
    /// Возвращает среднее значение.
    /// </summary>
    public double Average { get; private set; }

    /// <summary>
    /// Возвращает СКО.
    /// </summary>
    public double Deviation { get; private set; }

    ///// <summary>
    ///// Возвращает нулевое значение.
    ///// </summary>
    //public double ZeroValue { get; private set; }

    ///// <summary>
    ///// Возвращает время установки нулевого значения.
    ///// </summary>
    //public DateTime ZeroTime { get; private set; }

    /// <summary>
    /// Возвращает элемент с указанным синхромаркером.
    /// </summary>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    /// <returns>
    /// Элемент с указанным синхромаркером.
    /// </returns>
    public Signal? this[Synchromarker synchromarker] => _Buffer[synchromarker];

    /// <summary>
    /// Регистрирует новый элемент.
    /// </summary>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    /// <param name="item">
    /// Новый элемент.
    /// </param>
    public void Register(Synchromarker synchromarker, Signal item)
    {
        //  Регистрация в буфере.
        _Buffer.Register(synchromarker, item);
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

        //  Определение значения, определяющего загружены ли данные канала.
        IsLoaded = _Buffer.Count >= BasisConstants.ChannelSourceZeroCount;

        //  Проверка значения.
        if (IsLoaded)
        {
            //  Определение начальных значений.
            int count = 0;
            double sum = 0;
            double squaresSum = 0;

            //  Перебор синхромаркеров.
            for (int index = 0; index != BasisConstants.ChannelSourceZeroCount; ++index)
            {
                //  Получение синхромаркера.
                Synchromarker synchromarker = _Buffer.Synchromarker + (-index);

                //  Получение сигнала.
                Signal? signal = _Buffer[synchromarker];

                //  Проверка сигнала.
                if (signal is not null)
                {
                    //  Получение значений.
                    double[] items = signal.Items;

                    //  Перебор значений.
                    for (int i = 0; i < items.Length; i++)
                    {
                        //  Чтение значения.
                        double value = items[i] /*- ZeroValue*/;

                        //  Корректировка сумм.
                        sum += value;
                        squaresSum += value * value;
                    }

                    //  Корректировка колличества.
                    count += items.Length;
                }
            }

            ////  Перебор сигналов.
            //foreach (Signal? signal in _Buffer)
            //{
            //    //  Проверка сигнала.
            //    if (signal is not null)
            //    {
            //        //  Получение значений.
            //        double[] items = signal.Items;

            //        //  Перебор значений.
            //        for (int i = 0; i < items.Length; i++)
            //        {
            //            //  Чтение значения.
            //            double value = items[i] - ZeroValue;

            //            //  Корректировка сумм.
            //            sum += value;
            //            squaresSum += value * value;
            //        }

            //        //  Корректировка колличества.
            //        count += items.Length;
            //    }
            //}
            
            //  Проверка количества.
            if (count > 1)
            {
                //  Расчёт значений.
                Average = sum / count;
                Deviation = Math.Sqrt((count * squaresSum - sum * sum) / (count * (count - 1)));
            }
            else
            {
                //  Корректировка значения, определяющего загружены ли данные канала.
                IsLoaded = false;
            }
        }

        //  Проверка значения.
        if (!IsLoaded)
        {
            //  Сброс значений.
            Average = 0;
            Deviation = 0;
        }
    }

    ///// <summary>
    ///// Асинхронно устанавливает ноль.
    ///// </summary>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, устанавливающая ноль.
    ///// </returns>
    //public async Task ZeroAsync(CancellationToken cancellationToken)
    //{
    //    //  Проверка токена отмены.
    //    await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Проверка наличия данных.
    //    if (IsLoaded)
    //    {
    //        //  Корректировка нулевого значения.
    //        ZeroValue += Average;

    //        //  Корректировка среднего значения.
    //        Average = 0;

    //        //  Установка времени.
    //        ZeroTime = DateTime.Now;
    //    }
    //}
}


//        /// <summary>
//        /// Возвращает канал.
//        /// </summary>
//        internal Channel Channel { get; }

//        /// <summary>
//        /// Возвращает последний блок записанных данных.
//        /// </summary>
//        public RealVector LastData { get; }

//        /// <summary>
//        /// Записывает данные.
//        /// </summary>
//        /// <param name="blockIndex">
//        /// Индекс блока.
//        /// </param>
//        /// <param name="data">
//        /// Указатель на область памяти, содержащую данные.
//        /// </param>
//        internal unsafe void Write(int blockIndex, double* data)
//        {
//            MemoryManager.Copy(LastData.Pointer.ToPointer(), data, SignalBuilder.BlockSize * sizeof(double));
//            if (!Virtual)
//            {
//                for (int i = 1; i != _NextOffset.Length; ++i)
//                {
//                    _NextOffset[i - 1] = _NextOffset[i];
//                }
//                _NextOffset[_NextOffset.Length - 1] = LastData.Average;
//                LastData.Move(-_Offset);
//            }
//            MemoryManager.Copy(Channel.Vector.Pointer + blockIndex * SignalBuilder.BlockSize * sizeof(double),
//                LastData.Pointer, SignalBuilder.BlockSize * sizeof(double));
//        }

//        /// <summary>
//        /// Считывает данные.
//        /// </summary>
//        /// <param name="blockIndex">
//        /// Индекс блока.
//        /// </param>
//        /// <param name="data">
//        /// Данные.
//        /// </param>
//        public unsafe void Read(int blockIndex, double[] data)
//        {
//            fixed (double* pointer = data)
//            {
//                MemoryManager.Copy(pointer, ((double*)Channel.Vector.Pointer.ToPointer()) + blockIndex * SignalBuilder.BlockSize,
//                    SignalBuilder.BlockSize * sizeof(double));
//            }
//        }

//        /// <summary>
//        /// Считывает данные.
//        /// </summary>
//        /// <param name="beginIndex">
//        /// Начальный индекс.
//        /// </param>
//        /// <param name="endIndex">
//        /// Конечный индекс.
//        /// </param>
//        /// <returns>
//        /// Прочитанные данные.
//        /// </returns>
//        internal unsafe RealVector Read(int beginIndex, int endIndex)
//        {
//            int count = (endIndex - beginIndex + SignalBuilder.BlockCount) % SignalBuilder.BlockCount;
//            RealVector vector = new RealVector(count * SignalBuilder.BlockSize);
//            for (int i = 0; i != count; ++i)
//            {
//                MemoryManager.Copy(vector.Pointer + i * SignalBuilder.BlockSize * sizeof(double),
//                    Channel.Vector.Pointer + ((beginIndex + i) % SignalBuilder.BlockCount) * SignalBuilder.BlockSize * sizeof(double),
//                    SignalBuilder.BlockSize * sizeof(double));
//            }
//            return vector;
//        }

//        /// <summary>
//        /// Считывает первое значение блока.
//        /// </summary>
//        /// <param name="blockIndex">
//        /// Индекс блока.
//        /// </param>
//        /// <returns>
//        /// Первое значение блока.
//        /// </returns>
//        internal double ReadFirst(int blockIndex)
//        {
//            return Channel[blockIndex * SignalBuilder.BlockSize];
//        }

//    }
//}
