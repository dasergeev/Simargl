using System.Collections.Generic;

namespace Simargl.Platform.QuantumX
{
    /// <summary>
    /// Представляет конфигурацию каналов для записи в кадры <see cref="QuantumXParser"/>
    /// </summary>
    public  class QuantumParserOptions
    {

        /// <summary>
        /// Устанавливает и возвращает массив конфигураций каналов.
        /// </summary>
        public List<ChannelOptions> Channels { get; set; } = new();

        /// <summary>
        /// Устанавливает и возвращает путь сохранения кадра.
        /// </summary>
        public string SavePath { get; set; } = string.Empty;
    }
}
