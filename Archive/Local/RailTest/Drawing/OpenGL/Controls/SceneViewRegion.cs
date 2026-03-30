using RailTest.Algebra.Specialized;
using RailTest.Drawing.OpenGL.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Область рендеринга элемента управления, отображающего сцену.
    /// </summary>
    internal class SceneViewRegion : RenderingRegion
    {
        /// <summary>
        /// Матрица вращения.
        /// </summary>
        public Matrix3x3 Rotation;

        /// <summary>
        /// Смещение.
        /// </summary>
        public Vector3 Bias;

        /// <summary>
        /// Горизонтальный масштаб.
        /// </summary>
        public double ScaleX;

        /// <summary>
        /// Вертикальный масштаб.
        /// </summary>
        public double ScaleY;

        /// <summary>
        /// Аппликата ближней плоскости отсечения.
        /// </summary>
        public double ZNear;

        /// <summary>
        /// Аппликата дальней плоскости отсечения.
        /// </summary>
        public double ZFar;

        /// <summary>
        /// Аппликата дальней плоскости курсора.
        /// </summary>
        public double ZCursor;

        /// <summary>
        /// Перспектива (тангенс угла перспективы).
        /// </summary>
        public double Perspective;

        /// <summary>
        /// Область просмотра.
        /// </summary>
        public double ViewportX;

        /// <summary>
        /// Область просмотра.
        /// </summary>
        public double ViewportY;

        /// <summary>
        /// Область просмотра.
        /// </summary>
        public double ViewportWidth;

        /// <summary>
        /// Область просмотра.
        /// </summary>
        public double ViewportHeight;

        /// <summary>
        /// Матрица вида.
        /// </summary>
        public Matrix4x4 ViewMatrix;

        /// <summary>
        /// Матрица проекции.
        /// </summary>
        public Matrix4x4 ProjectionMatrix;

        /// <summary>
        /// Матрица области просмотра.
        /// </summary>
        public Matrix4x4 ViewportMatrix;

        /// <summary>
        /// Комплексная матрица.
        /// </summary>
        public Matrix4x4 ComplexMatrix;


        private readonly double Angle = Math.PI / 3;

        /// <summary>
        /// Инициализирует новый экземпляр.
        /// </summary>
        /// <param name="control">
        /// Элемент управления, с которым связана область рендеринга.
        /// </param>
        public SceneViewRegion(SceneViewOld control)
        {
            Control = control;
            Reset();

            Control.Resize += Control_Resize;
            Control.MouseWheel += Control_MouseWheel;
            Control.MouseMove += Control_MouseMove;
            Control.PreviewKeyDown += Control_PreviewKeyDown;
        }

        /// <summary>
        /// Выполняет сброс настроек.
        /// </summary>
        private void Reset()
        {
            Rotation = Matrix3x3.Identity;
            Bias = new Vector3();
            ScaleX = 50;
            ScaleY = 50;
            ZNear = 10;
            ZFar = -10;
            ZCursor = 0;
            Perspective = Math.Tan(Angle);
            ViewportX = 0;
            ViewportY = 0;
            ViewportWidth = Control.Width;
            ViewportHeight = Control.Height;
            UpdateMatrix();
        }

        /// <summary>
        /// Обновляет матрицы.
        /// </summary>
        private void UpdateMatrix()
        {
            ViewMatrix = new Matrix4x4(Rotation, Bias);
            double w = ViewportWidth;
            double h = ViewportHeight;

            ProjectionMatrix = new Matrix4x4(
                2 / w, 0, 0, 0,
                0, 2 / h, 0, 0,
                0, 0, -(Perspective + 2), -1,
                0, 0, -Perspective, 1);

            Matrix4x4 scaleMatrix = new Matrix4x4(
                ScaleX, 0, 0, 0,
                0, ScaleY, 0, 0,
                0, 0, 1 / (ZNear - ZFar), -ZNear / (ZNear - ZFar),
                0, 0, 0, 1);
            Matrix4x4.Multiply(out ProjectionMatrix, ref ProjectionMatrix, scaleMatrix);

            ViewportMatrix = new Matrix4x4(
                0.5 * w, 0, 0, ViewportX + 0.5 * w,
                0, -0.5 * h, 0, ViewportY + 0.5 * h,
                0, 0, 1, 0,
                0, 0, 0, 1);

            Matrix4x4.Multiply(out ComplexMatrix, ref ProjectionMatrix, ViewMatrix);
            Matrix4x4.Multiply(out ComplexMatrix, ref ViewportMatrix, ComplexMatrix);
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            double angle = Math.PI / 64;
            void update()
            {
                UpdateMatrix();
                Control.Invalidate();
            }
            switch (e.KeyCode)
            {
                case Keys.Home:
                    Reset();
                    update();
                    break;
                case Keys.Add:
                    ScaleX *= 1.25;
                    ScaleY *= 1.25;
                    update();
                    break;
                case Keys.Subtract:
                    ScaleX *= 0.8;
                    ScaleY *= 0.8;
                    update();
                    break;
                case Keys.Space:
                    if (Perspective == 0)
                    {
                        ChangePerspective(Math.Tan(Angle));
                    }
                    else
                    {
                        ChangePerspective(0);
                    }
                    update();
                    break;
                case Keys.Left:
                    {
                        Matrix3x3.RotationY(out Matrix3x3 factor, -angle);
                        Matrix3x3.Multiply(out Rotation, ref factor, Rotation);
                        factor.Transform(ref Bias);
                    }
                    update();
                    break;
                case Keys.Right:
                    {
                        Matrix3x3.RotationY(out Matrix3x3 factor, angle);
                        Matrix3x3.Multiply(out Rotation, ref factor, Rotation);
                        factor.Transform(ref Bias);
                    }
                    update();
                    break;
                case Keys.Up:
                    {
                        Matrix3x3.RotationX(out Matrix3x3 factor, -angle);
                        Matrix3x3.Multiply(out Rotation, ref factor, Rotation);
                        factor.Transform(ref Bias);
                    }
                    update();
                    break;
                case Keys.Down:
                    {
                        Matrix3x3.RotationX(out Matrix3x3 factor, angle);
                        Matrix3x3.Multiply(out Rotation, ref factor, Rotation);
                        factor.Transform(ref Bias);
                    }
                    update();
                    break;
                case Keys.A:
                    Bias.X -= 0.1;
                    update();
                    break;
                case Keys.D:
                    Bias.X += 0.1;
                    update();
                    break;
                case Keys.W:
                    Bias.Y += 0.1;
                    update();
                    break;
                case Keys.S:
                    Bias.Y -= 0.1;
                    update();
                    break;
            }
        }

        private void Control_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ScaleAtPoint(e.Location, 1.25, 1.25);
            }
            else
            {
                ScaleAtPoint(e.Location, 0.8, 0.8);
            }
        }

        private Point _LastPoint;
        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                Move(_LastPoint, e.Location);
                UpdateMatrix();
                Control.Invalidate();
            }
            else if((e.Button & MouseButtons.Middle) != 0)
            {
                Vector3 current = new Vector3(
                    (e.Location.X - ViewportX - 0.5 * ViewportWidth) / ScaleX,
                    (ViewportY + 0.5 * ViewportHeight - e.Location.Y) / ScaleY,
                    ZNear);
                Vector3 last = new Vector3(
                    (_LastPoint.X - ViewportX - 0.5 * ViewportWidth) / ScaleX,
                    (ViewportY + 0.5 * ViewportHeight - _LastPoint.Y) / ScaleY,
                    ZNear);
                Vector3 axis = Vector3.Normalize(Vector3.Cross(current, last));
                double angle = -0.01 * Math.Sqrt(
                    Math.Pow(_LastPoint.X - e.Location.X, 2)
                    + Math.Pow(e.Location.Y - _LastPoint.Y, 2));

                Matrix3x3.RotationAxis(out Matrix3x3 rotation, axis, angle);
                Matrix3x3.Multiply(out Rotation, ref rotation, Rotation);
                rotation.Transform(ref Bias);
                UpdateMatrix();
                Control.Invalidate();
            }
            _LastPoint = e.Location;
        }

        private void ScaleAtPoint(Point point, double xFactor, double yFactor)
        {
            double factor = 1 + Perspective *(ZNear - ZCursor) / (ZNear - ZFar);

            Bias.X += factor * (1 / ScaleX) * (1 / xFactor - 1) * (point.X - ViewportX - 0.5 * ViewportWidth);
            Bias.Y += factor * (1 / ScaleY) * (1 / yFactor - 1) * (ViewportY - point.Y + 0.5 * ViewportHeight);
            ScaleX *= xFactor;
            ScaleY *= yFactor;
            UpdateMatrix();
            Control.Invalidate();
        }

        private void ChangePerspective(double perspective)
        {
            double factor = ((ZNear - ZFar) - (ZCursor - ZNear) * perspective) /
                ((ZNear - ZFar) - (ZCursor - ZNear) * Perspective);
            ScaleX *= factor;
            ScaleY *= factor;
            Perspective = perspective;
            UpdateMatrix();
            Control.Invalidate();
        }

        private void Move(Point begin, Point end)
        {
            double factor = (1 + Perspective * (ZNear - ZCursor) / (ZNear - ZFar));
            Bias.X += factor * (1 / ScaleX) * (end.X - begin.X);
            Bias.Y += factor * (1 / ScaleY) * (begin.Y - end.Y);
        }

        /// <summary>
        /// Элемент управления, с которым связана область рендеринга.
        /// </summary>
        public SceneViewOld Control { get; }

        /// <summary>
        /// Вызывает событие <see cref="RenderingRegion.Render"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected internal override void OnRender(EventArgs e)
        {
            Origin.glMatrixMode(Origin.GL_MODELVIEW);

            Matrix4x4 modelView = new Matrix4x4(Rotation, Bias);
            Plain.LoadMatrix(modelView);

            Origin.glMatrixMode(Origin.GL_PROJECTION);
            Plain.LoadMatrix(ProjectionMatrix);

            Plain.Viewport(ViewportX, ViewportY, ViewportWidth, ViewportHeight);

            Color lightColor = Color.LightSeaGreen;
            Vector3 lightDirection = new Vector3(0.3, 0.3, 1);
            Rotation.TransposedTransform(ref lightDirection);
            lightDirection = Vector3.Normalize(lightDirection);

            float[] light0_diffuse = { lightColor.R / 256.0f, lightColor.G / 256.0f, lightColor.B / 256.0f };


            float[] light0_direction = { (float)lightDirection.X, (float)lightDirection.Y, (float)lightDirection.Z, 0.0f };
            Origin.glEnable(Origin.GL_LIGHT0);
            unsafe
            {
                fixed (float* pointer = light0_diffuse)
                {
                    Origin.glLightfv(Origin.GL_LIGHT0, Origin.GL_DIFFUSE, pointer);
                }
                fixed (float* pointer = light0_direction)
                {
                    Origin.glLightfv(Origin.GL_LIGHT0, Origin.GL_POSITION, pointer);
                }
            }

            base.OnRender(e);
        }

        /// <summary>
        /// Обрабатывает событие <see cref="Control.Resize"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Control_Resize(object sender, EventArgs e)
        {
            ViewportX = 0;
            ViewportY = 0;
            ViewportWidth = Control.Width - 2 * ViewportX;
            ViewportHeight = Control.Height - 2 * ViewportY;
            UpdateMatrix();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override Vector2 GetScreenPoint(Vector3 source)
        {
            double factor = 1 / (source.X * ComplexMatrix.M41 + source.Y * ComplexMatrix.M42 + source.Z * ComplexMatrix.M43 + ComplexMatrix.M44);
            double x = factor * (source.X * ComplexMatrix.M11 + source.Y * ComplexMatrix.M12 + source.Z * ComplexMatrix.M13 + ComplexMatrix.M14);
            double y = factor * (source.X * ComplexMatrix.M21 + source.Y * ComplexMatrix.M22 + source.Z * ComplexMatrix.M23 + ComplexMatrix.M24);
            return new Vector2(x, y);
        }
    }
}
