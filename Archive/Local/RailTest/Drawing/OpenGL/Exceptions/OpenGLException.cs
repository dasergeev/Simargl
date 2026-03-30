using RailTest.Drawing.OpenGL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет базовый класс для всех исключений библиотеки <see cref="RailTest.Drawing.OpenGL"/>.
    /// </summary>
    public abstract class OpenGLException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="message">
        /// Сообщение, описывающее ошибку.
        /// </param>
        internal OpenGLException(string message) : base(message)
        {

        }

        /// <summary>
        /// Выполняет проверку кода ошибки.
        /// </summary>
        /// <param name="code">
        /// Код ошибки.
        /// </param>
        /// <exception cref="OpenGLException">
        /// В параметре <paramref name="code"/> передано значение, отличное от нуля.
        /// </exception>
        internal static void CheckErrorCode(int code)
        {
            switch (code)
            {
                case Origin.GL_NO_ERROR:
                    break;
                case Origin.GL_INVALID_ENUM:
                    throw new OpenGLInvalidEnumerationException();
                case Origin.GL_INVALID_VALUE:
                    throw new OpenGLInvalidValueException();
                case Origin.GL_INVALID_OPERATION:
                    throw new OpenGLInvalidOperationException();
                case Origin.GL_STACK_OVERFLOW:
                    throw new OpenGLStackOverflowException();
                case Origin.GL_STACK_UNDERFLOW:
                    throw new OpenGLStackUnderflowException();
                case Origin.GL_OUT_OF_MEMORY:
                    throw new OpenGLOutOfMemoryException();
                default:
                    throw new OpenGLUnknownException(code);
            }
        }
    }
}
