using RailTest.Memory;
using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет буфер местоположений.
    /// </summary>
    public class LocationBuffer
    {
        /// <summary>
        /// Постоянная для хранения размера заголовка.
        /// </summary>
        private const int _HeaderSize = 16;

        /// <summary>
        /// Постоянная для хранения длины сигнала.
        /// </summary>
        private const uint _SignalLength = Settings.Duration * 2;

        /// <summary>
        /// Постоянная для хранения размера сигнала.
        /// </summary>
        private const uint _SignalSize = _SignalLength * 8;

        /// <summary>
        /// Постоянная для хранения размера памяти.
        /// </summary>
        private const uint _FullSize = _HeaderSize + 3 * _SignalSize;

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
        /// Инициализиирует новый экземпляр класса.
        /// </summary>
        public LocationBuffer()
        {
            _Memory = new SharedMemory("Location", _FullSize);
            _Header = _Memory.CreateViewAccessor(0, _HeaderSize);
            Mutex = _Memory.Mutex;
            _Signals = new MemoryMappedViewAccessor[3];
            for (int i = 0; i != 3; ++i)
            {
                _Signals[i] = _Memory.CreateViewAccessor(_HeaderSize + i * _SignalSize, _SignalSize);
            }
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

                for (int i = 0; i != 3; ++i)
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
        /// <param name="speed">
        /// Скорость.
        /// </param>
        /// <param name="longitude">
        /// Долгота.
        /// </param>
        /// <param name="latitude">
        /// Широта.
        /// </param>
        public void Write(double speed, double longitude, double latitude)
        {
            uint position = (uint)Position;
            _Signals[0].Write(position * 8, speed);
            _Signals[1].Write(position * 8, longitude);
            _Signals[2].Write(position * 8, latitude);
            Position = (position + 1) % _SignalLength;
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
                    position = (position + 1) % _SignalLength;
                    for (int j = 0; j != 3; ++j)
                    {
                        _Signals[j].Write(position * 8, data[j, i]);
                    }
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
            double[,] data = new double[3, count];
            Mutex.WaitOne();
            try
            {
                uint position = (uint)(Position + _SignalLength - count) % _SignalLength;
                for (int i = 0; i != count; ++i)
                {
                    for (int j = 0; j != 3; ++j)
                    {
                        data[j, i] = _Signals[j].ReadDouble(position * 8);
                    }
                    position = (position + 1) % _SignalLength;
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
            double[,] data = new double[3, count];
            for (int i = 0; i != count; ++i)
            {
                for (int j = 0; j != 3; ++j)
                {
                    data[j, i] = _Signals[j].ReadDouble((position + i) * 8);
                }
            }
            return data;
        }
    }
}
