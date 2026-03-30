using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет исключение, которое возникает при нехватке памяти для выполнения команды.
    /// </summary>
    public class OpenGLOutOfMemoryException : OpenGLException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal OpenGLOutOfMemoryException() : base("Недостаточно памяти для выполнения команды.")
        {

        }
    }
}
