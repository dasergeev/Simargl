using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Communication.CommunicationDatabase;

/// <summary>
/// Представляет данные сообщения.
/// </summary>
public sealed class MessageData :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор отправителя.
    /// </summary>
    public long SenderID { get; set; }

    /// <summary>
    /// Возвращает или задаёт идентификатор получателя.
    /// </summary>
    public long RecipientID { get; set; }

    /// <summary>
    /// Возвращает или задаёт текст сообщения.
    /// </summary>
    public string Text { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт время отправки.
    /// </summary>
    public DateTime SendTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт время регистрации.
    /// </summary>
    public DateTime RegistrationTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт отправителя.
    /// </summary>
    public UserData Sender { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт получателя.
    /// </summary>
    public UserData Recipient { get; set; } = null!;

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<MessageData> typeBuilder)
    {
        //  Настройка базового класса.
        Entity.BuildAction(typeBuilder);

        //  Настройка индексов.
        typeBuilder.HasIndex(message => message.SendTime).IsUnique(true);
        typeBuilder.HasIndex(message => message.RegistrationTime).IsUnique(true);

        //  Настройка альтернативного ключа.
        typeBuilder.HasAlternateKey(message => new { message.SenderID, message.RecipientID, message.SendTime })
            .HasName("AlternateKey_Message");

        //  Настрока отправленных сообщений.
        typeBuilder.HasOne(message => message.Sender)
            .WithMany(user => user.SentMessages)
            .HasForeignKey(message => message.SenderID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_SentMessages_Users");

        //  Настрока полученных сообщений.
        typeBuilder.HasOne(message => message.Recipient)
            .WithMany(user => user.ReceivedMessages)
            .HasForeignKey(message => message.RecipientID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ReceivedMessages_Users");
    }
}
