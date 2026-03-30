using RailTest.Drawing.OpenGL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Код ошибки.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Не было зарегистрировано ни одной ошибки.
        /// </summary>
        NoError = Origin.GL_NO_ERROR,

        /// <summary>
        /// Недопустимое значение для перечисляемого аргумента.
        /// </summary>
        InvalidEnumeration = Origin.GL_INVALID_ENUM,

        /// <summary>
        /// Числовой аргумент находится вне допустимого диапазона значений.
        /// </summary>
        InvalidValue = Origin.GL_INVALID_VALUE,

        /// <summary>
        /// Указанная операция не разрешена в текущем состоянии.
        /// </summary>
        InvalidOperation = Origin.GL_INVALID_OPERATION,

        /// <summary>
        /// Команда вызовет переполнение стека.
        /// </summary>
        StackOverflow = Origin.GL_STACK_OVERFLOW,

        /// <summary>
        /// Команда вызовет опустошение стека.
        /// </summary>
        StackUnderflow = Origin.GL_STACK_UNDERFLOW,

        /// <summary>
        /// Недостаточно памяти для выполнения команды.
        /// </summary>
        OutOfMemory = Origin.GL_OUT_OF_MEMORY,
    }
}
