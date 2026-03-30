using System.Net.Mail;
using System.Text;

namespace CopyBackupFiles;

/// <summary>
/// Класс реализует паттерн Builder(Строитель) для конфигурирования и создание сложного объекта MailMessage.
/// </summary>
internal sealed class MailMessageBuilder
{
    /// <summary>
    /// Поле для хранения объекта MailMessage.
    /// </summary>
    private readonly MailMessage _mailMessage = new();

    public MailMessageBuilder From(string address)
    {
        _mailMessage.From = new MailAddress(address);
        return this;
    }

    public MailMessageBuilder To(string address)
    {
        _mailMessage.To.Add(address);
        return this;
    }

    public MailMessageBuilder Subject(string subject)
    {
        _mailMessage.Subject = subject;
        return this;
    }

    public MailMessageBuilder Body(string body, Encoding encoding)
    {
        _mailMessage.Body = body;
        _mailMessage.BodyEncoding = encoding;
        return this;
    }

    public MailMessageBuilder IsBodyHtml(bool isBodyHtml)
    {
        _mailMessage.IsBodyHtml = isBodyHtml;
        return this;
    }

    /// <summary>
    /// Возвращает созданный и сконфигурированный объект MailMessage.
    /// </summary>
    /// <returns>Объект MailMessage</returns>
    /// <exception cref="InvalidOperationException">Ошибка возникающая при отсутствии получателя.</exception>
    public MailMessage Build()
    {
        if (_mailMessage.To.Count == 0)
            throw new InvalidOperationException("Необходимо указать получателя.");

        return _mailMessage;
    }
}
