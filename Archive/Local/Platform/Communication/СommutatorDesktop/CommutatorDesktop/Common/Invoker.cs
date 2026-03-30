using System.Windows.Threading;

namespace Apeiron.Platform.Communication.СommutatorDesktop;

/// <summary>
/// Представляет средство вызова методов.
/// </summary>
public sealed class Invoker :
    Active,
    IPrimaryInvokeProvider
{
    /// <summary>
    /// Поле для хранения диспетчера основного потока.
    /// </summary>
    private readonly Dispatcher _Dispatcher;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер основного потока.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    public Invoker(Dispatcher dispatcher)
    {
        //  Установка диспетчера основого потока.
        _Dispatcher = IsNotNull(dispatcher, nameof(dispatcher));
    }

    /// <summary>
    /// Возвращает метод вызывающий действие в основном потоке.
    /// </summary>
    DirectInvoker? IPrimaryInvokeProvider.PrimaryInvoker => _Dispatcher.Invoke;

    /// <summary>
    /// Выполняет действие в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void Primary(Action action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка необходимости переключения потока.
        if (Environment.CurrentManagedThreadId != _Dispatcher.Thread.ManagedThreadId)
        {
            //  Передача делегата в основной поток для выполнения.
            _Dispatcher.Invoke(action);
        }
        else
        {
            //  Выполнение в текущем потоке.
            action();
        }
    }
}
