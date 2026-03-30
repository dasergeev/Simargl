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

public class tsort
{
    /*************************************************************************
    This function sorts array of real keys by ascending.

    Its results are:
    * sorted array A
    * permutation tables P1, P2

    Algorithm outputs permutation tables using two formats:
    * as usual permutation of [0..N-1]. If P1[i]=j, then sorted A[i]  contains
      value which was moved there from J-th position.
    * as a sequence of pairwise permutations. Sorted A[] may  be  obtained  by
      swaping A[i] and A[P2[i]] for all i from 0 to N-1.
      
    INPUT PARAMETERS:
        A       -   unsorted array
        N       -   array size

    OUPUT PARAMETERS:
        A       -   sorted array
        P1, P2  -   permutation tables, array[N]
        
    NOTES:
        this function assumes that A[] is finite; it doesn't checks that
        condition. All other conditions (size of input arrays, etc.) are not
        checked too.

      -- ALGLIB --
         Copyright 14.05.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsort(ref double[] a,
        int n,
        ref int[] p1,
        ref int[] p2,
        xparams _params)
    {
        apserv.apbuffers buf = new apserv.apbuffers();

        p1 = new int[0];
        p2 = new int[0];

        tagsortbuf(ref a, n, ref p1, ref p2, buf, _params);
    }


    /*************************************************************************
    Buffered variant of TagSort, which accepts preallocated output arrays as
    well as special structure for buffered allocations. If arrays are too
    short, they are reallocated. If they are large enough, no memory
    allocation is done.

    It is intended to be used in the performance-critical parts of code, where
    additional allocations can lead to severe performance degradation

      -- ALGLIB --
         Copyright 14.05.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsortbuf(ref double[] a,
        int n,
        ref int[] p1,
        ref int[] p2,
        apserv.apbuffers buf,
        xparams _params)
    {
        int i = 0;
        int lv = 0;
        int lp = 0;
        int rv = 0;
        int rp = 0;


        //
        // Special cases
        //
        if (n <= 0)
        {
            return;
        }
        if (n == 1)
        {
            apserv.ivectorsetlengthatleast(ref p1, 1, _params);
            apserv.ivectorsetlengthatleast(ref p2, 1, _params);
            p1[0] = 0;
            p2[0] = 0;
            return;
        }

        //
        // General case, N>1: prepare permutations table P1
        //
        apserv.ivectorsetlengthatleast(ref p1, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            p1[i] = i;
        }

        //
        // General case, N>1: sort, update P1
        //
        apserv.rvectorsetlengthatleast(ref buf.ra0, n, _params);
        apserv.ivectorsetlengthatleast(ref buf.ia0, n, _params);
        tagsortfasti(ref a, ref p1, ref buf.ra0, ref buf.ia0, n, _params);

        //
        // General case, N>1: fill permutations table P2
        //
        // To fill P2 we maintain two arrays:
        // * PV (Buf.IA0), Position(Value). PV[i] contains position of I-th key at the moment
        // * VP (Buf.IA1), Value(Position). VP[i] contains key which has position I at the moment
        //
        // At each step we making permutation of two items:
        //   Left, which is given by position/value pair LP/LV
        //   and Right, which is given by RP/RV
        // and updating PV[] and VP[] correspondingly.
        //
        apserv.ivectorsetlengthatleast(ref buf.ia0, n, _params);
        apserv.ivectorsetlengthatleast(ref buf.ia1, n, _params);
        apserv.ivectorsetlengthatleast(ref p2, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            buf.ia0[i] = i;
            buf.ia1[i] = i;
        }
        for (i = 0; i <= n - 1; i++)
        {

            //
            // calculate LP, LV, RP, RV
            //
            lp = i;
            lv = buf.ia1[lp];
            rv = p1[i];
            rp = buf.ia0[rv];

            //
            // Fill P2
            //
            p2[i] = rp;

            //
            // update PV and VP
            //
            buf.ia1[lp] = rv;
            buf.ia1[rp] = lv;
            buf.ia0[lv] = rp;
            buf.ia0[rv] = lp;
        }
    }


    /*************************************************************************
    Same as TagSort, but optimized for real keys and integer labels.

    A is sorted, and same permutations are applied to B.

    NOTES:
    1.  this function assumes that A[] is finite; it doesn't checks that
        condition. All other conditions (size of input arrays, etc.) are not
        checked too.
    2.  this function uses two buffers, BufA and BufB, each is N elements large.
        They may be preallocated (which will save some time) or not, in which
        case function will automatically allocate memory.

      -- ALGLIB --
         Copyright 11.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsortfasti(ref double[] a,
        ref int[] b,
        ref double[] bufa,
        ref int[] bufb,
        int n,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        bool isascending = new bool();
        bool isdescending = new bool();
        double tmpr = 0;
        int tmpi = 0;


        //
        // Special case
        //
        if (n <= 1)
        {
            return;
        }

        //
        // Test for already sorted set
        //
        isascending = true;
        isdescending = true;
        for (i = 1; i <= n - 1; i++)
        {
            isascending = isascending && a[i] >= a[i - 1];
            isdescending = isdescending && a[i] <= a[i - 1];
        }
        if (isascending)
        {
            return;
        }
        if (isdescending)
        {
            for (i = 0; i <= n - 1; i++)
            {
                j = n - 1 - i;
                if (j <= i)
                {
                    break;
                }
                tmpr = a[i];
                a[i] = a[j];
                a[j] = tmpr;
                tmpi = b[i];
                b[i] = b[j];
                b[j] = tmpi;
            }
            return;
        }

        //
        // General case
        //
        if (ap.len(bufa) < n)
        {
            bufa = new double[n];
        }
        if (ap.len(bufb) < n)
        {
            bufb = new int[n];
        }
        tagsortfastirec(ref a, ref b, ref bufa, ref bufb, 0, n - 1, _params);
    }


    /*************************************************************************
    Same as TagSort, but optimized for real keys and real labels.

    A is sorted, and same permutations are applied to B.

    NOTES:
    1.  this function assumes that A[] is finite; it doesn't checks that
        condition. All other conditions (size of input arrays, etc.) are not
        checked too.
    2.  this function uses two buffers, BufA and BufB, each is N elements large.
        They may be preallocated (which will save some time) or not, in which
        case function will automatically allocate memory.

      -- ALGLIB --
         Copyright 11.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsortfastr(ref double[] a,
        ref double[] b,
        ref double[] bufa,
        ref double[] bufb,
        int n,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        bool isascending = new bool();
        bool isdescending = new bool();
        double tmpr = 0;


        //
        // Special case
        //
        if (n <= 1)
        {
            return;
        }

        //
        // Test for already sorted set
        //
        isascending = true;
        isdescending = true;
        for (i = 1; i <= n - 1; i++)
        {
            isascending = isascending && a[i] >= a[i - 1];
            isdescending = isdescending && a[i] <= a[i - 1];
        }
        if (isascending)
        {
            return;
        }
        if (isdescending)
        {
            for (i = 0; i <= n - 1; i++)
            {
                j = n - 1 - i;
                if (j <= i)
                {
                    break;
                }
                tmpr = a[i];
                a[i] = a[j];
                a[j] = tmpr;
                tmpr = b[i];
                b[i] = b[j];
                b[j] = tmpr;
            }
            return;
        }

        //
        // General case
        //
        if (ap.len(bufa) < n)
        {
            bufa = new double[n];
        }
        if (ap.len(bufb) < n)
        {
            bufb = new double[n];
        }
        tagsortfastrrec(ref a, ref b, ref bufa, ref bufb, 0, n - 1, _params);
    }


    /*************************************************************************
    Same as TagSort, but optimized for real keys without labels.

    A is sorted, and that's all.

    NOTES:
    1.  this function assumes that A[] is finite; it doesn't checks that
        condition. All other conditions (size of input arrays, etc.) are not
        checked too.
    2.  this function uses buffer, BufA, which is N elements large. It may be
        preallocated (which will save some time) or not, in which case
        function will automatically allocate memory.

      -- ALGLIB --
         Copyright 11.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsortfast(ref double[] a,
        ref double[] bufa,
        int n,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        bool isascending = new bool();
        bool isdescending = new bool();
        double tmpr = 0;


        //
        // Special case
        //
        if (n <= 1)
        {
            return;
        }

        //
        // Test for already sorted set
        //
        isascending = true;
        isdescending = true;
        for (i = 1; i <= n - 1; i++)
        {
            isascending = isascending && a[i] >= a[i - 1];
            isdescending = isdescending && a[i] <= a[i - 1];
        }
        if (isascending)
        {
            return;
        }
        if (isdescending)
        {
            for (i = 0; i <= n - 1; i++)
            {
                j = n - 1 - i;
                if (j <= i)
                {
                    break;
                }
                tmpr = a[i];
                a[i] = a[j];
                a[j] = tmpr;
            }
            return;
        }

        //
        // General case
        //
        if (ap.len(bufa) < n)
        {
            bufa = new double[n];
        }
        tagsortfastrec(ref a, ref bufa, 0, n - 1, _params);
    }


    /*************************************************************************
    Sorting function optimized for integer keys and real labels, can be used
    to sort middle of the array

    A is sorted, and same permutations are applied to B.

    NOTES:
        this function assumes that A[] is finite; it doesn't checks that
        condition. All other conditions (size of input arrays, etc.) are not
        checked too.

      -- ALGLIB --
         Copyright 11.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsortmiddleir(ref int[] a,
        ref double[] b,
        int offset,
        int n,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int t = 0;
        int tmp = 0;
        double tmpr = 0;
        int p0 = 0;
        int p1 = 0;
        int at = 0;
        int ak = 0;
        int ak1 = 0;
        double bt = 0;


        //
        // Special cases
        //
        if (n <= 1)
        {
            return;
        }

        //
        // General case, N>1: sort, update B
        //
        for (i = 2; i <= n; i++)
        {
            t = i;
            while (t != 1)
            {
                k = t / 2;
                p0 = offset + k - 1;
                p1 = offset + t - 1;
                ak = a[p0];
                at = a[p1];
                if (ak >= at)
                {
                    break;
                }
                a[p0] = at;
                a[p1] = ak;
                tmpr = b[p0];
                b[p0] = b[p1];
                b[p1] = tmpr;
                t = k;
            }
        }
        for (i = n - 1; i >= 1; i--)
        {
            p0 = offset + 0;
            p1 = offset + i;
            tmp = a[p1];
            a[p1] = a[p0];
            a[p0] = tmp;
            at = tmp;
            tmpr = b[p1];
            b[p1] = b[p0];
            b[p0] = tmpr;
            bt = tmpr;
            t = 0;
            while (true)
            {
                k = 2 * t + 1;
                if (k + 1 > i)
                {
                    break;
                }
                p0 = offset + t;
                p1 = offset + k;
                ak = a[p1];
                if (k + 1 < i)
                {
                    ak1 = a[p1 + 1];
                    if (ak1 > ak)
                    {
                        ak = ak1;
                        p1 = p1 + 1;
                        k = k + 1;
                    }
                }
                if (at >= ak)
                {
                    break;
                }
                a[p1] = at;
                a[p0] = ak;
                b[p0] = b[p1];
                b[p1] = bt;
                t = k;
            }
        }
    }


    /*************************************************************************
    Sorting function optimized for real keys and integer labels, can be used
    to sort middle of the array

    A is sorted, and same permutations are applied to B.

    NOTES:
        this function assumes that A[] is finite; it doesn't checks that
        condition. All other conditions (size of input arrays, etc.) are not
        checked too.

      -- ALGLIB --
         Copyright 11.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsortmiddleri(double[] a,
        int[] b,
        int offset,
        int n,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int t = 0;
        double tmpa = 0;
        int tmpi = 0;
        int p0 = 0;
        int p1 = 0;
        double at = 0;
        double ak = 0;
        double ak1 = 0;
        int bt = 0;


        //
        // Special cases
        //
        if (n <= 1)
        {
            return;
        }

        //
        // General case, N>1: sort, update B
        //
        for (i = 2; i <= n; i++)
        {
            t = i;
            while (t != 1)
            {
                k = t / 2;
                p0 = offset + k - 1;
                p1 = offset + t - 1;
                ak = a[p0];
                at = a[p1];
                if ((double)(ak) >= (double)(at))
                {
                    break;
                }
                a[p0] = at;
                a[p1] = ak;
                tmpi = b[p0];
                b[p0] = b[p1];
                b[p1] = tmpi;
                t = k;
            }
        }
        for (i = n - 1; i >= 1; i--)
        {
            p0 = offset + 0;
            p1 = offset + i;
            tmpa = a[p1];
            a[p1] = a[p0];
            a[p0] = tmpa;
            at = tmpa;
            tmpi = b[p1];
            b[p1] = b[p0];
            b[p0] = tmpi;
            bt = tmpi;
            t = 0;
            while (true)
            {
                k = 2 * t + 1;
                if (k + 1 > i)
                {
                    break;
                }
                p0 = offset + t;
                p1 = offset + k;
                ak = a[p1];
                if (k + 1 < i)
                {
                    ak1 = a[p1 + 1];
                    if ((double)(ak1) > (double)(ak))
                    {
                        ak = ak1;
                        p1 = p1 + 1;
                        k = k + 1;
                    }
                }
                if ((double)(at) >= (double)(ak))
                {
                    break;
                }
                a[p1] = at;
                a[p0] = ak;
                b[p0] = b[p1];
                b[p1] = bt;
                t = k;
            }
        }
    }


    /*************************************************************************
    Sorting function optimized for integer keys and integer labels, can be used
    to sort middle of the array

    A is sorted, and same permutations are applied to B.

    NOTES:
        this function assumes that A[] is finite; it doesn't checks that
        condition. All other conditions (size of input arrays, etc.) are not
        checked too.

      -- ALGLIB --
         Copyright 11.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsortmiddleii(ref int[] a,
        ref int[] b,
        int offset,
        int n,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int t = 0;
        int tmp = 0;
        int tmpi = 0;
        int p0 = 0;
        int p1 = 0;
        int at = 0;
        int ak = 0;
        int ak1 = 0;
        int bt = 0;


        //
        // Special cases
        //
        if (n <= 1)
        {
            return;
        }

        //
        // General case, N>1: sort, update B
        //
        for (i = 2; i <= n; i++)
        {
            t = i;
            while (t != 1)
            {
                k = t / 2;
                p0 = offset + k - 1;
                p1 = offset + t - 1;
                ak = a[p0];
                at = a[p1];
                if (ak >= at)
                {
                    break;
                }
                a[p0] = at;
                a[p1] = ak;
                tmpi = b[p0];
                b[p0] = b[p1];
                b[p1] = tmpi;
                t = k;
            }
        }
        for (i = n - 1; i >= 1; i--)
        {
            p0 = offset + 0;
            p1 = offset + i;
            tmp = a[p1];
            a[p1] = a[p0];
            a[p0] = tmp;
            at = tmp;
            tmpi = b[p1];
            b[p1] = b[p0];
            b[p0] = tmpi;
            bt = tmpi;
            t = 0;
            while (true)
            {
                k = 2 * t + 1;
                if (k + 1 > i)
                {
                    break;
                }
                p0 = offset + t;
                p1 = offset + k;
                ak = a[p1];
                if (k + 1 < i)
                {
                    ak1 = a[p1 + 1];
                    if (ak1 > ak)
                    {
                        ak = ak1;
                        p1 = p1 + 1;
                        k = k + 1;
                    }
                }
                if (at >= ak)
                {
                    break;
                }
                a[p1] = at;
                a[p0] = ak;
                b[p0] = b[p1];
                b[p1] = bt;
                t = k;
            }
        }
    }


    /*************************************************************************
    Sorting function optimized for integer keys and real labels, can be used
    to sort middle of the array

    A is sorted, and same permutations are applied to B.

    NOTES:
        this function assumes that A[] is finite; it doesn't checks that
        condition. All other conditions (size of input arrays, etc.) are not
        checked too.

      -- ALGLIB --
         Copyright 11.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void tagsortmiddlei(ref int[] a,
        int offset,
        int n,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int t = 0;
        int tmp = 0;
        int p0 = 0;
        int p1 = 0;
        int at = 0;
        int ak = 0;
        int ak1 = 0;


        //
        // Special cases
        //
        if (n <= 1)
        {
            return;
        }

        //
        // General case, N>1: sort, update B
        //
        for (i = 2; i <= n; i++)
        {
            t = i;
            while (t != 1)
            {
                k = t / 2;
                p0 = offset + k - 1;
                p1 = offset + t - 1;
                ak = a[p0];
                at = a[p1];
                if (ak >= at)
                {
                    break;
                }
                a[p0] = at;
                a[p1] = ak;
                t = k;
            }
        }
        for (i = n - 1; i >= 1; i--)
        {
            p0 = offset + 0;
            p1 = offset + i;
            tmp = a[p1];
            a[p1] = a[p0];
            a[p0] = tmp;
            at = tmp;
            t = 0;
            while (true)
            {
                k = 2 * t + 1;
                if (k + 1 > i)
                {
                    break;
                }
                p0 = offset + t;
                p1 = offset + k;
                ak = a[p1];
                if (k + 1 < i)
                {
                    ak1 = a[p1 + 1];
                    if (ak1 > ak)
                    {
                        ak = ak1;
                        p1 = p1 + 1;
                        k = k + 1;
                    }
                }
                if (at >= ak)
                {
                    break;
                }
                a[p1] = at;
                a[p0] = ak;
                t = k;
            }
        }
    }


    /*************************************************************************
    Sorting function optimized for integer values (only keys, no labels),  can
    be used to sort middle of the array

      -- ALGLIB --
         Copyright 11.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void sortmiddlei(int[] a,
        int offset,
        int n,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int t = 0;
        int tmp = 0;
        int p0 = 0;
        int p1 = 0;
        int at = 0;
        int ak = 0;
        int ak1 = 0;


        //
        // Special cases
        //
        if (n <= 1)
        {
            return;
        }

        //
        // General case, N>1: sort, update B
        //
        for (i = 2; i <= n; i++)
        {
            t = i;
            while (t != 1)
            {
                k = t / 2;
                p0 = offset + k - 1;
                p1 = offset + t - 1;
                ak = a[p0];
                at = a[p1];
                if (ak >= at)
                {
                    break;
                }
                a[p0] = at;
                a[p1] = ak;
                t = k;
            }
        }
        for (i = n - 1; i >= 1; i--)
        {
            p0 = offset + 0;
            p1 = offset + i;
            tmp = a[p1];
            a[p1] = a[p0];
            a[p0] = tmp;
            at = tmp;
            t = 0;
            while (true)
            {
                k = 2 * t + 1;
                if (k + 1 > i)
                {
                    break;
                }
                p0 = offset + t;
                p1 = offset + k;
                ak = a[p1];
                if (k + 1 < i)
                {
                    ak1 = a[p1 + 1];
                    if (ak1 > ak)
                    {
                        ak = ak1;
                        p1 = p1 + 1;
                        k = k + 1;
                    }
                }
                if (at >= ak)
                {
                    break;
                }
                a[p1] = at;
                a[p0] = ak;
                t = k;
            }
        }
    }


    /*************************************************************************
    Heap operations: adds element to the heap

    PARAMETERS:
        A       -   heap itself, must be at least array[0..N]
        B       -   array of integer tags, which are updated according to
                    permutations in the heap
        N       -   size of the heap (without new element).
                    updated on output
        VA      -   value of the element being added
        VB      -   value of the tag

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void tagheappushi(ref double[] a,
        ref int[] b,
        ref int n,
        double va,
        int vb,
        xparams _params)
    {
        int j = 0;
        int k = 0;
        double v = 0;

        if (n < 0)
        {
            return;
        }

        //
        // N=0 is a special case
        //
        if (n == 0)
        {
            a[0] = va;
            b[0] = vb;
            n = n + 1;
            return;
        }

        //
        // add current point to the heap
        // (add to the bottom, then move up)
        //
        // we don't write point to the heap
        // until its final position is determined
        // (it allow us to reduce number of array access operations)
        //
        j = n;
        n = n + 1;
        while (j > 0)
        {
            k = (j - 1) / 2;
            v = a[k];
            if (v < va)
            {

                //
                // swap with higher element
                //
                a[j] = v;
                b[j] = b[k];
                j = k;
            }
            else
            {

                //
                // element in its place. terminate.
                //
                break;
            }
        }
        a[j] = va;
        b[j] = vb;
    }


    /*************************************************************************
    Heap operations: replaces top element with new element
    (which is moved down)

    PARAMETERS:
        A       -   heap itself, must be at least array[0..N-1]
        B       -   array of integer tags, which are updated according to
                    permutations in the heap
        N       -   size of the heap
        VA      -   value of the element which replaces top element
        VB      -   value of the tag

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void tagheapreplacetopi(ref double[] a,
        ref int[] b,
        int n,
        double va,
        int vb,
        xparams _params)
    {
        int j = 0;
        int k1 = 0;
        int k2 = 0;
        double v = 0;
        double v1 = 0;
        double v2 = 0;

        if (n < 1)
        {
            return;
        }

        //
        // N=1 is a special case
        //
        if (n == 1)
        {
            a[0] = va;
            b[0] = vb;
            return;
        }

        //
        // move down through heap:
        // * J  -   current element
        // * K1 -   first child (always exists)
        // * K2 -   second child (may not exists)
        //
        // we don't write point to the heap
        // until its final position is determined
        // (it allow us to reduce number of array access operations)
        //
        j = 0;
        k1 = 1;
        k2 = 2;
        while (k1 < n)
        {
            if (k2 >= n)
            {

                //
                // only one child.
                //
                // swap and terminate (because this child
                // have no siblings due to heap structure)
                //
                v = a[k1];
                if (v > va)
                {
                    a[j] = v;
                    b[j] = b[k1];
                    j = k1;
                }
                break;
            }
            else
            {

                //
                // two childs
                //
                v1 = a[k1];
                v2 = a[k2];
                if (v1 > v2)
                {
                    if (va < v1)
                    {
                        a[j] = v1;
                        b[j] = b[k1];
                        j = k1;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (va < v2)
                    {
                        a[j] = v2;
                        b[j] = b[k2];
                        j = k2;
                    }
                    else
                    {
                        break;
                    }
                }
                k1 = 2 * j + 1;
                k2 = 2 * j + 2;
            }
        }
        a[j] = va;
        b[j] = vb;
    }


    /*************************************************************************
    Heap operations: pops top element from the heap

    PARAMETERS:
        A       -   heap itself, must be at least array[0..N-1]
        B       -   array of integer tags, which are updated according to
                    permutations in the heap
        N       -   size of the heap, N>=1

    On output top element is moved to A[N-1], B[N-1], heap is reordered, N is
    decreased by 1.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void tagheappopi(ref double[] a,
        ref int[] b,
        ref int n,
        xparams _params)
    {
        double va = 0;
        int vb = 0;

        if (n < 1)
        {
            return;
        }

        //
        // N=1 is a special case
        //
        if (n == 1)
        {
            n = 0;
            return;
        }

        //
        // swap top element and last element,
        // then reorder heap
        //
        va = a[n - 1];
        vb = b[n - 1];
        a[n - 1] = a[0];
        b[n - 1] = b[0];
        n = n - 1;
        tagheapreplacetopi(ref a, ref b, n, va, vb, _params);
    }


    /*************************************************************************
    Search first element less than T in sorted array.

    PARAMETERS:
        A - sorted array by ascending from 0 to N-1
        N - number of elements in array
        T - the desired element

    RESULT:
        The very first element's index, which isn't less than T.
    In the case when there aren't such elements, returns N.
    *************************************************************************/
    public static int lowerbound(double[] a,
        int n,
        double t,
        xparams _params)
    {
        int result = 0;
        int l = 0;
        int half = 0;
        int first = 0;
        int middle = 0;

        l = n;
        first = 0;
        while (l > 0)
        {
            half = l / 2;
            middle = first + half;
            if ((double)(a[middle]) < (double)(t))
            {
                first = middle + 1;
                l = l - half - 1;
            }
            else
            {
                l = half;
            }
        }
        result = first;
        return result;
    }


    /*************************************************************************
    Search first element more than T in sorted array.

    PARAMETERS:
        A - sorted array by ascending from 0 to N-1
        N - number of elements in array
        T - the desired element

        RESULT:
        The very first element's index, which more than T.
    In the case when there aren't such elements, returns N.
    *************************************************************************/
    public static int upperbound(double[] a,
        int n,
        double t,
        xparams _params)
    {
        int result = 0;
        int l = 0;
        int half = 0;
        int first = 0;
        int middle = 0;

        l = n;
        first = 0;
        while (l > 0)
        {
            half = l / 2;
            middle = first + half;
            if ((double)(t) < (double)(a[middle]))
            {
                l = half;
            }
            else
            {
                first = middle + 1;
                l = l - half - 1;
            }
        }
        result = first;
        return result;
    }


    /*************************************************************************
    Internal TagSortFastI: sorts A[I1...I2] (both bounds are included),
    applies same permutations to B.

      -- ALGLIB --
         Copyright 06.09.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void tagsortfastirec(ref double[] a,
        ref int[] b,
        ref double[] bufa,
        ref int[] bufb,
        int i1,
        int i2,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int cntless = 0;
        int cnteq = 0;
        int cntgreater = 0;
        double tmpr = 0;
        int tmpi = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double vp = 0;


        //
        // Fast exit
        //
        if (i2 <= i1)
        {
            return;
        }

        //
        // Non-recursive sort for small arrays
        //
        if (i2 - i1 <= 16)
        {
            for (j = i1 + 1; j <= i2; j++)
            {

                //
                // Search elements [I1..J-1] for place to insert Jth element.
                //
                // This code stops immediately if we can leave A[J] at J-th position
                // (all elements have same value of A[J] larger than any of them)
                //
                tmpr = a[j];
                tmpi = j;
                for (k = j - 1; k >= i1; k--)
                {
                    if (a[k] <= tmpr)
                    {
                        break;
                    }
                    tmpi = k;
                }
                k = tmpi;

                //
                // Insert Jth element into Kth position
                //
                if (k != j)
                {
                    tmpr = a[j];
                    tmpi = b[j];
                    for (i = j - 1; i >= k; i--)
                    {
                        a[i + 1] = a[i];
                        b[i + 1] = b[i];
                    }
                    a[k] = tmpr;
                    b[k] = tmpi;
                }
            }
            return;
        }

        //
        // Quicksort: choose pivot
        // Here we assume that I2-I1>=2
        //
        v0 = a[i1];
        v1 = a[i1 + (i2 - i1) / 2];
        v2 = a[i2];
        if (v0 > v1)
        {
            tmpr = v1;
            v1 = v0;
            v0 = tmpr;
        }
        if (v1 > v2)
        {
            tmpr = v2;
            v2 = v1;
            v1 = tmpr;
        }
        if (v0 > v1)
        {
            tmpr = v1;
            v1 = v0;
            v0 = tmpr;
        }
        vp = v1;

        //
        // now pass through A/B and:
        // * move elements that are LESS than VP to the left of A/B
        // * move elements that are EQUAL to VP to the right of BufA/BufB (in the reverse order)
        // * move elements that are GREATER than VP to the left of BufA/BufB (in the normal order
        // * move elements from the tail of BufA/BufB to the middle of A/B (restoring normal order)
        // * move elements from the left of BufA/BufB to the end of A/B
        //
        cntless = 0;
        cnteq = 0;
        cntgreater = 0;
        for (i = i1; i <= i2; i++)
        {
            v0 = a[i];
            if (v0 < vp)
            {

                //
                // LESS
                //
                k = i1 + cntless;
                if (i != k)
                {
                    a[k] = v0;
                    b[k] = b[i];
                }
                cntless = cntless + 1;
                continue;
            }
            if (v0 == vp)
            {

                //
                // EQUAL
                //
                k = i2 - cnteq;
                bufa[k] = v0;
                bufb[k] = b[i];
                cnteq = cnteq + 1;
                continue;
            }

            //
            // GREATER
            //
            k = i1 + cntgreater;
            bufa[k] = v0;
            bufb[k] = b[i];
            cntgreater = cntgreater + 1;
        }
        for (i = 0; i <= cnteq - 1; i++)
        {
            j = i1 + cntless + cnteq - 1 - i;
            k = i2 + i - (cnteq - 1);
            a[j] = bufa[k];
            b[j] = bufb[k];
        }
        for (i = 0; i <= cntgreater - 1; i++)
        {
            j = i1 + cntless + cnteq + i;
            k = i1 + i;
            a[j] = bufa[k];
            b[j] = bufb[k];
        }

        //
        // Sort left and right parts of the array (ignoring middle part)
        //
        tagsortfastirec(ref a, ref b, ref bufa, ref bufb, i1, i1 + cntless - 1, _params);
        tagsortfastirec(ref a, ref b, ref bufa, ref bufb, i1 + cntless + cnteq, i2, _params);
    }


    /*************************************************************************
    Internal TagSortFastR: sorts A[I1...I2] (both bounds are included),
    applies same permutations to B.

      -- ALGLIB --
         Copyright 06.09.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void tagsortfastrrec(ref double[] a,
        ref double[] b,
        ref double[] bufa,
        ref double[] bufb,
        int i1,
        int i2,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        double tmpr = 0;
        double tmpr2 = 0;
        int tmpi = 0;
        int cntless = 0;
        int cnteq = 0;
        int cntgreater = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double vp = 0;


        //
        // Fast exit
        //
        if (i2 <= i1)
        {
            return;
        }

        //
        // Non-recursive sort for small arrays
        //
        if (i2 - i1 <= 16)
        {
            for (j = i1 + 1; j <= i2; j++)
            {

                //
                // Search elements [I1..J-1] for place to insert Jth element.
                //
                // This code stops immediatly if we can leave A[J] at J-th position
                // (all elements have same value of A[J] larger than any of them)
                //
                tmpr = a[j];
                tmpi = j;
                for (k = j - 1; k >= i1; k--)
                {
                    if (a[k] <= tmpr)
                    {
                        break;
                    }
                    tmpi = k;
                }
                k = tmpi;

                //
                // Insert Jth element into Kth position
                //
                if (k != j)
                {
                    tmpr = a[j];
                    tmpr2 = b[j];
                    for (i = j - 1; i >= k; i--)
                    {
                        a[i + 1] = a[i];
                        b[i + 1] = b[i];
                    }
                    a[k] = tmpr;
                    b[k] = tmpr2;
                }
            }
            return;
        }

        //
        // Quicksort: choose pivot
        // Here we assume that I2-I1>=16
        //
        v0 = a[i1];
        v1 = a[i1 + (i2 - i1) / 2];
        v2 = a[i2];
        if (v0 > v1)
        {
            tmpr = v1;
            v1 = v0;
            v0 = tmpr;
        }
        if (v1 > v2)
        {
            tmpr = v2;
            v2 = v1;
            v1 = tmpr;
        }
        if (v0 > v1)
        {
            tmpr = v1;
            v1 = v0;
            v0 = tmpr;
        }
        vp = v1;

        //
        // now pass through A/B and:
        // * move elements that are LESS than VP to the left of A/B
        // * move elements that are EQUAL to VP to the right of BufA/BufB (in the reverse order)
        // * move elements that are GREATER than VP to the left of BufA/BufB (in the normal order
        // * move elements from the tail of BufA/BufB to the middle of A/B (restoring normal order)
        // * move elements from the left of BufA/BufB to the end of A/B
        //
        cntless = 0;
        cnteq = 0;
        cntgreater = 0;
        for (i = i1; i <= i2; i++)
        {
            v0 = a[i];
            if (v0 < vp)
            {

                //
                // LESS
                //
                k = i1 + cntless;
                if (i != k)
                {
                    a[k] = v0;
                    b[k] = b[i];
                }
                cntless = cntless + 1;
                continue;
            }
            if (v0 == vp)
            {

                //
                // EQUAL
                //
                k = i2 - cnteq;
                bufa[k] = v0;
                bufb[k] = b[i];
                cnteq = cnteq + 1;
                continue;
            }

            //
            // GREATER
            //
            k = i1 + cntgreater;
            bufa[k] = v0;
            bufb[k] = b[i];
            cntgreater = cntgreater + 1;
        }
        for (i = 0; i <= cnteq - 1; i++)
        {
            j = i1 + cntless + cnteq - 1 - i;
            k = i2 + i - (cnteq - 1);
            a[j] = bufa[k];
            b[j] = bufb[k];
        }
        for (i = 0; i <= cntgreater - 1; i++)
        {
            j = i1 + cntless + cnteq + i;
            k = i1 + i;
            a[j] = bufa[k];
            b[j] = bufb[k];
        }

        //
        // Sort left and right parts of the array (ignoring middle part)
        //
        tagsortfastrrec(ref a, ref b, ref bufa, ref bufb, i1, i1 + cntless - 1, _params);
        tagsortfastrrec(ref a, ref b, ref bufa, ref bufb, i1 + cntless + cnteq, i2, _params);
    }


    /*************************************************************************
    Internal TagSortFastI: sorts A[I1...I2] (both bounds are included),
    applies same permutations to B.

      -- ALGLIB --
         Copyright 06.09.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void tagsortfastrec(ref double[] a,
        ref double[] bufa,
        int i1,
        int i2,
        xparams _params)
    {
        int cntless = 0;
        int cnteq = 0;
        int cntgreater = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double tmpr = 0;
        int tmpi = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double vp = 0;


        //
        // Fast exit
        //
        if (i2 <= i1)
        {
            return;
        }

        //
        // Non-recursive sort for small arrays
        //
        if (i2 - i1 <= 16)
        {
            for (j = i1 + 1; j <= i2; j++)
            {

                //
                // Search elements [I1..J-1] for place to insert Jth element.
                //
                // This code stops immediatly if we can leave A[J] at J-th position
                // (all elements have same value of A[J] larger than any of them)
                //
                tmpr = a[j];
                tmpi = j;
                for (k = j - 1; k >= i1; k--)
                {
                    if (a[k] <= tmpr)
                    {
                        break;
                    }
                    tmpi = k;
                }
                k = tmpi;

                //
                // Insert Jth element into Kth position
                //
                if (k != j)
                {
                    tmpr = a[j];
                    for (i = j - 1; i >= k; i--)
                    {
                        a[i + 1] = a[i];
                    }
                    a[k] = tmpr;
                }
            }
            return;
        }

        //
        // Quicksort: choose pivot
        // Here we assume that I2-I1>=16
        //
        v0 = a[i1];
        v1 = a[i1 + (i2 - i1) / 2];
        v2 = a[i2];
        if (v0 > v1)
        {
            tmpr = v1;
            v1 = v0;
            v0 = tmpr;
        }
        if (v1 > v2)
        {
            tmpr = v2;
            v2 = v1;
            v1 = tmpr;
        }
        if (v0 > v1)
        {
            tmpr = v1;
            v1 = v0;
            v0 = tmpr;
        }
        vp = v1;

        //
        // now pass through A/B and:
        // * move elements that are LESS than VP to the left of A/B
        // * move elements that are EQUAL to VP to the right of BufA/BufB (in the reverse order)
        // * move elements that are GREATER than VP to the left of BufA/BufB (in the normal order
        // * move elements from the tail of BufA/BufB to the middle of A/B (restoring normal order)
        // * move elements from the left of BufA/BufB to the end of A/B
        //
        cntless = 0;
        cnteq = 0;
        cntgreater = 0;
        for (i = i1; i <= i2; i++)
        {
            v0 = a[i];
            if (v0 < vp)
            {

                //
                // LESS
                //
                k = i1 + cntless;
                if (i != k)
                {
                    a[k] = v0;
                }
                cntless = cntless + 1;
                continue;
            }
            if (v0 == vp)
            {

                //
                // EQUAL
                //
                k = i2 - cnteq;
                bufa[k] = v0;
                cnteq = cnteq + 1;
                continue;
            }

            //
            // GREATER
            //
            k = i1 + cntgreater;
            bufa[k] = v0;
            cntgreater = cntgreater + 1;
        }
        for (i = 0; i <= cnteq - 1; i++)
        {
            j = i1 + cntless + cnteq - 1 - i;
            k = i2 + i - (cnteq - 1);
            a[j] = bufa[k];
        }
        for (i = 0; i <= cntgreater - 1; i++)
        {
            j = i1 + cntless + cnteq + i;
            k = i1 + i;
            a[j] = bufa[k];
        }

        //
        // Sort left and right parts of the array (ignoring middle part)
        //
        tagsortfastrec(ref a, ref bufa, i1, i1 + cntless - 1, _params);
        tagsortfastrec(ref a, ref bufa, i1 + cntless + cnteq, i2, _params);
    }


}
