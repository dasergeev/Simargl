using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет первичный объект, обращение к основным данным которого осуществляется в основном потоке приложения.
/// </summary>
public abstract class Primary :
    Active,
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит в основном потоке при изменении значения своства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public Primary(Engine engine) :
        base(engine)
    {

    }

    /// <summary>
    /// Выполняет действие в основном потоке.
    /// </summary>
    /// <typeparam name="TResult">
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
    protected TResult PrimaryInvoke<TResult>(Func<TResult> action)
    {
        return Invoker.Primary(action);
    }

    /// <summary>
    /// Асинхронно выполняет действие в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие в основном потоке.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected async Task PrimaryInvokeAsync(Action action, CancellationToken cancellationToken)
    {
        await Invoker.PrimaryAsync(action, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет действие в основном потоке.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата действия.
    /// </typeparam>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие в основном потоке.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected async Task<TResult> PrimaryInvokeAsync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
    {
        return await Invoker.PrimaryAsync(action, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Захват текущего делегата.
        PropertyChangedEventHandler? handler = Volatile.Read(ref PropertyChanged);

        //  Проверка ссылки на делегат.
        if (handler is not null)
        {
            //  Выполнение действия в основном потоке.
            Invoker.Primary(delegate
            {
                //  Вызов события.
                handler(this, e);
            });
        }
    }
}
