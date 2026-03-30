using Apeiron.Platform.Demo.AdxlDemo.Channels;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;
namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет элемент управления, отображающий коллекцию каналов.
/// </summary>
public partial class ChannelCollectionView :
    NodeView
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ChannelCollectionView()
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

        //  Получение коллекции организаторов каналов.
        if (node is ChannelOrganizerCollection organizers)
        {
            //  Длительность запрашиваемого фрагмента.
            TimeSpan duration = TimeSpan.FromSeconds(organizers.Duration);

            //  Получение текущего времени.
            DateTime nowTime = DateTime.Now;

            //  Начало запрашиваемого фрагмента.
            DateTime beginTime = organizers.BeginTime;

            //  Выполнение в основном потоке.
            await Invoker.PrimaryAsync(delegate
            {
                //  Установка источника списка.
                _ChannelListView.ItemsSource = organizers.Nodes;

                //  Установка минимального и максимального времени.
                _BeginTimePicker.DisplayDateStart = organizers.MinTime;
                _BeginTimePicker.DisplayDateEnd = nowTime;

                _BeginTimeSlider.Minimum = organizers.MinTime.ToOADate();
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

            //  Создание коллекции ломаных линий.
            PolylineCollection polylines = new();

            //  Перебор всех каналов.
            foreach (ChannelOrganizer organizer in organizers.Nodes)
            {
                //  Проверка видимости канала.
                if (organizer.IsVisible)
                {
                    //  Получение данных.
                    polylines.AddRange(await organizer.GetDataAsync(
                        beginTime, duration, xMin, cancellationToken).ConfigureAwait(false));
                }
            }

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
        //  Получение коллекции организаторов каналов и проверка выбранного времени.
        if (Node is ChannelOrganizerCollection organizers && _BeginTimePicker.SelectedDate.HasValue)
        {
            //  Установка времени.
            organizers.BeginTime = _BeginTimePicker.SelectedDate.Value;
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
        //  Получение коллекции организаторов каналов.
        if (Node is ChannelOrganizerCollection organizers)
        {
            //  Установка времени.
            organizers.BeginTime = DateTime.FromOADate(_BeginTimeSlider.Value);
        }
    }
}
