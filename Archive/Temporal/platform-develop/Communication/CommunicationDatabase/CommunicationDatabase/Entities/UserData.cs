using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Communication.CommunicationDatabase;

/// <summary>
/// Представляет данные пользователя.
/// </summary>
public sealed class UserData :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт имя пользователя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт пароль.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт коллекцию отправленных сообщений.
    /// </summary>
    public HashSet<MessageData> SentMessages { get; set; } = new();

    /// <summary>
    /// Возвращает или задаёт коллекцию полученных сообщений.
    /// </summary>
    public HashSet<MessageData> ReceivedMessages { get; set; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<UserData> typeBuilder)
    {
        //  Настройка базового класса.
        Entity.BuildAction(typeBuilder);

        //  Настройка уникального индекса.
        typeBuilder.HasIndex(user => user.Name).IsUnique(true);
    }
}
