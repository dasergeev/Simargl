using RailTest.Satellite.Autonomic.Registrar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class AssistantForm : Form
    {
        /// <summary>
        /// Поле для хранения элемента управления, отображающего осциллограммы.
        /// </summary>
        readonly OscillogramView _OscillogramView;

        /// <summary>
        /// Поле для хранения элемента управления, отображающего сообщения журнала.
        /// </summary>
        readonly LoggerView _LoggerView;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public AssistantForm()
        {
            InitializeComponent();

            _OscillogramView = new OscillogramView { Dock = DockStyle.Fill };
            _OscillogramPanel.Controls.Add(_OscillogramView);

            _LoggerView = new LoggerView { Dock = DockStyle.Fill };
            _LoggerPanel.Controls.Add(_LoggerView);
        }
    }
}
