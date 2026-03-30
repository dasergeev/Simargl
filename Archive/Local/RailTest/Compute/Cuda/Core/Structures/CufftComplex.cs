using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda.Core
{
    /// <summary>
    /// Представляет комплексное число.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 8)]
    public struct CufftComplex
    {
        /// <summary>
        /// Действительная часть.
        /// </summary>
        public float Real;

        /// <summary>
        /// Мнимая часть.
        /// </summary>
        public float Imaginary;
    }
}
