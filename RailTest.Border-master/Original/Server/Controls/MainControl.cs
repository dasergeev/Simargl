using System.Windows.Forms;
using RailTest.Controls;

namespace RailTest.Border.Server
{
    /// <summary>
    /// Представляет главный элемент управления.
    /// </summary>
    public partial class MainControl : UserControl
    {
        /// <summary>
        /// Поле для хранения элемента, отображающего текстовую информацию.
        /// </summary>
        private readonly OutputView _OutputView;

        /// <summary>
        /// Поле для хранения элемента управления, отображающего информацию о модулях.
        /// </summary>
        private readonly ModuleControl _ModuleControl;

        /// <summary>
        /// Поле для хранения элемента управления, на котором отображаются осцилограммы.
        /// </summary>
        private readonly OscillogramControl _OscillogramControl;

        /// <summary>
        /// Поле для хранения элемента управления, отображающего информацию об обработке.
        /// </summary>
        private readonly ProcessingControl _ProcessingControl;

        /// <summary>
        /// Поле для хранения массива элементов управления.
        /// </summary>
        private readonly Control[] _Controls;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="mainForm">
        /// Главное окно приложения.
        /// </param>
        public MainControl(MainForm mainForm)
        {
            MainForm = mainForm;

            InitializeComponent();

            _OutputView = new OutputView
            {
                Dock = DockStyle.Fill
            };
            _OutputPanel.Controls.Add(_OutputView);

            Instance = new Instance(this);

            _ModuleControl = new ModuleControl(Instance);
            _OscillogramControl = new OscillogramControl(Instance);
            _ProcessingControl = new ProcessingControl(Instance);

            _TreeView.Nodes.Add("Модули").Tag = _ModuleControl;
            _TreeView.Nodes.Add("Осциллограммы").Tag = _OscillogramControl;
            _TreeView.Nodes.Add("Обработка").Tag = _ProcessingControl;

            _Controls = new Control[] { _ModuleControl, _OscillogramControl, _ProcessingControl };
            foreach (var control in _Controls)
            {
                control.Visible = false;
                control.Dock = DockStyle.Fill;
                _WorkPanel.Controls.Add(control);
            }
        }

        /// <summary>
        /// Возвращает главное окно приложения.
        /// </summary>
        public MainForm MainForm { get; }

        /// <summary>
        /// Возвращает экземпляр приложения.
        /// </summary>
        public Instance Instance { get; }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output
        {
            get
            {
                return _OutputView.Output;
            }
        }
        
        /// <summary>
        /// Возвращает диаграмму.
        /// </summary>
        public System.Windows.Forms.DataVisualization.Charting.Chart Chart
        {
            get
            {
                return _OscillogramControl.Chart;
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (var control in _Controls)
            {
                control.Visible = false;
            }
            var node = _TreeView.SelectedNode;
            if (node is object)
            {
                var control = node.Tag as Control;
                if (control is object)
                {
                    control.Visible = true;
                }
            }
        }
    }
}
