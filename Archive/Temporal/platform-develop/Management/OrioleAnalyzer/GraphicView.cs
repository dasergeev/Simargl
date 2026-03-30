using Apeiron.Recording.Adxl357;
using RailTest.TwoSection.DataViewer.OpenGL;
using System.Drawing;
using System.Windows.Forms;

namespace RailTest.TwoSection.DataViewer
{
    /// <summary>
    /// Представляет элемент управления, отображающий графическую информацию.
    /// </summary>
    unsafe public class GraphicView : Control
    {
        /// <summary>
        /// Происходит при изменении свойства <see cref="LineColor"/>.
        /// </summary>
        public event EventHandler? LineColorChanged;

        /// <summary>
        /// Поле для хранения списка графических команд.
        /// </summary>
        private uint _DisplayList;

        /// <summary>
        /// Поле для хранения дескриптора контекста устройства.
        /// </summary>
        private IntPtr _DeviceContext;

        /// <summary>
        /// Поле для хранения дескриптора контекста отображения.
        /// </summary>
        private IntPtr _RenderingContext;

        /// <summary>
        /// Поле для хранения данных для отображения.
        /// </summary>
        private Adxl357DataPackage? _Data;

        private double _Sampling = 1;

        ///// <summary>
        ///// Поле для хранения списка пиков.
        ///// </summary>
        //private List<Peak> _Peaks;

        /// <summary>
        /// Поле для хранения цвета отображения линий.
        /// </summary>
        private Color _LineColor;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public GraphicView()
        {
            ResizeRedraw = true;
        }

        /// <summary>
        /// Возвращает или задаёт цвет отображения линий.
        /// </summary>
        public Color LineColor
        {
            get
            {
                return _LineColor;
            }
            set
            {
                if (_LineColor != value)
                {
                    _LineColor = value;
                    OnLineColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Загружает канал для отображения.
        /// </summary>
        /// <param name="channel">
        /// Канал для отображения.
        /// </param>
        /// <param name="sampling"></param>
        ///// <param name="peaks">
        ///// Список пиков.
        ///// </param>
        public void Load(Adxl357DataPackage channel, int sampling)//, List<Peak> peaks)
        {
            DeleteDisplayList();
            _Data = channel;
            _Sampling = sampling;
            //_Peaks = null;
        }

        ///// <summary>
        ///// Загружает блок коэффициентов для отображения.
        ///// </summary>
        ///// <param name="factor">
        ///// Блок коэффициентов для отображения.
        ///// </param>
        //public void Load(Factor factor)
        //{
        //    DeleteDisplayList();
        //    _Data = factor;
        //}

        /// <summary>
        /// Вызывает событие <see cref="Control.Resize"/>
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnResize(EventArgs e)
        {
            UpdateFactors();
            base.OnResize(e);
        }

        private double _XMinimum;
        private double _XMaximum;
        private double _YMinimum;
        private double _YMaximum;

        private double _XSourceMinimum;
        private double _XSourceMaximum;
        private double _YSourceMinimum;
        private double _YSourceMaximum;

        /// <summary>
        /// Сбрасывает настройки вида.
        /// </summary>
        public void Reset()
        {
            _XMinimum = _XSourceMinimum;
            _XMaximum = _XSourceMaximum;
            _YMinimum = 1.025 * _YSourceMinimum - 0.025 * _YSourceMaximum;
            _YMaximum = 1.025 * _YSourceMaximum - 0.025 * _YSourceMinimum;
            UpdateFactors();
        }

        private readonly Color PeakBackColor = Color.MistyRose;

        /// <summary>
        /// Создаёт список графических команд.
        /// </summary>
        private void CreateDisplayList()
        {
            if (_Data is not null)
            {
                _DisplayList = Import.glGenLists(1);
                Import.glNewList(_DisplayList, Import.GL_COMPILE);

                //Channel channel = (Channel)_Data;

                Import.glColor3ub(PeakBackColor.R, PeakBackColor.G, PeakBackColor.B);
                //foreach (Peak peak in channel.FindPeaks())
                //{
                //    if (peak.EndIndex < channel.Length)
                //    {
                //        double xBegin = peak.BeginIndex / channel.Sampling;
                //        double xEnd = peak.EndIndex / channel.Sampling;
                //        double yBegin = channel[peak.EndIndex];
                //        double yEnd = channel[peak.EndIndex];
                //        for (int i = peak.BeginIndex; i != peak.EndIndex; ++i)
                //        {
                //            if (yBegin > channel[i])
                //            {
                //                yBegin = channel[i];
                //            }
                //            if (yEnd < channel[i])
                //            {
                //                yEnd = channel[i];
                //            }
                //        }

                //        Import.glBegin(Import.GL_QUADS);
                //        Import.glVertex2d(xBegin, yBegin);
                //        Import.glVertex2d(xEnd, yBegin);
                //        Import.glVertex2d(xEnd, yEnd);
                //        Import.glVertex2d(xBegin, yEnd);
                //        Import.glEnd();
                //    }
                //}

                _XSourceMinimum = 0;
                _XSourceMaximum = (_Data.Signals.XSignal.Length - 1) / _Sampling;
                _YSourceMinimum = double.MaxValue;
                _YSourceMaximum = double.MinValue;

                Color xColor = Color.DarkRed;
                Color yColor = Color.DarkGreen;
                Color zColor = Color.DarkBlue;

                Import.glColor3ub(xColor.R, xColor.G, xColor.B);
                Import.glBegin(Import.GL_LINE_STRIP);
                for (int i = 0; i != _Data.Signals.XSignal.Length - 1; ++i)
                {
                    double x = i / _Sampling;
                    double y = _Data.Signals.XSignal[i];
                    if (_YSourceMinimum > y)
                    {
                        _YSourceMinimum = y;
                    }
                    if (_YSourceMaximum < y)
                    {
                        _YSourceMaximum = y;
                    }
                    Import.glVertex2d(x, y);
                }
                Import.glEnd();


                Import.glColor3ub(yColor.R, yColor.G, yColor.B);
                Import.glBegin(Import.GL_LINE_STRIP);
                for (int i = 0; i != _Data.Signals.YSignal.Length - 1; ++i)
                {
                    double x = i / _Sampling;
                    double y = _Data.Signals.YSignal[i];
                    if (_YSourceMinimum > y)
                    {
                        _YSourceMinimum = y;
                    }
                    if (_YSourceMaximum < y)
                    {
                        _YSourceMaximum = y;
                    }
                    Import.glVertex2d(x, y);
                }
                Import.glEnd();

                Import.glColor3ub(zColor.R, zColor.G, zColor.B);
                Import.glBegin(Import.GL_LINE_STRIP);
                for (int i = 0; i != _Data.Signals.ZSignal.Length - 1; ++i)
                {
                    double x = i / _Sampling;
                    double y = _Data.Signals.ZSignal[i];
                    if (_YSourceMinimum > y)
                    {
                        _YSourceMinimum = y;
                    }
                    if (_YSourceMaximum < y)
                    {
                        _YSourceMaximum = y;
                    }
                    Import.glVertex2d(x, y);
                }
                Import.glEnd();


                Import.glEndList();
                Reset();
            }
        }

        /// <summary>
        /// Удаляет список графических команд.
        /// </summary>
        private void DeleteDisplayList()
        {
            if (_DisplayList != 0)
            {
                Import.glDeleteLists(_DisplayList, 1);
                _DisplayList = 0;
            }
        }

        /// <summary>
        /// Выполняет список графических команд.
        /// </summary>
        private void CallDisplayList()
        {
            if (_DisplayList != 0)
            {
                Import.glCallList(_DisplayList);
            }
        }

        /// <summary>
        /// Выполняет подготовку к отображению.
        /// </summary>
        private void Preparation()
        {
            if (_DisplayList == 0)
            {
                CreateDisplayList();
            }
        }

        /// <summary>
        /// Отступ слева.
        /// </summary>
        public readonly int LeftIndent = 64;

        /// <summary>
        /// Отступ справа.
        /// </summary>
        public readonly int RightIndent = 16;

        /// <summary>
        /// Отступ сверху.
        /// </summary>
        public readonly int TopIndent = 16;

        /// <summary>
        /// Отступ снизу.
        /// </summary>
        public readonly int BottomIndent = 32;

        /// <summary>
        /// Цвет фона графика.
        /// </summary>
        public readonly Color GraphicBackColor = Color.White;

        /// <summary>
        /// Цвет границы графика.
        /// </summary>
        public readonly Color GraphicBorderColor = Color.DarkGray;

        /// <summary>
        /// Цвет сетки.
        /// </summary>
        public readonly Color GridColor = Color.LightGray;

        /// <summary>
        /// Минимальный шаг сетки.
        /// </summary>
        public readonly int GridStepMinimum = 64;

        /// <summary>
        /// Поле для хранения масштаба по оси Ox.
        /// </summary>
        private double _XScale;

        /// <summary>
        /// Поле для хранения смещения по оси Ox.
        /// </summary>
        private double _XOffset;

        /// <summary>
        /// Поле для хранения масштаба по оси Oy.
        /// </summary>
        private double _YScale;

        /// <summary>
        /// Поле для хранения смещения по оси Oy.
        /// </summary>
        private double _YOffset;

        /// <summary>
        /// Обновляет коэффициенты.
        /// </summary>
        private void UpdateFactors()
        {
            _XScale = (Width - LeftIndent - RightIndent) / (_XMaximum - _XMinimum);
            _XOffset = LeftIndent - _XScale * _XMinimum;
            _YScale = (Height - TopIndent - BottomIndent) / (_YMinimum - _YMaximum);
            _YOffset = TopIndent - _YScale * _YMaximum;
        }

        /// <summary>
        /// Выполняет нормализацию шага сетки.
        /// </summary>
        /// <param name="value">
        /// Исходный шаг сетки.
        /// </param>
        /// <returns>
        /// Нормализованный шаг сетки.
        /// </returns>
        private static double NormalizeStep(double value)
        {
            int power = (int)(Math.Floor(Math.Log10(value)));
            double amplitude = value / Math.Pow(10, power);
            if (amplitude < 2)
            {
                amplitude = 1;
            }
            else if (amplitude < 2.5)
            {
                amplitude = 2;
            }
            else if (amplitude < 5)
            {
                amplitude = 2.5;
            }
            else
            {
                amplitude = 5;
            }
            return amplitude * Math.Pow(10, power);
        }

        /// <summary>
        /// Рисует фон элемента.
        /// </summary>
        /// <param name="pevent">
        /// Сведения об элементе управления, который следует нарисовать.
        /// </param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Preparation();
            Import.glClear(0x4100);
            Import.glClearColor(BackColor.R / 255.0f, BackColor.G / 255.0f, BackColor.B / 255.0f, 1.0f);
            Import.glMatrixMode(0x1701);
            Import.glLoadIdentity();

            Import.glViewport(LeftIndent, BottomIndent, Width - LeftIndent - RightIndent, Height - TopIndent - BottomIndent);
            Import.glOrtho(_XMinimum, _XMaximum, _YMinimum, _YMaximum, 0, 1);

            SetCurrentColor(GraphicBackColor);
            Import.glBegin(Import.GL_QUADS);
            Import.glVertex2d(_XMinimum, _YMinimum);
            Import.glVertex2d(_XMaximum, _YMinimum);
            Import.glVertex2d(_XMaximum, _YMaximum);
            Import.glVertex2d(_XMinimum, _YMaximum);
            Import.glEnd();

            SetCurrentColor(GridColor);

            double xGridStep = NormalizeStep(2 * GridStepMinimum / _XScale);
            double xBegin = Math.Ceiling(_XMinimum / xGridStep) * xGridStep;
            double x = xBegin;
            Import.glBegin(Import.GL_LINES);
            while (x <= _XMaximum)
            {
                Import.glVertex2d(x, _YMinimum);
                Import.glVertex2d(x, _YMaximum);
                x += xGridStep;
            }
            Import.glEnd();

            double yGridStep = NormalizeStep(-2 * GridStepMinimum / _YScale);
            double yBegin = Math.Ceiling(_YMinimum / yGridStep) * yGridStep;
            double y = yBegin;
            Import.glBegin(Import.GL_LINES);
            while (y <= _YMaximum)
            {
                Import.glVertex2d(_XMinimum, y);
                Import.glVertex2d(_XMaximum, y);
                y += yGridStep;
            }
            Import.glEnd();
            
            CallDisplayList();

            Import.glViewport(0, 0, Width, Height);
            Import.glMatrixMode(0x1701);
            Import.glLoadIdentity();
            Import.glOrtho(0, Width, 0, Height, 0, 1);

            SetCurrentColor(GraphicBorderColor);
            Import.glBegin(Import.GL_LINE_LOOP);
            Import.glVertex2i(LeftIndent, BottomIndent);
            Import.glVertex2i(Width - RightIndent, BottomIndent);
            Import.glVertex2i(Width - RightIndent, Height - TopIndent);
            Import.glVertex2i(LeftIndent, Height - TopIndent);
            Import.glEnd();

            x = xBegin;
            SetCurrentColor(Color.Black);
            while (x <= _XMaximum)
            {
                int xLabel = (int)(_XScale * x + _XOffset);
                if (xLabel >= LeftIndent && xLabel <= Width - RightIndent)
                {
                    DrawText(xLabel, 10, x.ToString("0.#####"));
                }
                x += xGridStep;
            }

            y = yBegin;
            SetCurrentColor(Color.Black);
            while (y <= _YMaximum)
            {
                int yLabel = (int)(_YScale * y + _YOffset);
                if (yLabel >= TopIndent && yLabel <= Height - BottomIndent)
                {
                    DrawText(5, Height - yLabel - 5, y.ToString("0.#####"));
                }
                y += yGridStep;
            }

            Import.glFlush();
            _ = Import.SwapBuffers(_DeviceContext);
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.DoubleClick"/>
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnDoubleClick(EventArgs e)
        {
            Reset();
            Invalidate();
            base.OnDoubleClick(e);
        }

        /// <summary>
        /// Устанавливает текущий цвет.
        /// </summary>
        /// <param name="color">
        /// Цвет, который необходимо установить как текущий.
        /// </param>
        private static void SetCurrentColor(Color color)
        {
            Import.glColor4ub(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Поле для хранения последней точки.
        /// </summary>
        private Point _LastPoint;

        /// <summary>
        /// Вызывает событие <see cref="Control.MouseMove"/>
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.X > LeftIndent && e.X < Width - RightIndent &&
                e.Y > BottomIndent && e.Y < Height - TopIndent)
            {
                Cursor = Cursors.Cross;
            }
            else
            {
                Cursor = Cursors.Default;
            }
            Capture = (e.Button & MouseButtons.Right) != 0;
            if (Capture)
            {
                Capture = true;
                double temporal = _XMinimum + (_LastPoint.X - e.X) / _XScale;
                _XMaximum += temporal - _XMinimum;
                _XMinimum = temporal;
                temporal = _YMaximum + (_LastPoint.Y - e.Y) / _YScale;
                _YMinimum = temporal - _YMaximum + _YMinimum;
                _YMaximum = temporal;
                UpdateFactors();
                Invalidate();
            }
            _LastPoint = e.Location;
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.MouseWheel"/>
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.X > LeftIndent && e.X < Width - RightIndent &&
                e.Y > BottomIndent && e.Y < Height - TopIndent)
            {
                double sX = 1.25;
                double sY = 1.25;
                if ((ModifierKeys & Keys.Shift) != 0)
                {
                    sY = 1;
                }
                if (e.Delta > 0)
                {
                    sX = 1 / sX;
                    sY = 1 / sY;
                }
                double temporal = _XMinimum + (e.X - LeftIndent) * (1 - sX) / _XScale;
                _XMaximum = temporal + sX * (_XMaximum - _XMinimum);
                _XMinimum = temporal;
                temporal = _YMaximum + (TopIndent - e.Y) * (sY - 1) / _YScale;
                _YMinimum = temporal - sY * (_YMaximum - _YMinimum);
                _YMaximum = temporal;
                UpdateFactors();
                Invalidate();
            }
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.HandleCreated"/>
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnHandleCreated(EventArgs e)
        {
            bool isFail = true;
            try
            {
                PIXELFORMATDESCRIPTOR pfd;
                pfd.nSize = (ushort)sizeof(PIXELFORMATDESCRIPTOR);
                pfd.nVersion = 1;
                pfd.dwFlags = 0x25;
                pfd.iPixelType = 0;
                pfd.cColorBits = 16;
                pfd.cDepthBits = 16;
                _DeviceContext = Import.GetDC(Handle);
                int iPixelFormat = Import.ChoosePixelFormat(_DeviceContext, &pfd);
                if (iPixelFormat != 0)
                {
                    PIXELFORMATDESCRIPTOR bestMatch_pfd;
                    _ = Import.DescribePixelFormat(_DeviceContext, iPixelFormat, (uint)sizeof(PIXELFORMATDESCRIPTOR), &bestMatch_pfd);
                    if (Import.SetPixelFormat(_DeviceContext, iPixelFormat, &pfd) != 0)
                    {
                        _RenderingContext = Import.wglCreateContext(_DeviceContext);
                        if (_RenderingContext != IntPtr.Zero)
                        {
                            if (Import.wglMakeCurrent(_DeviceContext, _RenderingContext) != 0)
                            {
                                Import.glClear(0x4100);
                                Import.glClearColor(BackColor.R / 255.0f, BackColor.G / 255.0f, BackColor.B / 255.0f, BackColor.A / 255.0f);
                                Import.glFlush();
                                _ = Import.SwapBuffers(_DeviceContext);
                                Import.SelectObject(_DeviceContext, Font.ToHfont());
                                _TextListBase = Import.glGenLists(256);
                                _ = Import.wglUseFontBitmapsW(_DeviceContext, 44, 14, _TextListBase);
                                isFail = false;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (isFail)
                {

                }
            }
            base.OnHandleCreated(e);
        }

        /// <summary>
        /// Поле для хранения списка графических команд для отображения текста.
        /// </summary>
        private uint _TextListBase;

        /// <summary>
        /// Отображает текст.
        /// </summary>
        /// <param name="x">
        /// Координата x положения точки вывода.
        /// </param>
        /// <param name="y">
        /// Координата y положения точки вывода.
        /// </param>
        /// <param name="text">
        /// Выводимый текст.
        /// </param>
        private void DrawText(int x, int y, string text)
        {
            Import.glListBase(_TextListBase);
            Import.glRasterPos2f(x, y);
            for (int i = 0; i != text.Length; ++i)
            {
                Import.glCallList((uint)(text[i] - 43));
            }
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.HandleDestroyed"/>
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            _ = Import.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
            _ = Import.wglDeleteContext(_RenderingContext);
            _ = Import.ReleaseDC(Handle, _DeviceContext);
            base.OnHandleDestroyed(e);
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.BackColorChanged"/>
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnBackColorChanged(e);
        }

        /// <summary>
        /// Вызывает событие <see cref="LineColorChanged"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected virtual void OnLineColorChanged(EventArgs e)
        {
            LineColorChanged?.Invoke(this, e);
        }
    }
}
