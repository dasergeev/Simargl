using System;

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

public class nearestneighbor
{
    /*************************************************************************
    Buffer object which is used to perform nearest neighbor  requests  in  the
    multithreaded mode (multiple threads working with same KD-tree object).

    This object should be created with KDTreeCreateRequestBuffer().
    *************************************************************************/
    public class kdtreerequestbuffer : apobject
    {
        public double[] x;
        public double[] boxmin;
        public double[] boxmax;
        public int kneeded;
        public double rneeded;
        public bool selfmatch;
        public double approxf;
        public int kcur;
        public int[] idx;
        public double[] r;
        public double[] buf;
        public double[] curboxmin;
        public double[] curboxmax;
        public double curdist;
        public kdtreerequestbuffer()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            boxmin = new double[0];
            boxmax = new double[0];
            idx = new int[0];
            r = new double[0];
            buf = new double[0];
            curboxmin = new double[0];
            curboxmax = new double[0];
        }
        public override apobject make_copy()
        {
            kdtreerequestbuffer _result = new kdtreerequestbuffer();
            _result.x = (double[])x.Clone();
            _result.boxmin = (double[])boxmin.Clone();
            _result.boxmax = (double[])boxmax.Clone();
            _result.kneeded = kneeded;
            _result.rneeded = rneeded;
            _result.selfmatch = selfmatch;
            _result.approxf = approxf;
            _result.kcur = kcur;
            _result.idx = (int[])idx.Clone();
            _result.r = (double[])r.Clone();
            _result.buf = (double[])buf.Clone();
            _result.curboxmin = (double[])curboxmin.Clone();
            _result.curboxmax = (double[])curboxmax.Clone();
            _result.curdist = curdist;
            return _result;
        }
    };


    /*************************************************************************
    KD-tree object.
    *************************************************************************/
    public class kdtree : apobject
    {
        public int n;
        public int nx;
        public int ny;
        public int normtype;
        public double[,] xy;
        public int[] tags;
        public double[] boxmin;
        public double[] boxmax;
        public int[] nodes;
        public double[] splits;
        public kdtreerequestbuffer innerbuf;
        public int debugcounter;
        public kdtree()
        {
            init();
        }
        public override void init()
        {
            xy = new double[0, 0];
            tags = new int[0];
            boxmin = new double[0];
            boxmax = new double[0];
            nodes = new int[0];
            splits = new double[0];
            innerbuf = new kdtreerequestbuffer();
        }
        public override apobject make_copy()
        {
            kdtree _result = new kdtree();
            _result.n = n;
            _result.nx = nx;
            _result.ny = ny;
            _result.normtype = normtype;
            _result.xy = (double[,])xy.Clone();
            _result.tags = (int[])tags.Clone();
            _result.boxmin = (double[])boxmin.Clone();
            _result.boxmax = (double[])boxmax.Clone();
            _result.nodes = (int[])nodes.Clone();
            _result.splits = (double[])splits.Clone();
            _result.innerbuf = (kdtreerequestbuffer)innerbuf.make_copy();
            _result.debugcounter = debugcounter;
            return _result;
        }
    };




    public const int splitnodesize = 6;
    public const int kdtreefirstversion = 0;


    /*************************************************************************
    KD-tree creation

    This subroutine creates KD-tree from set of X-values and optional Y-values

    INPUT PARAMETERS
        XY      -   dataset, array[0..N-1,0..NX+NY-1].
                    one row corresponds to one point.
                    first NX columns contain X-values, next NY (NY may be zero)
                    columns may contain associated Y-values
        N       -   number of points, N>=0.
        NX      -   space dimension, NX>=1.
        NY      -   number of optional Y-values, NY>=0.
        NormType-   norm type:
                    * 0 denotes infinity-norm
                    * 1 denotes 1-norm
                    * 2 denotes 2-norm (Euclidean norm)
                    
    OUTPUT PARAMETERS
        KDT     -   KD-tree
        
        
    NOTES

    1. KD-tree  creation  have O(N*logN) complexity and O(N*(2*NX+NY))  memory
       requirements.
    2. Although KD-trees may be used with any combination of N  and  NX,  they
       are more efficient than brute-force search only when N >> 4^NX. So they
       are most useful in low-dimensional tasks (NX=2, NX=3). NX=1  is another
       inefficient case, because  simple  binary  search  (without  additional
       structures) is much more efficient in such tasks than KD-trees.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreebuild(double[,] xy,
        int n,
        int nx,
        int ny,
        int normtype,
        kdtree kdt,
        xparams _params)
    {
        int[] tags = new int[0];
        int i = 0;

        ap.assert(n >= 0, "KDTreeBuild: N<0");
        ap.assert(nx >= 1, "KDTreeBuild: NX<1");
        ap.assert(ny >= 0, "KDTreeBuild: NY<0");
        ap.assert(normtype >= 0 && normtype <= 2, "KDTreeBuild: incorrect NormType");
        ap.assert(ap.rows(xy) >= n, "KDTreeBuild: rows(X)<N");
        ap.assert(ap.cols(xy) >= nx + ny || n == 0, "KDTreeBuild: cols(X)<NX+NY");
        ap.assert(apserv.apservisfinitematrix(xy, n, nx + ny, _params), "KDTreeBuild: XY contains infinite or NaN values");
        if (n > 0)
        {
            tags = new int[n];
            for (i = 0; i <= n - 1; i++)
            {
                tags[i] = 0;
            }
        }
        kdtreebuildtagged(xy, tags, n, nx, ny, normtype, kdt, _params);
    }


    /*************************************************************************
    KD-tree creation

    This  subroutine  creates  KD-tree  from set of X-values, integer tags and
    optional Y-values

    INPUT PARAMETERS
        XY      -   dataset, array[0..N-1,0..NX+NY-1].
                    one row corresponds to one point.
                    first NX columns contain X-values, next NY (NY may be zero)
                    columns may contain associated Y-values
        Tags    -   tags, array[0..N-1], contains integer tags associated
                    with points.
        N       -   number of points, N>=0
        NX      -   space dimension, NX>=1.
        NY      -   number of optional Y-values, NY>=0.
        NormType-   norm type:
                    * 0 denotes infinity-norm
                    * 1 denotes 1-norm
                    * 2 denotes 2-norm (Euclidean norm)

    OUTPUT PARAMETERS
        KDT     -   KD-tree

    NOTES

    1. KD-tree  creation  have O(N*logN) complexity and O(N*(2*NX+NY))  memory
       requirements.
    2. Although KD-trees may be used with any combination of N  and  NX,  they
       are more efficient than brute-force search only when N >> 4^NX. So they
       are most useful in low-dimensional tasks (NX=2, NX=3). NX=1  is another
       inefficient case, because  simple  binary  search  (without  additional
       structures) is much more efficient in such tasks than KD-trees.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreebuildtagged(double[,] xy,
        int[] tags,
        int n,
        int nx,
        int ny,
        int normtype,
        kdtree kdt,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int nodesoffs = 0;
        int splitsoffs = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(n >= 0, "KDTreeBuildTagged: N<0");
        ap.assert(nx >= 1, "KDTreeBuildTagged: NX<1");
        ap.assert(ny >= 0, "KDTreeBuildTagged: NY<0");
        ap.assert(normtype >= 0 && normtype <= 2, "KDTreeBuildTagged: incorrect NormType");
        ap.assert(ap.rows(xy) >= n, "KDTreeBuildTagged: rows(X)<N");
        ap.assert(ap.cols(xy) >= nx + ny || n == 0, "KDTreeBuildTagged: cols(X)<NX+NY");
        ap.assert(apserv.apservisfinitematrix(xy, n, nx + ny, _params), "KDTreeBuildTagged: XY contains infinite or NaN values");

        //
        // initialize
        //
        kdt.n = n;
        kdt.nx = nx;
        kdt.ny = ny;
        kdt.normtype = normtype;
        kdt.innerbuf.kcur = 0;

        //
        // N=0 => quick exit
        //
        if (n == 0)
        {
            return;
        }

        //
        // Allocate
        //
        kdtreeallocdatasetindependent(kdt, nx, ny, _params);
        kdtreeallocdatasetdependent(kdt, n, nx, ny, _params);
        kdtreecreaterequestbuffer(kdt, kdt.innerbuf, _params);

        //
        // Initial fill
        //
        for (i = 0; i <= n - 1; i++)
        {
            for (i_ = 0; i_ <= nx - 1; i_++)
            {
                kdt.xy[i, i_] = xy[i, i_];
            }
            i1_ = (0) - (nx);
            for (i_ = nx; i_ <= 2 * nx + ny - 1; i_++)
            {
                kdt.xy[i, i_] = xy[i, i_ + i1_];
            }
            kdt.tags[i] = tags[i];
        }

        //
        // Determine bounding box
        //
        for (i_ = 0; i_ <= nx - 1; i_++)
        {
            kdt.boxmin[i_] = kdt.xy[0, i_];
        }
        for (i_ = 0; i_ <= nx - 1; i_++)
        {
            kdt.boxmax[i_] = kdt.xy[0, i_];
        }
        for (i = 1; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                kdt.boxmin[j] = Math.Min(kdt.boxmin[j], kdt.xy[i, j]);
                kdt.boxmax[j] = Math.Max(kdt.boxmax[j], kdt.xy[i, j]);
            }
        }

        //
        // Generate tree
        //
        nodesoffs = 0;
        splitsoffs = 0;
        for (i_ = 0; i_ <= nx - 1; i_++)
        {
            kdt.innerbuf.curboxmin[i_] = kdt.boxmin[i_];
        }
        for (i_ = 0; i_ <= nx - 1; i_++)
        {
            kdt.innerbuf.curboxmax[i_] = kdt.boxmax[i_];
        }
        kdtreegeneratetreerec(kdt, ref nodesoffs, ref splitsoffs, 0, n, 8, _params);
        apserv.ivectorresize(ref kdt.nodes, nodesoffs, _params);
        apserv.rvectorresize(ref kdt.splits, splitsoffs, _params);
    }


    /*************************************************************************
    This function creates buffer  structure  which  can  be  used  to  perform
    parallel KD-tree requests.

    KD-tree subpackage provides two sets of request functions - ones which use
    internal buffer of KD-tree object  (these  functions  are  single-threaded
    because they use same buffer, which can not shared between  threads),  and
    ones which use external buffer.

    This function is used to initialize external buffer.

    INPUT PARAMETERS
        KDT         -   KD-tree which is associated with newly created buffer

    OUTPUT PARAMETERS
        Buf         -   external buffer.
        
        
    IMPORTANT: KD-tree buffer should be used only with  KD-tree  object  which
               was used to initialize buffer. Any attempt to use buffer   with
               different object is dangerous - you  may  get  integrity  check
               failure (exception) because sizes of internal arrays do not fit
               to dimensions of KD-tree structure.

      -- ALGLIB --
         Copyright 18.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreecreaterequestbuffer(kdtree kdt,
        kdtreerequestbuffer buf,
        xparams _params)
    {
        buf.x = new double[kdt.nx];
        buf.boxmin = new double[kdt.nx];
        buf.boxmax = new double[kdt.nx];
        buf.idx = new int[kdt.n];
        buf.r = new double[kdt.n];
        buf.buf = new double[Math.Max(kdt.n, kdt.nx)];
        buf.curboxmin = new double[kdt.nx];
        buf.curboxmax = new double[kdt.nx];
        buf.kcur = 0;
    }


    /*************************************************************************
    K-NN query: K nearest neighbors

    IMPORTANT: this function can not be used in multithreaded code because  it
               uses internal temporary buffer of kd-tree object, which can not
               be shared between multiple threads.  If  you  want  to  perform
               parallel requests, use function  which  uses  external  request
               buffer: KDTreeTsQueryKNN() ("Ts" stands for "thread-safe").

    INPUT PARAMETERS
        KDT         -   KD-tree
        X           -   point, array[0..NX-1].
        K           -   number of neighbors to return, K>=1
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True

    RESULT
        number of actual neighbors found (either K or N, if K>N).

    This  subroutine  performs  query  and  stores  its result in the internal
    structures of the KD-tree. You can use  following  subroutines  to  obtain
    these results:
    * KDTreeQueryResultsX() to get X-values
    * KDTreeQueryResultsXY() to get X- and Y-values
    * KDTreeQueryResultsTags() to get tag values
    * KDTreeQueryResultsDistances() to get distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreequeryknn(kdtree kdt,
        double[] x,
        int k,
        bool selfmatch,
        xparams _params)
    {
        int result = 0;

        ap.assert(k >= 1, "KDTreeQueryKNN: K<1!");
        ap.assert(ap.len(x) >= kdt.nx, "KDTreeQueryKNN: Length(X)<NX!");
        ap.assert(apserv.isfinitevector(x, kdt.nx, _params), "KDTreeQueryKNN: X contains infinite or NaN values!");
        result = kdtreetsqueryaknn(kdt, kdt.innerbuf, x, k, selfmatch, 0.0, _params);
        return result;
    }


    /*************************************************************************
    K-NN query: K nearest neighbors, using external thread-local buffer.

    You can call this function from multiple threads for same kd-tree instance,
    assuming that different instances of buffer object are passed to different
    threads.

    INPUT PARAMETERS
        KDT         -   kd-tree
        Buf         -   request buffer  object  created  for  this  particular
                        instance of kd-tree structure with kdtreecreaterequestbuffer()
                        function.
        X           -   point, array[0..NX-1].
        K           -   number of neighbors to return, K>=1
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True

    RESULT
        number of actual neighbors found (either K or N, if K>N).

    This  subroutine  performs  query  and  stores  its result in the internal
    structures  of  the  buffer object. You can use following  subroutines  to
    obtain these results (pay attention to "buf" in their names):
    * KDTreeTsQueryResultsX() to get X-values
    * KDTreeTsQueryResultsXY() to get X- and Y-values
    * KDTreeTsQueryResultsTags() to get tag values
    * KDTreeTsQueryResultsDistances() to get distances
        
    IMPORTANT: kd-tree buffer should be used only with  KD-tree  object  which
               was used to initialize buffer. Any attempt to use biffer   with
               different object is dangerous - you  may  get  integrity  check
               failure (exception) because sizes of internal arrays do not fit
               to dimensions of KD-tree structure.

      -- ALGLIB --
         Copyright 18.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreetsqueryknn(kdtree kdt,
        kdtreerequestbuffer buf,
        double[] x,
        int k,
        bool selfmatch,
        xparams _params)
    {
        int result = 0;

        ap.assert(k >= 1, "KDTreeTsQueryKNN: K<1!");
        ap.assert(ap.len(x) >= kdt.nx, "KDTreeTsQueryKNN: Length(X)<NX!");
        ap.assert(apserv.isfinitevector(x, kdt.nx, _params), "KDTreeTsQueryKNN: X contains infinite or NaN values!");
        result = kdtreetsqueryaknn(kdt, buf, x, k, selfmatch, 0.0, _params);
        return result;
    }


    /*************************************************************************
    R-NN query: all points within R-sphere centered at X, ordered by  distance
    between point and X (by ascending).

    NOTE: it is also possible to perform undordered queries performed by means
          of kdtreequeryrnnu() and kdtreetsqueryrnnu() functions. Such queries
          are faster because we do not have to use heap structure for sorting.

    IMPORTANT: this function can not be used in multithreaded code because  it
               uses internal temporary buffer of kd-tree object, which can not
               be shared between multiple threads.  If  you  want  to  perform
               parallel requests, use function  which  uses  external  request
               buffer: kdtreetsqueryrnn() ("Ts" stands for "thread-safe").

    INPUT PARAMETERS
        KDT         -   KD-tree
        X           -   point, array[0..NX-1].
        R           -   radius of sphere (in corresponding norm), R>0
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True

    RESULT
        number of neighbors found, >=0

    This  subroutine  performs  query  and  stores  its result in the internal
    structures of the KD-tree. You can use  following  subroutines  to  obtain
    actual results:
    * KDTreeQueryResultsX() to get X-values
    * KDTreeQueryResultsXY() to get X- and Y-values
    * KDTreeQueryResultsTags() to get tag values
    * KDTreeQueryResultsDistances() to get distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreequeryrnn(kdtree kdt,
        double[] x,
        double r,
        bool selfmatch,
        xparams _params)
    {
        int result = 0;

        ap.assert((double)(r) > (double)(0), "KDTreeQueryRNN: incorrect R!");
        ap.assert(ap.len(x) >= kdt.nx, "KDTreeQueryRNN: Length(X)<NX!");
        ap.assert(apserv.isfinitevector(x, kdt.nx, _params), "KDTreeQueryRNN: X contains infinite or NaN values!");
        result = kdtreetsqueryrnn(kdt, kdt.innerbuf, x, r, selfmatch, _params);
        return result;
    }


    /*************************************************************************
    R-NN query: all points within R-sphere  centered  at  X,  no  ordering  by
    distance as undicated by "U" suffix (faster that ordered query, for  large
    queries - significantly faster).

    IMPORTANT: this function can not be used in multithreaded code because  it
               uses internal temporary buffer of kd-tree object, which can not
               be shared between multiple threads.  If  you  want  to  perform
               parallel requests, use function  which  uses  external  request
               buffer: kdtreetsqueryrnn() ("Ts" stands for "thread-safe").

    INPUT PARAMETERS
        KDT         -   KD-tree
        X           -   point, array[0..NX-1].
        R           -   radius of sphere (in corresponding norm), R>0
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True

    RESULT
        number of neighbors found, >=0

    This  subroutine  performs  query  and  stores  its result in the internal
    structures of the KD-tree. You can use  following  subroutines  to  obtain
    actual results:
    * KDTreeQueryResultsX() to get X-values
    * KDTreeQueryResultsXY() to get X- and Y-values
    * KDTreeQueryResultsTags() to get tag values
    * KDTreeQueryResultsDistances() to get distances

    As indicated by "U" suffix, this function returns unordered results.

      -- ALGLIB --
         Copyright 01.11.2018 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreequeryrnnu(kdtree kdt,
        double[] x,
        double r,
        bool selfmatch,
        xparams _params)
    {
        int result = 0;

        ap.assert((double)(r) > (double)(0), "KDTreeQueryRNNU: incorrect R!");
        ap.assert(ap.len(x) >= kdt.nx, "KDTreeQueryRNNU: Length(X)<NX!");
        ap.assert(apserv.isfinitevector(x, kdt.nx, _params), "KDTreeQueryRNNU: X contains infinite or NaN values!");
        result = kdtreetsqueryrnnu(kdt, kdt.innerbuf, x, r, selfmatch, _params);
        return result;
    }


    /*************************************************************************
    R-NN query: all points within  R-sphere  centered  at  X,  using  external
    thread-local buffer, sorted by distance between point and X (by ascending)

    You can call this function from multiple threads for same kd-tree instance,
    assuming that different instances of buffer object are passed to different
    threads.

    NOTE: it is also possible to perform undordered queries performed by means
          of kdtreequeryrnnu() and kdtreetsqueryrnnu() functions. Such queries
          are faster because we do not have to use heap structure for sorting.

    INPUT PARAMETERS
        KDT         -   KD-tree
        Buf         -   request buffer  object  created  for  this  particular
                        instance of kd-tree structure with kdtreecreaterequestbuffer()
                        function.
        X           -   point, array[0..NX-1].
        R           -   radius of sphere (in corresponding norm), R>0
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True

    RESULT
        number of neighbors found, >=0

    This  subroutine  performs  query  and  stores  its result in the internal
    structures  of  the  buffer object. You can use following  subroutines  to
    obtain these results (pay attention to "buf" in their names):
    * KDTreeTsQueryResultsX() to get X-values
    * KDTreeTsQueryResultsXY() to get X- and Y-values
    * KDTreeTsQueryResultsTags() to get tag values
    * KDTreeTsQueryResultsDistances() to get distances
        
    IMPORTANT: kd-tree buffer should be used only with  KD-tree  object  which
               was used to initialize buffer. Any attempt to use biffer   with
               different object is dangerous - you  may  get  integrity  check
               failure (exception) because sizes of internal arrays do not fit
               to dimensions of KD-tree structure.

      -- ALGLIB --
         Copyright 18.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreetsqueryrnn(kdtree kdt,
        kdtreerequestbuffer buf,
        double[] x,
        double r,
        bool selfmatch,
        xparams _params)
    {
        int result = 0;

        ap.assert(math.isfinite(r) && (double)(r) > (double)(0), "KDTreeTsQueryRNN: incorrect R!");
        ap.assert(ap.len(x) >= kdt.nx, "KDTreeTsQueryRNN: Length(X)<NX!");
        ap.assert(apserv.isfinitevector(x, kdt.nx, _params), "KDTreeTsQueryRNN: X contains infinite or NaN values!");
        result = tsqueryrnn(kdt, buf, x, r, selfmatch, true, _params);
        return result;
    }


    /*************************************************************************
    R-NN query: all points within  R-sphere  centered  at  X,  using  external
    thread-local buffer, no ordering by distance as undicated  by  "U"  suffix
    (faster that ordered query, for large queries - significantly faster).

    You can call this function from multiple threads for same kd-tree instance,
    assuming that different instances of buffer object are passed to different
    threads.

    INPUT PARAMETERS
        KDT         -   KD-tree
        Buf         -   request buffer  object  created  for  this  particular
                        instance of kd-tree structure with kdtreecreaterequestbuffer()
                        function.
        X           -   point, array[0..NX-1].
        R           -   radius of sphere (in corresponding norm), R>0
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True

    RESULT
        number of neighbors found, >=0

    This  subroutine  performs  query  and  stores  its result in the internal
    structures  of  the  buffer object. You can use following  subroutines  to
    obtain these results (pay attention to "buf" in their names):
    * KDTreeTsQueryResultsX() to get X-values
    * KDTreeTsQueryResultsXY() to get X- and Y-values
    * KDTreeTsQueryResultsTags() to get tag values
    * KDTreeTsQueryResultsDistances() to get distances

    As indicated by "U" suffix, this function returns unordered results.
        
    IMPORTANT: kd-tree buffer should be used only with  KD-tree  object  which
               was used to initialize buffer. Any attempt to use biffer   with
               different object is dangerous - you  may  get  integrity  check
               failure (exception) because sizes of internal arrays do not fit
               to dimensions of KD-tree structure.

      -- ALGLIB --
         Copyright 18.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreetsqueryrnnu(kdtree kdt,
        kdtreerequestbuffer buf,
        double[] x,
        double r,
        bool selfmatch,
        xparams _params)
    {
        int result = 0;

        ap.assert(math.isfinite(r) && (double)(r) > (double)(0), "KDTreeTsQueryRNNU: incorrect R!");
        ap.assert(ap.len(x) >= kdt.nx, "KDTreeTsQueryRNNU: Length(X)<NX!");
        ap.assert(apserv.isfinitevector(x, kdt.nx, _params), "KDTreeTsQueryRNNU: X contains infinite or NaN values!");
        result = tsqueryrnn(kdt, buf, x, r, selfmatch, false, _params);
        return result;
    }


    /*************************************************************************
    K-NN query: approximate K nearest neighbors

    IMPORTANT: this function can not be used in multithreaded code because  it
               uses internal temporary buffer of kd-tree object, which can not
               be shared between multiple threads.  If  you  want  to  perform
               parallel requests, use function  which  uses  external  request
               buffer: KDTreeTsQueryAKNN() ("Ts" stands for "thread-safe").

    INPUT PARAMETERS
        KDT         -   KD-tree
        X           -   point, array[0..NX-1].
        K           -   number of neighbors to return, K>=1
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True
        Eps         -   approximation factor, Eps>=0. eps-approximate  nearest
                        neighbor  is  a  neighbor  whose distance from X is at
                        most (1+eps) times distance of true nearest neighbor.

    RESULT
        number of actual neighbors found (either K or N, if K>N).
        
    NOTES
        significant performance gain may be achieved only when Eps  is  is  on
        the order of magnitude of 1 or larger.

    This  subroutine  performs  query  and  stores  its result in the internal
    structures of the KD-tree. You can use  following  subroutines  to  obtain
    these results:
    * KDTreeQueryResultsX() to get X-values
    * KDTreeQueryResultsXY() to get X- and Y-values
    * KDTreeQueryResultsTags() to get tag values
    * KDTreeQueryResultsDistances() to get distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreequeryaknn(kdtree kdt,
        double[] x,
        int k,
        bool selfmatch,
        double eps,
        xparams _params)
    {
        int result = 0;

        result = kdtreetsqueryaknn(kdt, kdt.innerbuf, x, k, selfmatch, eps, _params);
        return result;
    }


    /*************************************************************************
    K-NN query: approximate K nearest neighbors, using thread-local buffer.

    You can call this function from multiple threads for same kd-tree instance,
    assuming that different instances of buffer object are passed to different
    threads.

    INPUT PARAMETERS
        KDT         -   KD-tree
        Buf         -   request buffer  object  created  for  this  particular
                        instance of kd-tree structure with kdtreecreaterequestbuffer()
                        function.
        X           -   point, array[0..NX-1].
        K           -   number of neighbors to return, K>=1
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True
        Eps         -   approximation factor, Eps>=0. eps-approximate  nearest
                        neighbor  is  a  neighbor  whose distance from X is at
                        most (1+eps) times distance of true nearest neighbor.

    RESULT
        number of actual neighbors found (either K or N, if K>N).
        
    NOTES
        significant performance gain may be achieved only when Eps  is  is  on
        the order of magnitude of 1 or larger.

    This  subroutine  performs  query  and  stores  its result in the internal
    structures  of  the  buffer object. You can use following  subroutines  to
    obtain these results (pay attention to "buf" in their names):
    * KDTreeTsQueryResultsX() to get X-values
    * KDTreeTsQueryResultsXY() to get X- and Y-values
    * KDTreeTsQueryResultsTags() to get tag values
    * KDTreeTsQueryResultsDistances() to get distances
        
    IMPORTANT: kd-tree buffer should be used only with  KD-tree  object  which
               was used to initialize buffer. Any attempt to use biffer   with
               different object is dangerous - you  may  get  integrity  check
               failure (exception) because sizes of internal arrays do not fit
               to dimensions of KD-tree structure.

      -- ALGLIB --
         Copyright 18.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreetsqueryaknn(kdtree kdt,
        kdtreerequestbuffer buf,
        double[] x,
        int k,
        bool selfmatch,
        double eps,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int j = 0;

        ap.assert(k > 0, "KDTreeTsQueryAKNN: incorrect K!");
        ap.assert((double)(eps) >= (double)(0), "KDTreeTsQueryAKNN: incorrect Eps!");
        ap.assert(ap.len(x) >= kdt.nx, "KDTreeTsQueryAKNN: Length(X)<NX!");
        ap.assert(apserv.isfinitevector(x, kdt.nx, _params), "KDTreeTsQueryAKNN: X contains infinite or NaN values!");

        //
        // Handle special case: KDT.N=0
        //
        if (kdt.n == 0)
        {
            buf.kcur = 0;
            result = 0;
            return result;
        }

        //
        // Check consistency of request buffer
        //
        checkrequestbufferconsistency(kdt, buf, _params);

        //
        // Prepare parameters
        //
        k = Math.Min(k, kdt.n);
        buf.kneeded = k;
        buf.rneeded = 0;
        buf.selfmatch = selfmatch;
        if (kdt.normtype == 2)
        {
            buf.approxf = 1 / math.sqr(1 + eps);
        }
        else
        {
            buf.approxf = 1 / (1 + eps);
        }
        buf.kcur = 0;

        //
        // calculate distance from point to current bounding box
        //
        kdtreeinitbox(kdt, x, buf, _params);

        //
        // call recursive search
        // results are returned as heap
        //
        kdtreequerynnrec(kdt, buf, 0, _params);

        //
        // pop from heap to generate ordered representation
        //
        // last element is non pop'ed because it is already in
        // its place
        //
        result = buf.kcur;
        j = buf.kcur;
        for (i = buf.kcur; i >= 2; i--)
        {
            tsort.tagheappopi(ref buf.r, ref buf.idx, ref j, _params);
        }
        return result;
    }


    /*************************************************************************
    Box query: all points within user-specified box.

    IMPORTANT: this function can not be used in multithreaded code because  it
               uses internal temporary buffer of kd-tree object, which can not
               be shared between multiple threads.  If  you  want  to  perform
               parallel requests, use function  which  uses  external  request
               buffer: KDTreeTsQueryBox() ("Ts" stands for "thread-safe").

    INPUT PARAMETERS
        KDT         -   KD-tree
        BoxMin      -   lower bounds, array[0..NX-1].
        BoxMax      -   upper bounds, array[0..NX-1].


    RESULT
        number of actual neighbors found (in [0,N]).

    This  subroutine  performs  query  and  stores  its result in the internal
    structures of the KD-tree. You can use  following  subroutines  to  obtain
    these results:
    * KDTreeQueryResultsX() to get X-values
    * KDTreeQueryResultsXY() to get X- and Y-values
    * KDTreeQueryResultsTags() to get tag values
    * KDTreeQueryResultsDistances() returns zeros for this request
        
    NOTE: this particular query returns unordered results, because there is no
          meaningful way of  ordering  points.  Furthermore,  no 'distance' is
          associated with points - it is either INSIDE  or OUTSIDE (so request
          for distances will return zeros).

      -- ALGLIB --
         Copyright 14.05.2016 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreequerybox(kdtree kdt,
        double[] boxmin,
        double[] boxmax,
        xparams _params)
    {
        int result = 0;

        result = kdtreetsquerybox(kdt, kdt.innerbuf, boxmin, boxmax, _params);
        return result;
    }


    /*************************************************************************
    Box query: all points within user-specified box, using thread-local buffer.

    You can call this function from multiple threads for same kd-tree instance,
    assuming that different instances of buffer object are passed to different
    threads.

    INPUT PARAMETERS
        KDT         -   KD-tree
        Buf         -   request buffer  object  created  for  this  particular
                        instance of kd-tree structure with kdtreecreaterequestbuffer()
                        function.
        BoxMin      -   lower bounds, array[0..NX-1].
        BoxMax      -   upper bounds, array[0..NX-1].

    RESULT
        number of actual neighbors found (in [0,N]).

    This  subroutine  performs  query  and  stores  its result in the internal
    structures  of  the  buffer object. You can use following  subroutines  to
    obtain these results (pay attention to "ts" in their names):
    * KDTreeTsQueryResultsX() to get X-values
    * KDTreeTsQueryResultsXY() to get X- and Y-values
    * KDTreeTsQueryResultsTags() to get tag values
    * KDTreeTsQueryResultsDistances() returns zeros for this query
        
    NOTE: this particular query returns unordered results, because there is no
          meaningful way of  ordering  points.  Furthermore,  no 'distance' is
          associated with points - it is either INSIDE  or OUTSIDE (so request
          for distances will return zeros).
        
    IMPORTANT: kd-tree buffer should be used only with  KD-tree  object  which
               was used to initialize buffer. Any attempt to use biffer   with
               different object is dangerous - you  may  get  integrity  check
               failure (exception) because sizes of internal arrays do not fit
               to dimensions of KD-tree structure.

      -- ALGLIB --
         Copyright 14.05.2016 by Bochkanov Sergey
    *************************************************************************/
    public static int kdtreetsquerybox(kdtree kdt,
        kdtreerequestbuffer buf,
        double[] boxmin,
        double[] boxmax,
        xparams _params)
    {
        int result = 0;
        int j = 0;

        ap.assert(ap.len(boxmin) >= kdt.nx, "KDTreeTsQueryBox: Length(BoxMin)<NX!");
        ap.assert(ap.len(boxmax) >= kdt.nx, "KDTreeTsQueryBox: Length(BoxMax)<NX!");
        ap.assert(apserv.isfinitevector(boxmin, kdt.nx, _params), "KDTreeTsQueryBox: BoxMin contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(boxmax, kdt.nx, _params), "KDTreeTsQueryBox: BoxMax contains infinite or NaN values!");

        //
        // Check consistency of request buffer
        //
        checkrequestbufferconsistency(kdt, buf, _params);

        //
        // Quick exit for degenerate boxes
        //
        for (j = 0; j <= kdt.nx - 1; j++)
        {
            if ((double)(boxmin[j]) > (double)(boxmax[j]))
            {
                buf.kcur = 0;
                result = 0;
                return result;
            }
        }

        //
        // Prepare parameters
        //
        for (j = 0; j <= kdt.nx - 1; j++)
        {
            buf.boxmin[j] = boxmin[j];
            buf.boxmax[j] = boxmax[j];
            buf.curboxmin[j] = boxmin[j];
            buf.curboxmax[j] = boxmax[j];
        }
        buf.kcur = 0;

        //
        // call recursive search
        //
        kdtreequeryboxrec(kdt, buf, 0, _params);
        result = buf.kcur;
        return result;
    }


    /*************************************************************************
    X-values from last query.

    This function retuns results stored in  the  internal  buffer  of  kd-tree
    object. If you performed buffered requests (ones which  use  instances  of
    kdtreerequestbuffer class), you  should  call  buffered  version  of  this
    function - kdtreetsqueryresultsx().

    INPUT PARAMETERS
        KDT     -   KD-tree
        X       -   possibly pre-allocated buffer. If X is too small to store
                    result, it is resized. If size(X) is enough to store
                    result, it is left unchanged.

    OUTPUT PARAMETERS
        X       -   rows are filled with X-values

    NOTES
    1. points are ordered by distance from the query point (first = closest)
    2. if  XY is larger than required to store result, only leading part  will
       be overwritten; trailing part will be left unchanged. So  if  on  input
       XY = [[A,B],[C,D]], and result is [1,2],  then  on  exit  we  will  get
       XY = [[1,2],[C,D]]. This is done purposely to increase performance;  if
       you want function  to  resize  array  according  to  result  size,  use
       function with same name and suffix 'I'.

    SEE ALSO
    * KDTreeQueryResultsXY()            X- and Y-values
    * KDTreeQueryResultsTags()          tag values
    * KDTreeQueryResultsDistances()     distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreequeryresultsx(kdtree kdt,
        ref double[,] x,
        xparams _params)
    {
        kdtreetsqueryresultsx(kdt, kdt.innerbuf, ref x, _params);
    }


    /*************************************************************************
    X- and Y-values from last query

    This function retuns results stored in  the  internal  buffer  of  kd-tree
    object. If you performed buffered requests (ones which  use  instances  of
    kdtreerequestbuffer class), you  should  call  buffered  version  of  this
    function - kdtreetsqueryresultsxy().

    INPUT PARAMETERS
        KDT     -   KD-tree
        XY      -   possibly pre-allocated buffer. If XY is too small to store
                    result, it is resized. If size(XY) is enough to store
                    result, it is left unchanged.

    OUTPUT PARAMETERS
        XY      -   rows are filled with points: first NX columns with
                    X-values, next NY columns - with Y-values.

    NOTES
    1. points are ordered by distance from the query point (first = closest)
    2. if  XY is larger than required to store result, only leading part  will
       be overwritten; trailing part will be left unchanged. So  if  on  input
       XY = [[A,B],[C,D]], and result is [1,2],  then  on  exit  we  will  get
       XY = [[1,2],[C,D]]. This is done purposely to increase performance;  if
       you want function  to  resize  array  according  to  result  size,  use
       function with same name and suffix 'I'.

    SEE ALSO
    * KDTreeQueryResultsX()             X-values
    * KDTreeQueryResultsTags()          tag values
    * KDTreeQueryResultsDistances()     distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreequeryresultsxy(kdtree kdt,
        ref double[,] xy,
        xparams _params)
    {
        kdtreetsqueryresultsxy(kdt, kdt.innerbuf, ref xy, _params);
    }


    /*************************************************************************
    Tags from last query

    This function retuns results stored in  the  internal  buffer  of  kd-tree
    object. If you performed buffered requests (ones which  use  instances  of
    kdtreerequestbuffer class), you  should  call  buffered  version  of  this
    function - kdtreetsqueryresultstags().

    INPUT PARAMETERS
        KDT     -   KD-tree
        Tags    -   possibly pre-allocated buffer. If X is too small to store
                    result, it is resized. If size(X) is enough to store
                    result, it is left unchanged.

    OUTPUT PARAMETERS
        Tags    -   filled with tags associated with points,
                    or, when no tags were supplied, with zeros

    NOTES
    1. points are ordered by distance from the query point (first = closest)
    2. if  XY is larger than required to store result, only leading part  will
       be overwritten; trailing part will be left unchanged. So  if  on  input
       XY = [[A,B],[C,D]], and result is [1,2],  then  on  exit  we  will  get
       XY = [[1,2],[C,D]]. This is done purposely to increase performance;  if
       you want function  to  resize  array  according  to  result  size,  use
       function with same name and suffix 'I'.

    SEE ALSO
    * KDTreeQueryResultsX()             X-values
    * KDTreeQueryResultsXY()            X- and Y-values
    * KDTreeQueryResultsDistances()     distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreequeryresultstags(kdtree kdt,
        ref int[] tags,
        xparams _params)
    {
        kdtreetsqueryresultstags(kdt, kdt.innerbuf, ref tags, _params);
    }


    /*************************************************************************
    Distances from last query

    This function retuns results stored in  the  internal  buffer  of  kd-tree
    object. If you performed buffered requests (ones which  use  instances  of
    kdtreerequestbuffer class), you  should  call  buffered  version  of  this
    function - kdtreetsqueryresultsdistances().

    INPUT PARAMETERS
        KDT     -   KD-tree
        R       -   possibly pre-allocated buffer. If X is too small to store
                    result, it is resized. If size(X) is enough to store
                    result, it is left unchanged.

    OUTPUT PARAMETERS
        R       -   filled with distances (in corresponding norm)

    NOTES
    1. points are ordered by distance from the query point (first = closest)
    2. if  XY is larger than required to store result, only leading part  will
       be overwritten; trailing part will be left unchanged. So  if  on  input
       XY = [[A,B],[C,D]], and result is [1,2],  then  on  exit  we  will  get
       XY = [[1,2],[C,D]]. This is done purposely to increase performance;  if
       you want function  to  resize  array  according  to  result  size,  use
       function with same name and suffix 'I'.

    SEE ALSO
    * KDTreeQueryResultsX()             X-values
    * KDTreeQueryResultsXY()            X- and Y-values
    * KDTreeQueryResultsTags()          tag values

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreequeryresultsdistances(kdtree kdt,
        ref double[] r,
        xparams _params)
    {
        kdtreetsqueryresultsdistances(kdt, kdt.innerbuf, ref r, _params);
    }


    /*************************************************************************
    X-values from last query associated with kdtreerequestbuffer object.

    INPUT PARAMETERS
        KDT     -   KD-tree
        Buf     -   request  buffer  object  created   for   this   particular
                    instance of kd-tree structure.
        X       -   possibly pre-allocated buffer. If X is too small to store
                    result, it is resized. If size(X) is enough to store
                    result, it is left unchanged.

    OUTPUT PARAMETERS
        X       -   rows are filled with X-values

    NOTES
    1. points are ordered by distance from the query point (first = closest)
    2. if  XY is larger than required to store result, only leading part  will
       be overwritten; trailing part will be left unchanged. So  if  on  input
       XY = [[A,B],[C,D]], and result is [1,2],  then  on  exit  we  will  get
       XY = [[1,2],[C,D]]. This is done purposely to increase performance;  if
       you want function  to  resize  array  according  to  result  size,  use
       function with same name and suffix 'I'.

    SEE ALSO
    * KDTreeQueryResultsXY()            X- and Y-values
    * KDTreeQueryResultsTags()          tag values
    * KDTreeQueryResultsDistances()     distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreetsqueryresultsx(kdtree kdt,
        kdtreerequestbuffer buf,
        ref double[,] x,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int i_ = 0;
        int i1_ = 0;

        if (buf.kcur == 0)
        {
            return;
        }
        if (ap.rows(x) < buf.kcur || ap.cols(x) < kdt.nx)
        {
            x = new double[buf.kcur, kdt.nx];
        }
        k = buf.kcur;
        for (i = 0; i <= k - 1; i++)
        {
            i1_ = (kdt.nx) - (0);
            for (i_ = 0; i_ <= kdt.nx - 1; i_++)
            {
                x[i, i_] = kdt.xy[buf.idx[i], i_ + i1_];
            }
        }
    }


    /*************************************************************************
    X- and Y-values from last query associated with kdtreerequestbuffer object.

    INPUT PARAMETERS
        KDT     -   KD-tree
        Buf     -   request  buffer  object  created   for   this   particular
                    instance of kd-tree structure.
        XY      -   possibly pre-allocated buffer. If XY is too small to store
                    result, it is resized. If size(XY) is enough to store
                    result, it is left unchanged.

    OUTPUT PARAMETERS
        XY      -   rows are filled with points: first NX columns with
                    X-values, next NY columns - with Y-values.

    NOTES
    1. points are ordered by distance from the query point (first = closest)
    2. if  XY is larger than required to store result, only leading part  will
       be overwritten; trailing part will be left unchanged. So  if  on  input
       XY = [[A,B],[C,D]], and result is [1,2],  then  on  exit  we  will  get
       XY = [[1,2],[C,D]]. This is done purposely to increase performance;  if
       you want function  to  resize  array  according  to  result  size,  use
       function with same name and suffix 'I'.

    SEE ALSO
    * KDTreeQueryResultsX()             X-values
    * KDTreeQueryResultsTags()          tag values
    * KDTreeQueryResultsDistances()     distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreetsqueryresultsxy(kdtree kdt,
        kdtreerequestbuffer buf,
        ref double[,] xy,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int i_ = 0;
        int i1_ = 0;

        if (buf.kcur == 0)
        {
            return;
        }
        if (ap.rows(xy) < buf.kcur || ap.cols(xy) < kdt.nx + kdt.ny)
        {
            xy = new double[buf.kcur, kdt.nx + kdt.ny];
        }
        k = buf.kcur;
        for (i = 0; i <= k - 1; i++)
        {
            i1_ = (kdt.nx) - (0);
            for (i_ = 0; i_ <= kdt.nx + kdt.ny - 1; i_++)
            {
                xy[i, i_] = kdt.xy[buf.idx[i], i_ + i1_];
            }
        }
    }


    /*************************************************************************
    Tags from last query associated with kdtreerequestbuffer object.

    This function retuns results stored in  the  internal  buffer  of  kd-tree
    object. If you performed buffered requests (ones which  use  instances  of
    kdtreerequestbuffer class), you  should  call  buffered  version  of  this
    function - KDTreeTsqueryresultstags().

    INPUT PARAMETERS
        KDT     -   KD-tree
        Buf     -   request  buffer  object  created   for   this   particular
                    instance of kd-tree structure.
        Tags    -   possibly pre-allocated buffer. If X is too small to store
                    result, it is resized. If size(X) is enough to store
                    result, it is left unchanged.

    OUTPUT PARAMETERS
        Tags    -   filled with tags associated with points,
                    or, when no tags were supplied, with zeros

    NOTES
    1. points are ordered by distance from the query point (first = closest)
    2. if  XY is larger than required to store result, only leading part  will
       be overwritten; trailing part will be left unchanged. So  if  on  input
       XY = [[A,B],[C,D]], and result is [1,2],  then  on  exit  we  will  get
       XY = [[1,2],[C,D]]. This is done purposely to increase performance;  if
       you want function  to  resize  array  according  to  result  size,  use
       function with same name and suffix 'I'.

    SEE ALSO
    * KDTreeQueryResultsX()             X-values
    * KDTreeQueryResultsXY()            X- and Y-values
    * KDTreeQueryResultsDistances()     distances

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreetsqueryresultstags(kdtree kdt,
        kdtreerequestbuffer buf,
        ref int[] tags,
        xparams _params)
    {
        int i = 0;
        int k = 0;

        if (buf.kcur == 0)
        {
            return;
        }
        if (ap.len(tags) < buf.kcur)
        {
            tags = new int[buf.kcur];
        }
        k = buf.kcur;
        for (i = 0; i <= k - 1; i++)
        {
            tags[i] = kdt.tags[buf.idx[i]];
        }
    }


    /*************************************************************************
    Distances from last query associated with kdtreerequestbuffer object.

    This function retuns results stored in  the  internal  buffer  of  kd-tree
    object. If you performed buffered requests (ones which  use  instances  of
    kdtreerequestbuffer class), you  should  call  buffered  version  of  this
    function - KDTreeTsqueryresultsdistances().

    INPUT PARAMETERS
        KDT     -   KD-tree
        Buf     -   request  buffer  object  created   for   this   particular
                    instance of kd-tree structure.
        R       -   possibly pre-allocated buffer. If X is too small to store
                    result, it is resized. If size(X) is enough to store
                    result, it is left unchanged.

    OUTPUT PARAMETERS
        R       -   filled with distances (in corresponding norm)

    NOTES
    1. points are ordered by distance from the query point (first = closest)
    2. if  XY is larger than required to store result, only leading part  will
       be overwritten; trailing part will be left unchanged. So  if  on  input
       XY = [[A,B],[C,D]], and result is [1,2],  then  on  exit  we  will  get
       XY = [[1,2],[C,D]]. This is done purposely to increase performance;  if
       you want function  to  resize  array  according  to  result  size,  use
       function with same name and suffix 'I'.

    SEE ALSO
    * KDTreeQueryResultsX()             X-values
    * KDTreeQueryResultsXY()            X- and Y-values
    * KDTreeQueryResultsTags()          tag values

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreetsqueryresultsdistances(kdtree kdt,
        kdtreerequestbuffer buf,
        ref double[] r,
        xparams _params)
    {
        int i = 0;
        int k = 0;

        if (buf.kcur == 0)
        {
            return;
        }
        if (ap.len(r) < buf.kcur)
        {
            r = new double[buf.kcur];
        }
        k = buf.kcur;

        //
        // unload norms
        //
        // Abs() call is used to handle cases with negative norms
        // (generated during KFN requests)
        //
        if (kdt.normtype == 0)
        {
            for (i = 0; i <= k - 1; i++)
            {
                r[i] = Math.Abs(buf.r[i]);
            }
        }
        if (kdt.normtype == 1)
        {
            for (i = 0; i <= k - 1; i++)
            {
                r[i] = Math.Abs(buf.r[i]);
            }
        }
        if (kdt.normtype == 2)
        {
            for (i = 0; i <= k - 1; i++)
            {
                r[i] = Math.Sqrt(Math.Abs(buf.r[i]));
            }
        }
    }


    /*************************************************************************
    X-values from last query; 'interactive' variant for languages like  Python
    which   support    constructs   like  "X = KDTreeQueryResultsXI(KDT)"  and
    interactive mode of interpreter.

    This function allocates new array on each call,  so  it  is  significantly
    slower than its 'non-interactive' counterpart, but it is  more  convenient
    when you call it from command line.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreequeryresultsxi(kdtree kdt,
        ref double[,] x,
        xparams _params)
    {
        x = new double[0, 0];

        kdtreequeryresultsx(kdt, ref x, _params);
    }


    /*************************************************************************
    XY-values from last query; 'interactive' variant for languages like Python
    which   support    constructs   like "XY = KDTreeQueryResultsXYI(KDT)" and
    interactive mode of interpreter.

    This function allocates new array on each call,  so  it  is  significantly
    slower than its 'non-interactive' counterpart, but it is  more  convenient
    when you call it from command line.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreequeryresultsxyi(kdtree kdt,
        ref double[,] xy,
        xparams _params)
    {
        xy = new double[0, 0];

        kdtreequeryresultsxy(kdt, ref xy, _params);
    }


    /*************************************************************************
    Tags  from  last  query;  'interactive' variant for languages like  Python
    which  support  constructs  like "Tags = KDTreeQueryResultsTagsI(KDT)" and
    interactive mode of interpreter.

    This function allocates new array on each call,  so  it  is  significantly
    slower than its 'non-interactive' counterpart, but it is  more  convenient
    when you call it from command line.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreequeryresultstagsi(kdtree kdt,
        ref int[] tags,
        xparams _params)
    {
        tags = new int[0];

        kdtreequeryresultstags(kdt, ref tags, _params);
    }


    /*************************************************************************
    Distances from last query; 'interactive' variant for languages like Python
    which  support  constructs   like  "R = KDTreeQueryResultsDistancesI(KDT)"
    and interactive mode of interpreter.

    This function allocates new array on each call,  so  it  is  significantly
    slower than its 'non-interactive' counterpart, but it is  more  convenient
    when you call it from command line.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreequeryresultsdistancesi(kdtree kdt,
        ref double[] r,
        xparams _params)
    {
        r = new double[0];

        kdtreequeryresultsdistances(kdt, ref r, _params);
    }


    /*************************************************************************
    It is informational function which returns bounding box for entire dataset.
    This function is not visible to ALGLIB users, only ALGLIB itself  may  use
    it.

    This function assumes that output buffers are preallocated by caller.

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreeexplorebox(kdtree kdt,
        ref double[] boxmin,
        ref double[] boxmax,
        xparams _params)
    {
        int i = 0;

        apserv.rvectorsetlengthatleast(ref boxmin, kdt.nx, _params);
        apserv.rvectorsetlengthatleast(ref boxmax, kdt.nx, _params);
        for (i = 0; i <= kdt.nx - 1; i++)
        {
            boxmin[i] = kdt.boxmin[i];
            boxmax[i] = kdt.boxmax[i];
        }
    }


    /*************************************************************************
    It is informational function which allows to get  information  about  node
    type. Node index is given by integer value, with 0  corresponding  to root
    node and other node indexes obtained via exploration.

    You should not expect that serialization/unserialization will retain  node
    indexes. You should keep in  mind  that  future  versions  of  ALGLIB  may
    introduce new node types.

    OUTPUT VALUES:
        NodeType    -   node type:
                        * 0 corresponds to leaf node, which can be explored by
                          kdtreeexploreleaf() function
                        * 1 corresponds to split node, which can  be  explored
                          by kdtreeexploresplit() function

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreeexplorenodetype(kdtree kdt,
        int node,
        ref int nodetype,
        xparams _params)
    {
        nodetype = 0;

        ap.assert(node >= 0, "KDTreeExploreNodeType: incorrect node");
        ap.assert(node < ap.len(kdt.nodes), "KDTreeExploreNodeType: incorrect node");
        if (kdt.nodes[node] > 0)
        {

            //
            // Leaf node
            //
            nodetype = 0;
            return;
        }
        if (kdt.nodes[node] == 0)
        {

            //
            // Split node
            //
            nodetype = 1;
            return;
        }
        ap.assert(false, "KDTreeExploreNodeType: integrity check failure");
    }


    /*************************************************************************
    It is informational function which allows to get  information  about  leaf
    node. Node index is given by integer value, with 0  corresponding  to root
    node and other node indexes obtained via exploration.

    You should not expect that serialization/unserialization will retain  node
    indexes. You should keep in  mind  that  future  versions  of  ALGLIB  may
    introduce new node types.

    OUTPUT VALUES:
        XT      -   output buffer is reallocated (if too small) and filled by
                    XY values
        K       -   number of rows in XY

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreeexploreleaf(kdtree kdt,
        int node,
        ref double[,] xy,
        ref int k,
        xparams _params)
    {
        int offs = 0;
        int i = 0;
        int j = 0;

        k = 0;

        ap.assert(node >= 0, "KDTreeExploreLeaf: incorrect node index");
        ap.assert(node + 1 < ap.len(kdt.nodes), "KDTreeExploreLeaf: incorrect node index");
        ap.assert(kdt.nodes[node] > 0, "KDTreeExploreLeaf: incorrect node index");
        k = kdt.nodes[node];
        offs = kdt.nodes[node + 1];
        ap.assert(offs >= 0, "KDTreeExploreLeaf: integrity error");
        ap.assert(offs + k - 1 < ap.rows(kdt.xy), "KDTreeExploreLeaf: integrity error");
        apserv.rmatrixsetlengthatleast(ref xy, k, kdt.nx + kdt.ny, _params);
        for (i = 0; i <= k - 1; i++)
        {
            for (j = 0; j <= kdt.nx + kdt.ny - 1; j++)
            {
                xy[i, j] = kdt.xy[offs + i, kdt.nx + j];
            }
        }
    }


    /*************************************************************************
    It is informational function which allows to get  information  about split
    node. Node index is given by integer value, with 0  corresponding  to root
    node and other node indexes obtained via exploration.

    You should not expect that serialization/unserialization will retain  node
    indexes. You should keep in  mind  that  future  versions  of  ALGLIB  may
    introduce new node types.

    OUTPUT VALUES:
        XT      -   output buffer is reallocated (if too small) and filled by
                    XY values
        K       -   number of rows in XY

        //      Nodes[idx+1]=dim    dimension to split
        //      Nodes[idx+2]=offs   offset of splitting point in Splits[]
        //      Nodes[idx+3]=left   position of left child in Nodes[]
        //      Nodes[idx+4]=right  position of right child in Nodes[]
        
      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreeexploresplit(kdtree kdt,
        int node,
        ref int d,
        ref double s,
        ref int nodele,
        ref int nodege,
        xparams _params)
    {
        d = 0;
        s = 0;
        nodele = 0;
        nodege = 0;

        ap.assert(node >= 0, "KDTreeExploreSplit: incorrect node index");
        ap.assert(node + 4 < ap.len(kdt.nodes), "KDTreeExploreSplit: incorrect node index");
        ap.assert(kdt.nodes[node] == 0, "KDTreeExploreSplit: incorrect node index");
        d = kdt.nodes[node + 1];
        s = kdt.splits[kdt.nodes[node + 2]];
        nodele = kdt.nodes[node + 3];
        nodege = kdt.nodes[node + 4];
        ap.assert(d >= 0, "KDTreeExploreSplit: integrity failure");
        ap.assert(d < kdt.nx, "KDTreeExploreSplit: integrity failure");
        ap.assert(math.isfinite(s), "KDTreeExploreSplit: integrity failure");
        ap.assert(nodele >= 0, "KDTreeExploreSplit: integrity failure");
        ap.assert(nodele < ap.len(kdt.nodes), "KDTreeExploreSplit: integrity failure");
        ap.assert(nodege >= 0, "KDTreeExploreSplit: integrity failure");
        ap.assert(nodege < ap.len(kdt.nodes), "KDTreeExploreSplit: integrity failure");
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreealloc(serializer s,
        kdtree tree,
        xparams _params)
    {

        //
        // Header
        //
        s.alloc_entry();
        s.alloc_entry();

        //
        // Data
        //
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        apserv.allocrealmatrix(s, tree.xy, -1, -1, _params);
        apserv.allocintegerarray(s, tree.tags, -1, _params);
        apserv.allocrealarray(s, tree.boxmin, -1, _params);
        apserv.allocrealarray(s, tree.boxmax, -1, _params);
        apserv.allocintegerarray(s, tree.nodes, -1, _params);
        apserv.allocrealarray(s, tree.splits, -1, _params);
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreeserialize(serializer s,
        kdtree tree,
        xparams _params)
    {

        //
        // Header
        //
        s.serialize_int(scodes.getkdtreeserializationcode(_params));
        s.serialize_int(kdtreefirstversion);

        //
        // Data
        //
        s.serialize_int(tree.n);
        s.serialize_int(tree.nx);
        s.serialize_int(tree.ny);
        s.serialize_int(tree.normtype);
        apserv.serializerealmatrix(s, tree.xy, -1, -1, _params);
        apserv.serializeintegerarray(s, tree.tags, -1, _params);
        apserv.serializerealarray(s, tree.boxmin, -1, _params);
        apserv.serializerealarray(s, tree.boxmax, -1, _params);
        apserv.serializeintegerarray(s, tree.nodes, -1, _params);
        apserv.serializerealarray(s, tree.splits, -1, _params);
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void kdtreeunserialize(serializer s,
        kdtree tree,
        xparams _params)
    {
        int i0 = 0;
        int i1 = 0;


        //
        // check correctness of header
        //
        i0 = s.unserialize_int();
        ap.assert(i0 == scodes.getkdtreeserializationcode(_params), "KDTreeUnserialize: stream header corrupted");
        i1 = s.unserialize_int();
        ap.assert(i1 == kdtreefirstversion, "KDTreeUnserialize: stream header corrupted");

        //
        // Unserialize data
        //
        tree.n = s.unserialize_int();
        tree.nx = s.unserialize_int();
        tree.ny = s.unserialize_int();
        tree.normtype = s.unserialize_int();
        apserv.unserializerealmatrix(s, ref tree.xy, _params);
        apserv.unserializeintegerarray(s, ref tree.tags, _params);
        apserv.unserializerealarray(s, ref tree.boxmin, _params);
        apserv.unserializerealarray(s, ref tree.boxmax, _params);
        apserv.unserializeintegerarray(s, ref tree.nodes, _params);
        apserv.unserializerealarray(s, ref tree.splits, _params);
        kdtreecreaterequestbuffer(tree, tree.innerbuf, _params);
    }


    /*************************************************************************
    R-NN query: all points within  R-sphere  centered  at  X,  using  external
    thread-local buffer, sorted by distance between point and X (by ascending)

    You can call this function from multiple threads for same kd-tree instance,
    assuming that different instances of buffer object are passed to different
    threads.

    NOTE: it is also possible to perform undordered queries performed by means
          of kdtreequeryrnnu() and kdtreetsqueryrnnu() functions. Such queries
          are faster because we do not have to use heap structure for sorting.

    INPUT PARAMETERS
        KDT         -   KD-tree
        Buf         -   request buffer  object  created  for  this  particular
                        instance of kd-tree structure with kdtreecreaterequestbuffer()
                        function.
        X           -   point, array[0..NX-1].
        R           -   radius of sphere (in corresponding norm), R>0
        SelfMatch   -   whether self-matches are allowed:
                        * if True, nearest neighbor may be the point itself
                          (if it exists in original dataset)
                        * if False, then only points with non-zero distance
                          are returned
                        * if not given, considered True

    RESULT
        number of neighbors found, >=0

    This  subroutine  performs  query  and  stores  its result in the internal
    structures  of  the  buffer object. You can use following  subroutines  to
    obtain these results (pay attention to "buf" in their names):
    * KDTreeTsQueryResultsX() to get X-values
    * KDTreeTsQueryResultsXY() to get X- and Y-values
    * KDTreeTsQueryResultsTags() to get tag values
    * KDTreeTsQueryResultsDistances() to get distances
        
    IMPORTANT: kd-tree buffer should be used only with  KD-tree  object  which
               was used to initialize buffer. Any attempt to use biffer   with
               different object is dangerous - you  may  get  integrity  check
               failure (exception) because sizes of internal arrays do not fit
               to dimensions of KD-tree structure.

      -- ALGLIB --
         Copyright 18.03.2016 by Bochkanov Sergey
    *************************************************************************/
    private static int tsqueryrnn(kdtree kdt,
        kdtreerequestbuffer buf,
        double[] x,
        double r,
        bool selfmatch,
        bool orderedbydist,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int j = 0;


        //
        // Handle special case: KDT.N=0
        //
        if (kdt.n == 0)
        {
            buf.kcur = 0;
            result = 0;
            return result;
        }

        //
        // Check consistency of request buffer
        //
        checkrequestbufferconsistency(kdt, buf, _params);

        //
        // Prepare parameters
        //
        buf.kneeded = 0;
        if (kdt.normtype != 2)
        {
            buf.rneeded = r;
        }
        else
        {
            buf.rneeded = math.sqr(r);
        }
        buf.selfmatch = selfmatch;
        buf.approxf = 1;
        buf.kcur = 0;

        //
        // calculate distance from point to current bounding box
        //
        kdtreeinitbox(kdt, x, buf, _params);

        //
        // call recursive search
        // results are returned as heap
        //
        kdtreequerynnrec(kdt, buf, 0, _params);
        result = buf.kcur;

        //
        // pop from heap to generate ordered representation
        //
        // last element is not pop'ed because it is already in
        // its place
        //
        if (orderedbydist)
        {
            j = buf.kcur;
            for (i = buf.kcur; i >= 2; i--)
            {
                tsort.tagheappopi(ref buf.r, ref buf.idx, ref j, _params);
            }
        }
        return result;
    }


    /*************************************************************************
    Rearranges nodes [I1,I2) using partition in D-th dimension with S as threshold.
    Returns split position I3: [I1,I3) and [I3,I2) are created as result.

    This subroutine doesn't create tree structures, just rearranges nodes.
    *************************************************************************/
    private static void kdtreesplit(kdtree kdt,
        int i1,
        int i2,
        int d,
        double s,
        ref int i3,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int ileft = 0;
        int iright = 0;
        double v = 0;

        i3 = 0;

        ap.assert(kdt.n > 0, "KDTreeSplit: internal error");

        //
        // split XY/Tags in two parts:
        // * [ILeft,IRight] is non-processed part of XY/Tags
        //
        // After cycle is done, we have Ileft=IRight. We deal with
        // this element separately.
        //
        // After this, [I1,ILeft) contains left part, and [ILeft,I2)
        // contains right part.
        //
        ileft = i1;
        iright = i2 - 1;
        while (ileft < iright)
        {
            if (kdt.xy[ileft, d] <= s)
            {

                //
                // XY[ILeft] is on its place.
                // Advance ILeft.
                //
                ileft = ileft + 1;
            }
            else
            {

                //
                // XY[ILeft,..] must be at IRight.
                // Swap and advance IRight.
                //
                for (i = 0; i <= 2 * kdt.nx + kdt.ny - 1; i++)
                {
                    v = kdt.xy[ileft, i];
                    kdt.xy[ileft, i] = kdt.xy[iright, i];
                    kdt.xy[iright, i] = v;
                }
                j = kdt.tags[ileft];
                kdt.tags[ileft] = kdt.tags[iright];
                kdt.tags[iright] = j;
                iright = iright - 1;
            }
        }
        if (kdt.xy[ileft, d] <= s)
        {
            ileft = ileft + 1;
        }
        else
        {
            iright = iright - 1;
        }
        i3 = ileft;
    }


    /*************************************************************************
    Recursive kd-tree generation subroutine.

    PARAMETERS
        KDT         tree
        NodesOffs   unused part of Nodes[] which must be filled by tree
        SplitsOffs  unused part of Splits[]
        I1, I2      points from [I1,I2) are processed
        
    NodesOffs[] and SplitsOffs[] must be large enough.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void kdtreegeneratetreerec(kdtree kdt,
        ref int nodesoffs,
        ref int splitsoffs,
        int i1,
        int i2,
        int maxleafsize,
        xparams _params)
    {
        int n = 0;
        int nx = 0;
        int ny = 0;
        int i = 0;
        int j = 0;
        int oldoffs = 0;
        int i3 = 0;
        int cntless = 0;
        int cntgreater = 0;
        double minv = 0;
        double maxv = 0;
        int minidx = 0;
        int maxidx = 0;
        int d = 0;
        double ds = 0;
        double s = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(kdt.n > 0, "KDTreeGenerateTreeRec: internal error");
        ap.assert(i2 > i1, "KDTreeGenerateTreeRec: internal error");

        //
        // Generate leaf if needed
        //
        if (i2 - i1 <= maxleafsize)
        {
            kdt.nodes[nodesoffs + 0] = i2 - i1;
            kdt.nodes[nodesoffs + 1] = i1;
            nodesoffs = nodesoffs + 2;
            return;
        }

        //
        // Load values for easier access
        //
        nx = kdt.nx;
        ny = kdt.ny;

        //
        // Select dimension to split:
        // * D is a dimension number
        // In case bounding box has zero size, we enforce creation of the leaf node.
        //
        d = 0;
        ds = kdt.innerbuf.curboxmax[0] - kdt.innerbuf.curboxmin[0];
        for (i = 1; i <= nx - 1; i++)
        {
            v = kdt.innerbuf.curboxmax[i] - kdt.innerbuf.curboxmin[i];
            if (v > ds)
            {
                ds = v;
                d = i;
            }
        }
        if ((double)(ds) == (double)(0))
        {
            kdt.nodes[nodesoffs + 0] = i2 - i1;
            kdt.nodes[nodesoffs + 1] = i1;
            nodesoffs = nodesoffs + 2;
            return;
        }

        //
        // Select split position S using sliding midpoint rule,
        // rearrange points into [I1,I3) and [I3,I2).
        //
        // In case all points has same value of D-th component
        // (MinV=MaxV) we enforce D-th dimension of bounding
        // box to become exactly zero and repeat tree construction.
        //
        s = kdt.innerbuf.curboxmin[d] + 0.5 * ds;
        i1_ = (i1) - (0);
        for (i_ = 0; i_ <= i2 - i1 - 1; i_++)
        {
            kdt.innerbuf.buf[i_] = kdt.xy[i_ + i1_, d];
        }
        n = i2 - i1;
        cntless = 0;
        cntgreater = 0;
        minv = kdt.innerbuf.buf[0];
        maxv = kdt.innerbuf.buf[0];
        minidx = i1;
        maxidx = i1;
        for (i = 0; i <= n - 1; i++)
        {
            v = kdt.innerbuf.buf[i];
            if (v < minv)
            {
                minv = v;
                minidx = i1 + i;
            }
            if (v > maxv)
            {
                maxv = v;
                maxidx = i1 + i;
            }
            if (v < s)
            {
                cntless = cntless + 1;
            }
            if (v > s)
            {
                cntgreater = cntgreater + 1;
            }
        }
        if (minv == maxv)
        {

            //
            // In case all points has same value of D-th component
            // (MinV=MaxV) we enforce D-th dimension of bounding
            // box to become exactly zero and repeat tree construction.
            //
            v0 = kdt.innerbuf.curboxmin[d];
            v1 = kdt.innerbuf.curboxmax[d];
            kdt.innerbuf.curboxmin[d] = minv;
            kdt.innerbuf.curboxmax[d] = maxv;
            kdtreegeneratetreerec(kdt, ref nodesoffs, ref splitsoffs, i1, i2, maxleafsize, _params);
            kdt.innerbuf.curboxmin[d] = v0;
            kdt.innerbuf.curboxmax[d] = v1;
            return;
        }
        if (cntless > 0 && cntgreater > 0)
        {

            //
            // normal midpoint split
            //
            kdtreesplit(kdt, i1, i2, d, s, ref i3, _params);
        }
        else
        {

            //
            // sliding midpoint
            //
            if (cntless == 0)
            {

                //
                // 1. move split to MinV,
                // 2. place one point to the left bin (move to I1),
                //    others - to the right bin
                //
                s = minv;
                if (minidx != i1)
                {
                    for (i = 0; i <= 2 * nx + ny - 1; i++)
                    {
                        v = kdt.xy[minidx, i];
                        kdt.xy[minidx, i] = kdt.xy[i1, i];
                        kdt.xy[i1, i] = v;
                    }
                    j = kdt.tags[minidx];
                    kdt.tags[minidx] = kdt.tags[i1];
                    kdt.tags[i1] = j;
                }
                i3 = i1 + 1;
            }
            else
            {

                //
                // 1. move split to MaxV,
                // 2. place one point to the right bin (move to I2-1),
                //    others - to the left bin
                //
                s = maxv;
                if (maxidx != i2 - 1)
                {
                    for (i = 0; i <= 2 * nx + ny - 1; i++)
                    {
                        v = kdt.xy[maxidx, i];
                        kdt.xy[maxidx, i] = kdt.xy[i2 - 1, i];
                        kdt.xy[i2 - 1, i] = v;
                    }
                    j = kdt.tags[maxidx];
                    kdt.tags[maxidx] = kdt.tags[i2 - 1];
                    kdt.tags[i2 - 1] = j;
                }
                i3 = i2 - 1;
            }
        }

        //
        // Generate 'split' node
        //
        kdt.nodes[nodesoffs + 0] = 0;
        kdt.nodes[nodesoffs + 1] = d;
        kdt.nodes[nodesoffs + 2] = splitsoffs;
        kdt.splits[splitsoffs + 0] = s;
        oldoffs = nodesoffs;
        nodesoffs = nodesoffs + splitnodesize;
        splitsoffs = splitsoffs + 1;

        //
        // Recursive generation:
        // * update CurBox
        // * call subroutine
        // * restore CurBox
        //
        kdt.nodes[oldoffs + 3] = nodesoffs;
        v = kdt.innerbuf.curboxmax[d];
        kdt.innerbuf.curboxmax[d] = s;
        kdtreegeneratetreerec(kdt, ref nodesoffs, ref splitsoffs, i1, i3, maxleafsize, _params);
        kdt.innerbuf.curboxmax[d] = v;
        kdt.nodes[oldoffs + 4] = nodesoffs;
        v = kdt.innerbuf.curboxmin[d];
        kdt.innerbuf.curboxmin[d] = s;
        kdtreegeneratetreerec(kdt, ref nodesoffs, ref splitsoffs, i3, i2, maxleafsize, _params);
        kdt.innerbuf.curboxmin[d] = v;

        //
        // Zero-fill unused portions of the node (avoid false warnings by Valgrind
        // about attempt to serialize uninitialized values)
        //
        ap.assert(splitnodesize == 6, "KDTreeGenerateTreeRec: node size has unexpectedly changed");
        kdt.nodes[oldoffs + 5] = 0;
    }


    /*************************************************************************
    Recursive subroutine for NN queries.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void kdtreequerynnrec(kdtree kdt,
        kdtreerequestbuffer buf,
        int offs,
        xparams _params)
    {
        double ptdist = 0;
        int i = 0;
        int j = 0;
        int nx = 0;
        int i1 = 0;
        int i2 = 0;
        int d = 0;
        double s = 0;
        double v = 0;
        double t1 = 0;
        int childbestoffs = 0;
        int childworstoffs = 0;
        int childoffs = 0;
        double prevdist = 0;
        bool todive = new bool();
        bool bestisleft = new bool();
        bool updatemin = new bool();

        ap.assert(kdt.n > 0, "KDTreeQueryNNRec: internal error");

        //
        // Leaf node.
        // Process points.
        //
        if (kdt.nodes[offs] > 0)
        {
            i1 = kdt.nodes[offs + 1];
            i2 = i1 + kdt.nodes[offs];
            for (i = i1; i <= i2 - 1; i++)
            {

                //
                // Calculate distance
                //
                ptdist = 0;
                nx = kdt.nx;
                if (kdt.normtype == 0)
                {
                    for (j = 0; j <= nx - 1; j++)
                    {
                        ptdist = Math.Max(ptdist, Math.Abs(kdt.xy[i, j] - buf.x[j]));
                    }
                }
                if (kdt.normtype == 1)
                {
                    for (j = 0; j <= nx - 1; j++)
                    {
                        ptdist = ptdist + Math.Abs(kdt.xy[i, j] - buf.x[j]);
                    }
                }
                if (kdt.normtype == 2)
                {
                    for (j = 0; j <= nx - 1; j++)
                    {
                        ptdist = ptdist + math.sqr(kdt.xy[i, j] - buf.x[j]);
                    }
                }

                //
                // Skip points with zero distance if self-matches are turned off
                //
                if (ptdist == 0 && !buf.selfmatch)
                {
                    continue;
                }

                //
                // We CAN'T process point if R-criterion isn't satisfied,
                // i.e. (RNeeded<>0) AND (PtDist>R).
                //
                if (buf.rneeded == 0 || ptdist <= buf.rneeded)
                {

                    //
                    // R-criterion is satisfied, we must either:
                    // * replace worst point, if (KNeeded<>0) AND (KCur=KNeeded)
                    //   (or skip, if worst point is better)
                    // * add point without replacement otherwise
                    //
                    if (buf.kcur < buf.kneeded || buf.kneeded == 0)
                    {

                        //
                        // add current point to heap without replacement
                        //
                        tsort.tagheappushi(ref buf.r, ref buf.idx, ref buf.kcur, ptdist, i, _params);
                    }
                    else
                    {

                        //
                        // New points are added or not, depending on their distance.
                        // If added, they replace element at the top of the heap
                        //
                        if (ptdist < buf.r[0])
                        {
                            if (buf.kneeded == 1)
                            {
                                buf.idx[0] = i;
                                buf.r[0] = ptdist;
                            }
                            else
                            {
                                tsort.tagheapreplacetopi(ref buf.r, ref buf.idx, buf.kneeded, ptdist, i, _params);
                            }
                        }
                    }
                }
            }
            return;
        }

        //
        // Simple split
        //
        if (kdt.nodes[offs] == 0)
        {

            //
            // Load:
            // * D  dimension to split
            // * S  split position
            //
            d = kdt.nodes[offs + 1];
            s = kdt.splits[kdt.nodes[offs + 2]];

            //
            // Calculate:
            // * ChildBestOffs      child box with best chances
            // * ChildWorstOffs     child box with worst chances
            //
            if (buf.x[d] <= s)
            {
                childbestoffs = kdt.nodes[offs + 3];
                childworstoffs = kdt.nodes[offs + 4];
                bestisleft = true;
            }
            else
            {
                childbestoffs = kdt.nodes[offs + 4];
                childworstoffs = kdt.nodes[offs + 3];
                bestisleft = false;
            }

            //
            // Navigate through childs
            //
            for (i = 0; i <= 1; i++)
            {

                //
                // Select child to process:
                // * ChildOffs      current child offset in Nodes[]
                // * UpdateMin      whether minimum or maximum value
                //                  of bounding box is changed on update
                //
                if (i == 0)
                {
                    childoffs = childbestoffs;
                    updatemin = !bestisleft;
                }
                else
                {
                    updatemin = bestisleft;
                    childoffs = childworstoffs;
                }

                //
                // Update bounding box and current distance
                //
                if (updatemin)
                {
                    prevdist = buf.curdist;
                    t1 = buf.x[d];
                    v = buf.curboxmin[d];
                    if (t1 <= s)
                    {
                        if (kdt.normtype == 0)
                        {
                            buf.curdist = Math.Max(buf.curdist, s - t1);
                        }
                        if (kdt.normtype == 1)
                        {
                            buf.curdist = buf.curdist - Math.Max(v - t1, 0) + s - t1;
                        }
                        if (kdt.normtype == 2)
                        {
                            buf.curdist = buf.curdist - math.sqr(Math.Max(v - t1, 0)) + math.sqr(s - t1);
                        }
                    }
                    buf.curboxmin[d] = s;
                }
                else
                {
                    prevdist = buf.curdist;
                    t1 = buf.x[d];
                    v = buf.curboxmax[d];
                    if (t1 >= s)
                    {
                        if (kdt.normtype == 0)
                        {
                            buf.curdist = Math.Max(buf.curdist, t1 - s);
                        }
                        if (kdt.normtype == 1)
                        {
                            buf.curdist = buf.curdist - Math.Max(t1 - v, 0) + t1 - s;
                        }
                        if (kdt.normtype == 2)
                        {
                            buf.curdist = buf.curdist - math.sqr(Math.Max(t1 - v, 0)) + math.sqr(t1 - s);
                        }
                    }
                    buf.curboxmax[d] = s;
                }

                //
                // Decide: to dive into cell or not to dive
                //
                if (buf.rneeded != 0 && buf.curdist > buf.rneeded)
                {
                    todive = false;
                }
                else
                {
                    if (buf.kcur < buf.kneeded || buf.kneeded == 0)
                    {

                        //
                        // KCur<KNeeded (i.e. not all points are found)
                        //
                        todive = true;
                    }
                    else
                    {

                        //
                        // KCur=KNeeded, decide to dive or not to dive
                        // using point position relative to bounding box.
                        //
                        todive = buf.curdist <= buf.r[0] * buf.approxf;
                    }
                }
                if (todive)
                {
                    kdtreequerynnrec(kdt, buf, childoffs, _params);
                }

                //
                // Restore bounding box and distance
                //
                if (updatemin)
                {
                    buf.curboxmin[d] = v;
                }
                else
                {
                    buf.curboxmax[d] = v;
                }
                buf.curdist = prevdist;
            }
            return;
        }
    }


    /*************************************************************************
    Recursive subroutine for box queries.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void kdtreequeryboxrec(kdtree kdt,
        kdtreerequestbuffer buf,
        int offs,
        xparams _params)
    {
        bool inbox = new bool();
        int nx = 0;
        int i1 = 0;
        int i2 = 0;
        int i = 0;
        int j = 0;
        int d = 0;
        double s = 0;
        double v = 0;

        ap.assert(kdt.n > 0, "KDTreeQueryBoxRec: internal error");
        nx = kdt.nx;

        //
        // Check that intersection of query box with bounding box is non-empty.
        // This check is performed once for Offs=0 (tree root).
        //
        if (offs == 0)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                if (buf.boxmin[j] > buf.curboxmax[j])
                {
                    return;
                }
                if (buf.boxmax[j] < buf.curboxmin[j])
                {
                    return;
                }
            }
        }

        //
        // Leaf node.
        // Process points.
        //
        if (kdt.nodes[offs] > 0)
        {
            i1 = kdt.nodes[offs + 1];
            i2 = i1 + kdt.nodes[offs];
            for (i = i1; i <= i2 - 1; i++)
            {

                //
                // Check whether point is in box or not
                //
                inbox = true;
                for (j = 0; j <= nx - 1; j++)
                {
                    inbox = inbox && kdt.xy[i, j] >= buf.boxmin[j];
                    inbox = inbox && kdt.xy[i, j] <= buf.boxmax[j];
                }
                if (!inbox)
                {
                    continue;
                }

                //
                // Add point to unordered list
                //
                buf.r[buf.kcur] = 0.0;
                buf.idx[buf.kcur] = i;
                buf.kcur = buf.kcur + 1;
            }
            return;
        }

        //
        // Simple split
        //
        if (kdt.nodes[offs] == 0)
        {

            //
            // Load:
            // * D  dimension to split
            // * S  split position
            //
            d = kdt.nodes[offs + 1];
            s = kdt.splits[kdt.nodes[offs + 2]];

            //
            // Check lower split (S is upper bound of new bounding box)
            //
            if (s >= buf.boxmin[d])
            {
                v = buf.curboxmax[d];
                buf.curboxmax[d] = s;
                kdtreequeryboxrec(kdt, buf, kdt.nodes[offs + 3], _params);
                buf.curboxmax[d] = v;
            }

            //
            // Check upper split (S is lower bound of new bounding box)
            //
            if (s <= buf.boxmax[d])
            {
                v = buf.curboxmin[d];
                buf.curboxmin[d] = s;
                kdtreequeryboxrec(kdt, buf, kdt.nodes[offs + 4], _params);
                buf.curboxmin[d] = v;
            }
            return;
        }
    }


    /*************************************************************************
    Copies X[] to Buf.X[]
    Loads distance from X[] to bounding box.
    Initializes Buf.CurBox[].

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void kdtreeinitbox(kdtree kdt,
        double[] x,
        kdtreerequestbuffer buf,
        xparams _params)
    {
        int i = 0;
        double vx = 0;
        double vmin = 0;
        double vmax = 0;

        ap.assert(kdt.n > 0, "KDTreeInitBox: internal error");

        //
        // calculate distance from point to current bounding box
        //
        buf.curdist = 0;
        if (kdt.normtype == 0)
        {
            for (i = 0; i <= kdt.nx - 1; i++)
            {
                vx = x[i];
                vmin = kdt.boxmin[i];
                vmax = kdt.boxmax[i];
                buf.x[i] = vx;
                buf.curboxmin[i] = vmin;
                buf.curboxmax[i] = vmax;
                if (vx < vmin)
                {
                    buf.curdist = Math.Max(buf.curdist, vmin - vx);
                }
                else
                {
                    if (vx > vmax)
                    {
                        buf.curdist = Math.Max(buf.curdist, vx - vmax);
                    }
                }
            }
        }
        if (kdt.normtype == 1)
        {
            for (i = 0; i <= kdt.nx - 1; i++)
            {
                vx = x[i];
                vmin = kdt.boxmin[i];
                vmax = kdt.boxmax[i];
                buf.x[i] = vx;
                buf.curboxmin[i] = vmin;
                buf.curboxmax[i] = vmax;
                if (vx < vmin)
                {
                    buf.curdist = buf.curdist + vmin - vx;
                }
                else
                {
                    if (vx > vmax)
                    {
                        buf.curdist = buf.curdist + vx - vmax;
                    }
                }
            }
        }
        if (kdt.normtype == 2)
        {
            for (i = 0; i <= kdt.nx - 1; i++)
            {
                vx = x[i];
                vmin = kdt.boxmin[i];
                vmax = kdt.boxmax[i];
                buf.x[i] = vx;
                buf.curboxmin[i] = vmin;
                buf.curboxmax[i] = vmax;
                if (vx < vmin)
                {
                    buf.curdist = buf.curdist + math.sqr(vmin - vx);
                }
                else
                {
                    if (vx > vmax)
                    {
                        buf.curdist = buf.curdist + math.sqr(vx - vmax);
                    }
                }
            }
        }
    }


    /*************************************************************************
    This function allocates all dataset-independend array  fields  of  KDTree,
    i.e.  such  array  fields  that  their dimensions do not depend on dataset
    size.

    This function do not sets KDT.NX or KDT.NY - it just allocates arrays

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    private static void kdtreeallocdatasetindependent(kdtree kdt,
        int nx,
        int ny,
        xparams _params)
    {
        ap.assert(kdt.n > 0, "KDTreeAllocDatasetIndependent: internal error");
        kdt.boxmin = new double[nx];
        kdt.boxmax = new double[nx];
    }


    /*************************************************************************
    This function allocates all dataset-dependent array fields of KDTree, i.e.
    such array fields that their dimensions depend on dataset size.

    This function do not sets KDT.N, KDT.NX or KDT.NY -
    it just allocates arrays.

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    private static void kdtreeallocdatasetdependent(kdtree kdt,
        int n,
        int nx,
        int ny,
        xparams _params)
    {
        ap.assert(n > 0, "KDTreeAllocDatasetDependent: internal error");
        kdt.xy = new double[n, 2 * nx + ny];
        kdt.tags = new int[n];
        kdt.nodes = new int[splitnodesize * 2 * n];
        kdt.splits = new double[2 * n];
    }


    /*************************************************************************
    This  function   checks  consistency  of  request  buffer  structure  with
    dimensions of kd-tree object.

      -- ALGLIB --
         Copyright 02.04.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void checkrequestbufferconsistency(kdtree kdt,
        kdtreerequestbuffer buf,
        xparams _params)
    {
        ap.assert(ap.len(buf.x) >= kdt.nx, "KDTree: dimensions of kdtreerequestbuffer are inconsistent with kdtree structure");
        ap.assert(ap.len(buf.idx) >= kdt.n, "KDTree: dimensions of kdtreerequestbuffer are inconsistent with kdtree structure");
        ap.assert(ap.len(buf.r) >= kdt.n, "KDTree: dimensions of kdtreerequestbuffer are inconsistent with kdtree structure");
        ap.assert(ap.len(buf.buf) >= Math.Max(kdt.n, kdt.nx), "KDTree: dimensions of kdtreerequestbuffer are inconsistent with kdtree structure");
        ap.assert(ap.len(buf.curboxmin) >= kdt.nx, "KDTree: dimensions of kdtreerequestbuffer are inconsistent with kdtree structure");
        ap.assert(ap.len(buf.curboxmax) >= kdt.nx, "KDTree: dimensions of kdtreerequestbuffer are inconsistent with kdtree structure");
    }


}
