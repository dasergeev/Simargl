using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda.Core
{
    /// <summary>
    /// Предоставляет прямой доступ к исходным функциям библиотеки CUDA. 
    /// </summary>
    public static partial class Original
    {
        /// <summary>
        /// Постоянная, определяющая имя библиотеки CUDA Runtime.
        /// </summary>
        public const string Cudart = "cudart64_102";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devPtr"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [DllImport(Cudart, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe CudaError cudaMalloc(void** devPtr, ulong size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devPtr"></param>
        /// <returns></returns>
        [DllImport(Cudart, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe CudaError cudaFree(void* devPtr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="src"></param>
        /// <param name="count"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        [DllImport(Cudart, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe CudaError cudaMemcpy(void* dst, void* src, ulong count, CudaMemcpyKind kind);





        //  cudaError_t CUDARTAPI cudaGetDeviceCount(int *count);
        //  cudaGetDeviceProperties

    }
}