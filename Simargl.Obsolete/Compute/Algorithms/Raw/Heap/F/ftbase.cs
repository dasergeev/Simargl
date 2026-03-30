using System;

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

public class ftbase
{
    /*************************************************************************
    This record stores execution plan for the fast transformation  along  with
    preallocated temporary buffers and precalculated values.

    FIELDS:
        Entries         -   plan entries, one row = one entry (see below for
                            description).
        Buf0,Buf1,Buf2  -   global temporary buffers; some of them are allocated,
                            some of them are not (as decided by plan generation
                            subroutine).
        Buffer          -   global buffer whose size is equal to plan size.
                            There is one-to-one correspondence between elements
                            of global buffer and elements of array transformed.
                            Because of it global buffer can be used as temporary
                            thread-safe storage WITHOUT ACQUIRING LOCK - each
                            worker thread works with its part of input array,
                            and each part of input array corresponds to distinct
                            part of buffer.
        
    FORMAT OF THE ENTRIES TABLE:

    Entries table is 2D array which stores one entry per row. Row format is:
        row[0]      operation type:
                    *  0 for "end of plan/subplan"
                    * +1 for "reference O(N^2) complex FFT"
                    * -1 for complex transposition
                    * -2 for multiplication by twiddle factors of complex FFT
                    * -3 for "start of plan/subplan"
        row[1]      repetition count, >=1
        row[2]      base operand size (number of microvectors), >=1
        row[3]      microvector size (measured in real numbers), >=1
        row[4]      parameter0, meaning depends on row[0]
        row[5]      parameter1, meaning depends on row[0]

    FORMAT OF THE DATA:

    Transformation plan works with row[1]*row[2]*row[3]  real  numbers,  which
    are (in most cases) interpreted as sequence of complex numbers. These data
    are grouped as follows:
    * we have row[1] contiguous OPERANDS, which can be treated separately
    * each operand includes row[2] contiguous MICROVECTORS
    * each microvector includes row[3] COMPONENTS, which can be treated separately
    * pair of components form complex number, so in most cases row[3] will be even

    Say, if you want to perform complex FFT of length 3, then:
    * you have 1 operand: row[1]=1
    * operand consists of 3 microvectors:   row[2]=3
    * each microvector has two components:  row[3]=2
    * a pair of subsequent components is treated as complex number

    if you want to perform TWO simultaneous complex FFT's of length 3, then you
    can choose between two representations:
    * 1 operand, 3 microvectors, 4 components; storage format is given below:
      [ A0X A0Y B0X B0Y A1X A1Y B1X B1Y ... ]
      (here A denotes first sequence, B - second one).
    * 2 operands, 3 microvectors, 2 components; storage format is given below:
      [ A0X A0Y A1X A2Y ... B0X B0Y B1X B1Y ... ]
    Most FFT operations are supported only for the second format, but you
    should remember that first format sometimes can be used too.

    SUPPORTED OPERATIONS:

    row[0]=0:
    * "end of plan/subplan"
    * in case we meet entry with such type,  FFT  transformation  is  finished
      (or we return from recursive FFT subplan, in case it was subplan).

    row[0]=+1:
    * "reference 1D complex FFT"
    * we perform reference O(N^2) complex FFT on input data, which are treated
      as row[1] arrays, each of row[2] complex numbers, and row[3] must be
      equal to 2
    * transformation is performed using temporary buffer

    row[0]=opBluesteinsFFT:
    * input array is handled with Bluestein's algorithm (by zero-padding to
      Param0 complex numbers).
    * this plan calls Param0-point subplan which is located at offset Param1
      (offset is measured with respect to location of the calling entry)
    * this plan uses precomputed quantities stored in Plan.PrecR at
      offset Param2.
    * transformation is performed using 4 temporary buffers, which are
      retrieved from Plan.BluesteinPool.

    row[0]=+3:
    * "optimized 1D complex FFT"
    * this function supports only several operand sizes: from 1 to 5.
      These transforms are hard-coded and performed very efficiently

    row[0]=opRadersFFT:
    * input array is handled with Rader's algorithm (permutation and
      reduction to N-1-point FFT)
    * this plan calls N-1-point subplan which is located at offset Param0
      (offset is measured with respect to location of the calling entry)
    * this plan uses precomputed primitive root and its inverse (modulo N)
      which are stored in Param1 and Param2.
    * Param3 stores offset of the precomputed data for the plan
    * plan length must be prime, (N-1)*(N-1) must fit into integer variable

    row[0]=-1
    * "complex transposition"
    * input data are treated as row[1] independent arrays, which are processed
      separately
    * each of operands is treated as matrix with row[4] rows and row[2]/row[4]
      columns. Each element of the matrix is microvector with row[3] components.
    * transposition is performed using temporary buffer

    row[0]=-2
    * "multiplication by twiddle factors of complex FFT"
    * input data are treated as row[1] independent arrays, which are processed
      separately
    * row[4] contains N1 - length of the "first FFT"  in  a  Cooley-Tukey  FFT
      algorithm
    * this function does not require temporary buffers

    row[0]=-3
    * "start of the plan"
    * each subplan must start from this entry
    * param0 is ignored
    * param1 stores approximate (optimistic) estimate of KFLOPs required to
      transform one operand of the plan. Total cost of the plan is approximately
      equal to row[1]*param1 KFLOPs.
    * this function does not require temporary buffers

    row[0]=-4
    * "jump"
    * param0 stores relative offset of the jump site
      (+1 corresponds to the next entry)

    row[0]=-5
    * "parallel call"
    * input data are treated as row[1] independent arrays
    * child subplan is applied independently for each of arrays - row[1] times
    * subplan length must be equal to row[2]*row[3]
    * param0 stores relative offset of the child subplan site
      (+1 corresponds to the next entry)
    * param1 stores approximate total cost of plan, measured in UNITS
      (1 UNIT = 100 KFLOPs). Plan cost must be rounded DOWN to nearest integer.


        
    TODO 
         2. from KFLOPs to UNITs, 1 UNIT = 100 000 FLOP!!!!!!!!!!!

         3. from IsRoot to TaskType = {0, -1, +1}; or maybe, add IsSeparatePlan
            to distinguish root of child subplan from global root which uses
            separate buffer
            
         4. child subplans in parallel call must NOT use buffer provided by parent plan;
            they must allocate their own local buffer
    *************************************************************************/
    public class fasttransformplan : apobject
    {
        public int[,] entries;
        public double[] buffer;
        public double[] precr;
        public double[] preci;
        public smp.shared_pool bluesteinpool;
        public fasttransformplan()
        {
            init();
        }
        public override void init()
        {
            entries = new int[0, 0];
            buffer = new double[0];
            precr = new double[0];
            preci = new double[0];
            bluesteinpool = new smp.shared_pool();
        }
        public override apobject make_copy()
        {
            fasttransformplan _result = new fasttransformplan();
            _result.entries = (int[,])entries.Clone();
            _result.buffer = (double[])buffer.Clone();
            _result.precr = (double[])precr.Clone();
            _result.preci = (double[])preci.Clone();
            _result.bluesteinpool = (smp.shared_pool)bluesteinpool.make_copy();
            return _result;
        }
    };




    public const int coltype = 0;
    public const int coloperandscnt = 1;
    public const int coloperandsize = 2;
    public const int colmicrovectorsize = 3;
    public const int colparam0 = 4;
    public const int colparam1 = 5;
    public const int colparam2 = 6;
    public const int colparam3 = 7;
    public const int colscnt = 8;
    public const int opend = 0;
    public const int opcomplexreffft = 1;
    public const int opbluesteinsfft = 2;
    public const int opcomplexcodeletfft = 3;
    public const int opcomplexcodelettwfft = 4;
    public const int opradersfft = 5;
    public const int opcomplextranspose = -1;
    public const int opcomplexfftfactors = -2;
    public const int opstart = -3;
    public const int opjmp = -4;
    public const int opparallelcall = -5;
    public const int maxradix = 6;
    public const int updatetw = 16;
    public const int recursivethreshold = 1024;
    public const int raderthreshold = 19;
    public const int ftbasecodeletrecommended = 5;
    public const double ftbaseinefficiencyfactor = 1.3;
    public const int ftbasemaxsmoothfactor = 5;


    /*************************************************************************
    This subroutine generates FFT plan for K complex FFT's with length N each.

    INPUT PARAMETERS:
        N           -   FFT length (in complex numbers), N>=1
        K           -   number of repetitions, K>=1
        
    OUTPUT PARAMETERS:
        Plan        -   plan

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void ftcomplexfftplan(int n,
        int k,
        fasttransformplan plan,
        xparams _params)
    {
        apserv.srealarray bluesteinbuf = new apserv.srealarray();
        int rowptr = 0;
        int bluesteinsize = 0;
        int precrptr = 0;
        int preciptr = 0;
        int precrsize = 0;
        int precisize = 0;


        //
        // Initial check for parameters
        //
        ap.assert(n > 0, "FTComplexFFTPlan: N<=0");
        ap.assert(k > 0, "FTComplexFFTPlan: K<=0");

        //
        // Determine required sizes of precomputed real and integer
        // buffers. This stage of code is highly dependent on internals
        // of FTComplexFFTPlanRec() and must be kept synchronized with
        // possible changes in internals of plan generation function.
        //
        // Buffer size is determined as follows:
        // * N is factorized
        // * we factor out anything which is less or equal to MaxRadix
        // * prime factor F>RaderThreshold requires 4*FTBaseFindSmooth(2*F-1)
        //   real entries to store precomputed Quantities for Bluestein's
        //   transformation
        // * prime factor F<=RaderThreshold does NOT require
        //   precomputed storage
        //
        precrsize = 0;
        precisize = 0;
        ftdeterminespacerequirements(n, ref precrsize, ref precisize, _params);
        if (precrsize > 0)
        {
            plan.precr = new double[precrsize];
        }
        if (precisize > 0)
        {
            plan.preci = new double[precisize];
        }

        //
        // Generate plan
        //
        rowptr = 0;
        precrptr = 0;
        preciptr = 0;
        bluesteinsize = 1;
        plan.buffer = new double[2 * n * k];
        ftcomplexfftplanrec(n, k, true, true, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
        bluesteinbuf.val = new double[bluesteinsize];
        smp.ae_shared_pool_set_seed(plan.bluesteinpool, bluesteinbuf);

        //
        // Check that actual amount of precomputed space used by transformation
        // plan is EXACTLY equal to amount of space allocated by us.
        //
        ap.assert(precrptr == precrsize, "FTComplexFFTPlan: internal error (PrecRPtr<>PrecRSize)");
        ap.assert(preciptr == precisize, "FTComplexFFTPlan: internal error (PrecRPtr<>PrecRSize)");
    }


    /*************************************************************************
    This subroutine applies transformation plan to input/output array A.

    INPUT PARAMETERS:
        Plan        -   transformation plan
        A           -   array, must be large enough for plan to work
        OffsA       -   offset of the subarray to process
        RepCnt      -   repetition count (transformation is repeatedly applied
                        to subsequent subarrays)
        
    OUTPUT PARAMETERS:
        Plan        -   plan (temporary buffers can be modified, plan itself
                        is unchanged and can be reused)
        A           -   transformed array

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void ftapplyplan(fasttransformplan plan,
        double[] a,
        int offsa,
        int repcnt,
        xparams _params)
    {
        int plansize = 0;
        int i = 0;

        plansize = plan.entries[0, coloperandscnt] * plan.entries[0, coloperandsize] * plan.entries[0, colmicrovectorsize];
        for (i = 0; i <= repcnt - 1; i++)
        {
            ftapplysubplan(plan, 0, a, offsa + plansize * i, 0, plan.buffer, 1, _params);
        }
    }


    /*************************************************************************
    Returns good factorization N=N1*N2.

    Usually N1<=N2 (but not always - small N's may be exception).
    if N1<>1 then N2<>1.

    Factorization is chosen depending on task type and codelets we have.

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void ftbasefactorize(int n,
        int tasktype,
        ref int n1,
        ref int n2,
        xparams _params)
    {
        int j = 0;

        n1 = 0;
        n2 = 0;

        n1 = 0;
        n2 = 0;

        //
        // try to find good codelet
        //
        if (n1 * n2 != n)
        {
            for (j = ftbasecodeletrecommended; j >= 2; j--)
            {
                if (n % j == 0)
                {
                    n1 = j;
                    n2 = n / j;
                    break;
                }
            }
        }

        //
        // try to factorize N
        //
        if (n1 * n2 != n)
        {
            for (j = ftbasecodeletrecommended + 1; j <= n - 1; j++)
            {
                if (n % j == 0)
                {
                    n1 = j;
                    n2 = n / j;
                    break;
                }
            }
        }

        //
        // looks like N is prime :(
        //
        if (n1 * n2 != n)
        {
            n1 = 1;
            n2 = n;
        }

        //
        // normalize
        //
        if (n2 == 1 && n1 != 1)
        {
            n2 = n1;
            n1 = 1;
        }
    }


    /*************************************************************************
    Is number smooth?

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool ftbaseissmooth(int n,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;

        for (i = 2; i <= ftbasemaxsmoothfactor; i++)
        {
            while (n % i == 0)
            {
                n = n / i;
            }
        }
        result = n == 1;
        return result;
    }


    /*************************************************************************
    Returns smallest smooth (divisible only by 2, 3, 5) number that is greater
    than or equal to max(N,2)

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    public static int ftbasefindsmooth(int n,
        xparams _params)
    {
        int result = 0;
        int best = 0;

        best = 2;
        while (best < n)
        {
            best = 2 * best;
        }
        ftbasefindsmoothrec(n, 1, 2, ref best, _params);
        result = best;
        return result;
    }


    /*************************************************************************
    Returns  smallest  smooth  (divisible only by 2, 3, 5) even number that is
    greater than or equal to max(N,2)

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    public static int ftbasefindsmootheven(int n,
        xparams _params)
    {
        int result = 0;
        int best = 0;

        best = 2;
        while (best < n)
        {
            best = 2 * best;
        }
        ftbasefindsmoothrec(n, 2, 2, ref best, _params);
        result = best;
        return result;
    }


    /*************************************************************************
    Returns estimate of FLOP count for the FFT.

    It is only an estimate based on operations count for the PERFECT FFT
    and relative inefficiency of the algorithm actually used.

    N should be power of 2, estimates are badly wrong for non-power-of-2 N's.

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double ftbasegetflopestimate(int n,
        xparams _params)
    {
        double result = 0;

        result = ftbaseinefficiencyfactor * (4 * n * Math.Log(n) / Math.Log(2) - 6 * n + 8);
        return result;
    }


    /*************************************************************************
    This function returns EXACT estimate of the space requirements for N-point
    FFT. Internals of this function are highly dependent on details of different
    FFTs employed by this unit, so every time algorithm is changed this function
    has to be rewritten.

    INPUT PARAMETERS:
        N           -   transform length
        PrecRSize   -   must be set to zero
        PrecISize   -   must be set to zero
        
    OUTPUT PARAMETERS:
        PrecRSize   -   number of real temporaries required for transformation
        PrecISize   -   number of integer temporaries required for transformation

        
      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftdeterminespacerequirements(int n,
        ref int precrsize,
        ref int precisize,
        xparams _params)
    {
        int ncur = 0;
        int f = 0;
        int i = 0;


        //
        // Determine required sizes of precomputed real and integer
        // buffers. This stage of code is highly dependent on internals
        // of FTComplexFFTPlanRec() and must be kept synchronized with
        // possible changes in internals of plan generation function.
        //
        // Buffer size is determined as follows:
        // * N is factorized
        // * we factor out anything which is less or equal to MaxRadix
        // * prime factor F>RaderThreshold requires 4*FTBaseFindSmooth(2*F-1)
        //   real entries to store precomputed Quantities for Bluestein's
        //   transformation
        // * prime factor F<=RaderThreshold requires 2*(F-1)+ESTIMATE(F-1)
        //   precomputed storage
        //
        ncur = n;
        for (i = 2; i <= maxradix; i++)
        {
            while (ncur % i == 0)
            {
                ncur = ncur / i;
            }
        }
        f = 2;
        while (f <= ncur)
        {
            while (ncur % f == 0)
            {
                if (f > raderthreshold)
                {
                    precrsize = precrsize + 4 * ftbasefindsmooth(2 * f - 1, _params);
                }
                else
                {
                    precrsize = precrsize + 2 * (f - 1);
                    ftdeterminespacerequirements(f - 1, ref precrsize, ref precisize, _params);
                }
                ncur = ncur / f;
            }
            f = f + 1;
        }
    }


    /*************************************************************************
    Recurrent function called by FTComplexFFTPlan() and other functions. It
    recursively builds transformation plan

    INPUT PARAMETERS:
        N           -   FFT length (in complex numbers), N>=1
        K           -   number of repetitions, K>=1
        ChildPlan   -   if True, plan generator inserts OpStart/opEnd in the
                        plan header/footer.
        TopmostPlan -   if True, plan generator assumes that it is topmost plan:
                        * it may use global buffer for transpositions
                        and there is no other plan which executes in parallel
        RowPtr      -   index which points to past-the-last entry generated so far
        BluesteinSize-  amount of storage (in real numbers) required for Bluestein buffer
        PrecRPtr    -   pointer to unused part of precomputed real buffer (Plan.PrecR):
                        * when this function stores some data to precomputed buffer,
                          it advances pointer.
                        * it is responsibility of the function to assert that
                          Plan.PrecR has enough space to store data before actually
                          writing to buffer.
                        * it is responsibility of the caller to allocate enough
                          space before calling this function
        PrecIPtr    -   pointer to unused part of precomputed integer buffer (Plan.PrecI):
                        * when this function stores some data to precomputed buffer,
                          it advances pointer.
                        * it is responsibility of the function to assert that
                          Plan.PrecR has enough space to store data before actually
                          writing to buffer.
                        * it is responsibility of the caller to allocate enough
                          space before calling this function
        Plan        -   plan (generated so far)
        
    OUTPUT PARAMETERS:
        RowPtr      -   updated pointer (advanced by number of entries generated
                        by function)
        BluesteinSize-  updated amount
                        (may be increased, but may never be decreased)
            
    NOTE: in case TopmostPlan is True, ChildPlan is also must be True.
        
      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftcomplexfftplanrec(int n,
        int k,
        bool childplan,
        bool topmostplan,
        ref int rowptr,
        ref int bluesteinsize,
        ref int precrptr,
        ref int preciptr,
        fasttransformplan plan,
        xparams _params)
    {
        apserv.srealarray localbuf = new apserv.srealarray();
        int m = 0;
        int n1 = 0;
        int n2 = 0;
        int gq = 0;
        int giq = 0;
        int row0 = 0;
        int row1 = 0;
        int row2 = 0;
        int row3 = 0;

        ap.assert(n > 0, "FTComplexFFTPlan: N<=0");
        ap.assert(k > 0, "FTComplexFFTPlan: K<=0");
        ap.assert(!topmostplan || childplan, "FTComplexFFTPlan: ChildPlan is inconsistent with TopmostPlan");

        //
        // Try to generate "topmost" plan
        //
        if (topmostplan && n > recursivethreshold)
        {
            ftfactorize(n, false, ref n1, ref n2, _params);
            if (n1 * n2 == 0)
            {

                //
                // Handle prime-factor FFT with Bluestein's FFT.
                // Determine size of Bluestein's buffer.
                //
                m = ftbasefindsmooth(2 * n - 1, _params);
                bluesteinsize = Math.Max(2 * m, bluesteinsize);

                //
                // Generate plan
                //
                ftpushentry2(plan, ref rowptr, opstart, k, n, 2, -1, ftoptimisticestimate(n, _params), _params);
                ftpushentry4(plan, ref rowptr, opbluesteinsfft, k, n, 2, m, 2, precrptr, 0, _params);
                row0 = rowptr;
                ftpushentry(plan, ref rowptr, opjmp, 0, 0, 0, 0, _params);
                ftcomplexfftplanrec(m, 1, true, true, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
                row1 = rowptr;
                plan.entries[row0, colparam0] = row1 - row0;
                ftpushentry(plan, ref rowptr, opend, k, n, 2, 0, _params);

                //
                // Fill precomputed buffer
                //
                ftprecomputebluesteinsfft(n, m, plan.precr, precrptr, _params);

                //
                // Update pointer to the precomputed area
                //
                precrptr = precrptr + 4 * m;
            }
            else
            {

                //
                // Handle composite FFT with recursive Cooley-Tukey which
                // uses global buffer instead of local one.
                //
                ftpushentry2(plan, ref rowptr, opstart, k, n, 2, -1, ftoptimisticestimate(n, _params), _params);
                ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n1, _params);
                row0 = rowptr;
                ftpushentry2(plan, ref rowptr, opparallelcall, k * n2, n1, 2, 0, ftoptimisticestimate(n, _params), _params);
                ftpushentry(plan, ref rowptr, opcomplexfftfactors, k, n, 2, n1, _params);
                ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n2, _params);
                row2 = rowptr;
                ftpushentry2(plan, ref rowptr, opparallelcall, k * n1, n2, 2, 0, ftoptimisticestimate(n, _params), _params);
                ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n1, _params);
                ftpushentry(plan, ref rowptr, opend, k, n, 2, 0, _params);
                row1 = rowptr;
                ftcomplexfftplanrec(n1, 1, true, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
                plan.entries[row0, colparam0] = row1 - row0;
                row3 = rowptr;
                ftcomplexfftplanrec(n2, 1, true, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
                plan.entries[row2, colparam0] = row3 - row2;
            }
            return;
        }

        //
        // Prepare "non-topmost" plan:
        // * calculate factorization
        // * use local (shared) buffer
        // * update buffer size - ANY plan will need at least
        //   2*N temporaries, additional requirements can be
        //   applied later
        //
        ftfactorize(n, false, ref n1, ref n2, _params);

        //
        // Handle FFT's with N1*N2=0: either small-N or prime-factor
        //
        if (n1 * n2 == 0)
        {
            if (n <= maxradix)
            {

                //
                // Small-N FFT
                //
                if (childplan)
                {
                    ftpushentry2(plan, ref rowptr, opstart, k, n, 2, -1, ftoptimisticestimate(n, _params), _params);
                }
                ftpushentry(plan, ref rowptr, opcomplexcodeletfft, k, n, 2, 0, _params);
                if (childplan)
                {
                    ftpushentry(plan, ref rowptr, opend, k, n, 2, 0, _params);
                }
                return;
            }
            if (n <= raderthreshold)
            {

                //
                // Handle prime-factor FFT's with Rader's FFT
                //
                m = n - 1;
                if (childplan)
                {
                    ftpushentry2(plan, ref rowptr, opstart, k, n, 2, -1, ftoptimisticestimate(n, _params), _params);
                }
                ntheory.findprimitiverootandinverse(n, ref gq, ref giq, _params);
                ftpushentry4(plan, ref rowptr, opradersfft, k, n, 2, 2, gq, giq, precrptr, _params);
                ftprecomputeradersfft(n, gq, giq, plan.precr, precrptr, _params);
                precrptr = precrptr + 2 * (n - 1);
                row0 = rowptr;
                ftpushentry(plan, ref rowptr, opjmp, 0, 0, 0, 0, _params);
                ftcomplexfftplanrec(m, 1, true, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
                row1 = rowptr;
                plan.entries[row0, colparam0] = row1 - row0;
                if (childplan)
                {
                    ftpushentry(plan, ref rowptr, opend, k, n, 2, 0, _params);
                }
            }
            else
            {

                //
                // Handle prime-factor FFT's with Bluestein's FFT
                //
                m = ftbasefindsmooth(2 * n - 1, _params);
                bluesteinsize = Math.Max(2 * m, bluesteinsize);
                if (childplan)
                {
                    ftpushentry2(plan, ref rowptr, opstart, k, n, 2, -1, ftoptimisticestimate(n, _params), _params);
                }
                ftpushentry4(plan, ref rowptr, opbluesteinsfft, k, n, 2, m, 2, precrptr, 0, _params);
                ftprecomputebluesteinsfft(n, m, plan.precr, precrptr, _params);
                precrptr = precrptr + 4 * m;
                row0 = rowptr;
                ftpushentry(plan, ref rowptr, opjmp, 0, 0, 0, 0, _params);
                ftcomplexfftplanrec(m, 1, true, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
                row1 = rowptr;
                plan.entries[row0, colparam0] = row1 - row0;
                if (childplan)
                {
                    ftpushentry(plan, ref rowptr, opend, k, n, 2, 0, _params);
                }
            }
            return;
        }

        //
        // Handle Cooley-Tukey FFT with small N1
        //
        if (n1 <= maxradix)
        {

            //
            // Specialized transformation for small N1:
            // * N2 short inplace FFT's, each N1-point, with integrated twiddle factors
            // * N1 long FFT's
            // * final transposition
            //
            if (childplan)
            {
                ftpushentry2(plan, ref rowptr, opstart, k, n, 2, -1, ftoptimisticestimate(n, _params), _params);
            }
            ftpushentry(plan, ref rowptr, opcomplexcodelettwfft, k, n1, 2 * n2, 0, _params);
            ftcomplexfftplanrec(n2, k * n1, false, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
            ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n1, _params);
            if (childplan)
            {
                ftpushentry(plan, ref rowptr, opend, k, n, 2, 0, _params);
            }
            return;
        }

        //
        // Handle general Cooley-Tukey FFT, either "flat" or "recursive"
        //
        if (n <= recursivethreshold)
        {

            //
            // General code for large N1/N2, "flat" version without explicit recurrence
            // (nested subplans are inserted directly into the body of the plan)
            //
            if (childplan)
            {
                ftpushentry2(plan, ref rowptr, opstart, k, n, 2, -1, ftoptimisticestimate(n, _params), _params);
            }
            ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n1, _params);
            ftcomplexfftplanrec(n1, k * n2, false, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
            ftpushentry(plan, ref rowptr, opcomplexfftfactors, k, n, 2, n1, _params);
            ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n2, _params);
            ftcomplexfftplanrec(n2, k * n1, false, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
            ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n1, _params);
            if (childplan)
            {
                ftpushentry(plan, ref rowptr, opend, k, n, 2, 0, _params);
            }
        }
        else
        {

            //
            // General code for large N1/N2, "recursive" version - nested subplans
            // are separated from the plan body.
            //
            // Generate parent plan.
            //
            if (childplan)
            {
                ftpushentry2(plan, ref rowptr, opstart, k, n, 2, -1, ftoptimisticestimate(n, _params), _params);
            }
            ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n1, _params);
            row0 = rowptr;
            ftpushentry2(plan, ref rowptr, opparallelcall, k * n2, n1, 2, 0, ftoptimisticestimate(n, _params), _params);
            ftpushentry(plan, ref rowptr, opcomplexfftfactors, k, n, 2, n1, _params);
            ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n2, _params);
            row2 = rowptr;
            ftpushentry2(plan, ref rowptr, opparallelcall, k * n1, n2, 2, 0, ftoptimisticestimate(n, _params), _params);
            ftpushentry(plan, ref rowptr, opcomplextranspose, k, n, 2, n1, _params);
            if (childplan)
            {
                ftpushentry(plan, ref rowptr, opend, k, n, 2, 0, _params);
            }

            //
            // Generate child subplans, insert refence to parent plans
            //
            row1 = rowptr;
            ftcomplexfftplanrec(n1, 1, true, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
            plan.entries[row0, colparam0] = row1 - row0;
            row3 = rowptr;
            ftcomplexfftplanrec(n2, 1, true, false, ref rowptr, ref bluesteinsize, ref precrptr, ref preciptr, plan, _params);
            plan.entries[row2, colparam0] = row3 - row2;
        }
    }


    /*************************************************************************
    This function pushes one more entry to the plan. It resizes Entries matrix
    if needed.

    INPUT PARAMETERS:
        Plan        -   plan (generated so far)
        RowPtr      -   index which points to past-the-last entry generated so far
        EType       -   entry type
        EOpCnt      -   operands count
        EOpSize     -   operand size
        EMcvSize    -   microvector size
        EParam0     -   parameter 0
        
    OUTPUT PARAMETERS:
        Plan        -   updated plan    
        RowPtr      -   updated pointer

    NOTE: Param1 is set to -1.
        
      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftpushentry(fasttransformplan plan,
        ref int rowptr,
        int etype,
        int eopcnt,
        int eopsize,
        int emcvsize,
        int eparam0,
        xparams _params)
    {
        ftpushentry2(plan, ref rowptr, etype, eopcnt, eopsize, emcvsize, eparam0, -1, _params);
    }


    /*************************************************************************
    Same as FTPushEntry(), but sets Param0 AND Param1.
    This function pushes one more entry to the plan. It resized Entries matrix
    if needed.

    INPUT PARAMETERS:
        Plan        -   plan (generated so far)
        RowPtr      -   index which points to past-the-last entry generated so far
        EType       -   entry type
        EOpCnt      -   operands count
        EOpSize     -   operand size
        EMcvSize    -   microvector size
        EParam0     -   parameter 0
        EParam1     -   parameter 1
        
    OUTPUT PARAMETERS:
        Plan        -   updated plan    
        RowPtr      -   updated pointer

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftpushentry2(fasttransformplan plan,
        ref int rowptr,
        int etype,
        int eopcnt,
        int eopsize,
        int emcvsize,
        int eparam0,
        int eparam1,
        xparams _params)
    {
        if (rowptr >= ap.rows(plan.entries))
        {
            apserv.imatrixresize(ref plan.entries, Math.Max(2 * ap.rows(plan.entries), 1), colscnt, _params);
        }
        plan.entries[rowptr, coltype] = etype;
        plan.entries[rowptr, coloperandscnt] = eopcnt;
        plan.entries[rowptr, coloperandsize] = eopsize;
        plan.entries[rowptr, colmicrovectorsize] = emcvsize;
        plan.entries[rowptr, colparam0] = eparam0;
        plan.entries[rowptr, colparam1] = eparam1;
        plan.entries[rowptr, colparam2] = 0;
        plan.entries[rowptr, colparam3] = 0;
        rowptr = rowptr + 1;
    }


    /*************************************************************************
    Same as FTPushEntry(), but sets Param0, Param1, Param2 and Param3.
    This function pushes one more entry to the plan. It resized Entries matrix
    if needed.

    INPUT PARAMETERS:
        Plan        -   plan (generated so far)
        RowPtr      -   index which points to past-the-last entry generated so far
        EType       -   entry type
        EOpCnt      -   operands count
        EOpSize     -   operand size
        EMcvSize    -   microvector size
        EParam0     -   parameter 0
        EParam1     -   parameter 1
        EParam2     -   parameter 2
        EParam3     -   parameter 3
        
    OUTPUT PARAMETERS:
        Plan        -   updated plan    
        RowPtr      -   updated pointer

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftpushentry4(fasttransformplan plan,
        ref int rowptr,
        int etype,
        int eopcnt,
        int eopsize,
        int emcvsize,
        int eparam0,
        int eparam1,
        int eparam2,
        int eparam3,
        xparams _params)
    {
        if (rowptr >= ap.rows(plan.entries))
        {
            apserv.imatrixresize(ref plan.entries, Math.Max(2 * ap.rows(plan.entries), 1), colscnt, _params);
        }
        plan.entries[rowptr, coltype] = etype;
        plan.entries[rowptr, coloperandscnt] = eopcnt;
        plan.entries[rowptr, coloperandsize] = eopsize;
        plan.entries[rowptr, colmicrovectorsize] = emcvsize;
        plan.entries[rowptr, colparam0] = eparam0;
        plan.entries[rowptr, colparam1] = eparam1;
        plan.entries[rowptr, colparam2] = eparam2;
        plan.entries[rowptr, colparam3] = eparam3;
        rowptr = rowptr + 1;
    }


    /*************************************************************************
    This subroutine applies subplan to input/output array A.

    INPUT PARAMETERS:
        Plan        -   transformation plan
        SubPlan     -   subplan index
        A           -   array, must be large enough for plan to work
        ABase       -   base offset in array A, this value points to start of
                        subarray whose length is equal to length of the plan
        AOffset     -   offset with respect to ABase, 0<=AOffset<PlanLength.
                        This is an offset within large PlanLength-subarray of
                        the chunk to process.
        Buf         -   temporary buffer whose length is equal to plan length
                        (without taking into account RepCnt) or larger.
        OffsBuf     -   offset in the buffer array
        RepCnt      -   repetition count (transformation is repeatedly applied
                        to subsequent subarrays)
        
    OUTPUT PARAMETERS:
        Plan        -   plan (temporary buffers can be modified, plan itself
                        is unchanged and can be reused)
        A           -   transformed array

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftapplysubplan(fasttransformplan plan,
        int subplan,
        double[] a,
        int abase,
        int aoffset,
        double[] buf,
        int repcnt,
        xparams _params)
    {
        int rowidx = 0;
        int i = 0;
        int n1 = 0;
        int n2 = 0;
        int operation = 0;
        int operandscnt = 0;
        int operandsize = 0;
        int microvectorsize = 0;
        int param0 = 0;
        int param1 = 0;
        int parentsize = 0;
        int childsize = 0;
        int chunksize = 0;
        apserv.srealarray bufa = null;
        apserv.srealarray bufb = null;
        apserv.srealarray bufc = null;
        apserv.srealarray bufd = null;

        ap.assert(plan.entries[subplan, coltype] == opstart, "FTApplySubPlan: incorrect subplan header");
        rowidx = subplan + 1;
        while (plan.entries[rowidx, coltype] != opend)
        {
            operation = plan.entries[rowidx, coltype];
            operandscnt = repcnt * plan.entries[rowidx, coloperandscnt];
            operandsize = plan.entries[rowidx, coloperandsize];
            microvectorsize = plan.entries[rowidx, colmicrovectorsize];
            param0 = plan.entries[rowidx, colparam0];
            param1 = plan.entries[rowidx, colparam1];
            apserv.touchint(ref param1, _params);

            //
            // Process "jump" operation
            //
            if (operation == opjmp)
            {
                rowidx = rowidx + plan.entries[rowidx, colparam0];
                continue;
            }

            //
            // Process "parallel call" operation:
            // * we perform initial check for consistency between parent and child plans
            // * we call FTSplitAndApplyParallelPlan(), which splits parallel plan into
            //   several parallel tasks
            //
            if (operation == opparallelcall)
            {
                parentsize = operandsize * microvectorsize;
                childsize = plan.entries[rowidx + param0, coloperandscnt] * plan.entries[rowidx + param0, coloperandsize] * plan.entries[rowidx + param0, colmicrovectorsize];
                ap.assert(plan.entries[rowidx + param0, coltype] == opstart, "FTApplySubPlan: incorrect child subplan header");
                ap.assert(parentsize == childsize, "FTApplySubPlan: incorrect child subplan header");
                chunksize = Math.Max(recursivethreshold / childsize, 1);
                i = 0;
                while (i < operandscnt)
                {
                    chunksize = Math.Min(chunksize, operandscnt - i);
                    ftapplysubplan(plan, rowidx + param0, a, abase, aoffset + i * childsize, buf, chunksize, _params);
                    i = i + chunksize;
                }
                rowidx = rowidx + 1;
                continue;
            }

            //
            // Process "reference complex FFT" operation
            //
            if (operation == opcomplexreffft)
            {
                ftapplycomplexreffft(a, abase + aoffset, operandscnt, operandsize, microvectorsize, buf, _params);
                rowidx = rowidx + 1;
                continue;
            }

            //
            // Process "codelet FFT" operation
            //
            if (operation == opcomplexcodeletfft)
            {
                ftapplycomplexcodeletfft(a, abase + aoffset, operandscnt, operandsize, microvectorsize, _params);
                rowidx = rowidx + 1;
                continue;
            }

            //
            // Process "integrated codelet FFT" operation
            //
            if (operation == opcomplexcodelettwfft)
            {
                ftapplycomplexcodelettwfft(a, abase + aoffset, operandscnt, operandsize, microvectorsize, _params);
                rowidx = rowidx + 1;
                continue;
            }

            //
            // Process Bluestein's FFT operation
            //
            if (operation == opbluesteinsfft)
            {
                ap.assert(microvectorsize == 2, "FTApplySubPlan: microvectorsize!=2 for Bluesteins FFT");
                smp.ae_shared_pool_retrieve(plan.bluesteinpool, ref bufa);
                smp.ae_shared_pool_retrieve(plan.bluesteinpool, ref bufb);
                smp.ae_shared_pool_retrieve(plan.bluesteinpool, ref bufc);
                smp.ae_shared_pool_retrieve(plan.bluesteinpool, ref bufd);
                ftbluesteinsfft(plan, a, abase, aoffset, operandscnt, operandsize, plan.entries[rowidx, colparam0], plan.entries[rowidx, colparam2], rowidx + plan.entries[rowidx, colparam1], bufa.val, bufb.val, bufc.val, bufd.val, _params);
                smp.ae_shared_pool_recycle(plan.bluesteinpool, ref bufa);
                smp.ae_shared_pool_recycle(plan.bluesteinpool, ref bufb);
                smp.ae_shared_pool_recycle(plan.bluesteinpool, ref bufc);
                smp.ae_shared_pool_recycle(plan.bluesteinpool, ref bufd);
                rowidx = rowidx + 1;
                continue;
            }

            //
            // Process Rader's FFT
            //
            if (operation == opradersfft)
            {
                ftradersfft(plan, a, abase, aoffset, operandscnt, operandsize, rowidx + plan.entries[rowidx, colparam0], plan.entries[rowidx, colparam1], plan.entries[rowidx, colparam2], plan.entries[rowidx, colparam3], buf, _params);
                rowidx = rowidx + 1;
                continue;
            }

            //
            // Process "complex twiddle factors" operation
            //
            if (operation == opcomplexfftfactors)
            {
                ap.assert(microvectorsize == 2, "FTApplySubPlan: MicrovectorSize<>1");
                n1 = plan.entries[rowidx, colparam0];
                n2 = operandsize / n1;
                for (i = 0; i <= operandscnt - 1; i++)
                {
                    ffttwcalc(a, abase + aoffset + i * operandsize * 2, n1, n2, _params);
                }
                rowidx = rowidx + 1;
                continue;
            }

            //
            // Process "complex transposition" operation
            //
            if (operation == opcomplextranspose)
            {
                ap.assert(microvectorsize == 2, "FTApplySubPlan: MicrovectorSize<>1");
                n1 = plan.entries[rowidx, colparam0];
                n2 = operandsize / n1;
                for (i = 0; i <= operandscnt - 1; i++)
                {
                    internalcomplexlintranspose(a, n1, n2, abase + aoffset + i * operandsize * 2, buf, _params);
                }
                rowidx = rowidx + 1;
                continue;
            }

            //
            // Error
            //
            ap.assert(false, "FTApplySubPlan: unexpected plan type");
        }
    }


    /*************************************************************************
    This subroutine applies complex reference FFT to input/output array A.

    VERY SLOW OPERATION, do not use it in real life plans :)

    INPUT PARAMETERS:
        A           -   array, must be large enough for plan to work
        Offs        -   offset of the subarray to process
        OperandsCnt -   operands count (see description of FastTransformPlan)
        OperandSize -   operand size (see description of FastTransformPlan)
        MicrovectorSize-microvector size (see description of FastTransformPlan)
        Buf         -   temporary array, must be at least OperandsCnt*OperandSize*MicrovectorSize
        
    OUTPUT PARAMETERS:
        A           -   transformed array

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftapplycomplexreffft(double[] a,
        int offs,
        int operandscnt,
        int operandsize,
        int microvectorsize,
        double[] buf,
        xparams _params)
    {
        int opidx = 0;
        int i = 0;
        int k = 0;
        double hre = 0;
        double him = 0;
        double c = 0;
        double s = 0;
        double re = 0;
        double im = 0;
        int n = 0;

        ap.assert(operandscnt >= 1, "FTApplyComplexRefFFT: OperandsCnt<1");
        ap.assert(operandsize >= 1, "FTApplyComplexRefFFT: OperandSize<1");
        ap.assert(microvectorsize == 2, "FTApplyComplexRefFFT: MicrovectorSize<>2");
        n = operandsize;
        for (opidx = 0; opidx <= operandscnt - 1; opidx++)
        {
            for (i = 0; i <= n - 1; i++)
            {
                hre = 0;
                him = 0;
                for (k = 0; k <= n - 1; k++)
                {
                    re = a[offs + opidx * operandsize * 2 + 2 * k + 0];
                    im = a[offs + opidx * operandsize * 2 + 2 * k + 1];
                    c = Math.Cos(-(2 * Math.PI * k * i / n));
                    s = Math.Sin(-(2 * Math.PI * k * i / n));
                    hre = hre + c * re - s * im;
                    him = him + c * im + s * re;
                }
                buf[2 * i + 0] = hre;
                buf[2 * i + 1] = him;
            }
            for (i = 0; i <= operandsize * 2 - 1; i++)
            {
                a[offs + opidx * operandsize * 2 + i] = buf[i];
            }
        }
    }


    /*************************************************************************
    This subroutine applies complex codelet FFT to input/output array A.

    INPUT PARAMETERS:
        A           -   array, must be large enough for plan to work
        Offs        -   offset of the subarray to process
        OperandsCnt -   operands count (see description of FastTransformPlan)
        OperandSize -   operand size (see description of FastTransformPlan)
        MicrovectorSize-microvector size, must be 2
        
    OUTPUT PARAMETERS:
        A           -   transformed array

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftapplycomplexcodeletfft(double[] a,
        int offs,
        int operandscnt,
        int operandsize,
        int microvectorsize,
        xparams _params)
    {
        int opidx = 0;
        int n = 0;
        int aoffset = 0;
        double a0x = 0;
        double a0y = 0;
        double a1x = 0;
        double a1y = 0;
        double a2x = 0;
        double a2y = 0;
        double a3x = 0;
        double a3y = 0;
        double a4x = 0;
        double a4y = 0;
        double a5x = 0;
        double a5y = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double v3 = 0;
        double t1x = 0;
        double t1y = 0;
        double t2x = 0;
        double t2y = 0;
        double t3x = 0;
        double t3y = 0;
        double t4x = 0;
        double t4y = 0;
        double t5x = 0;
        double t5y = 0;
        double m1x = 0;
        double m1y = 0;
        double m2x = 0;
        double m2y = 0;
        double m3x = 0;
        double m3y = 0;
        double m4x = 0;
        double m4y = 0;
        double m5x = 0;
        double m5y = 0;
        double s1x = 0;
        double s1y = 0;
        double s2x = 0;
        double s2y = 0;
        double s3x = 0;
        double s3y = 0;
        double s4x = 0;
        double s4y = 0;
        double s5x = 0;
        double s5y = 0;
        double c1 = 0;
        double c2 = 0;
        double c3 = 0;
        double c4 = 0;
        double c5 = 0;
        double v = 0;

        ap.assert(operandscnt >= 1, "FTApplyComplexCodeletFFT: OperandsCnt<1");
        ap.assert(operandsize >= 1, "FTApplyComplexCodeletFFT: OperandSize<1");
        ap.assert(microvectorsize == 2, "FTApplyComplexCodeletFFT: MicrovectorSize<>2");
        n = operandsize;

        //
        // Hard-coded transforms for different N's
        //
        ap.assert(n <= maxradix, "FTApplyComplexCodeletFFT: N>MaxRadix");
        if (n == 2)
        {
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset = offs + opidx * operandsize * 2;
                a0x = a[aoffset + 0];
                a0y = a[aoffset + 1];
                a1x = a[aoffset + 2];
                a1y = a[aoffset + 3];
                v0 = a0x + a1x;
                v1 = a0y + a1y;
                v2 = a0x - a1x;
                v3 = a0y - a1y;
                a[aoffset + 0] = v0;
                a[aoffset + 1] = v1;
                a[aoffset + 2] = v2;
                a[aoffset + 3] = v3;
            }
            return;
        }
        if (n == 3)
        {
            c1 = Math.Cos(2 * Math.PI / 3) - 1;
            c2 = Math.Sin(2 * Math.PI / 3);
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset = offs + opidx * operandsize * 2;
                a0x = a[aoffset + 0];
                a0y = a[aoffset + 1];
                a1x = a[aoffset + 2];
                a1y = a[aoffset + 3];
                a2x = a[aoffset + 4];
                a2y = a[aoffset + 5];
                t1x = a1x + a2x;
                t1y = a1y + a2y;
                a0x = a0x + t1x;
                a0y = a0y + t1y;
                m1x = c1 * t1x;
                m1y = c1 * t1y;
                m2x = c2 * (a1y - a2y);
                m2y = c2 * (a2x - a1x);
                s1x = a0x + m1x;
                s1y = a0y + m1y;
                a1x = s1x + m2x;
                a1y = s1y + m2y;
                a2x = s1x - m2x;
                a2y = s1y - m2y;
                a[aoffset + 0] = a0x;
                a[aoffset + 1] = a0y;
                a[aoffset + 2] = a1x;
                a[aoffset + 3] = a1y;
                a[aoffset + 4] = a2x;
                a[aoffset + 5] = a2y;
            }
            return;
        }
        if (n == 4)
        {
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset = offs + opidx * operandsize * 2;
                a0x = a[aoffset + 0];
                a0y = a[aoffset + 1];
                a1x = a[aoffset + 2];
                a1y = a[aoffset + 3];
                a2x = a[aoffset + 4];
                a2y = a[aoffset + 5];
                a3x = a[aoffset + 6];
                a3y = a[aoffset + 7];
                t1x = a0x + a2x;
                t1y = a0y + a2y;
                t2x = a1x + a3x;
                t2y = a1y + a3y;
                m2x = a0x - a2x;
                m2y = a0y - a2y;
                m3x = a1y - a3y;
                m3y = a3x - a1x;
                a[aoffset + 0] = t1x + t2x;
                a[aoffset + 1] = t1y + t2y;
                a[aoffset + 4] = t1x - t2x;
                a[aoffset + 5] = t1y - t2y;
                a[aoffset + 2] = m2x + m3x;
                a[aoffset + 3] = m2y + m3y;
                a[aoffset + 6] = m2x - m3x;
                a[aoffset + 7] = m2y - m3y;
            }
            return;
        }
        if (n == 5)
        {
            v = 2 * Math.PI / 5;
            c1 = (Math.Cos(v) + Math.Cos(2 * v)) / 2 - 1;
            c2 = (Math.Cos(v) - Math.Cos(2 * v)) / 2;
            c3 = -Math.Sin(v);
            c4 = -(Math.Sin(v) + Math.Sin(2 * v));
            c5 = Math.Sin(v) - Math.Sin(2 * v);
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset = offs + opidx * operandsize * 2;
                t1x = a[aoffset + 2] + a[aoffset + 8];
                t1y = a[aoffset + 3] + a[aoffset + 9];
                t2x = a[aoffset + 4] + a[aoffset + 6];
                t2y = a[aoffset + 5] + a[aoffset + 7];
                t3x = a[aoffset + 2] - a[aoffset + 8];
                t3y = a[aoffset + 3] - a[aoffset + 9];
                t4x = a[aoffset + 6] - a[aoffset + 4];
                t4y = a[aoffset + 7] - a[aoffset + 5];
                t5x = t1x + t2x;
                t5y = t1y + t2y;
                a[aoffset + 0] = a[aoffset + 0] + t5x;
                a[aoffset + 1] = a[aoffset + 1] + t5y;
                m1x = c1 * t5x;
                m1y = c1 * t5y;
                m2x = c2 * (t1x - t2x);
                m2y = c2 * (t1y - t2y);
                m3x = -(c3 * (t3y + t4y));
                m3y = c3 * (t3x + t4x);
                m4x = -(c4 * t4y);
                m4y = c4 * t4x;
                m5x = -(c5 * t3y);
                m5y = c5 * t3x;
                s3x = m3x - m4x;
                s3y = m3y - m4y;
                s5x = m3x + m5x;
                s5y = m3y + m5y;
                s1x = a[aoffset + 0] + m1x;
                s1y = a[aoffset + 1] + m1y;
                s2x = s1x + m2x;
                s2y = s1y + m2y;
                s4x = s1x - m2x;
                s4y = s1y - m2y;
                a[aoffset + 2] = s2x + s3x;
                a[aoffset + 3] = s2y + s3y;
                a[aoffset + 4] = s4x + s5x;
                a[aoffset + 5] = s4y + s5y;
                a[aoffset + 6] = s4x - s5x;
                a[aoffset + 7] = s4y - s5y;
                a[aoffset + 8] = s2x - s3x;
                a[aoffset + 9] = s2y - s3y;
            }
            return;
        }
        if (n == 6)
        {
            c1 = Math.Cos(2 * Math.PI / 3) - 1;
            c2 = Math.Sin(2 * Math.PI / 3);
            c3 = Math.Cos(-(Math.PI / 3));
            c4 = Math.Sin(-(Math.PI / 3));
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset = offs + opidx * operandsize * 2;
                a0x = a[aoffset + 0];
                a0y = a[aoffset + 1];
                a1x = a[aoffset + 2];
                a1y = a[aoffset + 3];
                a2x = a[aoffset + 4];
                a2y = a[aoffset + 5];
                a3x = a[aoffset + 6];
                a3y = a[aoffset + 7];
                a4x = a[aoffset + 8];
                a4y = a[aoffset + 9];
                a5x = a[aoffset + 10];
                a5y = a[aoffset + 11];
                v0 = a0x;
                v1 = a0y;
                a0x = a0x + a3x;
                a0y = a0y + a3y;
                a3x = v0 - a3x;
                a3y = v1 - a3y;
                v0 = a1x;
                v1 = a1y;
                a1x = a1x + a4x;
                a1y = a1y + a4y;
                a4x = v0 - a4x;
                a4y = v1 - a4y;
                v0 = a2x;
                v1 = a2y;
                a2x = a2x + a5x;
                a2y = a2y + a5y;
                a5x = v0 - a5x;
                a5y = v1 - a5y;
                t4x = a4x * c3 - a4y * c4;
                t4y = a4x * c4 + a4y * c3;
                a4x = t4x;
                a4y = t4y;
                t5x = -(a5x * c3) - a5y * c4;
                t5y = a5x * c4 - a5y * c3;
                a5x = t5x;
                a5y = t5y;
                t1x = a1x + a2x;
                t1y = a1y + a2y;
                a0x = a0x + t1x;
                a0y = a0y + t1y;
                m1x = c1 * t1x;
                m1y = c1 * t1y;
                m2x = c2 * (a1y - a2y);
                m2y = c2 * (a2x - a1x);
                s1x = a0x + m1x;
                s1y = a0y + m1y;
                a1x = s1x + m2x;
                a1y = s1y + m2y;
                a2x = s1x - m2x;
                a2y = s1y - m2y;
                t1x = a4x + a5x;
                t1y = a4y + a5y;
                a3x = a3x + t1x;
                a3y = a3y + t1y;
                m1x = c1 * t1x;
                m1y = c1 * t1y;
                m2x = c2 * (a4y - a5y);
                m2y = c2 * (a5x - a4x);
                s1x = a3x + m1x;
                s1y = a3y + m1y;
                a4x = s1x + m2x;
                a4y = s1y + m2y;
                a5x = s1x - m2x;
                a5y = s1y - m2y;
                a[aoffset + 0] = a0x;
                a[aoffset + 1] = a0y;
                a[aoffset + 2] = a3x;
                a[aoffset + 3] = a3y;
                a[aoffset + 4] = a1x;
                a[aoffset + 5] = a1y;
                a[aoffset + 6] = a4x;
                a[aoffset + 7] = a4y;
                a[aoffset + 8] = a2x;
                a[aoffset + 9] = a2y;
                a[aoffset + 10] = a5x;
                a[aoffset + 11] = a5y;
            }
            return;
        }
    }


    /*************************************************************************
    This subroutine applies complex "integrated" codelet FFT  to  input/output
    array A. "Integrated" codelet differs from "normal" one in following ways:
    * it can work with MicrovectorSize>1
    * hence, it can be used in Cooley-Tukey FFT without transpositions
    * it performs inlined multiplication by twiddle factors of Cooley-Tukey
      FFT with N2=MicrovectorSize/2.

    INPUT PARAMETERS:
        A           -   array, must be large enough for plan to work
        Offs        -   offset of the subarray to process
        OperandsCnt -   operands count (see description of FastTransformPlan)
        OperandSize -   operand size (see description of FastTransformPlan)
        MicrovectorSize-microvector size, must be 1
        
    OUTPUT PARAMETERS:
        A           -   transformed array

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftapplycomplexcodelettwfft(double[] a,
        int offs,
        int operandscnt,
        int operandsize,
        int microvectorsize,
        xparams _params)
    {
        int opidx = 0;
        int mvidx = 0;
        int n = 0;
        int m = 0;
        int aoffset0 = 0;
        int aoffset2 = 0;
        int aoffset4 = 0;
        int aoffset6 = 0;
        int aoffset8 = 0;
        int aoffset10 = 0;
        double a0x = 0;
        double a0y = 0;
        double a1x = 0;
        double a1y = 0;
        double a2x = 0;
        double a2y = 0;
        double a3x = 0;
        double a3y = 0;
        double a4x = 0;
        double a4y = 0;
        double a5x = 0;
        double a5y = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double v3 = 0;
        double q0x = 0;
        double q0y = 0;
        double t1x = 0;
        double t1y = 0;
        double t2x = 0;
        double t2y = 0;
        double t3x = 0;
        double t3y = 0;
        double t4x = 0;
        double t4y = 0;
        double t5x = 0;
        double t5y = 0;
        double m1x = 0;
        double m1y = 0;
        double m2x = 0;
        double m2y = 0;
        double m3x = 0;
        double m3y = 0;
        double m4x = 0;
        double m4y = 0;
        double m5x = 0;
        double m5y = 0;
        double s1x = 0;
        double s1y = 0;
        double s2x = 0;
        double s2y = 0;
        double s3x = 0;
        double s3y = 0;
        double s4x = 0;
        double s4y = 0;
        double s5x = 0;
        double s5y = 0;
        double c1 = 0;
        double c2 = 0;
        double c3 = 0;
        double c4 = 0;
        double c5 = 0;
        double v = 0;
        double tw0 = 0;
        double tw1 = 0;
        double twx = 0;
        double twxm1 = 0;
        double twy = 0;
        double tw2x = 0;
        double tw2y = 0;
        double tw3x = 0;
        double tw3y = 0;
        double tw4x = 0;
        double tw4y = 0;
        double tw5x = 0;
        double tw5y = 0;

        ap.assert(operandscnt >= 1, "FTApplyComplexCodeletFFT: OperandsCnt<1");
        ap.assert(operandsize >= 1, "FTApplyComplexCodeletFFT: OperandSize<1");
        ap.assert(microvectorsize >= 1, "FTApplyComplexCodeletFFT: MicrovectorSize<>1");
        ap.assert(microvectorsize % 2 == 0, "FTApplyComplexCodeletFFT: MicrovectorSize is not even");
        n = operandsize;
        m = microvectorsize / 2;

        //
        // Hard-coded transforms for different N's
        //
        ap.assert(n <= maxradix, "FTApplyComplexCodeletTwFFT: N>MaxRadix");
        if (n == 2)
        {
            v = -(2 * Math.PI / (n * m));
            tw0 = -(2 * math.sqr(Math.Sin(0.5 * v)));
            tw1 = Math.Sin(v);
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset0 = offs + opidx * operandsize * microvectorsize;
                aoffset2 = aoffset0 + microvectorsize;
                twxm1 = 0.0;
                twy = 0.0;
                for (mvidx = 0; mvidx <= m - 1; mvidx++)
                {
                    a0x = a[aoffset0];
                    a0y = a[aoffset0 + 1];
                    a1x = a[aoffset2];
                    a1y = a[aoffset2 + 1];
                    v0 = a0x + a1x;
                    v1 = a0y + a1y;
                    v2 = a0x - a1x;
                    v3 = a0y - a1y;
                    a[aoffset0] = v0;
                    a[aoffset0 + 1] = v1;
                    a[aoffset2] = v2 * (1 + twxm1) - v3 * twy;
                    a[aoffset2 + 1] = v3 * (1 + twxm1) + v2 * twy;
                    aoffset0 = aoffset0 + 2;
                    aoffset2 = aoffset2 + 2;
                    if ((mvidx + 1) % updatetw == 0)
                    {
                        v = -(2 * Math.PI * (mvidx + 1) / (n * m));
                        twxm1 = Math.Sin(0.5 * v);
                        twxm1 = -(2 * twxm1 * twxm1);
                        twy = Math.Sin(v);
                    }
                    else
                    {
                        v = twxm1 + tw0 + twxm1 * tw0 - twy * tw1;
                        twy = twy + tw1 + twxm1 * tw1 + twy * tw0;
                        twxm1 = v;
                    }
                }
            }
            return;
        }
        if (n == 3)
        {
            v = -(2 * Math.PI / (n * m));
            tw0 = -(2 * math.sqr(Math.Sin(0.5 * v)));
            tw1 = Math.Sin(v);
            c1 = Math.Cos(2 * Math.PI / 3) - 1;
            c2 = Math.Sin(2 * Math.PI / 3);
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset0 = offs + opidx * operandsize * microvectorsize;
                aoffset2 = aoffset0 + microvectorsize;
                aoffset4 = aoffset2 + microvectorsize;
                twx = 1.0;
                twxm1 = 0.0;
                twy = 0.0;
                for (mvidx = 0; mvidx <= m - 1; mvidx++)
                {
                    a0x = a[aoffset0];
                    a0y = a[aoffset0 + 1];
                    a1x = a[aoffset2];
                    a1y = a[aoffset2 + 1];
                    a2x = a[aoffset4];
                    a2y = a[aoffset4 + 1];
                    t1x = a1x + a2x;
                    t1y = a1y + a2y;
                    a0x = a0x + t1x;
                    a0y = a0y + t1y;
                    m1x = c1 * t1x;
                    m1y = c1 * t1y;
                    m2x = c2 * (a1y - a2y);
                    m2y = c2 * (a2x - a1x);
                    s1x = a0x + m1x;
                    s1y = a0y + m1y;
                    a1x = s1x + m2x;
                    a1y = s1y + m2y;
                    a2x = s1x - m2x;
                    a2y = s1y - m2y;
                    tw2x = twx * twx - twy * twy;
                    tw2y = 2 * twx * twy;
                    a[aoffset0] = a0x;
                    a[aoffset0 + 1] = a0y;
                    a[aoffset2] = a1x * twx - a1y * twy;
                    a[aoffset2 + 1] = a1y * twx + a1x * twy;
                    a[aoffset4] = a2x * tw2x - a2y * tw2y;
                    a[aoffset4 + 1] = a2y * tw2x + a2x * tw2y;
                    aoffset0 = aoffset0 + 2;
                    aoffset2 = aoffset2 + 2;
                    aoffset4 = aoffset4 + 2;
                    if ((mvidx + 1) % updatetw == 0)
                    {
                        v = -(2 * Math.PI * (mvidx + 1) / (n * m));
                        twxm1 = Math.Sin(0.5 * v);
                        twxm1 = -(2 * twxm1 * twxm1);
                        twy = Math.Sin(v);
                        twx = twxm1 + 1;
                    }
                    else
                    {
                        v = twxm1 + tw0 + twxm1 * tw0 - twy * tw1;
                        twy = twy + tw1 + twxm1 * tw1 + twy * tw0;
                        twxm1 = v;
                        twx = v + 1;
                    }
                }
            }
            return;
        }
        if (n == 4)
        {
            v = -(2 * Math.PI / (n * m));
            tw0 = -(2 * math.sqr(Math.Sin(0.5 * v)));
            tw1 = Math.Sin(v);
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset0 = offs + opidx * operandsize * microvectorsize;
                aoffset2 = aoffset0 + microvectorsize;
                aoffset4 = aoffset2 + microvectorsize;
                aoffset6 = aoffset4 + microvectorsize;
                twx = 1.0;
                twxm1 = 0.0;
                twy = 0.0;
                for (mvidx = 0; mvidx <= m - 1; mvidx++)
                {
                    a0x = a[aoffset0];
                    a0y = a[aoffset0 + 1];
                    a1x = a[aoffset2];
                    a1y = a[aoffset2 + 1];
                    a2x = a[aoffset4];
                    a2y = a[aoffset4 + 1];
                    a3x = a[aoffset6];
                    a3y = a[aoffset6 + 1];
                    t1x = a0x + a2x;
                    t1y = a0y + a2y;
                    t2x = a1x + a3x;
                    t2y = a1y + a3y;
                    m2x = a0x - a2x;
                    m2y = a0y - a2y;
                    m3x = a1y - a3y;
                    m3y = a3x - a1x;
                    tw2x = twx * twx - twy * twy;
                    tw2y = 2 * twx * twy;
                    tw3x = twx * tw2x - twy * tw2y;
                    tw3y = twx * tw2y + twy * tw2x;
                    a1x = m2x + m3x;
                    a1y = m2y + m3y;
                    a2x = t1x - t2x;
                    a2y = t1y - t2y;
                    a3x = m2x - m3x;
                    a3y = m2y - m3y;
                    a[aoffset0] = t1x + t2x;
                    a[aoffset0 + 1] = t1y + t2y;
                    a[aoffset2] = a1x * twx - a1y * twy;
                    a[aoffset2 + 1] = a1y * twx + a1x * twy;
                    a[aoffset4] = a2x * tw2x - a2y * tw2y;
                    a[aoffset4 + 1] = a2y * tw2x + a2x * tw2y;
                    a[aoffset6] = a3x * tw3x - a3y * tw3y;
                    a[aoffset6 + 1] = a3y * tw3x + a3x * tw3y;
                    aoffset0 = aoffset0 + 2;
                    aoffset2 = aoffset2 + 2;
                    aoffset4 = aoffset4 + 2;
                    aoffset6 = aoffset6 + 2;
                    if ((mvidx + 1) % updatetw == 0)
                    {
                        v = -(2 * Math.PI * (mvidx + 1) / (n * m));
                        twxm1 = Math.Sin(0.5 * v);
                        twxm1 = -(2 * twxm1 * twxm1);
                        twy = Math.Sin(v);
                        twx = twxm1 + 1;
                    }
                    else
                    {
                        v = twxm1 + tw0 + twxm1 * tw0 - twy * tw1;
                        twy = twy + tw1 + twxm1 * tw1 + twy * tw0;
                        twxm1 = v;
                        twx = v + 1;
                    }
                }
            }
            return;
        }
        if (n == 5)
        {
            v = -(2 * Math.PI / (n * m));
            tw0 = -(2 * math.sqr(Math.Sin(0.5 * v)));
            tw1 = Math.Sin(v);
            v = 2 * Math.PI / 5;
            c1 = (Math.Cos(v) + Math.Cos(2 * v)) / 2 - 1;
            c2 = (Math.Cos(v) - Math.Cos(2 * v)) / 2;
            c3 = -Math.Sin(v);
            c4 = -(Math.Sin(v) + Math.Sin(2 * v));
            c5 = Math.Sin(v) - Math.Sin(2 * v);
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset0 = offs + opidx * operandsize * microvectorsize;
                aoffset2 = aoffset0 + microvectorsize;
                aoffset4 = aoffset2 + microvectorsize;
                aoffset6 = aoffset4 + microvectorsize;
                aoffset8 = aoffset6 + microvectorsize;
                twx = 1.0;
                twxm1 = 0.0;
                twy = 0.0;
                for (mvidx = 0; mvidx <= m - 1; mvidx++)
                {
                    a0x = a[aoffset0];
                    a0y = a[aoffset0 + 1];
                    a1x = a[aoffset2];
                    a1y = a[aoffset2 + 1];
                    a2x = a[aoffset4];
                    a2y = a[aoffset4 + 1];
                    a3x = a[aoffset6];
                    a3y = a[aoffset6 + 1];
                    a4x = a[aoffset8];
                    a4y = a[aoffset8 + 1];
                    t1x = a1x + a4x;
                    t1y = a1y + a4y;
                    t2x = a2x + a3x;
                    t2y = a2y + a3y;
                    t3x = a1x - a4x;
                    t3y = a1y - a4y;
                    t4x = a3x - a2x;
                    t4y = a3y - a2y;
                    t5x = t1x + t2x;
                    t5y = t1y + t2y;
                    q0x = a0x + t5x;
                    q0y = a0y + t5y;
                    m1x = c1 * t5x;
                    m1y = c1 * t5y;
                    m2x = c2 * (t1x - t2x);
                    m2y = c2 * (t1y - t2y);
                    m3x = -(c3 * (t3y + t4y));
                    m3y = c3 * (t3x + t4x);
                    m4x = -(c4 * t4y);
                    m4y = c4 * t4x;
                    m5x = -(c5 * t3y);
                    m5y = c5 * t3x;
                    s3x = m3x - m4x;
                    s3y = m3y - m4y;
                    s5x = m3x + m5x;
                    s5y = m3y + m5y;
                    s1x = q0x + m1x;
                    s1y = q0y + m1y;
                    s2x = s1x + m2x;
                    s2y = s1y + m2y;
                    s4x = s1x - m2x;
                    s4y = s1y - m2y;
                    tw2x = twx * twx - twy * twy;
                    tw2y = 2 * twx * twy;
                    tw3x = twx * tw2x - twy * tw2y;
                    tw3y = twx * tw2y + twy * tw2x;
                    tw4x = tw2x * tw2x - tw2y * tw2y;
                    tw4y = tw2x * tw2y + tw2y * tw2x;
                    a1x = s2x + s3x;
                    a1y = s2y + s3y;
                    a2x = s4x + s5x;
                    a2y = s4y + s5y;
                    a3x = s4x - s5x;
                    a3y = s4y - s5y;
                    a4x = s2x - s3x;
                    a4y = s2y - s3y;
                    a[aoffset0] = q0x;
                    a[aoffset0 + 1] = q0y;
                    a[aoffset2] = a1x * twx - a1y * twy;
                    a[aoffset2 + 1] = a1x * twy + a1y * twx;
                    a[aoffset4] = a2x * tw2x - a2y * tw2y;
                    a[aoffset4 + 1] = a2x * tw2y + a2y * tw2x;
                    a[aoffset6] = a3x * tw3x - a3y * tw3y;
                    a[aoffset6 + 1] = a3x * tw3y + a3y * tw3x;
                    a[aoffset8] = a4x * tw4x - a4y * tw4y;
                    a[aoffset8 + 1] = a4x * tw4y + a4y * tw4x;
                    aoffset0 = aoffset0 + 2;
                    aoffset2 = aoffset2 + 2;
                    aoffset4 = aoffset4 + 2;
                    aoffset6 = aoffset6 + 2;
                    aoffset8 = aoffset8 + 2;
                    if ((mvidx + 1) % updatetw == 0)
                    {
                        v = -(2 * Math.PI * (mvidx + 1) / (n * m));
                        twxm1 = Math.Sin(0.5 * v);
                        twxm1 = -(2 * twxm1 * twxm1);
                        twy = Math.Sin(v);
                        twx = twxm1 + 1;
                    }
                    else
                    {
                        v = twxm1 + tw0 + twxm1 * tw0 - twy * tw1;
                        twy = twy + tw1 + twxm1 * tw1 + twy * tw0;
                        twxm1 = v;
                        twx = v + 1;
                    }
                }
            }
            return;
        }
        if (n == 6)
        {
            c1 = Math.Cos(2 * Math.PI / 3) - 1;
            c2 = Math.Sin(2 * Math.PI / 3);
            c3 = Math.Cos(-(Math.PI / 3));
            c4 = Math.Sin(-(Math.PI / 3));
            v = -(2 * Math.PI / (n * m));
            tw0 = -(2 * math.sqr(Math.Sin(0.5 * v)));
            tw1 = Math.Sin(v);
            for (opidx = 0; opidx <= operandscnt - 1; opidx++)
            {
                aoffset0 = offs + opidx * operandsize * microvectorsize;
                aoffset2 = aoffset0 + microvectorsize;
                aoffset4 = aoffset2 + microvectorsize;
                aoffset6 = aoffset4 + microvectorsize;
                aoffset8 = aoffset6 + microvectorsize;
                aoffset10 = aoffset8 + microvectorsize;
                twx = 1.0;
                twxm1 = 0.0;
                twy = 0.0;
                for (mvidx = 0; mvidx <= m - 1; mvidx++)
                {
                    a0x = a[aoffset0 + 0];
                    a0y = a[aoffset0 + 1];
                    a1x = a[aoffset2 + 0];
                    a1y = a[aoffset2 + 1];
                    a2x = a[aoffset4 + 0];
                    a2y = a[aoffset4 + 1];
                    a3x = a[aoffset6 + 0];
                    a3y = a[aoffset6 + 1];
                    a4x = a[aoffset8 + 0];
                    a4y = a[aoffset8 + 1];
                    a5x = a[aoffset10 + 0];
                    a5y = a[aoffset10 + 1];
                    v0 = a0x;
                    v1 = a0y;
                    a0x = a0x + a3x;
                    a0y = a0y + a3y;
                    a3x = v0 - a3x;
                    a3y = v1 - a3y;
                    v0 = a1x;
                    v1 = a1y;
                    a1x = a1x + a4x;
                    a1y = a1y + a4y;
                    a4x = v0 - a4x;
                    a4y = v1 - a4y;
                    v0 = a2x;
                    v1 = a2y;
                    a2x = a2x + a5x;
                    a2y = a2y + a5y;
                    a5x = v0 - a5x;
                    a5y = v1 - a5y;
                    t4x = a4x * c3 - a4y * c4;
                    t4y = a4x * c4 + a4y * c3;
                    a4x = t4x;
                    a4y = t4y;
                    t5x = -(a5x * c3) - a5y * c4;
                    t5y = a5x * c4 - a5y * c3;
                    a5x = t5x;
                    a5y = t5y;
                    t1x = a1x + a2x;
                    t1y = a1y + a2y;
                    a0x = a0x + t1x;
                    a0y = a0y + t1y;
                    m1x = c1 * t1x;
                    m1y = c1 * t1y;
                    m2x = c2 * (a1y - a2y);
                    m2y = c2 * (a2x - a1x);
                    s1x = a0x + m1x;
                    s1y = a0y + m1y;
                    a1x = s1x + m2x;
                    a1y = s1y + m2y;
                    a2x = s1x - m2x;
                    a2y = s1y - m2y;
                    t1x = a4x + a5x;
                    t1y = a4y + a5y;
                    a3x = a3x + t1x;
                    a3y = a3y + t1y;
                    m1x = c1 * t1x;
                    m1y = c1 * t1y;
                    m2x = c2 * (a4y - a5y);
                    m2y = c2 * (a5x - a4x);
                    s1x = a3x + m1x;
                    s1y = a3y + m1y;
                    a4x = s1x + m2x;
                    a4y = s1y + m2y;
                    a5x = s1x - m2x;
                    a5y = s1y - m2y;
                    tw2x = twx * twx - twy * twy;
                    tw2y = 2 * twx * twy;
                    tw3x = twx * tw2x - twy * tw2y;
                    tw3y = twx * tw2y + twy * tw2x;
                    tw4x = tw2x * tw2x - tw2y * tw2y;
                    tw4y = 2 * tw2x * tw2y;
                    tw5x = tw3x * tw2x - tw3y * tw2y;
                    tw5y = tw3x * tw2y + tw3y * tw2x;
                    a[aoffset0 + 0] = a0x;
                    a[aoffset0 + 1] = a0y;
                    a[aoffset2 + 0] = a3x * twx - a3y * twy;
                    a[aoffset2 + 1] = a3y * twx + a3x * twy;
                    a[aoffset4 + 0] = a1x * tw2x - a1y * tw2y;
                    a[aoffset4 + 1] = a1y * tw2x + a1x * tw2y;
                    a[aoffset6 + 0] = a4x * tw3x - a4y * tw3y;
                    a[aoffset6 + 1] = a4y * tw3x + a4x * tw3y;
                    a[aoffset8 + 0] = a2x * tw4x - a2y * tw4y;
                    a[aoffset8 + 1] = a2y * tw4x + a2x * tw4y;
                    a[aoffset10 + 0] = a5x * tw5x - a5y * tw5y;
                    a[aoffset10 + 1] = a5y * tw5x + a5x * tw5y;
                    aoffset0 = aoffset0 + 2;
                    aoffset2 = aoffset2 + 2;
                    aoffset4 = aoffset4 + 2;
                    aoffset6 = aoffset6 + 2;
                    aoffset8 = aoffset8 + 2;
                    aoffset10 = aoffset10 + 2;
                    if ((mvidx + 1) % updatetw == 0)
                    {
                        v = -(2 * Math.PI * (mvidx + 1) / (n * m));
                        twxm1 = Math.Sin(0.5 * v);
                        twxm1 = -(2 * twxm1 * twxm1);
                        twy = Math.Sin(v);
                        twx = twxm1 + 1;
                    }
                    else
                    {
                        v = twxm1 + tw0 + twxm1 * tw0 - twy * tw1;
                        twy = twy + tw1 + twxm1 * tw1 + twy * tw0;
                        twxm1 = v;
                        twx = v + 1;
                    }
                }
            }
            return;
        }
    }


    /*************************************************************************
    This subroutine precomputes data for complex Bluestein's  FFT  and  writes
    them to array PrecR[] at specified offset. It  is  responsibility  of  the
    caller to make sure that PrecR[] is large enough.

    INPUT PARAMETERS:
        N           -   original size of the transform
        M           -   size of the "padded" Bluestein's transform
        PrecR       -   preallocated array
        Offs        -   offset
        
    OUTPUT PARAMETERS:
        PrecR       -   data at Offs:Offs+4*M-1 are modified:
                        * PrecR[Offs:Offs+2*M-1] stores Z[k]=exp(i*pi*k^2/N)
                        * PrecR[Offs+2*M:Offs+4*M-1] stores FFT of the Z
                        Other parts of PrecR are unchanged.
                        
    NOTE: this function performs internal M-point FFT. It allocates temporary
          plan which is destroyed after leaving this function.

      -- ALGLIB --
         Copyright 08.05.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftprecomputebluesteinsfft(int n,
        int m,
        double[] precr,
        int offs,
        xparams _params)
    {
        int i = 0;
        double bx = 0;
        double by = 0;
        fasttransformplan plan = new fasttransformplan();


        //
        // Fill first half of PrecR with b[k] = exp(i*pi*k^2/N)
        //
        for (i = 0; i <= 2 * m - 1; i++)
        {
            precr[offs + i] = 0;
        }
        for (i = 0; i <= n - 1; i++)
        {
            bx = Math.Cos(Math.PI / n * i * i);
            by = Math.Sin(Math.PI / n * i * i);
            precr[offs + 2 * i + 0] = bx;
            precr[offs + 2 * i + 1] = by;
            precr[offs + 2 * ((m - i) % m) + 0] = bx;
            precr[offs + 2 * ((m - i) % m) + 1] = by;
        }

        //
        // Precomputed FFT
        //
        ftcomplexfftplan(m, 1, plan, _params);
        for (i = 0; i <= 2 * m - 1; i++)
        {
            precr[offs + 2 * m + i] = precr[offs + i];
        }
        ftapplysubplan(plan, 0, precr, offs + 2 * m, 0, plan.buffer, 1, _params);
    }


    /*************************************************************************
    This subroutine applies complex Bluestein's FFT to input/output array A.

    INPUT PARAMETERS:
        Plan        -   transformation plan
        A           -   array, must be large enough for plan to work
        ABase       -   base offset in array A, this value points to start of
                        subarray whose length is equal to length of the plan
        AOffset     -   offset with respect to ABase, 0<=AOffset<PlanLength.
                        This is an offset within large PlanLength-subarray of
                        the chunk to process.
        OperandsCnt -   number of repeated operands (length N each)
        N           -   original data length (measured in complex numbers)
        M           -   padded data length (measured in complex numbers)
        PrecOffs    -   offset of the precomputed data for the plan
        SubPlan     -   position of the length-M FFT subplan which is used by
                        transformation
        BufA        -   temporary buffer, at least 2*M elements
        BufB        -   temporary buffer, at least 2*M elements
        BufC        -   temporary buffer, at least 2*M elements
        BufD        -   temporary buffer, at least 2*M elements
        
    OUTPUT PARAMETERS:
        A           -   transformed array

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftbluesteinsfft(fasttransformplan plan,
        double[] a,
        int abase,
        int aoffset,
        int operandscnt,
        int n,
        int m,
        int precoffs,
        int subplan,
        double[] bufa,
        double[] bufb,
        double[] bufc,
        double[] bufd,
        xparams _params)
    {
        int op = 0;
        int i = 0;
        double x = 0;
        double y = 0;
        double bx = 0;
        double by = 0;
        double ax = 0;
        double ay = 0;
        double rx = 0;
        double ry = 0;
        int p0 = 0;
        int p1 = 0;
        int p2 = 0;

        for (op = 0; op <= operandscnt - 1; op++)
        {

            //
            // Multiply A by conj(Z), store to buffer.
            // Pad A by zeros.
            //
            // NOTE: Z[k]=exp(i*pi*k^2/N)
            //
            p0 = abase + aoffset + op * 2 * n;
            p1 = precoffs;
            for (i = 0; i <= n - 1; i++)
            {
                x = a[p0 + 0];
                y = a[p0 + 1];
                bx = plan.precr[p1 + 0];
                by = -plan.precr[p1 + 1];
                bufa[2 * i + 0] = x * bx - y * by;
                bufa[2 * i + 1] = x * by + y * bx;
                p0 = p0 + 2;
                p1 = p1 + 2;
            }
            for (i = 2 * n; i <= 2 * m - 1; i++)
            {
                bufa[i] = 0;
            }

            //
            // Perform convolution of A and Z (using precomputed
            // FFT of Z stored in Plan structure).
            //
            ftapplysubplan(plan, subplan, bufa, 0, 0, bufc, 1, _params);
            p0 = 0;
            p1 = precoffs + 2 * m;
            for (i = 0; i <= m - 1; i++)
            {
                ax = bufa[p0 + 0];
                ay = bufa[p0 + 1];
                bx = plan.precr[p1 + 0];
                by = plan.precr[p1 + 1];
                bufa[p0 + 0] = ax * bx - ay * by;
                bufa[p0 + 1] = -(ax * by + ay * bx);
                p0 = p0 + 2;
                p1 = p1 + 2;
            }
            ftapplysubplan(plan, subplan, bufa, 0, 0, bufc, 1, _params);

            //
            // Post processing:
            //     A:=conj(Z)*conj(A)/M
            // Here conj(A)/M corresponds to last stage of inverse DFT,
            // and conj(Z) comes from Bluestein's FFT algorithm.
            //
            p0 = precoffs;
            p1 = 0;
            p2 = abase + aoffset + op * 2 * n;
            for (i = 0; i <= n - 1; i++)
            {
                bx = plan.precr[p0 + 0];
                by = plan.precr[p0 + 1];
                rx = bufa[p1 + 0] / m;
                ry = -(bufa[p1 + 1] / m);
                a[p2 + 0] = rx * bx - ry * -by;
                a[p2 + 1] = rx * -by + ry * bx;
                p0 = p0 + 2;
                p1 = p1 + 2;
                p2 = p2 + 2;
            }
        }
    }


    /*************************************************************************
    This subroutine precomputes data for complex Rader's FFT and  writes  them
    to array PrecR[] at specified offset. It  is  responsibility of the caller
    to make sure that PrecR[] is large enough.

    INPUT PARAMETERS:
        N           -   original size of the transform (before reduction to N-1)
        RQ          -   primitive root modulo N
        RIQ         -   inverse of primitive root modulo N
        PrecR       -   preallocated array
        Offs        -   offset
        
    OUTPUT PARAMETERS:
        PrecR       -   data at Offs:Offs+2*(N-1)-1 store FFT of Rader's factors,
                        other parts of PrecR are unchanged.
                        
    NOTE: this function performs internal (N-1)-point FFT. It allocates temporary
          plan which is destroyed after leaving this function.

      -- ALGLIB --
         Copyright 08.05.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftprecomputeradersfft(int n,
        int rq,
        int riq,
        double[] precr,
        int offs,
        xparams _params)
    {
        int q = 0;
        fasttransformplan plan = new fasttransformplan();
        int kiq = 0;
        double v = 0;


        //
        // Fill PrecR with Rader factors, perform FFT
        //
        kiq = 1;
        for (q = 0; q <= n - 2; q++)
        {
            v = -(2 * Math.PI * kiq / n);
            precr[offs + 2 * q + 0] = Math.Cos(v);
            precr[offs + 2 * q + 1] = Math.Sin(v);
            kiq = kiq * riq % n;
        }
        ftcomplexfftplan(n - 1, 1, plan, _params);
        ftapplysubplan(plan, 0, precr, offs, 0, plan.buffer, 1, _params);
    }


    /*************************************************************************
    This subroutine applies complex Rader's FFT to input/output array A.

    INPUT PARAMETERS:
        A           -   array, must be large enough for plan to work
        ABase       -   base offset in array A, this value points to start of
                        subarray whose length is equal to length of the plan
        AOffset     -   offset with respect to ABase, 0<=AOffset<PlanLength.
                        This is an offset within large PlanLength-subarray of
                        the chunk to process.
        OperandsCnt -   number of repeated operands (length N each)
        N           -   original data length (measured in complex numbers)
        SubPlan     -   position of the (N-1)-point FFT subplan which is used
                        by transformation
        RQ          -   primitive root modulo N
        RIQ         -   inverse of primitive root modulo N
        PrecOffs    -   offset of the precomputed data for the plan
        Buf         -   temporary array
        
    OUTPUT PARAMETERS:
        A           -   transformed array

      -- ALGLIB --
         Copyright 05.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftradersfft(fasttransformplan plan,
        double[] a,
        int abase,
        int aoffset,
        int operandscnt,
        int n,
        int subplan,
        int rq,
        int riq,
        int precoffs,
        double[] buf,
        xparams _params)
    {
        int opidx = 0;
        int i = 0;
        int q = 0;
        int kq = 0;
        int kiq = 0;
        double x0 = 0;
        double y0 = 0;
        int p0 = 0;
        int p1 = 0;
        double ax = 0;
        double ay = 0;
        double bx = 0;
        double by = 0;
        double rx = 0;
        double ry = 0;

        ap.assert(operandscnt >= 1, "FTApplyComplexRefFFT: OperandsCnt<1");

        //
        // Process operands
        //
        for (opidx = 0; opidx <= operandscnt - 1; opidx++)
        {

            //
            // fill QA
            //
            kq = 1;
            p0 = abase + aoffset + opidx * n * 2;
            p1 = aoffset + opidx * n * 2;
            rx = a[p0 + 0];
            ry = a[p0 + 1];
            x0 = rx;
            y0 = ry;
            for (q = 0; q <= n - 2; q++)
            {
                ax = a[p0 + 2 * kq + 0];
                ay = a[p0 + 2 * kq + 1];
                buf[p1 + 0] = ax;
                buf[p1 + 1] = ay;
                rx = rx + ax;
                ry = ry + ay;
                kq = kq * rq % n;
                p1 = p1 + 2;
            }
            p0 = abase + aoffset + opidx * n * 2;
            p1 = aoffset + opidx * n * 2;
            for (q = 0; q <= n - 2; q++)
            {
                a[p0] = buf[p1];
                a[p0 + 1] = buf[p1 + 1];
                p0 = p0 + 2;
                p1 = p1 + 2;
            }

            //
            // Convolution
            //
            ftapplysubplan(plan, subplan, a, abase, aoffset + opidx * n * 2, buf, 1, _params);
            p0 = abase + aoffset + opidx * n * 2;
            p1 = precoffs;
            for (i = 0; i <= n - 2; i++)
            {
                ax = a[p0 + 0];
                ay = a[p0 + 1];
                bx = plan.precr[p1 + 0];
                by = plan.precr[p1 + 1];
                a[p0 + 0] = ax * bx - ay * by;
                a[p0 + 1] = -(ax * by + ay * bx);
                p0 = p0 + 2;
                p1 = p1 + 2;
            }
            ftapplysubplan(plan, subplan, a, abase, aoffset + opidx * n * 2, buf, 1, _params);
            p0 = abase + aoffset + opidx * n * 2;
            for (i = 0; i <= n - 2; i++)
            {
                a[p0 + 0] = a[p0 + 0] / (n - 1);
                a[p0 + 1] = -(a[p0 + 1] / (n - 1));
                p0 = p0 + 2;
            }

            //
            // Result
            //
            buf[aoffset + opidx * n * 2 + 0] = rx;
            buf[aoffset + opidx * n * 2 + 1] = ry;
            kiq = 1;
            p0 = aoffset + opidx * n * 2;
            p1 = abase + aoffset + opidx * n * 2;
            for (q = 0; q <= n - 2; q++)
            {
                buf[p0 + 2 * kiq + 0] = x0 + a[p1 + 0];
                buf[p0 + 2 * kiq + 1] = y0 + a[p1 + 1];
                kiq = kiq * riq % n;
                p1 = p1 + 2;
            }
            p0 = abase + aoffset + opidx * n * 2;
            p1 = aoffset + opidx * n * 2;
            for (q = 0; q <= n - 1; q++)
            {
                a[p0] = buf[p1];
                a[p0 + 1] = buf[p1 + 1];
                p0 = p0 + 2;
                p1 = p1 + 2;
            }
        }
    }


    /*************************************************************************
    Factorizes task size N into product of two smaller sizes N1 and N2

    INPUT PARAMETERS:
        N       -   task size, N>0
        IsRoot  -   whether taks is root task (first one in a sequence)
        
    OUTPUT PARAMETERS:
        N1, N2  -   such numbers that:
                    * for prime N:                  N1=N2=0
                    * for composite N<=MaxRadix:    N1=N2=0
                    * for composite N>MaxRadix:     1<=N1<=N2, N1*N2=N

      -- ALGLIB --
         Copyright 08.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void ftfactorize(int n,
        bool isroot,
        ref int n1,
        ref int n2,
        xparams _params)
    {
        int j = 0;
        int k = 0;

        n1 = 0;
        n2 = 0;

        ap.assert(n > 0, "FTFactorize: N<=0");
        n1 = 0;
        n2 = 0;

        //
        // Small N
        //
        if (n <= maxradix)
        {
            return;
        }

        //
        // Large N, recursive split
        //
        if (n > recursivethreshold)
        {
            k = (int)Math.Ceiling(Math.Sqrt(n)) + 1;
            ap.assert(k * k >= n, "FTFactorize: internal error during recursive factorization");
            for (j = k; j >= 2; j--)
            {
                if (n % j == 0)
                {
                    n1 = Math.Min(n / j, j);
                    n2 = Math.Max(n / j, j);
                    return;
                }
            }
        }

        //
        // N>MaxRadix, try to find good codelet
        //
        for (j = maxradix; j >= 2; j--)
        {
            if (n % j == 0)
            {
                n1 = j;
                n2 = n / j;
                break;
            }
        }

        //
        // In case no good codelet was found,
        // try to factorize N into product of ANY primes.
        //
        if (n1 * n2 != n)
        {
            for (j = 2; j <= n - 1; j++)
            {
                if (n % j == 0)
                {
                    n1 = j;
                    n2 = n / j;
                    break;
                }
                if (j * j > n)
                {
                    break;
                }
            }
        }

        //
        // normalize
        //
        if (n1 > n2)
        {
            j = n1;
            n1 = n2;
            n2 = j;
        }
    }


    /*************************************************************************
    Returns optimistic estimate of the FFT cost, in UNITs (1 UNIT = 100 KFLOPs)

    INPUT PARAMETERS:
        N       -   task size, N>0
        
    RESULU:
        cost in UNITs, rounded down to nearest integer

    NOTE: If FFT cost is less than 1 UNIT, it will return 0 as result.

      -- ALGLIB --
         Copyright 08.04.2013 by Bochkanov Sergey
    *************************************************************************/
    private static int ftoptimisticestimate(int n,
        xparams _params)
    {
        int result = 0;

        ap.assert(n > 0, "FTOptimisticEstimate: N<=0");
        result = (int)Math.Floor(1.0E-5 * 5 * n * Math.Log(n) / Math.Log(2));
        return result;
    }


    /*************************************************************************
    Twiddle factors calculation

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void ffttwcalc(double[] a,
        int aoffset,
        int n1,
        int n2,
        xparams _params)
    {
        int i = 0;
        int j2 = 0;
        int n = 0;
        int halfn1 = 0;
        int offs = 0;
        double x = 0;
        double y = 0;
        double twxm1 = 0;
        double twy = 0;
        double twbasexm1 = 0;
        double twbasey = 0;
        double twrowxm1 = 0;
        double twrowy = 0;
        double tmpx = 0;
        double tmpy = 0;
        double v = 0;
        int updatetw2 = 0;


        //
        // Multiplication by twiddle factors for complex Cooley-Tukey FFT
        // with N factorized as N1*N2.
        //
        // Naive solution to this problem is given below:
        //
        //     > for K:=1 to N2-1 do
        //     >     for J:=1 to N1-1 do
        //     >     begin
        //     >         Idx:=K*N1+J;
        //     >         X:=A[AOffset+2*Idx+0];
        //     >         Y:=A[AOffset+2*Idx+1];
        //     >         TwX:=Cos(-2*Pi()*K*J/(N1*N2));
        //     >         TwY:=Sin(-2*Pi()*K*J/(N1*N2));
        //     >         A[AOffset+2*Idx+0]:=X*TwX-Y*TwY;
        //     >         A[AOffset+2*Idx+1]:=X*TwY+Y*TwX;
        //     >     end;
        //
        // However, there are exist more efficient solutions.
        //
        // Each pass of the inner cycle corresponds to multiplication of one
        // entry of A by W[k,j]=exp(-I*2*pi*k*j/N). This factor can be rewritten
        // as exp(-I*2*pi*k/N)^j. So we can replace costly exponentiation by
        // repeated multiplication: W[k,j+1]=W[k,j]*exp(-I*2*pi*k/N), with
        // second factor being computed once in the beginning of the iteration.
        //
        // Also, exp(-I*2*pi*k/N) can be represented as exp(-I*2*pi/N)^k, i.e.
        // we have W[K+1,1]=W[K,1]*W[1,1].
        //
        // In our loop we use following variables:
        // * [TwBaseXM1,TwBaseY] =   [cos(2*pi/N)-1,     sin(2*pi/N)]
        // * [TwRowXM1, TwRowY]  =   [cos(2*pi*I/N)-1,   sin(2*pi*I/N)]
        // * [TwXM1,    TwY]     =   [cos(2*pi*I*J/N)-1, sin(2*pi*I*J/N)]
        //
        // Meaning of the variables:
        // * [TwXM1,TwY] is current twiddle factor W[I,J]
        // * [TwRowXM1, TwRowY] is W[I,1]
        // * [TwBaseXM1,TwBaseY] is W[1,1]
        //
        // During inner loop we multiply current twiddle factor by W[I,1],
        // during outer loop we update W[I,1].
        //
        //
        ap.assert(updatetw >= 2, "FFTTwCalc: internal error - UpdateTw<2");
        updatetw2 = updatetw / 2;
        halfn1 = n1 / 2;
        n = n1 * n2;
        v = -(2 * Math.PI / n);
        twbasexm1 = -(2 * math.sqr(Math.Sin(0.5 * v)));
        twbasey = Math.Sin(v);
        twrowxm1 = 0;
        twrowy = 0;
        offs = aoffset;
        for (i = 0; i <= n2 - 1; i++)
        {

            //
            // Initialize twiddle factor for current row
            //
            twxm1 = 0;
            twy = 0;

            //
            // N1-point block is separated into 2-point chunks and residual 1-point chunk
            // (in case N1 is odd). Unrolled loop is several times faster.
            //
            for (j2 = 0; j2 <= halfn1 - 1; j2++)
            {

                //
                // Processing:
                // * process first element in a chunk.
                // * update twiddle factor (unconditional update)
                // * process second element
                // * conditional update of the twiddle factor
                //
                x = a[offs + 0];
                y = a[offs + 1];
                tmpx = x * (1 + twxm1) - y * twy;
                tmpy = x * twy + y * (1 + twxm1);
                a[offs + 0] = tmpx;
                a[offs + 1] = tmpy;
                tmpx = (1 + twxm1) * twrowxm1 - twy * twrowy;
                twy = twy + (1 + twxm1) * twrowy + twy * twrowxm1;
                twxm1 = twxm1 + tmpx;
                x = a[offs + 2];
                y = a[offs + 3];
                tmpx = x * (1 + twxm1) - y * twy;
                tmpy = x * twy + y * (1 + twxm1);
                a[offs + 2] = tmpx;
                a[offs + 3] = tmpy;
                offs = offs + 4;
                if ((j2 + 1) % updatetw2 == 0 && j2 < halfn1 - 1)
                {

                    //
                    // Recalculate twiddle factor
                    //
                    v = -(2 * Math.PI * i * 2 * (j2 + 1) / n);
                    twxm1 = Math.Sin(0.5 * v);
                    twxm1 = -(2 * twxm1 * twxm1);
                    twy = Math.Sin(v);
                }
                else
                {

                    //
                    // Update twiddle factor
                    //
                    tmpx = (1 + twxm1) * twrowxm1 - twy * twrowy;
                    twy = twy + (1 + twxm1) * twrowy + twy * twrowxm1;
                    twxm1 = twxm1 + tmpx;
                }
            }
            if (n1 % 2 == 1)
            {

                //
                // Handle residual chunk
                //
                x = a[offs + 0];
                y = a[offs + 1];
                tmpx = x * (1 + twxm1) - y * twy;
                tmpy = x * twy + y * (1 + twxm1);
                a[offs + 0] = tmpx;
                a[offs + 1] = tmpy;
                offs = offs + 2;
            }

            //
            // update TwRow: TwRow(new) = TwRow(old)*TwBase
            //
            if (i < n2 - 1)
            {
                if ((i + 1) % updatetw == 0)
                {
                    v = -(2 * Math.PI * (i + 1) / n);
                    twrowxm1 = Math.Sin(0.5 * v);
                    twrowxm1 = -(2 * twrowxm1 * twrowxm1);
                    twrowy = Math.Sin(v);
                }
                else
                {
                    tmpx = twbasexm1 + twrowxm1 * twbasexm1 - twrowy * twbasey;
                    tmpy = twbasey + twrowxm1 * twbasey + twrowy * twbasexm1;
                    twrowxm1 = twrowxm1 + tmpx;
                    twrowy = twrowy + tmpy;
                }
            }
        }
    }


    /*************************************************************************
    Linear transpose: transpose complex matrix stored in 1-dimensional array

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void internalcomplexlintranspose(double[] a,
        int m,
        int n,
        int astart,
        double[] buf,
        xparams _params)
    {
        int i_ = 0;
        int i1_ = 0;

        ffticltrec(a, astart, n, buf, 0, m, m, n, _params);
        i1_ = (0) - (astart);
        for (i_ = astart; i_ <= astart + 2 * m * n - 1; i_++)
        {
            a[i_] = buf[i_ + i1_];
        }
    }


    /*************************************************************************
    Recurrent subroutine for a InternalComplexLinTranspose

    Write A^T to B, where:
    * A is m*n complex matrix stored in array A as pairs of real/image values,
      beginning from AStart position, with AStride stride
    * B is n*m complex matrix stored in array B as pairs of real/image values,
      beginning from BStart position, with BStride stride
    stride is measured in complex numbers, i.e. in real/image pairs.

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void ffticltrec(double[] a,
        int astart,
        int astride,
        double[] b,
        int bstart,
        int bstride,
        int m,
        int n,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int idx1 = 0;
        int idx2 = 0;
        int m2 = 0;
        int m1 = 0;
        int n1 = 0;

        if (m == 0 || n == 0)
        {
            return;
        }
        if (Math.Max(m, n) <= 8)
        {
            m2 = 2 * bstride;
            for (i = 0; i <= m - 1; i++)
            {
                idx1 = bstart + 2 * i;
                idx2 = astart + 2 * i * astride;
                for (j = 0; j <= n - 1; j++)
                {
                    b[idx1 + 0] = a[idx2 + 0];
                    b[idx1 + 1] = a[idx2 + 1];
                    idx1 = idx1 + m2;
                    idx2 = idx2 + 2;
                }
            }
            return;
        }
        if (n > m)
        {

            //
            // New partition:
            //
            // "A^T -> B" becomes "(A1 A2)^T -> ( B1 )
            //                                  ( B2 )
            //
            n1 = n / 2;
            if (n - n1 >= 8 && n1 % 8 != 0)
            {
                n1 = n1 + (8 - n1 % 8);
            }
            ap.assert(n - n1 > 0);
            ffticltrec(a, astart, astride, b, bstart, bstride, m, n1, _params);
            ffticltrec(a, astart + 2 * n1, astride, b, bstart + 2 * n1 * bstride, bstride, m, n - n1, _params);
        }
        else
        {

            //
            // New partition:
            //
            // "A^T -> B" becomes "( A1 )^T -> ( B1 B2 )
            //                     ( A2 )
            //
            m1 = m / 2;
            if (m - m1 >= 8 && m1 % 8 != 0)
            {
                m1 = m1 + (8 - m1 % 8);
            }
            ap.assert(m - m1 > 0);
            ffticltrec(a, astart, astride, b, bstart, bstride, m1, n, _params);
            ffticltrec(a, astart + 2 * m1 * astride, astride, b, bstart + 2 * m1, bstride, m - m1, n, _params);
        }
    }


    /*************************************************************************
    recurrent subroutine for FFTFindSmoothRec

      -- ALGLIB --
         Copyright 01.05.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void ftbasefindsmoothrec(int n,
        int seed,
        int leastfactor,
        ref int best,
        xparams _params)
    {
        ap.assert(ftbasemaxsmoothfactor <= 5, "FTBaseFindSmoothRec: internal error!");
        if (seed >= n)
        {
            best = Math.Min(best, seed);
            return;
        }
        if (leastfactor <= 2)
        {
            ftbasefindsmoothrec(n, seed * 2, 2, ref best, _params);
        }
        if (leastfactor <= 3)
        {
            ftbasefindsmoothrec(n, seed * 3, 3, ref best, _params);
        }
        if (leastfactor <= 5)
        {
            ftbasefindsmoothrec(n, seed * 5, 5, ref best, _params);
        }
    }


}
