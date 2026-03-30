using RailTest.Frames;
using System.Collections.Generic;
using System.Drawing;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Предоставляет параметры.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Постоянная для хранения IP адреса тельтоники.
        /// </summary>
        public const string TeltonikaIPAddress = "192.168.1.1";

        /// <summary>
        /// Постоянная для хранения IP адреса регистратора.
        /// </summary>
        public const string RegistrarIPAddress = "192.168.1.3";

        /// <summary>
        /// Постоянная для хранения IP адреса устройства QuantumX.
        /// </summary>  //                                                    1615 ЦБ           1615 Доп        840 Доп
        //public static readonly string[] QuantumXIPAddresses = new string[] { "192.168.1.131", "192.168.1.103", "192.168.1.128" };
        //public static readonly string[] QuantumXIPAddresses = new string[] { "192.168.1.131", "192.168.1.108" };
        public static readonly string[] QuantumXIPAddresses = new string[] { "192.168.1.131" };

        /// <summary>
        /// Постоянная для хранения количетсва сигналов.
        /// </summary>
        public const int CountSignals = 16;

        /// <summary>
        /// Поле для хранения списка имён каналов.
        /// </summary>
        //public static readonly IReadOnlyList<string> ChannelNames = new List<string>()
        //{
        //    "UX1", "UY1", "UZ1", "UX2", "UY2", "UZ2", "UYb1", "UYb2",
        //    "UYl1", "UYg1", "MHR1", "MHR2", "MKDR1", "MKDR2", "P1", "P2",
        //    "MHR01", "MHR02", "MKDR01", "MKDR02", "S1", "S2", "S3", "S4",
        //    "S5", "S6", "S7", "S8", "S9", "S10", "S11", "S12",
        //    "X1z", "X2z", "X3y", "X4y", "X5v", "X6l", "МК1", "МК2"
        //};
        //public static readonly IReadOnlyList<string> ChannelNames = new List<string>()
        //{
        //    "UXB1", "UYB1", "UZB1", "UXK1",
        //    "UYK1", "UZK1", "UXK2", "UYK2",
        //    "UZK2", "UXR", "UYR", "UZR",
        //    "Nk1", "Nk2", "CB_CH_15", "CB_CH_16",
        //    "Mo", "Mr", "Mir1", "Mir2",
        //    "KMT_CH_05", "KMT_CH_06", "KMT_CH_07", "KMT_CH_08",
        //    "KMT_CH_09", "KMT_CH_10", "KMT_CH_11", "KMT_CH_12",
        //    "KMT_CH_13", "KMT_CH_14", "KMT_CH_15", "KMT_CH_16",
        //};
        public static readonly IReadOnlyList<string> ChannelNames = new List<string>()
        {
            "P_23", "S5_23", "S6_23", "S7_23", "S8_23", "P_34", "S5_34", "S6_34",
            "S7_34", "S8_34", "X", "Y", "Z", "UX", "UY", "UZ"
        };

        /// <summary>
        /// Поле для хранения списка размерностей.
        /// </summary>
        public static readonly IReadOnlyList<string> ChannelUnits = new List<string>()
        {
            " ", " ", " ", " ", " ", " ", " ", " ",
            " ", " ", " ", " ", " ", " ", " ", " "
        };

        /// <summary>
        /// Постоянная для хранения порта для получения сообщений GPS.
        /// </summary>
        public const int GpsPort = 8500;

        /// <summary>
        /// Постоянная для хранения частоты дискретизации.
        /// </summary>
        public const int Sampling = 1200;

        /// <summary>
        /// Постоянная для хранения длительности записи.
        /// </summary>
        public const int Duration = 60;

        /// <summary>
        /// Постоянная для хранения времени в милисекундах, в течении которого ожидаются данные от QuantumX без сброса.
        /// </summary>
        public const int QuantumXTimeout = 2000;

        /// <summary>
        /// Постоянная для хранения периода в милисекундах опроса устройства QuantumX.
        /// </summary>
        public const int QuantumXSurveyPeriod = 250;

        /// <summary>
        /// Постоянная для хранения каталога с исполняемыми файлами на разведчике.
        /// </summary>
        public const string RegistrarNetBinary = @"\\Autonomic\Programs\Release\Binary\";

        /// <summary>
        /// Постоянная для хранения временного каталога разведчика.
        /// </summary>
        public const string RegistrarTemporal = @"C:\OneDrive\Autonomic\Oriole\";

        /// <summary>
        /// Поле для хранения списка стандартных цветов.
        /// </summary>
        public static readonly IReadOnlyList<Color> Colors = new List<Color>()
        {
            Color.FromArgb(65, 140, 240),
            Color.FromArgb(252, 180, 65),
            Color.FromArgb(224, 64, 10),
            Color.FromArgb(5, 100, 146),
            Color.FromArgb(191, 191, 191),
            Color.FromArgb(26, 59, 105),
            Color.FromArgb(255, 227, 130),
            Color.FromArgb(18, 156, 221),
            Color.FromArgb(202, 107, 75),
            Color.FromArgb(0, 92, 219),
            Color.FromArgb(243, 210, 136),
            Color.FromArgb(80, 99, 129),
            Color.FromArgb(241, 185, 168),
            Color.FromArgb(224, 131, 10),
            Color.FromArgb(120, 147, 190),
            Color.FromArgb(65, 140, 240),
            Color.FromArgb(65, 140, 240),
            Color.FromArgb(252, 180, 65),
            Color.FromArgb(224, 64, 10),
            Color.FromArgb(5, 100, 146),
            Color.FromArgb(191, 191, 191),
            Color.FromArgb(26, 59, 105),
            Color.FromArgb(255, 227, 130),
            Color.FromArgb(18, 156, 221),
            Color.FromArgb(202, 107, 75),
            Color.FromArgb(0, 92, 219),
            Color.FromArgb(243, 210, 136),
            Color.FromArgb(80, 99, 129),
            Color.FromArgb(241, 185, 168),
            Color.FromArgb(224, 131, 10),
            Color.FromArgb(120, 147, 190),
            Color.FromArgb(65, 140, 240),
            Color.FromArgb(65, 140, 240),
            Color.FromArgb(252, 180, 65),
            Color.FromArgb(224, 64, 10),
            Color.FromArgb(5, 100, 146),
            Color.FromArgb(191, 191, 191),
            Color.FromArgb(26, 59, 105),
            Color.FromArgb(255, 227, 130),
            Color.FromArgb(18, 156, 221),
            Color.FromArgb(202, 107, 75),
            Color.FromArgb(0, 92, 219),
            Color.FromArgb(243, 210, 136),
            Color.FromArgb(80, 99, 129),
            Color.FromArgb(241, 185, 168),
            Color.FromArgb(224, 131, 10),
            Color.FromArgb(120, 147, 190),
            Color.FromArgb(65, 140, 240),
        };
    }
}
