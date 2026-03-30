using RailTest.Algebra.Specialized;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL.Core
{
    /// <summary>
    /// Предоставляет прямой доступ к OpenGL.
    /// </summary>
    public static class Plain
    {
        /// <summary>
        /// Начинает блок вершин примитива или группы примитивов.
        /// </summary>
        /// <param name="mode">
        /// Значение, определяющее тип примитива или примитивов, которые будут созданы из вершин.
        /// </param>
        /// <remarks>
        /// <para>В случае ошибки функция <see cref="GetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="ErrorCode.InvalidEnumeration"/></term>
        /// <description>В параметре <paramref name="mode"/> передано недопустимое значение.</description>
        /// </item>
        /// <item>
        /// <term><see cref="ErrorCode.InvalidOperation"/></term>
        /// <description>г<see cref="Begin"/> вызвана между <see cref="Begin"/> и соответствующим выполнением <see cref="End"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        public static void Begin(PrimitiveMode mode)
        {
            Origin.glBegin((int)mode);
        }

        /// <summary>
        /// Завершает блок вершин примитива или группы примитивов.
        /// </summary>
        public static void End()
        {
            Origin.glEnd();
        }

        /// <summary>
        /// Возвращает код последней ошибки.
        /// </summary>
        /// <returns>
        /// Код последней ошибки.
        /// </returns>
        public static ErrorCode GetError()
        {
            return (ErrorCode)Origin.glGetError();
        }

        /// <summary>
        /// Устанавливает область просмотра.
        /// </summary>
        /// <param name="x">
        /// Абсцисса нижней левой точки области просмотра в пикселях.
        /// </param>
        /// <param name="y">
        /// Ордината нижней левой точки области просмотра в пикселях.
        /// </param>
        /// <param name="width">
        /// Ширина области просмотра в пикселях.
        /// </param>
        /// <param name="height">
        /// Высота области просмотра в пикселях.
        /// </param>
        /// <remarks>
        /// <para>
        /// Когда контекст рендеринга OpenGL впервые присоединяется к окну,
        /// ширина и высота устанавливаются в соответствии с размерами этого окна,
        /// а координаты левой точки области просмотра инициализируются нулевыми значениями.
        /// </para>
        /// <para>В случае ошибки функция <see cref="GetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="ErrorCode.InvalidValue"/></term>
        /// <description>В параметре <paramref name="height"/> или <paramref name="width"/> передано отрицательное значение.</description>
        /// </item>
        /// <item>
        /// <term><see cref="ErrorCode.InvalidOperation"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        public static void Viewport(int x, int y, int width, int height)
        {
            Origin.glViewport(x, y, width, height);
        }

        /// <summary>
        /// Устанавливает область просмотра.
        /// </summary>
        /// <param name="x">
        /// Абсцисса нижней левой точки области просмотра в пикселях.
        /// </param>
        /// <param name="y">
        /// Ордината нижней левой точки области просмотра в пикселях.
        /// </param>
        /// <param name="width">
        /// Ширина области просмотра в пикселях.
        /// </param>
        /// <param name="height">
        /// Высота области просмотра в пикселях.
        /// </param>
        /// <remarks>
        /// <para>
        /// Когда контекст рендеринга OpenGL впервые присоединяется к окну,
        /// ширина и высота устанавливаются в соответствии с размерами этого окна,
        /// а координаты левой точки области просмотра инициализируются нулевыми значениями.
        /// </para>
        /// <para>В случае ошибки функция <see cref="GetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="ErrorCode.InvalidValue"/></term>
        /// <description>В параметре <paramref name="height"/> или <paramref name="width"/> передано отрицательное значение.</description>
        /// </item>
        /// <item>
        /// <term><see cref="ErrorCode.InvalidOperation"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        public static void Viewport(double x, double y, double width, double height)
        {
            Viewport((int)x, (int)y, (int)width, (int)height);
        }

        /// <summary>
        /// Заменяют текущую матрицу.
        /// </summary>
        /// <param name="m">
        /// Указатель на матрицу размера 4 x 4, сохраненную в главном порядке столбцов как 16 последовательных значений.
        /// </param>
        /// <remarks>
        /// <para>В случае ошибки функция <see cref="GetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="ErrorCode.InvalidOperation"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [CLSCompliant(false)]
        public static unsafe void LoadMatrix(double* m)
        {
            Origin.glLoadMatrixd(m);
        }

        /// <summary>
        /// Заменяют текущую матрицу.
        /// </summary>
        /// <param name="m">
        /// Указатель на матрицу размера 4 x 4, сохраненную в главном порядке столбцов как 16 последовательных значений.
        /// </param>
        /// <remarks>
        /// <para>В случае ошибки функция <see cref="GetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="ErrorCode.InvalidOperation"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [CLSCompliant(false)]
        public static unsafe void LoadMatrix(float* m)
        {
            Origin.glLoadMatrixf(m);
        }

        /// <summary>
        /// Заменяют текущую матрицу.
        /// </summary>
        /// <param name="matrix">
        /// Указатель на матрицу размера.
        /// </param>
        /// <remarks>
        /// <para>В случае ошибки функция <see cref="GetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="ErrorCode.InvalidOperation"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [CLSCompliant(false)]
        public static unsafe void LoadMatrix(Matrix4x4* matrix)
        {
            LoadMatrix((double*)matrix);
        }

        /// <summary>
        /// Заменяют текущую матрицу.
        /// </summary>
        /// <param name="matrix">
        /// Матрица.
        /// </param>
        /// <remarks>
        /// <para>В случае ошибки функция <see cref="GetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="ErrorCode.InvalidOperation"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [CLSCompliant(false)]
        public static unsafe void LoadMatrix(Matrix4x4 matrix)
        {
            LoadMatrix(&matrix);
        }

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.
        /// </remarks>
        public static void Vertex(double x, double y)
        {
            Origin.glVertex2d(x, y);
        }

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <param name="z">Аппликата вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.
        /// </remarks>
        public static void Vertex(double x, double y, double z)
        {
            Origin.glVertex3d(x, y, z);
        }

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="point">Точка, определяющая вершину.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.
        /// </remarks>
        public static void Vertex(Vector3 point)
        {
            Vertex(point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <param name="z">Аппликата вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.
        /// </remarks>
        public static void Vertex(float x, float y, float z)
        {
            Origin.glVertex3f(x, y, z);
        }

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <param name="z">Аппликата вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.
        /// </remarks>
        public static void Vertex(int x, int y, int z)
        {
            Origin.glVertex3i(x, y, z);
        }

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <param name="z">Аппликата вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="Begin"/> и <see cref="End"/>.
        /// </remarks>
        public static void Vertex(short x, short y, short z)
        {
            Origin.glVertex3s(x, y, z);
        }

        /// <summary>
        /// Устанавливает текущий цвет.
        /// </summary>
        /// <param name="color">
        /// Новое значение текущего цвета.
        /// </param>
        public static void Color(Color color)
        {
            Origin.glColor4ub(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Устанавливает текущий стек матриц.
        /// </summary>
        /// <param name="mode">
        /// Значение, определяющее стек матриц.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="mode"/> передано значение, которое не содержится в перечислении <see cref="OpenGL.MatrixMode"/>.
        /// </exception>
        public static void MatrixMode(MatrixMode mode)
        {
            switch (mode)
            {
                case OpenGL.MatrixMode.ModelView:
                    Origin.glMatrixMode(Origin.GL_MODELVIEW);
                    break;
                case OpenGL.MatrixMode.Projection:
                    Origin.glMatrixMode(Origin.GL_PROJECTION);
                    break;
                case OpenGL.MatrixMode.Texture:
                    Origin.glMatrixMode(Origin.GL_TEXTURE);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode", "Передано значение, которое не содержится в перечислении.");
            }
        }
    }
}
