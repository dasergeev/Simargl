using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
public partial class MainForm :
    Form
{
    /// <summary>
    /// Поле для хранения массива элементов строки состояния.
    /// </summary>
    private readonly ToolStripItem[] _StatusItems;

    /// <summary>
    /// Поле для хранения объекта, обновляющего данные графиков сигнала.
    /// </summary>
    private SignalChart? _SignalChart = null;

    /// <summary>
    /// Поле для хранения объекта, обновляющего данные графиков спектров.
    /// </summary>
    private SpectrumChart? _SpectrumChart = null;

    /// <summary>
    /// Поле для хранения объекта, обновляющего данные графиков мощности.
    /// </summary>
    private PowerChart? _PowerChart = null;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public MainForm()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Скрытие элемента управления, отображающего графики.
        _Chart.Visible = false;

        //  Добавление элементов строки состояния.
        _StatusItems =
            [
                _StatusStrip.Items.Add(_ImageList.Images[0]),
                _StatusStrip.Items.Add(_ImageList.Images[1]),
                _StatusStrip.Items.Add("Device"),
                addSeparator(),
                _StatusStrip.Items.Add(_ImageList.Images[0]),
                _StatusStrip.Items.Add(_ImageList.Images[1]),
                _StatusStrip.Items.Add("Device"),
                addSeparator(),
                _StatusStrip.Items.Add(_ImageList.Images[0]),
                _StatusStrip.Items.Add(_ImageList.Images[1]),
                _StatusStrip.Items.Add("Device"),
                addSeparator(),
            ];

        //  Создание ядра приложения.
        Core = new(this, _Timer, _RichTextBox);

        //  Перебор устройств.
        for (int i = 0; i < Core.Devices.Count; i++)
        {
            //  Получение устройства.
            Device device = Core.Devices[i];

            //  Скрытие изображения.
            _StatusItems[4 * i].Visible = false;

            //  Обновление имени устройства.
            _StatusItems[2 + 4 * i].Text = device.Name;

            //  Добавление устройства в дерево.
            TreeNode deviceNode = _TreeView.Nodes.Add(device.Name);
            deviceNode.Tag = device;
            deviceNode.ImageIndex = 2;
            deviceNode.SelectedImageIndex = 2;

            //  Перебор сигналов.
            foreach (AccelEth3TSignal signal in device.Signals)
            {
                //  Добавление сигнала в дерево.
                TreeNode signalNode = deviceNode.Nodes.Add(signal.Name);
                signalNode.Tag = signal;
                signalNode.ImageIndex = 3;
                signalNode.SelectedImageIndex = 3;

                //  Добавление спектра в дерево.
                TreeNode spectrumNode = signalNode.Nodes.Add("Spectrum");
                spectrumNode.Tag = signal.Spectrum;
                spectrumNode.ImageIndex = 4;
                spectrumNode.SelectedImageIndex = 4;

                //  Добавление мощности в дерево.
                TreeNode powerNode = signalNode.Nodes.Add("Power");
                powerNode.Tag = signal.Power;
                powerNode.ImageIndex = 5;
                powerNode.SelectedImageIndex = 5;
            }
        }

        //  Добавляет разделитель в строку состояния.
        ToolStripItem addSeparator()
        {
            //  Создание разделителя.
            ToolStripSeparator separator = new();

            //  Добавление разделителя.
            _StatusStrip.Items.Add(separator);

            //  Возврат разделителя.
            return separator;
        }
    }

    /// <summary>
    /// Возвращает ядро.
    /// </summary>
    public Core Core { get; }

    /// <summary>
    /// Устанавливает флаг активности устройства.
    /// </summary>
    /// <param name="index">
    /// Индекс устройства.
    /// </param>
    /// <param name="active">
    /// Флаг активности устройства.
    /// </param>
    public void SetDeviceActive(int index, bool active)
    {
        //  Настройка видимости изображений.
        _StatusItems[4 * index].Visible = active;
        _StatusItems[4 * index + 1].Visible = !active;
    }

    /// <summary>
    /// Происходит при нажатии клавиши отображения окна вывода.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void RichTextBoxButton_CheckedChanged(object sender, EventArgs e)
    {
        //  Изменение видимости элементов управления.
        _RichTextBoxSplitter.Visible = _RichTextBoxButton.Checked;
        _RichTextBox.Visible = _RichTextBoxButton.Checked;
        _Chart.BringToFront();
    }

    /// <summary>
    /// Происходит при изменении выбранного элемента в дереве.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        //  Остановка объектов.
        stopChart(ref _SignalChart);
        stopChart(ref _SpectrumChart);
        stopChart(ref _PowerChart);

        //  Проверка выбранного узла.
        if (_TreeView.SelectedNode is TreeNode node)
        {
            //  Проверка сигнала.
            if (node.Tag is AccelEth3TSignal signal)
            {
                //  Показ элемента управления, отображающего графики.
                _Chart.Visible = true;

                //  Настройка горизонтальной оси.
                Axis axis = _Chart.ChartAreas[0].AxisX;
                axis.Minimum = -Settings.SignalDuration;
                axis.Maximum = 0;
                axis.Interval = 0.1 * Settings.SignalDuration;

                //  Создание объекта, отображающего сигнал.
                _SignalChart = new(Core, _Chart, signal);

                //  Завершение обработки события.
                return;
            }

            //  Проверка спектра.
            if (node.Tag is AccelEth3TSpectrum spectrum)
            {
                //  Показ элемента управления, отображающего графики.
                _Chart.Visible = true;

                //  Настройка горизонтальной оси.
                Axis axis = _Chart.ChartAreas[0].AxisX;
                axis.Minimum = 0;// -Settings.SignalDuration;
                axis.Maximum = 1000;
                axis.Interval = 100;

                //  Создание объекта, отображающего сигнал.
                _SpectrumChart = new(Core, _Chart, spectrum);

                //  Завершение обработки события.
                return;
            }

            //  Проверка мощности.
            if (node.Tag is AccelEth3TPower power)
            {
                //  Показ элемента управления, отображающего графики.
                _Chart.Visible = true;

                //  Настройка горизонтальной оси.
                Axis axis = _Chart.ChartAreas[0].AxisX;
                axis.Minimum = -Settings.SignalDuration;
                axis.Maximum = 0;
                axis.Interval = 0.1 * Settings.SignalDuration;

                //  Создание объекта, отображающего сигнал.
                _PowerChart = new(Core, _Chart, power);

                //  Завершение обработки события.
                return;
            }
        }

        //  Скрытие элемента управления, отображающего графики.
        _Chart.Visible = false;

        //  Останавливает объект, работающий с графиком.
        static void stopChart<T>(ref T? worker) where T : ChartWorker
        {
            //  Проверка объекта, отображающего спектр.
            if (worker is not null)
            {
                //  Остановка объекта.
                worker.Stop();

                //  Сброс ссылки на объект.
                worker = null;
            }
        }
    }

    /// <summary>
    /// Обрабатывает событие изменения состояния отслеживания.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void StoreButton_CheckedChanged(object sender, EventArgs e)
    {
        //  Изменение состояния.
        Core.Store.Enable = _StoreButton.Checked;

        //  Проверка возможности контроля.
        if (!Core.Store.Enable)
        {
            //  Включение кнопки запуска контроля.
            _ControlButton.Enabled = true;
        }
    }

    /// <summary>
    /// Обрабатывает событие изменения состояния контроля.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void ControlButton_Click(object sender, EventArgs e)
    {
        //  Изменение состояния.
        Core.Controller.Enable = _ControlButton.Checked;
    }
}
