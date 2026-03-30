using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RailTest.Satellite.Autonomic.Telemetry
{
    /// <summary>
    /// Представляет коллекцию значений.
    /// </summary>
    public sealed class PackageValueCollection : IReadOnlyList<double>
    {
        /// <summary>
        /// Постоянная для хранения количества значений.
        /// </summary>
        private const int _Count = 10;

        /// <summary>
        /// Поле для хранения массива значений.
        /// </summary>
        private readonly double[] _Items;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal PackageValueCollection()
        {
            _Items = new double[_Count];
        }

        /// <summary>
        /// Возвращает или задаёт значение по указанному индексу.
        /// </summary>
        /// <param name="index">
        /// Индекс значения.
        /// </param>
        /// <returns>
        /// Значение.
        /// </returns>
        public double this[int index]
        {
            get
            {
                return _Items[index];
            }
            set
            {
                _Items[index] = value;
            }
        }

        /// <summary>
        /// Возвращает количество значений в коллекции.
        /// </summary>
        public int Count => _Count;

        /// <summary>
        /// Выполняет чтение данных.
        /// </summary>
        /// <param name="reader">
        /// Средство чтения данных.
        /// </param>
        internal void Read(BinaryReader reader)
        {
            for (int i = 0; i != _Count; ++i)
            {
                _Items[i] = reader.ReadSingle();
            }
        }

        /// <summary>
        /// Выполняет запись данных.
        /// </summary>
        /// <param name="writer">
        /// Средство записи данных.
        /// </param>
        internal void Write(BinaryWriter writer)
        {
            for (int i = 0; i != _Count; ++i)
            {
                writer.Write((float)_Items[i]);
            }
        }

        /// <summary>
        /// Возвращает среднее значение.
        /// </summary>
        public double Average => _Items.Average();

        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель коллекции.
        /// </returns>
        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)_Items).GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }
    }
}
