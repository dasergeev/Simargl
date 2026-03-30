using Apeiron.Platform.Communication.Elements;

namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет информацию о пользователе.
/// </summary>
public sealed class User :
    Element
{
    /// <summary>
    /// Поле для хранения идентификатора пользователя.
    /// </summary>
    private long _ID;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <param name="id">
    /// Идентификатор пользователя.
    /// </param>
    /// <param name="name">
    /// Имя пользователя.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    internal User(Communicator communicator, long id, string name) :
        base(communicator)
    {
        //  Установка значений свойств.
        ID = id;
        Name = IsNotNull(name, nameof(name));
    }

    /// <summary>
    /// Возвращает или задаёт идентификатор пользователя.
    /// </summary>
    internal long ID
    {
        get => Interlocked.Read(ref _ID);
        set => Interlocked.Exchange(ref _ID, value);
    }

    /// <summary>
    /// Возвращает имя пользователя.
    /// </summary>
    public string Name { get; }
}
