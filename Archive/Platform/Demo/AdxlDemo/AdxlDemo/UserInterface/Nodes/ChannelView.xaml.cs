using Apeiron.Platform.Demo.AdxlDemo.Channels;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;
namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет элемент управления, отображающий канал.
/// </summary>
public partial class ChannelView :
    NodeView
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ChannelView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Настройка отображения значений по оси Ox.
        _ChannelPlotter.Workspace.Axes.XСonverter = x => x - Math.Floor(x / 60) * 60;
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
    protected override async Task TickAsync(INode node, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение организатора канала.
        if (node is ChannelOrganizer organizer)
        {
            //  Длительность запрашиваемого фрагмента.
            TimeSpan duration = TimeSpan.FromSeconds(organizer.Duration);

            //  Получение текущего времени.
            DateTime nowTime = DateTime.Now;

            //  Начало запрашиваемого фрагмента.
            DateTime beginTime = organizer.BeginTime;// DateTime.Now - duration;

            //string text = $"{beginTime:dd.MM.yyyy HH:mm:ss}";

            //  Выполнение в основном потоке.
            await Invoker.PrimaryAsync(delegate
            {
                //  Установка минимального и максимального времени.
                _BeginTimePicker.DisplayDateStart = organizer.MinTime;
                _BeginTimePicker.DisplayDateEnd = nowTime;

                _BeginTimeSlider.Minimum = organizer.MinTime.ToOADate();
                _BeginTimeSlider.Maximum = nowTime.ToOADate();

                //  Установка текущего времени.
                _BeginTimePicker.SelectedDate = beginTime;
                _BeginTimeSlider.Value = beginTime.ToOADate();

                //  Установка подсказки.
                _SliderToolTip.Content = $"{beginTime}";
            }, cancellationToken).ConfigureAwait(false);

            //  Минимальное значение.
            double xMin = beginTime.Second + beginTime.Millisecond / 1000.0;

            //  Максимальное значение.
            double xMax = xMin + duration.TotalSeconds;

            //  Получение данных.
            PolylineCollection polylines = await organizer.GetDataAsync(
                beginTime, duration, xMin, cancellationToken).ConfigureAwait(false);

            //  Передача данных для отображения.
            await _ChannelPlotter.LoadAsync(polylines, xMin, xMax, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        //  Вызов метода базовго класса.
        base.OnPropertyChanged(e);

        //  Проверка изменения видимости.
        if (e.Property.Name == nameof(Visibility))
        {
            //  Изменение активности отображения.
            _ChannelPlotter.IsActive = Visibility == Visibility.Visible;
        }
    }

    /// <summary>
    /// Происходит при изменении текущего времени.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void BeginTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        //  Получение организатора канала и проверка выбранного времени.
        if (Node is ChannelOrganizer organizer && _BeginTimePicker.SelectedDate.HasValue)
        {
            //  Установка времени.
            organizer.BeginTime = _BeginTimePicker.SelectedDate.Value;
        }
    }

    /// <summary>
    /// Происходит при изменении текущего времени.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void BeginTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        //  Получение организатора канала.
        if (Node is ChannelOrganizer organizer)
        {
            //  Установка времени.
            organizer.BeginTime = DateTime.FromOADate(_BeginTimeSlider.Value);
        }
    }
}
