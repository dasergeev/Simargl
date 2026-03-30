namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет значение, определяющее мнемонику сообщения NMEA протокола.
    /// </summary>
    enum NmeaMnemonics
    {
        /// <summary>
        /// Данные местоположения.
        /// </summary>
        Gga,

        /// <summary>
        /// Географические координаты.
        /// </summary>
        Ggl,

        /// <summary>
        /// Данные метоположения GNSS.
        /// </summary>
        Gns,

        /// <summary>
        /// Геометрический фактор ухудшения точности и активные спутники.
        /// </summary>
        Gsa,

        /// <summary>
        /// Видимые спутники.
        /// </summary>
        Gsv,

        /// <summary>
        /// Минимальный рекомендованный набор данных.
        /// </summary>
        Rmc,

        /// <summary>
        /// Скорость и курс относительно земли.
        /// </summary>
        Vtg,

        /// <summary>
        /// Время и дата.
        /// </summary>
        Zda,

        /// <summary>
        /// Система координат.
        /// </summary>
        Dtm,

        /// <summary>
        /// Сообщение RLM GALILEO.
        /// </summary>
        Rlm,
    }
}
