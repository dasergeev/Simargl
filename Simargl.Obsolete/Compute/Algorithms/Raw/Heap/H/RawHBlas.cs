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

public class RawHBlas
{
    public static void hermitianmatrixvectormultiply(complex[,] a,
        bool isupper,
        int i1,
        int i2,
        complex[] x,
        complex alpha,
        ref complex[] y,
        xparams _params)
    {
        int i = 0;
        int ba1 = 0;
        int by1 = 0;
        int by2 = 0;
        int bx1 = 0;
        int bx2 = 0;
        int n = 0;
        complex v = 0;
        int i_ = 0;
        int i1_ = 0;

        n = i2 - i1 + 1;
        if (n <= 0)
        {
            return;
        }

        //
        // Let A = L + D + U, where
        //  L is strictly lower triangular (main diagonal is zero)
        //  D is diagonal
        //  U is strictly upper triangular (main diagonal is zero)
        //
        // A*x = L*x + D*x + U*x
        //
        // Calculate D*x first
        //
        for (i = i1; i <= i2; i++)
        {
            y[i - i1 + 1] = a[i, i] * x[i - i1 + 1];
        }

        //
        // Add L*x + U*x
        //
        if (isupper)
        {
            for (i = i1; i <= i2 - 1; i++)
            {

                //
                // Add L*x to the result
                //
                v = x[i - i1 + 1];
                by1 = i - i1 + 2;
                by2 = n;
                ba1 = i + 1;
                i1_ = (ba1) - (by1);
                for (i_ = by1; i_ <= by2; i_++)
                {
                    y[i_] = y[i_] + v * math.conj(a[i, i_ + i1_]);
                }

                //
                // Add U*x to the result
                //
                bx1 = i - i1 + 2;
                bx2 = n;
                ba1 = i + 1;
                i1_ = (ba1) - (bx1);
                v = 0.0;
                for (i_ = bx1; i_ <= bx2; i_++)
                {
                    v += x[i_] * a[i, i_ + i1_];
                }
                y[i - i1 + 1] = y[i - i1 + 1] + v;
            }
        }
        else
        {
            for (i = i1 + 1; i <= i2; i++)
            {

                //
                // Add L*x to the result
                //
                bx1 = 1;
                bx2 = i - i1;
                ba1 = i1;
                i1_ = (ba1) - (bx1);
                v = 0.0;
                for (i_ = bx1; i_ <= bx2; i_++)
                {
                    v += x[i_] * a[i, i_ + i1_];
                }
                y[i - i1 + 1] = y[i - i1 + 1] + v;

                //
                // Add U*x to the result
                //
                v = x[i - i1 + 1];
                by1 = 1;
                by2 = i - i1;
                ba1 = i1;
                i1_ = (ba1) - (by1);
                for (i_ = by1; i_ <= by2; i_++)
                {
                    y[i_] = y[i_] + v * math.conj(a[i, i_ + i1_]);
                }
            }
        }
        for (i_ = 1; i_ <= n; i_++)
        {
            y[i_] = alpha * y[i_];
        }
    }


    public static void hermitianrank2update(complex[,] a,
        bool isupper,
        int i1,
        int i2,
        complex[] x,
        complex[] y,
        ref complex[] t,
        complex alpha,
        xparams _params)
    {
        int i = 0;
        int tp1 = 0;
        int tp2 = 0;
        complex v = 0;
        int i_ = 0;
        int i1_ = 0;

        if (isupper)
        {
            for (i = i1; i <= i2; i++)
            {
                tp1 = i + 1 - i1;
                tp2 = i2 - i1 + 1;
                v = alpha * x[i + 1 - i1];
                for (i_ = tp1; i_ <= tp2; i_++)
                {
                    t[i_] = v * math.conj(y[i_]);
                }
                v = math.conj(alpha) * y[i + 1 - i1];
                for (i_ = tp1; i_ <= tp2; i_++)
                {
                    t[i_] = t[i_] + v * math.conj(x[i_]);
                }
                i1_ = (tp1) - (i);
                for (i_ = i; i_ <= i2; i_++)
                {
                    a[i, i_] = a[i, i_] + t[i_ + i1_];
                }
            }
        }
        else
        {
            for (i = i1; i <= i2; i++)
            {
                tp1 = 1;
                tp2 = i + 1 - i1;
                v = alpha * x[i + 1 - i1];
                for (i_ = tp1; i_ <= tp2; i_++)
                {
                    t[i_] = v * math.conj(y[i_]);
                }
                v = math.conj(alpha) * y[i + 1 - i1];
                for (i_ = tp1; i_ <= tp2; i_++)
                {
                    t[i_] = t[i_] + v * math.conj(x[i_]);
                }
                i1_ = (tp1) - (i1);
                for (i_ = i1; i_ <= i; i_++)
                {
                    a[i, i_] = a[i, i_] + t[i_ + i1_];
                }
            }
        }
    }


}
