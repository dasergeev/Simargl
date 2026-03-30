using Simargl.Storaging.CentralStorage.Entities.Above;

namespace Simargl.Storaging.CentralStorage.Entities.Main;

/// <summary>
/// Представляет данные пользователя.
/// </summary>
public sealed class UserData :
    EntityData
{
    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<UserData> typeBuilder)
    {
        //  Настройка базовой сущности.
        EntityData.BuildAction(typeBuilder);

        //  Настройка имени пользователя.
        typeBuilder.HasIndex(x => x.Name).IsUnique();
    }

    /// <summary>
    /// Возвращает или задаёт имя пользователя.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт хэш пароля.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
}
