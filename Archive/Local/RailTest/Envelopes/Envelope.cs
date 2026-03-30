using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Envelopes
{
    /// <summary>
    /// Представляет базовый класс для всех объектов, реализующих оболочку вокруг объекта.
    /// </summary>
    public class Envelope : Ancestor
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="target">
        /// Целевой объект.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="target"/> передана пустая ссылка.
        /// </exception>
        public Envelope(object target)
        {
            Target = target ?? throw new ArgumentNullException("target", "Передана пустая ссылка.");
        }

        /// <summary>
        /// Возвращает целевой объект.
        /// </summary>
        public object Target { get; }
    }

    /// <summary>
    /// Представляет универсальную оболочку вокруг объекта.
    /// </summary>
    /// <typeparam name="T">
    /// Тип объекта, вокруг которого создаётся оболочка.
    /// </typeparam>
    public class Envelope<T> : Envelope
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="target">
        /// Целевой объект.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="target"/> передана пустая ссылка.
        /// </exception>
        public Envelope(T target) :
            base(target)
        {

        }

        /// <summary>
        /// Возвращает целевой объект.
        /// </summary>
        public new T Target => (T)base.Target;
    }
}
