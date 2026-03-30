namespace Apeiron.Platform.Teltonika;

/// <summary>
/// Прелставляет интерфейс командной строки.
/// </summary>
public interface ICommandLine
{
    /// <summary>
    /// Представляет метод отправки команды и получения результата команды.
    /// </summary>
    /// <param name="sendString">
    /// Отправляемая команда.
    /// </param>
    /// <returns>
    /// Результат команды.
    /// </returns>
    public string SendCommand(string sendString);

    /// <summary>
    /// Представляет метод подключения по интерфейсу.
    /// </summary>
    public void Connect();

    /// <summary>
    /// Представляет метод отключения от интерфейса.
    /// </summary>
    public void Disconnect();

    /// <summary>
    /// Представляет метод загрузки файла на устройство.
    /// </summary>
    /// <param name="localPath">
    /// Путь до файла на устройстве управления.
    /// </param>
    /// <param name="remotePath">
    /// Путь до файла на удаленном устройстве
    /// </param>
    public void UploadFile(string localPath, string remotePath);

    /// <summary>
    /// Представляет метод загрузки файла на компьютер.
    /// </summary>
    /// <param name="localPath">
    /// Путь до файла на устройстве управления.
    /// </param>
    /// <param name="remotePath">
    /// Путь до файла на удаленном устройстве.
    /// </param>
    public void DowloadFile(string localPath, string remotePath);

    /// <summary>
    /// Представляет метод получения потока соединения.
    /// </summary>
    /// <returns>
    /// Возвращает поток.
    /// </returns>
    public Stream GetStream();
}
