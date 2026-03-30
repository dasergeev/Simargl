using System;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Предоставляет настройки приложения.
    /// </summary>
    public static class Customization
    {
        /// <summary>
        /// Возвращает исходный путь.
        /// </summary>
        public static string SourcePath { get; } = @"E:\OneDrive\Autonomic\Oriole";

        /// <summary>
        /// Возвращает путь назначения.
        /// </summary>
        public static string DestinationPath { get; } = @"\\Snickers.railtest.ru\F\Oriole";

        /// <summary>
        /// Возвращает тайм-аут переноса.
        /// </summary>
        public static TimeSpan TransferTimeout { get; } = new TimeSpan(0, 5, 0);
    }
}
