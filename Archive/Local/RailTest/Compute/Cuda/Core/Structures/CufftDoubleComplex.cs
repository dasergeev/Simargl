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
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 16)]
    public struct CufftDoubleComplex
    {
        /// <summary>
        /// Действительная часть.
        /// </summary>
        public double Real;

        /// <summary>
        /// Мнимая часть.
        /// </summary>
        public double Imaginary;
    }
}
