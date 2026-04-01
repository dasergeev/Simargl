using System.Collections.ObjectModel; // Подключает коллекции для привязки данных к интерфейсу.
using System.ComponentModel; // Подключает уведомления об изменении свойств.
using System.Globalization; // Подключает форматирование чисел и дат.
using System.Runtime.CompilerServices; // Подключает CallerMemberName для уведомлений о свойствах.
using System.Text; // Подключает сборку строк для сообщений состояния.
using System.Windows; // Подключает базовые типы WPF.
using System.Windows.Controls; // Подключает элементы управления WPF.
using System.Windows.Input; // Подключает обработку ввода мыши и drag-and-drop.
using System.Windows.Media; // Подключает графические типы WPF.
using System.Windows.Shapes; // Подключает графические примитивы WPF для отрисовки линий.
using Microsoft.Win32; // Подключает системный диалог открытия файла.

namespace Simargl.Viewer; // Определяет пространство имён приложения.

/// <summary>
/// Представляет главное окно приложения просмотра файлов регистрации TESTLAB.
/// </summary>
partial class MainWindow // Определяет класс главного окна приложения.
{
    private const double PlotLeftPadding = 52d; // Задаёт левый внутренний отступ области графика.
    private const double PlotTopPadding = 12d; // Задаёт верхний внутренний отступ области графика.
    private const double PlotRightPadding = 16d; // Задаёт правый внутренний отступ области графика.
    private const double PlotBottomPadding = 28d; // Задаёт нижний внутренний отступ области графика.

    private static readonly Color[] ChannelPalette = // Хранит палитру цветов для линий каналов.
    [
        Color.FromRgb(0x00, 0x78, 0xD4), // Определяет синий цвет для первого канала.
        Color.FromRgb(0xE8, 0x4B, 0x3C), // Определяет красный цвет для второго канала.
        Color.FromRgb(0x10, 0x8C, 0x4A), // Определяет зелёный цвет для третьего канала.
        Color.FromRgb(0xC1, 0x9C, 0x00), // Определяет жёлтый цвет для четвёртого канала.
        Color.FromRgb(0x88, 0x3E, 0xB8), // Определяет фиолетовый цвет для пятого канала.
        Color.FromRgb(0x00, 0x9A, 0xB3), // Определяет бирюзовый цвет для шестого канала.
        Color.FromRgb(0xD1, 0x6B, 0x00), // Определяет оранжевый цвет для седьмого канала.
        Color.FromRgb(0x5C, 0x6B, 0x73), // Определяет графитовый цвет для восьмого канала.
    ]; // Завершает описание палитры.

    private readonly ObservableCollection<ChannelListItem> _channels = []; // Хранит список каналов для правой панели.
    private string? _currentPath; // Хранит путь к последнему открытому файлу.
    private Rect _plotBounds; // Хранит текущие границы рабочей области графика.
    private int _visibleStartIndex; // Хранит первый индекс текущего отображаемого диапазона.
    private int _visiblePointCount; // Хранит число точек текущего отображаемого диапазона.
    private double _visibleMinValue; // Хранит минимум по всем отображаемым каналам в текущем окне.
    private double _visibleMaxValue; // Хранит максимум по всем отображаемым каналам в текущем окне.
    private bool _isUpdatingViewport; // Предотвращает лишние реакции на программное обновление ползунка.

    /// <summary>
    /// Инициализирует новый экземпляр окна.
    /// </summary>
    public MainWindow() // Создаёт и инициализирует главное окно.
    {
        InitializeComponent(); // Загружает XAML-разметку окна.
        ChannelsListBox.ItemsSource = _channels; // Привязывает список каналов к правой панели.
        ResetDocumentView(); // Подготавливает интерфейс к стартовому состоянию без файла.
        SetStatus("Откройте файл формата TESTLAB или перетащите его в окно."); // Показывает стартовую подсказку.
    }

    /// <summary>
    /// Открывает диалог выбора файла.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void OpenFileButton_Click(object sender, RoutedEventArgs e) // Обрабатывает нажатие кнопки открытия файла.
    {
        var dialog = new OpenFileDialog // Создаёт системный диалог выбора файла.
        {
            Title = "Открытие файла регистрации TESTLAB", // Задаёт заголовок диалога.
            Filter = "Файлы регистрации|*.*", // Разрешает выбрать произвольный файл регистрации.
            CheckFileExists = true, // Требует существования выбранного файла.
            Multiselect = false, // Разрешает выбирать только один файл.
        }; // Завершает инициализацию диалога.

        if (dialog.ShowDialog(this) == true) // Проверяет, подтвердил ли пользователь выбор файла.
        {
            LoadDocument(dialog.FileName); // Загружает выбранный файл.
        }
    }

    /// <summary>
    /// Повторно открывает последний выбранный файл.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void ReloadFileButton_Click(object sender, RoutedEventArgs e) // Обрабатывает нажатие кнопки повторного открытия.
    {
        if (string.IsNullOrWhiteSpace(_currentPath)) // Проверяет наличие пути к последнему открытому файлу.
        {
            return; // Завершает обработку, если повторно открывать нечего.
        }

        LoadDocument(_currentPath); // Повторно загружает текущий файл.
    }

    /// <summary>
    /// Обрабатывает включение или выключение канала в списке.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void ChannelVisibilityCheckBox_Click(object sender, RoutedEventArgs e) // Реагирует на изменение флажка канала.
    {
        UpdateViewportState(); // Пересчитывает параметры окна просмотра для нового набора каналов.
        RefreshPlot(); // Перерисовывает график с новым набором линий.
        UpdateSelectionStatus(); // Обновляет строку состояния с числом выбранных каналов.
    }

    /// <summary>
    /// Обрабатывает смену режима отображения значений.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void DisplayModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) // Реагирует на изменение режима Raw или Actual.
    {
        UpdateViewportState(); // Пересчитывает окно просмотра для нового режима.
        RefreshPlot(); // Перерисовывает график для нового режима.
    }

    /// <summary>
    /// Обрабатывает смену размера окна просмотра.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void WindowSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) // Реагирует на изменение размера окна просмотра.
    {
        UpdateViewportState(); // Пересчитывает параметры ползунка и диапазона.
        RefreshPlot(); // Перерисовывает график с новым окном.
    }

    /// <summary>
    /// Обрабатывает изменение положения ползунка просмотра.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void ViewportSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // Реагирует на прокрутку диапазона по оси X.
    {
        if (ViewportSlider is null) // Проверяет, инициализирован ли ползунок окна.
        {
            return; // Игнорирует раннее событие инициализации.
        }

        if (_isUpdatingViewport) // Проверяет, не обновляется ли ползунок программно.
        {
            return; // Завершает обработку, чтобы не создавать лишнюю перерисовку.
        }

        RefreshPlot(); // Перерисовывает график для нового смещения диапазона.
    }

    /// <summary>
    /// Обрабатывает изменение размеров поверхности графика.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void PlotCanvas_SizeChanged(object sender, SizeChangedEventArgs e) // Реагирует на изменение размеров области рисования.
    {
        RefreshPlot(); // Перестраивает график с новым масштабом поверхности.
    }

    /// <summary>
    /// Показывает значения каналов под курсором.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void PlotCanvas_MouseMove(object sender, MouseEventArgs e) // Реагирует на перемещение курсора над графиком.
    {
        var visibleChannels = GetVisibleChannels(); // Получает набор отображаемых каналов.
        if (visibleChannels.Count == 0 || _visiblePointCount <= 0) // Проверяет наличие каналов и данных на графике.
        {
            HoverValueTextBlock.Text = "—"; // Сбрасывает подсказку при отсутствии данных.
            return; // Завершает обработку.
        }

        if (_plotBounds.Width <= 0d || _plotBounds.Height <= 0d) // Проверяет корректность геометрии области графика.
        {
            HoverValueTextBlock.Text = "—"; // Сбрасывает подсказку при невозможности вычислений.
            return; // Завершает обработку.
        }

        var position = e.GetPosition(PlotCanvas); // Получает координаты курсора относительно поверхности графика.
        if (!_plotBounds.Contains(position)) // Проверяет, находится ли курсор внутри рабочей области графика.
        {
            HoverValueTextBlock.Text = "—"; // Скрывает подсказку вне графика.
            return; // Завершает обработку.
        }

        var relativeX = (position.X - _plotBounds.Left) / _plotBounds.Width; // Вычисляет относительное положение курсора по оси X.
        var pointIndex = _visibleStartIndex + (int)Math.Round(relativeX * Math.Max(_visiblePointCount - 1, 0)); // Находит ближайший индекс точки.
        pointIndex = Math.Clamp(pointIndex, 0, Math.Max(GetReferencePointCount() - 1, 0)); // Ограничивает индекс допустимым диапазоном.

        var builder = new StringBuilder(); // Создаёт строитель текста для подсказки по курсору.
        builder.Append("i="); // Добавляет префикс индекса точки.
        builder.Append(pointIndex.ToString(CultureInfo.CurrentCulture)); // Добавляет индекс точки.

        foreach (var item in visibleChannels) // Проходит по всем отображаемым каналам.
        {
            var values = GetDisplayedValues(item.Channel); // Получает значения текущего канала для выбранного режима.
            if (pointIndex >= values.Length) // Проверяет наличие точки в текущем канале.
            {
                continue; // Пропускает канал, если его длины не хватает.
            }

            builder.Append(" | "); // Добавляет разделитель между значениями каналов.
            builder.Append(item.Name); // Добавляет имя канала.
            builder.Append('='); // Добавляет разделитель имени и значения.
            builder.Append(values[pointIndex].ToString("G9", CultureInfo.CurrentCulture)); // Добавляет значение канала.
        }

        HoverValueTextBlock.Text = builder.ToString(); // Показывает собранную строку со значениями под курсором.
    }

    /// <summary>
    /// Скрывает подсказку при уходе курсора с графика.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void PlotCanvas_MouseLeave(object sender, MouseEventArgs e) // Реагирует на уход курсора с поверхности графика.
    {
        HoverValueTextBlock.Text = "—"; // Очищает текст подсказки курсора.
    }

    /// <summary>
    /// Настраивает эффект drag-and-drop при перемещении файла над окном.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void Window_DragOver(object sender, DragEventArgs e) // Реагирует на перенос файла над окном.
    {
        e.Effects = IsSingleFileDrop(e) ? DragDropEffects.Copy : DragDropEffects.None; // Показывает допустимость операции перетаскивания.
        e.Handled = true; // Отмечает событие как обработанное.
    }

    /// <summary>
    /// Открывает файл, перетащенный в окно.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы события.</param>
    private void Window_Drop(object sender, DragEventArgs e) // Реагирует на отпускание файла над окном.
    {
        if (!IsSingleFileDrop(e)) // Проверяет корректность перетаскиваемого содержимого.
        {
            return; // Завершает обработку для неподдерживаемого объекта.
        }

        var paths = (string[])e.Data.GetData(DataFormats.FileDrop)!; // Получает список путей перетащенных файлов.
        LoadDocument(paths[0]); // Загружает единственный переданный файл.
    }

    /// <summary>
    /// Проверяет, что в drag-and-drop передан ровно один существующий файл.
    /// </summary>
    /// <param name="e">Аргументы drag-and-drop.</param>
    /// <returns><see langword="true"/>, если передан один существующий файл; иначе <see langword="false"/>.</returns>
    private static bool IsSingleFileDrop(DragEventArgs e) // Проверяет допустимость операции drag-and-drop.
    {
        if (!e.Data.GetDataPresent(DataFormats.FileDrop)) // Проверяет наличие списка файлов в объекте данных.
        {
            return false; // Возвращает отрицательный результат, если файлов нет.
        }

        var paths = e.Data.GetData(DataFormats.FileDrop) as string[]; // Приводит объект данных к массиву путей.
        return paths is [var path] && File.Exists(path); // Возвращает признак наличия одного существующего файла.
    }

    /// <summary>
    /// Загружает документ TESTLAB и обновляет интерфейс.
    /// </summary>
    /// <param name="path">Путь к файлу.</param>
    private void LoadDocument(string path) // Открывает и читает файл регистрации.
    {
        try // Выполняет чтение файла с обработкой диагностических ошибок.
        {
            Mouse.OverrideCursor = Cursors.Wait; // Показывает пользователю состояние ожидания.
            var document = TestLabReader.Read(path); // Читает файл по спецификации TESTLAB.
            _currentPath = path; // Запоминает путь к текущему файлу.
            _channels.Clear(); // Очищает ранее загруженный список каналов.

            for (var index = 0; index < document.Channels.Count; index++) // Проходит по всем каналам документа.
            {
                _channels.Add(new ChannelListItem(index, document.Channels[index], CreateChannelBrush(index))); // Добавляет канал в список с назначенным цветом.
            }

            if (_channels.Count > 0) // Проверяет наличие каналов в документе.
            {
                _channels[0].IsSelected = true; // Включает первый канал по умолчанию.
                ChannelsListBox.ScrollIntoView(_channels[0]); // Прокручивает список к первому каналу.
            }

            ReloadFileButton.IsEnabled = true; // Разрешает повторное открытие файла.
            CurrentFileTextBlock.Text = document.FilePath; // Показывает полный путь к текущему файлу.
            UpdateViewportState(); // Настраивает диапазон просмотра для текущего набора каналов.
            RefreshPlot(); // Перерисовывает график для выбранных каналов.
            UpdateSelectionStatus(); // Обновляет строку состояния для загруженного документа.
        }
        catch (TestLabFormatException ex) // Обрабатывает ошибки несоответствия формату TESTLAB.
        {
            ResetDocumentView(); // Возвращает окно в состояние без открытого файла.
            SetStatus($"Ошибка формата файла: {ex.Message}"); // Показывает причину ошибки в строке состояния.
            MessageBox.Show(this, ex.Message, "Ошибка формата файла", MessageBoxButton.OK, MessageBoxImage.Error); // Показывает пользователю подробное сообщение об ошибке.
        }
        catch (Exception ex) // Обрабатывает прочие ошибки открытия и чтения файла.
        {
            ResetDocumentView(); // Сбрасывает интерфейс после неудачной загрузки.
            SetStatus($"Ошибка открытия файла: {ex.Message}"); // Показывает причину общей ошибки в строке состояния.
            MessageBox.Show(this, ex.Message, "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error); // Показывает пользователю подробное сообщение об ошибке.
        }
        finally // Завершает операцию открытия файла.
        {
            Mouse.OverrideCursor = null; // Восстанавливает обычный курсор.
        }
    }

    /// <summary>
    /// Сбрасывает отображение документа и графика.
    /// </summary>
    private void ResetDocumentView() // Подготавливает интерфейс к состоянию без данных.
    {
        _currentPath = null; // Очищает путь к текущему файлу.
        _channels.Clear(); // Очищает список каналов.
        _visibleStartIndex = 0; // Сбрасывает стартовый индекс диапазона.
        _visiblePointCount = 0; // Сбрасывает число точек видимого окна.
        _visibleMinValue = 0d; // Сбрасывает минимум видимого диапазона.
        _visibleMaxValue = 0d; // Сбрасывает максимум видимого диапазона.
        CurrentFileTextBlock.Text = "Файл не открыт."; // Сбрасывает отображение пути к файлу.
        VisibleRangeTextBlock.Text = "—"; // Сбрасывает отображаемый диапазон индексов.
        XStartScaleTextBlock.Text = "—"; // Сбрасывает левую подпись шкалы X.
        XEndScaleTextBlock.Text = "—"; // Сбрасывает правую подпись шкалы X.
        YTopScaleTextBlock.Text = "—"; // Сбрасывает верхнюю подпись шкалы Y.
        YBottomScaleTextBlock.Text = "—"; // Сбрасывает нижнюю подпись шкалы Y.
        HoverValueTextBlock.Text = "—"; // Сбрасывает подсказку курсора.
        MinValueTextBlock.Text = "Min: —"; // Сбрасывает минимум текущего окна.
        MaxValueTextBlock.Text = "Max: —"; // Сбрасывает максимум текущего окна.
        PointCountTextBlock.Text = "Точек: —"; // Сбрасывает число точек текущего окна.
        ReloadFileButton.IsEnabled = false; // Отключает кнопку повторного открытия.
        ViewportSlider.IsEnabled = false; // Отключает ползунок просмотра.
        ViewportSlider.Minimum = 0d; // Сбрасывает минимальное значение ползунка.
        ViewportSlider.Maximum = 0d; // Сбрасывает максимальное значение ползунка.
        ViewportSlider.Value = 0d; // Возвращает ползунок в начало.
        if (PlotCanvas is not null) // Проверяет готовность полотна графика после инициализации XAML.
        {
            ClearPlotSeries(); // Удаляет все линии каналов с полотна.
        }

        if (PlotAxisX is not null && PlotAxisY is not null) // Проверяет готовность осей графика после инициализации XAML.
        {
            ResetAxes(); // Сбрасывает оси графика.
        }
    }

    /// <summary>
    /// Обновляет строку состояния на основе текущего выбора каналов.
    /// </summary>
    private void UpdateSelectionStatus() // Формирует краткое сообщение о выбранных каналах.
    {
        if (string.IsNullOrWhiteSpace(_currentPath)) // Проверяет наличие открытого файла.
        {
            SetStatus("Откройте файл формата TESTLAB или перетащите его в окно."); // Возвращает стартовую подсказку.
            return; // Завершает обновление состояния.
        }

        var selectedCount = GetVisibleChannels().Count; // Подсчитывает число включённых каналов.
        SetStatus($"Файл: {_currentPath}. Отображается каналов: {selectedCount.ToString(CultureInfo.CurrentCulture)}."); // Показывает краткую информацию о текущем выборе.
    }

    /// <summary>
    /// Пересчитывает параметры ползунка диапазона по текущим настройкам окна.
    /// </summary>
    private void UpdateViewportState() // Пересчитывает состояние ползунка просмотра графика.
    {
        if (!ArePlotControlsReady()) // Проверяет готовность элементов графика и шкал.
        {
            return; // Завершает обработку до завершения подключения элементов XAML.
        }

        var referencePointCount = GetReferencePointCount(); // Получает опорную длину диапазона по всем выбранным каналам.
        if (referencePointCount <= 0) // Проверяет наличие данных для просмотра.
        {
            _visibleStartIndex = 0; // Сбрасывает стартовый индекс диапазона.
            _visiblePointCount = 0; // Сбрасывает число видимых точек.
            _isUpdatingViewport = true; // Блокирует реакцию на программные изменения ползунка.
            ViewportSlider.Minimum = 0d; // Устанавливает минимальную позицию ползунка.
            ViewportSlider.Maximum = 0d; // Устанавливает максимальную позицию ползунка.
            ViewportSlider.Value = 0d; // Возвращает ползунок в начало.
            ViewportSlider.IsEnabled = false; // Отключает ползунок при отсутствии данных.
            _isUpdatingViewport = false; // Разрешает обычную обработку событий ползунка.
            return; // Завершает обновление состояния.
        }

        var windowPointCount = GetRequestedWindowSize(referencePointCount); // Определяет размер отображаемого окна.
        var maximumStartIndex = Math.Max(referencePointCount - windowPointCount, 0); // Вычисляет максимальное допустимое смещение окна.
        _isUpdatingViewport = true; // Блокирует реакцию на программные изменения ползунка.
        ViewportSlider.Minimum = 0d; // Устанавливает минимальную позицию ползунка.
        ViewportSlider.Maximum = maximumStartIndex; // Устанавливает максимальную позицию ползунка.
        ViewportSlider.SmallChange = Math.Max(1d, windowPointCount / 20d); // Настраивает шаг небольшой прокрутки.
        ViewportSlider.LargeChange = Math.Max(1d, windowPointCount / 4d); // Настраивает шаг крупной прокрутки.
        ViewportSlider.IsEnabled = referencePointCount > windowPointCount; // Включает ползунок только при наличии прокрутки.
        ViewportSlider.Value = Math.Clamp(ViewportSlider.Value, ViewportSlider.Minimum, ViewportSlider.Maximum); // Ограничивает текущее смещение допустимым диапазоном.
        _isUpdatingViewport = false; // Разрешает обычную обработку событий ползунка.
    }

    /// <summary>
    /// Перерисовывает график выбранных каналов.
    /// </summary>
    private void RefreshPlot() // Перестраивает график с учётом выбранных каналов, режима и размера окна.
    {
        if (!ArePlotControlsReady()) // Проверяет готовность элементов графика и шкал.
        {
            return; // Завершает обработку до полной инициализации XAML.
        }

        var visibleChannels = GetVisibleChannels(); // Получает список каналов, которые нужно отображать.
        if (visibleChannels.Count == 0) // Проверяет наличие включённых каналов.
        {
            ClearPlotSeries(); // Удаляет линии каналов при пустом выборе.
            ResetAxes(); // Сбрасывает оси графика.
            VisibleRangeTextBlock.Text = "—"; // Сбрасывает диапазон по X.
            XStartScaleTextBlock.Text = "—"; // Сбрасывает левую подпись оси X.
            XEndScaleTextBlock.Text = "—"; // Сбрасывает правую подпись оси X.
            YTopScaleTextBlock.Text = "—"; // Сбрасывает верхнюю подпись оси Y.
            YBottomScaleTextBlock.Text = "—"; // Сбрасывает нижнюю подпись оси Y.
            MinValueTextBlock.Text = "Min: —"; // Сбрасывает минимум диапазона.
            MaxValueTextBlock.Text = "Max: —"; // Сбрасывает максимум диапазона.
            PointCountTextBlock.Text = "Точек: —"; // Сбрасывает число точек.
            HoverValueTextBlock.Text = "—"; // Сбрасывает текст подсказки.
            return; // Завершает перерисовку.
        }

        UpdatePlotBounds(); // Пересчитывает границы рабочей области графика.
        if (_plotBounds.Width <= 0d || _plotBounds.Height <= 0d) // Проверяет пригодность области рисования.
        {
            ClearPlotSeries(); // Очищает линии каналов при невозможности рисования.
            ResetAxes(); // Сбрасывает оси до безопасного состояния.
            return; // Завершает перерисовку.
        }

        var referencePointCount = GetReferencePointCount(); // Получает общую длину отображаемых данных.
        if (referencePointCount <= 0) // Проверяет наличие точек в выбранных каналах.
        {
            ClearPlotSeries(); // Удаляет линии каналов при отсутствии данных.
            ResetAxes(); // Сбрасывает оси графика.
            return; // Завершает перерисовку.
        }

        _visiblePointCount = Math.Min(GetRequestedWindowSize(referencePointCount), referencePointCount); // Вычисляет число точек текущего окна.
        _visibleStartIndex = Math.Clamp((int)Math.Round(ViewportSlider.Value), 0, Math.Max(referencePointCount - _visiblePointCount, 0)); // Вычисляет стартовый индекс текущего окна.

        var globalMinValue = double.PositiveInfinity; // Инициализирует общий минимум диапазона.
        var globalMaxValue = double.NegativeInfinity; // Инициализирует общий максимум диапазона.
        var renderedChannels = new List<RenderedChannel>(visibleChannels.Count); // Готовит список серий, которые реально будут нарисованы.

        foreach (var item in visibleChannels) // Проходит по всем включённым каналам.
        {
            var values = GetDisplayedValues(item.Channel); // Получает значения канала в выбранном режиме.
            if (_visibleStartIndex >= values.Length) // Проверяет, попадает ли текущее окно в длину канала.
            {
                continue; // Пропускает канал, если окно выходит за его длину.
            }

            var availablePointCount = Math.Min(_visiblePointCount, values.Length - _visibleStartIndex); // Вычисляет число точек канала внутри текущего окна.
            if (availablePointCount <= 0) // Проверяет наличие точек внутри текущего окна.
            {
                continue; // Пропускает канал без видимых точек.
            }

            var range = GetValueRange(values.AsSpan(_visibleStartIndex, availablePointCount)); // Вычисляет минимум и максимум канала в текущем окне.
            globalMinValue = Math.Min(globalMinValue, range.MinValue); // Обновляет общий минимум видимого диапазона.
            globalMaxValue = Math.Max(globalMaxValue, range.MaxValue); // Обновляет общий максимум видимого диапазона.
            renderedChannels.Add(new RenderedChannel(item, values, availablePointCount)); // Сохраняет канал для последующей отрисовки.
        }

        if (renderedChannels.Count == 0) // Проверяет, нашлись ли каналы с данными внутри текущего окна.
        {
            ClearPlotSeries(); // Очищает линии каналов при отсутствии видимых данных.
            ResetAxes(); // Сбрасывает оси графика.
            HoverValueTextBlock.Text = "—"; // Сбрасывает подсказку курсора.
            return; // Завершает перерисовку.
        }

        if (Math.Abs(globalMaxValue - globalMinValue) < double.Epsilon) // Проверяет вырожденный диапазон по оси Y.
        {
            globalMinValue -= 1d; // Искусственно расширяет диапазон снизу.
            globalMaxValue += 1d; // Искусственно расширяет диапазон сверху.
        }

        _visibleMinValue = globalMinValue; // Сохраняет общий минимум видимого диапазона.
        _visibleMaxValue = globalMaxValue; // Сохраняет общий максимум видимого диапазона.
        ClearPlotSeries(); // Удаляет старые линии каналов перед новой отрисовкой.

        foreach (var renderedChannel in renderedChannels) // Проходит по сериям, подготовленным к отрисовке.
        {
            var polyline = new Polyline // Создаёт линию графика для текущего канала.
            {
                Stroke = renderedChannel.Item.ChannelBrush, // Назначает линии цвет канала.
                StrokeThickness = 1.5d, // Задаёт толщину линии канала.
                Points = CreatePlotPoints(renderedChannel.Values, _visibleStartIndex, renderedChannel.AvailablePointCount, _visiblePointCount, _plotBounds, _visibleMinValue, _visibleMaxValue), // Строит набор точек линии графика.
                SnapsToDevicePixels = true, // Улучшает чёткость линии на экране.
            }; // Завершает инициализацию линии канала.

            PlotCanvas.Children.Add(polyline); // Добавляет линию канала на полотно графика.
        }

        UpdateAxes(); // Обновляет положение осей по текущим границам графика.
        UpdateScaleLabels(); // Обновляет текстовые подписи шкал.
    }

    /// <summary>
    /// Пересчитывает границы рабочей области графика.
    /// </summary>
    private void UpdatePlotBounds() // Обновляет геометрию рабочей области графика.
    {
        if (PlotCanvas is null) // Проверяет готовность полотна графика.
        {
            _plotBounds = Rect.Empty; // Сбрасывает границы области графика при раннем вызове.
            return; // Завершает обработку до полной инициализации XAML.
        }

        var width = Math.Max(PlotCanvas.ActualWidth - PlotLeftPadding - PlotRightPadding, 0d); // Вычисляет доступную ширину области графика.
        var height = Math.Max(PlotCanvas.ActualHeight - PlotTopPadding - PlotBottomPadding, 0d); // Вычисляет доступную высоту области графика.
        _plotBounds = new Rect(PlotLeftPadding, PlotTopPadding, width, height); // Сохраняет вычисленные границы области графика.
    }

    /// <summary>
    /// Возвращает оси графика в безопасное стартовое состояние.
    /// </summary>
    private void ResetAxes() // Возвращает оси графика в безопасное стартовое состояние.
    {
        if (PlotCanvas is null || PlotAxisX is null || PlotAxisY is null) // Проверяет готовность полотна и осей графика.
        {
            return; // Завершает обработку до полной инициализации XAML.
        }

        UpdatePlotBounds(); // Пересчитывает текущие границы рабочей области.
        PlotAxisX.X1 = _plotBounds.Left; // Задаёт начало оси X по горизонтали.
        PlotAxisX.Y1 = _plotBounds.Bottom; // Задаёт начало оси X по вертикали.
        PlotAxisX.X2 = _plotBounds.Right; // Задаёт конец оси X по горизонтали.
        PlotAxisX.Y2 = _plotBounds.Bottom; // Задаёт конец оси X по вертикали.
        PlotAxisY.X1 = _plotBounds.Left; // Задаёт начало оси Y по горизонтали.
        PlotAxisY.Y1 = _plotBounds.Top; // Задаёт начало оси Y по вертикали.
        PlotAxisY.X2 = _plotBounds.Left; // Задаёт конец оси Y по горизонтали.
        PlotAxisY.Y2 = _plotBounds.Bottom; // Задаёт конец оси Y по вертикали.
    }

    /// <summary>
    /// Обновляет положение осей для текущего размера графика.
    /// </summary>
    private void UpdateAxes() // Привязывает оси к текущим границам рабочего поля.
    {
        if (PlotAxisX is null || PlotAxisY is null) // Проверяет готовность осей графика.
        {
            return; // Завершает обработку до полной инициализации XAML.
        }

        PlotAxisX.X1 = _plotBounds.Left; // Задаёт начало оси X по горизонтали.
        PlotAxisX.Y1 = _plotBounds.Bottom; // Задаёт начало оси X по вертикали.
        PlotAxisX.X2 = _plotBounds.Right; // Задаёт конец оси X по горизонтали.
        PlotAxisX.Y2 = _plotBounds.Bottom; // Задаёт конец оси X по вертикали.
        PlotAxisY.X1 = _plotBounds.Left; // Задаёт начало оси Y по горизонтали.
        PlotAxisY.Y1 = _plotBounds.Top; // Задаёт начало оси Y по вертикали.
        PlotAxisY.X2 = _plotBounds.Left; // Задаёт конец оси Y по горизонтали.
        PlotAxisY.Y2 = _plotBounds.Bottom; // Задаёт конец оси Y по вертикали.
    }

    /// <summary>
    /// Удаляет с полотна все линии каналов, сохраняя оси.
    /// </summary>
    private void ClearPlotSeries() // Очищает полотно графика от полилиний каналов.
    {
        if (PlotCanvas is null) // Проверяет готовность полотна графика.
        {
            return; // Завершает обработку до полной инициализации XAML.
        }

        for (var childIndex = PlotCanvas.Children.Count - 1; childIndex >= 0; childIndex--) // Проходит по дочерним элементам полотна в обратном порядке.
        {
            var child = PlotCanvas.Children[childIndex]; // Получает очередной дочерний элемент полотна.
            if (ReferenceEquals(child, PlotAxisX) || ReferenceEquals(child, PlotAxisY)) // Проверяет, не является ли элемент осью графика.
            {
                continue; // Оставляет оси на полотне.
            }

            PlotCanvas.Children.RemoveAt(childIndex); // Удаляет линию канала с полотна.
        }
    }

    /// <summary>
    /// Обновляет подписи шкал по текущему диапазону отображения.
    /// </summary>
    private void UpdateScaleLabels() // Обновляет текстовые подписи осей и статистики окна.
    {
        var visibleEndIndex = _visibleStartIndex + _visiblePointCount - 1; // Вычисляет конечный индекс текущего окна.
        VisibleRangeTextBlock.Text = $"{_visibleStartIndex.ToString(CultureInfo.CurrentCulture)}..{visibleEndIndex.ToString(CultureInfo.CurrentCulture)}"; // Показывает текущий диапазон индексов.
        XStartScaleTextBlock.Text = _visibleStartIndex.ToString(CultureInfo.CurrentCulture); // Показывает начало шкалы X.
        XEndScaleTextBlock.Text = visibleEndIndex.ToString(CultureInfo.CurrentCulture); // Показывает конец шкалы X.
        YTopScaleTextBlock.Text = _visibleMaxValue.ToString("G9", CultureInfo.CurrentCulture); // Показывает верхнюю отметку шкалы Y.
        YBottomScaleTextBlock.Text = _visibleMinValue.ToString("G9", CultureInfo.CurrentCulture); // Показывает нижнюю отметку шкалы Y.
        MinValueTextBlock.Text = $"Min: {_visibleMinValue.ToString("G9", CultureInfo.CurrentCulture)}"; // Показывает минимум текущего окна.
        MaxValueTextBlock.Text = $"Max: {_visibleMaxValue.ToString("G9", CultureInfo.CurrentCulture)}"; // Показывает максимум текущего окна.
        PointCountTextBlock.Text = $"Точек: {_visiblePointCount.ToString(CultureInfo.CurrentCulture)}"; // Показывает число точек текущего окна.
    }

    /// <summary>
    /// Возвращает список включённых каналов.
    /// </summary>
    /// <returns>Список каналов, отмеченных для отображения.</returns>
    private List<ChannelListItem> GetVisibleChannels() // Собирает текущий набор отображаемых каналов.
    {
        var result = new List<ChannelListItem>(_channels.Count); // Готовит список выбранных каналов.

        foreach (var item in _channels) // Проходит по всем каналам правой панели.
        {
            if (!item.IsSelected) // Проверяет, отмечен ли текущий канал.
            {
                continue; // Пропускает невыбранный канал.
            }

            result.Add(item); // Добавляет выбранный канал в результат.
        }

        return result; // Возвращает список выбранных каналов.
    }

    /// <summary>
    /// Возвращает максимальную длину среди отображаемых каналов.
    /// </summary>
    /// <returns>Опорное число точек для текущего окна просмотра.</returns>
    private int GetReferencePointCount() // Определяет общую длину окна по выбранным каналам.
    {
        var pointCount = 0; // Инициализирует длину диапазона нулём.

        foreach (var item in _channels) // Проходит по всем каналам списка.
        {
            if (!item.IsSelected) // Проверяет, включён ли текущий канал.
            {
                continue; // Пропускает невыбранный канал.
            }

            pointCount = Math.Max(pointCount, GetDisplayedValues(item.Channel).Length); // Обновляет общую длину по самому длинному каналу.
        }

        return pointCount; // Возвращает найденную длину диапазона.
    }

    /// <summary>
    /// Возвращает массив данных, который должен использоваться для графика.
    /// </summary>
    /// <param name="channel">Канал для отображения.</param>
    /// <returns>Массив значений выбранного режима отображения.</returns>
    private double[] GetDisplayedValues(TestLabChannel channel) // Возвращает массив данных текущего режима.
    {
        return GetSelectedDisplayMode() == ChannelDisplayMode.Raw // Проверяет текущий режим отображения значений.
            ? channel.RawValues // Возвращает исходные значения канала.
            : channel.ActualValues; // Возвращает фактические значения канала.
    }

    /// <summary>
    /// Возвращает размер окна просмотра с учётом настройки пользователя.
    /// </summary>
    /// <param name="totalPointCount">Общее число точек в опорном диапазоне.</param>
    /// <returns>Количество точек, которое должно быть показано на графике.</returns>
    private int GetRequestedWindowSize(int totalPointCount) // Определяет число точек в отображаемом окне.
    {
        if (totalPointCount <= 0) // Проверяет наличие точек в опорном диапазоне.
        {
            return 0; // Возвращает нулевой размер окна для пустого диапазона.
        }

        if (WindowSizeComboBox.SelectedItem is not ComboBoxItem selectedItem) // Проверяет наличие выбранного пункта окна просмотра.
        {
            return totalPointCount; // Возвращает весь диапазон, если выбор ещё не инициализирован.
        }

        if (!int.TryParse(selectedItem.Tag?.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var requestedPointCount)) // Пытается разобрать значение окна из тега.
        {
            return totalPointCount; // Возвращает весь диапазон при ошибке разбора.
        }

        if (requestedPointCount <= 0) // Проверяет признак отображения всего диапазона.
        {
            return totalPointCount; // Возвращает все точки диапазона.
        }

        return Math.Min(requestedPointCount, totalPointCount); // Ограничивает окно общей длиной диапазона.
    }

    /// <summary>
    /// Возвращает выбранный режим отображения графика.
    /// </summary>
    /// <returns>Текущий режим отображения данных.</returns>
    private ChannelDisplayMode GetSelectedDisplayMode() // Определяет текущий режим отображения сигнала.
    {
        if (DisplayModeComboBox.SelectedItem is not ComboBoxItem selectedItem) // Проверяет наличие выбранного режима в комбобоксе.
        {
            return ChannelDisplayMode.Actual; // Возвращает безопасный режим по умолчанию.
        }

        return string.Equals(selectedItem.Tag?.ToString(), "Raw", StringComparison.Ordinal) // Проверяет тег выбранного режима.
            ? ChannelDisplayMode.Raw // Возвращает режим исходных значений.
            : ChannelDisplayMode.Actual; // Возвращает режим фактических значений.
    }

    /// <summary>
    /// Вычисляет минимальное и максимальное значение в диапазоне.
    /// </summary>
    /// <param name="values">Массив значений текущего окна.</param>
    /// <returns>Кортеж с минимальным и максимальным значением диапазона.</returns>
    private static (double MinValue, double MaxValue) GetValueRange(ReadOnlySpan<double> values) // Вычисляет диапазон значений на участке сигнала.
    {
        var minimumValue = values[0]; // Инициализирует минимум первым значением диапазона.
        var maximumValue = values[0]; // Инициализирует максимум первым значением диапазона.

        for (var index = 1; index < values.Length; index++) // Проходит по остальным значениям диапазона.
        {
            var value = values[index]; // Получает очередное значение диапазона.
            if (value < minimumValue) // Проверяет обновление минимума.
            {
                minimumValue = value; // Сохраняет новый минимум диапазона.
            }

            if (value > maximumValue) // Проверяет обновление максимума.
            {
                maximumValue = value; // Сохраняет новый максимум диапазона.
            }
        }

        return (minimumValue, maximumValue); // Возвращает вычисленные границы диапазона.
    }

    /// <summary>
    /// Строит набор экранных точек для отображения линии канала.
    /// </summary>
    /// <param name="values">Полный массив значений канала.</param>
    /// <param name="startIndex">Начальный индекс отображаемого окна.</param>
    /// <param name="availablePointCount">Число точек канала в текущем окне.</param>
    /// <param name="windowPointCount">Общее число точек текущего окна по оси X.</param>
    /// <param name="plotBounds">Границы области графика.</param>
    /// <param name="minValue">Минимальное значение диапазона.</param>
    /// <param name="maxValue">Максимальное значение диапазона.</param>
    /// <returns>Коллекция экранных точек линии графика.</returns>
    private static PointCollection CreatePlotPoints(double[] values, int startIndex, int availablePointCount, int windowPointCount, Rect plotBounds, double minValue, double maxValue) // Преобразует данные канала в набор точек полилинии.
    {
        var points = new PointCollection(); // Создаёт результирующую коллекцию точек графика.
        if (availablePointCount <= 0 || windowPointCount <= 0 || plotBounds.Width <= 0d || plotBounds.Height <= 0d) // Проверяет пригодность входных параметров.
        {
            return points; // Возвращает пустую коллекцию при отсутствии отображаемых данных.
        }

        var pixelWidth = Math.Max(plotBounds.Width, 1d); // Определяет доступную ширину графика в пикселях.
        if (availablePointCount <= pixelWidth * 1.5d) // Проверяет возможность прямого отображения всех точек без упрощения.
        {
            for (var dataIndex = startIndex; dataIndex < startIndex + availablePointCount; dataIndex++) // Проходит по всем точкам видимого участка канала.
            {
                points.Add(MapPoint(dataIndex, values[dataIndex], startIndex, windowPointCount, plotBounds, minValue, maxValue)); // Добавляет очередную точку линии графика.
            }

            return points; // Возвращает полный набор точек без упрощения.
        }

        var bucketCount = Math.Max((int)Math.Ceiling(pixelWidth), 1); // Определяет число корзин для упрощения плотного сигнала.
        var bucketSize = (double)availablePointCount / bucketCount; // Вычисляет размер одной корзины в точках данных.

        for (var bucketIndex = 0; bucketIndex < bucketCount; bucketIndex++) // Проходит по всем корзинам упрощения сигнала.
        {
            var from = startIndex + (int)Math.Floor(bucketIndex * bucketSize); // Вычисляет начальный индекс текущей корзины.
            var to = startIndex + (int)Math.Floor((bucketIndex + 1) * bucketSize); // Вычисляет конечный индекс текущей корзины.
            if (from >= startIndex + availablePointCount) // Проверяет выход за границы доступных данных.
            {
                break; // Завершает обработку корзин после конца диапазона.
            }

            to = Math.Min(to, startIndex + availablePointCount); // Ограничивает правую границу корзины концом диапазона.
            if (to <= from) // Проверяет наличие хотя бы одной точки в корзине.
            {
                to = Math.Min(from + 1, startIndex + availablePointCount); // Расширяет пустую корзину до одной точки.
            }

            var minimumIndex = from; // Инициализирует индекс минимума первым элементом корзины.
            var maximumIndex = from; // Инициализирует индекс максимума первым элементом корзины.
            var minimumValue = values[from]; // Инициализирует минимум корзины первым значением.
            var maximumValue = values[from]; // Инициализирует максимум корзины первым значением.

            for (var dataIndex = from + 1; dataIndex < to; dataIndex++) // Проходит по точкам внутри текущей корзины.
            {
                var value = values[dataIndex]; // Получает очередное значение корзины.
                if (value < minimumValue) // Проверяет обновление минимума корзины.
                {
                    minimumValue = value; // Сохраняет новое минимальное значение корзины.
                    minimumIndex = dataIndex; // Сохраняет индекс нового минимума корзины.
                }

                if (value > maximumValue) // Проверяет обновление максимума корзины.
                {
                    maximumValue = value; // Сохраняет новое максимальное значение корзины.
                    maximumIndex = dataIndex; // Сохраняет индекс нового максимума корзины.
                }
            }

            if (minimumIndex <= maximumIndex) // Проверяет естественный порядок следования экстремумов.
            {
                points.Add(MapPoint(minimumIndex, minimumValue, startIndex, windowPointCount, plotBounds, minValue, maxValue)); // Добавляет точку минимума в естественном порядке.
                if (maximumIndex != minimumIndex) // Проверяет различие минимума и максимума корзины.
                {
                    points.Add(MapPoint(maximumIndex, maximumValue, startIndex, windowPointCount, plotBounds, minValue, maxValue)); // Добавляет точку максимума корзины.
                }
            }
            else // Обрабатывает случай, когда максимум встречается раньше минимума.
            {
                points.Add(MapPoint(maximumIndex, maximumValue, startIndex, windowPointCount, plotBounds, minValue, maxValue)); // Добавляет точку максимума первой.
                if (maximumIndex != minimumIndex) // Проверяет различие экстремумов корзины.
                {
                    points.Add(MapPoint(minimumIndex, minimumValue, startIndex, windowPointCount, plotBounds, minValue, maxValue)); // Добавляет точку минимума второй.
                }
            }
        }

        return points; // Возвращает упрощённую коллекцию точек графика.
    }

    /// <summary>
    /// Переводит точку сигнала в координаты области графика.
    /// </summary>
    /// <param name="dataIndex">Индекс значения в исходном массиве.</param>
    /// <param name="value">Значение сигнала.</param>
    /// <param name="startIndex">Начальный индекс отображаемого окна.</param>
    /// <param name="windowPointCount">Общее число точек окна по оси X.</param>
    /// <param name="plotBounds">Границы области графика.</param>
    /// <param name="minValue">Минимальное значение диапазона.</param>
    /// <param name="maxValue">Максимальное значение диапазона.</param>
    /// <returns>Точка в координатах экрана.</returns>
    private static Point MapPoint(int dataIndex, double value, int startIndex, int windowPointCount, Rect plotBounds, double minValue, double maxValue) // Преобразует индекс и значение сигнала в экранную координату.
    {
        var xRatio = windowPointCount <= 1 ? 0d : (double)(dataIndex - startIndex) / (windowPointCount - 1); // Нормализует положение точки по оси X.
        var yRatio = (value - minValue) / (maxValue - minValue); // Нормализует положение точки по оси Y.
        var x = plotBounds.Left + (xRatio * plotBounds.Width); // Вычисляет экранную координату X.
        var y = plotBounds.Bottom - (yRatio * plotBounds.Height); // Вычисляет экранную координату Y с инверсией оси.
        return new Point(x, y); // Возвращает экранную точку графика.
    }

    /// <summary>
    /// Создаёт кисть для очередного канала.
    /// </summary>
    /// <param name="index">Номер канала в документе.</param>
    /// <returns>Кисть, которая будет использоваться для линии и маркера канала.</returns>
    private static SolidColorBrush CreateChannelBrush(int index) // Назначает каналу цвет из циклической палитры.
    {
        var brush = new SolidColorBrush(ChannelPalette[index % ChannelPalette.Length]); // Создаёт кисть из палитры по индексу канала.
        brush.Freeze(); // Замораживает кисть для безопасного повторного использования.
        return brush; // Возвращает готовую кисть канала.
    }

    /// <summary>
    /// Обновляет текст в строке состояния.
    /// </summary>
    /// <param name="message">Сообщение для пользователя.</param>
    private void SetStatus(string message) // Показывает сообщение в строке состояния окна.
    {
        StatusTextBlock.Text = message; // Обновляет текст строки состояния.
    }

    /// <summary>
    /// Описывает режим отображения графика.
    /// </summary>
    private enum ChannelDisplayMode // Перечисляет доступные режимы визуализации сигнала.
    {
        Actual, // Отображает фактические значения после применения формулы масштаба.
        Raw, // Отображает исходные значения из файла без преобразования.
    }

    /// <summary>
    /// Представляет канал, подготовленный к отрисовке на графике.
    /// </summary>
    /// <param name="Item">Элемент списка каналов.</param>
    /// <param name="Values">Массив отображаемых значений канала.</param>
    /// <param name="AvailablePointCount">Число точек канала в текущем окне.</param>
    private sealed record RenderedChannel(ChannelListItem Item, double[] Values, int AvailablePointCount); // Хранит временные данные серии для отрисовки.

    /// <summary>
    /// Представляет элемент списка каналов в правой панели.
    /// </summary>
    private sealed class ChannelListItem : INotifyPropertyChanged // Описывает данные, отображаемые в строке списка каналов.
    {
        private bool _isSelected; // Хранит признак включения канала на графике.

        /// <summary>
        /// Инициализирует новый элемент списка каналов.
        /// </summary>
        /// <param name="index">Индекс канала в документе.</param>
        /// <param name="channel">Исходный канал документа.</param>
        /// <param name="channelBrush">Цвет канала на графике.</param>
        public ChannelListItem(int index, TestLabChannel channel, SolidColorBrush channelBrush) // Создаёт представление канала для списка.
        {
            Channel = channel; // Сохраняет исходный канал для построения графика.
            Index = (index + 1).ToString(CultureInfo.CurrentCulture); // Формирует отображаемый индекс канала с единицы.
            Name = string.IsNullOrWhiteSpace(channel.Description.Name) ? $"Канал {Index}" : channel.Description.Name; // Подготавливает имя канала для списка.
            Description = string.IsNullOrWhiteSpace(channel.Description.Description) ? "Без описания" : channel.Description.Description; // Подготавливает описание канала для списка.
            MetaText = $"Ед.: {DisplayText(channel.Description.Unit)}  |  Формат: {channel.Description.Format}  |  Длина: {channel.Description.Length.ToString(CultureInfo.CurrentCulture)}"; // Формирует краткую служебную строку канала.
            ChannelBrush = channelBrush; // Сохраняет цвет канала для линии и маркера.
        }

        /// <summary>
        /// Возникает при изменении свойства элемента списка.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged; // Позволяет интерфейсу реагировать на изменение состояния канала.

        /// <summary>
        /// Получает исходный канал документа.
        /// </summary>
        public TestLabChannel Channel { get; } // Возвращает исходный канал.

        /// <summary>
        /// Получает отображаемый индекс канала.
        /// </summary>
        public string Index { get; } // Возвращает индекс канала в списке.

        /// <summary>
        /// Получает отображаемое имя канала.
        /// </summary>
        public string Name { get; } // Возвращает имя канала.

        /// <summary>
        /// Получает отображаемое описание канала.
        /// </summary>
        public string Description { get; } // Возвращает описание канала.

        /// <summary>
        /// Получает краткую служебную информацию о канале.
        /// </summary>
        public string MetaText { get; } // Возвращает строку с единицей измерения, форматом и длиной.

        /// <summary>
        /// Получает цвет канала на графике.
        /// </summary>
        public SolidColorBrush ChannelBrush { get; } // Возвращает кисть канала.

        /// <summary>
        /// Получает или задаёт признак отображения канала на графике.
        /// </summary>
        public bool IsSelected // Управляет включением канала в общий график.
        {
            get => _isSelected; // Возвращает текущее состояние включения канала.
            set // Обновляет текущее состояние включения канала.
            {
                if (_isSelected == value) // Проверяет фактическое изменение признака.
                {
                    return; // Завершает обновление без лишнего уведомления.
                }

                _isSelected = value; // Сохраняет новое состояние включения канала.
                OnPropertyChanged(); // Уведомляет интерфейс об изменении свойства.
            }
        }

        /// <summary>
        /// Поднимает уведомление об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя изменённого свойства.</param>
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) // Отправляет уведомление о смене значения свойства.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Вызывает подписчиков события изменения свойства.
        }
    }

    /// <summary>
    /// Возвращает отображаемый текст для строки.
    /// </summary>
    /// <param name="value">Исходное строковое значение.</param>
    /// <returns>Непустое значение либо длинное тире.</returns>
    private static string DisplayText(string value) // Преобразует пустые строки в человекочитаемый маркер отсутствия значения.
    {
        return string.IsNullOrWhiteSpace(value) ? "—" : value; // Возвращает исходный текст или символ отсутствия значения.
    }

    /// <summary>
    /// Проверяет готовность элементов интерфейса, связанных с графиком.
    /// </summary>
    /// <returns><see langword="true"/>, если элементы графика готовы к обновлению; иначе <see langword="false"/>.</returns>
    private bool ArePlotControlsReady() // Проверяет готовность именованных элементов блока графика.
    {
        return PlotCanvas is not null // Проверяет готовность полотна графика.
            && PlotAxisX is not null // Проверяет готовность оси X.
            && PlotAxisY is not null // Проверяет готовность оси Y.
            && ViewportSlider is not null // Проверяет готовность ползунка диапазона.
            && VisibleRangeTextBlock is not null // Проверяет готовность текстового блока диапазона X.
            && XStartScaleTextBlock is not null // Проверяет готовность левой подписи шкалы X.
            && XEndScaleTextBlock is not null // Проверяет готовность правой подписи шкалы X.
            && YTopScaleTextBlock is not null // Проверяет готовность верхней подписи шкалы Y.
            && YBottomScaleTextBlock is not null // Проверяет готовность нижней подписи шкалы Y.
            && MinValueTextBlock is not null // Проверяет готовность блока минимума.
            && MaxValueTextBlock is not null // Проверяет готовность блока максимума.
            && PointCountTextBlock is not null // Проверяет готовность блока числа точек.
            && HoverValueTextBlock is not null; // Проверяет готовность блока подсказки курсора.
    }
}
