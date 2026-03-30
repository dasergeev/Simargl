using RailTest.Algebra;
using RailTest.Frames;
using RailTest.Memory;

namespace RailTest.Border
{
    /// <summary>
    /// Представляет сигнал.
    /// </summary>
    public class Signal
    {
        /// <summary>
        /// Поле для хранения смещения.
        /// </summary>
        private double _Offset;

        /// <summary>
        /// Поле для хранения следующего смещения.
        /// </summary>
        private readonly double[] _NextOffset;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя сигнала.
        /// </param>
        /// <param name="unit">
        /// Единица измерения сигнала.
        /// </param>
        /// <param name="virtual">
        /// Значение, определяющее, является ли канал виртуальным.
        /// </param>
        internal Signal(string name, string unit, bool @virtual)
        {
            Name = name;
            Unit = unit;
            Virtual = @virtual;
            _Offset = 0;
            if (!Virtual)
            {
                _NextOffset = new double[40];
                for (int i = 0; i != _NextOffset.Length; ++i)
                {
                    _NextOffset[i] = 0;
                }
            }
            LastData = new RealVector(SignalBuilder.BlockSize);
            Channel = new Channel(name, unit, 2000, 1000, SignalBuilder.DataSize);
        }

        /// <summary>
        /// Возвращает имя сигнала.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает единицу измерения сигнала.
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// Возвращает значение, определяющее, является ли сигнал виртуальным.
        /// </summary>
        public bool Virtual { get; }

        /// <summary>
        /// Возвращает канал.
        /// </summary>
        internal Channel Channel { get; }

        /// <summary>
        /// Возвращает последний блок записанных данных.
        /// </summary>
        public RealVector LastData { get; }

        /// <summary>
        /// Записывает данные.
        /// </summary>
        /// <param name="blockIndex">
        /// Индекс блока.
        /// </param>
        /// <param name="data">
        /// Указатель на область памяти, содержащую данные.
        /// </param>
        internal unsafe void Write(int blockIndex, double* data)
        {
            MemoryManager.Copy(LastData.Pointer.ToPointer(), data, SignalBuilder.BlockSize * sizeof(double));
            if (!Virtual)
            {
                for (int i = 1; i != _NextOffset.Length; ++i)
                {
                    _NextOffset[i - 1] = _NextOffset[i];
                }
                _NextOffset[_NextOffset.Length - 1] = LastData.Average;
                LastData.Move(-_Offset);
            }
            MemoryManager.Copy(Channel.Vector.Pointer + blockIndex * SignalBuilder.BlockSize * sizeof(double),
                LastData.Pointer, SignalBuilder.BlockSize * sizeof(double));
        }

        /// <summary>
        /// Считывает данные.
        /// </summary>
        /// <param name="blockIndex">
        /// Индекс блока.
        /// </param>
        /// <param name="data">
        /// Данные.
        /// </param>
        public unsafe void Read(int blockIndex, double[] data)
        {
            fixed (double* pointer = data)
            {
                MemoryManager.Copy(pointer, ((double*)Channel.Vector.Pointer.ToPointer()) + blockIndex * SignalBuilder.BlockSize,
                    SignalBuilder.BlockSize * sizeof(double));
            }
        }

        /// <summary>
        /// Считывает данные.
        /// </summary>
        /// <param name="beginIndex">
        /// Начальный индекс.
        /// </param>
        /// <param name="endIndex">
        /// Конечный индекс.
        /// </param>
        /// <returns>
        /// Прочитанные данные.
        /// </returns>
        internal unsafe RealVector Read(int beginIndex, int endIndex)
        {
            int count = (endIndex - beginIndex + SignalBuilder.BlockCount) % SignalBuilder.BlockCount;
            RealVector vector = new RealVector(count * SignalBuilder.BlockSize);
            for (int i = 0; i != count; ++i)
            {
                MemoryManager.Copy(vector.Pointer + i * SignalBuilder.BlockSize * sizeof(double),
                    Channel.Vector.Pointer + ((beginIndex + i) % SignalBuilder.BlockCount) * SignalBuilder.BlockSize * sizeof(double),
                    SignalBuilder.BlockSize * sizeof(double));
            }
            return vector;
        }

        /// <summary>
        /// Считывает первое значение блока.
        /// </summary>
        /// <param name="blockIndex">
        /// Индекс блока.
        /// </param>
        /// <returns>
        /// Первое значение блока.
        /// </returns>
        internal double ReadFirst(int blockIndex)
        {
            return Channel[blockIndex * SignalBuilder.BlockSize];
        }

        /// <summary>
        /// Устанавливает ноль.
        /// </summary>
        internal void Zero()
        {
            if (!Virtual)
            {
                _Offset = 0;
                for (int i = 0; i != _NextOffset.Length; ++i)
                {
                    _Offset += _NextOffset[i];
                }
                _Offset /= _NextOffset.Length;
            }
        }
    }
}
