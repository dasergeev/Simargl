using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda
{
    /// <summary>
    /// Представляет свойства устройства.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CudaDeviceProperties
    {
        /// <summary>
        /// Поле для хранения имени устройства.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private readonly string _Name;

        /// <summary>
        /// 16-byte unique identifier.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private readonly byte[] _Uuid;

        /// <summary>
        /// 8-byte locally unique identifier. Value is undefined on TCC and non-Windows platforms.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        private readonly byte[] _Luid;

        /// <summary>
        /// LUID device node mask. Value is undefined on TCC and non-Windows platforms.
        /// </summary>
        private readonly uint _LuidDeviceNodeMask;

        /// <summary>
        /// Global memory available on device in bytes.
        /// </summary>
        private readonly ulong _TotalGlobalMemory;

        /// <summary>
        /// Shared memory available per block in bytes.
        /// </summary>
        private readonly ulong _SharedMemoryPerBlock;

        /// <summary>
        /// 32-bit registers available per block.
        /// </summary>
        private readonly int _RegistersPerBlock;

        /// <summary>
        /// Warp size in threads.
        /// </summary>
        private readonly int _WarpSize;

        /// <summary>
        /// Maximum pitch in bytes allowed by memory copies.
        /// </summary>
        private readonly ulong _MemoryPitch;

        /// <summary>
        /// Maximum number of threads per block.
        /// </summary>
        private readonly int _MaximumThreadsPerBlock;

        /// <summary>
        /// Maximum size of each dimension of a block.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private readonly int[] _MaximumThreadsDim;

        /// <summary>
        /// Maximum size of each dimension of a grid.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private readonly int[] _MaximumGridSize;

        /// <summary>
        /// Clock frequency in kilohertz.
        /// </summary>
        private readonly int _ClockRate;

        /// <summary>
        /// Constant memory available on device in bytes.
        /// </summary>
        private readonly ulong _TotalConstantMemory;

        /// <summary>
        /// Major compute capability.
        /// </summary>
        private readonly int _Major;

        /// <summary>
        /// Minor compute capability.
        /// </summary>
        private readonly int _Minor;

        /// <summary>
        /// Alignment requirement for textures.
        /// </summary>
        private readonly ulong _TextureAlignment;

        /// <summary>
        /// Pitch alignment requirement for texture references bound to pitched memory.
        /// </summary>
        private readonly ulong TexturePitchAlignment;

        /// <summary>
        /// Device can concurrently copy memory and execute a kernel. Deprecated. Use instead asyncEngineCount.
        /// </summary>
        private readonly int _DeviceOverlap;

        /// <summary>
        /// Number of multiprocessors on device.
        /// </summary>
        private readonly int _MultiprocessorsCount;

        /// <summary>
        /// Specified whether there is a run time limit on kernels.
        /// </summary>
        private readonly int _KernelExecTimeoutEnabled;

        /// <summary>
        /// Device is integrated as opposed to discrete.
        /// </summary>
        private readonly int _Integrated;

        /// <summary>
        /// Device can map host memory with cudaHostAlloc/cudaHostGetDevicePointer.
        /// </summary>
        private readonly int _CanMapHostMemory;

        /// <summary>
        /// Compute mode (See ::cudaComputeMode).
        /// </summary>
        private readonly int _ComputeMode;

        /// <summary>
        /// Maximum 1D texture size.
        /// </summary>
        private readonly int _MaximumTexture1D;

        /// <summary>
        /// Maximum 1D mipmapped texture size.
        /// </summary>
        private readonly int _MaximumTexture1DMipmap;

        /// <summary>
        /// Maximum size for 1D textures bound to linear memory.
        /// </summary>
        private readonly int _MaximumTexture1DLinear;

        /// <summary>
        /// Maximum 2D texture dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private readonly int[] _MaximumTexture2D;

        /// <summary>
        /// Maximum 2D mipmapped texture dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private readonly int[] _MaximumTexture2DMipmap;

        /// <summary>
        /// Maximum dimensions (width, height, pitch) for 2D textures bound to pitched memory.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private readonly int[] _MaximumTexture2DLinear;

        /// <summary>
        /// Maximum 2D texture dimensions if texture gather operations have to be performed.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private readonly int[] _MaximumTexture2DGather;

        /// <summary>
        /// Maximum 3D texture dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private readonly int[] _MaximumTexture3D;

        /// <summary>
        /// Maximum alternate 3D texture dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private readonly int[] _MaximumTexture3DAlternate;

        /// <summary>
        /// Maximum Cubemap texture dimensions.
        /// </summary>
        private readonly int _MaximumTextureCubemap;

        /// <summary>
        /// Maximum 1D layered texture dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private readonly int[] _MaximumTexture1DLayered;

        /// <summary>
        /// Maximum 2D layered texture dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private readonly int[] _MaximumTexture2DLayered;

        /// <summary>
        /// Maximum Cubemap layered texture dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private readonly int[] _MaximumTextureCubemapLayered;

        /// <summary>
        /// Maximum 1D surface size.
        /// </summary>
        private readonly int _MaximumSurface1D;

        /// <summary>
        /// Maximum 2D surface dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private readonly int[] _MaximumSurface2D;

        /// <summary>
        /// Maximum 3D surface dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private readonly int[] _MaximumSurface3D;

        /// <summary>
        /// Maximum 1D layered surface dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private readonly int[] _MaximumSurface1DLayered;

        /// <summary>
        /// Maximum 2D layered surface dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private readonly int[] _MaximumSurface2DLayered;

        /// <summary>
        /// Maximum Cubemap surface dimensions.
        /// </summary>
        private readonly int _MaximumSurfaceCubemap;

        /// <summary>
        /// Maximum Cubemap layered surface dimensions.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private readonly int[] _MaximumSurfaceCubemapLayered;

        /// <summary>
        /// Alignment requirements for surfaces.
        /// </summary>
        private readonly ulong _SurfaceAlignment;

        /// <summary>
        /// Device can possibly execute multiple kernels concurrently.
        /// </summary>
        private readonly int _ConcurrentKernels;

        /// <summary>
        /// Device has ECC support enabled.
        /// </summary>
        private readonly int _EccEnabled;

        /// <summary>
        /// PCI bus ID of the device.
        /// </summary>
        private readonly int _PciBusID;

        /// <summary>
        /// PCI device ID of the device.
        /// </summary>
        private readonly int _PciDeviceID;

        /// <summary>
        /// PCI domain ID of the device.
        /// </summary>
        private readonly int _PciDomainID;

        /// <summary>
        /// 1 if device is a Tesla device using TCC driver, 0 otherwise.
        /// </summary>
        private readonly int _TccDriver;

        /// <summary>
        /// Number of asynchronous engines.
        /// </summary>
        private readonly int _AsyncEngineCount;

        /// <summary>
        /// Device shares a unified address space with the host.
        /// </summary>
        private readonly int _UnifiedAddressing;

        /// <summary>
        /// Peak memory clock frequency in kilohertz.
        /// </summary>
        private readonly int _MemoryClockRate;

        /// <summary>
        /// Global memory bus width in bits.
        /// </summary>
        private readonly int _MemoryBusWidth;

        /// <summary>
        /// Size of L2 cache in bytes.
        /// </summary>
        private readonly int _L2CacheSize;

        /// <summary>
        /// Maximum resident threads per multiprocessor.
        /// </summary>
        private readonly int _aximumThreadsPerMultiProcessor;

        /// <summary>
        /// Device supports stream priorities.
        /// </summary>
        private readonly int _StreamPrioritiesSupported;

        /// <summary>
        /// Device supports caching globals in L1.
        /// </summary>
        private readonly int _GlobalL1CacheSupported;

        /// <summary>
        /// Device supports caching locals in L1.
        /// </summary>
        private readonly int _LocalL1CacheSupported;

        /// <summary>
        /// Shared memory available per multiprocessor in bytes.
        /// </summary>
        private readonly ulong _SharedMemPerMultiprocessor;

        /// <summary>
        /// 32-bit registers available per multiprocessor.
        /// </summary>
        private readonly int _RegistersPerMultiprocessor;

        /// <summary>
        /// Device supports allocating managed memory on this system.
        /// </summary>
        private readonly int _ManagedMemory;

        /// <summary>
        /// Device is on a multi-GPU board.
        /// </summary>
        private readonly int _IsMultiGpuBoard;

        /// <summary>
        /// Unique identifier for a group of devices on the same multi-GPU board.
        /// </summary>
        private readonly int _MultiGpuBoardGroupID;

        /// <summary>
        /// Link between the device and the host supports native atomic operations.
        /// </summary>
        private readonly int _HostNativeAtomicSupported;

        /// <summary>
        /// Ratio of single precision performance (in floating-point operations per second) to double precision performance.
        /// </summary>
        private readonly int _SingleToDoublePrecisionPerformanceRatio;

        /// <summary>
        /// Device supports coherently accessing pageable memory without calling cudaHostRegister on it.
        /// </summary>
        private readonly int _PageableMemoryAccess;

        /// <summary>
        /// Device can coherently access managed memory concurrently with the CPU.
        /// </summary>
        private readonly int _ConcurrentManagedAccess;

        /// <summary>
        /// Device supports Compute Preemption.
        /// </summary>
        private readonly int _ComputePreemptionSupported;

        /// <summary>
        /// Device can access host registered memory at the same virtual address as the CPU.
        /// </summary>
        private readonly int _CanUseHostPointerForRegisteredMemory;

        /// <summary>
        /// Device supports launching cooperative kernels via ::cudaLaunchCooperativeKernel
        /// </summary>
        private readonly int _CooperativeLaunch;

        /// <summary>
        /// Device can participate in cooperative kernels launched via ::cudaLaunchCooperativeKernelMultiDevice
        /// </summary>
        private readonly int _CooperativeMultiDeviceLaunch;

        /// <summary>
        /// Per device maximum shared memory per block usable by special opt in.
        /// </summary>
        private readonly ulong _SharedMemPerBlockOptIn;

        /// <summary>
        /// Device accesses pageable memory via the host's page tables.
        /// </summary>
        private readonly int _PageableMemoryAccessUsesHostPageTables;

        /// <summary>
        /// Host can directly access managed memory on the device without migration.
        /// </summary>
        private readonly int _DirectManagedMemoryAccessFromHost;

        /// <summary>
        /// Возвращает имя устройства.
        /// </summary>
        public string Name => _Name;

        ///// <summary>
        ///// 16-byte unique identifier.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        //private readonly byte[] _Uuid;

        ///// <summary>
        ///// 8-byte locally unique identifier. Value is undefined on TCC and non-Windows platforms.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        //private readonly byte[] _Luid;

        ///// <summary>
        ///// LUID device node mask. Value is undefined on TCC and non-Windows platforms.
        ///// </summary>
        //private readonly uint _LuidDeviceNodeMask;

        ///// <summary>
        ///// Global memory available on device in bytes.
        ///// </summary>
        //private readonly ulong _TotalGlobalMemory;

        ///// <summary>
        ///// Shared memory available per block in bytes.
        ///// </summary>
        //private readonly ulong _SharedMemoryPerBlock;

        ///// <summary>
        ///// 32-bit registers available per block.
        ///// </summary>
        //private readonly int _RegistersPerBlock;

        ///// <summary>
        ///// Warp size in threads.
        ///// </summary>
        //private readonly int _WarpSize;

        ///// <summary>
        ///// Maximum pitch in bytes allowed by memory copies.
        ///// </summary>
        //private readonly ulong _MemoryPitch;

        ///// <summary>
        ///// Maximum number of threads per block.
        ///// </summary>
        //private readonly int _MaximumThreadsPerBlock;

        ///// <summary>
        ///// Maximum size of each dimension of a block.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //private readonly int[] _MaximumThreadsDim;

        ///// <summary>
        ///// Maximum size of each dimension of a grid.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //private readonly int[] _MaximumGridSize;

        ///// <summary>
        ///// Clock frequency in kilohertz.
        ///// </summary>
        //private readonly int _ClockRate;

        ///// <summary>
        ///// Constant memory available on device in bytes.
        ///// </summary>
        //private readonly ulong _TotalConstantMemory;

        ///// <summary>
        ///// Major compute capability.
        ///// </summary>
        //private readonly int _Major;

        ///// <summary>
        ///// Minor compute capability.
        ///// </summary>
        //private readonly int _Minor;

        ///// <summary>
        ///// Alignment requirement for textures.
        ///// </summary>
        //private readonly ulong _TextureAlignment;

        ///// <summary>
        ///// Pitch alignment requirement for texture references bound to pitched memory.
        ///// </summary>
        //private readonly ulong TexturePitchAlignment;

        ///// <summary>
        ///// Device can concurrently copy memory and execute a kernel. Deprecated. Use instead asyncEngineCount.
        ///// </summary>
        //private readonly int _DeviceOverlap;

        ///// <summary>
        ///// Number of multiprocessors on device.
        ///// </summary>
        //private readonly int _MultiprocessorsCount;

        ///// <summary>
        ///// Specified whether there is a run time limit on kernels.
        ///// </summary>
        //private readonly int _KernelExecTimeoutEnabled;

        ///// <summary>
        ///// Device is integrated as opposed to discrete.
        ///// </summary>
        //private readonly int _Integrated;

        ///// <summary>
        ///// Device can map host memory with cudaHostAlloc/cudaHostGetDevicePointer.
        ///// </summary>
        //private readonly int _CanMapHostMemory;

        ///// <summary>
        ///// Compute mode (See ::cudaComputeMode).
        ///// </summary>
        //private readonly int _ComputeMode;

        ///// <summary>
        ///// Maximum 1D texture size.
        ///// </summary>
        //private readonly int _MaximumTexture1D;

        ///// <summary>
        ///// Maximum 1D mipmapped texture size.
        ///// </summary>
        //private readonly int _MaximumTexture1DMipmap;

        ///// <summary>
        ///// Maximum size for 1D textures bound to linear memory.
        ///// </summary>
        //private readonly int _MaximumTexture1DLinear;

        ///// <summary>
        ///// Maximum 2D texture dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //private readonly int[] _MaximumTexture2D;

        ///// <summary>
        ///// Maximum 2D mipmapped texture dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //private readonly int[] _MaximumTexture2DMipmap;

        ///// <summary>
        ///// Maximum dimensions (width, height, pitch) for 2D textures bound to pitched memory.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //private readonly int[] _MaximumTexture2DLinear;

        ///// <summary>
        ///// Maximum 2D texture dimensions if texture gather operations have to be performed.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //private readonly int[] _MaximumTexture2DGather;

        ///// <summary>
        ///// Maximum 3D texture dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //private readonly int[] _MaximumTexture3D;

        ///// <summary>
        ///// Maximum alternate 3D texture dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //private readonly int[] _MaximumTexture3DAlternate;

        ///// <summary>
        ///// Maximum Cubemap texture dimensions.
        ///// </summary>
        //private readonly int _MaximumTextureCubemap;

        ///// <summary>
        ///// Maximum 1D layered texture dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //private readonly int[] _MaximumTexture1DLayered;

        ///// <summary>
        ///// Maximum 2D layered texture dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //private readonly int[] _MaximumTexture2DLayered;

        ///// <summary>
        ///// Maximum Cubemap layered texture dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //private readonly int[] _MaximumTextureCubemapLayered;

        ///// <summary>
        ///// Maximum 1D surface size.
        ///// </summary>
        //private readonly int _MaximumSurface1D;

        ///// <summary>
        ///// Maximum 2D surface dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //private readonly int[] _MaximumSurface2D;

        ///// <summary>
        ///// Maximum 3D surface dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //private readonly int[] _MaximumSurface3D;

        ///// <summary>
        ///// Maximum 1D layered surface dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //private readonly int[] _MaximumSurface1DLayered;

        ///// <summary>
        ///// Maximum 2D layered surface dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //private readonly int[] _MaximumSurface2DLayered;

        ///// <summary>
        ///// Maximum Cubemap surface dimensions.
        ///// </summary>
        //private readonly int _MaximumSurfaceCubemap;

        ///// <summary>
        ///// Maximum Cubemap layered surface dimensions.
        ///// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //private readonly int[] _MaximumSurfaceCubemapLayered;

        ///// <summary>
        ///// Alignment requirements for surfaces.
        ///// </summary>
        //private readonly ulong _SurfaceAlignment;

        ///// <summary>
        ///// Device can possibly execute multiple kernels concurrently.
        ///// </summary>
        //private readonly int _ConcurrentKernels;

        ///// <summary>
        ///// Device has ECC support enabled.
        ///// </summary>
        //private readonly int _EccEnabled;

        ///// <summary>
        ///// PCI bus ID of the device.
        ///// </summary>
        //private readonly int _PciBusID;

        ///// <summary>
        ///// PCI device ID of the device.
        ///// </summary>
        //private readonly int _PciDeviceID;

        ///// <summary>
        ///// PCI domain ID of the device.
        ///// </summary>
        //private readonly int _PciDomainID;

        ///// <summary>
        ///// 1 if device is a Tesla device using TCC driver, 0 otherwise.
        ///// </summary>
        //private readonly int _TccDriver;

        ///// <summary>
        ///// Number of asynchronous engines.
        ///// </summary>
        //private readonly int _AsyncEngineCount;

        ///// <summary>
        ///// Device shares a unified address space with the host.
        ///// </summary>
        //private readonly int _UnifiedAddressing;

        ///// <summary>
        ///// Peak memory clock frequency in kilohertz.
        ///// </summary>
        //private readonly int _MemoryClockRate;

        ///// <summary>
        ///// Global memory bus width in bits.
        ///// </summary>
        //private readonly int _MemoryBusWidth;

        ///// <summary>
        ///// Size of L2 cache in bytes.
        ///// </summary>
        //private readonly int _L2CacheSize;

        ///// <summary>
        ///// Maximum resident threads per multiprocessor.
        ///// </summary>
        //private readonly int _aximumThreadsPerMultiProcessor;

        ///// <summary>
        ///// Device supports stream priorities.
        ///// </summary>
        //private readonly int _StreamPrioritiesSupported;

        ///// <summary>
        ///// Device supports caching globals in L1.
        ///// </summary>
        //private readonly int _GlobalL1CacheSupported;

        ///// <summary>
        ///// Device supports caching locals in L1.
        ///// </summary>
        //private readonly int _LocalL1CacheSupported;

        ///// <summary>
        ///// Shared memory available per multiprocessor in bytes.
        ///// </summary>
        //private readonly ulong _SharedMemPerMultiprocessor;

        ///// <summary>
        ///// 32-bit registers available per multiprocessor.
        ///// </summary>
        //private readonly int _RegistersPerMultiprocessor;

        ///// <summary>
        ///// Device supports allocating managed memory on this system.
        ///// </summary>
        //private readonly int _ManagedMemory;

        ///// <summary>
        ///// Device is on a multi-GPU board.
        ///// </summary>
        //private readonly int _IsMultiGpuBoard;

        ///// <summary>
        ///// Unique identifier for a group of devices on the same multi-GPU board.
        ///// </summary>
        //private readonly int _MultiGpuBoardGroupID;

        ///// <summary>
        ///// Link between the device and the host supports native atomic operations.
        ///// </summary>
        //private readonly int _HostNativeAtomicSupported;

        ///// <summary>
        ///// Ratio of single precision performance (in floating-point operations per second) to double precision performance.
        ///// </summary>
        //private readonly int _SingleToDoublePrecisionPerformanceRatio;

        ///// <summary>
        ///// Device supports coherently accessing pageable memory without calling cudaHostRegister on it.
        ///// </summary>
        //private readonly int _PageableMemoryAccess;

        ///// <summary>
        ///// Device can coherently access managed memory concurrently with the CPU.
        ///// </summary>
        //private readonly int _ConcurrentManagedAccess;

        ///// <summary>
        ///// Device supports Compute Preemption.
        ///// </summary>
        //private readonly int _ComputePreemptionSupported;

        ///// <summary>
        ///// Device can access host registered memory at the same virtual address as the CPU.
        ///// </summary>
        //private readonly int _CanUseHostPointerForRegisteredMemory;

        ///// <summary>
        ///// Device supports launching cooperative kernels via ::cudaLaunchCooperativeKernel
        ///// </summary>
        //private readonly int _CooperativeLaunch;

        ///// <summary>
        ///// Device can participate in cooperative kernels launched via ::cudaLaunchCooperativeKernelMultiDevice
        ///// </summary>
        //private readonly int _CooperativeMultiDeviceLaunch;

        ///// <summary>
        ///// Per device maximum shared memory per block usable by special opt in.
        ///// </summary>
        //private readonly ulong _SharedMemPerBlockOptIn;

        ///// <summary>
        ///// Device accesses pageable memory via the host's page tables.
        ///// </summary>
        //private readonly int _PageableMemoryAccessUsesHostPageTables;

        ///// <summary>
        ///// Host can directly access managed memory on the device without migration.
        ///// </summary>
        //private readonly int _DirectManagedMemoryAccessFromHost;
    }
}
