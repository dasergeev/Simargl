#pragma warning disable CS1591

#if ALGLIB_USE_SIMD
#define _ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
using Sse2 = System.Runtime.Intrinsics.X86.Sse2;
using Avx2 = System.Runtime.Intrinsics.X86.Avx2;
using Fma  = System.Runtime.Intrinsics.X86.Fma;
using Intrinsics = System.Runtime.Intrinsics;
#endif

#if ALGLIB_USE_SIMD && !_ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
#define _ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
using Sse2 = System.Runtime.Intrinsics.X86.Sse2;
using Avx2 = System.Runtime.Intrinsics.X86.Avx2;
using Fma  = System.Runtime.Intrinsics.X86.Fma;
using Intrinsics = System.Runtime.Intrinsics;
#endif

namespace Simargl.Algorithms.Raw;

public class ablasmkl
{
    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         12.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixgermkl(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        double alpha,
        double[] u,
        int iu,
        double[] v,
        int iv,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         12.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixrank1mkl(int m,
        int n,
        complex[,] a,
        int ia,
        int ja,
        complex[] u,
        int iu,
        complex[] v,
        int iv,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         12.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixrank1mkl(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        double[] u,
        int iu,
        double[] v,
        int iv,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         12.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixmvmkl(int m,
        int n,
        complex[,] a,
        int ia,
        int ja,
        int opa,
        complex[] x,
        int ix,
        complex[] y,
        int iy,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         12.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixmvmkl(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        int opa,
        double[] x,
        int ix,
        double[] y,
        int iy,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         12.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixgemvmkl(int m,
        int n,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int opa,
        double[] x,
        int ix,
        double beta,
        double[] y,
        int iy,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         12.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixtrsvmkl(int n,
        double[,] a,
        int ia,
        int ja,
        bool isupper,
        bool isunit,
        int optype,
        double[] x,
        int ix,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         01.10.2013
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixsyrkmkl(int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        double[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         01.10.2013
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixherkmkl(int n,
        int k,
        double alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        complex[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         01.10.2013
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixgemmmkl(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double[,] b,
        int ib,
        int jb,
        int optypeb,
        double beta,
        double[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         01.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixsymvmkl(int n,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        bool isupper,
        double[] x,
        int ix,
        double beta,
        double[] y,
        int iy,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         16.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixgemmmkl(int m,
        int n,
        int k,
        complex alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        complex[,] b,
        int ib,
        int jb,
        int optypeb,
        complex beta,
        complex[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         16.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixlefttrsmmkl(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         16.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixrighttrsmmkl(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         16.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixlefttrsmmkl(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel

      -- ALGLIB routine --
         16.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixrighttrsmmkl(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    NOTE:

    if function returned False, CholResult is NOT modified. Not ever referenced!
    if function returned True, CholResult is set to status of Cholesky decomposition
    (True on succeess).

      -- ALGLIB routine --
         16.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool spdmatrixcholeskymkl(double[,] a,
        int offs,
        int n,
        bool isupper,
        ref bool cholresult,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixplumkl(double[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    NOTE: this function needs preallocated output/temporary arrays.
          D and E must be at least max(M,N)-wide.

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixbdmkl(double[,] a,
        int m,
        int n,
        double[] d,
        double[] e,
        double[] tauq,
        double[] taup,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    If ByQ is True,  TauP is not used (can be empty array).
    If ByQ is False, TauQ is not used (can be empty array).

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixbdmultiplybymkl(double[,] qp,
        int m,
        int n,
        double[] tauq,
        double[] taup,
        double[,] z,
        int zrows,
        int zcolumns,
        bool byq,
        bool fromtheright,
        bool dotranspose,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    NOTE: Tau must be preallocated array with at least N-1 elements.

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixhessenbergmkl(double[,] a,
        int n,
        double[] tau,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    NOTE: Q must be preallocated N*N array

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixhessenbergunpackqmkl(double[,] a,
        int n,
        double[] tau,
        double[,] q,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    NOTE: Tau, D, E must be preallocated arrays;
          length(E)=length(Tau)=N-1 (or larger)
          length(D)=N (or larger)

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool smatrixtdmkl(double[,] a,
        int n,
        bool isupper,
        double[] tau,
        double[] d,
        double[] e,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    NOTE: Q must be preallocated N*N array

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool smatrixtdunpackqmkl(double[,] a,
        int n,
        bool isupper,
        double[] tau,
        double[,] q,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    NOTE: Tau, D, E must be preallocated arrays;
          length(E)=length(Tau)=N-1 (or larger)
          length(D)=N (or larger)

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool hmatrixtdmkl(complex[,] a,
        int n,
        bool isupper,
        complex[] tau,
        double[] d,
        double[] e,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    NOTE: Q must be preallocated N*N array

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool hmatrixtdunpackqmkl(complex[,] a,
        int n,
        bool isupper,
        complex[] tau,
        complex[,] q,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    Returns True if MKL was present and handled request (MKL  completion  code
    is returned as separate output parameter).

    D and E are pre-allocated arrays with length N (both of them!). On output,
    D constraints singular values, and E is destroyed.

    SVDResult is modified if and only if MKL is present.

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixbdsvdmkl(double[] d,
        double[] e,
        int n,
        bool isupper,
        double[,] u,
        int nru,
        double[,] c,
        int ncc,
        double[,] vt,
        int ncvt,
        ref bool svdresult,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based DHSEQR kernel.

    Returns True if MKL was present and handled request.

    WR and WI are pre-allocated arrays with length N.
    Z is pre-allocated array[N,N].

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixinternalschurdecompositionmkl(double[,] h,
        int n,
        int tneeded,
        int zneeded,
        double[] wr,
        double[] wi,
        double[,] z,
        ref int info,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based DTREVC kernel.

    Returns True if MKL was present and handled request.

    NOTE: this function does NOT support HOWMNY=3!!!!

    VL and VR are pre-allocated arrays with length N*N, if required. If particalar
    variables is not required, it can be dummy (empty) array.

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixinternaltrevcmkl(double[,] t,
        int n,
        int side,
        int howmny,
        double[,] vl,
        double[,] vr,
        ref int m,
        ref int info,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    Returns True if MKL was present and handled request (MKL  completion  code
    is returned as separate output parameter).

    D and E are pre-allocated arrays with length N (both of them!). On output,
    D constraints eigenvalues, and E is destroyed.

    Z is preallocated array[N,N] for ZNeeded<>0; ignored for ZNeeded=0.

    EVDResult is modified if and only if MKL is present.

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool smatrixtdevdmkl(double[] d,
        double[] e,
        int n,
        int zneeded,
        double[,] z,
        ref bool evdresult,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    MKL-based kernel.

    Returns True if MKL was present and handled request (MKL  completion  code
    is returned as separate output parameter).

    D and E are pre-allocated arrays with length N (both of them!). On output,
    D constraints eigenvalues, and E is destroyed.

    Z is preallocated array[N,N] for ZNeeded<>0; ignored for ZNeeded=0.

    EVDResult is modified if and only if MKL is present.

      -- ALGLIB routine --
         20.10.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool sparsegemvcrsmkl(int opa,
        int arows,
        int acols,
        double alpha,
        double[] vals,
        int[] cidx,
        int[] ridx,
        double[] x,
        int ix,
        double beta,
        double[] y,
        int iy,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


}
