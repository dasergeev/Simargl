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


/********************************************************************
math functions
********************************************************************/
public class math
{
    //public static System.Random RndObject = new System.Random(System.DateTime.Now.Millisecond);
    public static System.Random rndobject = new System.Random(System.DateTime.Now.Millisecond + 1000 * System.DateTime.Now.Second + 60 * 1000 * System.DateTime.Now.Minute);

    public const double machineepsilon = 5E-16;
    public const double maxrealnumber = 1E300;
    public const double minrealnumber = 1E-300;

    public static bool isfinite(double d)
    {
        return !System.Double.IsNaN(d) && !System.Double.IsInfinity(d);
    }

    public static double randomreal()
    {
        double r = 0;
        lock (rndobject) { r = rndobject.NextDouble(); }
        return r;
    }
    public static int randominteger(int N)
    {
        int r = 0;
        lock (rndobject) { r = rndobject.Next(N); }
        return r;
    }
    public static double sqr(double X)
    {
        return X * X;
    }
    public static double abscomplex(complex z)
    {
        double w;
        double xabs;
        double yabs;
        double v;

        xabs = System.Math.Abs(z.x);
        yabs = System.Math.Abs(z.y);
        w = xabs > yabs ? xabs : yabs;
        v = xabs < yabs ? xabs : yabs;
        if (v == 0)
            return w;
        else
        {
            double t = v / w;
            return w * System.Math.Sqrt(1 + t * t);
        }
    }
    public static complex conj(complex z)
    {
        return new complex(z.x, -z.y);
    }
    public static complex csqr(complex z)
    {
        return new complex(z.x * z.x - z.y * z.y, 2 * z.x * z.y);
    }

}
