using RailTest.Algebra.Specialized;
using RailTest.Drawing.OpenGL.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет графопостроитель.
    /// </summary>
    public class Plotter : Control
    {
        /// <summary>
        /// Происходит при отрисовке сцены.
        /// </summary>
        public event EventHandler Render;

        /// <summary>
        /// Поле для хранения элемента управления, отображающего сцену.
        /// </summary>
        private readonly SceneView _SceneView;

        /// <summary>
        /// Поле для хранения отступа слева.
        /// </summary>
        private double _LeftIndent;

        /// <summary>
        /// Поле для хранения отступа слева.
        /// </summary>
        private double _RightIndent;

        /// <summary>
        /// Поле для хранения отступа сверху.
        /// </summary>
        private double _TopIndent;

        /// <summary>
        /// Поле для хранения отступа сверху.
        /// </summary>
        private double _BottomIndent;

        /// <summary>
        /// Поле для хранения полной ширины.
        /// </summary>
        private double _FullWidth;

        /// <summary>
        /// Поле для хранения полной высоты.
        /// </summary>
        private double _FullHeight;

        /// <summary>
        /// Поле для хранения абсциссы начальной точки области отрисовки.
        /// </summary>
        private double _RenderX;

        /// <summary>
        /// Поле для хранения ординаты начальной точки области отрисовки.
        /// </summary>
        private double _RenderY;

        /// <summary>
        /// Поле для хранения ширины области отрисовки.
        /// </summary>
        private double _RenderWidth;

        /// <summary>
        /// Поле для хранения высоты области отрисовки.
        /// </summary>
        private double _RenderHeight;

        /// <summary>
        /// Поле для хранения масштаба вдоль оси абсцисс для матрицы вида.
        /// </summary>
        private double _ViewScaleX;

        /// <summary>
        /// Поле для хранения масштаба вдоль оси ординат для матрицы вида.
        /// </summary>
        private double _ViewScaleY;

        /// <summary>
        /// Поле для хранения смещения вдоль оси абсцисс для матрицы вида.
        /// </summary>
        private double _ViewOffsetX;

        /// <summary>
        /// Поле для хранения смещения вдоль оси ординат для матрицы вида.
        /// </summary>
        private double _ViewOffsetY;

        /// <summary>
        /// Поле для хранения смещения вдоль оси абсцисс для курсора.
        /// </summary>
        private double _MouseOffsetX;

        /// <summary>
        /// Поле для хранения смещения вдоль оси ординат для курсора.
        /// </summary>
        private double _MouseOffsetY;

        /// <summary>
        /// Поле для хранения последнего положения курсора.
        /// </summary>
        private Point _LastMouseLocation;

        /// <summary>
        /// Поле для хранения контуров шрифтов.
        /// </summary>
        private FontOutlines _FontOutlines;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public Plotter()
        {
            ResizeRedraw = true;
            _SceneView = new SceneView
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_SceneView);
            _SceneView.BackColor = BackColor;
            _SceneView.Render += SceneView_Render;
            _SceneView.MouseWheel += SceneView_MouseWheel;
            _SceneView.MouseMove += SceneView_MouseMove;

            _LeftIndent = 32;
            _RightIndent = 16;
            _TopIndent = 16;
            _BottomIndent = 32;

            _ViewScaleX = 1;
            _ViewScaleY = 1;
            _ViewOffsetX = 0;
            _ViewOffsetY = 0;

            _LastMouseLocation = MousePosition;

            _FontOutlines = null;

            MetricUpdate();
        }

        /// <summary>
        /// Выполняет обновление метрики.
        /// </summary>
        private void MetricUpdate()
        {
            _FullWidth = _SceneView.Width;
            _FullHeight = _SceneView.Height;

            _RenderX = _LeftIndent;
            _RenderY = _BottomIndent;

            _RenderWidth = _FullWidth - (_LeftIndent + _RightIndent);
            _RenderHeight = _FullHeight - (_TopIndent + _BottomIndent);

            _MouseOffsetX = -0.5 * _RenderWidth - _LeftIndent;
            _MouseOffsetY = 0.5 * _RenderHeight + _TopIndent;
        }

        ///// <summary>
        ///// Получает графические координаты.
        ///// </summary>
        ///// <param name="point">
        ///// Точка элемента управления.
        ///// </param>
        ///// <returns>
        ///// Графические координаты.
        ///// </returns>
        //private Vector2 GetGraphicCoordinates(Point point)
        //{
        //    return new Vector2(
        //        _MouseOffsetX + point.X,
        //        _MouseOffsetY - point.Y);
        //}

        /// <summary>
        /// Отрисовывает область графиков.
        /// </summary>
        private void DrawGraphics()
        {
            if (_FontOutlines is null)
            {
                _FontOutlines = _SceneView.CreateFontOutlines(Font);
            }

            Plain.Viewport(_RenderX, _RenderY, _RenderWidth, _RenderHeight);
            Plain.MatrixMode(MatrixMode.ModelView);
            Plain.LoadMatrix(Matrix4x4.Identity);
            Plain.MatrixMode(MatrixMode.Projection);
            Plain.LoadMatrix(Matrix4x4.Identity);

            Plain.Begin(PrimitiveMode.Quads);
            try
            {
                Plain.Color(Color.White);
                Plain.Vertex(-1, -1);
                Plain.Vertex(1, -1);
                Plain.Vertex(1, 1);
                Plain.Vertex(-1, 1);
            }
            finally
            {
                Plain.End();
            }

            Plain.MatrixMode(MatrixMode.ModelView);
            Plain.LoadMatrix(new Matrix4x4(
                _ViewScaleX, 0, 0, _ViewOffsetX,
                0, _ViewScaleY, 0, _ViewOffsetY,
                0, 0, 1, 0,
                0, 0, 0, 1));

            Plain.MatrixMode(MatrixMode.Projection);
            Plain.LoadMatrix(new Matrix4x4(
                2.0 / _RenderWidth, 0, 0, 0,
                0, 2.0 / _RenderHeight, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1));

            OnRender(EventArgs.Empty);

            //Plain.MatrixMode(MatrixMode.ModelView);
            //Plain.LoadMatrix(Matrix4x4.Identity);

            //double fontScale = 32;

            //Plain.MatrixMode(MatrixMode.Projection);
            //Plain.LoadMatrix(new Matrix4x4(
            //    2.0 * fontScale / _RenderWidth, 0, 0, 0,
            //    0, 2.0 * fontScale / _RenderHeight, 0, 0,
            //    0, 0, 1, 0,
            //    0, 0, 0, 1));
            //Origin.glTranslated(-0.5, 0, 0);
            //Origin.glScaled(0.5, 0.5, 0.5);
            //Origin.glShadeModel(Origin.GL_SMOOTH);
            _FontOutlines.DrawText(0, 0, "2.56");
        }

        /// <summary>
        /// Обрабатывает событие <see cref="SceneView.Render"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void SceneView_Render(object sender, EventArgs e)
        {
            if (_RenderWidth > 0 && _RenderHeight > 0)
            {
                DrawGraphics();
            }
        }

        /// <summary>
        /// Обрабатывает событие <see cref="Control.MouseWheel"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void SceneView_MouseWheel(object sender, MouseEventArgs e)
        {
            double factor = e.Delta > 0 ? 1.25 : 0.8;

            if ((ModifierKeys & Keys.Shift) == 0)
            {
                _ViewOffsetX = (1.0 - factor) * (_MouseOffsetX + e.Location.X) + factor * _ViewOffsetX;
                _ViewScaleX *= factor;
            }

            if ((ModifierKeys & Keys.Control) == 0)
            {
                _ViewOffsetY = (1.0 - factor) * (_MouseOffsetY - e.Location.Y) + factor * _ViewOffsetY;
                _ViewScaleY *= factor;
            }

            MetricUpdate();
            _SceneView.Invalidate();
        }

        /// <summary>
        /// Обрабатывает событие <see cref="Control.MouseMove"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void SceneView_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0 && _LastMouseLocation != e.Location)
            {
                _ViewOffsetX += e.Location.X - _LastMouseLocation.X;
                _ViewOffsetY += _LastMouseLocation.Y - e.Location.Y;
                MetricUpdate();
                _SceneView.Invalidate();
            }
            _LastMouseLocation = e.Location;
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.BackColorChanged"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            _SceneView.BackColor = BackColor;
            base.OnBackColorChanged(e);
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.Resize"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            MetricUpdate();
        }

        /// <summary>
        /// Вызывает событие <see cref="Render"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected virtual void OnRender(EventArgs e)
        {
            Render?.Invoke(this, e);
        }
    }
}
