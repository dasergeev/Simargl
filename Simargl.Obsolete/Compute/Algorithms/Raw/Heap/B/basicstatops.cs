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

public class basicstatops
{
    /*************************************************************************
    Internal tied ranking subroutine.

    INPUT PARAMETERS:
        X       -   array to rank
        N       -   array size
        IsCentered- whether ranks are centered or not:
                    * True      -   ranks are centered in such way that  their
                                    sum is zero
                    * False     -   ranks are not centered
        Buf     -   temporary buffers
        
    NOTE: when IsCentered is True and all X[] are equal, this  function  fills
          X by zeros (exact zeros are used, not sum which is only approximately
          equal to zero).
    *************************************************************************/
    public static void rankx(double[] x,
        int n,
        bool iscentered,
        apserv.apbuffers buf,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        double tmp = 0;
        double voffs = 0;


        //
        // Prepare
        //
        if (n < 1)
        {
            return;
        }
        if (n == 1)
        {
            x[0] = 0;
            return;
        }
        if (ap.len(buf.ra1) < n)
        {
            buf.ra1 = new double[n];
        }
        if (ap.len(buf.ia1) < n)
        {
            buf.ia1 = new int[n];
        }
        for (i = 0; i <= n - 1; i++)
        {
            buf.ra1[i] = x[i];
            buf.ia1[i] = i;
        }
        tsort.tagsortfasti(ref buf.ra1, ref buf.ia1, ref buf.ra2, ref buf.ia2, n, _params);

        //
        // Special test for all values being equal
        //
        if ((double)(buf.ra1[0]) == (double)(buf.ra1[n - 1]))
        {
            if (iscentered)
            {
                tmp = 0.0;
            }
            else
            {
                tmp = (double)(n - 1) / (double)2;
            }
            for (i = 0; i <= n - 1; i++)
            {
                x[i] = tmp;
            }
            return;
        }

        //
        // compute tied ranks
        //
        i = 0;
        while (i <= n - 1)
        {
            j = i + 1;
            while (j <= n - 1)
            {
                if ((double)(buf.ra1[j]) != (double)(buf.ra1[i]))
                {
                    break;
                }
                j = j + 1;
            }
            for (k = i; k <= j - 1; k++)
            {
                buf.ra1[k] = (double)(i + j - 1) / (double)2;
            }
            i = j;
        }

        //
        // back to x
        //
        if (iscentered)
        {
            voffs = (double)(n - 1) / (double)2;
        }
        else
        {
            voffs = 0.0;
        }
        for (i = 0; i <= n - 1; i++)
        {
            x[buf.ia1[i]] = buf.ra1[i] - voffs;
        }
    }


    /*************************************************************************
    Internal untied ranking subroutine.

    INPUT PARAMETERS:
        X       -   array to rank
        N       -   array size
        Buf     -   temporary buffers

    Returns untied ranks (in case of a tie ranks are resolved arbitrarily).
    *************************************************************************/
    public static void rankxuntied(double[] x,
        int n,
        apserv.apbuffers buf,
        xparams _params)
    {
        int i = 0;


        //
        // Prepare
        //
        if (n < 1)
        {
            return;
        }
        if (n == 1)
        {
            x[0] = 0;
            return;
        }
        if (ap.len(buf.ra1) < n)
        {
            buf.ra1 = new double[n];
        }
        if (ap.len(buf.ia1) < n)
        {
            buf.ia1 = new int[n];
        }
        for (i = 0; i <= n - 1; i++)
        {
            buf.ra1[i] = x[i];
            buf.ia1[i] = i;
        }
        tsort.tagsortfasti(ref buf.ra1, ref buf.ia1, ref buf.ra2, ref buf.ia2, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            x[buf.ia1[i]] = i;
        }
    }


}
