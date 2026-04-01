using System;

#pragma warning disable CS3008
#pragma warning disable CS8618
#pragma warning disable CS8600
#pragma warning disable CS8631
#pragma warning disable CS8602
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

public class clustering
{
    /*************************************************************************
    This structure is used to  store  temporaries  for  KMeansGenerateInternal
    function, so we will be able to  reuse  them  during  multiple  subsequent
    calls.
        
      -- ALGLIB --
         Copyright 24.07.2015 by Bochkanov Sergey
    *************************************************************************/
    public class kmeansbuffers : apobject
    {
        public double[,] ct;
        public double[,] ctbest;
        public int[] xycbest;
        public int[] xycprev;
        public double[] d2;
        public int[] csizes;
        public apserv.apbuffers initbuf;
        public smp.shared_pool updatepool;
        public kmeansbuffers()
        {
            init();
        }
        public override void init()
        {
            ct = new double[0, 0];
            ctbest = new double[0, 0];
            xycbest = new int[0];
            xycprev = new int[0];
            d2 = new double[0];
            csizes = new int[0];
            initbuf = new apserv.apbuffers();
            updatepool = new smp.shared_pool();
        }
        public override apobject make_copy()
        {
            kmeansbuffers _result = new kmeansbuffers();
            _result.ct = (double[,])ct.Clone();
            _result.ctbest = (double[,])ctbest.Clone();
            _result.xycbest = (int[])xycbest.Clone();
            _result.xycprev = (int[])xycprev.Clone();
            _result.d2 = (double[])d2.Clone();
            _result.csizes = (int[])csizes.Clone();
            _result.initbuf = (apserv.apbuffers)initbuf.make_copy();
            _result.updatepool = (smp.shared_pool)updatepool.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    This structure is a clusterization engine.

    You should not try to access its fields directly.
    Use ALGLIB functions in order to work with this object.

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public class clusterizerstate : apobject
    {
        public int npoints;
        public int nfeatures;
        public int disttype;
        public double[,] xy;
        public double[,] d;
        public int ahcalgo;
        public int kmeansrestarts;
        public int kmeansmaxits;
        public int kmeansinitalgo;
        public bool kmeansdbgnoits;
        public int seed;
        public double[,] tmpd;
        public apserv.apbuffers distbuf;
        public kmeansbuffers kmeanstmp;
        public clusterizerstate()
        {
            init();
        }
        public override void init()
        {
            xy = new double[0, 0];
            d = new double[0, 0];
            tmpd = new double[0, 0];
            distbuf = new apserv.apbuffers();
            kmeanstmp = new kmeansbuffers();
        }
        public override apobject make_copy()
        {
            clusterizerstate _result = new clusterizerstate();
            _result.npoints = npoints;
            _result.nfeatures = nfeatures;
            _result.disttype = disttype;
            _result.xy = (double[,])xy.Clone();
            _result.d = (double[,])d.Clone();
            _result.ahcalgo = ahcalgo;
            _result.kmeansrestarts = kmeansrestarts;
            _result.kmeansmaxits = kmeansmaxits;
            _result.kmeansinitalgo = kmeansinitalgo;
            _result.kmeansdbgnoits = kmeansdbgnoits;
            _result.seed = seed;
            _result.tmpd = (double[,])tmpd.Clone();
            _result.distbuf = (apserv.apbuffers)distbuf.make_copy();
            _result.kmeanstmp = (kmeansbuffers)kmeanstmp.make_copy();
            return _result;
        }
    };


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
    public class ahcreport : apobject
    {
        public int terminationtype;
        public int npoints;
        public int[] p;
        public int[,] z;
        public int[,] pz;
        public int[,] pm;
        public double[] mergedist;
        public ahcreport()
        {
            init();
        }
        public override void init()
        {
            p = new int[0];
            z = new int[0, 0];
            pz = new int[0, 0];
            pm = new int[0, 0];
            mergedist = new double[0];
        }
        public override apobject make_copy()
        {
            ahcreport _result = new ahcreport();
            _result.terminationtype = terminationtype;
            _result.npoints = npoints;
            _result.p = (int[])p.Clone();
            _result.z = (int[,])z.Clone();
            _result.pz = (int[,])pz.Clone();
            _result.pm = (int[,])pm.Clone();
            _result.mergedist = (double[])mergedist.Clone();
            return _result;
        }
    };


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
    public class kmeansreport : apobject
    {
        public int npoints;
        public int nfeatures;
        public int terminationtype;
        public int iterationscount;
        public double energy;
        public int k;
        public double[,] c;
        public int[] cidx;
        public kmeansreport()
        {
            init();
        }
        public override void init()
        {
            c = new double[0, 0];
            cidx = new int[0];
        }
        public override apobject make_copy()
        {
            kmeansreport _result = new kmeansreport();
            _result.npoints = npoints;
            _result.nfeatures = nfeatures;
            _result.terminationtype = terminationtype;
            _result.iterationscount = iterationscount;
            _result.energy = energy;
            _result.k = k;
            _result.c = (double[,])c.Clone();
            _result.cidx = (int[])cidx.Clone();
            return _result;
        }
    };




    public const int kmeansblocksize = 32;
    public const int kmeansparalleldim = 8;
    public const int kmeansparallelk = 4;
    public const double complexitymultiplier = 1.0;


    /*************************************************************************
    This function initializes clusterizer object. Newly initialized object  is
    empty, i.e. it does not contain dataset. You should use it as follows:
    1. creation
    2. dataset is added with ClusterizerSetPoints()
    3. additional parameters are set
    3. clusterization is performed with one of the clustering functions

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizercreate(clusterizerstate s,
        xparams _params)
    {
        s.npoints = 0;
        s.nfeatures = 0;
        s.disttype = 2;
        s.ahcalgo = 0;
        s.kmeansrestarts = 1;
        s.kmeansmaxits = 0;
        s.kmeansinitalgo = 0;
        s.kmeansdbgnoits = false;
        s.seed = 1;
        kmeansinitbuf(s.kmeanstmp, _params);
    }


    /*************************************************************************
    This function adds dataset to the clusterizer structure.

    This function overrides all previous calls  of  ClusterizerSetPoints()  or
    ClusterizerSetDistances().

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()
        XY      -   array[NPoints,NFeatures], dataset
        NPoints -   number of points, >=0
        NFeatures-  number of features, >=1
        DistType-   distance function:
                    *  0    Chebyshev distance  (L-inf norm)
                    *  1    city block distance (L1 norm)
                    *  2    Euclidean distance  (L2 norm), non-squared
                    * 10    Pearson correlation:
                            dist(a,b) = 1-corr(a,b)
                    * 11    Absolute Pearson correlation:
                            dist(a,b) = 1-|corr(a,b)|
                    * 12    Uncentered Pearson correlation (cosine of the angle):
                            dist(a,b) = a'*b/(|a|*|b|)
                    * 13    Absolute uncentered Pearson correlation
                            dist(a,b) = |a'*b|/(|a|*|b|)
                    * 20    Spearman rank correlation:
                            dist(a,b) = 1-rankcorr(a,b)
                    * 21    Absolute Spearman rank correlation
                            dist(a,b) = 1-|rankcorr(a,b)|

    NOTE 1: different distance functions have different performance penalty:
            * Euclidean or Pearson correlation distances are the fastest ones
            * Spearman correlation distance function is a bit slower
            * city block and Chebyshev distances are order of magnitude slower
           
            The reason behing difference in performance is that correlation-based
            distance functions are computed using optimized linear algebra kernels,
            while Chebyshev and city block distance functions are computed using
            simple nested loops with two branches at each iteration.
            
    NOTE 2: different clustering algorithms have different limitations:
            * agglomerative hierarchical clustering algorithms may be used with
              any kind of distance metric
            * k-means++ clustering algorithm may be used only  with  Euclidean
              distance function
            Thus, list of specific clustering algorithms you may  use  depends
            on distance function you specify when you set your dataset.
           
      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizersetpoints(clusterizerstate s,
        double[,] xy,
        int npoints,
        int nfeatures,
        int disttype,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;

        ap.assert((((((((disttype == 0 || disttype == 1) || disttype == 2) || disttype == 10) || disttype == 11) || disttype == 12) || disttype == 13) || disttype == 20) || disttype == 21, "ClusterizerSetPoints: incorrect DistType");
        ap.assert(npoints >= 0, "ClusterizerSetPoints: NPoints<0");
        ap.assert(nfeatures >= 1, "ClusterizerSetPoints: NFeatures<1");
        ap.assert(ap.rows(xy) >= npoints, "ClusterizerSetPoints: Rows(XY)<NPoints");
        ap.assert(ap.cols(xy) >= nfeatures, "ClusterizerSetPoints: Cols(XY)<NFeatures");
        ap.assert(apserv.apservisfinitematrix(xy, npoints, nfeatures, _params), "ClusterizerSetPoints: XY contains NAN/INF");
        s.npoints = npoints;
        s.nfeatures = nfeatures;
        s.disttype = disttype;
        apserv.rmatrixsetlengthatleast(ref s.xy, npoints, nfeatures, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= nfeatures - 1; i_++)
            {
                s.xy[i, i_] = xy[i, i_];
            }
        }
    }


    /*************************************************************************
    This function adds dataset given by distance  matrix  to  the  clusterizer
    structure. It is important that dataset is not  given  explicitly  -  only
    distance matrix is given.

    This function overrides all previous calls  of  ClusterizerSetPoints()  or
    ClusterizerSetDistances().

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()
        D       -   array[NPoints,NPoints], distance matrix given by its upper
                    or lower triangle (main diagonal is  ignored  because  its
                    entries are expected to be zero).
        NPoints -   number of points
        IsUpper -   whether upper or lower triangle of D is given.
            
    NOTE 1: different clustering algorithms have different limitations:
            * agglomerative hierarchical clustering algorithms may be used with
              any kind of distance metric, including one  which  is  given  by
              distance matrix
            * k-means++ clustering algorithm may be used only  with  Euclidean
              distance function and explicitly given points - it  can  not  be
              used with dataset given by distance matrix
            Thus, if you call this function, you will be unable to use k-means
            clustering algorithm to process your problem.

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizersetdistances(clusterizerstate s,
        double[,] d,
        int npoints,
        bool isupper,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int j0 = 0;
        int j1 = 0;

        ap.assert(npoints >= 0, "ClusterizerSetDistances: NPoints<0");
        ap.assert(ap.rows(d) >= npoints, "ClusterizerSetDistances: Rows(D)<NPoints");
        ap.assert(ap.cols(d) >= npoints, "ClusterizerSetDistances: Cols(D)<NPoints");
        s.npoints = npoints;
        s.nfeatures = 0;
        s.disttype = -1;
        apserv.rmatrixsetlengthatleast(ref s.d, npoints, npoints, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            if (isupper)
            {
                j0 = i + 1;
                j1 = npoints - 1;
            }
            else
            {
                j0 = 0;
                j1 = i - 1;
            }
            for (j = j0; j <= j1; j++)
            {
                ap.assert(math.isfinite(d[i, j]) && (double)(d[i, j]) >= (double)(0), "ClusterizerSetDistances: D contains infinite, NAN or negative elements");
                s.d[i, j] = d[i, j];
                s.d[j, i] = d[i, j];
            }
            s.d[i, i] = 0;
        }
    }


    /*************************************************************************
    This function sets agglomerative hierarchical clustering algorithm

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()
        Algo    -   algorithm type:
                    * 0     complete linkage (default algorithm)
                    * 1     single linkage
                    * 2     unweighted average linkage
                    * 3     weighted average linkage
                    * 4     Ward's method

    NOTE: Ward's method works correctly only with Euclidean  distance,  that's
          why algorithm will return negative termination  code  (failure)  for
          any other distance type.
          
          It is possible, however,  to  use  this  method  with  user-supplied
          distance matrix. It  is  your  responsibility  to pass one which was
          calculated with Euclidean distance function.

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizersetahcalgo(clusterizerstate s,
        int algo,
        xparams _params)
    {
        ap.assert((((algo == 0 || algo == 1) || algo == 2) || algo == 3) || algo == 4, "ClusterizerSetHCAlgo: incorrect algorithm type");
        s.ahcalgo = algo;
    }


    /*************************************************************************
    This  function  sets k-means properties:  number  of  restarts and maximum
    number of iterations per one run.

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()
        Restarts-   restarts count, >=1.
                    k-means++ algorithm performs several restarts and  chooses
                    best set of centers (one with minimum squared distance).
        MaxIts  -   maximum number of k-means iterations performed during  one
                    run. >=0, zero value means that algorithm performs unlimited
                    number of iterations.

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizersetkmeanslimits(clusterizerstate s,
        int restarts,
        int maxits,
        xparams _params)
    {
        ap.assert(restarts >= 1, "ClusterizerSetKMeansLimits: Restarts<=0");
        ap.assert(maxits >= 0, "ClusterizerSetKMeansLimits: MaxIts<0");
        s.kmeansrestarts = restarts;
        s.kmeansmaxits = maxits;
    }


    /*************************************************************************
    This function sets k-means  initialization  algorithm.  Several  different
    algorithms can be chosen, including k-means++.

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()
        InitAlgo-   initialization algorithm:
                    * 0  automatic selection ( different  versions  of  ALGLIB
                         may select different algorithms)
                    * 1  random initialization
                    * 2  k-means++ initialization  (best  quality  of  initial
                         centers, but long  non-parallelizable  initialization
                         phase with bad cache locality)
                    * 3  "fast-greedy"  algorithm  with  efficient,  easy   to
                         parallelize initialization. Quality of initial centers
                         is  somewhat  worse  than  that  of  k-means++.  This
                         algorithm is a default one in the current version  of
                         ALGLIB.
                    *-1  "debug" algorithm which always selects first  K  rows
                         of dataset; this algorithm is used for debug purposes
                         only. Do not use it in the industrial code!

      -- ALGLIB --
         Copyright 21.01.2015 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizersetkmeansinit(clusterizerstate s,
        int initalgo,
        xparams _params)
    {
        ap.assert(initalgo >= -1 && initalgo <= 3, "ClusterizerSetKMeansInit: InitAlgo is incorrect");
        s.kmeansinitalgo = initalgo;
    }


    /*************************************************************************
    This  function  sets  seed  which  is  used to initialize internal RNG. By
    default, deterministic seed is used - same for each run of clusterizer. If
    you specify non-deterministic  seed  value,  then  some  algorithms  which
    depend on random initialization (in current version: k-means)  may  return
    slightly different results after each run.

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()
        Seed    -   seed:
                    * positive values = use deterministic seed for each run of
                      algorithms which depend on random initialization
                    * zero or negative values = use non-deterministic seed

      -- ALGLIB --
         Copyright 08.06.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizersetseed(clusterizerstate s,
        int seed,
        xparams _params)
    {
        s.seed = seed;
    }


    /*************************************************************************
    This function performs agglomerative hierarchical clustering

    NOTE: Agglomerative  hierarchical  clustering  algorithm  has two  phases:
          distance matrix calculation and clustering  itself. Only first phase
          (distance matrix calculation) is accelerated by SIMD and SMP.  Thus,
          acceleration is significant  only  for  medium  or  high-dimensional
          problems.
          
          Although activating multithreading gives some speedup  over  single-
          threaded execution, you  should  not  expect  nearly-linear  scaling
          with respect to cores count.

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()

    OUTPUT PARAMETERS:
        Rep     -   clustering results; see description of AHCReport
                    structure for more information.

    NOTE 1: hierarchical clustering algorithms require large amounts of memory.
            In particular, this implementation needs  sizeof(double)*NPoints^2
            bytes, which are used to store distance matrix. In  case  we  work
            with user-supplied matrix, this amount is multiplied by 2 (we have
            to store original matrix and to work with its copy).
            
            For example, problem with 10000 points  would require 800M of RAM,
            even when working in a 1-dimensional space.

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizerrunahc(clusterizerstate s,
        ahcreport rep,
        xparams _params)
    {
        int npoints = 0;
        int nfeatures = 0;

        npoints = s.npoints;
        nfeatures = s.nfeatures;

        //
        // Fill Rep.NPoints, quick exit when NPoints<=1
        //
        rep.npoints = npoints;
        if (npoints == 0)
        {
            rep.p = new int[0];
            rep.z = new int[0, 0];
            rep.pz = new int[0, 0];
            rep.pm = new int[0, 0];
            rep.mergedist = new double[0];
            rep.terminationtype = 1;
            return;
        }
        if (npoints == 1)
        {
            rep.p = new int[1];
            rep.z = new int[0, 0];
            rep.pz = new int[0, 0];
            rep.pm = new int[0, 0];
            rep.mergedist = new double[0];
            rep.p[0] = 0;
            rep.terminationtype = 1;
            return;
        }

        //
        // More than one point
        //
        if (s.disttype == -1)
        {

            //
            // Run clusterizer with user-supplied distance matrix
            //
            clusterizerrunahcinternal(s, ref s.d, rep, _params);
            return;
        }
        else
        {

            //
            // Check combination of AHC algo and distance type
            //
            if (s.ahcalgo == 4 && s.disttype != 2)
            {
                rep.terminationtype = -5;
                return;
            }

            //
            // Build distance matrix D.
            //
            clusterizergetdistancesbuf(s.distbuf, s.xy, npoints, nfeatures, s.disttype, ref s.tmpd, _params);

            //
            // Run clusterizer
            //
            clusterizerrunahcinternal(s, ref s.tmpd, rep, _params);
            return;
        }
    }


    /*************************************************************************
    This function performs clustering by k-means++ algorithm.

    You may change algorithm properties by calling:
    * ClusterizerSetKMeansLimits() to change number of restarts or iterations
    * ClusterizerSetKMeansInit() to change initialization algorithm

    By  default,  one  restart  and  unlimited number of iterations are  used.
    Initialization algorithm is chosen automatically.

    NOTE: k-means clustering  algorithm has two  phases:  selection of initial
          centers and clustering  itself.  ALGLIB  parallelizes  both  phases.
          Parallel version is optimized for the following  scenario: medium or
          high-dimensional problem (8 or more dimensions) with large number of
          points and clusters. However, some speed-up  can  be  obtained  even 
          when assumptions above are violated.

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()
        K       -   number of clusters, K>=0.
                    K  can  be  zero only when algorithm is called  for  empty
                    dataset,  in   this   case   completion  code  is  set  to
                    success (+1).
                    If  K=0  and  dataset  size  is  non-zero,  we   can   not
                    meaningfully assign points to some center  (there  are  no
                    centers because K=0) and  return  -3  as  completion  code
                    (failure).

    OUTPUT PARAMETERS:
        Rep     -   clustering results; see description of KMeansReport
                    structure for more information.

    NOTE 1: k-means  clustering  can  be  performed  only  for  datasets  with
            Euclidean  distance  function.  Algorithm  will  return   negative
            completion code in Rep.TerminationType in case dataset  was  added
            to clusterizer with DistType other than Euclidean (or dataset  was
            specified by distance matrix instead of explicitly given points).
            
    NOTE 2: by default, k-means uses non-deterministic seed to initialize  RNG
            which is used to select initial centers. As  result,  each  run of
            algorithm may return different values. If you  need  deterministic
            behavior, use ClusterizerSetSeed() function.

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizerrunkmeans(clusterizerstate s,
        int k,
        kmeansreport rep,
        xparams _params)
    {
        double[,] dummy = new double[0, 0];

        ap.assert(k >= 0, "ClusterizerRunKMeans: K<0");

        //
        // Incorrect distance type
        //
        if (s.disttype != 2)
        {
            rep.npoints = s.npoints;
            rep.terminationtype = -5;
            rep.k = k;
            rep.iterationscount = 0;
            rep.energy = 0.0;
            return;
        }

        //
        // K>NPoints or (K=0 and NPoints>0)
        //
        if (k > s.npoints || (k == 0 && s.npoints > 0))
        {
            rep.npoints = s.npoints;
            rep.terminationtype = -3;
            rep.k = k;
            rep.iterationscount = 0;
            rep.energy = 0.0;
            return;
        }

        //
        // No points
        //
        if (s.npoints == 0)
        {
            rep.npoints = 0;
            rep.terminationtype = 1;
            rep.k = k;
            rep.iterationscount = 0;
            rep.energy = 0.0;
            return;
        }

        //
        // Normal case:
        // 1<=K<=NPoints, Euclidean distance 
        //
        rep.npoints = s.npoints;
        rep.nfeatures = s.nfeatures;
        rep.k = k;
        rep.npoints = s.npoints;
        rep.nfeatures = s.nfeatures;
        kmeansgenerateinternal(s.xy, s.npoints, s.nfeatures, k, s.kmeansinitalgo, s.seed, s.kmeansmaxits, s.kmeansrestarts, s.kmeansdbgnoits, ref rep.terminationtype, ref rep.iterationscount, ref dummy, false, ref rep.c, true, ref rep.cidx, ref rep.energy, s.kmeanstmp, _params);
    }


    /*************************************************************************
    This function returns distance matrix for dataset

    INPUT PARAMETERS:
        XY      -   array[NPoints,NFeatures], dataset
        NPoints -   number of points, >=0
        NFeatures-  number of features, >=1
        DistType-   distance function:
                    *  0    Chebyshev distance  (L-inf norm)
                    *  1    city block distance (L1 norm)
                    *  2    Euclidean distance  (L2 norm, non-squared)
                    * 10    Pearson correlation:
                            dist(a,b) = 1-corr(a,b)
                    * 11    Absolute Pearson correlation:
                            dist(a,b) = 1-|corr(a,b)|
                    * 12    Uncentered Pearson correlation (cosine of the angle):
                            dist(a,b) = a'*b/(|a|*|b|)
                    * 13    Absolute uncentered Pearson correlation
                            dist(a,b) = |a'*b|/(|a|*|b|)
                    * 20    Spearman rank correlation:
                            dist(a,b) = 1-rankcorr(a,b)
                    * 21    Absolute Spearman rank correlation
                            dist(a,b) = 1-|rankcorr(a,b)|

    OUTPUT PARAMETERS:
        D       -   array[NPoints,NPoints], distance matrix
                    (full matrix is returned, with lower and upper triangles)

    NOTE:  different distance functions have different performance penalty:
           * Euclidean or Pearson correlation distances are the fastest ones
           * Spearman correlation distance function is a bit slower
           * city block and Chebyshev distances are order of magnitude slower
           
           The reason behing difference in performance is that correlation-based
           distance functions are computed using optimized linear algebra kernels,
           while Chebyshev and city block distance functions are computed using
           simple nested loops with two branches at each iteration.

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizergetdistances(double[,] xy,
        int npoints,
        int nfeatures,
        int disttype,
        ref double[,] d,
        xparams _params)
    {
        apserv.apbuffers buf = new apserv.apbuffers();

        d = new double[0, 0];

        ap.assert(nfeatures >= 1, "ClusterizerGetDistances: NFeatures<1");
        ap.assert(npoints >= 0, "ClusterizerGetDistances: NPoints<1");
        ap.assert((((((((disttype == 0 || disttype == 1) || disttype == 2) || disttype == 10) || disttype == 11) || disttype == 12) || disttype == 13) || disttype == 20) || disttype == 21, "ClusterizerGetDistances: incorrect DistType");
        ap.assert(ap.rows(xy) >= npoints, "ClusterizerGetDistances: Rows(XY)<NPoints");
        ap.assert(ap.cols(xy) >= nfeatures, "ClusterizerGetDistances: Cols(XY)<NFeatures");
        ap.assert(apserv.apservisfinitematrix(xy, npoints, nfeatures, _params), "ClusterizerGetDistances: XY contains NAN/INF");
        clusterizergetdistancesbuf(buf, xy, npoints, nfeatures, disttype, ref d, _params);
    }


    /*************************************************************************
    Buffered version  of  ClusterizerGetDistances()  which  reuses  previously
    allocated space.

      -- ALGLIB --
         Copyright 29.05.2015 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizergetdistancesbuf(apserv.apbuffers buf,
        double[,] xy,
        int npoints,
        int nfeatures,
        int disttype,
        ref double[,] d,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        double vv = 0;
        double vr = 0;
        int i_ = 0;

        ap.assert(nfeatures >= 1, "ClusterizerGetDistancesBuf: NFeatures<1");
        ap.assert(npoints >= 0, "ClusterizerGetDistancesBuf: NPoints<1");
        ap.assert((((((((disttype == 0 || disttype == 1) || disttype == 2) || disttype == 10) || disttype == 11) || disttype == 12) || disttype == 13) || disttype == 20) || disttype == 21, "ClusterizerGetDistancesBuf: incorrect DistType");
        ap.assert(ap.rows(xy) >= npoints, "ClusterizerGetDistancesBuf: Rows(XY)<NPoints");
        ap.assert(ap.cols(xy) >= nfeatures, "ClusterizerGetDistancesBuf: Cols(XY)<NFeatures");
        ap.assert(apserv.apservisfinitematrix(xy, npoints, nfeatures, _params), "ClusterizerGetDistancesBuf: XY contains NAN/INF");

        //
        // Quick exit
        //
        if (npoints == 0)
        {
            return;
        }
        if (npoints == 1)
        {
            apserv.rmatrixsetlengthatleast(ref d, 1, 1, _params);
            d[0, 0] = 0;
            return;
        }

        //
        // Build distance matrix D.
        //
        if (disttype == 0 || disttype == 1)
        {

            //
            // Chebyshev or city-block distances:
            // * recursively calculate upper triangle (with main diagonal)
            // * copy it to the bottom part of the matrix
            //
            apserv.rmatrixsetlengthatleast(ref d, npoints, npoints, _params);
            evaluatedistancematrixrec(xy, nfeatures, disttype, d, 0, npoints, 0, npoints, _params);
            ablas.rmatrixenforcesymmetricity(d, npoints, true, _params);
            return;
        }
        if (disttype == 2)
        {

            //
            // Euclidean distance
            //
            // NOTE: parallelization is done within RMatrixSYRK
            //
            apserv.rmatrixsetlengthatleast(ref d, npoints, npoints, _params);
            apserv.rmatrixsetlengthatleast(ref buf.rm0, npoints, nfeatures, _params);
            apserv.rvectorsetlengthatleast(ref buf.ra1, nfeatures, _params);
            apserv.rvectorsetlengthatleast(ref buf.ra0, npoints, _params);
            for (j = 0; j <= nfeatures - 1; j++)
            {
                buf.ra1[j] = 0.0;
            }
            v = (double)1 / (double)npoints;
            for (i = 0; i <= npoints - 1; i++)
            {
                for (i_ = 0; i_ <= nfeatures - 1; i_++)
                {
                    buf.ra1[i_] = buf.ra1[i_] + v * xy[i, i_];
                }
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                for (i_ = 0; i_ <= nfeatures - 1; i_++)
                {
                    buf.rm0[i, i_] = xy[i, i_];
                }
                for (i_ = 0; i_ <= nfeatures - 1; i_++)
                {
                    buf.rm0[i, i_] = buf.rm0[i, i_] - buf.ra1[i_];
                }
            }
            ablas.rmatrixsyrk(npoints, nfeatures, 1.0, buf.rm0, 0, 0, 0, 0.0, d, 0, 0, true, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                buf.ra0[i] = d[i, i];
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                d[i, i] = 0.0;
                for (j = i + 1; j <= npoints - 1; j++)
                {
                    v = Math.Sqrt(Math.Max(buf.ra0[i] + buf.ra0[j] - 2 * d[i, j], 0.0));
                    d[i, j] = v;
                }
            }
            ablas.rmatrixenforcesymmetricity(d, npoints, true, _params);
            return;
        }
        if (disttype == 10 || disttype == 11)
        {

            //
            // Absolute/nonabsolute Pearson correlation distance
            //
            // NOTE: parallelization is done within PearsonCorrM, which calls RMatrixSYRK internally
            //
            apserv.rmatrixsetlengthatleast(ref d, npoints, npoints, _params);
            apserv.rvectorsetlengthatleast(ref buf.ra0, npoints, _params);
            apserv.rmatrixsetlengthatleast(ref buf.rm0, npoints, nfeatures, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                v = 0.0;
                for (j = 0; j <= nfeatures - 1; j++)
                {
                    v = v + xy[i, j];
                }
                v = v / nfeatures;
                for (j = 0; j <= nfeatures - 1; j++)
                {
                    buf.rm0[i, j] = xy[i, j] - v;
                }
            }
            ablas.rmatrixsyrk(npoints, nfeatures, 1.0, buf.rm0, 0, 0, 0, 0.0, d, 0, 0, true, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                buf.ra0[i] = d[i, i];
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                d[i, i] = 0.0;
                for (j = i + 1; j <= npoints - 1; j++)
                {
                    v = d[i, j] / Math.Sqrt(buf.ra0[i] * buf.ra0[j]);
                    if (disttype == 10)
                    {
                        v = 1 - v;
                    }
                    else
                    {
                        v = 1 - Math.Abs(v);
                    }
                    v = Math.Max(v, 0.0);
                    d[i, j] = v;
                }
            }
            ablas.rmatrixenforcesymmetricity(d, npoints, true, _params);
            return;
        }
        if (disttype == 12 || disttype == 13)
        {

            //
            // Absolute/nonabsolute uncentered Pearson correlation distance
            //
            // NOTE: parallelization is done within RMatrixSYRK
            //
            apserv.rmatrixsetlengthatleast(ref d, npoints, npoints, _params);
            apserv.rvectorsetlengthatleast(ref buf.ra0, npoints, _params);
            ablas.rmatrixsyrk(npoints, nfeatures, 1.0, xy, 0, 0, 0, 0.0, d, 0, 0, true, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                buf.ra0[i] = d[i, i];
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                d[i, i] = 0.0;
                for (j = i + 1; j <= npoints - 1; j++)
                {
                    v = d[i, j] / Math.Sqrt(buf.ra0[i] * buf.ra0[j]);
                    if (disttype == 13)
                    {
                        v = Math.Abs(v);
                    }
                    v = Math.Min(v, 1.0);
                    d[i, j] = 1 - v;
                }
            }
            ablas.rmatrixenforcesymmetricity(d, npoints, true, _params);
            return;
        }
        if (disttype == 20 || disttype == 21)
        {

            //
            // Spearman rank correlation
            //
            // NOTE: parallelization of correlation matrix is done within
            //       PearsonCorrM, which calls RMatrixSYRK internally
            //
            apserv.rmatrixsetlengthatleast(ref d, npoints, npoints, _params);
            apserv.rvectorsetlengthatleast(ref buf.ra0, npoints, _params);
            apserv.rmatrixsetlengthatleast(ref buf.rm0, npoints, nfeatures, _params);
            ablas.rmatrixcopy(npoints, nfeatures, xy, 0, 0, buf.rm0, 0, 0, _params);
            basestat.rankdatacentered(buf.rm0, npoints, nfeatures, _params);
            ablas.rmatrixsyrk(npoints, nfeatures, 1.0, buf.rm0, 0, 0, 0, 0.0, d, 0, 0, true, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                if ((double)(d[i, i]) > (double)(0))
                {
                    buf.ra0[i] = 1 / Math.Sqrt(d[i, i]);
                }
                else
                {
                    buf.ra0[i] = 0.0;
                }
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                v = buf.ra0[i];
                d[i, i] = 0.0;
                for (j = i + 1; j <= npoints - 1; j++)
                {
                    vv = d[i, j] * v * buf.ra0[j];
                    if (disttype == 20)
                    {
                        vr = 1 - vv;
                    }
                    else
                    {
                        vr = 1 - Math.Abs(vv);
                    }
                    if ((double)(vr) < (double)(0))
                    {
                        vr = 0.0;
                    }
                    d[i, j] = vr;
                }
            }
            ablas.rmatrixenforcesymmetricity(d, npoints, true, _params);
            return;
        }
        ap.assert(false);
    }


    /*************************************************************************
    This function takes as input clusterization report Rep,  desired  clusters
    count K, and builds top K clusters from hierarchical clusterization  tree.
    It returns assignment of points to clusters (array of cluster indexes).

    INPUT PARAMETERS:
        Rep     -   report from ClusterizerRunAHC() performed on XY
        K       -   desired number of clusters, 1<=K<=NPoints.
                    K can be zero only when NPoints=0.

    OUTPUT PARAMETERS:
        CIdx    -   array[NPoints], I-th element contains cluster index  (from
                    0 to K-1) for I-th point of the dataset.
        CZ      -   array[K]. This array allows  to  convert  cluster  indexes
                    returned by this function to indexes used by  Rep.Z.  J-th
                    cluster returned by this function corresponds to  CZ[J]-th
                    cluster stored in Rep.Z/PZ/PM.
                    It is guaranteed that CZ[I]<CZ[I+1].

    NOTE: K clusters built by this subroutine are assumed to have no hierarchy.
          Although  they  were  obtained  by  manipulation with top K nodes of
          dendrogram  (i.e.  hierarchical  decomposition  of  dataset),   this
          function does not return information about hierarchy.  Each  of  the
          clusters stand on its own.
          
    NOTE: Cluster indexes returned by this function  does  not  correspond  to
          indexes returned in Rep.Z/PZ/PM. Either you work  with  hierarchical
          representation of the dataset (dendrogram), or you work with  "flat"
          representation returned by this function.  Each  of  representations
          has its own clusters indexing system (former uses [0, 2*NPoints-2]),
          while latter uses [0..K-1]), although  it  is  possible  to  perform
          conversion from one system to another by means of CZ array, returned
          by this function, which allows you to convert indexes stored in CIdx
          to the numeration system used by Rep.Z.
          
    NOTE: this subroutine is optimized for moderate values of K. Say, for  K=5
          it will perform many times faster than  for  K=100.  Its  worst-case
          performance is O(N*K), although in average case  it  perform  better
          (up to O(N*log(K))).

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizergetkclusters(ahcreport rep,
        int k,
        ref int[] cidx,
        ref int[] cz,
        xparams _params)
    {
        int i = 0;
        int mergeidx = 0;
        int i0 = 0;
        int i1 = 0;
        int t = 0;
        bool[] presentclusters = new bool[0];
        int[] clusterindexes = new int[0];
        int[] clustersizes = new int[0];
        int[] tmpidx = new int[0];
        int npoints = 0;

        cidx = new int[0];
        cz = new int[0];

        npoints = rep.npoints;
        ap.assert(npoints >= 0, "ClusterizerGetKClusters: internal error in Rep integrity");
        ap.assert(k >= 0, "ClusterizerGetKClusters: K<=0");
        ap.assert(k <= npoints, "ClusterizerGetKClusters: K>NPoints");
        ap.assert(k > 0 || npoints == 0, "ClusterizerGetKClusters: K<=0");
        ap.assert(npoints == rep.npoints, "ClusterizerGetKClusters: NPoints<>Rep.NPoints");

        //
        // Quick exit
        //
        if (npoints == 0)
        {
            return;
        }
        if (npoints == 1)
        {
            cz = new int[1];
            cidx = new int[1];
            cz[0] = 0;
            cidx[0] = 0;
            return;
        }

        //
        // Replay merges, from top to bottom,
        // keep track of clusters being present at the moment
        //
        presentclusters = new bool[2 * npoints - 1];
        tmpidx = new int[npoints];
        for (i = 0; i <= 2 * npoints - 3; i++)
        {
            presentclusters[i] = false;
        }
        presentclusters[2 * npoints - 2] = true;
        for (i = 0; i <= npoints - 1; i++)
        {
            tmpidx[i] = 2 * npoints - 2;
        }
        for (mergeidx = npoints - 2; mergeidx >= npoints - k; mergeidx--)
        {

            //
            // Update information about clusters being present at the moment
            //
            presentclusters[npoints + mergeidx] = false;
            presentclusters[rep.z[mergeidx, 0]] = true;
            presentclusters[rep.z[mergeidx, 1]] = true;

            //
            // Update TmpIdx according to the current state of the dataset
            //
            // NOTE: TmpIdx contains cluster indexes from [0..2*NPoints-2];
            //       we will convert them to [0..K-1] later.
            //
            i0 = rep.pm[mergeidx, 0];
            i1 = rep.pm[mergeidx, 1];
            t = rep.z[mergeidx, 0];
            for (i = i0; i <= i1; i++)
            {
                tmpidx[i] = t;
            }
            i0 = rep.pm[mergeidx, 2];
            i1 = rep.pm[mergeidx, 3];
            t = rep.z[mergeidx, 1];
            for (i = i0; i <= i1; i++)
            {
                tmpidx[i] = t;
            }
        }

        //
        // Fill CZ - array which allows us to convert cluster indexes
        // from one system to another.
        //
        cz = new int[k];
        clusterindexes = new int[2 * npoints - 1];
        t = 0;
        for (i = 0; i <= 2 * npoints - 2; i++)
        {
            if (presentclusters[i])
            {
                cz[t] = i;
                clusterindexes[i] = t;
                t = t + 1;
            }
        }
        ap.assert(t == k, "ClusterizerGetKClusters: internal error");

        //
        // Convert indexes stored in CIdx
        //
        cidx = new int[npoints];
        for (i = 0; i <= npoints - 1; i++)
        {
            cidx[i] = clusterindexes[tmpidx[rep.p[i]]];
        }
    }


    /*************************************************************************
    This  function  accepts  AHC  report  Rep,  desired  minimum  intercluster
    distance and returns top clusters from  hierarchical  clusterization  tree
    which are separated by distance R or HIGHER.

    It returns assignment of points to clusters (array of cluster indexes).

    There is one more function with similar name - ClusterizerSeparatedByCorr,
    which returns clusters with intercluster correlation equal to R  or  LOWER
    (note: higher for distance, lower for correlation).

    INPUT PARAMETERS:
        Rep     -   report from ClusterizerRunAHC() performed on XY
        R       -   desired minimum intercluster distance, R>=0

    OUTPUT PARAMETERS:
        K       -   number of clusters, 1<=K<=NPoints
        CIdx    -   array[NPoints], I-th element contains cluster index  (from
                    0 to K-1) for I-th point of the dataset.
        CZ      -   array[K]. This array allows  to  convert  cluster  indexes
                    returned by this function to indexes used by  Rep.Z.  J-th
                    cluster returned by this function corresponds to  CZ[J]-th
                    cluster stored in Rep.Z/PZ/PM.
                    It is guaranteed that CZ[I]<CZ[I+1].

    NOTE: K clusters built by this subroutine are assumed to have no hierarchy.
          Although  they  were  obtained  by  manipulation with top K nodes of
          dendrogram  (i.e.  hierarchical  decomposition  of  dataset),   this
          function does not return information about hierarchy.  Each  of  the
          clusters stand on its own.
          
    NOTE: Cluster indexes returned by this function  does  not  correspond  to
          indexes returned in Rep.Z/PZ/PM. Either you work  with  hierarchical
          representation of the dataset (dendrogram), or you work with  "flat"
          representation returned by this function.  Each  of  representations
          has its own clusters indexing system (former uses [0, 2*NPoints-2]),
          while latter uses [0..K-1]), although  it  is  possible  to  perform
          conversion from one system to another by means of CZ array, returned
          by this function, which allows you to convert indexes stored in CIdx
          to the numeration system used by Rep.Z.
          
    NOTE: this subroutine is optimized for moderate values of K. Say, for  K=5
          it will perform many times faster than  for  K=100.  Its  worst-case
          performance is O(N*K), although in average case  it  perform  better
          (up to O(N*log(K))).

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizerseparatedbydist(ahcreport rep,
        double r,
        ref int k,
        ref int[] cidx,
        ref int[] cz,
        xparams _params)
    {
        k = 0;
        cidx = new int[0];
        cz = new int[0];

        ap.assert(math.isfinite(r) && (double)(r) >= (double)(0), "ClusterizerSeparatedByDist: R is infinite or less than 0");
        k = 1;
        while (k < rep.npoints && (double)(rep.mergedist[rep.npoints - 1 - k]) >= (double)(r))
        {
            k = k + 1;
        }
        clusterizergetkclusters(rep, k, ref cidx, ref cz, _params);
    }


    /*************************************************************************
    This  function  accepts  AHC  report  Rep,  desired  maximum  intercluster
    correlation and returns top clusters from hierarchical clusterization tree
    which are separated by correlation R or LOWER.

    It returns assignment of points to clusters (array of cluster indexes).

    There is one more function with similar name - ClusterizerSeparatedByDist,
    which returns clusters with intercluster distance equal  to  R  or  HIGHER
    (note: higher for distance, lower for correlation).

    INPUT PARAMETERS:
        Rep     -   report from ClusterizerRunAHC() performed on XY
        R       -   desired maximum intercluster correlation, -1<=R<=+1

    OUTPUT PARAMETERS:
        K       -   number of clusters, 1<=K<=NPoints
        CIdx    -   array[NPoints], I-th element contains cluster index  (from
                    0 to K-1) for I-th point of the dataset.
        CZ      -   array[K]. This array allows  to  convert  cluster  indexes
                    returned by this function to indexes used by  Rep.Z.  J-th
                    cluster returned by this function corresponds to  CZ[J]-th
                    cluster stored in Rep.Z/PZ/PM.
                    It is guaranteed that CZ[I]<CZ[I+1].

    NOTE: K clusters built by this subroutine are assumed to have no hierarchy.
          Although  they  were  obtained  by  manipulation with top K nodes of
          dendrogram  (i.e.  hierarchical  decomposition  of  dataset),   this
          function does not return information about hierarchy.  Each  of  the
          clusters stand on its own.
          
    NOTE: Cluster indexes returned by this function  does  not  correspond  to
          indexes returned in Rep.Z/PZ/PM. Either you work  with  hierarchical
          representation of the dataset (dendrogram), or you work with  "flat"
          representation returned by this function.  Each  of  representations
          has its own clusters indexing system (former uses [0, 2*NPoints-2]),
          while latter uses [0..K-1]), although  it  is  possible  to  perform
          conversion from one system to another by means of CZ array, returned
          by this function, which allows you to convert indexes stored in CIdx
          to the numeration system used by Rep.Z.
          
    NOTE: this subroutine is optimized for moderate values of K. Say, for  K=5
          it will perform many times faster than  for  K=100.  Its  worst-case
          performance is O(N*K), although in average case  it  perform  better
          (up to O(N*log(K))).

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void clusterizerseparatedbycorr(ahcreport rep,
        double r,
        ref int k,
        ref int[] cidx,
        ref int[] cz,
        xparams _params)
    {
        k = 0;
        cidx = new int[0];
        cz = new int[0];

        ap.assert((math.isfinite(r) && (double)(r) >= (double)(-1)) && (double)(r) <= (double)(1), "ClusterizerSeparatedByCorr: R is infinite or less than 0");
        k = 1;
        while (k < rep.npoints && (double)(rep.mergedist[rep.npoints - 1 - k]) >= (double)(1 - r))
        {
            k = k + 1;
        }
        clusterizergetkclusters(rep, k, ref cidx, ref cz, _params);
    }


    /*************************************************************************
    K-means++ initialization

    INPUT PARAMETERS:
        Buf         -   special reusable structure which stores previously allocated
                        memory, intended to avoid memory fragmentation when solving
                        multiple subsequent problems. Must be initialized prior to
                        usage.

    OUTPUT PARAMETERS:
        Buf         -   initialized structure

      -- ALGLIB --
         Copyright 24.07.2015 by Bochkanov Sergey
    *************************************************************************/
    public static void kmeansinitbuf(kmeansbuffers buf,
        xparams _params)
    {
        apserv.apbuffers updateseed = new apserv.apbuffers();

        smp.ae_shared_pool_set_seed(buf.updatepool, updateseed);
    }


    /*************************************************************************
    K-means++ clusterization

    INPUT PARAMETERS:
        XY          -   dataset, array [0..NPoints-1,0..NVars-1].
        NPoints     -   dataset size, NPoints>=K
        NVars       -   number of variables, NVars>=1
        K           -   desired number of clusters, K>=1
        InitAlgo    -   initialization algorithm:
                        * 0 - automatic selection of best algorithm
                        * 1 - random selection of centers
                        * 2 - k-means++
                        * 3 - fast-greedy init
                        *-1 - first K rows of dataset are used
                              (special debug algorithm)
        Seed        -   seed value for internal RNG:
                        * positive value is used to initialize RNG in order to
                          induce deterministic behavior of algorithm
                        * zero or negative value means  that random  seed   is
                          generated
        MaxIts      -   iterations limit or zero for no limit
        Restarts    -   number of restarts, Restarts>=1
        KMeansDbgNoIts- debug flag; if set, Lloyd's iteration is not performed,
                        only initialization phase.
        Buf         -   special reusable structure which stores previously allocated
                        memory, intended to avoid memory fragmentation when solving
                        multiple subsequent problems:
                        * MUST BE INITIALIZED WITH KMeansInitBuffers() CALL BEFORE
                          FIRST PASS TO THIS FUNCTION!
                        * subsequent passes must be made without re-initialization

    OUTPUT PARAMETERS:
        Info        -   return code:
                        * -3, if task is degenerate (number of distinct points is
                              less than K)
                        * -1, if incorrect NPoints/NFeatures/K/Restarts was passed
                        *  1, if subroutine finished successfully
        IterationsCount- actual number of iterations performed by clusterizer
        CCol        -   array[0..NVars-1,0..K-1].matrix whose columns store
                        cluster's centers
        NeedCCol    -   True in case caller requires to store result in CCol
        CRow        -   array[0..K-1,0..NVars-1], same as CCol, but centers are
                        stored in rows
        NeedCRow    -   True in case caller requires to store result in CCol
        XYC         -   array[NPoints], which contains cluster indexes
        Energy      -   merit function of clusterization

      -- ALGLIB --
         Copyright 21.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void kmeansgenerateinternal(double[,] xy,
        int npoints,
        int nvars,
        int k,
        int initalgo,
        int seed,
        int maxits,
        int restarts,
        bool kmeansdbgnoits,
        ref int info,
        ref int iterationscount,
        ref double[,] ccol,
        bool needccol,
        ref double[,] crow,
        bool needcrow,
        ref int[] xyc,
        ref double energy,
        kmeansbuffers buf,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int i1 = 0;
        double e = 0;
        double eprev = 0;
        double v = 0;
        double vv = 0;
        bool waschanges = new bool();
        bool zerosizeclusters = new bool();
        int pass = 0;
        int itcnt = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        int i_ = 0;

        info = 0;
        iterationscount = 0;
        ccol = new double[0, 0];
        crow = new double[0, 0];
        xyc = new int[0];
        energy = 0;


        //
        // Test parameters
        //
        if (((npoints < k || nvars < 1) || k < 1) || restarts < 1)
        {
            info = -1;
            iterationscount = 0;
            return;
        }

        //
        // TODO: special case K=1
        // TODO: special case K=NPoints
        //
        info = 1;
        iterationscount = 0;

        //
        // Multiple passes of k-means++ algorithm
        //
        if (seed <= 0)
        {
            hqrnd.hqrndrandomize(rs, _params);
        }
        else
        {
            hqrnd.hqrndseed(325355, seed, rs, _params);
        }
        xyc = new int[npoints];
        apserv.rmatrixsetlengthatleast(ref buf.ct, k, nvars, _params);
        apserv.rmatrixsetlengthatleast(ref buf.ctbest, k, nvars, _params);
        apserv.ivectorsetlengthatleast(ref buf.xycprev, npoints, _params);
        apserv.ivectorsetlengthatleast(ref buf.xycbest, npoints, _params);
        apserv.rvectorsetlengthatleast(ref buf.d2, npoints, _params);
        apserv.ivectorsetlengthatleast(ref buf.csizes, k, _params);
        energy = math.maxrealnumber;
        for (pass = 1; pass <= restarts; pass++)
        {

            //
            // Select initial centers.
            //
            // Note that for performance reasons centers are stored in ROWS of CT, not
            // in columns. We'll transpose CT in the end and store it in the C.
            //
            // Also note that SelectInitialCenters() may return degenerate set of centers
            // (some of them have no corresponding points in dataset, some are non-distinct).
            // Algorithm below is robust enough to deal with such set.
            //
            selectinitialcenters(xy, npoints, nvars, initalgo, rs, k, ref buf.ct, buf.initbuf, buf.updatepool, _params);

            //
            // Lloyd's iteration
            //
            if (!kmeansdbgnoits)
            {

                //
                // Perform iteration as usual, in normal mode
                //
                for (i = 0; i <= npoints - 1; i++)
                {
                    xyc[i] = -1;
                }
                eprev = math.maxrealnumber;
                e = math.maxrealnumber;
                itcnt = 0;
                while (maxits == 0 || itcnt < maxits)
                {

                    //
                    // Update iteration counter
                    //
                    itcnt = itcnt + 1;
                    apserv.inc(ref iterationscount, _params);

                    //
                    // Call KMeansUpdateDistances(), fill XYC with center numbers,
                    // D2 with center distances.
                    //
                    for (i = 0; i <= npoints - 1; i++)
                    {
                        buf.xycprev[i] = xyc[i];
                    }
                    kmeansupdatedistances(xy, 0, npoints, nvars, buf.ct, 0, k, xyc, buf.d2, buf.updatepool, _params);
                    waschanges = false;
                    for (i = 0; i <= npoints - 1; i++)
                    {
                        waschanges = waschanges || xyc[i] != buf.xycprev[i];
                    }

                    //
                    // Update centers
                    //
                    for (j = 0; j <= k - 1; j++)
                    {
                        buf.csizes[j] = 0;
                    }
                    for (i = 0; i <= k - 1; i++)
                    {
                        for (j = 0; j <= nvars - 1; j++)
                        {
                            buf.ct[i, j] = 0;
                        }
                    }
                    for (i = 0; i <= npoints - 1; i++)
                    {
                        buf.csizes[xyc[i]] = buf.csizes[xyc[i]] + 1;
                        for (i_ = 0; i_ <= nvars - 1; i_++)
                        {
                            buf.ct[xyc[i], i_] = buf.ct[xyc[i], i_] + xy[i, i_];
                        }
                    }
                    zerosizeclusters = false;
                    for (j = 0; j <= k - 1; j++)
                    {
                        if (buf.csizes[j] != 0)
                        {
                            v = (double)1 / (double)buf.csizes[j];
                            for (i_ = 0; i_ <= nvars - 1; i_++)
                            {
                                buf.ct[j, i_] = v * buf.ct[j, i_];
                            }
                        }
                        zerosizeclusters = zerosizeclusters || buf.csizes[j] == 0;
                    }
                    if (zerosizeclusters)
                    {

                        //
                        // Some clusters have zero size - rare, but possible.
                        // We'll choose new centers for such clusters using k-means++ rule
                        // and restart algorithm, decrementing iteration counter
                        // in order to allow one more iteration (this one was useless
                        // and should not be counted).
                        //
                        if (!fixcenters(xy, npoints, nvars, buf.ct, k, buf.initbuf, buf.updatepool, _params))
                        {
                            info = -3;
                            return;
                        }
                        itcnt = itcnt - 1;
                        continue;
                    }

                    //
                    // Stop if one of two conditions is met:
                    // 1. nothing has changed during iteration
                    // 2. energy function increased after recalculation on new centers
                    //
                    e = 0;
                    for (i = 0; i <= npoints - 1; i++)
                    {
                        v = 0.0;
                        i1 = xyc[i];
                        for (j = 0; j <= nvars - 1; j++)
                        {
                            vv = xy[i, j] - buf.ct[i1, j];
                            v = v + vv * vv;
                        }
                        e = e + v;
                    }
                    if (!waschanges || (double)(e) >= (double)(eprev))
                    {
                        break;
                    }

                    //
                    // Update EPrev
                    //
                    eprev = e;
                }
            }
            else
            {

                //
                // Debug mode: no Lloyd's iteration.
                // We just calculate potential E.
                //
                kmeansupdatedistances(xy, 0, npoints, nvars, buf.ct, 0, k, xyc, buf.d2, buf.updatepool, _params);
                e = 0;
                for (i = 0; i <= npoints - 1; i++)
                {
                    e = e + buf.d2[i];
                }
            }

            //
            // Compare E with best centers found so far
            //
            if ((double)(e) < (double)(energy))
            {

                //
                // store partition.
                //
                energy = e;
                RawBlas.copymatrix(buf.ct, 0, k - 1, 0, nvars - 1, ref buf.ctbest, 0, k - 1, 0, nvars - 1, _params);
                for (i = 0; i <= npoints - 1; i++)
                {
                    buf.xycbest[i] = xyc[i];
                }
            }
        }

        //
        // Copy and transpose
        //
        if (needccol)
        {
            ccol = new double[nvars, k];
            RawBlas.copyandtranspose(buf.ctbest, 0, k - 1, 0, nvars - 1, ref ccol, 0, nvars - 1, 0, k - 1, _params);
        }
        if (needcrow)
        {
            crow = new double[k, nvars];
            ablas.rmatrixcopy(k, nvars, buf.ctbest, 0, 0, crow, 0, 0, _params);
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            xyc[i] = buf.xycbest[i];
        }
    }


    /*************************************************************************
    This procedure recalculates distances from points to centers  and  assigns
    each point to closest center.

    INPUT PARAMETERS:
        XY          -   dataset, array [0..NPoints-1,0..NVars-1].
        Idx0,Idx1   -   define range of dataset [Idx0,Idx1) to process;
                        right boundary is not included.
        NVars       -   number of variables, NVars>=1
        CT          -   matrix of centers, centers are stored in rows
        CIdx0,CIdx1 -   define range of centers [CIdx0,CIdx1) to process;
                        right boundary is not included.
        XYC         -   preallocated output buffer, 
        XYDist2     -   preallocated output buffer
        Tmp         -   temporary buffer, automatically reallocated if needed
        BufferPool  -   shared pool seeded with instance of APBuffers structure
                        (seed instance can be unitialized). It is recommended
                        to use this pool only with KMeansUpdateDistances()
                        function.

    OUTPUT PARAMETERS:
        XYC         -   new assignment of points to centers are stored
                        in [Idx0,Idx1)
        XYDist2     -   squared distances from points to their centers are
                        stored in [Idx0,Idx1)

      -- ALGLIB --
         Copyright 21.01.2015 by Bochkanov Sergey
    *************************************************************************/
    public static void kmeansupdatedistances(double[,] xy,
        int idx0,
        int idx1,
        int nvars,
        double[,] ct,
        int cidx0,
        int cidx1,
        int[] xyc,
        double[] xydist2,
        smp.shared_pool bufferpool,
        xparams _params)
    {
        int i = 0;
        int i0 = 0;
        int i1 = 0;
        int j = 0;
        int cclosest = 0;
        double dclosest = 0;
        double vv = 0;
        apserv.apbuffers buf = null;
        double rcomplexity = 0;
        int task0 = 0;
        int task1 = 0;
        int pblkcnt = 0;
        int cblkcnt = 0;
        int vblkcnt = 0;
        int pblk = 0;
        int cblk = 0;
        int vblk = 0;
        int p0 = 0;
        int p1 = 0;
        int c0 = 0;
        int c1 = 0;
        int v0 = 0;
        int v1 = 0;
        double v00 = 0;
        double v01 = 0;
        double v10 = 0;
        double v11 = 0;
        double vp0 = 0;
        double vp1 = 0;
        double vc0 = 0;
        double vc1 = 0;
        int pcnt = 0;
        int pcntpadded = 0;
        int ccnt = 0;
        int ccntpadded = 0;
        int offs0 = 0;
        int offs00 = 0;
        int offs01 = 0;
        int offs10 = 0;
        int offs11 = 0;
        int vcnt = 0;
        int stride = 0;


        //
        // Quick exit for special cases
        //
        if (idx1 <= idx0)
        {
            return;
        }
        if (cidx1 <= cidx0)
        {
            return;
        }
        if (nvars <= 0)
        {
            return;
        }

        //
        // Try to recursively divide/process dataset
        //
        // NOTE: real arithmetics is used to avoid integer overflow on large problem sizes
        //
        rcomplexity = 2 * apserv.rmul3(idx1 - idx0, cidx1 - cidx0, nvars, _params);
        if ((double)(rcomplexity) >= (double)(apserv.smpactivationlevel(_params)) && idx1 - idx0 >= 2 * kmeansblocksize)
        {
            if (_trypexec_kmeansupdatedistances(xy, idx0, idx1, nvars, ct, cidx0, cidx1, xyc, xydist2, bufferpool, _params))
            {
                return;
            }
        }
        if ((((double)(rcomplexity) >= (double)(apserv.spawnlevel(_params)) && idx1 - idx0 >= 2 * kmeansblocksize) && nvars >= kmeansparalleldim) && cidx1 - cidx0 >= kmeansparallelk)
        {
            apserv.splitlength(idx1 - idx0, kmeansblocksize, ref task0, ref task1, _params);
            kmeansupdatedistances(xy, idx0, idx0 + task0, nvars, ct, cidx0, cidx1, xyc, xydist2, bufferpool, _params);
            kmeansupdatedistances(xy, idx0 + task0, idx1, nvars, ct, cidx0, cidx1, xyc, xydist2, bufferpool, _params);
            return;
        }

        //
        // Dataset chunk is selected.
        // 
        // Process it with blocked algorithm:
        // * iterate over points, process them in KMeansBlockSize-ed chunks
        // * for each chunk of dataset, iterate over centers, process them in KMeansBlockSize-ed chunks
        // * for each chunk of dataset/centerset, iterate over variables, process them in KMeansBlockSize-ed chunks
        //
        ap.assert(kmeansblocksize % 2 == 0, "KMeansUpdateDistances: internal error");
        smp.ae_shared_pool_retrieve(bufferpool, ref buf);
        apserv.rvectorsetlengthatleast(ref buf.ra0, kmeansblocksize * kmeansblocksize, _params);
        apserv.rvectorsetlengthatleast(ref buf.ra1, kmeansblocksize * kmeansblocksize, _params);
        apserv.rvectorsetlengthatleast(ref buf.ra2, kmeansblocksize * kmeansblocksize, _params);
        apserv.rvectorsetlengthatleast(ref buf.ra3, kmeansblocksize, _params);
        apserv.ivectorsetlengthatleast(ref buf.ia3, kmeansblocksize, _params);
        pblkcnt = apserv.chunkscount(idx1 - idx0, kmeansblocksize, _params);
        cblkcnt = apserv.chunkscount(cidx1 - cidx0, kmeansblocksize, _params);
        vblkcnt = apserv.chunkscount(nvars, kmeansblocksize, _params);
        for (pblk = 0; pblk <= pblkcnt - 1; pblk++)
        {

            //
            // Process PBlk-th chunk of dataset.
            //
            p0 = idx0 + pblk * kmeansblocksize;
            p1 = Math.Min(p0 + kmeansblocksize, idx1);

            //
            // Prepare RA3[]/IA3[] for storage of best distances and best cluster numbers.
            //
            for (i = 0; i <= kmeansblocksize - 1; i++)
            {
                buf.ra3[i] = math.maxrealnumber;
                buf.ia3[i] = -1;
            }

            //
            // Iterare over chunks of centerset.
            //
            for (cblk = 0; cblk <= cblkcnt - 1; cblk++)
            {

                //
                // Process CBlk-th chunk of centerset
                //
                c0 = cidx0 + cblk * kmeansblocksize;
                c1 = Math.Min(c0 + kmeansblocksize, cidx1);

                //
                // At this point we have to calculate a set of pairwise distances
                // between points [P0,P1) and centers [C0,C1) and select best center
                // for each point. It can also be done with blocked algorithm
                // (blocking for variables).
                //
                // Following arrays are used:
                // * RA0[] - matrix of distances, padded by zeros for even size,
                //           rows are stored with stride KMeansBlockSize.
                // * RA1[] - matrix of points (variables corresponding to current
                //           block are extracted), padded by zeros for even size,
                //           rows are stored with stride KMeansBlockSize.
                // * RA2[] - matrix of centers (variables corresponding to current
                //           block are extracted), padded by zeros for even size,
                //           rows are stored with stride KMeansBlockSize.
                //
                //
                pcnt = p1 - p0;
                pcntpadded = pcnt + pcnt % 2;
                ccnt = c1 - c0;
                ccntpadded = ccnt + ccnt % 2;
                stride = kmeansblocksize;
                ap.assert(pcntpadded <= kmeansblocksize, "KMeansUpdateDistances: integrity error");
                ap.assert(ccntpadded <= kmeansblocksize, "KMeansUpdateDistances: integrity error");
                for (i = 0; i <= pcntpadded - 1; i++)
                {
                    for (j = 0; j <= ccntpadded - 1; j++)
                    {
                        buf.ra0[i * stride + j] = 0.0;
                    }
                }
                for (vblk = 0; vblk <= vblkcnt - 1; vblk++)
                {

                    //
                    // Fetch VBlk-th block of variables to arrays RA1 (points) and RA2 (centers).
                    // Pad points and centers with zeros.
                    //
                    v0 = vblk * kmeansblocksize;
                    v1 = Math.Min(v0 + kmeansblocksize, nvars);
                    vcnt = v1 - v0;
                    for (i = 0; i <= pcnt - 1; i++)
                    {
                        for (j = 0; j <= vcnt - 1; j++)
                        {
                            buf.ra1[i * stride + j] = xy[p0 + i, v0 + j];
                        }
                    }
                    for (i = pcnt; i <= pcntpadded - 1; i++)
                    {
                        for (j = 0; j <= vcnt - 1; j++)
                        {
                            buf.ra1[i * stride + j] = 0.0;
                        }
                    }
                    for (i = 0; i <= ccnt - 1; i++)
                    {
                        for (j = 0; j <= vcnt - 1; j++)
                        {
                            buf.ra2[i * stride + j] = ct[c0 + i, v0 + j];
                        }
                    }
                    for (i = ccnt; i <= ccntpadded - 1; i++)
                    {
                        for (j = 0; j <= vcnt - 1; j++)
                        {
                            buf.ra2[i * stride + j] = 0.0;
                        }
                    }

                    //
                    // Update distance matrix with sums-of-squared-differences of RA1 and RA2
                    //
                    i0 = 0;
                    while (i0 < pcntpadded)
                    {
                        i1 = 0;
                        while (i1 < ccntpadded)
                        {
                            offs0 = i0 * stride + i1;
                            v00 = buf.ra0[offs0];
                            v01 = buf.ra0[offs0 + 1];
                            v10 = buf.ra0[offs0 + stride];
                            v11 = buf.ra0[offs0 + stride + 1];
                            offs00 = i0 * stride;
                            offs01 = offs00 + stride;
                            offs10 = i1 * stride;
                            offs11 = offs10 + stride;
                            for (j = 0; j <= vcnt - 1; j++)
                            {
                                vp0 = buf.ra1[offs00 + j];
                                vp1 = buf.ra1[offs01 + j];
                                vc0 = buf.ra2[offs10 + j];
                                vc1 = buf.ra2[offs11 + j];
                                vv = vp0 - vc0;
                                v00 = v00 + vv * vv;
                                vv = vp0 - vc1;
                                v01 = v01 + vv * vv;
                                vv = vp1 - vc0;
                                v10 = v10 + vv * vv;
                                vv = vp1 - vc1;
                                v11 = v11 + vv * vv;
                            }
                            offs0 = i0 * stride + i1;
                            buf.ra0[offs0] = v00;
                            buf.ra0[offs0 + 1] = v01;
                            buf.ra0[offs0 + stride] = v10;
                            buf.ra0[offs0 + stride + 1] = v11;
                            i1 = i1 + 2;
                        }
                        i0 = i0 + 2;
                    }
                }
                for (i = 0; i <= pcnt - 1; i++)
                {
                    cclosest = buf.ia3[i];
                    dclosest = buf.ra3[i];
                    for (j = 0; j <= ccnt - 1; j++)
                    {
                        if ((double)(buf.ra0[i * stride + j]) < (double)(dclosest))
                        {
                            dclosest = buf.ra0[i * stride + j];
                            cclosest = c0 + j;
                        }
                    }
                    buf.ia3[i] = cclosest;
                    buf.ra3[i] = dclosest;
                }
            }

            //
            // Store best centers to XYC[]
            //
            for (i = p0; i <= p1 - 1; i++)
            {
                xyc[i] = buf.ia3[i - p0];
                xydist2[i] = buf.ra3[i - p0];
            }
        }
        smp.ae_shared_pool_recycle(bufferpool, ref buf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_kmeansupdatedistances(double[,] xy,
        int idx0,
        int idx1,
        int nvars,
        double[,] ct,
        int cidx0,
        int cidx1,
        int[] xyc,
        double[] xydist2,
        smp.shared_pool bufferpool, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function selects initial centers according to specified initialization
    algorithm.

    IMPORTANT: this function provides no  guarantees  regarding  selection  of
               DIFFERENT  centers.  Centers  returned  by  this  function  may
               include duplicates (say, when random sampling is  used). It  is
               also possible that some centers are empty.
               Algorithm which uses this function must be able to deal with it.
               Say, you may want to use FixCenters() in order to fix empty centers.

    INPUT PARAMETERS:
        XY          -   dataset, array [0..NPoints-1,0..NVars-1].
        NPoints     -   points count
        NVars       -   number of variables, NVars>=1
        InitAlgo    -   initialization algorithm:
                        * 0 - automatic selection of best algorithm
                        * 1 - random selection
                        * 2 - k-means++
                        * 3 - fast-greedy init
                        *-1 - first K rows of dataset are used (debug algorithm)
        RS          -   RNG used to select centers
        K           -   number of centers, K>=1
        CT          -   possibly preallocated output buffer, resized if needed
        InitBuf     -   internal buffer, possibly unitialized instance of
                        APBuffers. It is recommended to use this instance only
                        with SelectInitialCenters() and FixCenters() functions,
                        because these functions may allocate really large storage.
        UpdatePool  -   shared pool seeded with instance of APBuffers structure
                        (seed instance can be unitialized). Used internally with
                        KMeansUpdateDistances() function. It is recommended
                        to use this pool ONLY with KMeansUpdateDistances()
                        function.

    OUTPUT PARAMETERS:
        CT          -   set of K clusters, one per row
        
    RESULT:
        True on success, False on failure (impossible to create K independent clusters)

      -- ALGLIB --
         Copyright 21.01.2015 by Bochkanov Sergey
    *************************************************************************/
    private static void selectinitialcenters(double[,] xy,
        int npoints,
        int nvars,
        int initalgo,
        hqrnd.hqrndstate rs,
        int k,
        ref double[,] ct,
        apserv.apbuffers initbuf,
        smp.shared_pool updatepool,
        xparams _params)
    {
        int cidx = 0;
        int i = 0;
        int j = 0;
        double v = 0;
        double vv = 0;
        double s = 0;
        int lastnz = 0;
        int ptidx = 0;
        int samplesize = 0;
        int samplescntnew = 0;
        int samplescntall = 0;
        double samplescale = 0;
        int i_ = 0;


        //
        // Check parameters
        //
        ap.assert(npoints > 0, "SelectInitialCenters: internal error");
        ap.assert(nvars > 0, "SelectInitialCenters: internal error");
        ap.assert(k > 0, "SelectInitialCenters: internal error");
        if (initalgo == 0)
        {
            initalgo = 3;
        }
        apserv.rmatrixsetlengthatleast(ref ct, k, nvars, _params);

        //
        // Random initialization
        //
        if (initalgo == -1)
        {
            for (i = 0; i <= k - 1; i++)
            {
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    ct[i, i_] = xy[i % npoints, i_];
                }
            }
            return;
        }

        //
        // Random initialization
        //
        if (initalgo == 1)
        {
            for (i = 0; i <= k - 1; i++)
            {
                j = hqrnd.hqrnduniformi(rs, npoints, _params);
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    ct[i, i_] = xy[j, i_];
                }
            }
            return;
        }

        //
        // k-means++ initialization
        //
        if (initalgo == 2)
        {

            //
            // Prepare distances array.
            // Select initial center at random.
            //
            apserv.rvectorsetlengthatleast(ref initbuf.ra0, npoints, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                initbuf.ra0[i] = math.maxrealnumber;
            }
            ptidx = hqrnd.hqrnduniformi(rs, npoints, _params);
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                ct[0, i_] = xy[ptidx, i_];
            }

            //
            // For each newly added center repeat:
            // * reevaluate distances from points to best centers
            // * sample points with probability dependent on distance
            // * add new center
            //
            for (cidx = 0; cidx <= k - 2; cidx++)
            {

                //
                // Reevaluate distances
                //
                s = 0.0;
                for (i = 0; i <= npoints - 1; i++)
                {
                    v = 0.0;
                    for (j = 0; j <= nvars - 1; j++)
                    {
                        vv = xy[i, j] - ct[cidx, j];
                        v = v + vv * vv;
                    }
                    if ((double)(v) < (double)(initbuf.ra0[i]))
                    {
                        initbuf.ra0[i] = v;
                    }
                    s = s + initbuf.ra0[i];
                }

                //
                // If all distances are zero, it means that we can not find enough
                // distinct points. In this case we just select non-distinct center
                // at random and continue iterations. This issue will be handled
                // later in the FixCenters() function.
                //
                if ((double)(s) == (double)(0.0))
                {
                    ptidx = hqrnd.hqrnduniformi(rs, npoints, _params);
                    for (i_ = 0; i_ <= nvars - 1; i_++)
                    {
                        ct[cidx + 1, i_] = xy[ptidx, i_];
                    }
                    continue;
                }

                //
                // Select point as center using its distance.
                // We also handle situation when because of rounding errors
                // no point was selected - in this case, last non-zero one
                // will be used.
                //
                v = hqrnd.hqrnduniformr(rs, _params);
                vv = 0.0;
                lastnz = -1;
                ptidx = -1;
                for (i = 0; i <= npoints - 1; i++)
                {
                    if ((double)(initbuf.ra0[i]) == (double)(0.0))
                    {
                        continue;
                    }
                    lastnz = i;
                    vv = vv + initbuf.ra0[i];
                    if ((double)(v) <= (double)(vv / s))
                    {
                        ptidx = i;
                        break;
                    }
                }
                ap.assert(lastnz >= 0, "SelectInitialCenters: integrity error");
                if (ptidx < 0)
                {
                    ptidx = lastnz;
                }
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    ct[cidx + 1, i_] = xy[ptidx, i_];
                }
            }
            return;
        }

        //
        // "Fast-greedy" algorithm based on "Scalable k-means++".
        //
        // We perform several rounds, within each round we sample about 0.5*K points
        // (not exactly 0.5*K) until we have 2*K points sampled. Before each round
        // we calculate distances from dataset points to closest points sampled so far.
        // We sample dataset points independently using distance xtimes 0.5*K divided by total
        // as probability (similar to k-means++, but each point is sampled independently;
        // after each round we have roughtly 0.5*K points added to sample).
        //
        // After sampling is done, we run "greedy" version of k-means++ on this subsample
        // which selects most distant point on every round.
        //
        if (initalgo == 3)
        {

            //
            // Prepare arrays.
            // Select initial center at random, add it to "new" part of sample,
            // which is stored at the beginning of the array
            //
            samplesize = 2 * k;
            samplescale = 0.5 * k;
            apserv.rmatrixsetlengthatleast(ref initbuf.rm0, samplesize, nvars, _params);
            ptidx = hqrnd.hqrnduniformi(rs, npoints, _params);
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                initbuf.rm0[0, i_] = xy[ptidx, i_];
            }
            samplescntnew = 1;
            samplescntall = 1;
            apserv.rvectorsetlengthatleast(ref initbuf.ra0, npoints, _params);
            apserv.rvectorsetlengthatleast(ref initbuf.ra1, npoints, _params);
            apserv.ivectorsetlengthatleast(ref initbuf.ia1, npoints, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                initbuf.ra0[i] = math.maxrealnumber;
            }

            //
            // Repeat until samples count is 2*K
            //
            while (samplescntall < samplesize)
            {

                //
                // Evaluate distances from points to NEW centers, store to RA1.
                // Reset counter of "new" centers.
                //
                kmeansupdatedistances(xy, 0, npoints, nvars, initbuf.rm0, samplescntall - samplescntnew, samplescntall, initbuf.ia1, initbuf.ra1, updatepool, _params);
                samplescntnew = 0;

                //
                // Merge new distances with old ones.
                // Calculate sum of distances, if sum is exactly zero - fill sample
                // by randomly selected points and terminate.
                //
                s = 0.0;
                for (i = 0; i <= npoints - 1; i++)
                {
                    initbuf.ra0[i] = Math.Min(initbuf.ra0[i], initbuf.ra1[i]);
                    s = s + initbuf.ra0[i];
                }
                if ((double)(s) == (double)(0.0))
                {
                    while (samplescntall < samplesize)
                    {
                        ptidx = hqrnd.hqrnduniformi(rs, npoints, _params);
                        for (i_ = 0; i_ <= nvars - 1; i_++)
                        {
                            initbuf.rm0[samplescntall, i_] = xy[ptidx, i_];
                        }
                        apserv.inc(ref samplescntall, _params);
                        apserv.inc(ref samplescntnew, _params);
                    }
                    break;
                }

                //
                // Sample points independently.
                //
                for (i = 0; i <= npoints - 1; i++)
                {
                    if (samplescntall == samplesize)
                    {
                        break;
                    }
                    if ((double)(initbuf.ra0[i]) == (double)(0.0))
                    {
                        continue;
                    }
                    if ((double)(hqrnd.hqrnduniformr(rs, _params)) <= (double)(samplescale * initbuf.ra0[i] / s))
                    {
                        for (i_ = 0; i_ <= nvars - 1; i_++)
                        {
                            initbuf.rm0[samplescntall, i_] = xy[i, i_];
                        }
                        apserv.inc(ref samplescntall, _params);
                        apserv.inc(ref samplescntnew, _params);
                    }
                }
            }

            //
            // Run greedy version of k-means on sampled points
            //
            apserv.rvectorsetlengthatleast(ref initbuf.ra0, samplescntall, _params);
            for (i = 0; i <= samplescntall - 1; i++)
            {
                initbuf.ra0[i] = math.maxrealnumber;
            }
            ptidx = hqrnd.hqrnduniformi(rs, samplescntall, _params);
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                ct[0, i_] = initbuf.rm0[ptidx, i_];
            }
            for (cidx = 0; cidx <= k - 2; cidx++)
            {

                //
                // Reevaluate distances
                //
                for (i = 0; i <= samplescntall - 1; i++)
                {
                    v = 0.0;
                    for (j = 0; j <= nvars - 1; j++)
                    {
                        vv = initbuf.rm0[i, j] - ct[cidx, j];
                        v = v + vv * vv;
                    }
                    if ((double)(v) < (double)(initbuf.ra0[i]))
                    {
                        initbuf.ra0[i] = v;
                    }
                }

                //
                // Select point as center in greedy manner - most distant
                // point is selected.
                //
                ptidx = 0;
                for (i = 0; i <= samplescntall - 1; i++)
                {
                    if ((double)(initbuf.ra0[i]) > (double)(initbuf.ra0[ptidx]))
                    {
                        ptidx = i;
                    }
                }
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    ct[cidx + 1, i_] = initbuf.rm0[ptidx, i_];
                }
            }
            return;
        }

        //
        // Internal error
        //
        ap.assert(false, "SelectInitialCenters: internal error");
    }


    /*************************************************************************
    This function "fixes" centers, i.e. replaces ones which have  no  neighbor
    points by new centers which have at least one neighbor. If it is impossible
    to fix centers (not enough distinct points in the dataset), this function
    returns False.

    INPUT PARAMETERS:
        XY          -   dataset, array [0..NPoints-1,0..NVars-1].
        NPoints     -   points count, >=1
        NVars       -   number of variables, NVars>=1
        CT          -   centers
        K           -   number of centers, K>=1
        InitBuf     -   internal buffer, possibly unitialized instance of
                        APBuffers. It is recommended to use this instance only
                        with SelectInitialCenters() and FixCenters() functions,
                        because these functions may allocate really large storage.
        UpdatePool  -   shared pool seeded with instance of APBuffers structure
                        (seed instance can be unitialized). Used internally with
                        KMeansUpdateDistances() function. It is recommended
                        to use this pool ONLY with KMeansUpdateDistances()
                        function.

    OUTPUT PARAMETERS:
        CT          -   set of K centers, one per row
        
    RESULT:
        True on success, False on failure (impossible to create K independent clusters)

      -- ALGLIB --
         Copyright 21.01.2015 by Bochkanov Sergey
    *************************************************************************/
    private static bool fixcenters(double[,] xy,
        int npoints,
        int nvars,
        double[,] ct,
        int k,
        apserv.apbuffers initbuf,
        smp.shared_pool updatepool,
        xparams _params)
    {
        bool result = new bool();
        int fixiteration = 0;
        int centertofix = 0;
        int i = 0;
        int j = 0;
        int pdistant = 0;
        double ddistant = 0;
        double v = 0;
        int i_ = 0;

        ap.assert(npoints >= 1, "FixCenters: internal error");
        ap.assert(nvars >= 1, "FixCenters: internal error");
        ap.assert(k >= 1, "FixCenters: internal error");

        //
        // Calculate distances from points to best centers (RA0)
        // and best center indexes (IA0)
        //
        apserv.ivectorsetlengthatleast(ref initbuf.ia0, npoints, _params);
        apserv.rvectorsetlengthatleast(ref initbuf.ra0, npoints, _params);
        kmeansupdatedistances(xy, 0, npoints, nvars, ct, 0, k, initbuf.ia0, initbuf.ra0, updatepool, _params);

        //
        // Repeat loop:
        // * find first center which has no corresponding point
        // * set it to the most distant (from the rest of the centerset) point
        // * recalculate distances, update IA0/RA0
        // * repeat
        //
        // Loop is repeated for at most 2*K iterations. It is stopped once we have
        // no "empty" clusters.
        //
        apserv.bvectorsetlengthatleast(ref initbuf.ba0, k, _params);
        for (fixiteration = 0; fixiteration <= 2 * k; fixiteration++)
        {

            //
            // Select center to fix (one which is not mentioned in IA0),
            // terminate if there is no such center.
            // BA0[] stores True for centers which have at least one point.
            //
            for (i = 0; i <= k - 1; i++)
            {
                initbuf.ba0[i] = false;
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                initbuf.ba0[initbuf.ia0[i]] = true;
            }
            centertofix = -1;
            for (i = 0; i <= k - 1; i++)
            {
                if (!initbuf.ba0[i])
                {
                    centertofix = i;
                    break;
                }
            }
            if (centertofix < 0)
            {
                result = true;
                return result;
            }

            //
            // Replace center to fix by the most distant point.
            // Update IA0/RA0
            //
            pdistant = 0;
            ddistant = initbuf.ra0[pdistant];
            for (i = 0; i <= npoints - 1; i++)
            {
                if ((double)(initbuf.ra0[i]) > (double)(ddistant))
                {
                    ddistant = initbuf.ra0[i];
                    pdistant = i;
                }
            }
            if ((double)(ddistant) == (double)(0.0))
            {
                break;
            }
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                ct[centertofix, i_] = xy[pdistant, i_];
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                v = 0.0;
                for (j = 0; j <= nvars - 1; j++)
                {
                    v = v + math.sqr(xy[i, j] - ct[centertofix, j]);
                }
                if ((double)(v) < (double)(initbuf.ra0[i]))
                {
                    initbuf.ra0[i] = v;
                    initbuf.ia0[i] = centertofix;
                }
            }
        }
        result = false;
        return result;
    }


    /*************************************************************************
    This  function  performs  agglomerative  hierarchical  clustering    using
    precomputed  distance  matrix.  Internal  function,  should  not be called
    directly.

    INPUT PARAMETERS:
        S       -   clusterizer state, initialized by ClusterizerCreate()
        D       -   distance matrix, array[S.NFeatures,S.NFeatures]
                    Contents of the matrix is destroyed during
                    algorithm operation.

    OUTPUT PARAMETERS:
        Rep     -   clustering results; see description of AHCReport
                    structure for more information.

      -- ALGLIB --
         Copyright 10.07.2012 by Bochkanov Sergey
    *************************************************************************/
    private static void clusterizerrunahcinternal(clusterizerstate s,
        ref double[,] d,
        ahcreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        int mergeidx = 0;
        int c0 = 0;
        int c1 = 0;
        int s0 = 0;
        int s1 = 0;
        int ar = 0;
        int br = 0;
        int npoints = 0;
        int[] cidx = new int[0];
        int[] csizes = new int[0];
        int[] nnidx = new int[0];
        int[,] cinfo = new int[0, 0];
        int n0 = 0;
        int n1 = 0;
        int ni = 0;
        double d01 = 0;

        npoints = s.npoints;

        //
        // Fill Rep.NPoints, quick exit when NPoints<=1
        //
        rep.npoints = npoints;
        if (npoints == 0)
        {
            rep.p = new int[0];
            rep.z = new int[0, 0];
            rep.pz = new int[0, 0];
            rep.pm = new int[0, 0];
            rep.mergedist = new double[0];
            rep.terminationtype = 1;
            return;
        }
        if (npoints == 1)
        {
            rep.p = new int[1];
            rep.z = new int[0, 0];
            rep.pz = new int[0, 0];
            rep.pm = new int[0, 0];
            rep.mergedist = new double[0];
            rep.p[0] = 0;
            rep.terminationtype = 1;
            return;
        }
        rep.z = new int[npoints - 1, 2];
        rep.mergedist = new double[npoints - 1];
        rep.terminationtype = 1;

        //
        // Build list of nearest neighbors
        //
        nnidx = new int[npoints];
        for (i = 0; i <= npoints - 1; i++)
        {

            //
            // Calculate index of the nearest neighbor
            //
            k = -1;
            v = math.maxrealnumber;
            for (j = 0; j <= npoints - 1; j++)
            {
                if (j != i && (double)(d[i, j]) < (double)(v))
                {
                    k = j;
                    v = d[i, j];
                }
            }
            ap.assert((double)(v) < (double)(math.maxrealnumber), "ClusterizerRunAHC: internal error");
            nnidx[i] = k;
        }

        //
        // For AHCAlgo=4 (Ward's method) replace distances by their squares times 0.5
        //
        if (s.ahcalgo == 4)
        {
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= npoints - 1; j++)
                {
                    d[i, j] = 0.5 * d[i, j] * d[i, j];
                }
            }
        }

        //
        // Distance matrix is built, perform merges.
        //
        // NOTE 1: CIdx is array[NPoints] which maps rows/columns of the
        //         distance matrix D to indexes of clusters. Values of CIdx
        //         from [0,NPoints) denote single-point clusters, and values
        //         from [NPoints,2*NPoints-1) denote ones obtained by merging
        //         smaller clusters. Negative calues correspond to absent clusters.
        //
        //         Initially it contains [0...NPoints-1], after each merge
        //         one element of CIdx (one with index C0) is replaced by
        //         NPoints+MergeIdx, and another one with index C1 is
        //         rewritten by -1.
        // 
        // NOTE 2: CSizes is array[NPoints] which stores sizes of clusters.
        //         
        //
        cidx = new int[npoints];
        csizes = new int[npoints];
        for (i = 0; i <= npoints - 1; i++)
        {
            cidx[i] = i;
            csizes[i] = 1;
        }
        for (mergeidx = 0; mergeidx <= npoints - 2; mergeidx++)
        {

            //
            // Select pair of clusters (C0,C1) with CIdx[C0]<CIdx[C1] to merge.
            //
            c0 = -1;
            c1 = -1;
            d01 = math.maxrealnumber;
            for (i = 0; i <= npoints - 1; i++)
            {
                if (cidx[i] >= 0)
                {
                    if ((double)(d[i, nnidx[i]]) < (double)(d01))
                    {
                        c0 = i;
                        c1 = nnidx[i];
                        d01 = d[i, nnidx[i]];
                    }
                }
            }
            ap.assert((double)(d01) < (double)(math.maxrealnumber), "ClusterizerRunAHC: internal error");
            if (cidx[c0] > cidx[c1])
            {
                i = c1;
                c1 = c0;
                c0 = i;
            }

            //
            // Fill one row of Rep.Z and one element of Rep.MergeDist
            //
            rep.z[mergeidx, 0] = cidx[c0];
            rep.z[mergeidx, 1] = cidx[c1];
            rep.mergedist[mergeidx] = d01;

            //
            // Update distance matrix:
            // * row/column C0 are updated by distances to the new cluster
            // * row/column C1 are considered empty (we can fill them by zeros,
            //   but do not want to spend time - we just ignore them)
            //
            // NOTE: it is important to update distance matrix BEFORE CIdx/CSizes
            //       are updated.
            //
            ap.assert((((s.ahcalgo == 0 || s.ahcalgo == 1) || s.ahcalgo == 2) || s.ahcalgo == 3) || s.ahcalgo == 4, "ClusterizerRunAHC: internal error");
            for (i = 0; i <= npoints - 1; i++)
            {
                if (i != c0 && i != c1)
                {
                    n0 = csizes[c0];
                    n1 = csizes[c1];
                    ni = csizes[i];
                    if (s.ahcalgo == 0)
                    {
                        d[i, c0] = Math.Max(d[i, c0], d[i, c1]);
                    }
                    if (s.ahcalgo == 1)
                    {
                        d[i, c0] = Math.Min(d[i, c0], d[i, c1]);
                    }
                    if (s.ahcalgo == 2)
                    {
                        d[i, c0] = (csizes[c0] * d[i, c0] + csizes[c1] * d[i, c1]) / (csizes[c0] + csizes[c1]);
                    }
                    if (s.ahcalgo == 3)
                    {
                        d[i, c0] = (d[i, c0] + d[i, c1]) / 2;
                    }
                    if (s.ahcalgo == 4)
                    {
                        d[i, c0] = ((n0 + ni) * d[i, c0] + (n1 + ni) * d[i, c1] - ni * d01) / (n0 + n1 + ni);
                    }
                    d[c0, i] = d[i, c0];
                }
            }

            //
            // Update CIdx and CSizes
            //
            cidx[c0] = npoints + mergeidx;
            cidx[c1] = -1;
            csizes[c0] = csizes[c0] + csizes[c1];
            csizes[c1] = 0;

            //
            // Update nearest neighbors array:
            // * update nearest neighbors of everything except for C0/C1
            // * update neighbors of C0/C1
            //
            for (i = 0; i <= npoints - 1; i++)
            {
                if ((cidx[i] >= 0 && i != c0) && (nnidx[i] == c0 || nnidx[i] == c1))
                {

                    //
                    // I-th cluster which is distinct from C0/C1 has former C0/C1 cluster as its nearest
                    // neighbor. We handle this issue depending on specific AHC algorithm being used.
                    //
                    if (s.ahcalgo == 1)
                    {

                        //
                        // Single linkage. Merging of two clusters together
                        // does NOT change distances between new cluster and
                        // other clusters.
                        //
                        // The only thing we have to do is to update nearest neighbor index
                        //
                        nnidx[i] = c0;
                    }
                    else
                    {

                        //
                        // Something other than single linkage. We have to re-examine
                        // all the row to find nearest neighbor.
                        //
                        k = -1;
                        v = math.maxrealnumber;
                        for (j = 0; j <= npoints - 1; j++)
                        {
                            if ((cidx[j] >= 0 && j != i) && (double)(d[i, j]) < (double)(v))
                            {
                                k = j;
                                v = d[i, j];
                            }
                        }
                        ap.assert((double)(v) < (double)(math.maxrealnumber) || mergeidx == npoints - 2, "ClusterizerRunAHC: internal error");
                        nnidx[i] = k;
                    }
                }
            }
            k = -1;
            v = math.maxrealnumber;
            for (j = 0; j <= npoints - 1; j++)
            {
                if ((cidx[j] >= 0 && j != c0) && (double)(d[c0, j]) < (double)(v))
                {
                    k = j;
                    v = d[c0, j];
                }
            }
            ap.assert((double)(v) < (double)(math.maxrealnumber) || mergeidx == npoints - 2, "ClusterizerRunAHC: internal error");
            nnidx[c0] = k;
        }

        //
        // Calculate Rep.P and Rep.PM.
        //
        // In order to do that, we fill CInfo matrix - (2*NPoints-1)*3 matrix,
        // with I-th row containing:
        // * CInfo[I,0]     -   size of I-th cluster
        // * CInfo[I,1]     -   beginning of I-th cluster
        // * CInfo[I,2]     -   end of I-th cluster
        // * CInfo[I,3]     -   height of I-th cluster
        //
        // We perform it as follows:
        // * first NPoints clusters have unit size (CInfo[I,0]=1) and zero
        //   height (CInfo[I,3]=0)
        // * we replay NPoints-1 merges from first to last and fill sizes of
        //   corresponding clusters (new size is a sum of sizes of clusters
        //   being merged) and height (new height is max(heights)+1).
        // * now we ready to determine locations of clusters. Last cluster
        //   spans entire dataset, we know it. We replay merges from last to
        //   first, during each merge we already know location of the merge
        //   result, and we can position first cluster to the left part of
        //   the result, and second cluster to the right part.
        //
        rep.p = new int[npoints];
        rep.pm = new int[npoints - 1, 6];
        cinfo = new int[2 * npoints - 1, 4];
        for (i = 0; i <= npoints - 1; i++)
        {
            cinfo[i, 0] = 1;
            cinfo[i, 3] = 0;
        }
        for (i = 0; i <= npoints - 2; i++)
        {
            cinfo[npoints + i, 0] = cinfo[rep.z[i, 0], 0] + cinfo[rep.z[i, 1], 0];
            cinfo[npoints + i, 3] = Math.Max(cinfo[rep.z[i, 0], 3], cinfo[rep.z[i, 1], 3]) + 1;
        }
        cinfo[2 * npoints - 2, 1] = 0;
        cinfo[2 * npoints - 2, 2] = npoints - 1;
        for (i = npoints - 2; i >= 0; i--)
        {

            //
            // We merge C0 which spans [A0,B0] and C1 (spans [A1,B1]),
            // with unknown A0, B0, A1, B1. However, we know that result
            // is CR, which spans [AR,BR] with known AR/BR, and we know
            // sizes of C0, C1, CR (denotes as S0, S1, SR).
            //
            c0 = rep.z[i, 0];
            c1 = rep.z[i, 1];
            s0 = cinfo[c0, 0];
            s1 = cinfo[c1, 0];
            ar = cinfo[npoints + i, 1];
            br = cinfo[npoints + i, 2];
            cinfo[c0, 1] = ar;
            cinfo[c0, 2] = ar + s0 - 1;
            cinfo[c1, 1] = br - (s1 - 1);
            cinfo[c1, 2] = br;
            rep.pm[i, 0] = cinfo[c0, 1];
            rep.pm[i, 1] = cinfo[c0, 2];
            rep.pm[i, 2] = cinfo[c1, 1];
            rep.pm[i, 3] = cinfo[c1, 2];
            rep.pm[i, 4] = cinfo[c0, 3];
            rep.pm[i, 5] = cinfo[c1, 3];
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            ap.assert(cinfo[i, 1] == cinfo[i, 2]);
            rep.p[i] = cinfo[i, 1];
        }

        //
        // Calculate Rep.PZ
        //
        rep.pz = new int[npoints - 1, 2];
        for (i = 0; i <= npoints - 2; i++)
        {
            rep.pz[i, 0] = rep.z[i, 0];
            rep.pz[i, 1] = rep.z[i, 1];
            if (rep.pz[i, 0] < npoints)
            {
                rep.pz[i, 0] = rep.p[rep.pz[i, 0]];
            }
            if (rep.pz[i, 1] < npoints)
            {
                rep.pz[i, 1] = rep.p[rep.pz[i, 1]];
            }
        }
    }


    /*************************************************************************
    This function recursively evaluates distance matrix  for  SOME  (not all!)
    distance types.

    INPUT PARAMETERS:
        XY      -   array[?,NFeatures], dataset
        NFeatures-  number of features, >=1
        DistType-   distance function:
                    *  0    Chebyshev distance  (L-inf norm)
                    *  1    city block distance (L1 norm)
        D       -   preallocated output matrix
        I0,I1   -   half interval of rows to calculate: [I0,I1) is processed
        J0,J1   -   half interval of cols to calculate: [J0,J1) is processed

    OUTPUT PARAMETERS:
        D       -   array[NPoints,NPoints], distance matrix
                    upper triangle and main diagonal are initialized with
                    data.

    NOTE: intersection of [I0,I1) and [J0,J1)  may  completely  lie  in  upper
          triangle, only partially intersect with it, or have zero intersection.
          In any case, only intersection of submatrix given by [I0,I1)*[J0,J1)
          with upper triangle of the matrix is evaluated.
          
          Say, for 4x4 distance matrix A:
          * [0,2)*[0,2) will result in evaluation of A00, A01, A11
          * [2,4)*[2,4) will result in evaluation of A22, A23, A32, A33
          * [2,4)*[0,2) will result in evaluation of empty set of elements
          

      -- ALGLIB --
         Copyright 07.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void evaluatedistancematrixrec(double[,] xy,
        int nfeatures,
        int disttype,
        double[,] d,
        int i0,
        int i1,
        int j0,
        int j1,
        xparams _params)
    {
        double rcomplexity = 0;
        int len0 = 0;
        int len1 = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        double vv = 0;

        ap.assert(disttype == 0 || disttype == 1, "EvaluateDistanceMatrixRec: incorrect DistType");

        //
        // Normalize J0/J1:
        // * J0:=max(J0,I0) - we ignore lower triangle
        // * J1:=max(J1,J0) - normalize J1
        //
        j0 = Math.Max(j0, i0);
        j1 = Math.Max(j1, j0);
        if (j1 <= j0 || i1 <= i0)
        {
            return;
        }
        rcomplexity = complexitymultiplier * apserv.rmul3(i1 - i0, j1 - j0, nfeatures, _params);
        if ((i1 - i0 > 2 || j1 - j0 > 2) && (double)(rcomplexity) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_evaluatedistancematrixrec(xy, nfeatures, disttype, d, i0, i1, j0, j1, _params))
            {
                return;
            }
        }

        //
        // Try to process in parallel. Two condtions must hold in order to
        // activate parallel processing:
        // 1. I1-I0>2 or J1-J0>2
        // 2. (I1-I0)*(J1-J0)*NFeatures>=ParallelComplexity
        //
        // NOTE: all quantities are converted to reals in order to avoid
        //       integer overflow during multiplication
        //
        // NOTE: strict inequality in (1) is necessary to reduce task to 2x2
        //       basecases. In future versions we will be able to handle such
        //       basecases more efficiently than 1x1 cases.
        //
        if ((double)(rcomplexity) >= (double)(apserv.spawnlevel(_params)) && (i1 - i0 > 2 || j1 - j0 > 2))
        {

            //
            // Recursive division along largest of dimensions
            //
            if (i1 - i0 > j1 - j0)
            {
                apserv.splitlengtheven(i1 - i0, ref len0, ref len1, _params);
                evaluatedistancematrixrec(xy, nfeatures, disttype, d, i0, i0 + len0, j0, j1, _params);
                evaluatedistancematrixrec(xy, nfeatures, disttype, d, i0 + len0, i1, j0, j1, _params);
            }
            else
            {
                apserv.splitlengtheven(j1 - j0, ref len0, ref len1, _params);
                evaluatedistancematrixrec(xy, nfeatures, disttype, d, i0, i1, j0, j0 + len0, _params);
                evaluatedistancematrixrec(xy, nfeatures, disttype, d, i0, i1, j0 + len0, j1, _params);
            }
            return;
        }

        //
        // Sequential processing
        //
        for (i = i0; i <= i1 - 1; i++)
        {
            for (j = j0; j <= j1 - 1; j++)
            {
                if (j >= i)
                {
                    v = 0.0;
                    if (disttype == 0)
                    {
                        for (k = 0; k <= nfeatures - 1; k++)
                        {
                            vv = xy[i, k] - xy[j, k];
                            if ((double)(vv) < (double)(0))
                            {
                                vv = -vv;
                            }
                            if ((double)(vv) > (double)(v))
                            {
                                v = vv;
                            }
                        }
                    }
                    if (disttype == 1)
                    {
                        for (k = 0; k <= nfeatures - 1; k++)
                        {
                            vv = xy[i, k] - xy[j, k];
                            if ((double)(vv) < (double)(0))
                            {
                                vv = -vv;
                            }
                            v = v + vv;
                        }
                    }
                    d[i, j] = v;
                }
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_evaluatedistancematrixrec(double[,] xy,
        int nfeatures,
        int disttype,
        double[,] d,
        int i0,
        int i1,
        int j0,
        int j1, xparams _params)
    {
        return false;
    }


}
