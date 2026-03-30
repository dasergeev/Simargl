using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет окно просмотра.
    /// </summary>
    public class Viewport
    {
        /// <summary>
        /// Поле для хранения абсциссы верхней левой точки области просмотра в пикселях.
        /// </summary>
        private double _X;

        /// <summary>
        /// Поле для хранения ординаты верхней левой точки области просмотра в пикселях.
        /// </summary>
        private double _Y;

        /// <summary>
        /// Поле для хранения ширины области просмотра в пикселях.
        /// </summary>
        private double _Width;

        /// <summary>
        /// Поле для хранения высоты области просмотра в пикселях.
        /// </summary>
        private double _Height;

        /// <summary>
        /// Возвращает абсциссу верхней левой точки области просмотра.
        /// </summary>
        public double X => _X;

        /// <summary>
        /// Возвращает ординату верхней левой точки области просмотра.
        /// </summary>
        public double Y => _Y;

        /// <summary>
        /// Изменяет область просмотра.
        /// </summary>
        /// <param name="x">
        /// Абсцисса верхней левой точки области просмотра.
        /// </param>
        /// <param name="y">
        /// Ордината верхней левой точки области просмотра.
        /// </param>
        /// <param name="width">
        /// Ширина области просмотра.
        /// </param>
        /// <param name="height">
        /// Высота области просмотра.
        /// </param>
        public void Change(double x, double y, double width, double height)
        {
            _X = x;
            _Y = y;
            _Width = width;
            _Height = height;
        }
    }
}
