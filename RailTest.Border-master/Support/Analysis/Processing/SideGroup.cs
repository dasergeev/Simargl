using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Представляет группу каналов, находящихся на одной стороне рельса.
    /// </summary>
    public class SideGroup
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="rail">
        /// Рельс.
        /// </param>
        /// <param name="side">
        /// Значение, определяющее сторону рельса.
        /// </param>
        /// <param name="frame">
        /// Кадр регистрации.
        /// </param>
        public SideGroup(RailGroup rail, Side side, Frame frame)
        {
            Rail = rail;
            Side = side;
            Label = side == Side.External ? "e" : "i";
            First = frame.Channels["S" + rail.Label + Label + rail.Section.Label + "_0"];
            Second = frame.Channels["S" + rail.Label + Label + rail.Section.Label + "_1"];
            Third = frame.Channels["S" + rail.Label + Label + rail.Section.Label + "_2"];
        }

        /// <summary>
        /// Возвращает рельс.
        /// </summary>
        public RailGroup Rail { get; }

        /// <summary>
        /// Возвращает значение, определяющее сторону рельса.
        /// </summary>
        public Side Side { get; }

        /// <summary>
        /// Возвращает текстовую метку рельса.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Возвращает первый канал.
        /// </summary>
        public Channel First { get; }

        /// <summary>
        /// Возвращает второй канал.
        /// </summary>
        public Channel Second { get; }

        /// <summary>
        /// Возвращает третий канал.
        /// </summary>
        public Channel Third { get; }

        /// <summary>
        /// Выполняет сброс каналов в кадр.
        /// </summary>
        /// <param name="frame">
        /// Кадр, в который необходимо поместить каналы.
        /// </param>
        public void Flush(Frame frame)
        {
            frame.Channels.Add(First);
            frame.Channels.Add(Second);
            frame.Channels.Add(Third);
        }
    }
}
