using Apeiron.Platform.Communication.Remoting;

namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет коммуникатор с серверным узлом.
/// </summary>
public sealed class Communicator :
    IDisposable
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="options">
    /// Параметры коммуникатора.
    /// </param>
    /// <param name="primaryInvokeProvider">
    /// Поставщик первичных вызовов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="options"/> передана пустая ссылка.
    /// </exception>
    public Communicator(CommunicatorOptions options,
        IPrimaryInvokeProvider? primaryInvokeProvider)
    {
        //  Установка параметров коммуникатора.
        Options = IsNotNull(options, nameof(options));

        //  Создание средства вызова методов в первичном потоке.
        PrimaryInvoker = new(primaryInvokeProvider);

        //  Создание средства вызова удалённых методов.
        RemoteInvoker = new(this);

        //  Создание коллекции диалогов.
        Dialogs = new(this);
    }

    /// <summary>
    /// Возвращает параметры коммуникатора.
    /// </summary>
    public CommunicatorOptions Options { get; }

    /// <summary>
    /// Возвращает средство вызова методов в первичном потоке.
    /// </summary>
    internal PrimaryInvoker PrimaryInvoker { get; }

    /// <summary>
    /// Возвращает средство вызова удалённых методов.
    /// </summary>
    internal RemoteInvoker RemoteInvoker { get; }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public User User => RemoteInvoker.User;

    /// <summary>
    /// Возвращает коллекцию диалогов.
    /// </summary>
    public DialogCollection Dialogs { get; }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    void IDisposable.Dispose()
    {
        //  Разрушение средства вызова удалённых методов.
        ((IDisposable)RemoteInvoker).Dispose();
    }
}
