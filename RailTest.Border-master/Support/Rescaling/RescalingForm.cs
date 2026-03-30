using RailTest.Fibers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailTest.Border.Support.Rescaling
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class RescalingForm : Form
    {
        /// <summary>
        /// Поле для хранения нити.
        /// </summary>
        readonly Fiber _Fiber;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public RescalingForm()
        {
            InitializeComponent();
            Output = _OutputView.Output;
            _Fiber = new Fiber(FiberEntryPoint);
            _Fiber.Started += Fiber_Started;
            _Fiber.Stopped += Fiber_Stopped;
            _Fiber.Failed += Fiber_Stopped;
        }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output { get; }

        /// <summary>
        /// Представляет точку входа в приложение.
        /// </summary>
        /// <param name="context">
        /// Контекст волокна.
        /// </param>
        private void FiberEntryPoint(FiberContext context)
        {
            //Invoke(new Action(() =>
            //{

            //}));

            while (context.IsWork)
            {
                if (!context.IsWork)
                {
                    break;
                }
                Output.WriteLine("Loop");
                Thread.Sleep(100);
            }

            //Invoke(new Action(() =>
            //{

            //}));
        }

        /// <summary>
        /// Обрабатывает событие запуска обработки.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, свзязанные с событием.
        /// </param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                _Fiber.Start();
            }
            catch (InvalidOperationException)
            {

            }
        }

        /// <summary>
        /// Обрабатывает событие остановки обработки.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, свзязанные с событием.
        /// </param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            _Fiber.Stop(10000);
        }

        /// <summary>
        /// Обрабатывает событие запуска волокна.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, свзязанные с событием.
        /// </param>
        private void Fiber_Started(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                _StartButton.Enabled = false;
                _StopButton.Enabled = true;
            }));
        }

        /// <summary>
        /// Обрабатывает событие остановки волокна.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, свзязанные с событием.
        /// </param>
        private void Fiber_Stopped(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                _StartButton.Enabled = true;
                _StopButton.Enabled = false;
            }));
        }
    }
}
