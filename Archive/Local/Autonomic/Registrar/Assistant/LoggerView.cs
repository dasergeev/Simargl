using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет элемент управления, отображающий журнал сообщений.
    /// </summary>
    public partial class LoggerView : UserControl
    {
        /// <summary>
        /// Поле для хранения отслеживателя самописца.
        /// </summary>
        private readonly Logging.LoggerTracker _LoggerTracker;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public LoggerView()
        {
            _LoggerTracker = new Logging.LoggerTracker("Assistant");
            InitializeComponent();
            _LoggerTracker.NewMessage += LoggerTracker_NewMessage;


            Type type = _ListView.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(_ListView, true, null);
        }

        /// <summary>
        /// Обрабатывает событие поступления нового сообщения.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void LoggerTracker_NewMessage(object sender, Logging.LoggerTrackerEventArgs e)
        {
            _ListView.Items.Add(e.Message);
            while (_ListView.Items.Count > Logging.Logger.LineCount)
            {
                _ListView.Items.RemoveAt(0);
            }
            _ListView.EnsureVisible(_ListView.Items.Count - 1);
        }

        /// <summary>
        /// Обрабатывает событие таймера.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            _LoggerTracker.MakeStep();
        }

        private void ListView_Resize(object sender, EventArgs e)
        {
            if (_ListView.Width > 0)
            {
                _ColumnHeader.Width = _ListView.Width - 24;
            }
        }
    }
}
