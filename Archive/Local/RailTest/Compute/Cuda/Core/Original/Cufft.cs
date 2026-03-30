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
        /// Постоянная, определяющая имя библиотеки cuFFT.
        /// </summary>
        public const string Cufft = "cufft64_10";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftCreate(CufftHandle * handle);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftDestroy(CufftHandle plan);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nx"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftEstimate1d(int nx, CufftType type, int batch, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <param name="type"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftEstimate2d(int nx, int ny, CufftType type, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <param name="nz"></param>
		/// <param name="type"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftEstimate3d(int nx, int ny, int nz, CufftType type, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rank"></param>
		/// <param name="n"></param>
		/// <param name="inembed"></param>
		/// <param name="istride"></param>
		/// <param name="idist"></param>
		/// <param name="onembed"></param>
		/// <param name="ostride"></param>
		/// <param name="odist"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftEstimateMany(int rank, int* n, int* inembed, int istride, int idist, int* onembed, int ostride, int odist, CufftType type, int batch, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="idata"></param>
		/// <param name="odata"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftExecC2C(CufftHandle plan, CufftComplex * idata, CufftComplex * odata, int direction);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="idata"></param>
		/// <param name="odata"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftExecC2R(CufftHandle plan, CufftComplex * idata, float * odata);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="idata"></param>
		/// <param name="odata"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftExecD2Z(CufftHandle plan, double * idata, CufftDoubleComplex * odata);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="idata"></param>
		/// <param name="odata"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftExecR2C(CufftHandle plan, float * idata, CufftComplex * odata);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="idata"></param>
		/// <param name="odata"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftExecZ2D(CufftHandle plan, CufftDoubleComplex * idata, double * odata);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="idata"></param>
		/// <param name="odata"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftExecZ2Z(CufftHandle plan, CufftDoubleComplex * idata, CufftDoubleComplex * odata, int direction);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftGetProperty(LibraryPropertyType type, int* value);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftGetSize(CufftHandle handle, ulong *workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="nx"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftGetSize1d(CufftHandle handle, int nx, CufftType type, int batch, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <param name="type"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftGetSize2d(CufftHandle handle, int nx, int ny, CufftType type, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <param name="nz"></param>
		/// <param name="type"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftGetSize3d(CufftHandle handle, int nx, int ny, int nz, CufftType type, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="rank"></param>
		/// <param name="n"></param>
		/// <param name="inembed"></param>
		/// <param name="istride"></param>
		/// <param name="idist"></param>
		/// <param name="onembed"></param>
		/// <param name="ostride"></param>
		/// <param name="odist"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <param name="workArea"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftGetSizeMany(CufftHandle handle, int rank, int* n, int* inembed, int istride, int idist, int* onembed, int ostride, int odist, CufftType type, int batch, ulong * workArea);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="rank"></param>
		/// <param name="n"></param>
		/// <param name="inembed"></param>
		/// <param name="istride"></param>
		/// <param name="idist"></param>
		/// <param name="onembed"></param>
		/// <param name="ostride"></param>
		/// <param name="odist"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftGetSizeMany64(CufftHandle plan, int rank, long* n, long* inembed, long istride, long idist, long* onembed, long ostride, long odist, CufftType type, long batch, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="version"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftGetVersion(int* version);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="nx"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftMakePlan1d(CufftHandle plan, int nx, CufftType type, int batch, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <param name="type"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftMakePlan2d(CufftHandle plan, int nx, int ny, CufftType type, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <param name="nz"></param>
		/// <param name="type"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftMakePlan3d(CufftHandle plan, int nx, int ny, int nz, CufftType type, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="rank"></param>
		/// <param name="n"></param>
		/// <param name="inembed"></param>
		/// <param name="istride"></param>
		/// <param name="idist"></param>
		/// <param name="onembed"></param>
		/// <param name="ostride"></param>
		/// <param name="odist"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftMakePlanMany(CufftHandle plan, int rank, int* n, int* inembed, int istride, int idist, int* onembed, int ostride, int odist, CufftType type, int batch, ulong * workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="rank"></param>
		/// <param name="n"></param>
		/// <param name="inembed"></param>
		/// <param name="istride"></param>
		/// <param name="idist"></param>
		/// <param name="onembed"></param>
		/// <param name="ostride"></param>
		/// <param name="odist"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <param name="workSize"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftMakePlanMany64(CufftHandle plan, int rank, long* n, long* inembed, long istride, long idist, long* onembed, long ostride, long odist, CufftType type, long batch, ulong* workSize);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="nx"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftPlan1d(CufftHandle * plan, int nx, CufftType type, int batch);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftPlan2d(CufftHandle * plan, int nx, int ny, CufftType type);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <param name="nz"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftPlan3d(CufftHandle * plan, int nx, int ny, int nz, CufftType type);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="rank"></param>
		/// <param name="n"></param>
		/// <param name="inembed"></param>
		/// <param name="istride"></param>
		/// <param name="idist"></param>
		/// <param name="onembed"></param>
		/// <param name="ostride"></param>
		/// <param name="odist"></param>
		/// <param name="type"></param>
		/// <param name="batch"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftPlanMany(CufftHandle * plan, int rank, int* n, int* inembed, int istride, int idist, int* onembed, int ostride, int odist, CufftType type, int batch);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="autoAllocate"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftSetAutoAllocation(CufftHandle plan, int autoAllocate);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="plan"></param>
		/// <param name="workArea"></param>
		/// <returns></returns>
		[DllImport(Cufft, CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		[HandleProcessCorruptedStateExceptions]
		[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
		[CLSCompliant(false)]
		public static extern unsafe CufftResult cufftSetWorkArea(CufftHandle plan, void *workArea);
	}
}
