namespace PointerDataViewer
{
    /// <summary>
    /// Представляет класс настроек программы.
    /// </summary>
    public class Options
    {

        /// <summary>
        /// Постоянная, определяющая номер порта для подключения датчиков тензо.
        /// </summary>
        public int TensoStreamPort { get; set; }

        /// <summary>
        /// Возвращает и устанавливает список серийных номеров тензо датчиков
        /// </summary>
        public string[]? TensoSerialNumber { get; set; }

    }
}

