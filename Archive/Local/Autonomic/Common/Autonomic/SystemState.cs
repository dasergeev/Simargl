using RailTest.Memory;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет состояние системы.
    /// </summary>
    public class SystemState
    {
        /// <summary>
        /// Постоянная для хранения положения состояния устройства QuantumX.
        /// </summary>
        private const long _QuantumXStatePosition = 0 * 8;

        /// <summary>
        /// Постоянная для хранения положения скорости.
        /// </summary>
        private const long _SpeedPosition = 1 * 8;

        /// <summary>
        /// Постоянная для хранения положения долготы.
        /// </summary>
        private const long _LongitudePosition = 2 * 8;

        /// <summary>
        /// Постоянная для хранения положения широты.
        /// </summary>
        private const long _LatitudePosition = 3 * 8;

        /// <summary>
        /// Постоянная для хранения размера памяти.
        /// </summary>
        private const long _FullSize = 4 * 8;

        /// <summary>
        /// Поле для хранения общей памяти.
        /// </summary>
        private readonly SharedMemory _Memory;

        /// <summary>
        /// Поле для хранения объекта доступа к памяти.
        /// </summary>
        private readonly MemoryMappedViewAccessor _View;

        /// <summary>
        /// Инициализиирует новый экземпляр класса.
        /// </summary>
        public SystemState()
        {
            _Memory = new SharedMemory("SystemState", _FullSize);
            _View = _Memory.CreateViewAccessor(0, _FullSize);
            Mutex = _Memory.Mutex;
        }

        /// <summary>
        /// Возвращает примитив межпроцессорной синхронизации.
        /// </summary>
        public Mutex Mutex { get; }

        /// <summary>
        /// Возвращает или задаёт состояние устройства QuantumX.
        /// </summary>
        public QuantumXState QuantumXState
        {
            get
            {
                return (QuantumXState)_View.ReadInt64(_QuantumXStatePosition);
            }
            set
            {
                Mutex.WaitOne();
                try
                {
                    _View.Write(_QuantumXStatePosition, (long)value);
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Возвращает или задаёт скорость движения.
        /// </summary>
        public double Speed
        {
            get
            {
                return _View.ReadDouble(_SpeedPosition);
            }
            set
            {
                Mutex.WaitOne();
                try
                {
                    _View.Write(_SpeedPosition, value);
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Возвращает или задаёт долготу.
        /// </summary>
        public double Longitude
        {
            get
            {
                return _View.ReadDouble(_LongitudePosition);
            }
            set
            {
                Mutex.WaitOne();
                try
                {
                    _View.Write(_LongitudePosition, value);
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Возвращает или задаёт широту.
        /// </summary>
        public double Latitude
        {
            get
            {
                return _View.ReadDouble(_LatitudePosition);
            }
            set
            {
                Mutex.WaitOne();
                try
                {
                    _View.Write(_LatitudePosition, value);
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
        }
    }
}
