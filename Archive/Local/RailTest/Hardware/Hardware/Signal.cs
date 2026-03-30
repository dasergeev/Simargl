using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Hardware
{
    /// <summary>
    /// Представляет базовый класс для всех сигналов.
    /// </summary>
    public abstract class Signal : Ancestor
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя сигнала.
        /// </param>
        public Signal(string name)
        {
            Name = name ?? "";
            Index = -1;
        }

        /// <summary>
        /// Возвращает имя сигнала.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает индекс сигнала.
        /// </summary>
        public int Index { get; internal set; }
    }
}
