using RailTest.Algebra;
using System.Collections.Generic;

namespace RailTest.Border
{
    /// <summary>
    /// Представляет группу сигналов в одном сечении.
    /// </summary>
    public class SectionGroup
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="section">
        /// Номер сечения.
        /// </param>
        internal SectionGroup(int section)
        {
            Section = section;
            Counter = new Signal("Counter" + section.ToString("00"), "", true);
            Left = new RailGroup(section, Rail.Left);
            Right = new RailGroup(section, Rail.Right);

            Signals = new List<Signal>
            {
                Counter
            };
            Signals.AddRange(Left.Signals);
            Signals.AddRange(Right.Signals);
        }

        /// <summary>
        /// Возвращает номер сечения.
        /// </summary>
        public int Section { get; }

        /// <summary>
        /// Возвращает сигнал счётчика осей.
        /// </summary>
        public Signal Counter { get; }

        /// <summary>
        /// Возвращает групу сигналов на левом рельсе.
        /// </summary>
        public RailGroup Left { get; }

        /// <summary>
        /// Возвращает групу сигналов на правом рельсе.
        /// </summary>
        public RailGroup Right { get; }

        /// <summary>
        /// Возвращает все сигналы.
        /// </summary>
        public List<Signal> Signals { get; }

        /// <summary>
        /// Возвращает положение сечения.
        /// </summary>
        public double Position { get; set; }

        /// <summary>
        /// Устанавливает ноль.
        /// </summary>
        internal void Zero()
        {
            Left.Zero();
            Right.Zero();
        }

        /// <summary>
        /// Обновляет данные.
        /// </summary>
        /// <param name="blockIndex">
        /// Индекс чтения.
        /// </param>
        internal unsafe void Update(int blockIndex)
        {
            Left.Update(blockIndex);
            Right.Update(blockIndex);

            RealVector left = Left.Continuous.LastData;
            RealVector right = Right.Continuous.LastData;
            RealVector result = new RealVector(SectionGroupCollection.BlockSize);

            if (left.Average + right.Average >= 6)
            {
                result.Move(1);
            }

            Counter.Write(blockIndex, (double*)result.Pointer.ToPointer());
        }
    }
}
