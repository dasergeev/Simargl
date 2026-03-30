using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RailTest.Border.Server
{
    /// <summary>
    /// Представляет элемент управления, отображающий информацию об обработке.
    /// </summary>
    public partial class ProcessingControl : UserControl
    {
        /// <summary>
        /// Поле для хранения списка элементов управления, отображающих информацию об оси.
        /// </summary>
        private readonly List<AxisControl> _AxisControls;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="instance">
        /// Экземпляр приложения.
        /// </param>
        public ProcessingControl(Instance instance)
        {
            Instance = instance;
            _AxisControls = new List<AxisControl>();
            InitializeComponent();

            Instance.Started += Instance_Started;
            Instance.Stopped += Instance_Stopped;
        }

        private void Axes_Added(object sender, AxisCollectionEventArgs e)
        {
            Action action = () =>
            {
                AxisControl control = new AxisControl(e.Axis);
                control.Dock = DockStyle.Top;
                Controls.Add(control);
                _AxisControls.Add(control);
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
                Instance.GetEquipment().Processing.Axes.Added += Axes_Added;
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
                foreach (var item in _AxisControls)
                {
                    Controls.Remove(item);
                }
                _AxisControls.Clear();
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

    }
}
