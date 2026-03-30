using Simargl.Hardware.Strain.Demo.Microservices;
using Simargl.Hardware.Strain.Demo.Nodes;
using Simargl.Hardware.Strain.Demo.UI;
using System.Reflection;

namespace Simargl.Hardware.Strain.Demo;

/// <summary>
/// Представляет сердце приложения.
/// </summary>
public sealed class Heart :
    Anything
{
    /// <summary>
    /// Поле для хранения приложения.
    /// </summary>
    private App? _Application;

    /// <summary>
    /// Происходит при изменении выбранного узла.
    /// </summary>
    public event EventHandler? SelectedNodeChanged;

    /// <summary>
    /// Поле для хранения элемента управления, отображающего графики.
    /// </summary>
    private readonly TargetChart _Chart;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Heart()
    {
        //  Обращение к объекту.
        Lay();

        //  Создание элемента управления, отображающего графики.
        _Chart = new();

        //  Поиск свойства, отвечающего за двойную буферизацию.
        if (typeof(System.Windows.Forms.Control).GetProperty(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic)
            is PropertyInfo property)
        {
            //  Включение двойной буферизации.
            property.SetValue(_Chart, true, null);
        }

        //  Создание корневого узла.
        RootNode = new(this);

        //  Создание микросервиса управляющего отображением графиков.
        ChartManager = new(this, _Chart);
    }

    /// <summary>
    /// Возвращает приложение.
    /// </summary>
    public App Application
    {
        get
        {
            //  Проверка приложения.
            if (_Application is not App application)
            {
                //  Получение приложения.
                application = (App)System.Windows.Application.Current;

                //  Замена приложения.
                application = Interlocked.CompareExchange(ref _Application, application, null) ?? application;
            }

            //  Возврат приложения.
            return application;
        }
    }

    /// <summary>
    /// Возвращает корневой узел.
    /// </summary>
    public RootNode RootNode { get; }

    /// <summary>
    /// Возвращает выбранный узел.
    /// </summary>
    public Node? SelectedNode { get; private set; }

    /// <summary>
    /// Возвращает микросервис управляющий отображением графиков.
    /// </summary>
    public ChartManager ChartManager { get; }

    /// <summary>
    /// Устанавливает выбранный узел.
    /// </summary>
    /// <param name="node">
    /// Выбранный узел.
    /// </param>
    public void SetSelectedNode(Node? node)
    {
        //  Проверка изменения значения.
        if (!ReferenceEquals(SelectedNode, node))
        {
            //  Установка нового значения.
            SelectedNode = node;

            //  Вызов события об изменении значения свойства.
            Volatile.Read(ref SelectedNodeChanged)?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу сердца приложения.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая работу сердца приложения.
    /// </returns>
    public async Task RunAsync()
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(Application.CancellationToken).ConfigureAwait(false);

        //  Запуск микросервисов.
        _ = new Scanner(this);
        _ = new Server(this);
        Application.Keeper.Add(ChartManager.InvokeAsync);
    }
}
