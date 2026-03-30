using RailTest.Drawing.OpenGL.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет контуры шрифта.
    /// </summary>
    public class FontOutlines : Ancestor
    {
        /// <summary>
        /// Поле для хранения шрифта.
        /// </summary>
        private readonly Font _Font;

        /// <summary>
        /// Поле для хранения контекста рендеринга.
        /// </summary>
        private readonly RenderingContext _RenderingContext;

        /// <summary>
        /// Поле для хранения массива базовых списков.
        /// </summary>
        private uint[] _ListBases;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="font">
        /// Шрифт.
        /// </param>
        /// <param name="renderingContext">
        /// Контекст рендеринга.
        /// </param>
        internal FontOutlines(Font font, RenderingContext renderingContext)
        {
            _Font = font;
            _RenderingContext = renderingContext;
            _ListBases = new uint[256];
        }


        ///// <exception cref="ObjectDisposedException">
        ///// Произошла попытка выполнения операции над удаленным объектом.
        ///// </exception>
        ///// <exception cref="InvalidOperationException">
        ///// Не удалось установить контекст рендеринга OpenGL.
        ///// </exception>


        /// <summary>
        /// Отображает символ.
        /// </summary>
        /// <param name="code">
        /// Код символа, который необходимо отобразить.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Не удалось выбрать шрифт.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось построить контуры шрифта.
        /// </exception>
        /// <exception cref="OpenGLException">
        /// Произошло исключение при вызове функции библиотеки OpenGL.
        /// </exception>
        private void DrawChar(ushort code)
        {
            byte listIndex, codeIndex;
            unchecked
            {
                code += 2;
                listIndex = (byte)(code >> 8);
                codeIndex = (byte)(0xff & code);
            }
            if (_ListBases[listIndex] == 0)
            {
                DeviceContextHandle deviceContextHandle = _RenderingContext.DeviceContext.Handle;
                IntPtr oldObject = Import.SelectObject(deviceContextHandle, _Font.ToHfont());
                if (oldObject == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Не удалось выбрать шрифт.");
                }
                try
                {
                    const int range = 256;
                    uint listBase = Origin.glGenLists(range);
                    OpenGLException.CheckErrorCode(Origin.glGetError());
                    unsafe
                    {
                        if (0 == Import.wglUseFontBitmapsW(deviceContextHandle, (uint)(listIndex << 8), range, listBase))
                        {
                            throw new InvalidOperationException("Не удалось построить контуры шрифта.");
                        }
                        //if (0 == Import.wglUseFontOutlinesW(deviceContextHandle, (uint)(listIndex << 8), range, listBase, 0.1f, 0.0f, 1 /*WGL_FONT_POLYGONS*/, null))
                        //{
                        //    throw new InvalidOperationException("Не удалось построить контуры шрифта.");
                        //}
                    }
                    _ListBases[listIndex] = listBase;
                }
                finally
                {
                    Import.SelectObject(deviceContextHandle, oldObject);
                }
            }
            Origin.glListBase(_ListBases[listIndex]);
            Origin.glCallList(codeIndex);
        }

        /// <summary>
        /// Отображает текст.
        /// </summary>
        /// <param name="x">
        /// Абсцисса.
        /// </param>
        /// <param name="y">
        /// Ордината.
        /// </param>
        /// <param name="text">
        /// Выводимый текст.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Не удалось выбрать шрифт.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось построить контуры шрифта.
        /// </exception>
        /// <exception cref="OpenGLException">
        /// Произошло исключение при вызове функции библиотеки OpenGL.
        /// </exception>
        public void DrawText(double x, double y, string text)
        {
            _RenderingContext.CheckHandle();
            Origin.glRasterPos2d(x, y);
            for (int i = 0; i != text.Length; ++i)
            {
                DrawChar(text[i]);
            }
        }
    }
}
