using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет значение, определяющее маску очистки, выполняемой перед отрисовкой сцены.
    /// </summary>
    [Flags]
    public enum CleaningMask
    {
        /// <summary>
        /// Не выполнять очистку.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Выполнить очистку цветового буфера.
        /// </summary>
        Color = 0x1,

        /// <summary>
        /// Выполнить очистку буфера глубины.
        /// </summary>
        Depth = 0x2,

        /// <summary>
        /// Выполнить очистку буфера накопления.
        /// </summary>
        Accumulator = 0x4,

        /// <summary>
        /// Выполнить очистку трафаретного буфера.
        /// </summary>
        Stencil = 0x8,
    }
}
