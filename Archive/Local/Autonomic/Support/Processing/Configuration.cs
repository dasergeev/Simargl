using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing
{
    /// <summary>
    /// Предоставляет настройки.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Максимальное количество потоков.
        /// </summary>
        public static int MaxDegreeOfParallelism = 32;

        /// <summary>
        /// Возвращает путь к исходным файлам.
        /// </summary>
        public static string SourcePath = @"\\railtest.ru\Data\06-НТО\03-Projects\004 Иволга - 2\001 Файлы регистрации";

        /// <summary>
        /// Возвращает путь к файлам с восстановленными силами.
        /// </summary>
        public static string RestorePath = @"\\railtest.ru\Data\06-НТО\03-Projects\004 Иволга - 2\003 Восстановление сил";

        /// <summary>
        /// Возвращает путь к файлу трассировки.
        /// </summary>
        public static string TraceFilePath = @"E:\Output.txt";

        /// <summary>
        /// Возвращает минимальную скорость движения.
        /// </summary>
        public static double MinSpeed = 0.5;

        /// <summary>
        /// Возвращает минимальную долготу.
        /// </summary>
        public static double MinLongitude = 20;

        /// <summary>
        /// Возвращает минимальную широту.
        /// </summary>
        public static double MinLatitude = 50;

        /// <summary>
        /// Постоянная, определяющая требуется использовать фильтрацию.
        /// </summary>
        public const bool IsFilter = true;

        /// <summary>
        /// Постоянная, определяющая частоту фильтрации.
        /// </summary>
        public const double FilteringFrequency = 5;   //  200;    40;     5;

        /// <summary>
        /// Возвращает список каналов, которые необходимо исключить из обработки.
        /// </summary>
        public static List<string> RemovedFiles = new List<string>
        {
            "Vp47_7 26.09.2020 17_34_37.0001",
            "Vp13_9 25.09.2020 18_34_20.0001",
            "Vp61_4 27.09.2020 17_50_23.0001",
            "Vp27_1 28.09.2020 5_51_46.0001",
            "Vp40_9 27.09.2020 10_33_52.0001",
        };

        /// <summary>
        /// Возвращает коллекцию имён исходных каналов.
        /// </summary>
        public static string[] ChannelNames =
        {
            "UX1", "UY1", "UZ1", "UX2",
            "UY2", "UZ2", "UYb1", "UYb2",
            "UYl1", "UYg1", "MHR1", "MHR2",
            "MKDR1", "MKDR2", "P1", "P2",
            "MHR01", "MHR02", "MKDR01", "MKDR02",
            "S1", "S2", "S3", "S4",
            "S5", "S6", "S7", "S8",
            "S9", "S10", "S11", "S12",
            "X1z", "X2z", "X3y", "X4y",
            "X5v", "X6l", "МК1"
        };
    }
}
