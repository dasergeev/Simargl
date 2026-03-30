using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda.Core
{
    /// <summary>
    /// CUFFT supports the following transform types 
    /// </summary>
    public enum CufftType
    {
        /// <summary>
        /// Real to Complex (interleaved).
        /// </summary>
        CUFFT_R2C = 0x2a,

        /// <summary>
        /// Complex (interleaved) to Real.
        /// </summary>
        CUFFT_C2R = 0x2c,

        /// <summary>
        /// Complex to Complex, interleaved.
        /// </summary>
        CUFFT_C2C = 0x29,

        /// <summary>
        /// Double to Double-Complex.
        /// </summary>
        CUFFT_D2Z = 0x6a,

        /// <summary>
        /// Double-Complex to Double.
        /// </summary>
        CUFFT_Z2D = 0x6c,

        /// <summary>
        /// Double-Complex to Double-Complex.
        /// </summary>
        CUFFT_Z2Z = 0x69
    }
}
