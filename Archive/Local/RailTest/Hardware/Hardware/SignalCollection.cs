using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Hardware
{
    /// <summary>
    /// Представляет базовый класс для всех коллекций сигналов.
    /// </summary>
    public abstract class SignalCollection : Ancestor, IEnumerable<Signal>
    {
        /// <summary>
        /// Поле для хранения списка сигналов.
        /// </summary>
        private readonly List<Signal> _Items;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public SignalCollection()
        {
            _Items = new List<Signal>();
        }

        /// <summary>
        /// Возвращает количество элементов в коллекции.
        /// </summary>
        public int Count => _Items.Count;

        /// <summary>
        /// Возвращает сигнал с указанным индексом.
        /// </summary>
        /// <param name="index">
        /// Индекс сигнала.
        /// </param>
        /// <returns>
        /// Сигнал с указанным индексом.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Значение параметра <paramref name="index"/> меньше 0.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Значение параметра <paramref name="index"/> больше или равно значению свойства <see cref="Count"/>.
        /// </exception>
        public Signal this [int index]
        {
            get
            {
                lock (_Items)
                {
                    return _Items[index];
                }
            }
        }

        /// <summary>
        /// Возвращает сигнал с указанным именем.
        /// </summary>
        /// <param name="name">
        /// Имя сигнала.
        /// </param>
        /// <returns>
        /// Сигнал с указанным именем.
        /// </returns>
        public Signal this[string name]
        {
            get
            {
                lock (_Items)
                {
                    foreach (Signal signal in _Items)
                    {
                        if (signal.Name == name)
                        {
                            return signal;
                        }
                    }
                    throw new ArgumentOutOfRangeException("name", "Сигнал с указанным именем не содержится в коллекции.");
                }
            }
        }

        /// <summary>
        /// Добавляет новый сигнал в коллекцию.
        /// </summary>
        /// <param name="signal">
        /// Сигнал, добавляемый в коллекцию.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="signal"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Сигнал уже содержится в коллекции.
        /// </exception>
        protected void Add(Signal signal)
        {
            lock (_Items)
            {
                if (signal is null)
                {
                    throw new ArgumentNullException("signal", "Передана пустая ссылка.");
                }
                if (_Items.Contains(signal))
                {
                    throw new InvalidOperationException("Сигнал уже содержится в коллекции.");
                }
                signal.Index = _Items.Count;
                _Items.Add(signal);
            }
        }

        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель коллекции.
        /// </returns>
        public IEnumerator<Signal> GetEnumerator()
        {
            return ((IEnumerable<Signal>)_Items).GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Signal>)_Items).GetEnumerator();
        }
    }
}
