using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет исключение, которое возникает при передаче недопустимого значения перечисляемого аргумента.
    /// </summary>
    public class OpenGLInvalidEnumerationException : OpenGLException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal OpenGLInvalidEnumerationException() : base("Недопустимое значение перечисляемого аргумента.")
        {

        }
    }
}
