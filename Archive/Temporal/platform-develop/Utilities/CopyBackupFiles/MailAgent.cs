using System.Net.Mail;
using System.Text;

namespace CopyBackupFiles;

/// <summary>
/// Класс представляет Mail Agent для отпрвки писем.
/// </summary>
internal sealed class MailAgent
{
    // Создаём Smtp клиент для отпрвки письма.
    private readonly SmtpClient smtpClient = new SmtpBuilder()
        .ServerAddress("10.47.49.65")
        .ServerPort(25)
        .Build();


    /// <summary>
    /// Функция отправки почты.
    /// </summary>
    /// <param name="subject">Тема письма.</param>
    /// <param name="mailBody">Тело присьма.</param>
    internal void SendMail(string subject, string mailBody)
    {
        if ((subject is not null) && (mailBody is not null))
        {
            // Создаём письмо для отправки.
            MailMessage mail = new MailMessageBuilder()
                .From("prod-db@railtest.ru")
                .To("devops@railtest.ru")
                .Subject(subject)
                .Body(mailBody, Encoding.UTF8)
                .IsBodyHtml(true)
                .Build();

            // Отправка письма.
            smtpClient.Send(mail);
        }
    }
}
