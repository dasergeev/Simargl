using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет исключение, которое возникает при вызове операции, которая не разрешена в текущем состоянии.
    /// </summary>
    public class OpenGLInvalidOperationException : OpenGLException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal OpenGLInvalidOperationException() : base("Операция не разрешена в текущем состоянии.")
        {

        }
    }
}
