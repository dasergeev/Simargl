namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет средство вызова методов в первичном потоке.
/// </summary>
internal sealed class PrimaryInvoker
{
    /// <summary>
    /// Поле для хранения критического объекта для метода <see cref="DefaultPrimaryInvoker"/>.
    /// </summary>
    private readonly object _SyncDefaultPrimary;

    /// <summary>
    /// Поле для хранения метода, вызывающего действие в основном потоке.
    /// </summary>
    private readonly DirectInvoker _PrimaryInvoker;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="provider">
    /// Поставщик первичных вызовов.
    /// </param>
    public PrimaryInvoker(IPrimaryInvokeProvider? provider)
    {
        //  Создание критического объекта.
        _SyncDefaultPrimary = new();

        //  Проверка ссылки на поставщика.
        if (provider is not null)
        {
            //  Установка методов.
            _PrimaryInvoker = provider.PrimaryInvoker ?? DefaultPrimaryInvoker;
        }
        else
        {
            //  Установка методов по умолчанию.
            _PrimaryInvoker = DefaultPrimaryInvoker;
        }
    }

    /// <summary>
    /// Выполняет действие в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void Invoke(Action action)
    {
        //  Проверка ссылки на метод.
        IsNotNull(action, nameof(action));

        //  Исключение, которое возникло при выполнении в основном потоке.
        Exception? exception = null;

        //  Выполнение в основном потоке.
        _PrimaryInvoker(delegate
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Вызов действия.
                action();
            }
            catch (Exception ex)
            {
                //  Установка исключения.
                exception = ex;
            }
        });

        //  Проверка исключения.
        if (exception is not null)
        {
            //  Выброс исключения.
            throw exception;
        }
    }

    /// <summary>
    /// Выполняет действие в основном потоке.
    /// </summary>
    /// <typeparam name="T">
    /// Тип результата.
    /// </typeparam>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <returns>
    /// Результат действия.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public T Invoke<T>(Func<T> action)
    {
        //  Проверка ссылки на метод.
        IsNotNull(action, nameof(action));

        //  Результат.
        T result = default!;

        //  Выполнение в основном потоке.
        Invoke(delegate
        {
            //  Выполнение действия.
            result = action();
        });

        //  Возврат результата.
        return result;
    }

    /// <summary>
    /// Метод, вызывающего действие в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, который необходимо выполнить в основном потоке.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    private void DefaultPrimaryInvoker(Action action)
    {
        //  Проверка ссылки на метод.
        IsNotNull(action, nameof(action));

        //  Блокировка критического объекта.
        lock (_SyncDefaultPrimary)
        {
            //  Вызов метода.
            action();
        }
    }
}
