using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет исключение, которое возникает при передаче числового аргумента, который находится вне допустимого диапазона значений.
    /// </summary>
    public class OpenGLInvalidValueException : OpenGLException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal OpenGLInvalidValueException() : base("Числовой аргумент вне допустимого диапазона значений.")
        {

        }
    }
}
