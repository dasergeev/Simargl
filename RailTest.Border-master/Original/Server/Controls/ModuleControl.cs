using System;
using System.Windows.Forms;
using System.Reflection;

namespace RailTest.Border.Server
{
    /// <summary>
    /// Представляет элемент управления, отображающий информацию о модулях.
    /// </summary>
    public partial class ModuleControl : UserControl
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="instance">
        /// Экземпляр приложения.
        /// </param>
        public ModuleControl(Instance instance)
        {
            Instance = instance;
            InitializeComponent();

            Type type = _ListView.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(_ListView, true, null);

            Instance.Started += Instance_Started;
            Instance.Stopped += Instance_Stopped;
        }

        /// <summary>
        /// Возвращает экземпляр приложения.
        /// </summary>
        public Instance Instance { get; }

        /// <summary>
        /// Обрабатывает событие <see cref="Instance.Started"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Instance_Started(object sender, EventArgs e)
        {
            Action action = () =>
            {
                var modules = Instance.GetEquipment().Modules;
                for (int i = 0; i != modules.Count; ++i)
                {
                    var module = modules[i];
                    var item = _ListView.Items.Add(i.ToString("00"));
                    item.ImageIndex = 0;
                    item.Tag = module;
                    item.SubItems.Add(module.Address);
                    item.SubItems.Add("Не подключен");
                    item.SubItems.Add("0");
                    item.SubItems.Add("0");
                    item.SubItems.Add("0");
                    item.SubItems.Add("0");
                }
                _Timer.Enabled = true;
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Обрабатывает событие <see cref="Instance.Stopped"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Instance_Stopped(object sender, EventArgs e)
        {
            Action action = () =>
            {
                _Timer.Enabled = false;
                _ListView.Items.Clear();
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in _ListView.Items)
            {
                var module = item.Tag as Module;
                if (module.Connected)
                {
                    item.SubItems[2].Text = "Подключен";
                    item.ImageIndex = 2;
                }
                else
                {
                    item.SubItems[2].Text = "Не подключен";
                    item.ImageIndex = 0;
                }
                item.SubItems[3].Text = module.Samples.ToString();
                item.SubItems[4].Text = module.Blocks.ToString();
                item.SubItems[5].Text = module.Marker.ToString();
                item.SubItems[6].Text = module.BlockIndex.ToString();
            }
        }
    }
}
