
using Apeiron.Platform.Communication;
using System.Collections.Concurrent;

namespace CommutatorMaui;

/// <summary>
/// Представляет основные настройки приложения.
/// </summary>
public sealed class Setting
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Имя пользователя.
    /// <paramref name="password"/>
    /// Пароль.
    /// </param>
    public Setting(string name, string password)
    {
        //  Создание параметров коммуникатора.
        CommunicatorOptions = new(
            new("morphism.ru", 7013, name, password),
            new(TimeSpan.FromMilliseconds(2000), 10, TimeSpan.FromMilliseconds(1000)));
    }


    /// <summary>
    /// Возвращает параметры коммуникатора.
    /// </summary>
    public CommunicatorOptions CommunicatorOptions { get; }


}
