using System.Net.Mail;

namespace CopyBackupFiles;

/// <summary>
/// Класс реализует паттерн Builder(Строитель) для конфигурирования и создание сложного объекта SmtpClient.
/// </summary>
internal sealed class SmtpBuilder
{
    /// <summary>
    /// Поле для хранения SmtpClient объекта.
    /// </summary>
    private readonly SmtpClient _smtpClient = new();

    public SmtpBuilder ServerAddress(string smtpServer)
    {
        _smtpClient.Host = smtpServer;
        return this;
    }

    public SmtpBuilder ServerPort(int port)
    {
        _smtpClient.Port = port;
        return this;
    }

    /// <summary>
    /// Возвращает созданный и сконфигурированный объект SmtpClient.
    /// </summary>
    /// <returns>Объект SmtpClient</returns>
    /// <exception cref="InvalidOperationException">Не задан сервер отправки электронной почты.</exception>
    public SmtpClient Build()
    {
        if (string.IsNullOrEmpty(_smtpClient.Host))
            throw new InvalidOperationException("Не задан сервер отправки электронной почты.");
        return _smtpClient;
    }
}
