#pragma warning disable CS8618
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

public class apstruct
{
    /*************************************************************************
    This structure is used to store set of N possible integers, in [0,N) range.
    The structure needs O(N) memory, independently from the actual set size.

    This structure allows external code to use following fields:
    * N - maximum set size
    * NStored - number of elements currently in the set
    * Items - first NStored elements are UNSORTED items
    * LocationOf - array[N] that allows quick access by key. If item I is present
      in the set, LocationOf[I]>=0 and stores position in Items[]  of  element
      I, i.e. Items[LocationOf[I]]=I.
      If item I is not present, LocationOf[I]<0.
    *************************************************************************/
    public class niset : apobject
    {
        public int n;
        public int nstored;
        public int[] items;
        public int[] locationof;
        public int iteridx;
        public niset()
        {
            init();
        }
        public override void init()
        {
            items = new int[0];
            locationof = new int[0];
        }
        public override apobject make_copy()
        {
            niset _result = new niset();
            _result.n = n;
            _result.nstored = nstored;
            _result.items = (int[])items.Clone();
            _result.locationof = (int[])locationof.Clone();
            _result.iteridx = iteridx;
            return _result;
        }
    };




    /*************************************************************************
    Initializes n-set by empty structure.

    IMPORTANT: this function need O(N) time for initialization. It is recommended
               to reduce its usage as much as possible, and use nisClear()
               where possible.

    INPUT PARAMETERS
        N           -   possible set size
        
    OUTPUT PARAMETERS
        SA          -   empty N-set

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void nisinitemptyslow(int n,
        niset sa,
        xparams _params)
    {
        sa.n = n;
        sa.nstored = 0;
        ablasf.isetallocv(n, -999999999, ref sa.locationof, _params);
        ablasf.isetallocv(n, -999999999, ref sa.items, _params);
    }


    /*************************************************************************
    Copies n-set to properly initialized target set. The target set has to  be
    properly initialized, and it can be non-empty. If  it  is  non-empty,  its
    contents is quickly erased before copying.

    The cost of this function is O(max(SrcSize,DstSize))

    INPUT PARAMETERS
        SSrc        -   source N-set
        SDst        -   destination N-set (has same size as SSrc)
        
    OUTPUT PARAMETERS
        SDst        -   copy of SSrc

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void niscopy(niset ssrc,
        niset sdst,
        xparams _params)
    {
        int ns = 0;
        int i = 0;
        int k = 0;

        nisclear(sdst, _params);
        ns = ssrc.nstored;
        for (i = 0; i <= ns - 1; i++)
        {
            k = ssrc.items[i];
            sdst.items[i] = k;
            sdst.locationof[k] = i;
        }
        sdst.nstored = ns;
    }


    /*************************************************************************
    Add K-th element to the set. The element may already exist in the set.

    INPUT PARAMETERS
        SA          -   set
        K           -   element to add, 0<=K<N.
        
    OUTPUT PARAMETERS
        SA          -   modified SA

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void nisaddelement(niset sa,
        int k,
        xparams _params)
    {
        int ns = 0;

        if (sa.locationof[k] >= 0)
        {
            return;
        }
        ns = sa.nstored;
        sa.locationof[k] = ns;
        sa.items[ns] = k;
        sa.nstored = ns + 1;
    }


    /*************************************************************************
    Subtracts K-th set from the source structure

    INPUT PARAMETERS
        SA          -   set
        Src, K      -   source kn-set and set index K
        
    OUTPUT PARAMETERS
        SA          -   modified SA

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void nissubtract1(niset sa,
        niset src,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int loc = 0;
        int item = 0;
        int ns = 0;
        int ss = 0;

        ns = sa.nstored;
        ss = src.nstored;
        if (ss < ns)
        {
            for (i = 0; i <= ss - 1; i++)
            {
                j = src.items[i];
                loc = sa.locationof[j];
                if (loc >= 0)
                {
                    item = sa.items[ns - 1];
                    sa.items[loc] = item;
                    sa.locationof[item] = loc;
                    sa.locationof[j] = -1;
                    ns = ns - 1;
                }
            }
        }
        else
        {
            i = 0;
            while (i < ns)
            {
                j = sa.items[i];
                loc = src.locationof[j];
                if (loc >= 0)
                {
                    item = sa.items[ns - 1];
                    sa.items[i] = item;
                    sa.locationof[item] = i;
                    sa.locationof[j] = -1;
                    ns = ns - 1;
                }
                else
                {
                    i = i + 1;
                }
            }
        }
        sa.nstored = ns;
    }


    /*************************************************************************
    Clears set

    INPUT PARAMETERS
        SA          -   set to be cleared
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void nisclear(niset sa,
        xparams _params)
    {
        int i = 0;
        int ns = 0;

        ns = sa.nstored;
        for (i = 0; i <= ns - 1; i++)
        {
            sa.locationof[sa.items[i]] = -1;
        }
        sa.nstored = 0;
    }


    /*************************************************************************
    Counts set elements

    INPUT PARAMETERS
        SA          -   set
        
    RESULT
        number of elements in SA

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static int niscount(niset sa,
        xparams _params)
    {
        int result = 0;

        result = sa.nstored;
        return result;
    }


    /*************************************************************************
    Compare two sets, returns True for equal sets

    INPUT PARAMETERS
        S0          -   set 0
        S1          -   set 1, must have same parameter N as set 0
        
    RESULT
        True, if sets are equal

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static bool nisequal(niset s0,
        niset s1,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int ns0 = 0;
        int ns1 = 0;

        result = false;
        if (s0.n != s1.n)
        {
            return result;
        }
        if (s0.nstored != s1.nstored)
        {
            return result;
        }
        ns0 = s0.nstored;
        ns1 = s1.nstored;
        for (i = 0; i <= ns0 - 1; i++)
        {
            if (s1.locationof[s0.items[i]] < 0)
            {
                return result;
            }
        }
        for (i = 0; i <= ns1 - 1; i++)
        {
            if (s0.locationof[s1.items[i]] < 0)
            {
                return result;
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    Prepares iteration over set

    INPUT PARAMETERS
        SA          -   set
        
    OUTPUT PARAMETERS
        SA          -   SA ready for repeated calls of nisEnumerate()

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void nisstartenumeration(niset sa,
        xparams _params)
    {
        sa.iteridx = 0;
    }


    /*************************************************************************
    Iterates over the set. Subsequent calls return True and set J to  new  set
    item until iteration stops and False is returned.

    INPUT PARAMETERS
        SA          -   n-set
        
    OUTPUT PARAMETERS
        J           -   if:
                        * Result=True - index of element in the set
                        * Result=False - not set


      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static bool nisenumerate(niset sa,
        ref int i,
        xparams _params)
    {
        bool result = new bool();
        int k = 0;

        i = 0;

        k = sa.iteridx;
        if (k >= sa.nstored)
        {
            result = false;
            return result;
        }
        i = sa.items[k];
        sa.iteridx = k + 1;
        result = true;
        return result;
    }


}

