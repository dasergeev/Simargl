using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;
using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет элемент управления, отображающий графики.
/// </summary>
public partial class Plotter :
    UserControl
{
    /// <summary>
    /// Поле для хранения элемента управления, управляющего рендерингом графиков в OpneGL.
    /// </summary>
    private readonly PlotterControl _PlotterControl;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Plotter()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание элемента управления, управляющего рендерингом графиков в OpneGL.
        _PlotterControl = new();

        //  Установка элемента.
        _Host.Child = _PlotterControl;
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее, выполняется ли активная перерисовка.
    /// </summary>
    public bool IsActive
    {
        get => _PlotterControl.IsActive;
        set => _PlotterControl.IsActive = value;
    }

    /// <summary>
    /// Возвращает рабочее пространство.
    /// </summary>
    public PlotterWorkspace Workspace => _PlotterControl.Workspace;

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
        await _PlotterControl.LoadAsync(polylines, xMin, xMax, cancellationToken);
    }
}
