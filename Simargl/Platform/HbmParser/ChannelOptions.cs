namespace Simargl.Platform.QuantumX
{
    /// <summary>
    /// Представляет конфигурацию канала для <see cref="QuantumParserOptions"/>
    /// </summary>
    public class ChannelOptions
    {

        /// <summary>
        /// Устанавливает и возврашает имя канала.
        /// </summary>
        public string Name { get; set; } = "Default";

        /// <summary>
        /// Устанавливает и возврашает ед. измерения
        /// </summary>
        public string Unit { get; set; } = "Default";

        /// <summary>
        /// Устанавливает и возвращает частоту дискретизации.
        /// </summary>
        public double Sampling { get; set; }

        /// <summary>
        /// Устанавливает и возвращает частоту среза
        /// </summary>
        public double Cutoff { get; set; }
    }
}
