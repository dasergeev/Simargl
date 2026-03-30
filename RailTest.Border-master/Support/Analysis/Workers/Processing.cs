using RailTest.Fibers;
using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Выполняет обработку файлов.
    /// </summary>
    public class Processing : Worker
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="output">
        /// Средство вывода текстовой информации.
        /// </param>
        public Processing(Output output) :
            base(output)
        {

        }

        /// <summary>
        /// Возвращает текстовое представление.
        /// </summary>
        /// <returns>
        /// Текстовое представление.
        /// </returns>
        public override string ToString() => "Обработка";

        /// <summary>
        /// Выполняет работу.
        /// </summary>
        /// <param name="context">
        /// Контекст волокна.
        /// </param>
        protected override void Work(FiberContext context)
        {
            Output.Clear();
            Output.WriteLine(Color.DarkBlue, "Начало работы.");
            List<Peak> peaks = new List<Peak>();
            foreach (var file in new DirectoryInfo(PreparationPath).GetFiles())
            {
                Output.WriteLine($"Файл: {file.Name}");
                if (!context.IsWork) return;
                var frame = new Frame(file.FullName);
                Output.TabLevelUp();

                SectionGroupCollection sections = new SectionGroupCollection(frame, file.Name);
                frame.Channels.Clear();
                sections.Process();

                for (int i = 0; i != sections.Count; ++i)
                {
                    peaks.Add(sections[i].Left.Peaks[0]);
                    peaks.Add(sections[i].Left.Peaks[1]);
                    peaks.Add(sections[i].Right.Peaks[0]);
                    peaks.Add(sections[i].Right.Peaks[1]);
                }

                sections.Flush(frame);
                Output.TabLevelDown();
                frame.Save(file.FullName.Replace(PreparationPath, ProcessingPath));
            }
            Output.WriteLine(Color.DarkBlue, "Работа окончена.");
            Output.Clear();
            foreach (var peak in peaks)
            {
                string text = $"{peak.Rail.Section.Index + 1}";
                text += ($"\t{peak.Rail.Rail}");
                text += ($"\t{peak.Average}");
                text += ($"\t{peak.Time}");
                text += ($"\t{peak.Speed}");
                text += ($"\t{peak.Load}");
                var factors = peak.Factors;

                for (int i = 0; i < factors.Length; i++)
                {
                    text += ($"\t{factors[i].Real}");
                    text += ($"\t{factors[i].Imaginary}");
                }
                Output.WriteLine(text);
            }
        }
    }
}
