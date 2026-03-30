using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda.Core
{
    /// <summary>
    /// All cuFFT Library return values except for CUFFT_SUCCESS indicate that the current API call failed and the user should reconfigure to correct the problem. The possible return values are defined as follows:
    /// </summary>
    public enum CufftResult
    {
        /// <summary>
        /// The cuFFT operation was successful.
        /// </summary>
        CUFFT_SUCCESS = 0,

        /// <summary>
        /// cuFFT was passed an invalid plan handle.
        /// </summary>
        CUFFT_INVALID_PLAN = 1,

        /// <summary>
        /// cuFFT failed to allocate GPU or CPU memory.
        /// </summary>
        CUFFT_ALLOC_FAILED = 2,

        /// <summary>
        /// No longer used.
        /// </summary>
        CUFFT_INVALID_TYPE = 3,

        /// <summary>
        /// User specified an invalid pointer or parameter.
        /// </summary>
        CUFFT_INVALID_VALUE = 4,

        /// <summary>
        /// Driver or internal cuFFT library error.
        /// </summary>
        CUFFT_INTERNAL_ERROR = 5,

        /// <summary>
        /// Failed to execute an FFT on the GPU.
        /// </summary>
        CUFFT_EXEC_FAILED = 6,

        /// <summary>
        /// The cuFFT library failed to initialize.
        /// </summary>
        CUFFT_SETUP_FAILED = 7,

        /// <summary>
        /// User specified an invalid transform size.
        /// </summary>
        CUFFT_INVALID_SIZE = 8,

        /// <summary>
        /// No longer used.
        /// </summary>
        CUFFT_UNALIGNED_DATA = 9,

        /// <summary>
        /// Missing parameters in call.
        /// </summary>
        CUFFT_INCOMPLETE_PARAMETER_LIST = 10, //  

        /// <summary>
        /// Execution of a plan was on different GPU than plan creation.
        /// </summary>
        CUFFT_INVALID_DEVICE = 11,

        /// <summary>
        /// Internal plan database error.
        /// </summary>
        CUFFT_PARSE_ERROR = 12,

        /// <summary>
        /// No workspace has been provided prior to plan execution.
        /// </summary>
        CUFFT_NO_WORKSPACE = 13,

        /// <summary>
        /// Function does not implement functionality for parameters given.
        /// </summary>
        CUFFT_NOT_IMPLEMENTED = 14,

        /// <summary>
        /// Used in previous versions.
        /// </summary>
        CUFFT_LICENSE_ERROR = 15,

        /// <summary>
        /// Operation is not supported for parameters given.
        /// </summary>
        CUFFT_NOT_SUPPORTED = 16
    }
}
