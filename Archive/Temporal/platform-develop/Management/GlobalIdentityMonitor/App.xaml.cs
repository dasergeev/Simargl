namespace Apeiron.Services.GlobalIdentity;

/// <summary>
/// Представляет приложение.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Содержит ресурс для CancellationToken приложения.
    /// </summary>
    private readonly CancellationTokenSource _GlobalCancellationTokenSource;

    /// <summary>
    /// Содержит токен отмены приложения.
    /// </summary>
    private readonly CancellationToken _GlobalCancellationToken;

    /// <summary>
    /// Конструктор.
    /// Получает текущий домен приложения и подписывается на обработку необработанных исключений.
    /// </summary>
    public App()
    {
        // Получает текущий домен приложения.
        var currentDomain = AppDomain.CurrentDomain;
        // Подписка на необработанные исклюения в текущем домене приложений.
        currentDomain.UnhandledException += CurrentDomain_UnhandledException;

        // Создание ресурса для токена отмены.
        _GlobalCancellationTokenSource = new CancellationTokenSource();
        // Создание токена отмены.
        _GlobalCancellationToken = _GlobalCancellationTokenSource.Token;
        // Добавление токена отмены уровня приложения в словарь ресурсов приложения.
        App.Current.Properties["GlobalToken"] = _GlobalCancellationToken;       
    }


    /// <summary>
    /// Обработчик событиея - выводит на экран необработанное исключение.
    /// </summary>
    /// <param name="sender">Объект создавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        // Получение и преобразование объекта исключения.
        Exception? ex = (Exception)e.ExceptionObject;

        // Повторный выброс исключения, если исключение системное и критичесое.
        if (ex is OutOfMemoryException || ex is StackOverflowException || ex is DllNotFoundException)
            throw ex;

        // Выводим сообщение об ошибке.
        MessageBox.Show($"Во время выполенния программы произошло исключение:\n\n\r{ex?.Message}\n\nСтек вызова:\n\r{ex?.StackTrace}\nВнутреннее исключение:\n\r{ex?.InnerException?.Message}", 
            "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);

        // Посылаем отмену всем задачам через токен отмены уровня приложения.
        _GlobalCancellationTokenSource.Cancel();
    }


    ///// <summary>
    ///// Безопасно вызывает действие.
    ///// </summary>
    ///// <param name="action">
    ///// Действие, которое необходимо выполнить безопасно.
    ///// </param>
    ///// <remarks>
    ///// Метод пробует выполнить действие. Если в действии возникает исключение, то обрабатывает его и выдаёт окно сообщение.
    ///// </remarks>
    ///// <exception cref="ArgumentNullException">
    ///// В параметре <paramref name="action"/> передана пустая ссылка.
    ///// </exception>
    //public static void SafeCall(Action action)
    //{
    //    //  Проверка действия.
    //    action = Check.IsNotNull(action, nameof(action));

    //    //  Блок перехвата несистемных исключений.
    //    try
    //    {
    //        //  Вызов действия.
    //        action.Invoke();
    //    }
    //    catch (Exception ex)
    //    {
    //        //  Проверка системного исключения.
    //        if (ex.IsSystem())
    //        {
    //            //  Повторный выброс.
    //            throw;
    //        }

    //        //  Вывод сообщения об ошибке пользователю.
    //        MessageBox.Show($"Во время выполенния программы произошло исключение:{ex}");
    //    }
    //}
}

