using RailTest.Memory;
using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет измерительный буфер.
    /// </summary>
    public class MeasuringBuffer
    {
        /// <summary>
        /// Постоянная для хранения размера заголовка.
        /// </summary>
        private const int _HeaderSize = 16;

        /// <summary>
        /// Постоянная для хранения длины сигнала.
        /// </summary>
        private const uint _SignalLength = Settings.Sampling * Settings.Duration * 2;

        /// <summary>
        /// Постоянная для хранения размера сигнала.
        /// </summary>
        private const uint _SignalSize = _SignalLength * 8;

        /// <summary>
        /// Постоянная для хранения размера нулей сигналов.
        /// </summary>
        private const uint _ZerosSize = Settings.CountSignals * 8;

        /// <summary>
        /// Постоянная для хранения размера памяти.
        /// </summary>
        private const uint _FullSize = _HeaderSize + Settings.CountSignals * _SignalSize + _ZerosSize;

        /// <summary>
        /// Поле для хранения общей памяти.
        /// </summary>
        private readonly SharedMemory _Memory;

        /// <summary>
        /// Поле для хранения объекта доступа к заголовку.
        /// </summary>
        private readonly MemoryMappedViewAccessor _Header;

        /// <summary>
        /// Поле для хранения объекта доступа к данным сигналов.
        /// </summary>
        private readonly MemoryMappedViewAccessor[] _Signals;

        /// <summary>
        /// Поле для хранения объекта доступа к нулям сигналов.
        /// </summary>
        private readonly MemoryMappedViewAccessor _Zeros;

        /// <summary>
        /// Инициализиирует новый экземпляр класса.
        /// </summary>
        public MeasuringBuffer()
        {
            _Memory = new SharedMemory("Measuring", _FullSize);
            _Header = _Memory.CreateViewAccessor(0, _HeaderSize);
            Mutex = _Memory.Mutex;
            _Signals = new MemoryMappedViewAccessor[Settings.CountSignals];
            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                _Signals[i] = _Memory.CreateViewAccessor(_HeaderSize + i * _SignalSize, _SignalSize);
            }
            _Zeros = _Memory.CreateViewAccessor(_HeaderSize + Settings.CountSignals * _SignalSize, _ZerosSize);
        }

        /// <summary>
        /// Возвращает ноль сигнала.
        /// </summary>
        /// <param name="index">
        /// Индекс сигнала.
        /// </param>
        /// <returns>
        /// Ноль сигнала.
        /// </returns>
        public double GetZero(int index)
        {
            return _Zeros.ReadDouble(index * 8);
        }

        /// <summary>
        /// Устанавливает ноль сигнала.
        /// </summary>
        /// <param name="index">
        /// Индекс сигнала.
        /// </param>
        /// <param name="value">
        /// Ноль сигнала.
        /// </param>
        public void SetZero(int index, double value)
        {
            _Zeros.Write(index * 8, value);
        }

        /// <summary>
        /// Возвращает примитив межпроцессорной синхронизации.
        /// </summary>
        public Mutex Mutex { get; }

        /// <summary>
        /// Возвращает время запуска.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return DateTime.FromOADate(_Header.ReadDouble(0));
            }
            private set
            {
                _Header.Write(0, value.ToOADate());
            }
        }

        /// <summary>
        /// Возвращает текущую позицию.
        /// </summary>
        public long Position
        {
            get
            {
                return _Header.ReadInt64(8);
            }
            private set
            {
                _Header.Write(8, value);
            }
        }

        /// <summary>
        /// Выполняет сброс буфера.
        /// </summary>
        public void Reset()
        {
            Mutex.WaitOne();
            try
            {
                Position = 0;

                for (int i = 0; i != Settings.CountSignals; ++i)
                {
                    var signal = _Signals[i];
                    for (int j = 0; j != _SignalLength; ++j)
                    {
                        signal.Write(j * 8, 0.0);
                    }
                }
                StartTime = DateTime.Now;
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Записывает данные в буфер.
        /// </summary>
        /// <param name="data">
        /// Данные, которые необходимо записать в буфер.
        /// </param>
        /// <param name="count">
        /// Количество измерений.
        /// </param>
        public void Write(double[,] data, int count)
        {
            Mutex.WaitOne();
            try
            {
                uint position = (uint)Position;
                for (int i = 0; i != count; ++i)
                {
                    for (int j = 0; j != Settings.CountSignals; ++j)
                    {
                        _Signals[j].Write(position * 8, data[j, i]);
                    }
                    position = (position + 1) % _SignalLength;
                }
                Position = (int)position;
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Выполняет чтение последних поступивших данных.
        /// </summary>
        /// <param name="count">
        /// Количество измерений.
        /// </param>
        /// <returns>
        /// Массив данных.
        /// </returns>
        public double[,] ReadLast(int count)
        {
            double[,] data = new double[Settings.CountSignals, count];
            Mutex.WaitOne();
            try
            {
                uint position = (uint)(Position + _SignalLength - count - 1) % _SignalLength;
                for (int i = 0; i != count; ++i)
                {
                    position = (position + 1) % _SignalLength;
                    for (int j = 0; j != Settings.CountSignals; ++j)
                    {
                        data[j, i] = _Signals[j].ReadDouble(position * 8);
                    }
                }
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
            return data;
        }

        /// <summary>
        /// Выполняет чтение последних поступивших данных.
        /// </summary>
        /// <param name="count">
        /// Количество измерений.
        /// </param>
        /// <param name="step">
        /// Шаг.
        /// </param>
        /// <returns>
        /// Массив данных.
        /// </returns>
        public double[,] ReadLast(int count, int step)
        {
            double[,] data = new double[Settings.CountSignals, count];
            Mutex.WaitOne();
            try
            {
                uint position = (uint)(Position + _SignalLength - (count + 1) * step) % _SignalLength;
                for (int i = 0; i != count; ++i)
                {
                    position = (position + (uint)step) % _SignalLength;
                    for (int j = 0; j != Settings.CountSignals; ++j)
                    {
                        data[j, i] = _Signals[j].ReadDouble(position * 8);
                    }
                }
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
            return data;
        }

        /// <summary>
        /// Выполняет чтение данных.
        /// </summary>
        /// <param name="position">
        /// Начальная позиция.
        /// </param>
        /// <param name="count">
        /// Количество значений.
        /// </param>
        /// <returns>
        /// Массив данных.
        /// </returns>
        public double[,] Read(int position, int count)
        {
            double[,] data = new double[Settings.CountSignals, count];
            for (int i = 0; i != count; ++i)
            {
                for (int j = 0; j != Settings.CountSignals; ++j)
                {
                    data[j, i] = _Signals[j].ReadDouble((position + i) * 8);
                }
            }
            return data;
        }
    }
}
