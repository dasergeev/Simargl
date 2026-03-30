using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет неизвестное исключение библиотеки OpenGL.
    /// </summary>
    public class OpenGLUnknownException : OpenGLException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="errorCode">
        /// Код ошибки.
        /// </param>
        internal OpenGLUnknownException(long errorCode) : base($"Неизвестная ошибка библиотеки OpenGL (код ошибки: {errorCode})")
        {

        }

        /// <summary>
        /// Возвращает код ошибки.
        /// </summary>
        public long ErrorCode { get; }
    }
}
