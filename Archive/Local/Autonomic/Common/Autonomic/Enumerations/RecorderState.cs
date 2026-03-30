namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет значение, определяющее состояние системы сохранения файлов.
    /// </summary>
    public enum RecorderState
    {
        /// <summary>
        /// Ожидание.
        /// </summary>
        Waiting,

        /// <summary>
        /// Запись первой части.
        /// </summary>
        First,

        /// <summary>
        /// Запись второй части.
        /// </summary>
        Second,
    }
}
