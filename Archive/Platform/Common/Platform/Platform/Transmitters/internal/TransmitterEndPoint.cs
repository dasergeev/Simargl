namespace Apeiron.Platform.Transmitters;

/// <summary>
/// Представляет класс, определеяющий сетевые параметры подключения.
/// </summary>
public class TransmitterEndPoint
{
    /// <summary>
    /// Возвращает и устанавливает IP строку.
    /// </summary>
    public string? IP { get; set; }

    /// <summary>
    /// Возвращает и устанавливает порт.
    /// </summary>
    public int Port { get; set; }
       
}
