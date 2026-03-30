using System.Linq.Expressions;
using System.Reflection;

namespace Apeiron.Events;

/// <summary>
/// Представляет поставщика события.
/// </summary>
public class EventProvider :
    EventProvider<EventArgs, EventHandler>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sender">
    /// Объект, создающий события.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    public EventProvider(object sender) :
        base(sender)
    {

    }
}

/// <summary>
/// Представляет поставщика события.
/// </summary>
/// <typeparam name="TEventArgs">
/// Тип аругментов события.
/// </typeparam>
public class EventProvider<TEventArgs> :
    EventProvider<TEventArgs, EventHandler<TEventArgs>>
    where TEventArgs : EventArgs
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sender">
    /// Объект, создающий события.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    public EventProvider(object sender) :
        base(sender)
    {

    }
}

/// <summary>
/// Представляет поставщика события.
/// </summary>
/// <typeparam name="TEventArgs">
/// Тип аргументов события.
/// </typeparam>
/// <typeparam name="TEventHandler">
/// Тип метода, обрабатывающего событие.
/// </typeparam>
public class EventProvider<TEventArgs, TEventHandler>
    where TEventArgs : EventArgs
    where TEventHandler : Delegate
{
    /// <summary>
    /// Представляет делегат, выполняющий вызов обработчика события.
    /// </summary>
    /// <param name="handler">
    /// Метод, обрабатывающий событие.
    /// </param>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private delegate void InvokeHandler(TEventHandler handler, object? sender, TEventArgs e);

    /// <summary>
    /// Статическое поле для хранения делегата, выполняющего вызов обработчика события.
    /// </summary>
    private static readonly InvokeHandler? _StaticInvokeHandler = CreateInvokeHandlerCore();

    /// <summary>
    /// Поле для хранения делегата, выполняющего вызов обработчика события.
    /// </summary>
    private readonly InvokeHandler _InvokeHandler;

    /// <summary>
    /// Поле для хранения объекта, создающего события.
    /// </summary>
    private readonly object _Sender;

    /// <summary>
    /// Поле для хранения объекта для синхронизации события.
    /// </summary>
    private readonly object _SyncRoot;

    /// <summary>
    /// Поле для хранения списка обработчиков события.
    /// </summary>
    private readonly List<TEventHandler> _Handlers;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sender">
    /// Объект, создающий события.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Операция не поддерживается для данных типов.
    /// </exception>
    public EventProvider(object sender)
    {
        //  Установка делегата, выполняющего вызов обработчика события.
        _InvokeHandler = _StaticInvokeHandler ?? throw Exceptions.OperationNotSupported();

        //  Установка объекта, создающего события.
        _Sender = Check.IsNotNull(sender, nameof(sender));

        //  Создание объекта для синхронизации.
        _SyncRoot = new();

        //  Создание списка обработчиков события.
        _Handlers = new();
    }

    /// <summary>
    /// Добавляет обработчика события.
    /// </summary>
    /// <param name="handler">
    /// Обработчик события.
    /// </param>
    public void AddHandler(TEventHandler? handler)
    {
        //  Проверка ссылки на обработчик.
        if (handler is object)
        {
            //  Блокировка критического объекта.
            lock (_SyncRoot)
            {
                //  Добавление обработчика в список.
                _Handlers.Add(handler);
            }
        }
    }

    /// <summary>
    /// Удаляет обработчика события.
    /// </summary>
    /// <param name="handler">
    /// Обработчик события.
    /// </param>
    public void RemoveHandler(TEventHandler? handler)
    {
        //  Проверка ссылки на обработчик.
        if (handler is object)
        {
            //  Блокировка критического объекта.
            lock (_SyncRoot)
            {
                //  Удаление обработчика из списка.
                _ = _Handlers.Remove(handler);
            }
        }
    }

    /// <summary>
    /// Вызывает событие.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с собятием.
    /// </param>
    public void RaiseEvent(TEventArgs e)
    {
        //  Блокировка критического объекта.
        lock (_SyncRoot)
        {
            //  Перебор всех обработчиков событий.
            foreach (TEventHandler handler in _Handlers)
            {
                //  Вызов обработчика события.
                _InvokeHandler(handler, _Sender, e);
            }
        }
    }

    /// <summary>
    /// Создаёт делегат, выполняющий вызов обработчика события.
    /// </summary>
    /// <returns>
    /// Делегат, выполняющий вызов обработчика события.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static InvokeHandler? CreateInvokeHandlerCore()
    {
        //  Получение информации о методе вызова делегата.
        if (typeof(TEventHandler).GetMethod("Invoke", 0, new Type[] { typeof(object), typeof(TEventArgs) }) is not MethodInfo method)
        {
            //  Метод не найден.
            return null;
        }

        //  Выражение параметра, в котором передаётся обработчик события.
        ParameterExpression handlerParameter = Expression.Parameter(typeof(TEventHandler));

        //  Выражение параметра, в котором передаётся объект, создавший событие.
        ParameterExpression senderParameter = Expression.Parameter(typeof(object));

        //  Выражение параметра, в котором передаются аргументы события.
        ParameterExpression argsParameter = Expression.Parameter(typeof(TEventArgs));

        //  Создание выражения, вызывающего делегат.
        if (Expression.Call(handlerParameter, method, senderParameter, argsParameter) is not MethodCallExpression invoke)
        {
            //  Не удалось получить выражение, вызывающее делегат.
            return null;
        }

        //  Возврат делегата, выполняющего вызов обработчика события.
        return Expression.Lambda<InvokeHandler>(invoke, new ParameterExpression[] { handlerParameter, senderParameter, argsParameter })
            is not Expression<EventProvider<TEventArgs, TEventHandler>.InvokeHandler> lambda
            ? null
            : lambda.Compile();
    }
}
