using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;
using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;
/// <summary>
/// Представляет элемент управления, управляющий рендерингом графиков в OpenGL.
/// </summary>
internal sealed class PlotterControl :
    RenderControl
{
    /// <summary>
    /// Постоянная, определяющая минимальное значение периода обновления в милисекундах.
    /// </summary>
    public const int MinUpdatePeriod = 10;

    /// <summary>
    /// Постоянная, определяющая максимальное значение периода обновления в милисекундах.
    /// </summary>
    public const int MaxUpdatePeriod = 1000;

    /// <summary>
    /// Поле для хранения таймера, управляющего перерисовкой.
    /// </summary>
    private readonly System.Windows.Forms.Timer _Timer;

    /// <summary>
    /// Поле для хранения периода обновления в милисекундах.
    /// </summary>
    private int _UpdatePeriod;

    /// <summary>
    /// Поле для хранения текущих ломаных линий.
    /// </summary>
    private volatile PolylineCollection _Polylines;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public PlotterControl()
    {
        //  Создание таймера, управляющего перерисовкой.
        _Timer = new();

        //  Установка периода обновления.
        _UpdatePeriod = 100;

        //  Добавление обработчика события таймера.
        _Timer.Tick += Timer_Tick;

        //  Установка интервала таймера.
        _Timer.Interval= _UpdatePeriod;

        //  Запуск таймера.
        _Timer.Enabled = true;

        //  Создание текущих ломаных линий.
        _Polylines = new();

        //  Создание рабочего пространства.
        Workspace = new();
    }

    /// <summary>
    /// Возвращает или задаёт период обновления в милисекундах.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое меньше значения <see cref="MinUpdatePeriod"/>
    /// или больше значения <see cref="MaxUpdatePeriod"/>.
    /// </exception>
    public int UpdatePeriod
    {
        get => _UpdatePeriod;
        set
        {
            //  Проверка нового значения.
            IsInRange(value, MinUpdatePeriod, MaxUpdatePeriod, nameof(UpdatePeriod));

            //  Установка нового значения.
            _UpdatePeriod = value;
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее, выполняется ли активная перерисовка.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Возвращает рабочее пространство.
    /// </summary>
    public PlotterWorkspace Workspace { get; }

    /// <summary>
    /// Обрабатывает событие таймера.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Timer_Tick(object? sender, EventArgs e)
    {
        //  Проверка необходимости перерисовки.
        if (IsActive)
        {
            //  Перерисовка.
            Invalidate();
        }
        
        //  Изменение периода обновления.
        _Timer.Interval = _UpdatePeriod;
    }

    /// <summary>
    /// Асинхронно загружает данные для отображения.
    /// </summary>
    /// <param name="polylines">
    /// Коллекция ломаных линий.
    /// </param>
    /// <param name="xMin">
    /// Минимальное значение по оси Ox.
    /// </param>
    /// <param name="xMax">
    /// Максимальное значение по оси Ox.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая загрузку данных для отображения.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LoadAsync(PolylineCollection polylines, double xMin, double xMax, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Замена коллекции ломаных.
        Interlocked.Exchange(ref _Polylines, polylines);

        //  Установка диапазона отображения.
        Workspace.Metrics.SetXRange(xMin, xMax);
    }

    /// <summary>
    /// Выполняет рендеринг.
    /// </summary>
    /// <param name="renderer">
    /// Средство рендеринга.
    /// </param>
    protected override void Render(Renderer renderer)
    {
        //  Получение коллекции ломаных линий.
        PolylineCollection? polylines = null;
        Interlocked.Exchange(ref polylines, _Polylines);

        //  Установка диапазона значений по оси Oy.
        Workspace.Metrics.SetYRange(polylines);

        //  Получение копии метрик.
        PlotterWorkspaceMetrics metrics = Workspace.Metrics.Clone();

        //  Установка текущего размера окна.
        metrics.SetSize(Width, Height);

        //  Проверка допустимости метрик.
        if (metrics.IsValid)
        {
            //  Настройка окна просмотра на область графиков.
            Original.Viewport(
                Workspace.Padding.Left, Workspace.Padding.Bottom,
                Width - Workspace.Padding.Left - Workspace.Padding.Right,
                Height - Workspace.Padding.Top - Workspace.Padding.Bottom);

            //  Настройка матрицы проекции.
            Original.MatrixMode(MatrixMode.Projection);
            Original.LoadIdentity();
            Original.Ortho(metrics.XMin, metrics.XMax, metrics.YMin, metrics.YMax, 0, 1);

            //  Отображение фона.
            Workspace.Background.Render(renderer, metrics);

            //  Отображение сетки.
            Workspace.Grid.Render(renderer, metrics);

            //  Отображение коллекции ломаных линий.
            polylines.Render(renderer);

            //  Настройка окна просмотра на весь элемент управления.
            Original.Viewport(0, 0, Width, Height);

            //  Настройка матрицы проекции.
            Original.MatrixMode(MatrixMode.Projection);
            Original.LoadIdentity();
            Original.Ortho(0, Width, 0, Height, 0, 1);

            //  Отображение границы.
            Workspace.Border.Render(renderer, metrics);

            //  Отображение осей.
            Workspace.Axes.Render(renderer, metrics);
        }
    }
}
