using RailTest.Fibers;
using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Выполняет предобработку файлов.
    /// </summary>
    public class Preparation : Worker
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="output">
        /// Средство вывода текстовой информации.
        /// </param>
        public Preparation(Output output) :
            base(output)
        {

        }

        /// <summary>
        /// Возвращает текстовое представление.
        /// </summary>
        /// <returns>
        /// Текстовое представление.
        /// </returns>
        public override string ToString() => "Предобработка";

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
            foreach (var file in new DirectoryInfo(SourcePath).GetFiles())
            {
                Output.WriteLine($"Файл: {file.Name}");
                if (!context.IsWork) return;
                var frame = new Frame(file.FullName);
                Output.TabLevelUp();
                var remove = new List<Channel>();
                foreach (var channel in frame.Channels)
                {
                    if (channel.Name.Contains("Counter") ||
                        channel.Name.Contains("FL") ||
                        channel.Name.Contains("FR") ||
                        channel.Name.Contains("ML") ||
                        channel.Name.Contains("MR"))
                    {
                        remove.Add(channel);
                    }
                    if (!context.IsWork) return;
                    Output.WriteLine($"Канал: {channel.Name}");
                    channel.SetZeroAtStart(5);
                }
                foreach (var channel in remove)
                {
                    frame.Channels.Remove(channel);
                }
                Output.TabLevelDown();
                frame.Save(file.FullName.Replace(SourcePath, PreparationPath));
            }
            Output.WriteLine(Color.DarkBlue, "Работа окончена.");
        }
    }
}
