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
This structure  is used to store results of the agglomerative hierarchical
clustering (AHC).

Following information is returned:

* TerminationType - completion code:
  * 1   for successful completion of algorithm
  * -5  inappropriate combination of  clustering  algorithm  and  distance
        function was used. As for now, it  is  possible  only when  Ward's
        method is called for dataset with non-Euclidean distance function.
  In case negative completion code is returned,  other  fields  of  report
  structure are invalid and should not be used.

* NPoints contains number of points in the original dataset

* Z contains information about merges performed  (see below).  Z  contains
  indexes from the original (unsorted) dataset and it can be used when you
  need to know what points were merged. However, it is not convenient when
  you want to build a dendrograd (see below).

* if  you  want  to  build  dendrogram, you  can use Z, but it is not good
  option, because Z contains  indexes from  unsorted  dataset.  Dendrogram
  built from such dataset is likely to have intersections. So, you have to
  reorder you points before building dendrogram.
  Permutation which reorders point is returned in P. Another representation
  of  merges,  which  is  more  convenient for dendorgram construction, is
  returned in PM.

* more information on format of Z, P and PM can be found below and in the
  examples from ALGLIB Reference Manual.

FORMAL DESCRIPTION OF FIELDS:
    NPoints         number of points
    Z               array[NPoints-1,2],  contains   indexes   of  clusters
                    linked in pairs to  form  clustering  tree.  I-th  row
                    corresponds to I-th merge:
                    * Z[I,0] - index of the first cluster to merge
                    * Z[I,1] - index of the second cluster to merge
                    * Z[I,0]<Z[I,1]
                    * clusters are  numbered  from 0 to 2*NPoints-2,  with
                      indexes from 0 to NPoints-1 corresponding to  points
                      of the original dataset, and indexes from NPoints to
                      2*NPoints-2  correspond  to  clusters  generated  by
                      subsequent  merges  (I-th  row  of Z creates cluster
                      with index NPoints+I).

                    IMPORTANT: indexes in Z[] are indexes in the ORIGINAL,
                    unsorted dataset. In addition to  Z algorithm  outputs
                    permutation which rearranges points in such  way  that
                    subsequent merges are  performed  on  adjacent  points
                    (such order is needed if you want to build dendrogram).
                    However,  indexes  in  Z  are  related  to   original,
                    unrearranged sequence of points.

    P               array[NPoints], permutation which reorders points  for
                    dendrogram  construction.  P[i] contains  index of the
                    position  where  we  should  move  I-th  point  of the
                    original dataset in order to apply merges PZ/PM.

    PZ              same as Z, but for permutation of points given  by  P.
                    The  only  thing  which  changed  are  indexes  of the
                    original points; indexes of clusters remained same.

    MergeDist       array[NPoints-1], contains distances between  clusters
                    being merged (MergeDist[i] correspond to merge  stored
                    in Z[i,...]):
                    * CLINK, SLINK and  average  linkage algorithms report
                      "raw", unmodified distance metric.
                    * Ward's   method   reports   weighted   intra-cluster
                      variance, which is equal to ||Ca-Cb||^2 * Sa*Sb/(Sa+Sb).
                      Here  A  and  B  are  clusters being merged, Ca is a
                      center of A, Cb is a center of B, Sa is a size of A,
                      Sb is a size of B.

    PM              array[NPoints-1,6], another representation of  merges,
                    which is suited for dendrogram construction. It  deals
                    with rearranged points (permutation P is applied)  and
                    represents merges in a form which different  from  one
                    used by Z.
                    For each I from 0 to NPoints-2, I-th row of PM represents
                    merge performed on two clusters C0 and C1. Here:
                    * C0 contains points with indexes PM[I,0]...PM[I,1]
                    * C1 contains points with indexes PM[I,2]...PM[I,3]
                    * indexes stored in PM are given for dataset sorted
                      according to permutation P
                    * PM[I,1]=PM[I,2]-1 (only adjacent clusters are merged)
                    * PM[I,0]<=PM[I,1], PM[I,2]<=PM[I,3], i.e. both
                      clusters contain at least one point
                    * heights of "subdendrograms" corresponding  to  C0/C1
                      are stored in PM[I,4]  and  PM[I,5].  Subdendrograms
                      corresponding   to   single-point   clusters    have
                      height=0. Dendrogram of the merge result has  height
                      H=max(H0,H1)+1.

NOTE: there is one-to-one correspondence between merges described by Z and
      PM. I-th row of Z describes same merge of clusters as I-th row of PM,
      with "left" cluster from Z corresponding to the "left" one from PM.

  -- ALGLIB --
     Copyright 10.07.2012 by Bochkanov Sergey
*************************************************************************/
public class ahcreport : alglibobject
{
    //
    // Public declarations
    //
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }
    public int npoints { get { return _innerobj.npoints; } set { _innerobj.npoints = value; } }
    public int[] p { get { return _innerobj.p; } set { _innerobj.p = value; } }
    public int[,] z { get { return _innerobj.z; } set { _innerobj.z = value; } }
    public int[,] pz { get { return _innerobj.pz; } set { _innerobj.pz = value; } }
    public int[,] pm { get { return _innerobj.pm; } set { _innerobj.pm = value; } }
    public double[] mergedist { get { return _innerobj.mergedist; } set { _innerobj.mergedist = value; } }

    public ahcreport()
    {
        _innerobj = new clustering.ahcreport();
    }

    public override alglibobject make_copy()
    {
        return new ahcreport((clustering.ahcreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private clustering.ahcreport _innerobj;
    public clustering.ahcreport innerobj { get { return _innerobj; } }
    public ahcreport(clustering.ahcreport obj)
    {
        _innerobj = obj;
    }
}
