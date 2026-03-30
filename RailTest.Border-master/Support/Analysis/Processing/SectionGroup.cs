using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Представляет группу каналов, расположенных в одном сечении.
    /// </summary>
    public class SectionGroup
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="index">
        /// Номер сечения.
        /// </param>
        /// <param name="position">
        /// Положение сечения.
        /// </param>
        /// <param name="frame">
        /// Кадр регистрации.
        /// </param>
        public SectionGroup(int index, double position, Frame frame)
        {
            Index = index;
            Position = position;
            Label = (index + 1).ToString("00");
            Left = new RailGroup(this, Rail.Left, frame);
            Right = new RailGroup(this, Rail.Right, frame);
        }

        /// <summary>
        /// Возвращает индекс сечения.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Возвращает положение сечения.
        /// </summary>
        public double Position { get; }

        /// <summary>
        /// Возвращает текстовую метку сечения.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Возвращает группу каналов левого рельса.
        /// </summary>
        public RailGroup Left { get; }

        /// <summary>
        /// Возвращает группу каналов правого рельса.
        /// </summary>
        public RailGroup Right { get; }

        /// <summary>
        /// Выполняет сброс каналов в кадр.
        /// </summary>
        /// <param name="frame">
        /// Кадр, в который необходимо поместить каналы.
        /// </param>
        public void Flush(Frame frame)
        {
            Left.Flush(frame);
            Right.Flush(frame);
        }

        /// <summary>
        /// Выполняет локализацию пиков.
        /// </summary>
        /// <param name="level">
        /// Уровень чувствительности.
        /// </param>
        public void Localization(double level)
        {
            Left.Localization(level);
            Right.Localization(level);
        }
    }
}
