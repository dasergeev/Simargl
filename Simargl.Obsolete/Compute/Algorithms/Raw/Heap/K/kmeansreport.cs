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


/*************************************************************************
This  structure   is  used  to  store  results of the  k-means  clustering
algorithm.

Following information is always returned:
* NPoints contains number of points in the original dataset
* TerminationType contains completion code, negative on failure, positive
  on success
* K contains number of clusters

For positive TerminationType we return:
* NFeatures contains number of variables in the original dataset
* C, which contains centers found by algorithm
* CIdx, which maps points of the original dataset to clusters

FORMAL DESCRIPTION OF FIELDS:
    NPoints         number of points, >=0
    NFeatures       number of variables, >=1
    TerminationType completion code:
                    * -5 if  distance  type  is  anything  different  from
                         Euclidean metric
                    * -3 for degenerate dataset: a) less  than  K  distinct
                         points, b) K=0 for non-empty dataset.
                    * +1 for successful completion
    K               number of clusters
    C               array[K,NFeatures], rows of the array store centers
    CIdx            array[NPoints], which contains cluster indexes
    IterationsCount actual number of iterations performed by clusterizer.
                    If algorithm performed more than one random restart,
                    total number of iterations is returned.
    Energy          merit function, "energy", sum  of  squared  deviations
                    from cluster centers

  -- ALGLIB --
     Copyright 27.11.2012 by Bochkanov Sergey
*************************************************************************/
public class kmeansreport : alglibobject
{
    //
    // Public declarations
    //
    public int npoints { get { return _innerobj.npoints; } set { _innerobj.npoints = value; } }
    public int nfeatures { get { return _innerobj.nfeatures; } set { _innerobj.nfeatures = value; } }
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }
    public int iterationscount { get { return _innerobj.iterationscount; } set { _innerobj.iterationscount = value; } }
    public double energy { get { return _innerobj.energy; } set { _innerobj.energy = value; } }
    public int k { get { return _innerobj.k; } set { _innerobj.k = value; } }
    public double[,] c { get { return _innerobj.c; } set { _innerobj.c = value; } }
    public int[] cidx { get { return _innerobj.cidx; } set { _innerobj.cidx = value; } }

    public kmeansreport()
    {
        _innerobj = new clustering.kmeansreport();
    }

    public override alglibobject make_copy()
    {
        return new kmeansreport((clustering.kmeansreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private clustering.kmeansreport _innerobj;
    public clustering.kmeansreport innerobj { get { return _innerobj; } }
    public kmeansreport(clustering.kmeansreport obj)
    {
        _innerobj = obj;
    }
}
