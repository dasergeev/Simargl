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

public class scodes
{
    public static int getrdfserializationcode(xparams _params)
    {
        int result = 0;

        result = 1;
        return result;
    }


    public static int getkdtreeserializationcode(xparams _params)
    {
        int result = 0;

        result = 2;
        return result;
    }


    public static int getmlpserializationcode(xparams _params)
    {
        int result = 0;

        result = 3;
        return result;
    }


    public static int getmlpeserializationcode(xparams _params)
    {
        int result = 0;

        result = 4;
        return result;
    }


    public static int getrbfserializationcode(xparams _params)
    {
        int result = 0;

        result = 5;
        return result;
    }


    public static int getspline2dserializationcode(xparams _params)
    {
        int result = 0;

        result = 6;
        return result;
    }


    public static int getidwserializationcode(xparams _params)
    {
        int result = 0;

        result = 7;
        return result;
    }


    public static int getsparsematrixserializationcode(xparams _params)
    {
        int result = 0;

        result = 8;
        return result;
    }


    public static int getspline2dwithmissingnodesserializationcode(xparams _params)
    {
        int result = 0;

        result = 9;
        return result;
    }


    public static int getspline1dserializationcode(xparams _params)
    {
        int result = 0;

        result = 10;
        return result;
    }


    public static int getknnserializationcode(xparams _params)
    {
        int result = 0;

        result = 108;
        return result;
    }


    public static int getlptestserializationcode(xparams _params)
    {
        int result = 0;

        result = 200;
        return result;
    }


}

