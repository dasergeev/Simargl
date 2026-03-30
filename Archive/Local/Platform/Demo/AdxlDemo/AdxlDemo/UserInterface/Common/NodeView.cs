using Apeiron.Platform.Demo.AdxlDemo.Logging;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет элемент управления, отображающий информацию узла.
/// </summary>
public abstract class NodeView :
    UserControl
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Node"/>.
    /// </summary>
    public event EventHandler? NodeChanged;

    /// <summary>
    /// Поле для хранения критического объекта.
    /// </summary>
    private readonly object _SyncRoot;

    /// <summary>
    /// Поле для хранения узла.
    /// </summary>
    private INode? _Node;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _TokenSource;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public NodeView()
    {
        //  Создание критического объекта.
        _SyncRoot = new();

        //  Установка текущего приложения.
        Application = (App)System.Windows.Application.Current;
    }

    /// <summary>
    /// Возвращает или задаёт интервал повторения отсчёта.
    /// </summary>
    protected TimeSpan Interval { get; set; } = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// Возвращает текущее приложение.
    /// </summary>
    protected App Application { get; }

    /// <summary>
    /// Возвращает основной активный объект.
    /// </summary>
    protected Engine Engine => Application.Engine;

    /// <summary>
    /// Возвращает общие настройки приложения.
    /// </summary>
    public Settings Settings => Application.Settings;

    /// <summary>
    /// Возвращает средство вызова методов.
    /// </summary>
    public Invoker Invoker => Engine.Invoker;

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    public Logger Logger => Engine.Logger;

    /// <summary>
    /// Возвращает или задаёт узел.
    /// </summary>
    public INode? Node
    {
        get
        {
            //  Блокировка критического объекта.
            lock (_SyncRoot)
            {
                //  Возврат узла.
                return _Node;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (_SyncRoot)
            {
                //  Проверка изменения значения свойства.
                if (!ReferenceEquals(_Node, value))
                {
                    //  Установка нового значения.
                    _Node = value;

                    //  Вызов события об изменении значения свойства.
                    OnNodeChanged(EventArgs.Empty);

                    //  Проверка необходимости запуска задачи.
                    if (_Node is not null && _TokenSource is null)
                    {
                        //  Создание токена отмены.
                        _TokenSource = new();

                        //  Запуск асинхронной задачи.
                        _ = Task.Run(async delegate
                        {
                            //  Выполнение основоной задачи.
                            await InvokeAsync(_TokenSource.Token);
                        });

                        //  Настройка видимости.
                        Visibility = Visibility.Visible;
                    }

                    //  Проверка необходимости остановки основной задачи.
                    if (_Node is null && _TokenSource is not null)
                    {
                        //  Остановка источника токена отмены.
                        _TokenSource.Cancel();

                        //  Сброс источника токена отмены.
                        _TokenSource = null;

                        //  Настройка видимости.
                        Visibility = Visibility.Collapsed;
                    }

                    //  Установка контекста данных.
                    DataContext = _Node;
                }
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет новый отсчёт.
    /// </summary>
    /// <param name="node">
    /// Текущий узел.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполнящая новый отсчёт.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected abstract Task TickAsync(INode node, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно выполняет основную задачу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Основная задача.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Безопасное выполнение.
            await Invoker.SystemAsync(async (cancellationToken) =>
            {
                //  Текущий узел.
                INode? node;

                //  Блокировка критического объекта.
                lock (_SyncRoot)
                {
                    //  Получение узла.
                    node = _Node;
                }

                //  Проверка узла.
                if (node is not null)
                {
                    //  Выполнение отсчёта.
                    await TickAsync(node, cancellationToken).ConfigureAwait(false);
                }
            }, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед следующим шагом.
            await Task.Delay(Interval, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="NodeChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnNodeChanged(EventArgs e)
    {
        //  Захват текущего делегата.
        EventHandler? handler = Volatile.Read(ref NodeChanged);

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
