using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Frames
{
    /// <summary>
    /// Представляет точку канала.
    /// </summary>
    public struct ChannelPoint
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="index">
        /// Индекс точки.
        /// </param>
        /// <param name="value">
        /// Значение точки.
        /// </param>
        public ChannelPoint(int index, double value)
        {
            Index = index;
            Value = value;
        }

        /// <summary>
        /// Возвращает или задаёт индекс точки.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Возвращает или задаёт значение точки.
        /// </summary>
        public double Value { get; set; }
    }
}
