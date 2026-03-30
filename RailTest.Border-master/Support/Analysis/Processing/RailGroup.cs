using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Представляет группу каналов, расположенных на одном рельсе.
    /// </summary>
    public class RailGroup
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="section">
        /// Сечение, в котором расположена группа.
        /// </param>
        /// <param name="rail">
        /// Значение, определяющее рельс.
        /// </param>
        /// <param name="frame">
        /// Кадр регистрации.
        /// </param>
        public RailGroup(SectionGroup section, Rail rail, Frame frame)
        {
            Section = section;
            Rail = rail;
            Label = rail == Rail.Left ? "L" : "R";
            External = new SideGroup(this, Side.External, frame);
            Internal = new SideGroup(this, Side.Internal, frame);
            Continuous = frame.Channels["PC" + Label + section.Label];
            Standard = frame.Channels["P" + Label + section.Label];
            Peaks = new Peak[2];
        }

        /// <summary>
        /// Возвращает сечение, в котором расположена группа.
        /// </summary>
        public SectionGroup Section { get; }

        /// <summary>
        /// Возвращает значение, определяющее рельс.
        /// </summary>
        public Rail Rail { get; }

        /// <summary>
        /// Возвращает текстовую метку рельса.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Группа каналов, расположенная на внешней стороне.
        /// </summary>
        public SideGroup External { get; }

        /// <summary>
        /// Группа каналов, расположенная на внутренней стороне.
        /// </summary>
        public SideGroup Internal { get; }

        /// <summary>
        /// Возвращает канал непрерывной вертикальной силы.
        /// </summary>
        public Channel Continuous { get; }

        /// <summary>
        /// Возвращает канал стандартной вертикальной силы.
        /// </summary>
        public Channel Standard { get; }

        /// <summary>
        /// Возвращает пики.
        /// </summary>
        public Peak[] Peaks { get; }

        /// <summary>
        /// Выполняет сброс каналов в кадр.
        /// </summary>
        /// <param name="frame">
        /// Кадр, в который необходимо поместить каналы.
        /// </param>
        public void Flush(Frame frame)
        {
            frame.Channels.Add(Continuous);
            frame.Channels.Add(Standard);
            //External.Flush(frame);
            //Internal.Flush(frame);
        }

        /// <summary>
        /// Выполняет локализацию пиков.
        /// </summary>
        /// <param name="level">
        /// Уровень чувствительности.
        /// </param>
        public void Localization(double level)
        {
            if (Rail == Rail.Left)
            {
                Continuous.Vector = External.First.Vector - Internal.First.Vector;
            }
            else
            {
                Continuous.Vector = Internal.First.Vector - External.First.Vector;
            }
            Standard.Vector = Continuous.Vector.Clone();
            Standard.FourierFiltering(-1, 10);

            List<int> peaks = new List<int>();
            unsafe
            {
                int length = Standard.Length;
                double* data = (double*)Standard.Vector.Pointer.ToPointer();
                for (int i = 0; i != length; ++i)
                {
                    if (data[i] < level)
                    {
                        data[i] = 0;
                    }
                    else
                    {
                        data[i] = 1;
                    }
                }

                bool inPeak = false;
                int begin = 0;
                int end = 0;
                for (int i = 0; i != length; ++i)
                {
                    double value = data[i];
                    if (value < 0.5)
                    {
                        if (inPeak)
                        {
                            //  Конец пика.
                            end = i - 1;
                            peaks.Add((begin + end) >> 1);
                            inPeak = false;
                        }
                    }
                    else
                    {
                        if (!inPeak)
                        {
                            //  Начало пика.
                            begin = i;
                            inPeak = true;
                        }
                    }
                    data[i] = 0;
                }
            }

            for (int i = 0; i < peaks.Count; i++)
            {
                Peaks[i] = new Peak(peaks[i], this);
            }

            foreach (var peak in peaks)
            {
                Standard[peak] = Continuous[peak];
            }
        }
    }
}
