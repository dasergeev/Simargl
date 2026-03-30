using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.Nodes;

/// <summary>
/// Представляет узел проекта.
/// </summary>
/// <typeparam name="TChildNode">
/// Тип дочернего узла.
/// </typeparam>
public abstract class Node<TChildNode> :
    Primary,
    INode
    where TChildNode : INode
{
    /// <summary>
    /// Поле для хранения имени узла.
    /// </summary>
    private string _Name;

    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly ObservableCollection<TChildNode> _Nodes;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    /// <param name="nodeFormat">
    /// Формат узла.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="nodeFormat"/> передано значение,
    /// которое не содержится в перечислении <see cref="NodeFormat"/>.
    /// </exception>
    public Node(Engine engine, string name, NodeFormat nodeFormat) :
        base(engine)
    {
        //  Установка имени узла.
        _Name = IsNotNull(name, nameof(name));

        //  Создание хранилища элементов коллекции.
        _Nodes = new();

        //  Создание коллекции дочерних узлов.
        Nodes = new(engine, _Nodes);

        //  Установка формата узла.
        NodeFormat = IsDefined(nodeFormat, nameof(nodeFormat));
    }

    /// <summary>
    /// Возвращает или задаёт имя узла.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    [Browsable(false)]
    public string Name
    {
        get => _Name;
        set
        {
            //  Проверка параметра.
            IsNotNull(value, nameof(Name));

            //  Выполнение в основном потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_Name != value)
                {
                    //  Установка нового значения.
                    _Name = value;

                    //  Вызов события об изменении значения.
                    OnPropertyChanged(new(nameof(Name)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    [Browsable(false)]
    public NodeCollection<TChildNode> Nodes { get; }

    /// <summary>
    /// Возвращает формат узла.
    /// </summary>
    [Browsable(false)]
    public NodeFormat NodeFormat { get; }

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
    protected async Task PrimaryInvokeAsync(Action<ObservableCollection<TChildNode>> action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполнение.
        await base.PrimaryInvokeAsync(delegate
        {
            //  Выполнение действия.
            action(_Nodes);
        }, cancellationToken).ConfigureAwait(false);
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
    protected async Task<TResult> PrimaryInvokeAsync<TResult>(
        Func<ObservableCollection<TChildNode>, TResult> action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполнение.
        return await base.PrimaryInvokeAsync(delegate
        {
            //  Выполнение действия.
            return action(_Nodes);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Возвращает коллекцию дочерних узлов.
    /// </summary>
    INodeCollection INode.Nodes => Nodes;
}
