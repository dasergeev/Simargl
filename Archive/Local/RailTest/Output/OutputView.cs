using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static RailTest.OutputView;

namespace RailTest.Controls
{
    /// <summary>
    /// Представляет элемент управления, отображающий окно вывода текстовой информации.
    /// </summary>
    public partial class OutputView : Control
    {
        /// <summary>
        /// Поле для хранения элемента управления, отображающего форматированный текст.
        /// </summary>
        private readonly RichTextBox _RichTextBox;

        /// <summary>
        /// Поле для хранения потока, в котором осуществляется вывод текстовой информации.
        /// </summary>
        private Thread _Thread;

        /// <summary>
        /// Поле для хранения очереди действий.
        /// </summary>
        private readonly ConcurrentQueue<Action> _Actions;

        /// <summary>
        /// Поля для хранения средства вывода текстовой информации.
        /// </summary>
        private OutputRelease _Output;
        
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public OutputView()
        {
            _Actions = new ConcurrentQueue<Action>();
            _RichTextBox = new RichTextBox();

            _RichTextBox.HandleCreated += RichTextBox_HandleCreated;
            _RichTextBox.HandleDestroyed += RichTextBox_HandleDestroyed;

            _RichTextBox.BackColor = Color.White;
            _RichTextBox.BorderStyle = BorderStyle.None;
            _RichTextBox.Font = new Font("Consolas", 10);
            _RichTextBox.Dock = DockStyle.Fill;
            _RichTextBox.AcceptsTab = true;
            _RichTextBox.DetectUrls = false;
            _RichTextBox.HideSelection = false;
            _RichTextBox.ReadOnly = true;
            _RichTextBox.ShowSelectionMargin = true;
            _RichTextBox.WordWrap = false;

            int[] tabs = new int[32];
            for (int i = 0; i != 32; ++i)
            {
                tabs[i] = (i + 1) * 28;
            }

            _RichTextBox.SelectionTabs = tabs;
            _RichTextBox.ScrollBars = RichTextBoxScrollBars.ForcedBoth;

            Controls.Add(_RichTextBox);

            _Output = new OutputRelease(this);
        }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output
        {
            get
            {
                return _Output;
            }
        }
        
        /// <summary>
        /// Удаляет весь текст из текстового поля.
        /// </summary>
        internal void Clear()
        {
            _Actions.Enqueue(() => _RichTextBox.Clear());
        }

        /// <summary>
        /// Добавляет текст в конец текущего содержимого в текстовом поле.
        /// </summary>
        /// <param name="text">
        /// Текст, добавляемый в конец текущего содержимого в текстовом поле.
        /// </param>
        internal void AppendText(string text)
        {
            _Actions.Enqueue(() => _RichTextBox.AppendText(text));
        }

        /// <summary>
        /// Добаляет текст в конец текущего содержимого в текстовое поле.
        /// </summary>
        /// <param name="text">
        /// Текст, добавляемый в конец текущего содержимого в текстовом поле.
        /// </param>
        /// <param name="color">
        /// Цвет, которым отображается текст.
        /// </param>
        internal void AppendText(string text, Color color)
        {
            _Actions.Enqueue(() =>
            {
                _RichTextBox.SuspendLayout();
                _RichTextBox.Enabled = false;
                _RichTextBox.AppendText("");
                _RichTextBox.SelectionColor = color;
                _RichTextBox.AppendText(text);
                _RichTextBox.SelectionColor = _RichTextBox.ForeColor;
                _RichTextBox.Enabled = true;
                _RichTextBox.ResumeLayout();
                _RichTextBox.PerformLayout();
            });
        }

        /// <summary>
        /// Выбирает весь текст в текстовом поле.
        /// </summary>
        public void SelectAll()
        {
            _RichTextBox.SelectAll();
        }


        /// <summary>
        /// Прокручивает содержимое до текущей позиции.
        /// </summary>
        internal void ScrollToCaret()
        {
            _Actions.Enqueue(() => _RichTextBox.ScrollToCaret());
        }

        /// <summary>
        /// Реализует точку входа потока.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void ThreadEntry()
        {
            try
            {
                while (_Thread != null)
                {
                    Action action = null;
                    if (_Actions.TryDequeue(out action))
                    {
                        _RichTextBox.Invoke(action);
                    }
                    else
                    {
                        Thread.Sleep(256);
                    }
                }
            }
            catch (Exception)
            {
                                
            }
            _Thread = null;
        }

        /// <summary>
        /// Сохраняет содержимое в файл.
        /// </summary>
        /// <param name="path">
        /// Пусть к файлу.
        /// </param>
        public void Save(string path)
        {
            _RichTextBox.SaveFile(path, RichTextBoxStreamType.RichText);
        }

        /// <summary>
        /// Обрабатывает событие <see cref="Control.HandleCreated"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void RichTextBox_HandleCreated(object sender, EventArgs e)
        {
            _Thread = new Thread(ThreadEntry);
            _Thread.Start();
        }

        /// <summary>
        /// Обрабатывает событие <see cref="Control.HandleDestroyed"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void RichTextBox_HandleDestroyed(object sender, EventArgs e)
        {
            Thread thread = _Thread;
            if (thread != null)
            {
                _Thread = null;

                try
                {
                    thread.Join(1024);
                }
                catch (Exception)
                {
                    
                }

                try
                {
                    thread.Abort();
                }
                catch (Exception)
                {
                    
                }
            }
        }
    }
}