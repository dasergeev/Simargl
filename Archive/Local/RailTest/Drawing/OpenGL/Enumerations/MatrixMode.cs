using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет значение, определяющее стек матриц.
    /// </summary>
    public enum MatrixMode
    {
        /// <summary>
        /// Cтек матриц вида модели.
        /// </summary>
        ModelView,

        /// <summary>
        /// Cтек проекционных матриц.
        /// </summary>
        Projection,

        /// <summary>
        /// Cтек текстурных матриц.
        /// </summary>
        Texture
    }
}
