using Apeiron.Platform.Communication;

namespace CommutatorMaui;

/// <summary>
/// Представляет средство вызова методов.
/// </summary>
public sealed class Invoker :
    IPrimaryInvokeProvider
{
    /// <summary>
    /// Поле для хранения метода, вызывающего действие в основном потоке.
    /// </summary>
    private readonly Action<Action> _CoreInvoker;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="coreInvoker"/> передана пустая ссылка.
    /// </exception>
    public Invoker(Action<Action> coreInvoker)
    {
        //  Утсановка метода, вызывающего действие в основном потоке.
        _CoreInvoker = IsNotNull(coreInvoker, nameof(coreInvoker));
    }

    /// <summary>
    /// Возвращает метод вызывающий действие в основном потоке.
    /// </summary>
    DirectInvoker? IPrimaryInvokeProvider.PrimaryInvoker => Primary;

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

        //  Выполнение действия в основном потоке.
        _CoreInvoker(action);

        //if (action is not null)
        //{
        //    //if (DeviceInfo.Platform == DevicePlatform)
        //    //{
        //    //    if (Application.Current is not null)
        //    //    {
        //    //        Application.Current.Dispatcher.Dispatch(action);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    MainThread.BeginInvokeOnMainThread(action);
        //    //}

        //    //if (MainThread.IsMainThread)
        //    //{
        //    //    action();
        //    //}
        //    //else
        //    //{
        //       await MainThread.InvokeOnMainThreadAsync(action);
        //    }
        ////}
        //else
        //{
        //    throw new ArgumentNullException(nameof(action));
        //}

    }
}
