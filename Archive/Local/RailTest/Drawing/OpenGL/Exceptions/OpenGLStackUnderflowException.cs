using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет исключение, которое возникает при вызове команды, которая приведёт к опустошению стека.
    /// </summary>
    public class OpenGLStackUnderflowException : OpenGLException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal OpenGLStackUnderflowException() : base("Команда вызовет опустошение стека.")
        {

        }
    }
}
