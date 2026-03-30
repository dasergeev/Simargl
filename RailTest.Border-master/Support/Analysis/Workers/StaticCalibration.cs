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
    /// Выполняет анализ файлов статической калибровки.
    /// </summary>
    public class StaticCalibration : Worker
    {
        /// <summary>
        /// Постоянная, содержащая корневой путь к файлам.
        /// </summary>
        private const string _RootPath = @"\\railtest.ru\Data\06-НТО\03-Projects\Border\01 Файлы регистрации\2020-06-03 Статическая калибровка\";

        /// <summary>
        /// Постоянная, содержащая путь к исходным файлам.
        /// </summary>
        private const string _SourcePath = _RootPath + @"#01 Source\";

        /// <summary>
        /// Постоянная, содержащая путь к подготовленным файлам.
        /// </summary>
        private const string _PreparationPath = _RootPath + @"#02 Preparation\";

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="output">
        /// Средство вывода текстовой информации.
        /// </param>
        public StaticCalibration(Output output) :
            base(output)
        {

        }

        /// <summary>
        /// Возвращает текстовое представление.
        /// </summary>
        /// <returns>
        /// Текстовое представление.
        /// </returns>
        public override string ToString() => "Статическая калибровка";

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
            var files = new DirectoryInfo(_SourcePath).GetFiles();
            foreach (var file in files)
            {
                WorkBody(file);
            }

            //Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = 5 }, WorkBody);
            Output.WriteLine(Color.DarkBlue, "Завершение работы.");
        }

        /// <summary>
        /// Реализует тело рабочего потока.
        /// </summary>
        /// <param name="file">
        /// Файл для работы.
        /// </param>
        private void WorkBody(FileInfo file)
        {
            var output = Output.CreateSubOutput();
            output.Write(file.FullName.Replace(_SourcePath, ""));
            try
            {
                var frame = new Frame(file.FullName);
                output.WriteLine(" " + ((TestLabFrameHeader)frame.Header).Time.ToString());
                Channel[,] channels = new Channel[2, 21];
                for (int i = 0; i != 21; ++i)
                {
                    string section = (i + 1).ToString("00");
                    foreach (string rail in new string[] { "L", "R" })
                    {
                        Channel external = frame.Channels[$"S{rail}e{section}_0"];
                        Channel @internal = frame.Channels[$"S{rail}i{section}_0"];

                        external.Name = $"PC{rail}{section}";

                        int length = external.Length;
                        for (int j = 0; j != length; ++j)
                        {
                            external[j] -= @internal[j];
                        }
                        if (rail == "L")
                        {
                            channels[0, i] = external;
                        }
                        else
                        {
                            external.Scale(-1);
                            channels[1, i] = external;
                        }

                        //external.FourierFiltering(-1, 20);
                        //external.Resampling(50);
                    }
                }
                frame.Channels.Clear();
                for (int i = 0; i != 21; ++i)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        frame.Channels.Add(channels[j, i]);
                    }
                }

                frame.Save(file.FullName.Replace(_SourcePath, _PreparationPath));
            }
            catch (Exception ex)
            {
                output.WriteLine(Color.DarkRed, "Произошло исключение:");
                output.TabLevelUp();
                output.WriteLine(ex.ToString());
                output.TabLevelDown();
            }
            output.Flush();
        }
    }
}
