using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет исключение, которое возникает при вызове команды, которая приведёт к переполнению стека.
    /// </summary>
    public class OpenGLStackOverflowException : OpenGLException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal OpenGLStackOverflowException() : base("Команда вызовет переполнение стека.")
        {

        }
    }
}
