using Apeiron.Platform.Communication.Remoting;
using System.ComponentModel;

namespace Apeiron.Platform.Communication.Elements;

/// <summary>
/// Представляет элемент коммуникации.
/// </summary>
public abstract class Element :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    internal Element(Communicator communicator)
    {
        //  Установка коммуникатора с серверным узлом.
        Communicator = IsNotNull(communicator, nameof(communicator));
    }

    /// <summary>
    /// Возвращает коммуникатор с серверным узлом.
    /// </summary>
    public Communicator Communicator { get; }

    /// <summary>
    /// Возвращает параметры подключения к серверу.
    /// </summary>
    internal ConnectionOptions ConnectionOptions => Communicator.Options.ConnectionOptions;

    /// <summary>
    /// Возвращает параметры удалённого вызова.
    /// </summary>
    internal RemoteInvokeOptions RemoteInvokeOptions => Communicator.Options.RemoteInvokeOptions;

    /// <summary>
    /// Возвращает средство вызова методов в первичном потоке.
    /// </summary>
    internal PrimaryInvoker PrimaryInvoker => Communicator.PrimaryInvoker;

    /// <summary>
    /// Возвращает средство вызова удалённых методов.
    /// </summary>
    internal RemoteInvoker RemoteInvoker => Communicator.RemoteInvoker;

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Выполнение в основном потоке.
        PrimaryInvoker.Invoke(delegate
        {
            //  Захват текущего делегата.
            PropertyChangedEventHandler? handler = Volatile.Read(ref PropertyChanged);

            //  Вызов события.
            handler?.Invoke(this, e);
        });
    }
}
