using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing
{
    class Program
    {
        /// <summary>
        /// Точка входа приложения.
        /// </summary>
        static void Main()
        {
            ////  Восстановление сил.
            //Performer performer = new RestoreForces();

            ////  Экспресс анализ.
            Performer performer = new ExpressAnalysis(@"\\Snickers\E\03-Projects\004 Иволга - 2\004 Восстановление сил - новое");

            //  Выполнение работы.
            performer.Invoke();




            ////  Переименование файлов.
            //string path = @"\\railtest.ru\Data\06-НТО\03-Projects\004 Иволга - 2\003 Восстановление сил";
            //foreach (var file in new DirectoryInfo(path).GetFiles())
            //{
            //    var sourceFileName = file.Name;
            //    var index = sourceFileName.IndexOf('.');
            //    var targetFileName = sourceFileName.Substring(0, index) + "_"
            //        + sourceFileName.Substring(index + 1, sourceFileName.Length - index - 6);
            //    Console.WriteLine($"{sourceFileName} -> {targetFileName}");
            //    File.Move(Path.Combine(path, sourceFileName), Path.Combine(path, targetFileName));
            //}

        }
    }
}
