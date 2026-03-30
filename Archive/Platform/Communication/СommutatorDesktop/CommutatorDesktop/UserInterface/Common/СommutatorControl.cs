using Apeiron.Platform.Communication.СommutatorDesktop.Logging;

namespace Apeiron.Platform.Communication.СommutatorDesktop.UserInterface;

/// <summary>
/// Представляет базовый класс для всех элементов управления.
/// </summary>
public abstract class СommutatorControl :
    UserControl
{
    /// <summary>
    /// Поле для хранения активного объекта.
    /// </summary>
    private readonly Active _Active;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public СommutatorControl()
    {
        //  Создание активного объекта.
        _Active = new();
    }

    /// <summary>
    /// Возвращает текущее приложение.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public App Application => _Active.Application;

    /// <summary>
    /// Возвращает основной токен отмены.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public CancellationToken CancellationToken => _Active.CancellationToken;

    /// <summary>
    /// Возвращает основные настройки приложения.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public Setting Setting => _Active.Setting;

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    public Logger Logger => _Active.Logger;

    /// <summary>
    /// Возвращает средство вызова методов.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public Invoker Invoker => _Active.Invoker;

    /// <summary>
    /// Возвращает коммуникатор с серверным узлом.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public Communicator Communicator => _Active.Communicator;
}
