using Apeiron.Platform.Communication.Elements;

namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет сообщение.
/// </summary>
public sealed class Message :
    Element
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <param name="id">
    /// Идентификатор сообщения.
    /// </param>
    /// <param name="text">
    /// Текст сообщения.
    /// </param>
    /// <param name="sendTime">
    /// Время отправки.
    /// </param>
    /// <param name="registrationTime">
    /// Время регистрации.
    /// </param>
    /// <param name="sender">
    /// Отправитель.
    /// </param>
    /// <param name="recipient">
    /// Получатель.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="recipient"/> передана пустая ссылка.
    /// </exception>
    internal Message(Communicator communicator, long id, string text,
        DateTime sendTime, DateTime registrationTime,
        User sender, User recipient) :
        base(communicator)
    {
        //  Установка значений свойств.
        ID = id;
        Text = IsNotNull(text, nameof(text));
        SendTime = sendTime;
        RegistrationTime = registrationTime;
        Sender = IsNotNull(sender, nameof(sender));
        Recipient = IsNotNull(recipient, nameof(recipient));
        IsMine = Sender.ID == Communicator.User.ID;
    }

    /// <summary>
    /// Возвращает идентификатор пакета.
    /// </summary>
    internal long ID { get; }

    /// <summary>
    /// Возвращает текст сообщения.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Возвращает время отправки.
    /// </summary>
    public DateTime SendTime { get; }

    /// <summary>
    /// Возвращает время регистрации.
    /// </summary>
    public DateTime RegistrationTime { get; }

    /// <summary>
    /// Возвращает отправителя.
    /// </summary>
    public User Sender { get; }

    /// <summary>
    /// Возвращает получателя.
    /// </summary>
    public User Recipient { get; }

    /// <summary>
    /// Возвращает значение, определяющее было ли сообщение отправлено от текущего пользователя.
    /// </summary>
    public bool IsMine { get; }
}
