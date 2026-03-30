using Apeiron.Platform.Communication.СommutatorDesktop.Logging;

namespace Apeiron.Platform.Communication.СommutatorDesktop;

/// <summary>
/// Представляет активный объект.
/// </summary>
public class Active :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения приложения.
    /// </summary>
    private App? _Application;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Active()
    {

    }

    /// <summary>
    /// Возвращает текущее приложение.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public App Application
    {
        get
        {
            //  Проверка текущего приложения.
            if (_Application is null)
            {
                //  Получение текущего приложения.
                Interlocked.CompareExchange(ref _Application, (App)System.Windows.Application.Current, null);
            }

            //  Проверка полученного приложения.
            if (_Application is null)
            {
                //  Недопустимая операция.
                throw Exceptions.OperationInvalid();
            }

            //  Возврат приложения.
            return _Application;
        }
    }

    /// <summary>
    /// Возвращает основной токен отмены.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public CancellationToken CancellationToken => Application.CancellationToken;

    /// <summary>
    /// Возвращает основные настройки приложения.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public Setting Setting => Application.Setting;

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    public Logger Logger => Application.Logger;

    /// <summary>
    /// Возвращает средство вызова методов.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public Invoker Invoker => Application.Invoker;

    /// <summary>
    /// Возвращает коммуникатор с серверным узлом.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public Communicator Communicator => Application.Communicator;

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        PropertyChanged?.Invoke(this, e);
    }
}
