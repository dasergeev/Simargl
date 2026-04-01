using System;

#pragma warning disable CS3008
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




/*************************************************************************
Sparse Cholesky/LDLT kernels
*************************************************************************/
public partial class spchol
{
    private static int spsymmgetmaxsimd(xparams _params)
    {
#if ALGLIB_USE_SIMD
        return 4;
#else
        return 1;
#endif
    }

    /*************************************************************************
    Solving linear system: propagating computed supernode.

    Propagates computed supernode to the rest of the RHS  using  SIMD-friendly
    RHS storage format.

    INPUT PARAMETERS:

    OUTPUT PARAMETERS:

      -- ALGLIB routine --
         08.09.2021
         Bochkanov Sergey
    *************************************************************************/
    private static void propagatefwd(double[] x,
        int cols0,
        int blocksize,
        int[] superrowidx,
        int rbase,
        int offdiagsize,
        double[] rowstorage,
        int offss,
        int sstride,
        double[] simdbuf,
        int simdwidth,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int baseoffs = 0;
        double v = 0;

        for (k = 0; k <= offdiagsize - 1; k++)
        {
            i = superrowidx[rbase + k];
            baseoffs = offss + (k + blocksize) * sstride;
            v = simdbuf[i * simdwidth];
            for (j = 0; j <= blocksize - 1; j++)
            {
                v = v - rowstorage[baseoffs + j] * x[cols0 + j];
            }
            simdbuf[i * simdwidth] = v;
        }
    }

    /*************************************************************************
    Fast kernels for small supernodal updates: special 4x4x4x4 function.

    ! See comments on UpdateSupernode() for information  on generic supernodal
    ! updates, including notation used below.

    The generic update has following form:

        S := S - scatter(U*D*Uc')

    This specialized function performs AxBxCx4 update, i.e.:
    * S is a tHeight*A matrix with row stride equal to 4 (usually it means that
      it has 3 or 4 columns)
    * U is a uHeight*B matrix
    * Uc' is a B*C matrix, with C<=A
    * scatter() scatters rows and columns of U*Uc'
      
    Return value:
    * True if update was applied
    * False if kernel refused to perform an update (quick exit for unsupported
      combinations of input sizes)

      -- ALGLIB routine --
         20.09.2020
         Bochkanov Sergey
    *************************************************************************/
#if ALGLIB_USE_SIMD
    private static unsafe bool try_updatekernelabc4(double* rowstorage,
        int offss,
        int twidth,
        int offsu,
        int uheight,
        int urank,
        int urowstride,
        int uwidth,
        double* diagd,
        int offsd,
        int* raw2smap,
        int* superrowidx,
        int urbase)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int k;
            int targetrow;
            int targetcol;
            
            /*
             * Filter out unsupported combinations (ones that are too sparse for the non-SIMD code)
             */
            if( twidth<3||twidth>4 )
                return false;
            if( uwidth<1||uwidth>4 )
                return false;
            if( urank>4 )
                return false;
            
            /*
             * Shift input arrays to the beginning of the working area.
             * Prepare SIMD masks
             */
            Intrinsics.Vector256<double> v_rankmask = Fma.CompareGreaterThan(
                Intrinsics.Vector256.Create((double)urank, (double)urank, (double)urank, (double)urank),
                Intrinsics.Vector256.Create(0.0, 1.0, 2.0, 3.0));
            double *update_storage = rowstorage+offsu;
            double *target_storage = rowstorage+offss;
            superrowidx += urbase;
            
            /*
             * Load head of the update matrix
             */
            Intrinsics.Vector256<double> v_d0123 = Fma.MaskLoad(diagd+offsd, v_rankmask);
            Intrinsics.Vector256<double> u_0_0123 = Intrinsics.Vector256<double>.Zero;
            Intrinsics.Vector256<double> u_1_0123 = Intrinsics.Vector256<double>.Zero;
            Intrinsics.Vector256<double> u_2_0123 = Intrinsics.Vector256<double>.Zero;
            Intrinsics.Vector256<double> u_3_0123 = Intrinsics.Vector256<double>.Zero;
            for(k=0; k<=uwidth-1; k++)
            {
                targetcol = raw2smap[superrowidx[k]];
                if( targetcol==0 )
                    u_0_0123 = Fma.Multiply(v_d0123, Fma.MaskLoad(update_storage+k*urowstride, v_rankmask));
                if( targetcol==1 )
                    u_1_0123 = Fma.Multiply(v_d0123, Fma.MaskLoad(update_storage+k*urowstride, v_rankmask));
                if( targetcol==2 )
                    u_2_0123 = Fma.Multiply(v_d0123, Fma.MaskLoad(update_storage+k*urowstride, v_rankmask));
                if( targetcol==3 )
                    u_3_0123 = Fma.Multiply(v_d0123, Fma.MaskLoad(update_storage+k*urowstride, v_rankmask));
            }
            
            /*
             * Transpose head
             */
            Intrinsics.Vector256<double> u01_lo = Fma.UnpackLow( u_0_0123,u_1_0123);
            Intrinsics.Vector256<double> u01_hi = Fma.UnpackHigh(u_0_0123,u_1_0123);
            Intrinsics.Vector256<double> u23_lo = Fma.UnpackLow( u_2_0123,u_3_0123);
            Intrinsics.Vector256<double> u23_hi = Fma.UnpackHigh(u_2_0123,u_3_0123);
            Intrinsics.Vector256<double> u_0123_0 = Fma.Permute2x128(u01_lo, u23_lo, 0x20);
            Intrinsics.Vector256<double> u_0123_1 = Fma.Permute2x128(u01_hi, u23_hi, 0x20);
            Intrinsics.Vector256<double> u_0123_2 = Fma.Permute2x128(u23_lo, u01_lo, 0x13);
            Intrinsics.Vector256<double> u_0123_3 = Fma.Permute2x128(u23_hi, u01_hi, 0x13);
            
            /*
             * Run update
             */
            if( urank==1 )
            {
                for(k=0; k<=uheight-1; k++)
                {
                    targetrow = raw2smap[superrowidx[k]]*4;
                    double *update_row = rowstorage+offsu+k*urowstride;
                    Fma.Store(target_storage+targetrow,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+0), u_0123_0,
                            Fma.LoadVector256(target_storage+targetrow)));
                }
            }
            if( urank==2 )
            {
                for(k=0; k<=uheight-1; k++)
                {
                    targetrow = raw2smap[superrowidx[k]]*4;
                    double *update_row = rowstorage+offsu+k*urowstride;
                    Fma.Store(target_storage+targetrow,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+1), u_0123_1,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+0), u_0123_0,
                            Fma.LoadVector256(target_storage+targetrow))));
                }
            }
            if( urank==3 )
            {
                for(k=0; k<=uheight-1; k++)
                {
                    targetrow = raw2smap[superrowidx[k]]*4;
                    double *update_row = rowstorage+offsu+k*urowstride;
                    Fma.Store(target_storage+targetrow,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+2), u_0123_2,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+1), u_0123_1,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+0), u_0123_0,
                            Fma.LoadVector256(target_storage+targetrow)))));
                }
            }
            if( urank==4 )
            {
                for(k=0; k<=uheight-1; k++)
                {
                    targetrow = raw2smap[superrowidx[k]]*4;
                    double *update_row = rowstorage+offsu+k*urowstride;
                    Fma.Store(target_storage+targetrow,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+3), u_0123_3,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+2), u_0123_2,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+1), u_0123_1,
                        Fma.MultiplyAddNegated(Fma.BroadcastScalarToVector256(update_row+0), u_0123_0,
                            Fma.LoadVector256(target_storage+targetrow))))));
                }
            }
            return true;
        }
#endif // no-fma
        if( Avx2.IsSupported )
        {
            int k;
            int targetrow;
            int targetcol;
            
            /*
             * Filter out unsupported combinations (ones that are too sparse for the non-SIMD code)
             */
            if( twidth<3||twidth>4 )
                return false;
            if( uwidth<1||uwidth>4 )
                return false;
            if( urank>4 )
                return false;
            
            /*
             * Shift input arrays to the beginning of the working area.
             * Prepare SIMD masks
             */
            Intrinsics.Vector256<double> v_rankmask = Avx2.CompareGreaterThan(
                Intrinsics.Vector256.Create((double)urank, (double)urank, (double)urank, (double)urank),
                Intrinsics.Vector256.Create(0.0, 1.0, 2.0, 3.0));
            double *update_storage = rowstorage+offsu;
            double *target_storage = rowstorage+offss;
            superrowidx += urbase;
            
            /*
             * Load head of the update matrix
             */
            Intrinsics.Vector256<double> v_d0123 = Avx2.MaskLoad(diagd+offsd, v_rankmask);
            Intrinsics.Vector256<double> u_0_0123 = Intrinsics.Vector256<double>.Zero;
            Intrinsics.Vector256<double> u_1_0123 = Intrinsics.Vector256<double>.Zero;
            Intrinsics.Vector256<double> u_2_0123 = Intrinsics.Vector256<double>.Zero;
            Intrinsics.Vector256<double> u_3_0123 = Intrinsics.Vector256<double>.Zero;
            for(k=0; k<=uwidth-1; k++)
            {
                targetcol = raw2smap[superrowidx[k]];
                if( targetcol==0 )
                    u_0_0123 = Avx2.Multiply(v_d0123, Avx2.MaskLoad(update_storage+k*urowstride, v_rankmask));
                if( targetcol==1 )
                    u_1_0123 = Avx2.Multiply(v_d0123, Avx2.MaskLoad(update_storage+k*urowstride, v_rankmask));
                if( targetcol==2 )
                    u_2_0123 = Avx2.Multiply(v_d0123, Avx2.MaskLoad(update_storage+k*urowstride, v_rankmask));
                if( targetcol==3 )
                    u_3_0123 = Avx2.Multiply(v_d0123, Avx2.MaskLoad(update_storage+k*urowstride, v_rankmask));
            }
            
            /*
             * Transpose head
             */
            Intrinsics.Vector256<double> u01_lo = Avx2.UnpackLow( u_0_0123,u_1_0123);
            Intrinsics.Vector256<double> u01_hi = Avx2.UnpackHigh(u_0_0123,u_1_0123);
            Intrinsics.Vector256<double> u23_lo = Avx2.UnpackLow( u_2_0123,u_3_0123);
            Intrinsics.Vector256<double> u23_hi = Avx2.UnpackHigh(u_2_0123,u_3_0123);
            Intrinsics.Vector256<double> u_0123_0 = Avx2.Permute2x128(u01_lo, u23_lo, 0x20);
            Intrinsics.Vector256<double> u_0123_1 = Avx2.Permute2x128(u01_hi, u23_hi, 0x20);
            Intrinsics.Vector256<double> u_0123_2 = Avx2.Permute2x128(u23_lo, u01_lo, 0x13);
            Intrinsics.Vector256<double> u_0123_3 = Avx2.Permute2x128(u23_hi, u01_hi, 0x13);
            
            /*
             * Run update
             */
            if( urank==1 )
            {
                for(k=0; k<=uheight-1; k++)
                {
                    targetrow = raw2smap[superrowidx[k]]*4;
                    double *update_row = rowstorage+offsu+k*urowstride;
                    Avx2.Store(target_storage+targetrow,
                        Avx2.Subtract(Avx2.LoadVector256(target_storage+targetrow),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+0), u_0123_0)));
                }
            }
            if( urank==2 )
            {
                for(k=0; k<=uheight-1; k++)
                {
                    targetrow = raw2smap[superrowidx[k]]*4;
                    double *update_row = rowstorage+offsu+k*urowstride;
                    Avx2.Store(target_storage+targetrow,
                        Avx2.Subtract(Avx2.Subtract(Avx2.LoadVector256(target_storage+targetrow),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+1), u_0123_1)),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+0), u_0123_0)));
                }
            }
            if( urank==3 )
            {
                for(k=0; k<=uheight-1; k++)
                {
                    targetrow = raw2smap[superrowidx[k]]*4;
                    double *update_row = rowstorage+offsu+k*urowstride;
                    Avx2.Store(target_storage+targetrow,
                        Avx2.Subtract(Avx2.Subtract(Avx2.Subtract(Avx2.LoadVector256(target_storage+targetrow),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+2), u_0123_2)),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+1), u_0123_1)),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+0), u_0123_0)));
                }
            }
            if( urank==4 )
            {
                for(k=0; k<=uheight-1; k++)
                {
                    targetrow = raw2smap[superrowidx[k]]*4;
                    double *update_row = rowstorage+offsu+k*urowstride;
                    Avx2.Store(target_storage+targetrow,
                        Avx2.Subtract(Avx2.Subtract(Avx2.Subtract(Avx2.Subtract(Avx2.LoadVector256(target_storage+targetrow),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+3), u_0123_3)),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+2), u_0123_2)),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+1), u_0123_1)),
                            Avx2.Multiply(Avx2.BroadcastScalarToVector256(update_row+0), u_0123_0)));
                }
            }
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif
    private static bool updatekernelabc4(double[] rowstorage,
        int offss,
        int twidth,
        int offsu,
        int uheight,
        int urank,
        int urowstride,
        int uwidth,
        double[] diagd,
        int offsd,
        int[] raw2smap,
        int[] superrowidx,
        int urbase,
        xparams _params)
    {
#if ALGLIB_USE_SIMD
        unsafe
        {
            fixed(double* p_rowstorage=rowstorage, p_diagd = diagd)
            fixed(int* p_raw2smap=raw2smap, p_superrowidx=superrowidx)
            {
                if( try_updatekernelabc4(p_rowstorage, offss, twidth, offsu, uheight, urank, urowstride, uwidth, p_diagd, offsd, p_raw2smap, p_superrowidx, urbase) )
                    return true;
            }
        }
#endif

        //
        // Fallback pure C# code
        //
        bool result = new bool();
        int k = 0;
        int targetrow = 0;
        int targetcol = 0;
        int offsk = 0;
        double d0 = 0;
        double d1 = 0;
        double d2 = 0;
        double d3 = 0;
        double u00 = 0;
        double u01 = 0;
        double u02 = 0;
        double u03 = 0;
        double u10 = 0;
        double u11 = 0;
        double u12 = 0;
        double u13 = 0;
        double u20 = 0;
        double u21 = 0;
        double u22 = 0;
        double u23 = 0;
        double u30 = 0;
        double u31 = 0;
        double u32 = 0;
        double u33 = 0;
        double uk0 = 0;
        double uk1 = 0;
        double uk2 = 0;
        double uk3 = 0;
        int srccol0 = 0;
        int srccol1 = 0;
        int srccol2 = 0;
        int srccol3 = 0;


        //
        // Filter out unsupported combinations (ones that are too sparse for the non-SIMD code)
        //
        result = false;
        if (twidth < 3 || twidth > 4)
        {
            return result;
        }
        if (uwidth < 3 || uwidth > 4)
        {
            return result;
        }
        if (urank > 4)
        {
            return result;
        }

        //
        // Determine source columns for target columns, -1 if target column
        // is not updated.
        //
        srccol0 = -1;
        srccol1 = -1;
        srccol2 = -1;
        srccol3 = -1;
        for (k = 0; k <= uwidth - 1; k++)
        {
            targetcol = raw2smap[superrowidx[urbase + k]];
            if (targetcol == 0)
            {
                srccol0 = k;
            }
            if (targetcol == 1)
            {
                srccol1 = k;
            }
            if (targetcol == 2)
            {
                srccol2 = k;
            }
            if (targetcol == 3)
            {
                srccol3 = k;
            }
        }

        //
        // Load update matrix into aligned/rearranged 4x4 storage
        //
        d0 = 0;
        d1 = 0;
        d2 = 0;
        d3 = 0;
        u00 = 0;
        u01 = 0;
        u02 = 0;
        u03 = 0;
        u10 = 0;
        u11 = 0;
        u12 = 0;
        u13 = 0;
        u20 = 0;
        u21 = 0;
        u22 = 0;
        u23 = 0;
        u30 = 0;
        u31 = 0;
        u32 = 0;
        u33 = 0;
        if (urank >= 1)
        {
            d0 = diagd[offsd + 0];
        }
        if (urank >= 2)
        {
            d1 = diagd[offsd + 1];
        }
        if (urank >= 3)
        {
            d2 = diagd[offsd + 2];
        }
        if (urank >= 4)
        {
            d3 = diagd[offsd + 3];
        }
        if (srccol0 >= 0)
        {
            if (urank >= 1)
            {
                u00 = d0 * rowstorage[offsu + srccol0 * urowstride + 0];
            }
            if (urank >= 2)
            {
                u01 = d1 * rowstorage[offsu + srccol0 * urowstride + 1];
            }
            if (urank >= 3)
            {
                u02 = d2 * rowstorage[offsu + srccol0 * urowstride + 2];
            }
            if (urank >= 4)
            {
                u03 = d3 * rowstorage[offsu + srccol0 * urowstride + 3];
            }
        }
        if (srccol1 >= 0)
        {
            if (urank >= 1)
            {
                u10 = d0 * rowstorage[offsu + srccol1 * urowstride + 0];
            }
            if (urank >= 2)
            {
                u11 = d1 * rowstorage[offsu + srccol1 * urowstride + 1];
            }
            if (urank >= 3)
            {
                u12 = d2 * rowstorage[offsu + srccol1 * urowstride + 2];
            }
            if (urank >= 4)
            {
                u13 = d3 * rowstorage[offsu + srccol1 * urowstride + 3];
            }
        }
        if (srccol2 >= 0)
        {
            if (urank >= 1)
            {
                u20 = d0 * rowstorage[offsu + srccol2 * urowstride + 0];
            }
            if (urank >= 2)
            {
                u21 = d1 * rowstorage[offsu + srccol2 * urowstride + 1];
            }
            if (urank >= 3)
            {
                u22 = d2 * rowstorage[offsu + srccol2 * urowstride + 2];
            }
            if (urank >= 4)
            {
                u23 = d3 * rowstorage[offsu + srccol2 * urowstride + 3];
            }
        }
        if (srccol3 >= 0)
        {
            if (urank >= 1)
            {
                u30 = d0 * rowstorage[offsu + srccol3 * urowstride + 0];
            }
            if (urank >= 2)
            {
                u31 = d1 * rowstorage[offsu + srccol3 * urowstride + 1];
            }
            if (urank >= 3)
            {
                u32 = d2 * rowstorage[offsu + srccol3 * urowstride + 2];
            }
            if (urank >= 4)
            {
                u33 = d3 * rowstorage[offsu + srccol3 * urowstride + 3];
            }
        }

        //
        // Run update
        //
        if (urank == 1)
        {
            for (k = 0; k <= uheight - 1; k++)
            {
                targetrow = offss + raw2smap[superrowidx[urbase + k]] * 4;
                offsk = offsu + k * urowstride;
                uk0 = rowstorage[offsk + 0];
                rowstorage[targetrow + 0] = rowstorage[targetrow + 0] - u00 * uk0;
                rowstorage[targetrow + 1] = rowstorage[targetrow + 1] - u10 * uk0;
                rowstorage[targetrow + 2] = rowstorage[targetrow + 2] - u20 * uk0;
                rowstorage[targetrow + 3] = rowstorage[targetrow + 3] - u30 * uk0;
            }
        }
        if (urank == 2)
        {
            for (k = 0; k <= uheight - 1; k++)
            {
                targetrow = offss + raw2smap[superrowidx[urbase + k]] * 4;
                offsk = offsu + k * urowstride;
                uk0 = rowstorage[offsk + 0];
                uk1 = rowstorage[offsk + 1];
                rowstorage[targetrow + 0] = rowstorage[targetrow + 0] - u00 * uk0 - u01 * uk1;
                rowstorage[targetrow + 1] = rowstorage[targetrow + 1] - u10 * uk0 - u11 * uk1;
                rowstorage[targetrow + 2] = rowstorage[targetrow + 2] - u20 * uk0 - u21 * uk1;
                rowstorage[targetrow + 3] = rowstorage[targetrow + 3] - u30 * uk0 - u31 * uk1;
            }
        }
        if (urank == 3)
        {
            for (k = 0; k <= uheight - 1; k++)
            {
                targetrow = offss + raw2smap[superrowidx[urbase + k]] * 4;
                offsk = offsu + k * urowstride;
                uk0 = rowstorage[offsk + 0];
                uk1 = rowstorage[offsk + 1];
                uk2 = rowstorage[offsk + 2];
                rowstorage[targetrow + 0] = rowstorage[targetrow + 0] - u00 * uk0 - u01 * uk1 - u02 * uk2;
                rowstorage[targetrow + 1] = rowstorage[targetrow + 1] - u10 * uk0 - u11 * uk1 - u12 * uk2;
                rowstorage[targetrow + 2] = rowstorage[targetrow + 2] - u20 * uk0 - u21 * uk1 - u22 * uk2;
                rowstorage[targetrow + 3] = rowstorage[targetrow + 3] - u30 * uk0 - u31 * uk1 - u32 * uk2;
            }
        }
        if (urank == 4)
        {
            for (k = 0; k <= uheight - 1; k++)
            {
                targetrow = offss + raw2smap[superrowidx[urbase + k]] * 4;
                offsk = offsu + k * urowstride;
                uk0 = rowstorage[offsk + 0];
                uk1 = rowstorage[offsk + 1];
                uk2 = rowstorage[offsk + 2];
                uk3 = rowstorage[offsk + 3];
                rowstorage[targetrow + 0] = rowstorage[targetrow + 0] - u00 * uk0 - u01 * uk1 - u02 * uk2 - u03 * uk3;
                rowstorage[targetrow + 1] = rowstorage[targetrow + 1] - u10 * uk0 - u11 * uk1 - u12 * uk2 - u13 * uk3;
                rowstorage[targetrow + 2] = rowstorage[targetrow + 2] - u20 * uk0 - u21 * uk1 - u22 * uk2 - u23 * uk3;
                rowstorage[targetrow + 3] = rowstorage[targetrow + 3] - u30 * uk0 - u31 * uk1 - u32 * uk2 - u33 * uk3;
            }
        }
        result = true;
        return result;
    }

    /*************************************************************************
    Fast kernels for small supernodal updates: special 4x4x4x4 function.

    ! See comments on UpdateSupernode() for information  on generic supernodal
    ! updates, including notation used below.

    The generic update has following form:

        S := S - scatter(U*D*Uc')

    This specialized function performs 4x4x4x4 update, i.e.:
    * S is a tHeight*4 matrix
    * U is a uHeight*4 matrix
    * Uc' is a 4*4 matrix
    * scatter() scatters rows of U*Uc', but does not scatter columns (they are
      densely packed).
      
    Return value:
    * True if update was applied
    * False if kernel refused to perform an update.

      -- ALGLIB routine --
         20.09.2020
         Bochkanov Sergey
    *************************************************************************/
#if ALGLIB_USE_SIMD
    private static unsafe bool try_updatekernel4444(double* rowstorage,
        int offss,
        int sheight,
        int offsu,
        int uheight,
        double *diagd,
        int offsd,
        int[] raw2smap,
        int[] superrowidx,
        int urbase)
    {
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
#if !ALGLIB_NO_FMA
        if( Fma.IsSupported )
        {
            int k, targetrow, offsk;
            Intrinsics.Vector256<double> v_negd_u0, v_negd_u1, v_negd_u2, v_negd_u3, v_negd;
            Intrinsics.Vector256<double> v_w0, v_w1, v_w2, v_w3, u01_lo, u01_hi, u23_lo, u23_hi;
            
            /*
             * Compute W = -D*transpose(U[0:3])
             */
            v_negd = Avx2.Multiply(Avx2.LoadVector256(diagd+offsd),Intrinsics.Vector256.Create(-1.0));
            v_negd_u0   = Avx2.Multiply(Avx2.LoadVector256(rowstorage+offsu+0*4),v_negd);
            v_negd_u1   = Avx2.Multiply(Avx2.LoadVector256(rowstorage+offsu+1*4),v_negd);
            v_negd_u2   = Avx2.Multiply(Avx2.LoadVector256(rowstorage+offsu+2*4),v_negd);
            v_negd_u3   = Avx2.Multiply(Avx2.LoadVector256(rowstorage+offsu+3*4),v_negd);
            u01_lo = Avx2.UnpackLow( v_negd_u0,v_negd_u1);
            u01_hi = Avx2.UnpackHigh(v_negd_u0,v_negd_u1);
            u23_lo = Avx2.UnpackLow( v_negd_u2,v_negd_u3);
            u23_hi = Avx2.UnpackHigh(v_negd_u2,v_negd_u3);
            v_w0 = Avx2.Permute2x128(u01_lo, u23_lo, 0x20);
            v_w1 = Avx2.Permute2x128(u01_hi, u23_hi, 0x20);
            v_w2 = Avx2.Permute2x128(u23_lo, u01_lo, 0x13);
            v_w3 = Avx2.Permute2x128(u23_hi, u01_hi, 0x13);
            
            //
            // Compute update S:= S + row_scatter(U*W)
            //
            if( sheight==uheight )
            {
                /*
                 * No row scatter, the most efficient code
                 */
                for(k=0; k<=uheight-1; k++)
                {
                    Intrinsics.Vector256<double> target;
                    
                    targetrow = offss+k*4;
                    offsk = offsu+k*4;
                    
                    target = Avx2.LoadVector256(rowstorage+targetrow);
                    target = Fma.MultiplyAdd(Avx2.BroadcastScalarToVector256(rowstorage+offsk+0),v_w0,target);
                    target = Fma.MultiplyAdd(Avx2.BroadcastScalarToVector256(rowstorage+offsk+1),v_w1,target);
                    target = Fma.MultiplyAdd(Avx2.BroadcastScalarToVector256(rowstorage+offsk+2),v_w2,target);
                    target = Fma.MultiplyAdd(Avx2.BroadcastScalarToVector256(rowstorage+offsk+3),v_w3,target);
                    Avx2.Store(rowstorage+targetrow, target);
                }
            }
            else
            {
                /*
                 * Row scatter is performed, less efficient code using double mapping to determine target row index
                 */
                for(k=0; k<=uheight-1; k++)
                {
                    Intrinsics.Vector256<double> target;
                    
                    targetrow = offss+raw2smap[superrowidx[urbase+k]]*4;
                    offsk = offsu+k*4;
                    
                    target = Avx2.LoadVector256(rowstorage+targetrow);
                    target = Fma.MultiplyAdd(Avx2.BroadcastScalarToVector256(rowstorage+offsk+0),v_w0,target);
                    target = Fma.MultiplyAdd(Avx2.BroadcastScalarToVector256(rowstorage+offsk+1),v_w1,target);
                    target = Fma.MultiplyAdd(Avx2.BroadcastScalarToVector256(rowstorage+offsk+2),v_w2,target);
                    target = Fma.MultiplyAdd(Avx2.BroadcastScalarToVector256(rowstorage+offsk+3),v_w3,target);
                    Avx2.Store(rowstorage+targetrow, target);
                }
            }
            return true;
        }
#endif // no-fma
        if( Avx2.IsSupported )
        {
            int k, targetrow, offsk;
            Intrinsics.Vector256<double> v_negd_u0, v_negd_u1, v_negd_u2, v_negd_u3, v_negd;
            Intrinsics.Vector256<double> v_w0, v_w1, v_w2, v_w3, u01_lo, u01_hi, u23_lo, u23_hi;
            
            /*
             * Compute W = -D*transpose(U[0:3])
             */
            v_negd = Avx2.Multiply(Avx2.LoadVector256(diagd+offsd),Intrinsics.Vector256.Create(-1.0));
            v_negd_u0   = Avx2.Multiply(Avx2.LoadVector256(rowstorage+offsu+0*4),v_negd);
            v_negd_u1   = Avx2.Multiply(Avx2.LoadVector256(rowstorage+offsu+1*4),v_negd);
            v_negd_u2   = Avx2.Multiply(Avx2.LoadVector256(rowstorage+offsu+2*4),v_negd);
            v_negd_u3   = Avx2.Multiply(Avx2.LoadVector256(rowstorage+offsu+3*4),v_negd);
            u01_lo = Avx2.UnpackLow( v_negd_u0,v_negd_u1);
            u01_hi = Avx2.UnpackHigh(v_negd_u0,v_negd_u1);
            u23_lo = Avx2.UnpackLow( v_negd_u2,v_negd_u3);
            u23_hi = Avx2.UnpackHigh(v_negd_u2,v_negd_u3);
            v_w0 = Avx2.Permute2x128(u01_lo, u23_lo, 0x20);
            v_w1 = Avx2.Permute2x128(u01_hi, u23_hi, 0x20);
            v_w2 = Avx2.Permute2x128(u23_lo, u01_lo, 0x13);
            v_w3 = Avx2.Permute2x128(u23_hi, u01_hi, 0x13);
            
            //
            // Compute update S:= S + row_scatter(U*W)
            //
            if( sheight==uheight )
            {
                /*
                 * No row scatter, the most efficient code
                 */
                for(k=0; k<=uheight-1; k++)
                {
                    Intrinsics.Vector256<double> target;
                    
                    targetrow = offss+k*4;
                    offsk = offsu+k*4;
                    
                    target = Avx2.LoadVector256(rowstorage+targetrow);
                    target = Avx2.Add(Avx2.Multiply(Avx2.BroadcastScalarToVector256(rowstorage+offsk+0),v_w0),target);
                    target = Avx2.Add(Avx2.Multiply(Avx2.BroadcastScalarToVector256(rowstorage+offsk+1),v_w1),target);
                    target = Avx2.Add(Avx2.Multiply(Avx2.BroadcastScalarToVector256(rowstorage+offsk+2),v_w2),target);
                    target = Avx2.Add(Avx2.Multiply(Avx2.BroadcastScalarToVector256(rowstorage+offsk+3),v_w3),target);
                    Avx2.Store(rowstorage+targetrow, target);
                }
            }
            else
            {
                /*
                 * Row scatter is performed, less efficient code using double mapping to determine target row index
                 */
                for(k=0; k<=uheight-1; k++)
                {
                    Intrinsics.Vector256<double> target;
                    
                    targetrow = offss+raw2smap[superrowidx[urbase+k]]*4;
                    offsk = offsu+k*4;
                    
                    target = Avx2.LoadVector256(rowstorage+targetrow);
                    target = Avx2.Add(Avx2.Multiply(Avx2.BroadcastScalarToVector256(rowstorage+offsk+0),v_w0),target);
                    target = Avx2.Add(Avx2.Multiply(Avx2.BroadcastScalarToVector256(rowstorage+offsk+1),v_w1),target);
                    target = Avx2.Add(Avx2.Multiply(Avx2.BroadcastScalarToVector256(rowstorage+offsk+2),v_w2),target);
                    target = Avx2.Add(Avx2.Multiply(Avx2.BroadcastScalarToVector256(rowstorage+offsk+3),v_w3),target);
                    Avx2.Store(rowstorage+targetrow, target);
                }
            }
            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif
    private static bool updatekernel4444(double[] rowstorage,
        int offss,
        int sheight,
        int offsu,
        int uheight,
        double[] diagd,
        int offsd,
        int[] raw2smap,
        int[] superrowidx,
        int urbase,
        xparams _params)
    {
#if ALGLIB_USE_SIMD
        unsafe
        {
            fixed(double* p_rowstorage=rowstorage, p_diagd = diagd)
            {
                if( try_updatekernel4444(p_rowstorage, offss, sheight, offsu, uheight, p_diagd, offsd, raw2smap, superrowidx, urbase) )
                    return true;
            }
        }
#endif

        //
        // Fallback pure C# code
        //
        bool result = new bool();
        int k = 0;
        int targetrow = 0;
        int offsk = 0;
        double d0 = 0;
        double d1 = 0;
        double d2 = 0;
        double d3 = 0;
        double u00 = 0;
        double u01 = 0;
        double u02 = 0;
        double u03 = 0;
        double u10 = 0;
        double u11 = 0;
        double u12 = 0;
        double u13 = 0;
        double u20 = 0;
        double u21 = 0;
        double u22 = 0;
        double u23 = 0;
        double u30 = 0;
        double u31 = 0;
        double u32 = 0;
        double u33 = 0;
        double uk0 = 0;
        double uk1 = 0;
        double uk2 = 0;
        double uk3 = 0;

        d0 = diagd[offsd + 0];
        d1 = diagd[offsd + 1];
        d2 = diagd[offsd + 2];
        d3 = diagd[offsd + 3];
        u00 = d0 * rowstorage[offsu + 0 * 4 + 0];
        u01 = d1 * rowstorage[offsu + 0 * 4 + 1];
        u02 = d2 * rowstorage[offsu + 0 * 4 + 2];
        u03 = d3 * rowstorage[offsu + 0 * 4 + 3];
        u10 = d0 * rowstorage[offsu + 1 * 4 + 0];
        u11 = d1 * rowstorage[offsu + 1 * 4 + 1];
        u12 = d2 * rowstorage[offsu + 1 * 4 + 2];
        u13 = d3 * rowstorage[offsu + 1 * 4 + 3];
        u20 = d0 * rowstorage[offsu + 2 * 4 + 0];
        u21 = d1 * rowstorage[offsu + 2 * 4 + 1];
        u22 = d2 * rowstorage[offsu + 2 * 4 + 2];
        u23 = d3 * rowstorage[offsu + 2 * 4 + 3];
        u30 = d0 * rowstorage[offsu + 3 * 4 + 0];
        u31 = d1 * rowstorage[offsu + 3 * 4 + 1];
        u32 = d2 * rowstorage[offsu + 3 * 4 + 2];
        u33 = d3 * rowstorage[offsu + 3 * 4 + 3];
        for (k = 0; k <= uheight - 1; k++)
        {
            targetrow = offss + raw2smap[superrowidx[urbase + k]] * 4;
            offsk = offsu + k * 4;
            uk0 = rowstorage[offsk + 0];
            uk1 = rowstorage[offsk + 1];
            uk2 = rowstorage[offsk + 2];
            uk3 = rowstorage[offsk + 3];
            rowstorage[targetrow + 0] = rowstorage[targetrow + 0] - u00 * uk0 - u01 * uk1 - u02 * uk2 - u03 * uk3;
            rowstorage[targetrow + 1] = rowstorage[targetrow + 1] - u10 * uk0 - u11 * uk1 - u12 * uk2 - u13 * uk3;
            rowstorage[targetrow + 2] = rowstorage[targetrow + 2] - u20 * uk0 - u21 * uk1 - u22 * uk2 - u23 * uk3;
            rowstorage[targetrow + 3] = rowstorage[targetrow + 3] - u30 * uk0 - u31 * uk1 - u32 * uk2 - u33 * uk3;
        }
        result = true;
        return result;
    }
        /*************************************************************************
        This structure is used  to  store supernodal dependencies  for  a Cholesky
        factorization as well as precomputed update sizes, offsets, costs, etc.

        * RowBegin, RowEnd          -   array[NSuper]. For an I-th supernode elements RowBegin[I] and
                                        RowEnd[I] store range of locations in Idx[], URow0[] and other
                                        arrays that describe updates feeding into supernode J.

        Below we assume that an element J, with RowBegin[I]<=J<RowEnd[I] for some I,
        describes update for a supernode I from the supernode Idx[J]<I. Arrays below
        store the following information:
        * Idx                       -   indexes of supernodes feeding updates
        * URow0                     -   update offsets. URow0[J] stores location
                                        in Analysis.SuperRowIdx[] for a first row
                                        of the update
        * UWidth                    -   update widths
        * UFLOP                     -   update flops (fused multiply-adds)

        Flop counts for supernodes:
        * NFLOP                     -   array[NSuper], NFLOP[I] is a number of fused
                                        multiply-adds required to compute updates
                                        targeting supernode I and to factorize supernode itself
        * SFLOP                     -   array[NSuper], SFLOP[I] is a summary flop count
                                        for supernode and all of its children.


        *************************************************************************/
        public class spcholadj : apobject
        {
            public int[] rowbegin;
            public int[] rowend;
            public int[] idx;
            public int[] urow0;
            public int[] uwidth;
            public double[] uflop;
            public double[] nflop;
            public double[] sflop;
            public spcholadj()
            {
                init();
            }
            public override void init()
            {
                rowbegin = new int[0];
                rowend = new int[0];
                idx = new int[0];
                urow0 = new int[0];
                uwidth = new int[0];
                uflop = new double[0];
                nflop = new double[0];
                sflop = new double[0];
            }
            public override apobject make_copy()
            {
                spcholadj _result = new spcholadj();
                _result.rowbegin = (int[])rowbegin.Clone();
                _result.rowend = (int[])rowend.Clone();
                _result.idx = (int[])idx.Clone();
                _result.urow0 = (int[])urow0.Clone();
                _result.uwidth = (int[])uwidth.Clone();
                _result.uflop = (double[])uflop.Clone();
                _result.nflop = (double[])nflop.Clone();
                _result.sflop = (double[])sflop.Clone();
                return _result;
            }
        };


        /*************************************************************************
        This structure is used to store preliminary analysis  results  for  sparse
        Cholesky: elimination tree, factorization costs, etc.
        *************************************************************************/
        public class spcholanalysis : apobject
        {
            public int tasktype;
            public int n;
            public int permtype;
            public bool unitd;
            public int modtype;
            public double modparam0;
            public double modparam1;
            public double modparam2;
            public double modparam3;
            public bool debugblocksupernodal;
            public bool extendeddebug;
            public bool dotrace;
            public bool dotracescheduler;
            public bool dotracesupernodalstructure;
            public int[] referenceridx;
            public int nsuper;
            public int[] parentsupernode;
            public int[] childsupernodesridx;
            public int[] childsupernodesidx;
            public int[] supercolrange;
            public int[] superrowridx;
            public int[] superrowidx;
            public int[] blkstruct;
            public bool useparallelism;
            public int[] fillinperm;
            public int[] invfillinperm;
            public int[] superperm;
            public int[] invsuperperm;
            public int[] effectiveperm;
            public int[] inveffectiveperm;
            public bool istopologicalordering;
            public bool applypermutationtooutput;
            public spcholadj ladj;
            public int[] outrowcounts;
            public double[] inputstorage;
            public double[] outputstorage;
            public int[] rowstrides;
            public int[] rowoffsets;
            public double[] diagd;
            public apserv.nbpool nbooleanpool;
            public apserv.nipool nintegerpool;
            public apserv.nrpool nrealpool;
            public int[] currowbegin;
            public bool[] flagarray;
            public bool[] eligible;
            public int[] curpriorities;
            public int[] tmpparent;
            public int[] node2supernode;
            public amdordering.amdbuffer amdtmp;
            public int[] tmp0;
            public int[] tmp1;
            public int[] tmp2;
            public int[] tmp3;
            public int[] tmp4;
            public int[] raw2smap;
            public sparse.sparsematrix tmpa;
            public sparse.sparsematrix tmpat;
            public sparse.sparsematrix tmpa2;
            public sparse.sparsematrix tmpbottomt;
            public sparse.sparsematrix tmpupdate;
            public sparse.sparsematrix tmpupdatet;
            public sparse.sparsematrix tmpnewtailt;
            public int[] tmpperm;
            public int[] invtmpperm;
            public double[] tmpx;
            public double[] simdbuf;
            public spcholanalysis()
            {
                init();
            }
            public override void init()
            {
                referenceridx = new int[0];
                parentsupernode = new int[0];
                childsupernodesridx = new int[0];
                childsupernodesidx = new int[0];
                supercolrange = new int[0];
                superrowridx = new int[0];
                superrowidx = new int[0];
                blkstruct = new int[0];
                fillinperm = new int[0];
                invfillinperm = new int[0];
                superperm = new int[0];
                invsuperperm = new int[0];
                effectiveperm = new int[0];
                inveffectiveperm = new int[0];
                ladj = new spcholadj();
                outrowcounts = new int[0];
                inputstorage = new double[0];
                outputstorage = new double[0];
                rowstrides = new int[0];
                rowoffsets = new int[0];
                diagd = new double[0];
                nbooleanpool = new apserv.nbpool();
                nintegerpool = new apserv.nipool();
                nrealpool = new apserv.nrpool();
                currowbegin = new int[0];
                flagarray = new bool[0];
                eligible = new bool[0];
                curpriorities = new int[0];
                tmpparent = new int[0];
                node2supernode = new int[0];
                amdtmp = new amdordering.amdbuffer();
                tmp0 = new int[0];
                tmp1 = new int[0];
                tmp2 = new int[0];
                tmp3 = new int[0];
                tmp4 = new int[0];
                raw2smap = new int[0];
                tmpa = new sparse.sparsematrix();
                tmpat = new sparse.sparsematrix();
                tmpa2 = new sparse.sparsematrix();
                tmpbottomt = new sparse.sparsematrix();
                tmpupdate = new sparse.sparsematrix();
                tmpupdatet = new sparse.sparsematrix();
                tmpnewtailt = new sparse.sparsematrix();
                tmpperm = new int[0];
                invtmpperm = new int[0];
                tmpx = new double[0];
                simdbuf = new double[0];
            }
            public override apobject make_copy()
            {
                spcholanalysis _result = new spcholanalysis();
                _result.tasktype = tasktype;
                _result.n = n;
                _result.permtype = permtype;
                _result.unitd = unitd;
                _result.modtype = modtype;
                _result.modparam0 = modparam0;
                _result.modparam1 = modparam1;
                _result.modparam2 = modparam2;
                _result.modparam3 = modparam3;
                _result.debugblocksupernodal = debugblocksupernodal;
                _result.extendeddebug = extendeddebug;
                _result.dotrace = dotrace;
                _result.dotracescheduler = dotracescheduler;
                _result.dotracesupernodalstructure = dotracesupernodalstructure;
                _result.referenceridx = (int[])referenceridx.Clone();
                _result.nsuper = nsuper;
                _result.parentsupernode = (int[])parentsupernode.Clone();
                _result.childsupernodesridx = (int[])childsupernodesridx.Clone();
                _result.childsupernodesidx = (int[])childsupernodesidx.Clone();
                _result.supercolrange = (int[])supercolrange.Clone();
                _result.superrowridx = (int[])superrowridx.Clone();
                _result.superrowidx = (int[])superrowidx.Clone();
                _result.blkstruct = (int[])blkstruct.Clone();
                _result.useparallelism = useparallelism;
                _result.fillinperm = (int[])fillinperm.Clone();
                _result.invfillinperm = (int[])invfillinperm.Clone();
                _result.superperm = (int[])superperm.Clone();
                _result.invsuperperm = (int[])invsuperperm.Clone();
                _result.effectiveperm = (int[])effectiveperm.Clone();
                _result.inveffectiveperm = (int[])inveffectiveperm.Clone();
                _result.istopologicalordering = istopologicalordering;
                _result.applypermutationtooutput = applypermutationtooutput;
                _result.ladj = (spcholadj)ladj.make_copy();
                _result.outrowcounts = (int[])outrowcounts.Clone();
                _result.inputstorage = (double[])inputstorage.Clone();
                _result.outputstorage = (double[])outputstorage.Clone();
                _result.rowstrides = (int[])rowstrides.Clone();
                _result.rowoffsets = (int[])rowoffsets.Clone();
                _result.diagd = (double[])diagd.Clone();
                _result.nbooleanpool = (apserv.nbpool)nbooleanpool.make_copy();
                _result.nintegerpool = (apserv.nipool)nintegerpool.make_copy();
                _result.nrealpool = (apserv.nrpool)nrealpool.make_copy();
                _result.currowbegin = (int[])currowbegin.Clone();
                _result.flagarray = (bool[])flagarray.Clone();
                _result.eligible = (bool[])eligible.Clone();
                _result.curpriorities = (int[])curpriorities.Clone();
                _result.tmpparent = (int[])tmpparent.Clone();
                _result.node2supernode = (int[])node2supernode.Clone();
                _result.amdtmp = (amdordering.amdbuffer)amdtmp.make_copy();
                _result.tmp0 = (int[])tmp0.Clone();
                _result.tmp1 = (int[])tmp1.Clone();
                _result.tmp2 = (int[])tmp2.Clone();
                _result.tmp3 = (int[])tmp3.Clone();
                _result.tmp4 = (int[])tmp4.Clone();
                _result.raw2smap = (int[])raw2smap.Clone();
                _result.tmpa = (sparse.sparsematrix)tmpa.make_copy();
                _result.tmpat = (sparse.sparsematrix)tmpat.make_copy();
                _result.tmpa2 = (sparse.sparsematrix)tmpa2.make_copy();
                _result.tmpbottomt = (sparse.sparsematrix)tmpbottomt.make_copy();
                _result.tmpupdate = (sparse.sparsematrix)tmpupdate.make_copy();
                _result.tmpupdatet = (sparse.sparsematrix)tmpupdatet.make_copy();
                _result.tmpnewtailt = (sparse.sparsematrix)tmpnewtailt.make_copy();
                _result.tmpperm = (int[])tmpperm.Clone();
                _result.invtmpperm = (int[])invtmpperm.Clone();
                _result.tmpx = (double[])tmpx.Clone();
                _result.simdbuf = (double[])simdbuf.Clone();
                return _result;
            }
        };




        public const int maxsupernode = 4;
        public const double maxmergeinefficiency = 0.25;
        public const int smallfakestolerance = 2;
        public const int maxfastkernel = 4;
        public const bool relaxedsupernodes = true;
        public const int updatesheadersize = 2;
        public const int groupheadersize = 2;
        public const int batchheadersize = 2;
        public const int sequenceentrysize = 3;
        public const int smallupdate = 128;
        public const double raw2sthreshold = 0.01;


        /*************************************************************************
        Informational function, useful for debugging
        *************************************************************************/
        public static int spsymmgetmaxfastkernel(xparams _params)
        {
            int result = 0;

            result = maxfastkernel;
            return result;
        }


        /*************************************************************************
        Symbolic phase of Cholesky decomposition.

        Performs preliminary analysis of Cholesky/LDLT factorization.  The  latter
        is computed with strictly diagonal D (no Bunch-Kauffman pivoting).

        The analysis object produced by this function will be used later to  guide
        actual decomposition.

        Depending on settings specified during factorization, may produce  vanilla
        Cholesky or L*D*LT  decomposition  (with  strictly  diagonal  D),  without
        permutation or with permutation P (being either  topological  ordering  or
        sparsity preserving ordering).

        Thus, A is represented as either L*LT or L*D*LT or P*L*LT*PT or P*L*D*LT*PT.

        NOTE: L*D*LT family of factorization may be used to  factorize  indefinite
              matrices. However, numerical stability is guaranteed ONLY for a class
              of quasi-definite matrices.

        INPUT PARAMETERS:
            A           -   sparse square matrix in CRS format, with LOWER triangle
                            being used to store the matrix.
            Priorities  -   array[N], optional priorities:
                            * ignored for PermType<>3 and PermType<>-3
                              (not referenced at all)
                            * for   PermType=3  or  PermType=-3  this  array  stores
                              nonnegative  column  elimination  priorities.  Columns
                              with  lower  priorities are eliminated first. At least
                              max(Priorities[])+1  internal  AMD  rounds   will   be
                              performed, so avoid specifying too large values here.
                              Ideally, 0<=Priorities[I]<5.
            PromoteAbove-   columns with degrees higher than PromoteAbove*max(MEAN(Degree),1)
                            may be promoted to the next priority group. Ignored  for
                            PermType<>3 and PermType<>-3.
                            This parameter can be used to make priorities  a  hard
                            requirement, a non-binding  suggestion,  or  something
                            in-between:
                            * big PromoteAbove (N or more) effectively means that
                              priorities are hard
                            * values between 2 and 10 are usually a good choice  for
                              soft priorities
                            * zero value means that appropriate  value  for  a  soft
                              priority (between 2 and 5) is automatically chosen.
                              Specific value may change in future ALGLIB versions.
            FactType    -   factorization type:
                            * 0 for traditional Cholesky
                            * 1 for LDLT decomposition with strictly diagonal D
            PermType    -   permutation type:
                            *-3 for debug improved AMD which debugs AMD itself and
                                parallel block supernodal code:
                                * AMD is debugged by generating a sequence of decreasing
                                  tail sizes, ~logN in total, even if ordering can be
                                  done with just one round of AMD. This ordering is
                                  used to test correctness of multiple AMD rounds.
                                * parallel block supernodal code is debugged by
                                  partitioning problems into smallest possible chunks,
                                  ignoring thresholds set by SMPActivationLevel()
                                  and SpawnLevel().
                            *-2 for column count ordering (NOT RECOMMENDED!)
                            *-1 for absence of permutation
                            * 0 for best permutation available
                            * 1 for supernodal ordering (improves locality and
                              performance, but does NOT change fill-in pattern)
                            * 2 for supernodal AMD ordering (improves fill-in)
                            * 3 for  improved  AMD  (approximate  minimum  degree)
                                ordering with better  handling  of  matrices  with
                                dense rows/columns and ability to perform priority
                                ordering
            Analysis    -   can be uninitialized instance, or previous analysis
                            results. Previously allocated memory is reused as much
                            as possible.
            Buf         -   buffer; may be completely uninitialized, or one remained
                            from previous calls (including ones with completely
                            different matrices). Previously allocated temporary
                            space will be reused as much as possible.

        OUTPUT PARAMETERS:
            Analysis    -   symbolic analysis of the matrix structure  which  will
                            be used later to guide  numerical  factorization.  The
                            numerical values are stored internally in the structure,
                            but you have to  run  factorization  phase  explicitly
                            with SPSymmFactorize().  You  can  also reload another
                            matrix with same sparsity pattern with  SPSymmReload()
                            or rewrite its diagonal with SPSymmReloadDiagonal().

        This function fails if and only if the matrix A is symbolically degenerate
        i.e. has diagonal element which is exactly zero. In  such  case  False  is
        returned.

        NOTE: defining 'SCHOLESKY' trace tag will activate tracing.
              defining 'SCHOLESKY.SS' trace tag will activate detailed tracing  of
              the supernodal structure.

        NOTE: defining 'DEBUG.SLOW' trace tag will  activate  extra-slow  (roughly
              N^3 ops) integrity checks, in addition to cheap O(1) ones.

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        public static bool spsymmanalyze(sparse.sparsematrix a,
            int[] priorities,
            double promoteabove,
            int facttype,
            int permtype,
            spcholanalysis analysis,
            xparams _params)
        {
            bool result = new bool();
            int n = 0;
            int m = 0;
            int i = 0;
            int j = 0;
            int jj = 0;
            int j0 = 0;
            int j1 = 0;
            int k = 0;
            int range0 = 0;
            int range1 = 0;
            int newrange0 = 0;
            int eligiblecnt = 0;
            bool permready = new bool();

            ap.assert(sparse.sparseiscrs(a, _params), "SPSymmAnalyze: A is not stored in CRS format");
            ap.assert(sparse.sparsegetnrows(a, _params) == sparse.sparsegetncols(a, _params), "SPSymmAnalyze: non-square A");
            ap.assert(facttype == 0 || facttype == 1, "SPSymmAnalyze: unexpected FactType");
            ap.assert((((((permtype == 0 || permtype == 1) || permtype == 2) || permtype == 3) || permtype == -1) || permtype == -2) || permtype == -3, "SPSymmAnalyze: unexpected PermType");
            ap.assert((permtype != 3 && permtype != -3) || (math.isfinite(promoteabove) && (double)(promoteabove) >= (double)(0)), "SPSymmAnalyze: unexpected PromoteAbove - infinite or negative");
            result = true;
            n = sparse.sparsegetnrows(a, _params);
            if (permtype == -3 || permtype == 3)
            {
                ap.assert(ap.len(priorities) >= n, "SPSymmAnalyze: length(Priorities)<N");
                ablasf.icopyallocv(n, priorities, ref analysis.curpriorities, _params);
            }
            if (permtype == 0)
            {
                ablasf.isetallocv(n, 0, ref analysis.curpriorities, _params);
                permtype = 3;
                promoteabove = 0.0;
            }
            analysis.tasktype = 0;
            analysis.n = n;
            analysis.unitd = facttype == 0;
            analysis.permtype = permtype;
            analysis.debugblocksupernodal = permtype == -3;
            analysis.extendeddebug = ap.istraceenabled("DEBUG.SLOW", _params) && n <= 100;
            analysis.dotrace = ap.istraceenabled("SCHOLESKY", _params);
            analysis.dotracescheduler = analysis.dotrace && ap.istraceenabled("SCHOLESKY.SCHEDULER", _params);
            analysis.dotracesupernodalstructure = analysis.dotrace && ap.istraceenabled("SCHOLESKY.SS", _params);
            analysis.istopologicalordering = permtype == -1 || permtype == 1;
            analysis.applypermutationtooutput = permtype == -1;
            analysis.modtype = 0;
            analysis.modparam0 = 0.0;
            analysis.modparam1 = 0.0;
            analysis.modparam2 = 0.0;
            analysis.modparam3 = 0.0;
            analysis.useparallelism = false;

            //
            // Allocate temporaries
            //
            apserv.ivectorsetlengthatleast(ref analysis.tmpparent, n + 1, _params);
            apserv.ivectorsetlengthatleast(ref analysis.tmp0, n + 1, _params);
            apserv.ivectorsetlengthatleast(ref analysis.tmp1, n + 1, _params);
            apserv.ivectorsetlengthatleast(ref analysis.tmp2, n + 1, _params);
            apserv.ivectorsetlengthatleast(ref analysis.tmp3, n + 1, _params);
            apserv.ivectorsetlengthatleast(ref analysis.tmp4, n + 1, _params);
            apserv.bvectorsetlengthatleast(ref analysis.flagarray, n + 1, _params);
            apserv.nbpoolinit(analysis.nbooleanpool, n, _params);
            apserv.nipoolinit(analysis.nintegerpool, n, _params);
            apserv.nrpoolinit(analysis.nrealpool, n, _params);

            //
            // Initial trace message
            //
            if (analysis.dotrace)
            {
                ap.trace("\n\n");
                ap.trace("////////////////////////////////////////////////////////////////////////////////////////////////////\n");
                ap.trace("//  SPARSE CHOLESKY ANALYSIS STARTED                                                              //\n");
                ap.trace("////////////////////////////////////////////////////////////////////////////////////////////////////\n");

                //
                // Nonzeros count of the original matrix
                //
                k = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    k = k + (a.didx[i] - a.ridx[i]) + 1;
                }
                ap.trace(System.String.Format("NZ(A) = {0,0:d}\n", k));

                //
                // Analyze row statistics
                //
                ap.trace("=== ANALYZING ROW STATISTICS =======================================================================\n");
                ap.trace("row size is:\n");
                ablasf.isetv(n, 1, analysis.tmp0, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    for (jj = a.ridx[i]; jj <= a.didx[i] - 1; jj++)
                    {
                        j = a.idx[jj];
                        analysis.tmp0[i] = analysis.tmp0[i] + 1;
                        analysis.tmp0[j] = analysis.tmp0[j] + 1;
                    }
                }
                k = 1;
                while (k <= n)
                {
                    j = 0;
                    for (i = 0; i <= n - 1; i++)
                    {
                        if (analysis.tmp0[i] >= k && analysis.tmp0[i] < 2 * k)
                        {
                            j = j + 1;
                        }
                    }
                    ap.trace(System.String.Format("* [{0,6:d}..{1,6:d}) elements: {2,6:d} rows\n", k, 2 * k, j));
                    k = k * 2;
                }
            }

            //
            // Initial integrity check - diagonal MUST be symbolically nonzero
            //
            for (i = 0; i <= n - 1; i++)
            {
                if (a.didx[i] == a.uidx[i])
                {
                    if (analysis.dotrace)
                    {
                        ap.trace("> the matrix diagonal is symbolically zero, stopping");
                    }
                    result = false;
                    return result;
                }
            }

            //
            // What type of permutation do we have?
            //
            if (analysis.istopologicalordering)
            {
                ap.assert(permtype == -1 || permtype == 1, "SPSymmAnalyze: integrity check failed (ihebd)");

                //
                // Build topologically ordered elimination tree
                //
                buildorderedetree(a, n, ref analysis.tmpparent, ref analysis.superperm, ref analysis.invsuperperm, analysis.tmp0, analysis.tmp1, analysis.tmp2, analysis.flagarray, _params);
                apserv.ivectorsetlengthatleast(ref analysis.fillinperm, n, _params);
                apserv.ivectorsetlengthatleast(ref analysis.invfillinperm, n, _params);
                apserv.ivectorsetlengthatleast(ref analysis.effectiveperm, n, _params);
                apserv.ivectorsetlengthatleast(ref analysis.inveffectiveperm, n, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    analysis.fillinperm[i] = i;
                    analysis.invfillinperm[i] = i;
                    analysis.effectiveperm[i] = analysis.superperm[i];
                    analysis.inveffectiveperm[i] = analysis.invsuperperm[i];
                }

                //
                // Reorder input matrix
                //
                topologicalpermutation(a, analysis.superperm, analysis.tmpat, _params);

                //
                // Analyze etree, build supernodal structure
                //
                createsupernodalstructure(analysis.tmpat, analysis.tmpparent, n, analysis, ref analysis.node2supernode, analysis.tmp0, analysis.tmp1, analysis.tmp2, analysis.tmp3, analysis.tmp4, analysis.flagarray, _params);

                //
                // Having fully initialized supernodal structure, analyze dependencies
                //
                analyzesupernodaldependencies(analysis, a, analysis.node2supernode, n, analysis.tmp0, analysis.tmp1, analysis.flagarray, _params);
            }
            else
            {

                //
                // Generate fill-in reducing permutation
                //
                permready = false;
                if (permtype == -2)
                {
                    generatedbgpermutation(a, n, ref analysis.fillinperm, ref analysis.invfillinperm, _params);
                    permready = true;
                }
                if (permtype == 2)
                {
                    amdordering.generateamdpermutation(a, n, ref analysis.fillinperm, ref analysis.invfillinperm, analysis.amdtmp, _params);
                    permready = true;
                }
                if (permtype == 3 || permtype == -3)
                {
                    ap.assert(ap.len(analysis.curpriorities) >= n, "SPSymmAnalyze: integrity check failed (4653)");

                    //
                    // Perform iterative AMD, with nearly-dense columns being postponed to be handled later.
                    //
                    // The current (residual) matrix A is divided into two parts: head, with its columns being
                    // properly ordered, and tail, with its columns being reordered at the next iteration.
                    //
                    // After each partial AMD we compute sparsity pattern of the tail, set it as the new residual
                    // and repeat iteration.
                    //
                    ablasf.iallocv(n, ref analysis.fillinperm, _params);
                    ablasf.iallocv(n, ref analysis.invfillinperm, _params);
                    ablasf.iallocv(n, ref analysis.tmpperm, _params);
                    ablasf.iallocv(n, ref analysis.invtmpperm, _params);
                    for (i = 0; i <= n - 1; i++)
                    {
                        analysis.fillinperm[i] = i;
                        analysis.invfillinperm[i] = i;
                    }
                    sparse.sparsecopybuf(a, analysis.tmpa, _params);
                    ablasf.ballocv(n, ref analysis.eligible, _params);
                    range0 = 0;
                    range1 = n;
                    while (range0 < range1)
                    {
                        m = range1 - range0;

                        //
                        // Perform partial AMD ordering of the residual matrix:
                        // * determine columns in the residual part that are eligible for elimination.
                        // * generate partial fill-in reducing permutation (leading Residual-Tail columns
                        //   are properly ordered, the rest is unordered).
                        // * update column elimination priorities (decrease by 1)
                        //
                        ablasf.bsetv(range1 - range0, false, analysis.eligible, _params);
                        eligiblecnt = 0;
                        for (i = 0; i <= n - 1; i++)
                        {
                            j = analysis.fillinperm[i];
                            if ((j >= range0 && j < range1) && analysis.curpriorities[i] <= 0)
                            {
                                analysis.eligible[j - range0] = true;
                                eligiblecnt = eligiblecnt + 1;
                            }
                        }
                        if (analysis.dotrace)
                        {
                            ap.trace(System.String.Format("> multiround AMD, column_range=[{0,7:d},{1,7:d}] ({2,7:d} out of {3,7:d}), {4,5:F1}% eligible\n", range0, range1, range1 - range0, n, (double)(100 * eligiblecnt) / (double)m));
                        }
                        newrange0 = range0 + amdordering.generateamdpermutationx(analysis.tmpa, analysis.eligible, range1 - range0, promoteabove, ref analysis.tmpperm, ref analysis.invtmpperm, 1, analysis.amdtmp, _params);
                        if (permtype == -3)
                        {

                            //
                            // Special debug ordering in order to test correctness of multiple AMD rounds
                            //
                            newrange0 = Math.Min(newrange0, range0 + m / 2 + 1);
                        }
                        for (i = 0; i <= n - 1; i++)
                        {
                            analysis.curpriorities[i] = analysis.curpriorities[i] - 1;
                        }

                        //
                        // If there were columns that both eligible and sparse enough,
                        // apply permutation and recompute trail.
                        //
                        if (newrange0 > range0)
                        {

                            //
                            // Apply permutation TmpPerm[] to the tail of the permutation FillInPerm[]
                            //
                            for (i = 0; i <= m - 1; i++)
                            {
                                analysis.fillinperm[analysis.invfillinperm[range0 + analysis.invtmpperm[i]]] = range0 + i;
                            }
                            for (i = 0; i <= n - 1; i++)
                            {
                                analysis.invfillinperm[analysis.fillinperm[i]] = i;
                            }

                            //
                            // Compute partial Cholesky of the trailing submatrix (after applying rank-K update to the
                            // trailing submatrix but before Cholesky-factorizing it).
                            //
                            if (newrange0 < range1)
                            {
                                sparse.sparsesymmpermtblbuf(analysis.tmpa, false, analysis.tmpperm, analysis.tmpa2, _params);
                                partialcholeskypattern(analysis.tmpa2, newrange0 - range0, range1 - newrange0, analysis.tmpa, analysis.tmpparent, analysis.tmp0, analysis.tmp1, analysis.tmp2, analysis.flagarray, analysis.tmpbottomt, analysis.tmpupdatet, analysis.tmpupdate, analysis.tmpnewtailt, _params);
                                if (analysis.extendeddebug)
                                {
                                    slowdebugchecks(a, analysis.fillinperm, n, range1 - newrange0, analysis.tmpa, _params);
                                }
                            }
                            range0 = newrange0;
                            m = range1 - range0;
                        }

                        //
                        // Analyze sparsity pattern of the current submatrix (TmpA), manually move completely dense rows to the end.
                        //
                        if (m > 0)
                        {
                            ap.assert((analysis.tmpa.m == m && analysis.tmpa.n == m) && analysis.tmpa.ninitialized == analysis.tmpa.ridx[m], "SPSymmAnalyze: integrity check failed (0572)");
                            ablasf.isetallocv(m, 1, ref analysis.tmp0, _params);
                            for (i = 0; i <= m - 1; i++)
                            {
                                j0 = analysis.tmpa.ridx[i];
                                j1 = analysis.tmpa.didx[i] - 1;
                                for (jj = j0; jj <= j1; jj++)
                                {
                                    j = analysis.tmpa.idx[jj];
                                    analysis.tmp0[i] = analysis.tmp0[i] + 1;
                                    analysis.tmp0[j] = analysis.tmp0[j] + 1;
                                }
                            }
                            j = 0;
                            k = 0;
                            for (i = 0; i <= m - 1; i++)
                            {
                                if (analysis.tmp0[i] < m)
                                {
                                    analysis.invtmpperm[j] = i;
                                    j = j + 1;
                                }
                            }
                            for (i = 0; i <= m - 1; i++)
                            {
                                if (analysis.tmp0[i] == m)
                                {
                                    analysis.invtmpperm[j] = i;
                                    j = j + 1;
                                    k = k + 1;
                                }
                            }
                            for (i = 0; i <= m - 1; i++)
                            {
                                analysis.tmpperm[analysis.invtmpperm[i]] = i;
                            }
                            ap.assert(j == m, "SPSymmAnalyze: integrity check failed (6432)");
                            if (k > 0)
                            {

                                //
                                // K dense rows are moved to the end
                                //
                                if (k < m)
                                {

                                    //
                                    // There are still exist sparse rows that need reordering, apply permutation and manually truncate matrix
                                    //
                                    for (i = 0; i <= m - 1; i++)
                                    {
                                        analysis.fillinperm[analysis.invfillinperm[range0 + analysis.invtmpperm[i]]] = range0 + i;
                                    }
                                    for (i = 0; i <= n - 1; i++)
                                    {
                                        analysis.invfillinperm[analysis.fillinperm[i]] = i;
                                    }
                                    sparse.sparsesymmpermtblbuf(analysis.tmpa, false, analysis.tmpperm, analysis.tmpa2, _params);
                                    sparse.sparsecopybuf(analysis.tmpa2, analysis.tmpa, _params);
                                    analysis.tmpa.m = m - k;
                                    analysis.tmpa.n = m - k;
                                    analysis.tmpa.ninitialized = analysis.tmpa.ridx[analysis.tmpa.m];
                                }
                                range1 = range1 - k;
                                m = range1 - range0;
                            }
                        }
                    }
                    if (analysis.dotrace)
                    {
                        ap.trace(System.String.Format("> multiround AMD, column_range=[{0,7:d},{1,7:d}], stopped\n", range0, range1));
                    }
                    permready = true;
                }
                ap.assert(permready, "SPSymmAnalyze: integrity check failed (pp4td)");

                //
                // Apply permutation to the matrix, perform analysis on the initially reordered matrix
                // (we may need one more reordering, now topological one, due to supernodal analysis).
                // Build topologically ordered elimination tree
                //
                sparse.sparsesymmpermtblbuf(a, false, analysis.fillinperm, analysis.tmpa, _params);
                buildorderedetree(analysis.tmpa, n, ref analysis.tmpparent, ref analysis.superperm, ref analysis.invsuperperm, analysis.tmp0, analysis.tmp1, analysis.tmp2, analysis.flagarray, _params);
                apserv.ivectorsetlengthatleast(ref analysis.effectiveperm, n, _params);
                apserv.ivectorsetlengthatleast(ref analysis.inveffectiveperm, n, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    analysis.effectiveperm[i] = analysis.superperm[analysis.fillinperm[i]];
                    analysis.inveffectiveperm[analysis.effectiveperm[i]] = i;
                }

                //
                // Reorder input matrix
                //
                topologicalpermutation(analysis.tmpa, analysis.superperm, analysis.tmpat, _params);

                //
                // Analyze etree, build supernodal structure
                //
                createsupernodalstructure(analysis.tmpat, analysis.tmpparent, n, analysis, ref analysis.node2supernode, analysis.tmp0, analysis.tmp1, analysis.tmp2, analysis.tmp3, analysis.tmp4, analysis.flagarray, _params);

                //
                // Having fully initialized supernodal structure, analyze dependencies
                //
                analyzesupernodaldependencies(analysis, analysis.tmpa, analysis.node2supernode, n, analysis.tmp0, analysis.tmp1, analysis.flagarray, _params);
            }

            //
            // Prepare block structure
            //
            createblockstructure(analysis, _params);

            //
            // Save information for integrity checks
            //
            ablasf.icopyallocv(n + 1, analysis.tmpat.ridx, ref analysis.referenceridx, _params);

            //
            // Load matrix into the supernodal storage
            //
            loadmatrix(analysis, analysis.tmpat, _params);
            return result;
        }


        /*************************************************************************
        Sets modified Cholesky type

        INPUT PARAMETERS:
            Analysis    -   symbolic analysis of the matrix structure
            ModStrategy -   modification type:
                            * 0 for traditional Cholesky/LDLT (Cholesky fails when
                              encounters nonpositive pivot, LDLT fails  when  zero
                              pivot   is  encountered,  no  stability  checks  for
                              overflows/underflows)
                            * 1 for modified Cholesky with additional checks:
                              * pivots less than ModParam0 are increased; (similar
                                procedure with proper generalization is applied to
                                LDLT)
                              * if,  at  some  moment,  sum  of absolute values of
                                elements in column  J  will  become  greater  than
                                ModParam1, Cholesky/LDLT will treat it as  failure
                                and will stop immediately
                              * if ModParam0 is zero, no pivot modification is applied
                              * if ModParam1 is zero, no overflow check is performed
            P0, P1, P2,P3 - modification parameters #0 #1, #2 and #3.
                            Params #2 and #3 are ignored in current version.

        OUTPUT PARAMETERS:
            Analysis    -   symbolic analysis of the matrix structure, new strategy
                            (results will be seen with next SPSymmFactorize() call)

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        public static void spsymmsetmodificationstrategy(spcholanalysis analysis,
            int modstrategy,
            double p0,
            double p1,
            double p2,
            double p3,
            xparams _params)
        {
            ap.assert(modstrategy == 0 || modstrategy == 1, "SPSymmSetModificationStrategy: unexpected ModStrategy");
            ap.assert(math.isfinite(p0) && (double)(p0) >= (double)(0), "SPSymmSetModificationStrategy: bad P0");
            ap.assert(math.isfinite(p1), "SPSymmSetModificationStrategy: bad P1");
            ap.assert(math.isfinite(p2), "SPSymmSetModificationStrategy: bad P2");
            ap.assert(math.isfinite(p3), "SPSymmSetModificationStrategy: bad P3");
            analysis.modtype = modstrategy;
            analysis.modparam0 = p0;
            analysis.modparam1 = p1;
            analysis.modparam2 = p2;
            analysis.modparam3 = p3;
        }


        /*************************************************************************
        Updates symmetric  matrix  internally  stored  in  previously  initialized
        Analysis object.

        You can use this function to perform  multiple  factorizations  with  same
        sparsity patterns: perform symbolic analysis  once  with  SPSymmAnalyze(),
        then update internal matrix with SPSymmReload() and call SPSymmFactorize().

        INPUT PARAMETERS:
            Analysis    -   symbolic analysis of the matrix structure
            A           -   sparse square matrix in CRS format with LOWER triangle
                            being used to store the matrix. The matrix  MUST  have
                            sparsity   pattern   exactly   same  as  one  used  to
                            initialize the Analysis object.
                            The algorithm will fail in  an  unpredictable  way  if
                            something different was passed.

        OUTPUT PARAMETERS:
            Analysis    -   symbolic analysis of the matrix structure  which  will
                            be used later to guide  numerical  factorization.  The
                            numerical values are stored internally in the structure,
                            but you have to  run  factorization  phase  explicitly
                            with SPSymmAnalyze().  You  can  also  reload  another
                            matrix with same sparsity pattern with SPSymmReload().

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        public static void spsymmreload(spcholanalysis analysis,
            sparse.sparsematrix a,
            xparams _params)
        {
            ap.assert(sparse.sparseiscrs(a, _params), "SPSymmReload: A is not stored in CRS format");
            ap.assert(sparse.sparsegetnrows(a, _params) == sparse.sparsegetncols(a, _params), "SPSymmReload: non-square A");
            if (analysis.istopologicalordering)
            {

                //
                // Topological (fill-in preserving) ordering is used, we can copy
                // A directly into WrkAT using joint permute+transpose
                //
                topologicalpermutation(a, analysis.effectiveperm, analysis.tmpat, _params);
                loadmatrix(analysis, analysis.tmpat, _params);
            }
            else
            {

                //
                // Non-topological permutation; first we perform generic symmetric
                // permutation, then transpose result
                //
                sparse.sparsesymmpermtblbuf(a, false, analysis.effectiveperm, analysis.tmpa, _params);
                sparse.sparsecopytransposecrsbuf(analysis.tmpa, analysis.tmpat, _params);
                loadmatrix(analysis, analysis.tmpat, _params);
            }
        }


        /*************************************************************************
        Updates  diagonal  of  the  symmetric  matrix  internally  stored  in  the
        previously initialized Analysis object.

        When only diagonal of the  matrix  has  changed,  this  function  is  more
        efficient than SPSymmReload() that has to perform  costly  permutation  of
        the entire matrix.

        You can use this function to perform  multiple  factorizations  with  same
        off-diagonal elements: perform symbolic analysis once with SPSymmAnalyze(),
        then update diagonal with SPSymmReloadDiagonal() and call SPSymmFactorize().

        INPUT PARAMETERS:
            Analysis    -   symbolic analysis of the matrix structure
            D           -   array[N], diagonal factor

        OUTPUT PARAMETERS:
            Analysis    -   symbolic analysis of the matrix structure  which  will
                            be used later to guide  numerical  factorization.  The
                            numerical values are stored internally in the structure,
                            but you have to  run  factorization  phase  explicitly
                            with SPSymmAnalyze().  You  can  also  reload  another
                            matrix with same sparsity pattern with SPSymmReload().

          -- ALGLIB routine --
             05.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void spsymmreloaddiagonal(spcholanalysis analysis,
            double[] d,
            xparams _params)
        {
            int sidx = 0;
            int cols0 = 0;
            int cols1 = 0;
            int offss = 0;
            int sstride = 0;
            int j = 0;

            ap.assert(ap.len(d) >= analysis.n, "SPSymmReloadDiagonal: length(D)<N");
            for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
            {
                cols0 = analysis.supercolrange[sidx];
                cols1 = analysis.supercolrange[sidx + 1];
                offss = analysis.rowoffsets[sidx];
                sstride = analysis.rowstrides[sidx];
                for (j = cols0; j <= cols1 - 1; j++)
                {
                    analysis.inputstorage[offss + (j - cols0) * sstride + (j - cols0)] = d[analysis.inveffectiveperm[j]];
                }
            }
        }


        /*************************************************************************
        Sparse Cholesky factorization of symmetric matrix stored  in  CRS  format,
        using precomputed analysis of the sparsity pattern stored  in the Analysis
        object and specific numeric values that  are  presently  loaded  into  the
        Analysis.

        The factorization can be retrieved  with  SPSymmExtract().  Alternatively,
        one can perform some operations without offloading  the  matrix  (somewhat
        faster due to itilization of  SIMD-friendly  supernodal  data structures),
        most importantly - linear system solution with SPSymmSolve().

        Depending on settings specified during factorization, may produce  vanilla
        Cholesky or L*D*LT  decomposition  (with  strictly  diagonal  D),  without
        permutation or with permutation P (being either  topological  ordering  or
        sparsity preserving ordering).

        Thus, A is represented as either L*LT or L*D*LT or P*L*LT*PT or P*L*D*LT*PT.

        NOTE: L*D*LT family of factorization may be used to  factorize  indefinite
              matrices. However, numerical stability is guaranteed ONLY for a class
              of quasi-definite matrices.

        INPUT PARAMETERS:
            Analysis    -   prior  analysis  performed on some sparse matrix, with
                            matrix being stored in Analysis.

        OUTPUT PARAMETERS:
            Analysis    -   contains factorization results

        The function returns True  when  factorization  resulted  in nondegenerate
        matrix. False is returned when factorization fails (Cholesky factorization
        of indefinite matrix) or LDLT factorization has exactly zero  elements  at
        the diagonal.

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        public static bool spsymmfactorize(spcholanalysis analysis,
            xparams _params)
        {
            bool result = new bool();
            int n = 0;
            apserv.sboolean b = new apserv.sboolean();

            ap.assert(analysis.tasktype == 0, "SPCholFactorize: Analysis type does not match current task");
            n = analysis.n;

            //
            // Allocate temporaries
            //
            apserv.ivectorsetlengthatleast(ref analysis.tmp0, n + 1, _params);
            ablasf.rsetallocv(n, 0.0, ref analysis.diagd, _params);
            ablasf.rcopyallocv(analysis.rowoffsets[analysis.nsuper], analysis.inputstorage, ref analysis.outputstorage, _params);
            ablasf.icopyallocv(analysis.nsuper, analysis.ladj.rowbegin, ref analysis.currowbegin, _params);

            //
            // Perform recursive processing
            //
            b.val = false;
            spsymmfactorizeblockrec(analysis, analysis.currowbegin, 0, true, b, _params);
            result = !b.val;
            return result;
        }


        /*************************************************************************
        Extracts result of the last Cholesky/LDLT factorization performed  on  the
        Analysis object.

        Following calls will  result in the undefined behavior:
        * calling for Analysis that was not factorized with SPSymmFactorize()
        * calling after SPSymmFactorize() returned False

        INPUT PARAMETERS:
            Analysis    -   prior factorization performed on some sparse matrix
            D, P        -   possibly preallocated buffers

        OUTPUT PARAMETERS:
            A           -   Cholesky/LDLT decomposition  of A stored in CRS format
                            in LOWER triangle.
            D           -   array[N], diagonal factor. If no diagonal  factor  was
                            required during analysis  phase,  still  returned  but
                            filled with units.
            P           -   array[N], pivots. Permutation matrix P is a product of
                            P(0)*P(1)*...*P(N-1), where P(i) is a  permutation  of
                            row/col I and P[I] (with P[I]>=I).
                            If no permutation was requested during analysis phase,
                            still returned but filled with unit elements.

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        public static void spsymmextract(spcholanalysis analysis,
            sparse.sparsematrix a,
            ref double[] d,
            ref int[] p,
            xparams _params)
        {
            extractmatrix(analysis, analysis.rowoffsets, analysis.rowstrides, analysis.outputstorage, analysis.diagd, analysis.n, a, ref d, ref p, analysis.tmp0, _params);
        }


        /*************************************************************************
        Solve linear system A*x=b, using internally stored  factorization  of  the
        matrix A.

        Works faster than extracting the matrix and solving with SparseTRSV()  due
        to SIMD-friendly supernodal data structures being used.

        INPUT PARAMETERS:
            Analysis    -   prior factorization performed on some sparse matrix
            B           -   array[N], right-hand side

        OUTPUT PARAMETERS:
            B           -   overwritten by X

          -- ALGLIB routine --
             08.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void spsymmsolve(spcholanalysis analysis,
            double[] b,
            xparams _params)
        {
            int n = 0;
            int i = 0;
            int j = 0;
            int k = 0;
            double v = 0;
            int baseoffs = 0;
            int cols0 = 0;
            int cols1 = 0;
            int offss = 0;
            int sstride = 0;
            int sidx = 0;
            int blocksize = 0;
            int rbase = 0;
            int offdiagsize = 0;
            double x0 = 0;
            double x1 = 0;
            double x2 = 0;
            double x3 = 0;

            n = analysis.n;
            ablasf.rsetallocv(n, 0.0, ref analysis.tmpx, _params);
            x0 = 0;
            x1 = 0;
            x2 = 0;
            x3 = 0;

            //
            // Handle left-hand side permutation, convert data to internal SIMD-friendly format
            //
            for (i = 0; i <= n - 1; i++)
            {
                analysis.tmpx[i] = b[analysis.inveffectiveperm[i]];
            }

            //
            // Solve for L*tmp_x=rhs.
            //
            for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
            {
                cols0 = analysis.supercolrange[sidx];
                cols1 = analysis.supercolrange[sidx + 1];
                blocksize = cols1 - cols0;
                offss = analysis.rowoffsets[sidx];
                sstride = analysis.rowstrides[sidx];
                rbase = analysis.superrowridx[sidx];
                offdiagsize = analysis.superrowridx[sidx + 1] - rbase;

                //
                // Solve for variables in the supernode,
                // fetch vars to locals (when supernode is small enough)
                //
                ap.assert(blocksize <= 4, "SPSymm: integrity check 4228 failed");
                if (blocksize == 1)
                {

                    //
                    // One column, fetch to X0
                    //
                    ap.assert(sstride == 1, "SPSymm: integrity check 4620 failed");
                    x0 = analysis.tmpx[cols0] / analysis.outputstorage[offss];
                    analysis.tmpx[cols0] = x0;
                }
                else
                {
                    if (blocksize == 2)
                    {

                        //
                        // Two columns, fetch to X0 and X1
                        //
                        ap.assert(sstride == 2, "SPSymm: integrity check 5730 failed");
                        for (i = cols0; i <= cols1 - 1; i++)
                        {
                            baseoffs = offss + (i - cols0) * sstride + -cols0;
                            v = analysis.tmpx[i];
                            for (j = cols0; j <= i - 1; j++)
                            {
                                v = v - analysis.outputstorage[baseoffs + j] * analysis.tmpx[j];
                            }
                            analysis.tmpx[i] = v / analysis.outputstorage[baseoffs + i];
                        }
                        x0 = analysis.tmpx[cols0];
                        x1 = analysis.tmpx[cols0 + 1];
                    }
                    else
                    {
                        if (blocksize == 3)
                        {

                            //
                            // Three columns, fetch to X0, X1 and X2
                            //
                            ap.assert(sstride == 4, "SPSymm: integrity check 7446 failed");
                            for (i = cols0; i <= cols1 - 1; i++)
                            {
                                baseoffs = offss + (i - cols0) * sstride + -cols0;
                                v = analysis.tmpx[i];
                                for (j = cols0; j <= i - 1; j++)
                                {
                                    v = v - analysis.outputstorage[baseoffs + j] * analysis.tmpx[j];
                                }
                                analysis.tmpx[i] = v / analysis.outputstorage[baseoffs + i];
                            }
                            x0 = analysis.tmpx[cols0];
                            x1 = analysis.tmpx[cols0 + 1];
                            x2 = analysis.tmpx[cols0 + 2];
                        }
                        else
                        {
                            if (blocksize == 4)
                            {

                                //
                                // Four columns, fetch to X0, X1, X2, X3
                                //
                                ap.assert(sstride == 4, "SPSymm: integrity check 9252 failed");
                                for (i = cols0; i <= cols1 - 1; i++)
                                {
                                    baseoffs = offss + (i - cols0) * sstride + -cols0;
                                    v = analysis.tmpx[i];
                                    for (j = cols0; j <= i - 1; j++)
                                    {
                                        v = v - analysis.outputstorage[baseoffs + j] * analysis.tmpx[j];
                                    }
                                    analysis.tmpx[i] = v / analysis.outputstorage[baseoffs + i];
                                }
                                x0 = analysis.tmpx[cols0];
                                x1 = analysis.tmpx[cols0 + 1];
                                x2 = analysis.tmpx[cols0 + 2];
                                x3 = analysis.tmpx[cols0 + 3];
                            }
                            else
                            {

                                //
                                // Generic case
                                //
                                for (i = cols0; i <= cols1 - 1; i++)
                                {
                                    baseoffs = offss + (i - cols0) * sstride + -cols0;
                                    v = analysis.tmpx[i];
                                    for (j = cols0; j <= i - 1; j++)
                                    {
                                        v = v - analysis.outputstorage[baseoffs + j] * analysis.tmpx[j];
                                    }
                                    analysis.tmpx[i] = v / analysis.outputstorage[baseoffs + i];
                                }
                            }
                        }
                    }
                }

                //
                // Propagate update to other variables
                //
                if (blocksize == 1)
                {

                    //
                    // Special case: single column
                    //
                    baseoffs = offss + 1;
                    for (k = 0; k <= offdiagsize - 1; k++)
                    {
                        i = analysis.superrowidx[rbase + k];
                        analysis.tmpx[i] = analysis.tmpx[i] - analysis.outputstorage[baseoffs] * x0;
                        baseoffs = baseoffs + 1;
                    }
                }
                else
                {
                    if (blocksize == 2)
                    {

                        //
                        // Two columns
                        //
                        baseoffs = offss + 4;
                        for (k = 0; k <= offdiagsize - 1; k++)
                        {
                            i = analysis.superrowidx[rbase + k];
                            analysis.tmpx[i] = analysis.tmpx[i] - analysis.outputstorage[baseoffs] * x0 - analysis.outputstorage[baseoffs + 1] * x1;
                            baseoffs = baseoffs + 2;
                        }
                    }
                    else
                    {
                        if (blocksize == 3)
                        {

                            //
                            // Three columns
                            //
                            baseoffs = offss + 12;
                            for (k = 0; k <= offdiagsize - 1; k++)
                            {
                                i = analysis.superrowidx[rbase + k];
                                analysis.tmpx[i] = analysis.tmpx[i] - analysis.outputstorage[baseoffs] * x0 - analysis.outputstorage[baseoffs + 1] * x1 - analysis.outputstorage[baseoffs + 2] * x2;
                                baseoffs = baseoffs + 4;
                            }
                        }
                        else
                        {
                            if (blocksize == 4)
                            {

                                //
                                // Four columns
                                //
                                baseoffs = offss + 16;
                                for (k = 0; k <= offdiagsize - 1; k++)
                                {
                                    i = analysis.superrowidx[rbase + k];
                                    analysis.tmpx[i] = analysis.tmpx[i] - analysis.outputstorage[baseoffs] * x0 - analysis.outputstorage[baseoffs + 1] * x1 - analysis.outputstorage[baseoffs + 2] * x2 - analysis.outputstorage[baseoffs + 3] * x3;
                                    baseoffs = baseoffs + 4;
                                }
                            }
                            else
                            {

                                //
                                // Generic propagate
                                //
                                for (k = 0; k <= offdiagsize - 1; k++)
                                {
                                    i = analysis.superrowidx[rbase + k];
                                    baseoffs = offss + (k + blocksize) * sstride;
                                    v = analysis.tmpx[i];
                                    for (j = 0; j <= blocksize - 1; j++)
                                    {
                                        v = v - analysis.outputstorage[baseoffs + j] * analysis.tmpx[cols0 + j];
                                    }
                                    analysis.tmpx[i] = v;
                                }
                            }
                        }
                    }
                }
            }

            //
            // Solve for D*tmp_x=rhs.
            //
            for (i = 0; i <= n - 1; i++)
            {
                if (analysis.diagd[i] != 0.0)
                {
                    analysis.tmpx[i] = analysis.tmpx[i] / analysis.diagd[i];
                }
                else
                {
                    analysis.tmpx[i] = 0.0;
                }
            }

            //
            // Solve for L'*tmp_x=rhs
            //
            //
            for (sidx = analysis.nsuper - 1; sidx >= 0; sidx--)
            {
                cols0 = analysis.supercolrange[sidx];
                cols1 = analysis.supercolrange[sidx + 1];
                blocksize = cols1 - cols0;
                offss = analysis.rowoffsets[sidx];
                sstride = analysis.rowstrides[sidx];
                rbase = analysis.superrowridx[sidx];
                offdiagsize = analysis.superrowridx[sidx + 1] - rbase;

                //
                // Subtract already computed variables
                //
                if (blocksize == 1)
                {

                    //
                    // Single column, use value fetched in X0
                    //
                    x0 = analysis.tmpx[cols0];
                    baseoffs = offss + 1;
                    for (k = 0; k <= offdiagsize - 1; k++)
                    {
                        x0 = x0 - analysis.outputstorage[baseoffs] * analysis.tmpx[analysis.superrowidx[rbase + k]];
                        baseoffs = baseoffs + 1;
                    }
                    analysis.tmpx[cols0] = x0;
                }
                else
                {
                    if (blocksize == 2)
                    {

                        //
                        // Two columns, use values fetched in X0, X1
                        //
                        x0 = analysis.tmpx[cols0];
                        x1 = analysis.tmpx[cols0 + 1];
                        baseoffs = offss + 4;
                        for (k = 0; k <= offdiagsize - 1; k++)
                        {
                            v = analysis.tmpx[analysis.superrowidx[rbase + k]];
                            x0 = x0 - analysis.outputstorage[baseoffs] * v;
                            x1 = x1 - analysis.outputstorage[baseoffs + 1] * v;
                            baseoffs = baseoffs + 2;
                        }
                        analysis.tmpx[cols0] = x0;
                        analysis.tmpx[cols0 + 1] = x1;
                    }
                    else
                    {
                        if (blocksize == 3)
                        {

                            //
                            // Three columns, use values fetched in X0, X1, X2
                            //
                            x0 = analysis.tmpx[cols0];
                            x1 = analysis.tmpx[cols0 + 1];
                            x2 = analysis.tmpx[cols0 + 2];
                            baseoffs = offss + 12;
                            for (k = 0; k <= offdiagsize - 1; k++)
                            {
                                v = analysis.tmpx[analysis.superrowidx[rbase + k]];
                                x0 = x0 - analysis.outputstorage[baseoffs] * v;
                                x1 = x1 - analysis.outputstorage[baseoffs + 1] * v;
                                x2 = x2 - analysis.outputstorage[baseoffs + 2] * v;
                                baseoffs = baseoffs + 4;
                            }
                            analysis.tmpx[cols0] = x0;
                            analysis.tmpx[cols0 + 1] = x1;
                            analysis.tmpx[cols0 + 2] = x2;
                        }
                        else
                        {
                            if (blocksize == 4)
                            {

                                //
                                // Four columns, use values fetched in X0, X1, X2, X3
                                //
                                x0 = analysis.tmpx[cols0];
                                x1 = analysis.tmpx[cols0 + 1];
                                x2 = analysis.tmpx[cols0 + 2];
                                x3 = analysis.tmpx[cols0 + 3];
                                baseoffs = offss + 16;
                                for (k = 0; k <= offdiagsize - 1; k++)
                                {
                                    v = analysis.tmpx[analysis.superrowidx[rbase + k]];
                                    x0 = x0 - analysis.outputstorage[baseoffs] * v;
                                    x1 = x1 - analysis.outputstorage[baseoffs + 1] * v;
                                    x2 = x2 - analysis.outputstorage[baseoffs + 2] * v;
                                    x3 = x3 - analysis.outputstorage[baseoffs + 3] * v;
                                    baseoffs = baseoffs + 4;
                                }
                                analysis.tmpx[cols0] = x0;
                                analysis.tmpx[cols0 + 1] = x1;
                                analysis.tmpx[cols0 + 2] = x2;
                                analysis.tmpx[cols0 + 3] = x3;
                            }
                            else
                            {

                                //
                                // Generic case
                                //
                                for (k = 0; k <= offdiagsize - 1; k++)
                                {
                                    baseoffs = offss + (k + blocksize) * sstride;
                                    v = analysis.tmpx[analysis.superrowidx[rbase + k]];
                                    for (j = 0; j <= blocksize - 1; j++)
                                    {
                                        analysis.tmpx[cols0 + j] = analysis.tmpx[cols0 + j] - analysis.outputstorage[baseoffs + j] * v;
                                    }
                                }
                            }
                        }
                    }
                }

                //
                // Solve for variables in the supernode
                //
                for (i = blocksize - 1; i >= 0; i--)
                {
                    baseoffs = offss + i * sstride;
                    v = analysis.tmpx[cols0 + i] / analysis.outputstorage[baseoffs + i];
                    for (j = 0; j <= i - 1; j++)
                    {
                        analysis.tmpx[cols0 + j] = analysis.tmpx[cols0 + j] - v * analysis.outputstorage[baseoffs + j];
                    }
                    analysis.tmpx[cols0 + i] = v;
                }
            }

            //
            // Handle right-hand side permutation, convert data to internal SIMD-friendly format
            //
            for (i = 0; i <= n - 1; i++)
            {
                b[i] = analysis.tmpx[analysis.effectiveperm[i]];
            }
        }


        /*************************************************************************
        Compares diag(L*L') with that of the original A and returns  two  metrics:
        * SumSq - sum of squares of diag(A)
        * ErrSq - sum of squared errors, i.e. Frobenius norm of diag(L*L')-diag(A)

        These metrics can be used to check accuracy of the factorization.

        INPUT PARAMETERS:
            Analysis    -   prior factorization performed on some sparse matrix

        OUTPUT PARAMETERS:
            SumSq, ErrSq-   diagonal magnitude and absolute diagonal error

          -- ALGLIB routine --
             08.09.2021
             Bochkanov Sergey
        *************************************************************************/
        public static void spsymmdiagerr(spcholanalysis analysis,
            ref double sumsq,
            ref double errsq,
            xparams _params)
        {
            int n = 0;
            double v = 0;
            double vv = 0;
            int simdwidth = 0;
            int baseoffs = 0;
            int cols0 = 0;
            int cols1 = 0;
            int offss = 0;
            int sstride = 0;
            int sidx = 0;
            int blocksize = 0;
            int rbase = 0;
            int offdiagsize = 0;
            int i = 0;
            int j = 0;
            int k = 0;

            sumsq = 0;
            errsq = 0;

            n = analysis.n;
            simdwidth = 1;

            //
            // Scan L, compute diag(L*L')
            //
            ablasf.rsetallocv(simdwidth * n, 0.0, ref analysis.simdbuf, _params);
            for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
            {
                cols0 = analysis.supercolrange[sidx];
                cols1 = analysis.supercolrange[sidx + 1];
                blocksize = cols1 - cols0;
                offss = analysis.rowoffsets[sidx];
                sstride = analysis.rowstrides[sidx];
                rbase = analysis.superrowridx[sidx];
                offdiagsize = analysis.superrowridx[sidx + 1] - rbase;

                //
                // Handle triangular diagonal block
                //
                for (i = cols0; i <= cols1 - 1; i++)
                {
                    baseoffs = offss + (i - cols0) * sstride + -cols0;
                    v = 0;
                    for (j = 0; j <= simdwidth - 1; j++)
                    {
                        v = v + analysis.simdbuf[i * simdwidth + j];
                    }
                    for (j = cols0; j <= i; j++)
                    {
                        vv = analysis.outputstorage[baseoffs + j];
                        v = v + vv * vv * analysis.diagd[j];
                    }
                    sumsq = sumsq + math.sqr(analysis.inputstorage[baseoffs + i]);
                    errsq = errsq + math.sqr(analysis.inputstorage[baseoffs + i] - v);
                }

                //
                // Accumulate entries below triangular diagonal block
                //
                for (k = 0; k <= offdiagsize - 1; k++)
                {
                    i = analysis.superrowidx[rbase + k];
                    baseoffs = offss + (k + blocksize) * sstride;
                    v = analysis.simdbuf[i * simdwidth];
                    for (j = 0; j <= blocksize - 1; j++)
                    {
                        vv = analysis.outputstorage[baseoffs + j];
                        v = v + vv * vv * analysis.diagd[cols0 + j];
                    }
                    analysis.simdbuf[i * simdwidth] = v;
                }
            }
        }


#if ALGLIB_NO_FAST_KERNELS
    /*************************************************************************
    Informational function, useful for debugging
    *************************************************************************/
    private static int spsymmgetmaxsimd(xparams _params)
    {
        int result = 0;

        result = 1;
        return result;
    }
#endif


        /*************************************************************************
        Recursive factorization of the supernodal  block  (a set of interdependent
        supernodes) stored at BlkStruct[] at offset BlkOffs.

        Due to interdependencies, block supernodes can NOT be  factorized  in  the
        parallel manner. However, it is still possible to perform  parallel column
        updates (this part is handled by parallel DC  tree  associated  with  each
        block).

        INPUT PARAMETERS:
            Analysis    -   prior  analysis  performed on some sparse matrix, with
                            matrix being stored in Analysis.
            CurLAdjRowBegin-array[NSuper], pointers to unprocessed column  updates
                            stored in LAdj structure. Initially it is  a  copy  of
                            LAdj.RowBegin[], but every time  we  apply  update  to
                            a column, we advance its CurLAdjRowBegin[] entry.
                            This array is shared between all concurrently  running
                            worker threads.
            BlkOffs     -   block offset relative to the beginning of BlkStruct[]

        OUTPUT PARAMETERS:
            Analysis    -   contains factorization results
            FailureFlag -   on the failure is set to True, ignored on success.
                            Such design is thread-safe.

        The function returns True  when  factorization  resulted  in nondegenerate
        matrix. False is returned when factorization fails (Cholesky factorization
        of indefinite matrix) or LDLT factorization has exactly zero  elements  at
        the diagonal.

          -- ALGLIB routine --
             09.07.2022
             Bochkanov Sergey
        *************************************************************************/
        private static void spsymmfactorizeblockrec(spcholanalysis analysis,
            int[] curladjrowbegin,
            int blkoffs,
            bool isrootcall,
            apserv.sboolean failureflag,
            xparams _params)
        {
            int bs = 0;
            int cc = 0;
            int curoffs = 0;
            int childrenlistoffs = 0;
            int gidx = 0;
            int groupscnt = 0;
            int i = 0;

            ap.assert(analysis.tasktype == 0, "SPCholFactorize: Analysis type does not match current task");

            //
            // Try parallel execution
            //
            if (isrootcall && analysis.useparallelism)
            {
                if (_trypexec_spsymmfactorizeblockrec(analysis, curladjrowbegin, blkoffs, isrootcall, failureflag, _params))
                {
                    return;
                }
            }

            //
            // Analyze block information
            //
            curoffs = blkoffs;
            bs = analysis.blkstruct[curoffs];
            curoffs = curoffs + 1 + bs;
            cc = analysis.blkstruct[curoffs];
            childrenlistoffs = curoffs + 2;
            curoffs = curoffs + 2 + cc;

            //
            // Process children.
            // The very fact that we created more than one children node means that parallel processing is needed.
            //
            if (cc > 0)
            {
                for (i = 0; i <= cc - 1; i++)
                {
                    spsymmfactorizeblockrec(analysis, curladjrowbegin, analysis.blkstruct[childrenlistoffs + i], false, failureflag, _params);
                }
            }

            //
            // Apply precomputed update-and-factorize sequence stored in the UPDATES section of the block
            //
            groupscnt = analysis.blkstruct[curoffs + 1];
            curoffs = curoffs + updatesheadersize;
            for (gidx = 0; gidx <= groupscnt - 1; gidx++)
            {
                spsymmprocessupdatesgroup(analysis, curoffs, failureflag, _params);
                curoffs = curoffs + analysis.blkstruct[curoffs + 0];
            }
        }


        /*************************************************************************
        Serial stub for GPL edition.
        *************************************************************************/
        public static bool _trypexec_spsymmfactorizeblockrec(spcholanalysis analysis,
            int[] curladjrowbegin,
            int blkoffs,
            bool isrootcall,
            apserv.sboolean failureflag, xparams _params)
        {
            return false;
        }


        /*************************************************************************
        This function processes updates group (a set of precomputed update batches
        that can be applied concurrently) stored in Analysis.BlkStruct

        INPUT PARAMETERS:
            Analysis    -   prior  analysis  performed on some sparse matrix, with
                            matrix being stored in Analysis.
            BlkOffs     -   block offset relative to the beginning of BlkStruct[],
                            beginning of the group data

        OUTPUT PARAMETERS:
            Analysis    -   partial supernode update is applied
            FailureFlag -   on the failure is set to True, ignored on success.
                            Such design is thread-safe.

          -- ALGLIB routine --
             09.07.2022
             Bochkanov Sergey
        *************************************************************************/
        private static void spsymmprocessupdatesgroup(spcholanalysis analysis,
            int blkoffs,
            apserv.sboolean failureflag,
            xparams _params)
        {
            int bidx = 0;
            int batchescnt = 0;

            batchescnt = analysis.blkstruct[blkoffs + 1];
            blkoffs = blkoffs + groupheadersize;

            //
            // One batch, sequential processing
            //
            if (batchescnt == 1)
            {
                spsymmprocessupdatesbatch(analysis, blkoffs, failureflag, _params);
                return;
            }

            //
            // Parallel processing (more than one batch in the group means that we decided
            // to parallelize computations during the scheduling stage)
            //
            for (bidx = 0; bidx <= batchescnt - 1; bidx++)
            {
                spsymmprocessupdatesbatch(analysis, blkoffs, failureflag, _params);
                blkoffs = blkoffs + analysis.blkstruct[blkoffs + 0];
            }
        }


        /*************************************************************************
        Serial stub for GPL edition.
        *************************************************************************/
        public static bool _trypexec_spsymmprocessupdatesgroup(spcholanalysis analysis,
            int blkoffs,
            apserv.sboolean failureflag, xparams _params)
        {
            return false;
        }


        /*************************************************************************
        This function processes updates batch (a set of precomputed update
        sequences that has to be applied sequentially) stored in Analysis.BlkStruct

        INPUT PARAMETERS:
            Analysis    -   prior  analysis  performed on some sparse matrix, with
                            matrix being stored in Analysis.
            BlkOffs     -   block offset relative to the beginning of BlkStruct[],
                            beginning of the batch data

        OUTPUT PARAMETERS:
            Analysis    -   partial supernode update is applied
            FailureFlag -   on the failure is set to True, ignored on success.
                            Such design is thread-safe.

          -- ALGLIB routine --
             09.07.2022
             Bochkanov Sergey
        *************************************************************************/
        private static void spsymmprocessupdatesbatch(spcholanalysis analysis,
            int blkoffs,
            apserv.sboolean failureflag,
            xparams _params)
        {
            int seqidx = 0;
            int sequencescnt = 0;
            int sidx = 0;
            int cols0 = 0;
            int cols1 = 0;
            int supernodesize = 0;
            int offss = 0;
            int i = 0;
            int k = 0;
            int k0 = 0;
            int k1 = 0;
            int i0 = 0;
            int i1 = 0;
            int[] raw2smap = new int[0];

            apserv.nipoolretrieve(analysis.nintegerpool, ref raw2smap, _params);
            sequencescnt = analysis.blkstruct[blkoffs + 1];
            blkoffs = blkoffs + batchheadersize;
            for (seqidx = 0; seqidx <= sequencescnt - 1; seqidx++)
            {
                sidx = analysis.blkstruct[blkoffs + 0];
                i0 = analysis.blkstruct[blkoffs + 1];
                i1 = analysis.blkstruct[blkoffs + 2];
                cols0 = analysis.supercolrange[sidx];
                cols1 = analysis.supercolrange[sidx + 1];
                supernodesize = cols1 - cols0;
                offss = analysis.rowoffsets[sidx];

                //
                // Prepare mapping of raw (range 0...N-1) indexes into internal (range 0...SupernodeSize+OffdiagSize-1) ones
                //
                for (i = cols0; i <= cols1 - 1; i++)
                {
                    raw2smap[i] = i - cols0;
                }
                k0 = analysis.superrowridx[sidx];
                k1 = analysis.superrowridx[sidx + 1] - 1;
                for (k = k0; k <= k1; k++)
                {
                    raw2smap[analysis.superrowidx[k]] = supernodesize + (k - k0);
                }

                //
                // Update current supernode with remaining updates.
                //
                updatesupernode(analysis, sidx, cols0, cols1, offss, raw2smap, i0, i1, analysis.diagd, _params);

                //
                // Factorize current supernode if last update was applied
                //
                if (i1 == analysis.ladj.rowend[sidx] && !factorizesupernode(analysis, sidx, _params))
                {
                    apserv.nipoolrecycle(analysis.nintegerpool, ref raw2smap, _params);
                    failureflag.val = true;
                    return;
                }

                //
                // Next sequence
                //
                blkoffs = blkoffs + sequenceentrysize;
            }
            apserv.nipoolrecycle(analysis.nintegerpool, ref raw2smap, _params);
        }


        /*************************************************************************
        Serial stub for GPL edition.
        *************************************************************************/
        public static bool _trypexec_spsymmprocessupdatesbatch(spcholanalysis analysis,
            int blkoffs,
            apserv.sboolean failureflag, xparams _params)
        {
            return false;
        }


        /*************************************************************************
        Print blocked elimination tree to trace log.

          -- ALGLIB routine --
             09.07.2022
             Bochkanov Sergey
        *************************************************************************/
        private static void printblockedeliminationtreerec(spcholanalysis analysis,
            int blkoffs,
            int depth,
            xparams _params)
        {
            int curoffs = 0;
            int bs = 0;
            int cc = 0;
            int i = 0;
            int supernodeslistoffs = 0;
            int childrenlistoffs = 0;
            double selfcost = 0;
            double avgsnode = 0;
            int bidx = 0;
            int sidx = 0;
            int cols0 = 0;
            int cols1 = 0;

            curoffs = blkoffs;
            bs = analysis.blkstruct[curoffs];
            supernodeslistoffs = curoffs + 1;
            curoffs = curoffs + 1 + bs;
            cc = analysis.blkstruct[curoffs];
            childrenlistoffs = curoffs + 2;
            curoffs = curoffs + 2 + cc;

            //
            // Print blocked elimination tree node
            //
            selfcost = 0;
            avgsnode = 0;
            for (bidx = 0; bidx <= bs - 1; bidx++)
            {
                sidx = analysis.blkstruct[supernodeslistoffs + bidx];
                cols0 = analysis.supercolrange[sidx];
                cols1 = analysis.supercolrange[sidx + 1];
                avgsnode = avgsnode + (double)(cols1 - cols0) / (double)bs;
                selfcost = selfcost + analysis.ladj.nflop[sidx];
            }
            apserv.tracespaces(depth, _params);
            ap.trace(System.String.Format("* block of {0,0:d} supernodes (avg.size={1,0:F1})", bs, avgsnode));
            if (cc > 0)
            {
                ap.trace(System.String.Format(", {0,0:d} children", cc));
            }
            ap.trace(System.String.Format(", update-and-factorize = {0,0:F1} MFLOP", 1.0E-6 * selfcost));
            ap.trace("\n");

            //
            // Print children.
            //
            for (i = 0; i <= cc - 1; i++)
            {
                printblockedeliminationtreerec(analysis, analysis.blkstruct[childrenlistoffs + i], depth + 1, _params);
            }
        }


#if ALGLIB_NO_FAST_KERNELS
    /*************************************************************************
    Solving linear system: propagating computed supernode.

    Propagates computed supernode to the rest of the RHS  using  SIMD-friendly
    RHS storage format.

    INPUT PARAMETERS:

    OUTPUT PARAMETERS:

      -- ALGLIB routine --
         08.09.2021
         Bochkanov Sergey
    *************************************************************************/
    private static void propagatefwd(double[] x,
        int cols0,
        int blocksize,
        int[] superrowidx,
        int rbase,
        int offdiagsize,
        double[] rowstorage,
        int offss,
        int sstride,
        double[] simdbuf,
        int simdwidth,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int baseoffs = 0;
        double v = 0;

        for(k=0; k<=offdiagsize-1; k++)
        {
            i = superrowidx[rbase+k];
            baseoffs = offss+(k+blocksize)*sstride;
            v = simdbuf[i*simdwidth];
            for(j=0; j<=blocksize-1; j++)
            {
                v = v-rowstorage[baseoffs+j]*x[cols0+j];
            }
            simdbuf[i*simdwidth] = v;
        }
    }
#endif


        /*************************************************************************
        This function generates test reodering used for debug purposes only

        INPUT PARAMETERS
            A           -   lower triangular sparse matrix in CRS format
            N           -   problem size

        OUTPUT PARAMETERS
            Perm        -   array[N], maps original indexes I to permuted indexes
            InvPerm     -   array[N], maps permuted indexes I to original indexes

          -- ALGLIB PROJECT --
             Copyright 05.10.2020 by Bochkanov Sergey.
        *************************************************************************/
        private static void generatedbgpermutation(sparse.sparsematrix a,
            int n,
            ref int[] perm,
            ref int[] invperm,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            int j0 = 0;
            int j1 = 0;
            int jj = 0;
            double[] d = new double[0];
            double[] tmpr = new double[0];
            int[] tmpperm = new int[0];


            //
            // Initialize D by vertex degrees
            //
            ablasf.rsetallocv(n, 0, ref d, _params);
            for (i = 0; i <= n - 1; i++)
            {
                j0 = a.ridx[i];
                j1 = a.didx[i] - 1;
                d[i] = j1 - j0 + 1;
                for (jj = j0; jj <= j1; jj++)
                {
                    j = a.idx[jj];
                    d[j] = d[j] + 1;
                }
            }

            //
            // Prepare permutation that orders vertices by degrees
            //
            ablasf.iallocv(n, ref invperm, _params);
            for (i = 0; i <= n - 1; i++)
            {
                invperm[i] = i;
            }
            tsort.tagsortfasti(ref d, ref invperm, ref tmpr, ref tmpperm, n, _params);
            ablasf.iallocv(n, ref perm, _params);
            for (i = 0; i <= n - 1; i++)
            {
                perm[invperm[i]] = i;
            }
        }


        /*************************************************************************
        This function builds elimination tree in the original column order

        INPUT PARAMETERS
            A           -   lower triangular sparse matrix in CRS format
            N           -   problem size
            Parent,
            tAbove      -   preallocated temporary array, length at least N+1, no
                            meaningful output is provided in these variables

        OUTPUT PARAMETERS
            Parent      -   array[N], Parent[I] contains index of parent of I-th
                            column. -1 is used to denote column with no parents.

          -- ALGLIB PROJECT --
             Copyright 15.08.2021 by Bochkanov Sergey.
        *************************************************************************/
        private static void buildunorderedetree(sparse.sparsematrix a,
            int n,
            int[] parent,
            int[] tabove,
            xparams _params)
        {
            int r = 0;
            int abover = 0;
            int i = 0;
            int j = 0;
            int k = 0;
            int j0 = 0;
            int j1 = 0;
            int jj = 0;

            ap.assert(ap.len(parent) >= n + 1, "BuildUnorderedETree: input buffer Parent is too short");
            ap.assert(ap.len(tabove) >= n + 1, "BuildUnorderedETree: input buffer tAbove is too short");

            //
            // Build elimination tree using Liu's algorithm with path compression
            //
            for (j = 0; j <= n - 1; j++)
            {
                parent[j] = n;
                tabove[j] = n;
                j0 = a.ridx[j];
                j1 = a.didx[j] - 1;
                for (jj = j0; jj <= j1; jj++)
                {
                    r = a.idx[jj];
                    abover = tabove[r];
                    while (abover < j)
                    {
                        k = abover;
                        tabove[r] = j;
                        r = k;
                        abover = tabove[r];
                    }
                    if (abover == n)
                    {
                        tabove[r] = j;
                        parent[r] = j;
                    }
                }
            }

            //
            // Convert to external format
            //
            for (i = 0; i <= n - 1; i++)
            {
                if (parent[i] == n)
                {
                    parent[i] = -1;
                }
            }
        }


        /*************************************************************************
        This function analyzes  elimination  tree  stored  using  'parent-of-node'
        format and converts it to the 'childrens-of-node' format.

        INPUT PARAMETERS
            Parent      -   array[N], supernodal etree
            N           -   problem size
            ChildrenR,
            ChildrenI,
            tTmp0       -   preallocated arrays, length at least N+1

        OUTPUT PARAMETERS
            ChildrenR   -   array[N+1], children range (see below)
            ChildrenI   -   array[N+1], childrens of K-th node are stored  in  the
                            elements ChildrenI[ChildrenR[K]...ChildrenR[K+1]-1]

          -- ALGLIB PROJECT --
             Copyright 05.10.2020 by Bochkanov Sergey.
        *************************************************************************/
        private static void fromparenttochildren(int[] parent,
            int n,
            int[] childrenr,
            int[] childreni,
            int[] ttmp0,
            xparams _params)
        {
            int i = 0;
            int k = 0;
            int nodeidx = 0;

            ap.assert(ap.len(ttmp0) >= n + 1, "FromParentToChildren: input buffer tTmp0 is too short");
            ap.assert(ap.len(childrenr) >= n + 1, "FromParentToChildren: input buffer ChildrenR is too short");
            ap.assert(ap.len(childreni) >= n + 1, "FromParentToChildren: input buffer ChildrenI is too short");

            //
            // Convert etree from per-column parent array to per-column children list
            //
            ablasf.isetv(n, 0, ttmp0, _params);
            for (i = 0; i <= n - 1; i++)
            {
                nodeidx = parent[i];
                if (nodeidx >= 0)
                {
                    ttmp0[nodeidx] = ttmp0[nodeidx] + 1;
                }
            }
            childrenr[0] = 0;
            for (i = 0; i <= n - 1; i++)
            {
                childrenr[i + 1] = childrenr[i] + ttmp0[i];
            }
            ablasf.isetv(n, 0, ttmp0, _params);
            for (i = 0; i <= n - 1; i++)
            {
                k = parent[i];
                if (k >= 0)
                {
                    childreni[childrenr[k] + ttmp0[k]] = i;
                    ttmp0[k] = ttmp0[k] + 1;
                }
            }
        }


        /*************************************************************************
        This function builds elimination tree and reorders  it  according  to  the
        topological post-ordering.

        INPUT PARAMETERS
            A           -   lower triangular sparse matrix in CRS format
            N           -   problem size

            tRawParentOfRawNode,
            tRawParentOfReorderedNode,
            tTmp,
            tFlagArray  -   preallocated temporary arrays, length at least N+1, no
                            meaningful output is provided in these variables

        OUTPUT PARAMETERS
            Parent      -   array[N], Parent[I] contains index of parent of I-th
                            column (after topological reordering). -1 is used to
                            denote column with no parents.
            SupernodalPermutation
                        -   array[N], maps original indexes I to permuted indexes
            InvSupernodalPermutation
                        -   array[N], maps permuted indexes I to original indexes

          -- ALGLIB PROJECT --
             Copyright 05.10.2020 by Bochkanov Sergey.
        *************************************************************************/
        private static void buildorderedetree(sparse.sparsematrix a,
            int n,
            ref int[] parent,
            ref int[] supernodalpermutation,
            ref int[] invsupernodalpermutation,
            int[] trawparentofrawnode,
            int[] trawparentofreorderednode,
            int[] ttmp,
            bool[] tflagarray,
            xparams _params)
        {
            int i = 0;
            int k = 0;
            int sidx = 0;
            int unprocessedchildrencnt = 0;

            ap.assert(ap.len(trawparentofrawnode) >= n + 1, "BuildOrderedETree: input buffer tRawParentOfRawNode is too short");
            ap.assert(ap.len(ttmp) >= n + 1, "BuildOrderedETree: input buffer tTmp is too short");
            ap.assert(ap.len(trawparentofreorderednode) >= n + 1, "BuildOrderedETree: input buffer tRawParentOfReorderedNode is too short");
            ap.assert(ap.len(tflagarray) >= n + 1, "BuildOrderedETree: input buffer tFlagArray is too short");

            //
            // Avoid spurious compiler warnings
            //
            unprocessedchildrencnt = 0;

            //
            // Build elimination tree with original column order
            //
            buildunorderedetree(a, n, trawparentofrawnode, ttmp, _params);

            //
            // Compute topological ordering of the elimination tree, produce:
            // * direct and inverse permutations
            // * reordered etree stored in Parent[]
            //
            ablasf.isetallocv(n, -1, ref invsupernodalpermutation, _params);
            ablasf.isetallocv(n, -1, ref supernodalpermutation, _params);
            ablasf.isetallocv(n, -1, ref parent, _params);
            ablasf.isetv(n, -1, trawparentofreorderednode, _params);
            ablasf.isetv(n, 0, ttmp, _params);
            for (i = 0; i <= n - 1; i++)
            {
                k = trawparentofrawnode[i];
                if (k >= 0)
                {
                    ttmp[k] = ttmp[k] + 1;
                }
            }
            ablasf.bsetv(n, true, tflagarray, _params);
            sidx = 0;
            for (i = 0; i <= n - 1; i++)
            {
                if (tflagarray[i])
                {

                    //
                    // Move column I to position SIdx, decrease unprocessed children count
                    //
                    supernodalpermutation[i] = sidx;
                    invsupernodalpermutation[sidx] = i;
                    tflagarray[i] = false;
                    k = trawparentofrawnode[i];
                    trawparentofreorderednode[sidx] = k;
                    if (k >= 0)
                    {
                        unprocessedchildrencnt = ttmp[k] - 1;
                        ttmp[k] = unprocessedchildrencnt;
                    }
                    sidx = sidx + 1;

                    //
                    // Add parents (as long as parent has no unprocessed children)
                    //
                    while (k >= 0 && unprocessedchildrencnt == 0)
                    {
                        supernodalpermutation[k] = sidx;
                        invsupernodalpermutation[sidx] = k;
                        tflagarray[k] = false;
                        k = trawparentofrawnode[k];
                        trawparentofreorderednode[sidx] = k;
                        if (k >= 0)
                        {
                            unprocessedchildrencnt = ttmp[k] - 1;
                            ttmp[k] = unprocessedchildrencnt;
                        }
                        sidx = sidx + 1;
                    }
                }
            }
            for (i = 0; i <= n - 1; i++)
            {
                k = trawparentofreorderednode[i];
                if (k >= 0)
                {
                    parent[i] = supernodalpermutation[k];
                }
            }
        }


        /*************************************************************************
        This function analyzes postordered elimination tree and creates supernodal
        structure in Analysis object.

        INPUT PARAMETERS
            AT          -   upper triangular CRS matrix, transpose and  reordering
                            of the original input matrix A
            Parent      -   array[N], supernodal etree
            N           -   problem size

            tChildrenR,
            tChildrenI,
            tParentNodeOfSupernode,
            tNode2Supernode,
            tTmp0,
            tFlagArray  -   temporary arrays, length at least N+1, simply provide
                            preallocated place.

        OUTPUT PARAMETERS
            Analysis    -   following fields are initialized:
                            * Analysis.NSuper
                            * Analysis.SuperColRange
                            * Analysis.SuperRowRIdx
                            * Analysis.SuperRowIdx
                            * Analysis.ParentSupernode
                            * Analysis.ChildSupernodesRIdx, Analysis.ChildSupernodesIdx
                            * Analysis.OutRowCounts
                            other fields are ignored and not changed.
            Node2Supernode- array[N] that maps node indexes to supernode indexes

          -- ALGLIB PROJECT --
             Copyright 05.10.2020 by Bochkanov Sergey.
        *************************************************************************/
        private static void createsupernodalstructure(sparse.sparsematrix at,
            int[] parent,
            int n,
            spcholanalysis analysis,
            ref int[] node2supernode,
            int[] tchildrenr,
            int[] tchildreni,
            int[] tparentnodeofsupernode,
            int[] tfakenonzeros,
            int[] ttmp0,
            bool[] tflagarray,
            xparams _params)
        {
            int nsuper = 0;
            int i = 0;
            int j = 0;
            int k = 0;
            int sidx = 0;
            int i0 = 0;
            int ii = 0;
            int columnidx = 0;
            int nodeidx = 0;
            int rfirst = 0;
            int rlast = 0;
            int cols0 = 0;
            int cols1 = 0;
            int blocksize = 0;
            bool createsupernode = new bool();
            int colcount = 0;
            int offdiagcnt = 0;
            int childcolcount = 0;
            int childoffdiagcnt = 0;
            int fakezerosinnewsupernode = 0;
            double mergeinefficiency = 0;
            bool hastheonlychild = new bool();

            ap.assert(ap.len(ttmp0) >= n + 1, "CreateSupernodalStructure: input buffer tTmp0 is too short");
            ap.assert(ap.len(tchildrenr) >= n + 1, "CreateSupernodalStructure: input buffer ChildrenR is too short");
            ap.assert(ap.len(tchildreni) >= n + 1, "CreateSupernodalStructure: input buffer ChildrenI is too short");
            ap.assert(ap.len(tparentnodeofsupernode) >= n + 1, "CreateSupernodalStructure: input buffer tParentNodeOfSupernode is too short");
            ap.assert(ap.len(tfakenonzeros) >= n + 1, "CreateSupernodalStructure: input buffer tFakeNonzeros is too short");
            ap.assert(ap.len(tflagarray) >= n + 1, "CreateSupernodalStructure: input buffer tFlagArray is too short");

            //
            // Trace
            //
            if (analysis.dotracesupernodalstructure)
            {
                ap.trace("=== GENERATING SUPERNODAL STRUCTURE ================================================================\n");
            }

            //
            // Convert etree from per-column parent array to per-column children list
            //
            fromparenttochildren(parent, n, tchildrenr, tchildreni, ttmp0, _params);

            //
            // Analyze supernodal structure:
            // * determine children count for each node
            // * combine chains of children into supernodes
            // * generate direct and inverse supernodal (topological) permutations
            // * generate column structure of supernodes (after supernodal permutation)
            //
            ablasf.isetallocv(n, -1, ref node2supernode, _params);
            apserv.ivectorsetlengthatleast(ref analysis.supercolrange, n + 1, _params);
            apserv.ivectorsetlengthatleast(ref analysis.superrowridx, n + 1, _params);
            ablasf.isetv(n, n + 1, tparentnodeofsupernode, _params);
            ablasf.bsetv(n, true, tflagarray, _params);
            nsuper = 0;
            analysis.supercolrange[0] = 0;
            analysis.superrowridx[0] = 0;
            while (analysis.supercolrange[nsuper] < n)
            {
                columnidx = analysis.supercolrange[nsuper];

                //
                // Compute nonzero pattern of the column, create temporary standalone node
                // for possible supernodal merge. Newly created node has just one column
                // and no fake nonzeros.
                //
                rfirst = analysis.superrowridx[nsuper];
                rlast = computenonzeropattern(at, columnidx, n, analysis.superrowridx, ref analysis.superrowidx, nsuper, tchildrenr, tchildreni, node2supernode, tflagarray, ttmp0, _params);
                analysis.supercolrange[nsuper + 1] = columnidx + 1;
                analysis.superrowridx[nsuper + 1] = rlast;
                node2supernode[columnidx] = nsuper;
                tparentnodeofsupernode[nsuper] = parent[columnidx];
                tfakenonzeros[nsuper] = 0;
                offdiagcnt = rlast - rfirst;
                colcount = 1;
                nsuper = nsuper + 1;
                if (analysis.dotracesupernodalstructure)
                {
                    ap.trace(System.String.Format("> incoming column {0,0:d}\n", columnidx));
                    ap.trace(System.String.Format("offdiagnnz = {0,0:d}\n", rlast - rfirst));
                    ap.trace("children   = [ ");
                    for (i = tchildrenr[columnidx]; i <= tchildrenr[columnidx + 1] - 1; i++)
                    {
                        ap.trace(System.String.Format("S{0,0:d} ", node2supernode[tchildreni[i]]));
                    }
                    ap.trace("]\n");
                }

                //
                // Decide whether to merge column with previous supernode or not
                //
                childcolcount = 0;
                childoffdiagcnt = 0;
                mergeinefficiency = 0.0;
                fakezerosinnewsupernode = 0;
                createsupernode = false;
                hastheonlychild = false;
                if (nsuper >= 2 && tparentnodeofsupernode[nsuper - 2] == columnidx)
                {
                    childcolcount = analysis.supercolrange[nsuper - 1] - analysis.supercolrange[nsuper - 2];
                    childoffdiagcnt = analysis.superrowridx[nsuper - 1] - analysis.superrowridx[nsuper - 2];
                    hastheonlychild = tchildrenr[columnidx + 1] - tchildrenr[columnidx] == 1;
                    if ((hastheonlychild || relaxedsupernodes) && colcount + childcolcount <= maxsupernode)
                    {
                        i = colcount + childcolcount;
                        k = i * (i + 1) / 2 + offdiagcnt * i;
                        fakezerosinnewsupernode = tfakenonzeros[nsuper - 2] + tfakenonzeros[nsuper - 1] + (offdiagcnt - (childoffdiagcnt - 1)) * childcolcount;
                        mergeinefficiency = (double)fakezerosinnewsupernode / (double)k;
                        if (colcount + childcolcount == 2 && fakezerosinnewsupernode <= smallfakestolerance)
                        {
                            createsupernode = true;
                        }
                        if ((double)(mergeinefficiency) <= (double)(maxmergeinefficiency))
                        {
                            createsupernode = true;
                        }
                    }
                }

                //
                // Create supernode if needed
                //
                if (createsupernode)
                {

                    //
                    // Create supernode from nodes NSuper-2 and NSuper-1.
                    // Because these nodes are in the child-parent relation, we can simply
                    // copy nonzero pattern from NSuper-1.
                    //
                    ap.assert(tparentnodeofsupernode[nsuper - 2] == columnidx, "CreateSupernodalStructure: integrity check 9472 failed");
                    i0 = analysis.superrowridx[nsuper - 1];
                    ii = analysis.superrowridx[nsuper] - analysis.superrowridx[nsuper - 1];
                    rfirst = analysis.superrowridx[nsuper - 2];
                    rlast = rfirst + ii;
                    for (i = 0; i <= ii - 1; i++)
                    {
                        analysis.superrowidx[rfirst + i] = analysis.superrowidx[i0 + i];
                    }
                    analysis.supercolrange[nsuper - 1] = columnidx + 1;
                    analysis.superrowridx[nsuper - 1] = rlast;
                    node2supernode[columnidx] = nsuper - 2;
                    tfakenonzeros[nsuper - 2] = fakezerosinnewsupernode;
                    tparentnodeofsupernode[nsuper - 2] = parent[columnidx];
                    nsuper = nsuper - 1;

                    //
                    // Trace
                    //
                    if (analysis.dotracesupernodalstructure)
                    {
                        ap.trace(System.String.Format("> merged with supernode S{0,0:d}", nsuper - 1));
                        if ((double)(mergeinefficiency) != (double)(0))
                        {
                            ap.trace(System.String.Format(" ({0,2:F0}% inefficiency)", mergeinefficiency * 100));
                        }
                        ap.trace("\n*\n");
                    }
                }
                else
                {

                    //
                    // Trace
                    //
                    if (analysis.dotracesupernodalstructure)
                    {
                        ap.trace(System.String.Format("> standalone node S{0,0:d} created\n*\n", nsuper - 1));
                    }
                }
            }
            analysis.nsuper = nsuper;
            ap.assert(analysis.nsuper >= 1, "SPSymmAnalyze: integrity check failed (95mgd)");
            ap.assert(analysis.supercolrange[0] == 0, "SPCholFactorize: integrity check failed (f446s)");
            ap.assert(analysis.supercolrange[nsuper] == n, "SPSymmAnalyze: integrity check failed (04ut4)");
            ablasf.isetallocv(nsuper, -1, ref analysis.parentsupernode, _params);
            for (sidx = 0; sidx <= nsuper - 1; sidx++)
            {
                nodeidx = tparentnodeofsupernode[sidx];
                if (nodeidx >= 0)
                {
                    nodeidx = node2supernode[nodeidx];
                    analysis.parentsupernode[sidx] = nodeidx;
                }
            }
            ablasf.iallocv(nsuper + 2, ref analysis.childsupernodesridx, _params);
            ablasf.iallocv(nsuper + 1, ref analysis.childsupernodesidx, _params);
            fromparenttochildren(analysis.parentsupernode, nsuper, analysis.childsupernodesridx, analysis.childsupernodesidx, ttmp0, _params);
            i = analysis.childsupernodesridx[nsuper];
            for (sidx = 0; sidx <= nsuper - 1; sidx++)
            {
                j = analysis.parentsupernode[sidx];
                if (j < 0)
                {
                    analysis.childsupernodesidx[i] = sidx;
                    i = i + 1;
                }
            }
            ap.assert(i == nsuper, "SPSymmAnalyze: integrity check 4dr5 failed");
            analysis.childsupernodesridx[nsuper + 1] = i;

            //
            // Allocate supernodal storage
            //
            apserv.ivectorsetlengthatleast(ref analysis.rowoffsets, analysis.nsuper + 1, _params);
            apserv.ivectorsetlengthatleast(ref analysis.rowstrides, analysis.nsuper, _params);
            analysis.rowoffsets[0] = 0;
            for (i = 0; i <= analysis.nsuper - 1; i++)
            {
                blocksize = analysis.supercolrange[i + 1] - analysis.supercolrange[i];
                analysis.rowstrides[i] = recommendedstridefor(blocksize, _params);
                analysis.rowoffsets[i + 1] = analysis.rowoffsets[i];
                analysis.rowoffsets[i + 1] = analysis.rowoffsets[i + 1] + analysis.rowstrides[i] * blocksize;
                analysis.rowoffsets[i + 1] = analysis.rowoffsets[i + 1] + analysis.rowstrides[i] * (analysis.superrowridx[i + 1] - analysis.superrowridx[i]);
                analysis.rowoffsets[i + 1] = alignpositioninarray(analysis.rowoffsets[i + 1], _params);
            }

            //
            // Analyze output structure
            //
            ablasf.isetallocv(n, 0, ref analysis.outrowcounts, _params);
            for (sidx = 0; sidx <= nsuper - 1; sidx++)
            {
                cols0 = analysis.supercolrange[sidx];
                cols1 = analysis.supercolrange[sidx + 1];
                rfirst = analysis.superrowridx[sidx];
                rlast = analysis.superrowridx[sidx + 1];
                blocksize = cols1 - cols0;
                for (j = cols0; j <= cols1 - 1; j++)
                {
                    analysis.outrowcounts[j] = analysis.outrowcounts[j] + (j - cols0 + 1);
                }
                for (ii = rfirst; ii <= rlast - 1; ii++)
                {
                    i0 = analysis.superrowidx[ii];
                    analysis.outrowcounts[i0] = analysis.outrowcounts[i0] + blocksize;
                }
            }
        }


        /*************************************************************************
        This function analyzes supernodal  structure  and  precomputes  dependency
        matrix LAdj+

        INPUT PARAMETERS
            Analysis    -   analysis object with completely initialized supernodal
                            structure
            RawA        -   original (before reordering) input matrix
            Node2Supernode- mapping from node to supernode indexes
            N           -   problem size

            tTmp0,
            tTmp1,
            tFlagArray  -   temporary arrays, length at least N+1, simply provide
                            preallocated place.

        OUTPUT PARAMETERS
            Analysis    -   Analysis.LAdj is initialized
            Node2Supernode- array[N] that maps node indexes to supernode indexes

          -- ALGLIB PROJECT --
             Copyright 05.10.2020 by Bochkanov Sergey.
        *************************************************************************/
        private static void analyzesupernodaldependencies(spcholanalysis analysis,
            sparse.sparsematrix rawa,
            int[] node2supernode,
            int n,
            int[] ttmp0,
            int[] ttmp1,
            bool[] tflagarray,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            int rowidx = 0;
            int j0 = 0;
            int j1 = 0;
            int jj = 0;
            int rfirst = 0;
            int rlast = 0;
            int sidx = 0;
            int uidx = 0;
            int ladjcnt = 0;
            int dbgnzl = 0;
            int dbgrank1nodes = 0;
            int dbgrank2nodes = 0;
            int dbgrank3nodes = 0;
            int dbgrank4nodes = 0;
            int dbgbignodes = 0;
            double dbgtotalflop = 0;
            double dbgnoscatterflop = 0;
            double dbgnorowscatterflop = 0;
            double dbgnocolscatterflop = 0;
            double dbgcholeskyflop = 0;
            double dbgcholesky4flop = 0;
            double dbgrank1flop = 0;
            double dbgrank4plusflop = 0;
            double dbg444flop = 0;
            double dbgxx4flop = 0;
            double uflop = 0;
            double sflop = 0;
            int wrkrow = 0;
            int offdiagrow = 0;
            int lastrow = 0;
            int uwidth = 0;
            int uheight = 0;
            int urank = 0;
            int theight = 0;
            int twidth = 0;
            double[] dbgfastestpath = new double[0];

            ap.assert(ap.len(ttmp0) >= n + 1, "AnalyzeSupernodalDependencies: input buffer tTmp0 is too short");
            ap.assert(ap.len(ttmp1) >= n + 1, "AnalyzeSupernodalDependencies: input buffer tTmp1 is too short");
            ap.assert(ap.len(tflagarray) >= n + 1, "AnalyzeSupernodalDependencies: input buffer tTmp0 is too short");
            ap.assert(sparse.sparseiscrs(rawa, _params), "AnalyzeSupernodalDependencies: RawA must be CRS matrix");

            //
            // Determine LAdj - supernodes feeding updates to the SIdx-th one.
            //
            // Without supernodes we have: K-th row of L (also denoted as ladj+(K))
            // includes original nonzeros from A (also denoted as ladj(K)) as well
            // as all elements on paths in elimination tree from ladj(K) to K.
            //
            // With supernodes: same principle applied.
            //
            ablasf.isetallocv(analysis.nsuper, 0, ref analysis.ladj.rowbegin, _params);
            ablasf.isetallocv(analysis.nsuper, 0, ref analysis.ladj.rowend, _params);
            ablasf.rsetallocv(analysis.nsuper, 0, ref analysis.ladj.nflop, _params);
            if (analysis.dotrace)
            {
                ablasf.rsetallocv(analysis.nsuper, 0, ref dbgfastestpath, _params);
            }
            ablasf.bsetv(n, true, tflagarray, _params);
            ablasf.icopyv(analysis.nsuper, analysis.superrowridx, ttmp0, _params);
            ladjcnt = 0;
            for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
            {

                //
                // Generate ordered list of nodes feeding updates to SIdx-th one
                //
                ablasf.igrowv(ladjcnt + analysis.nsuper, ref analysis.ladj.idx, _params);
                ablasf.igrowv(ladjcnt + analysis.nsuper, ref analysis.ladj.urow0, _params);
                ablasf.igrowv(ladjcnt + analysis.nsuper, ref analysis.ladj.uwidth, _params);
                ablasf.rgrowv(ladjcnt + analysis.nsuper, ref analysis.ladj.uflop, _params);
                rfirst = ladjcnt;
                rlast = rfirst;
                analysis.ladj.rowbegin[sidx] = rfirst;
                for (rowidx = analysis.supercolrange[sidx]; rowidx <= analysis.supercolrange[sidx + 1] - 1; rowidx++)
                {
                    i = analysis.invsuperperm[rowidx];
                    j0 = rawa.ridx[i];
                    j1 = rawa.uidx[i] - 1;
                    for (jj = j0; jj <= j1; jj++)
                    {
                        j = node2supernode[analysis.superperm[rawa.idx[jj]]];

                        //
                        // add supernode and its parents up the chain
                        //
                        while ((j >= 0 && j < sidx) && tflagarray[j])
                        {
                            analysis.ladj.idx[rlast] = j;
                            tflagarray[j] = false;
                            rlast = rlast + 1;
                            j = analysis.parentsupernode[j];
                        }
                    }
                }
                tsort.sortmiddlei(analysis.ladj.idx, rfirst, rlast - rfirst, _params);

                //
                // Compute update-related information
                //
                sflop = 0;
                twidth = analysis.supercolrange[sidx + 1] - analysis.supercolrange[sidx];
                theight = twidth + (analysis.superrowridx[sidx + 1] - analysis.superrowridx[sidx]);
                for (i = rfirst; i <= rlast - 1; i++)
                {
                    j = analysis.ladj.idx[i];
                    wrkrow = ttmp0[j];
                    offdiagrow = wrkrow;
                    lastrow = analysis.superrowridx[j + 1];
                    while (offdiagrow < lastrow && analysis.superrowidx[offdiagrow] < analysis.supercolrange[sidx + 1])
                    {
                        offdiagrow = offdiagrow + 1;
                    }
                    uflop = (offdiagrow - wrkrow) * (lastrow - wrkrow) * (analysis.supercolrange[j + 1] - analysis.supercolrange[j]);
                    analysis.ladj.urow0[i] = wrkrow;
                    analysis.ladj.uwidth[i] = offdiagrow - wrkrow;
                    analysis.ladj.uflop[i] = uflop;
                    sflop = sflop + uflop;
                    ttmp0[j] = offdiagrow;
                }
                for (i = 0; i <= twidth - 1; i++)
                {
                    sflop = sflop + (theight - i) * (twidth - i);
                }
                analysis.ladj.nflop[sidx] = sflop;
                j = analysis.parentsupernode[sidx];
                if (analysis.dotrace && j >= 0)
                {
                    dbgfastestpath[j] = Math.Max(dbgfastestpath[j], sflop + dbgfastestpath[sidx]);
                }

                //
                // Finalize
                //
                for (i = rfirst; i <= rlast - 1; i++)
                {
                    tflagarray[analysis.ladj.idx[i]] = true;
                }
                analysis.ladj.rowend[sidx] = rlast;
                ladjcnt = rlast;
            }
            ablasf.rcopyallocv(analysis.nsuper, analysis.ladj.nflop, ref analysis.ladj.sflop, _params);
            for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
            {
                j = analysis.parentsupernode[sidx];
                if (j >= 0)
                {
                    analysis.ladj.sflop[j] = analysis.ladj.sflop[j] + analysis.ladj.sflop[sidx];
                }
            }
            if (analysis.dotrace)
            {
                for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
                {
                    dbgfastestpath[sidx] = dbgfastestpath[sidx] + analysis.ladj.nflop[sidx];
                }
            }

            //
            // Analyze statistics for trace output
            //
            if (analysis.dotrace)
            {
                ap.trace("=== ANALYZING SUPERNODAL DEPENDENCIES ==============================================================\n");
                dbgnzl = 0;
                dbgrank1nodes = 0;
                dbgrank2nodes = 0;
                dbgrank3nodes = 0;
                dbgrank4nodes = 0;
                dbgbignodes = 0;
                dbgtotalflop = 0;
                dbgnoscatterflop = 0;
                dbgnorowscatterflop = 0;
                dbgnocolscatterflop = 0;
                dbgrank1flop = 0;
                dbgrank4plusflop = 0;
                dbg444flop = 0;
                dbgxx4flop = 0;
                dbgcholeskyflop = 0;
                dbgcholesky4flop = 0;
                ablasf.isetv(analysis.nsuper, 0, ttmp0, _params);
                for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
                {

                    //
                    // Node sizes
                    //
                    if (analysis.supercolrange[sidx + 1] - analysis.supercolrange[sidx] == 1)
                    {
                        apserv.inc(ref dbgrank1nodes, _params);
                    }
                    if (analysis.supercolrange[sidx + 1] - analysis.supercolrange[sidx] == 2)
                    {
                        apserv.inc(ref dbgrank2nodes, _params);
                    }
                    if (analysis.supercolrange[sidx + 1] - analysis.supercolrange[sidx] == 3)
                    {
                        apserv.inc(ref dbgrank3nodes, _params);
                    }
                    if (analysis.supercolrange[sidx + 1] - analysis.supercolrange[sidx] == 4)
                    {
                        apserv.inc(ref dbgrank4nodes, _params);
                    }
                    if (analysis.supercolrange[sidx + 1] - analysis.supercolrange[sidx] > 4)
                    {
                        apserv.inc(ref dbgbignodes, _params);
                    }

                    //
                    // Nonzeros and FLOP counts
                    //
                    twidth = analysis.supercolrange[sidx + 1] - analysis.supercolrange[sidx];
                    theight = twidth + (analysis.superrowridx[sidx + 1] - analysis.superrowridx[sidx]);
                    dbgnzl = dbgnzl + theight * twidth - twidth * (twidth - 1) / 2;
                    for (i = analysis.ladj.rowbegin[sidx]; i <= analysis.ladj.rowend[sidx] - 1; i++)
                    {

                        //
                        // Determine update width, height, rank
                        //
                        uidx = analysis.ladj.idx[i];
                        uwidth = analysis.ladj.uwidth[i];
                        uheight = analysis.superrowridx[uidx + 1] - analysis.ladj.urow0[i];
                        urank = analysis.supercolrange[uidx + 1] - analysis.supercolrange[uidx];

                        //
                        // Compute update FLOP cost
                        //
                        uflop = analysis.ladj.uflop[i];
                        dbgtotalflop = dbgtotalflop + uflop;
                        if (uheight == theight && uwidth == twidth)
                        {
                            dbgnoscatterflop = dbgnoscatterflop + uflop;
                        }
                        if (uheight == theight)
                        {
                            dbgnorowscatterflop = dbgnorowscatterflop + uflop;
                        }
                        if (uwidth == twidth)
                        {
                            dbgnocolscatterflop = dbgnocolscatterflop + uflop;
                        }
                        if (urank == 1)
                        {
                            dbgrank1flop = dbgrank1flop + uflop;
                        }
                        if (urank >= 4)
                        {
                            dbgrank4plusflop = dbgrank4plusflop + uflop;
                        }
                        if ((urank == 4 && uwidth == 4) && twidth == 4)
                        {
                            dbg444flop = dbg444flop + uflop;
                        }
                        if (twidth == 4)
                        {
                            dbgxx4flop = dbgxx4flop + uflop;
                        }
                    }
                    uflop = 0;
                    for (i = 0; i <= twidth - 1; i++)
                    {
                        uflop = uflop + (theight - i) * i + (theight - i);
                    }
                    dbgtotalflop = dbgtotalflop + uflop;
                    dbgcholeskyflop = dbgcholeskyflop + uflop;
                    if (twidth == 4)
                    {
                        dbgcholesky4flop = dbgcholesky4flop + uflop;
                    }
                }

                //
                // Output
                //
                ap.trace("> factor size:\n");
                ap.trace(System.String.Format("nz(L)        = {0,6:d}\n", dbgnzl));
                ap.trace("> node size statistics:\n");
                ap.trace(System.String.Format("rank1        = {0,6:d}\n", dbgrank1nodes));
                ap.trace(System.String.Format("rank2        = {0,6:d}\n", dbgrank2nodes));
                ap.trace(System.String.Format("rank3        = {0,6:d}\n", dbgrank3nodes));
                ap.trace(System.String.Format("rank4        = {0,6:d}\n", dbgrank4nodes));
                ap.trace(System.String.Format("big nodes    = {0,6:d}\n", dbgbignodes));
                ap.trace("> Total FLOP count (fused multiply-adds):\n");
                ap.trace(System.String.Format("total        = {0,9:F1} MFLOP\n", 1.0E-6 * dbgtotalflop));
                ap.trace("> Analyzing potential parallelism speed-up (assuming infinite parallel resources):\n");
                ap.trace(System.String.Format("etree        = {0,4:F1}x (elimination tree parallelism, no internal parallelism)\n", dbgtotalflop / ablasf.rmaxv(analysis.nsuper, dbgfastestpath, _params)));
                ap.trace("> FLOP counts for updates:\n");
                ap.trace(System.String.Format("no-sctr      = {0,9:F1} MFLOP    (no row scatter, no col scatter, best case)\n", 1.0E-6 * dbgnoscatterflop));
                ap.trace(System.String.Format("M4*44->N4    = {0,9:F1} MFLOP    (no col scatter, big blocks, good case)\n", 1.0E-6 * dbg444flop));
                ap.trace(System.String.Format("no-row-sctr  = {0,9:F1} MFLOP    (no row scatter, good case for col-wise storage)\n", 1.0E-6 * dbgnorowscatterflop));
                ap.trace(System.String.Format("no-col-sctr  = {0,9:F1} MFLOP    (no col scatter, good case for row-wise storage)\n", 1.0E-6 * dbgnocolscatterflop));
                ap.trace(System.String.Format("XX*XX->N4    = {0,9:F1} MFLOP\n", 1.0E-6 * dbgxx4flop));
                ap.trace(System.String.Format("rank1        = {0,9:F1} MFLOP\n", 1.0E-6 * dbgrank1flop));
                ap.trace(System.String.Format("rank4+       = {0,9:F1} MFLOP\n", 1.0E-6 * dbgrank4plusflop));
                ap.trace("> FLOP counts for Cholesky:\n");
                ap.trace(System.String.Format("cholesky     = {0,9:F1} MFLOP\n", 1.0E-6 * dbgcholeskyflop));
                ap.trace(System.String.Format("cholesky4    = {0,9:F1} MFLOP\n", 1.0E-6 * dbgcholesky4flop));
            }
        }


        /*************************************************************************
        This function creates block-tree structure from the supernodal elimination
        tree.

        INPUT PARAMETERS
            Analysis    -   analysis object with completely initialized supernodal
                            structure, including LAdj

        OUTPUT PARAMETERS
            Analysis    -   Analysis.BlkStruct is initialized
                            Analysis.UseParallelism is set

          -- ALGLIB PROJECT --
             Copyright 05.07.2022 by Bochkanov Sergey.
        *************************************************************************/
        private static void createblockstructure(spcholanalysis analysis,
            xparams _params)
        {
            int i = 0;
            int nheads = 0;
            int nrootbatches = 0;
            int nunprocessedheads = 0;
            int offs = 0;
            int childrenoffs = 0;
            int[] heads = new int[0];
            int[] rootbatchsizes = new int[0];
            double[] costs = new double[0];
            double minrootbatchcost = 0;
            double smallcasecost = 0;
            double curcost = 0;
            double totalflops = 0;
            double sequentialflops = 0;
            double tmpsequentialflops = 0;

            offs = 0;

            //
            // Retrieve temporary arrays from the pool
            //
            apserv.nipoolretrieve(analysis.nintegerpool, ref heads, _params);
            apserv.nipoolretrieve(analysis.nintegerpool, ref rootbatchsizes, _params);
            apserv.nrpoolretrieve(analysis.nrealpool, ref costs, _params);

            //
            // Generate list of elimination forest heads, sort them by cost decrease
            //
            nheads = 0;
            for (i = analysis.childsupernodesridx[analysis.nsuper]; i <= analysis.childsupernodesridx[analysis.nsuper + 1] - 1; i++)
            {
                heads[nheads] = analysis.childsupernodesidx[i];
                costs[nheads] = analysis.ladj.sflop[heads[nheads]];
                nheads = nheads + 1;
            }
            ap.assert(nheads >= 1, "SPChol: integrity check 4t6d failed");
            ablasf.rmulv(nheads, -1, costs, _params);
            tsort.tagsortmiddleir(ref heads, ref costs, 0, nheads, _params);
            ablasf.rmulv(nheads, -1, costs, _params);

            //
            // Determine root batches count and their sizes.
            //
            minrootbatchcost = apserv.spawnlevel(_params);
            smallcasecost = 16 * 16 * 16;
            nrootbatches = 0;
            i = nheads - 1;
            curcost = 0;
            ablasf.isetv(analysis.nsuper, 0, rootbatchsizes, _params);
            while (i >= 0)
            {
                while (i >= 0)
                {

                    //
                    // Aggregate heads until we aggregate all small heads AND total cost is greater than the spawn theshold.
                    // When the debug mode is active, we randomly stop aggregating.
                    //
                    if (rootbatchsizes[nrootbatches] != 0)
                    {
                        if ((!analysis.debugblocksupernodal && (double)(curcost) > (double)(minrootbatchcost)) && (double)(costs[i]) > (double)(smallcasecost))
                        {
                            break;
                        }
                        if (analysis.debugblocksupernodal && (double)(math.randomreal()) > (double)(0.5))
                        {
                            break;
                        }
                    }
                    rootbatchsizes[nrootbatches] = rootbatchsizes[nrootbatches] + 1;
                    curcost = curcost + costs[i];
                    i = i - 1;
                }
                nrootbatches = nrootbatches + 1;
                curcost = 0;
            }

            //
            // Output empty supernodes list
            //
            ablasf.igrowv(offs + 1, ref analysis.blkstruct, _params);
            analysis.blkstruct[offs + 0] = 0;
            offs = offs + 1;

            //
            // Allocate place for CHILDREN
            //
            ablasf.igrowv(offs + 2 + nrootbatches, ref analysis.blkstruct, _params);
            analysis.blkstruct[offs + 0] = nrootbatches;
            analysis.blkstruct[offs + 1] = 0;
            childrenoffs = offs + 2;
            offs = childrenoffs + nrootbatches;

            //
            // Output empty UPDATES
            //
            ablasf.igrowv(offs + updatesheadersize, ref analysis.blkstruct, _params);
            analysis.blkstruct[offs + 0] = 2;
            analysis.blkstruct[offs + 1] = 0;
            offs = offs + updatesheadersize;

            //
            // For each batch of heads generate its child block
            //
            if (analysis.dotracescheduler)
            {
                ap.trace("--- printing blocked scheduler log -----------------------------------------------------------------\n");
            }
            totalflops = 0;
            sequentialflops = 0;
            nunprocessedheads = nheads;
            for (i = 0; i <= nrootbatches - 1; i++)
            {
                if (analysis.dotracescheduler)
                {
                    ap.trace(System.String.Format("> processing independent submatrix component ({0,0:d} of {1,0:d})\n", i, nrootbatches));
                }
                tmpsequentialflops = 0;
                analysis.blkstruct[childrenoffs + i] = offs;
                processbatchofheadsrec(analysis, heads, nunprocessedheads - rootbatchsizes[i], rootbatchsizes[i], ref analysis.blkstruct, ref offs, ref totalflops, ref tmpsequentialflops, _params);
                sequentialflops = Math.Max(sequentialflops, tmpsequentialflops);
                nunprocessedheads = nunprocessedheads - rootbatchsizes[i];
            }
            ap.assert(nunprocessedheads == 0, "SPSymm: integrity check 2ff5 failed");

            //
            // Decide on parallelism support
            //
            analysis.useparallelism = (double)(totalflops) > (double)(apserv.smpactivationlevel(_params)) && (double)(totalflops / (sequentialflops + 1)) > (double)(apserv.minspeedup(_params));

            //
            // Trace
            //
            if (analysis.dotrace)
            {
                if (analysis.dotracescheduler)
                {
                    ap.trace("--- printing blocked elimination tree --------------------------------------------------------------\n");
                    printblockedeliminationtreerec(analysis, 0, 0, _params);
                }
                ap.trace("> printing scheduler report\n");
                ap.trace(System.String.Format("FLOP count   = {0,9:F1} MFLOP\n", 1.0E-6 * totalflops));
                ap.trace(System.String.Format("CPUs count   = {0,0:d}\n", apserv.maxconcurrency(_params)));
                ap.trace(System.String.Format("best speedup = {0,0:F1}x (assuming infinite and fine grained parallelism resources)\n", totalflops / (sequentialflops + 1)));
                if (analysis.useparallelism)
                {
                    ap.trace("parallelism  = RECOMMENDED\n");
                }
                else
                {
                    ap.trace("parallelism  = NOT RECOMMENDED\n");
                }
            }

            //
            // Recycle temporary arrays
            //
            apserv.nipoolrecycle(analysis.nintegerpool, ref heads, _params);
            apserv.nipoolrecycle(analysis.nintegerpool, ref rootbatchsizes, _params);
            apserv.nrpoolrecycle(analysis.nrealpool, ref costs, _params);
        }


        /*************************************************************************
        This function appends batch of supernodes (heads)  and  their  descendants
        to the BlkStruct[] array.

        INPUT PARAMETERS
            Analysis    -   analysis object with completely initialized supernodal
                            structure, including LAdj
            HeadsStack  -   array[NSuper] which is used as a stack. NHeads elements
                            starting from position StackBase store supernode indexes,
                            elements above them can be used and overwritten by this
                            function
            BlkStruct   -   array[Offs] or larger, elements up to Offs contain
                            previous blocks
            Offs        -   points to the end of BlkStruct[]
            TotalFLOPs  -   contains already accumulated FLOP count
            SequentialFLOPs-contains already accumulated FLOP count

        OUTPUT PARAMETERS
            BlkStruct   -   elements from the former Offs (including) to the new
                            Offs (not including) contain supernodal blocks. The
                            array can be reallocated to store new data.
            Offs        -   positioned past the end of the block structure
            TotalFLOPs  -   updated with total FLOP count
            SequentialFLOPs-updated with sequential FLOP count - ones that can't
                            be parallelized; the rest can be and will be parallelized.

          -- ALGLIB PROJECT --
             Copyright 05.07.2022 by Bochkanov Sergey.
        *************************************************************************/
        private static void processbatchofheadsrec(spcholanalysis analysis,
            int[] headsstack,
            int stackbase,
            int nheads,
            ref int[] blkstruct,
            ref int offs,
            ref double totalflops,
            ref double sequentialflops,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            int blocksize = 0;
            int childrenbase = 0;
            int cidx = 0;
            int j0 = 0;
            int j1 = 0;
            int groupscreated = 0;
            int cndbigsubproblems = 0;
            double bigsubproblemsize = 0;
            int blockitemslistoffs = 0;
            int childrenlistoffs = 0;
            int nchildren = 0;
            int updatesheaderoffs = 0;
            double tmpsequentialflops = 0;
            double sequentialblockflops = 0;
            double sequentialchildrenflops = 0;
            bool[] isfactorized = new bool[0];
            int[] rowbegin = new int[0];
            double[] nflop = new double[0];

            ablasf.igrowv(offs + 1 + analysis.nsuper + 2, ref blkstruct, _params);

            //
            // Perform breadth-first traversal of children of supernodes stored in HeadsStack[],
            // extending the list by the newly traversed nodes. When we decide that it is feasible
            // to spawn parallel subproblems we add them to HeadsStack[] starting from offset ChildrenBase.
            //
            blocksize = nheads;
            childrenbase = analysis.nsuper;
            i = stackbase;
            while (i < stackbase + blocksize)
            {
                j0 = analysis.childsupernodesridx[headsstack[i]];
                j1 = analysis.childsupernodesridx[headsstack[i] + 1] - 1;

                //
                // Quick processing for a supernode with only one child: add to the block
                //
                if (j0 == j1)
                {
                    headsstack[stackbase + blocksize] = analysis.childsupernodesidx[j0];
                    blocksize = blocksize + 1;
                    i = i + 1;
                    continue;
                }

                //
                // More than one child.
                // Count big subproblems.
                // When debug mode is activated, we randomly decide on subproblem size
                //
                cndbigsubproblems = 0;
                bigsubproblemsize = apserv.spawnlevel(_params);
                if (analysis.debugblocksupernodal && (double)(math.randomreal()) > (double)(0.5))
                {
                    bigsubproblemsize = -1;
                }
                for (j = analysis.childsupernodesridx[headsstack[i]]; j <= analysis.childsupernodesridx[headsstack[i] + 1] - 1; j++)
                {
                    if ((double)(analysis.ladj.sflop[analysis.childsupernodesidx[j]]) >= (double)(bigsubproblemsize))
                    {
                        cndbigsubproblems = cndbigsubproblems + 1;
                    }
                }

                //
                // Analyze child nodes
                //
                for (j = j0; j <= j1; j++)
                {
                    if (cndbigsubproblems >= 2 && (double)(analysis.ladj.sflop[analysis.childsupernodesidx[j]]) >= (double)(bigsubproblemsize))
                    {

                        //
                        // Supernode has more than one child that is big enough to parallelize computations.
                        // Move big childs to a separate list.
                        //
                        childrenbase = childrenbase - 1;
                        headsstack[childrenbase] = analysis.childsupernodesidx[j];
                    }
                    else
                    {

                        //
                        // Either child is too small or we have only one big child (i.e. we follow elimination tree trunk).
                        // Sequential processing, add supernode to the block.
                        //
                        headsstack[stackbase + blocksize] = analysis.childsupernodesidx[j];
                        blocksize = blocksize + 1;
                    }
                }
                i = i + 1;
            }
            ap.assert(stackbase + blocksize <= childrenbase, "SPSymm: integrity check 4fb6 failed");
            ap.assert(childrenbase <= analysis.nsuper, "SPSymm: integrity check 4fb7 failed");

            //
            // Output SUPERNODES list.
            //
            blkstruct[offs + 0] = blocksize;
            blockitemslistoffs = offs + 1;
            for (i = 0; i <= blocksize - 1; i++)
            {
                blkstruct[blockitemslistoffs + i] = headsstack[stackbase + i];
            }
            tsort.sortmiddlei(blkstruct, blockitemslistoffs, blocksize, _params);
            offs = blockitemslistoffs + blocksize;

            //
            // Output CHILDREN list, temporarily store children supernode indexes instead of their offsets in BlkStruct[]
            //
            nchildren = analysis.nsuper - childrenbase;
            childrenlistoffs = offs + 2;
            blkstruct[offs + 0] = nchildren;
            blkstruct[offs + 1] = 0;
            for (i = 0; i <= nchildren - 1; i++)
            {
                blkstruct[childrenlistoffs + i] = headsstack[childrenbase + i];
            }
            offs = offs + 2 + nchildren;

            //
            // Output UPDATES part
            //
            if (analysis.dotracescheduler)
            {
                ap.trace(System.String.Format(">> running scheduler for a block of {0,0:d} supernodes\n", blocksize));
            }
            apserv.nbpoolretrieve(analysis.nbooleanpool, ref isfactorized, _params);
            apserv.nipoolretrieve(analysis.nintegerpool, ref rowbegin, _params);
            apserv.nrpoolretrieve(analysis.nrealpool, ref nflop, _params);
            ablasf.bsetv(analysis.nsuper, false, isfactorized, _params);
            ablasf.icopyv(analysis.nsuper, analysis.ladj.rowbegin, rowbegin, _params);
            ablasf.rcopyv(analysis.nsuper, analysis.ladj.nflop, nflop, _params);
            groupscreated = 0;
            updatesheaderoffs = offs;
            ablasf.igrowv(updatesheaderoffs + updatesheadersize, ref blkstruct, _params);
            offs = updatesheaderoffs + updatesheadersize;
            sequentialblockflops = 0;
            scheduleupdatesforablockrec(analysis, rowbegin, isfactorized, nflop, ref blkstruct, blockitemslistoffs, blocksize, 0, ref offs, ref groupscreated, ref totalflops, ref sequentialblockflops, _params);
            blkstruct[updatesheaderoffs + 0] = offs - updatesheaderoffs;
            blkstruct[updatesheaderoffs + 1] = groupscreated;
            apserv.nbpoolrecycle(analysis.nbooleanpool, ref isfactorized, _params);
            apserv.nipoolrecycle(analysis.nintegerpool, ref rowbegin, _params);
            apserv.nrpoolrecycle(analysis.nrealpool, ref nflop, _params);

            //
            // Recursively process children, replace supernode indexes by their offsets in BlkStruct[]
            //
            sequentialchildrenflops = 0;
            for (i = 0; i <= nchildren - 1; i++)
            {
                cidx = blkstruct[childrenlistoffs + i];
                blkstruct[childrenlistoffs + i] = offs;
                headsstack[stackbase + blocksize] = cidx;
                tmpsequentialflops = 0;
                processbatchofheadsrec(analysis, headsstack, stackbase + blocksize, 1, ref blkstruct, ref offs, ref totalflops, ref tmpsequentialflops, _params);
                sequentialchildrenflops = Math.Max(sequentialchildrenflops, tmpsequentialflops);
            }

            //
            // Compute final sequential FLOPs
            //
            sequentialflops = sequentialblockflops + sequentialchildrenflops;
        }


        /*************************************************************************
        This function appends a set of updates for a block of supernodes stored in
        the BlkStruct[] array.

        INPUT PARAMETERS
            Analysis    -   analysis object with completely initialized supernodal
                            structure, including LAdj
            RowBegin    -   on entry contains A COPY of LAdj.RowBegin[] array.
                            Modified during scheduling, so it is important to
                            provide a copy.
            IsFactorized-   array[NSuper], on entry must be set to False.
            NFLOP       -   on entry contains A COPY of LAdj.NFLOP[] array.
                            Modified during scheduling, so it is important to
                            provide a copy.
            BlkStruct   -   array[Offs] or larger, elements up to Offs contain
                            previous blocks
            BlockItemsOffs- offset in BlkStruct[] where block items are stored
                            (items must be sorted by ascending)
            BlockSize   -   number of supernodes in the block
            Depth       -   zero on the topmost call
            Offs        -   points to the end of BlkStruct[]
            GroupsCreated-  must be set to zero on entry
            TotalFLOPs  -   contains already accumulated FLOP count
            SequentialFLOPs-contains already accumulated FLOP count

        OUTPUT PARAMETERS
            BlkStruct   -   elements from the former Offs (including) to the new
                            Offs (not including) contain update information. The
                            array can be reallocated to store new data.
            Offs        -   positioned past the end of the block structure
            GroupsCreated-  increased by count of newly created update groups
            TotalFLOPs  -   updated with total FLOP count
            SequentialFLOPs-updated with sequential FLOP count - ones that can't
                            be parallelized; the rest can be and will be parallelized.

          -- ALGLIB PROJECT --
             Copyright 05.07.2022 by Bochkanov Sergey.
        *************************************************************************/
        private static void scheduleupdatesforablockrec(spcholanalysis analysis,
            int[] rowbegin,
            bool[] isfactorized,
            double[] nflop,
            ref int[] blkstruct,
            int blockitemsoffs,
            int blocksize,
            int depth,
            ref int offs,
            ref int groupscreated,
            ref double totalflops,
            ref double sequentialflops,
            xparams _params)
        {
            int i = 0;
            int k = 0;
            int kmid = 0;
            int sidx = 0;
            int batchesheaderoffs = 0;
            int groupsheaderoffs = 0;
            int updatesissued = 0;
            int batchesissued = 0;
            bool isbasecase = new bool();
            double residualcost = 0;
            double leftcost = 0;
            double updcost = 0;
            double raw2scost = 0;
            double batchcost = 0;
            double minbatchcost = 0;
            int repsupernodesupdated = 0;


            //
            // Initial evaluation of the block - should we divide it into two
            // smaller subblocks A and B and schedule parallel update A-to-B
            // or process it as single factorization?
            //
            residualcost = 0;
            for (k = 0; k <= blocksize - 1; k++)
            {
                sidx = blkstruct[blockitemsoffs + k];
                residualcost = residualcost + nflop[sidx];
            }
            kmid = blocksize / 2;
            leftcost = 0;
            for (k = 0; k <= kmid - 1; k++)
            {
                sidx = blkstruct[blockitemsoffs + k];
                leftcost = leftcost + nflop[sidx];
            }
            while (kmid < blocksize && (double)(leftcost) < (double)(0.05 * residualcost))
            {
                sidx = blkstruct[blockitemsoffs + kmid];
                leftcost = leftcost + nflop[sidx];
                kmid = kmid + 1;
            }
            raw2scost = 0;
            for (k = kmid; k <= blocksize - 1; k++)
            {
                sidx = blkstruct[blockitemsoffs + k];
                raw2scost = raw2scost + (analysis.superrowridx[sidx + 1] - analysis.superrowridx[sidx]);
            }
            repsupernodesupdated = 0;
            updcost = 0;
            for (k = kmid; k <= blocksize - 1; k++)
            {
                sidx = blkstruct[blockitemsoffs + k];
                i = rowbegin[sidx];
                while (true)
                {
                    if (i == analysis.ladj.rowend[sidx] || analysis.ladj.idx[i] >= blkstruct[blockitemsoffs + kmid])
                    {
                        break;
                    }
                    updcost = updcost + analysis.ladj.uflop[i];
                    i = i + 1;
                }
                if (i != rowbegin[sidx])
                {
                    repsupernodesupdated = repsupernodesupdated + 1;
                }
            }

            //
            // Basecase
            //
            isbasecase = false;
            if (!analysis.debugblocksupernodal)
            {
                if (!isbasecase && blocksize < smallupdate)
                {
                    if (analysis.dotracescheduler)
                    {
                        apserv.tracespaces(depth + 2, _params);
                        ap.trace(System.String.Format("* sequential block, {0,0:d} supernodes (small size)\n", blocksize));
                    }
                    isbasecase = true;
                }
                if (!isbasecase && kmid == blocksize)
                {
                    if (analysis.dotracescheduler)
                    {
                        apserv.tracespaces(depth + 2, _params);
                        ap.trace(System.String.Format("* sequential block, {0,0:d} supernodes (unbalanced block)\n", blocksize));
                    }
                    isbasecase = true;
                }
                if (!isbasecase && (double)(residualcost) < (double)(apserv.spawnlevel(_params)))
                {
                    if (analysis.dotracescheduler)
                    {
                        apserv.tracespaces(depth + 2, _params);
                        ap.trace(System.String.Format("* sequential block, {0,0:d} supernodes (lightweight block, {1,0:F1} MFLOP)\n", blocksize, 1.0E-6 * residualcost));
                    }
                    isbasecase = true;
                }
                if (!isbasecase && (double)(updcost) < (double)(apserv.spawnlevel(_params)))
                {
                    if (analysis.dotracescheduler)
                    {
                        apserv.tracespaces(depth + 2, _params);
                        ap.trace(System.String.Format("* sequential block, {0,0:d} supernodes (lightweight update, {1,0:F1} MFLOP; block cost is {2,0:F1} MFLOP)\n", blocksize, 1.0E-6 * updcost, 1.0E-6 * residualcost));
                    }
                    isbasecase = true;
                }
                if (!isbasecase && (double)(raw2scost) > (double)(raw2sthreshold * updcost))
                {
                    if (analysis.dotracescheduler)
                    {
                        apserv.tracespaces(depth + 2, _params);
                        ap.trace(System.String.Format("* sequential block, {0,0:d} supernodes (splitting overhead too high, {1,0:F1}M vs {2,0:F1}M for an update)\n", blocksize, 1.0E-6 * raw2scost, 1.0E-6 * updcost));
                    }
                    isbasecase = true;
                }
            }
            if (analysis.debugblocksupernodal)
            {
                isbasecase = isbasecase || ((blocksize <= 1 || kmid == blocksize) || (double)(math.randomreal()) < (double)(0.5));
            }
            if (isbasecase)
            {

                //
                // Schedule sequential updates for the entire block
                //
                groupsheaderoffs = offs;
                ablasf.igrowv(groupsheaderoffs + groupheadersize, ref blkstruct, _params);
                blkstruct[groupsheaderoffs + 0] = -1;
                blkstruct[groupsheaderoffs + 1] = 1;
                offs = groupsheaderoffs + groupheadersize;
                batchesheaderoffs = offs;
                ablasf.igrowv(batchesheaderoffs + batchheadersize + blocksize * sequenceentrysize, ref blkstruct, _params);
                blkstruct[batchesheaderoffs + 0] = -1;
                blkstruct[batchesheaderoffs + 1] = -1;
                offs = batchesheaderoffs + batchheadersize;
                updatesissued = 0;
                for (k = 0; k <= blocksize - 1; k++)
                {
                    sidx = blkstruct[blockitemsoffs + k];
                    if (isfactorized[sidx])
                    {
                        continue;
                    }
                    blkstruct[offs + 0] = sidx;
                    blkstruct[offs + 1] = rowbegin[sidx];
                    blkstruct[offs + 2] = analysis.ladj.rowend[sidx];
                    rowbegin[sidx] = analysis.ladj.rowend[sidx];
                    nflop[sidx] = 0;
                    isfactorized[sidx] = true;
                    updatesissued = updatesissued + 1;
                    offs = offs + sequenceentrysize;
                }
                blkstruct[batchesheaderoffs + 0] = offs - batchesheaderoffs;
                blkstruct[batchesheaderoffs + 1] = updatesissued;
                blkstruct[groupsheaderoffs + 0] = offs - groupsheaderoffs;
                groupscreated = groupscreated + 1;
                totalflops = totalflops + residualcost;
                sequentialflops = sequentialflops + residualcost;
                return;
            }

            //
            // Separate
            //
            if (analysis.dotracescheduler)
            {
                apserv.tracespaces(depth + 2, _params);
                ap.trace(System.String.Format("* splitting {0,0:d} supernodes into {1,0:d} and {2,0:d}, update cost: {3,0:F1} MFLOP ({4,0:d} supernodes)\n", blocksize, kmid, blocksize - kmid, updcost * 1.0E-6, repsupernodesupdated));
            }
            scheduleupdatesforablockrec(analysis, rowbegin, isfactorized, nflop, ref blkstruct, blockitemsoffs, kmid, depth + 1, ref offs, ref groupscreated, ref totalflops, ref sequentialflops, _params);
            groupsheaderoffs = offs;
            ablasf.igrowv(groupsheaderoffs + groupheadersize, ref blkstruct, _params);
            blkstruct[groupsheaderoffs + 0] = -1;
            blkstruct[groupsheaderoffs + 1] = -1;
            offs = groupsheaderoffs + groupheadersize;
            batchesissued = 0;
            minbatchcost = Math.Max(1.01 * updcost / apserv.maxconcurrency(_params), apserv.spawnlevel(_params));
            if (analysis.debugblocksupernodal)
            {
                minbatchcost = math.randomreal() * updcost;
            }
            k = kmid;
            while (k < blocksize)
            {
                batchcost = 0;
                updatesissued = 0;
                batchesheaderoffs = offs;
                ablasf.igrowv(offs + batchheadersize + (blocksize - kmid) * sequenceentrysize, ref blkstruct, _params);
                blkstruct[batchesheaderoffs + 0] = -1;
                blkstruct[batchesheaderoffs + 1] = -1;
                offs = batchesheaderoffs + batchheadersize;
                while (k < blocksize && (updatesissued == 0 || (double)(batchcost) < (double)(minbatchcost)))
                {
                    sidx = blkstruct[blockitemsoffs + k];
                    if (isfactorized[sidx])
                    {
                        k = k + 1;
                        continue;
                    }
                    i = rowbegin[sidx];
                    blkstruct[offs + 0] = sidx;
                    blkstruct[offs + 1] = i;
                    while (true)
                    {
                        if (i == analysis.ladj.rowend[sidx] || analysis.ladj.idx[i] >= blkstruct[blockitemsoffs + kmid])
                        {
                            break;
                        }
                        nflop[sidx] = nflop[sidx] - analysis.ladj.uflop[i];
                        totalflops = totalflops + analysis.ladj.uflop[i];
                        batchcost = batchcost + analysis.ladj.uflop[i];
                        i = i + 1;
                    }
                    rowbegin[sidx] = i;
                    blkstruct[offs + 2] = i;
                    isfactorized[sidx] = rowbegin[sidx] == analysis.ladj.rowend[sidx];
                    if (isfactorized[sidx])
                    {
                        totalflops = totalflops + nflop[sidx];
                        batchcost = batchcost + nflop[sidx];
                        nflop[sidx] = 0;
                    }
                    offs = offs + sequenceentrysize;
                    updatesissued = updatesissued + 1;
                    k = k + 1;
                }
                blkstruct[batchesheaderoffs + 0] = offs - batchesheaderoffs;
                blkstruct[batchesheaderoffs + 1] = updatesissued;
                batchesissued = batchesissued + 1;
            }
            blkstruct[groupsheaderoffs + 0] = offs - groupsheaderoffs;
            blkstruct[groupsheaderoffs + 1] = batchesissued;
            groupscreated = groupscreated + 1;
            scheduleupdatesforablockrec(analysis, rowbegin, isfactorized, nflop, ref blkstruct, blockitemsoffs + kmid, blocksize - kmid, depth + 1, ref offs, ref groupscreated, ref totalflops, ref sequentialflops, _params);
        }


        /*************************************************************************
        This function loads matrix into the supernodal storage.

          -- ALGLIB PROJECT --
             Copyright 05.10.2020 by Bochkanov Sergey.
        *************************************************************************/
        private static void loadmatrix(spcholanalysis analysis,
            sparse.sparsematrix at,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int ii = 0;
            int i0 = 0;
            int i1 = 0;
            int n = 0;
            int cols0 = 0;
            int cols1 = 0;
            int offss = 0;
            int sstride = 0;
            int blocksize = 0;
            int sidx = 0;
            bool rowsizesmatch = new bool();

            n = analysis.n;

            //
            // Perform quick integrity checks
            //
            rowsizesmatch = true;
            for (i = 0; i <= n; i++)
            {
                rowsizesmatch = rowsizesmatch && analysis.referenceridx[i] == at.ridx[i];
            }
            ap.assert(rowsizesmatch, "LoadMatrix: sparsity patterns do not match");

            //
            // Load
            //
            ablasf.iallocv(n, ref analysis.raw2smap, _params);
            ablasf.rsetallocv(analysis.rowoffsets[analysis.nsuper], 0.0, ref analysis.inputstorage, _params);
            for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
            {
                cols0 = analysis.supercolrange[sidx];
                cols1 = analysis.supercolrange[sidx + 1];
                blocksize = cols1 - cols0;
                offss = analysis.rowoffsets[sidx];
                sstride = analysis.rowstrides[sidx];

                //
                // Load supernode #SIdx using Raw2SMap to perform quick transformation between global and local indexing.
                //
                for (i = cols0; i <= cols1 - 1; i++)
                {
                    analysis.raw2smap[i] = i - cols0;
                }
                for (k = analysis.superrowridx[sidx]; k <= analysis.superrowridx[sidx + 1] - 1; k++)
                {
                    analysis.raw2smap[analysis.superrowidx[k]] = blocksize + (k - analysis.superrowridx[sidx]);
                }
                for (j = cols0; j <= cols1 - 1; j++)
                {
                    i0 = at.ridx[j];
                    i1 = at.ridx[j + 1] - 1;
                    for (ii = i0; ii <= i1; ii++)
                    {
                        analysis.inputstorage[offss + analysis.raw2smap[at.idx[ii]] * sstride + (j - cols0)] = at.vals[ii];
                    }
                }
            }
        }


        /*************************************************************************
        This function extracts computed matrix from the supernodal storage.
        Depending on settings, a supernodal permutation can be applied to the matrix.

        INPUT PARAMETERS
            Analysis    -   analysis object with completely initialized supernodal
                            structure
            Offsets     -   offsets for supernodal storage
            Strides     -   row strides for supernodal storage
            RowStorage  -   supernodal storage
            DiagD       -   diagonal factor
            N           -   problem size

            TmpP        -   preallocated temporary array[N+1]

        OUTPUT PARAMETERS
            A           -   sparse matrix in CRS format:
                            * for PermType=0, sparse matrix in the original ordering
                              (i.e. the matrix is reordered prior to output that
                              may require considerable amount of operations due to
                              permutation being applied)
                            * for PermType=1, sparse matrix in the topological
                              ordering. The least overhead for output.
            D           -   array[N], diagonal
            P           -   output permutation in product form

          -- ALGLIB PROJECT --
             Copyright 05.10.2020 by Bochkanov Sergey.
        *************************************************************************/
        private static void extractmatrix(spcholanalysis analysis,
            int[] offsets,
            int[] strides,
            double[] rowstorage,
            double[] diagd,
            int n,
            sparse.sparsematrix a,
            ref double[] d,
            ref int[] p,
            int[] tmpp,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int sidx = 0;
            int i0 = 0;
            int ii = 0;
            int rfirst = 0;
            int rlast = 0;
            int cols0 = 0;
            int cols1 = 0;
            int blocksize = 0;
            int rowstride = 0;
            int offdiagsize = 0;
            int offssdiag = 0;

            ap.assert(ap.len(tmpp) >= n + 1, "ExtractMatrix: preallocated temporary TmpP is too short");

            //
            // Basic initialization
            //
            a.matrixtype = 1;
            a.n = n;
            a.m = n;

            //
            // Various permutation types
            //
            if (analysis.applypermutationtooutput)
            {
                ap.assert(analysis.istopologicalordering, "ExtractMatrix: critical integrity check failed (attempt to merge in nontopological permutation)");

                //
                // Output matrix is topologically permuted, so we return A=L*L' instead of A=P*L*L'*P'.
                // Somewhat inefficient because we have to apply permutation to L returned by supernodal code.
                //
                apserv.ivectorsetlengthatleast(ref a.ridx, n + 1, _params);
                apserv.ivectorsetlengthatleast(ref a.didx, n, _params);
                a.ridx[0] = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    a.ridx[i + 1] = a.ridx[i] + analysis.outrowcounts[analysis.effectiveperm[i]];
                }
                for (i = 0; i <= n - 1; i++)
                {
                    a.didx[i] = a.ridx[i];
                }
                a.ninitialized = a.ridx[n];
                apserv.rvectorsetlengthatleast(ref a.vals, a.ninitialized, _params);
                apserv.ivectorsetlengthatleast(ref a.idx, a.ninitialized, _params);
                for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
                {
                    cols0 = analysis.supercolrange[sidx];
                    cols1 = analysis.supercolrange[sidx + 1];
                    rfirst = analysis.superrowridx[sidx];
                    rlast = analysis.superrowridx[sidx + 1];
                    blocksize = cols1 - cols0;
                    offdiagsize = rlast - rfirst;
                    rowstride = strides[sidx];
                    offssdiag = offsets[sidx];
                    for (i = 0; i <= blocksize - 1; i++)
                    {
                        i0 = analysis.inveffectiveperm[cols0 + i];
                        ii = a.didx[i0];
                        for (j = 0; j <= i; j++)
                        {
                            a.idx[ii] = analysis.inveffectiveperm[cols0 + j];
                            a.vals[ii] = rowstorage[offssdiag + i * rowstride + j];
                            ii = ii + 1;
                        }
                        a.didx[i0] = ii;
                    }
                    for (k = 0; k <= offdiagsize - 1; k++)
                    {
                        i0 = analysis.inveffectiveperm[analysis.superrowidx[k + rfirst]];
                        ii = a.didx[i0];
                        for (j = 0; j <= blocksize - 1; j++)
                        {
                            a.idx[ii] = analysis.inveffectiveperm[cols0 + j];
                            a.vals[ii] = rowstorage[offssdiag + (blocksize + k) * rowstride + j];
                            ii = ii + 1;
                        }
                        a.didx[i0] = ii;
                    }
                }
                for (i = 0; i <= n - 1; i++)
                {
                    ap.assert(a.didx[i] == a.ridx[i + 1], "ExtractMatrix: integrity check failed (9473t)");
                    tsort.tagsortmiddleir(ref a.idx, ref a.vals, a.ridx[i], a.ridx[i + 1] - a.ridx[i], _params);
                    ap.assert(a.idx[a.ridx[i + 1] - 1] == i, "ExtractMatrix: integrity check failed (e4tfd)");
                }
                sparse.sparseinitduidx(a, _params);

                //
                // Prepare D[] and P[]
                //
                apserv.rvectorsetlengthatleast(ref d, n, _params);
                apserv.ivectorsetlengthatleast(ref p, n, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    d[i] = diagd[analysis.effectiveperm[i]];
                    p[i] = i;
                }
            }
            else
            {

                //
                // The permutation is NOT applied to L prior to extraction,
                // we return both L and P: A=P*L*L'*P'.
                //
                apserv.ivectorsetlengthatleast(ref a.ridx, n + 1, _params);
                apserv.ivectorsetlengthatleast(ref a.didx, n, _params);
                a.ridx[0] = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    a.ridx[i + 1] = a.ridx[i] + analysis.outrowcounts[i];
                }
                for (i = 0; i <= n - 1; i++)
                {
                    a.didx[i] = a.ridx[i];
                }
                a.ninitialized = a.ridx[n];
                apserv.rvectorsetlengthatleast(ref a.vals, a.ninitialized, _params);
                apserv.ivectorsetlengthatleast(ref a.idx, a.ninitialized, _params);
                for (sidx = 0; sidx <= analysis.nsuper - 1; sidx++)
                {
                    cols0 = analysis.supercolrange[sidx];
                    cols1 = analysis.supercolrange[sidx + 1];
                    rfirst = analysis.superrowridx[sidx];
                    rlast = analysis.superrowridx[sidx + 1];
                    blocksize = cols1 - cols0;
                    offdiagsize = rlast - rfirst;
                    rowstride = strides[sidx];
                    offssdiag = offsets[sidx];
                    for (i = 0; i <= blocksize - 1; i++)
                    {
                        i0 = cols0 + i;
                        ii = a.didx[i0];
                        for (j = 0; j <= i; j++)
                        {
                            a.idx[ii] = cols0 + j;
                            a.vals[ii] = rowstorage[offssdiag + i * rowstride + j];
                            ii = ii + 1;
                        }
                        a.didx[i0] = ii;
                    }
                    for (k = 0; k <= offdiagsize - 1; k++)
                    {
                        i0 = analysis.superrowidx[k + rfirst];
                        ii = a.didx[i0];
                        for (j = 0; j <= blocksize - 1; j++)
                        {
                            a.idx[ii] = cols0 + j;
                            a.vals[ii] = rowstorage[offssdiag + (blocksize + k) * rowstride + j];
                            ii = ii + 1;
                        }
                        a.didx[i0] = ii;
                    }
                }
                for (i = 0; i <= n - 1; i++)
                {
                    ap.assert(a.didx[i] == a.ridx[i + 1], "ExtractMatrix: integrity check failed (34e43)");
                    ap.assert(a.idx[a.ridx[i + 1] - 1] == i, "ExtractMatrix: integrity check failed (k4df5)");
                }
                sparse.sparseinitduidx(a, _params);

                //
                // Extract diagonal
                //
                apserv.rvectorsetlengthatleast(ref d, n, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    d[i] = diagd[i];
                }

                //
                // Convert permutation table into product form
                //
                apserv.ivectorsetlengthatleast(ref p, n, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    p[i] = i;
                    tmpp[i] = i;
                }
                for (i = 0; i <= n - 1; i++)
                {

                    //
                    // We need to move element K to position I.
                    // J is where K actually stored
                    //
                    k = analysis.inveffectiveperm[i];
                    j = tmpp[k];

                    //
                    // Swap elements of P[I:N-1] that is used to store current locations of elements in different way
                    //
                    i0 = p[i];
                    p[i] = p[j];
                    p[j] = i0;

                    //
                    // record pivoting of positions I and J
                    //
                    p[i] = j;
                    tmpp[i0] = j;
                }
            }
        }


        /*************************************************************************
        Sparisity pattern of partial Cholesky.

        This function splits lower triangular L into two parts: leading HEAD  cols
        and trailing TAIL*TAIL submatrix. Then it computes sparsity pattern of the
        Cholesky decomposition of the HEAD, extracts bottom TAIL*HEAD update matrix
        U and applies it to the tail:

            pattern(TAIL) += pattern(U*U')

        The pattern(TAIL) is returned. It is important that pattern(TAIL)  is  not
        the sparsity pattern of trailing Cholesky factor, it is the pattern of the
        temporary matrix that will be factorized.

        The sparsity pattern of HEAD is NOT returned.

        INPUT PARAMETERS:
            A       -   lower triangular  matrix  A whose partial sparsity pattern
                        is  needed.  Only  sparsity  structure  matters,  specific
                        element values are ignored.
            Head,Tail-  sizes of the leading/traling submatrices

            tmpParent,
            tmpChildrenR,
            cmpChildrenI
            tmp1,
            FlagArray
                    -   preallocated temporary arrays, length at least Head+Tail
            tmpBottomT,
            tmpUpdateT,
            tmpUpdate-  temporary sparsematrix instances; previously allocated
                        space will be reused.

        OUTPUT PARAMETERS:
            ATail   -   sparsity pattern of the lower triangular temporary  matrix
                        computed prior to Cholesky factorization. Matrix  elements
                        are initialized by placeholder values.

          -- ALGLIB PROJECT --
             Copyright 21.08.2021 by Bochkanov Sergey.
        *************************************************************************/
        private static void partialcholeskypattern(sparse.sparsematrix a,
            int head,
            int tail,
            sparse.sparsematrix atail,
            int[] tmpparent,
            int[] tmpchildrenr,
            int[] tmpchildreni,
            int[] tmp1,
            bool[] flagarray,
            sparse.sparsematrix tmpbottomt,
            sparse.sparsematrix tmpupdatet,
            sparse.sparsematrix tmpupdate,
            sparse.sparsematrix tmpnewtailt,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int i1 = 0;
            int ii = 0;
            int j1 = 0;
            int jj = 0;
            int kb = 0;
            int cursize = 0;
            double v = 0;

            ap.assert(a.m == head + tail, "PartialCholeskyPattern: rows(A)!=Head+Tail");
            ap.assert(a.n == head + tail, "PartialCholeskyPattern: cols(A)!=Head+Tail");
            ap.assert(ap.len(tmpparent) >= head + tail + 1, "PartialCholeskyPattern: Length(tmpParent)<Head+Tail+1");
            ap.assert(ap.len(tmpchildrenr) >= head + tail + 1, "PartialCholeskyPattern: Length(tmpChildrenR)<Head+Tail+1");
            ap.assert(ap.len(tmpchildreni) >= head + tail + 1, "PartialCholeskyPattern: Length(tmpChildrenI)<Head+Tail+1");
            ap.assert(ap.len(tmp1) >= head + tail + 1, "PartialCholeskyPattern: Length(tmp1)<Head+Tail+1");
            ap.assert(ap.len(flagarray) >= head + tail + 1, "PartialCholeskyPattern: Length(tmp1)<Head+Tail+1");
            cursize = head + tail;
            v = (double)1 / (double)cursize;

            //
            // Compute leading Head columns of the Cholesky decomposition of A.
            // These columns will be used later to update sparsity pattern of the trailing
            // Tail*Tail matrix.
            //
            // Actually, we need just bottom Tail rows of these columns whose transpose (a
            // Head*Tail matrix) is stored in the tmpBottomT matrix. In order to do so in
            // the most efficient way we analyze elimination tree of the reordered matrix.
            //
            // In addition to BOTTOM matrix B we also compute an UPDATE matrix U which does
            // not include rows with duplicating sparsity patterns (only parents in the
            // elimination tree are included). Using update matrix to compute the sparsity
            // pattern is much more efficient because we do not spend time on children columns.
            //
            // NOTE: because Cholesky decomposition deals with matrix columns, we transpose
            //       A, store it into ATail, and work with transposed matrix.
            //
            sparse.sparsecopytransposecrsbuf(a, atail, _params);
            buildunorderedetree(a, cursize, tmpparent, tmp1, _params);
            fromparenttochildren(tmpparent, cursize, tmpchildrenr, tmpchildreni, tmp1, _params);
            tmpbottomt.m = head;
            tmpbottomt.n = tail;
            ablasf.iallocv(head + 1, ref tmpbottomt.ridx, _params);
            tmpbottomt.ridx[0] = 0;
            tmpupdatet.m = head;
            tmpupdatet.n = tail;
            ablasf.iallocv(head + 1, ref tmpupdatet.ridx, _params);
            tmpupdatet.ridx[0] = 0;
            ablasf.bsetv(tail, false, flagarray, _params);
            for (j = 0; j <= head - 1; j++)
            {

                //
                // Start J-th row of the tmpBottomT
                //
                kb = tmpbottomt.ridx[j];
                ablasf.igrowv(kb + tail, ref tmpbottomt.idx, _params);
                ablasf.rgrowv(kb + tail, ref tmpbottomt.vals, _params);

                //
                // copy sparsity pattern J-th column of the reordered matrix
                //
                jj = atail.didx[j];
                j1 = atail.ridx[j + 1] - 1;
                while (jj <= j1 && atail.idx[jj] < head)
                {
                    jj = jj + 1;
                }
                while (jj <= j1)
                {
                    i = atail.idx[jj] - head;
                    tmpbottomt.idx[kb] = i;
                    tmpbottomt.vals[kb] = v;
                    flagarray[i] = true;
                    kb = kb + 1;
                    jj = jj + 1;
                }

                //
                // Fetch sparsity pattern from the immediate children in the elimination tree
                //
                for (jj = tmpchildrenr[j]; jj <= tmpchildrenr[j + 1] - 1; jj++)
                {
                    j1 = tmpchildreni[jj];
                    ii = tmpbottomt.ridx[j1];
                    i1 = tmpbottomt.ridx[j1 + 1] - 1;
                    while (ii <= i1)
                    {
                        i = tmpbottomt.idx[ii];
                        if (!flagarray[i])
                        {
                            tmpbottomt.idx[kb] = i;
                            tmpbottomt.vals[kb] = v;
                            flagarray[i] = true;
                            kb = kb + 1;
                        }
                        ii = ii + 1;
                    }
                }

                //
                // Finalize row of tmpBottomT
                //
                for (ii = tmpbottomt.ridx[j]; ii <= kb - 1; ii++)
                {
                    flagarray[tmpbottomt.idx[ii]] = false;
                }
                tmpbottomt.ridx[j + 1] = kb;

                //
                // Only columns that forward their sparsity pattern directly into the tail are added to tmpUpdateT
                //
                if (tmpparent[j] >= head)
                {

                    //
                    // J-th column of the head forwards its sparsity pattern directly into the tail, save it to tmpUpdateT
                    //
                    k = tmpupdatet.ridx[j];
                    ablasf.igrowv(k + tail, ref tmpupdatet.idx, _params);
                    ablasf.rgrowv(k + tail, ref tmpupdatet.vals, _params);
                    jj = tmpbottomt.ridx[j];
                    j1 = tmpbottomt.ridx[j + 1] - 1;
                    while (jj <= j1)
                    {
                        i = tmpbottomt.idx[jj];
                        tmpupdatet.idx[k] = i;
                        tmpupdatet.vals[k] = v;
                        k = k + 1;
                        jj = jj + 1;
                    }
                    tmpupdatet.ridx[j + 1] = k;
                }
                else
                {

                    //
                    // J-th column of the head forwards its sparsity pattern to another column in the head,
                    // no need to save it to tmpUpdateT. Save empty row.
                    //
                    k = tmpupdatet.ridx[j];
                    tmpupdatet.ridx[j + 1] = k;
                }
            }
            sparse.sparsecreatecrsinplace(tmpupdatet, _params);
            sparse.sparsecopytransposecrsbuf(tmpupdatet, tmpupdate, _params);

            //
            // Apply update U*U' to the trailing Tail*Tail matrix and generate new
            // residual matrix in tmpNewTailT. Then transpose/copy it to TmpA[].
            //
            ablasf.bsetv(tail, false, flagarray, _params);
            tmpnewtailt.m = tail;
            tmpnewtailt.n = tail;
            ablasf.iallocv(tail + 1, ref tmpnewtailt.ridx, _params);
            tmpnewtailt.ridx[0] = 0;
            for (j = 0; j <= tail - 1; j++)
            {
                k = tmpnewtailt.ridx[j];
                ablasf.igrowv(k + tail, ref tmpnewtailt.idx, _params);
                ablasf.rgrowv(k + tail, ref tmpnewtailt.vals, _params);

                //
                // Copy row from the reordered/transposed matrix stored in TmpA
                //
                tmpnewtailt.idx[k] = j;
                tmpnewtailt.vals[k] = 1;
                flagarray[j] = true;
                k = k + 1;
                jj = atail.didx[head + j] + 1;
                j1 = atail.ridx[head + j + 1] - 1;
                while (jj <= j1)
                {
                    i = atail.idx[jj] - head;
                    tmpnewtailt.idx[k] = i;
                    tmpnewtailt.vals[k] = v;
                    flagarray[i] = true;
                    k = k + 1;
                    jj = jj + 1;
                }

                //
                // Apply update U*U' to J-th column of new tail (J-th row of tmpNewTailT):
                // * scan J-th row of U
                // * for each nonzero element, append corresponding row of U' (elements from J+1-th) to tmpNewTailT
                // * FlagArray[] is used to avoid duplication of nonzero elements
                //
                jj = tmpupdate.ridx[j];
                j1 = tmpupdate.ridx[j + 1] - 1;
                while (jj <= j1)
                {

                    //
                    // Get row of U', skip leading elements up to J-th
                    //
                    ii = tmpupdatet.ridx[tmpupdate.idx[jj]];
                    i1 = tmpupdatet.ridx[tmpupdate.idx[jj] + 1] - 1;
                    while (ii <= i1 && tmpupdatet.idx[ii] <= j)
                    {
                        ii = ii + 1;
                    }

                    //
                    // Append the rest of the row to tmpNewTailT
                    //
                    while (ii <= i1)
                    {
                        i = tmpupdatet.idx[ii];
                        if (!flagarray[i])
                        {
                            tmpnewtailt.idx[k] = i;
                            tmpnewtailt.vals[k] = v;
                            flagarray[i] = true;
                            k = k + 1;
                        }
                        ii = ii + 1;
                    }

                    //
                    // Continue or stop early (if we completely filled output buffer)
                    //
                    if (k - tmpnewtailt.ridx[j] == tail - j)
                    {
                        break;
                    }
                    jj = jj + 1;
                }

                //
                // Finalize:
                // * clean up FlagArray[]
                // * save K to RIdx[]
                //
                for (ii = tmpnewtailt.ridx[j]; ii <= k - 1; ii++)
                {
                    flagarray[tmpnewtailt.idx[ii]] = false;
                }
                tmpnewtailt.ridx[j + 1] = k;
            }
            sparse.sparsecreatecrsinplace(tmpnewtailt, _params);
            sparse.sparsecopytransposecrsbuf(tmpnewtailt, atail, _params);
        }


        /*************************************************************************
        This function is a specialized version of SparseSymmPermTbl()  that  takes
        into   account specifics of topological reorderings (improves performance)
        and additionally transposes its output.

        INPUT PARAMETERS
            A           -   sparse lower triangular matrix in CRS format.
            P           -   array[N] which stores permutation table;  P[I]=J means
                            that I-th row/column of matrix  A  is  moved  to  J-th
                            position. For performance reasons we do NOT check that
                            P[] is  a   correct   permutation  (that there  is  no
                            repetitions, just that all its elements  are  in [0,N)
                            range.
            B           -   sparse matrix object that will hold output.
                            Previously allocated memory will be reused as much  as
                            possible.

        OUTPUT PARAMETERS
            B           -   permuted and transposed upper triangular matrix in the
                            special internal CRS-like matrix format (MatrixType=-10082).

          -- ALGLIB PROJECT --
             Copyright 05.10.2020 by Bochkanov Sergey.
        *************************************************************************/
        private static void topologicalpermutation(sparse.sparsematrix a,
            int[] p,
            sparse.sparsematrix b,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            int jj = 0;
            int j0 = 0;
            int j1 = 0;
            int k = 0;
            int k0 = 0;
            int n = 0;
            bool bflag = new bool();

            ap.assert(a.matrixtype == 1, "TopologicalPermutation: incorrect matrix type (convert your matrix to CRS)");
            ap.assert(ap.len(p) >= a.n, "TopologicalPermutation: Length(P)<N");
            ap.assert(a.m == a.n, "TopologicalPermutation: matrix is non-square");
            ap.assert(a.ninitialized == a.ridx[a.n], "TopologicalPermutation: integrity check failed");
            bflag = true;
            n = a.n;
            for (i = 0; i <= n - 1; i++)
            {
                j = p[i];
                bflag = (bflag && j >= 0) && j < n;
            }
            ap.assert(bflag, "TopologicalPermutation: P[] contains values outside of [0,N) range");

            //
            // Prepare output
            //
            b.matrixtype = -10082;
            b.n = n;
            b.m = n;
            apserv.ivectorsetlengthatleast(ref b.didx, n, _params);
            apserv.ivectorsetlengthatleast(ref b.uidx, n, _params);

            //
            // Determine row sizes (temporary stored in DIdx) and ranges
            //
            ablasf.isetv(n, 0, b.uidx, _params);
            for (i = 0; i <= n - 1; i++)
            {
                j0 = a.ridx[i];
                j1 = a.uidx[i] - 1;
                for (jj = j0; jj <= j1; jj++)
                {
                    j = a.idx[jj];
                    b.uidx[j] = b.uidx[j] + 1;
                }
            }
            for (i = 0; i <= n - 1; i++)
            {
                b.didx[p[i]] = b.uidx[i];
            }
            apserv.ivectorsetlengthatleast(ref b.ridx, n + 1, _params);
            b.ridx[0] = 0;
            for (i = 0; i <= n - 1; i++)
            {
                b.ridx[i + 1] = b.ridx[i] + b.didx[i];
                b.uidx[i] = b.ridx[i];
            }
            b.ninitialized = b.ridx[n];
            apserv.ivectorsetlengthatleast(ref b.idx, b.ninitialized, _params);
            apserv.rvectorsetlengthatleast(ref b.vals, b.ninitialized, _params);

            //
            // Process matrix
            //
            for (i = 0; i <= n - 1; i++)
            {
                j0 = a.ridx[i];
                j1 = a.uidx[i] - 1;
                k = p[i];
                for (jj = j0; jj <= j1; jj++)
                {
                    j = p[a.idx[jj]];
                    k0 = b.uidx[j];
                    b.idx[k0] = k;
                    b.vals[k0] = a.vals[jj];
                    b.uidx[j] = k0 + 1;
                }
            }
        }


        /*************************************************************************
        Determine nonzero pattern of the column.

        This function takes as input:
        * A^T - transpose of original input matrix
        * index of column of L being computed
        * SuperRowRIdx[] and SuperRowIdx[] - arrays that store row structure of
          supernodes, and NSuper - supernodes count
        * ChildrenNodesR[], ChildrenNodesI[] - arrays that store children nodes
          for each node
        * Node2Supernode[] - array that maps node indexes to supernodes
        * TrueArray[] - array[N] that has all of its elements set to True (this
          invariant is preserved on output)
        * Tmp0[] - array[N], temporary array

        As output, it constructs nonzero pattern (diagonal element  not  included)
        of  the  column #ColumnIdx on top  of  SuperRowIdx[]  array,  starting  at
        location    SuperRowIdx[SuperRowRIdx[NSuper]]     and    till     location
        SuperRowIdx[Result-1], where Result is a function result.

        The SuperRowIdx[] array is automatically resized as needed.

        It is important that this function computes nonzero pattern, but  it  does
        NOT change other supernodal structures. The caller still has  to  finalize
        the column (setup supernode ranges, mappings, etc).

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        private static int computenonzeropattern(sparse.sparsematrix wrkat,
            int columnidx,
            int n,
            int[] superrowridx,
            ref int[] superrowidx,
            int nsuper,
            int[] childrennodesr,
            int[] childrennodesi,
            int[] node2supernode,
            bool[] truearray,
            int[] tmp0,
            xparams _params)
        {
            int result = 0;
            int i = 0;
            int ii = 0;
            int jj = 0;
            int i0 = 0;
            int i1 = 0;
            int j0 = 0;
            int j1 = 0;
            int cidx = 0;
            int rfirst = 0;
            int rlast = 0;
            int tfirst = 0;
            int tlast = 0;
            int supernodalchildrencount = 0;

            ap.assert(ap.len(truearray) >= n, "ComputeNonzeroPattern: input temporary is too short");
            ap.assert(ap.len(tmp0) >= n, "ComputeNonzeroPattern: input temporary is too short");

            //
            // Determine supernodal children in Tmp0
            //
            supernodalchildrencount = 0;
            i0 = childrennodesr[columnidx];
            i1 = childrennodesr[columnidx + 1] - 1;
            for (ii = i0; ii <= i1; ii++)
            {
                i = node2supernode[childrennodesi[ii]];
                if (truearray[i])
                {
                    tmp0[supernodalchildrencount] = i;
                    truearray[i] = false;
                    supernodalchildrencount = supernodalchildrencount + 1;
                }
            }
            for (i = 0; i <= supernodalchildrencount - 1; i++)
            {
                truearray[tmp0[i]] = true;
            }

            //
            // Initialized column by nonzero pattern from A
            //
            rfirst = superrowridx[nsuper];
            tfirst = rfirst + n;
            ablasf.igrowv(rfirst + 2 * n, ref superrowidx, _params);
            i0 = wrkat.ridx[columnidx] + 1;
            i1 = wrkat.ridx[columnidx + 1];
            ablasf.icopyvx(i1 - i0, wrkat.idx, i0, superrowidx, rfirst, _params);
            rlast = rfirst + (i1 - i0);

            //
            // For column with small number of children use ordered merge algorithm.
            // For column with many children it is better to perform unsorted merge,
            // and then sort the sequence.
            //
            if (supernodalchildrencount <= 4)
            {

                //
                // Ordered merge. The best approach for small number of children,
                // but may have O(N^2) running time when O(N) children are present.
                //
                for (cidx = 0; cidx <= supernodalchildrencount - 1; cidx++)
                {

                    //
                    // Skip initial elements that do not contribute to subdiagonal nonzero pattern
                    //
                    i0 = superrowridx[tmp0[cidx]];
                    i1 = superrowridx[tmp0[cidx] + 1] - 1;
                    while (i0 <= i1 && superrowidx[i0] <= columnidx)
                    {
                        i0 = i0 + 1;
                    }
                    j0 = rfirst;
                    j1 = rlast - 1;

                    //
                    // Handle degenerate cases: empty merge target or empty merge source.
                    //
                    if (j1 < j0)
                    {
                        ablasf.icopyvx(i1 - i0 + 1, superrowidx, i0, superrowidx, rlast, _params);
                        rlast = rlast + (i1 - i0 + 1);
                        continue;
                    }
                    if (i1 < i0)
                    {
                        continue;
                    }

                    //
                    // General case: two non-empty sorted sequences given by [I0,I1] and [J0,J1],
                    // have to be merged and stored into [RFirst,RLast).
                    //
                    ii = superrowidx[i0];
                    jj = superrowidx[j0];
                    tlast = tfirst;
                    while (true)
                    {
                        if (ii < jj)
                        {
                            superrowidx[tlast] = ii;
                            tlast = tlast + 1;
                            i0 = i0 + 1;
                            if (i0 > i1)
                            {
                                break;
                            }
                            ii = superrowidx[i0];
                        }
                        if (jj < ii)
                        {
                            superrowidx[tlast] = jj;
                            tlast = tlast + 1;
                            j0 = j0 + 1;
                            if (j0 > j1)
                            {
                                break;
                            }
                            jj = superrowidx[j0];
                        }
                        if (jj == ii)
                        {
                            superrowidx[tlast] = ii;
                            tlast = tlast + 1;
                            i0 = i0 + 1;
                            j0 = j0 + 1;
                            if (i0 > i1)
                            {
                                break;
                            }
                            if (j0 > j1)
                            {
                                break;
                            }
                            ii = superrowidx[i0];
                            jj = superrowidx[j0];
                        }
                    }
                    for (ii = i0; ii <= i1; ii++)
                    {
                        superrowidx[tlast] = superrowidx[ii];
                        tlast = tlast + 1;
                    }
                    for (jj = j0; jj <= j1; jj++)
                    {
                        superrowidx[tlast] = superrowidx[jj];
                        tlast = tlast + 1;
                    }
                    ablasf.icopyvx(tlast - tfirst, superrowidx, tfirst, superrowidx, rfirst, _params);
                    rlast = rfirst + (tlast - tfirst);
                }
                result = rlast;
            }
            else
            {

                //
                // Unordered merge followed by sort. Guaranteed N*logN worst case.
                //
                for (ii = rfirst; ii <= rlast - 1; ii++)
                {
                    truearray[superrowidx[ii]] = false;
                }
                for (cidx = 0; cidx <= supernodalchildrencount - 1; cidx++)
                {

                    //
                    // Skip initial elements that do not contribute to subdiagonal nonzero pattern
                    //
                    i0 = superrowridx[tmp0[cidx]];
                    i1 = superrowridx[tmp0[cidx] + 1] - 1;
                    while (i0 <= i1 && superrowidx[i0] <= columnidx)
                    {
                        i0 = i0 + 1;
                    }

                    //
                    // Append elements not present in the sequence
                    //
                    for (ii = i0; ii <= i1; ii++)
                    {
                        i = superrowidx[ii];
                        if (truearray[i])
                        {
                            superrowidx[rlast] = i;
                            rlast = rlast + 1;
                            truearray[i] = false;
                        }
                    }
                }
                for (ii = rfirst; ii <= rlast - 1; ii++)
                {
                    truearray[superrowidx[ii]] = true;
                }
                tsort.tagsortmiddlei(ref superrowidx, rfirst, rlast - rfirst, _params);
                result = rlast;
            }
            return result;
        }


        /*************************************************************************
        Update target supernode with data from its children. This operation  is  a
        supernodal equivalent of the column update by  all  preceding  cols  in  a
        left-looking Cholesky.

        This function applies LAdjIdx1-LAdjIdx0 updates, from LAdjidx0 to LAdjIdx1-1
        from child columns. Each update has following form:

            S := S - scatter(U*D*Uc')

        where
        * S is an tHeight*tWidth rectangular target matrix that is:
          * stored with tStride>=tWidth in RowStorage[OffsS:OffsS+tHeight*tStride-1]
          * lower trapezoidal i.e. its leading tWidth*tWidth  submatrix  is  lower
            triangular. One may update either entire  tWidth*tWidth  submatrix  or
            just its lower part, because upper triangle is not referenced anyway.
          * the height of S is not given because it is not actually needed
        * U is an uHeight*uRank rectangular update matrix tht is:
          * stored with row stride uStride>=uRank in RowStorage[OffsU:OffsU+uHeight*uStride-1].
        * Uc is the leading uWidth*uRank submatrix of U
        * D is uRank*uRank diagonal matrix that is:
          * stored in DiagD[OffsD:OffsD+uRank-1]
          * unit, when Analysis.UnitD=True. In this case it can be ignored, although
            DiagD still contains 1's in all of its entries
        * uHeight<=tHeight, uWidth<=tWidth, so scatter operation is needed to update
          S with smaller update.
        * scatter() is an operation  that  extends  smaller  uHeight*uWidth update
          matrix U*Uc' into larger tHeight*tWidth target matrix by adding zero rows
          and columns into U*Uc':
          * I-th row of update modifies Raw2SMap[SuperRowIdx[URBase+I]]-th row  of
            the matrix S
          * J-th column of update modifies Raw2SMap[SuperRowIdx[URBase+J]]-th  col
            of the matrix S

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        private static void updatesupernode(spcholanalysis analysis,
            int sidx,
            int cols0,
            int cols1,
            int offss,
            int[] raw2smap,
            int ladjidx0,
            int ladjidx1,
            double[] diagd,
            xparams _params)
        {
            int i = 0;
            int uidx = 0;
            int colu0 = 0;
            int colu1 = 0;
            int urbase = 0;
            int urlast = 0;
            int urank = 0;
            int uwidth = 0;
            int uheight = 0;
            int urowstride = 0;
            int twidth = 0;
            int theight = 0;
            int trowstride = 0;
            int offsu = 0;
            int wrkrow = 0;
            int ladjidx = 0;

            twidth = cols1 - cols0;
            theight = twidth + (analysis.superrowridx[sidx + 1] - analysis.superrowridx[sidx]);
            trowstride = analysis.rowstrides[sidx];
            for (ladjidx = ladjidx0; ladjidx <= ladjidx1 - 1; ladjidx++)
            {
                uidx = analysis.ladj.idx[ladjidx];
                offsu = analysis.rowoffsets[uidx];
                colu0 = analysis.supercolrange[uidx];
                colu1 = analysis.supercolrange[uidx + 1];
                urbase = analysis.superrowridx[uidx];
                urlast = analysis.superrowridx[uidx + 1];
                urank = colu1 - colu0;
                urowstride = analysis.rowstrides[uidx];
                wrkrow = analysis.ladj.urow0[ladjidx];
                uwidth = analysis.ladj.uwidth[ladjidx];
                uheight = urlast - wrkrow;
                if (analysis.extendeddebug)
                {

                    //
                    // Extended integrity check (if requested)
                    //
                    ap.assert(uwidth > 0, "SPCholFactorize: integrity check failed (44trg1)");
                    ap.assert(analysis.superrowidx[wrkrow] >= cols0, "SPCholFactorize: integrity check 6378 failed");
                    ap.assert(analysis.superrowidx[wrkrow] < cols1, "SPCholFactorize: integrity check 6729 failed");
                    for (i = wrkrow; i <= urlast - 1; i++)
                    {
                        ap.assert(raw2smap[analysis.superrowidx[i]] >= 0, "SPCholFactorize: integrity check failed (43t63)");
                    }
                }

                //
                // Skip leading uRank+WrkRow rows of U because they are not used.
                //
                offsu = offsu + (urank + (wrkrow - urbase)) * urowstride;

                //
                // Handle special cases
                //
                if (trowstride == 4)
                {

                    //
                    // Target is stride-4 column, try several kernels that may work with tWidth=3 and tWidth=4
                    //
                    if (((uwidth == 4 && twidth == 4) && urank == 4) && urowstride == 4)
                    {
                        if (updatekernel4444(analysis.outputstorage, offss, theight, offsu, uheight, analysis.diagd, colu0, raw2smap, analysis.superrowidx, wrkrow, _params))
                        {
                            continue;
                        }
                    }
                    if (updatekernelabc4(analysis.outputstorage, offss, twidth, offsu, uheight, urank, urowstride, uwidth, analysis.diagd, colu0, raw2smap, analysis.superrowidx, wrkrow, _params))
                    {
                        continue;
                    }
                }
                if (urank == 1 && urowstride == 1)
                {
                    if (updatekernelrank1(analysis.outputstorage, offss, twidth, trowstride, offsu, uheight, uwidth, analysis.diagd, colu0, raw2smap, analysis.superrowidx, wrkrow, _params))
                    {
                        continue;
                    }
                }
                if (urank == 2 && urowstride == 2)
                {
                    if (updatekernelrank2(analysis.outputstorage, offss, twidth, trowstride, offsu, uheight, uwidth, analysis.diagd, colu0, raw2smap, analysis.superrowidx, wrkrow, _params))
                    {
                        continue;
                    }
                }

                //
                // Handle general update with no specialized kernel
                //
                updatesupernodegeneric(analysis, sidx, cols0, cols1, offss, raw2smap, ladjidx, diagd, _params);
            }
        }


        /*************************************************************************
        Generic supernode update kernel

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        private static void updatesupernodegeneric(spcholanalysis analysis,
            int sidx,
            int cols0,
            int cols1,
            int offss,
            int[] raw2smap,
            int ladjidx,
            double[] diagd,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int uidx = 0;
            int colu0 = 0;
            int colu1 = 0;
            int urbase = 0;
            int urlast = 0;
            int urank = 0;
            int uwidth = 0;
            int uheight = 0;
            int urowstride = 0;
            int trowstride = 0;
            int targetrow = 0;
            int targetcol = 0;
            int offsu = 0;
            int offsd = 0;
            int offs0 = 0;
            int offsj = 0;
            int offsk = 0;
            double v = 0;
            int wrkrow = 0;
            int[] u2smap = new int[0];

            uidx = analysis.ladj.idx[ladjidx];
            offsd = analysis.supercolrange[uidx];
            offsu = analysis.rowoffsets[uidx];
            colu0 = analysis.supercolrange[uidx];
            colu1 = analysis.supercolrange[uidx + 1];
            urbase = analysis.superrowridx[uidx];
            urlast = analysis.superrowridx[uidx + 1];
            urank = colu1 - colu0;
            trowstride = analysis.rowstrides[sidx];
            urowstride = analysis.rowstrides[uidx];
            wrkrow = analysis.ladj.urow0[ladjidx];
            uwidth = analysis.ladj.uwidth[ladjidx];
            uheight = urlast - wrkrow;

            //
            // Skip leading uRank+WrkRow rows of U because they are not used.
            //
            offsu = offsu + (colu1 - colu0 + (wrkrow - urbase)) * urowstride;

            //
            // Handle general update, rerefence code
            //
            apserv.nipoolretrieve(analysis.nintegerpool, ref u2smap, _params);
            apserv.ivectorsetlengthatleast(ref u2smap, uheight, _params);
            for (i = 0; i <= uheight - 1; i++)
            {
                u2smap[i] = raw2smap[analysis.superrowidx[wrkrow + i]];
            }
            if (analysis.unitd)
            {

                //
                // Unit D, vanilla Cholesky
                //
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + u2smap[k] * trowstride;
                    for (j = 0; j <= uwidth - 1; j++)
                    {
                        targetcol = u2smap[j];
                        offsj = offsu + j * urowstride;
                        offsk = offsu + k * urowstride;
                        offs0 = targetrow + targetcol;
                        v = analysis.outputstorage[offs0];
                        for (i = 0; i <= urank - 1; i++)
                        {
                            v = v - analysis.outputstorage[offsj + i] * analysis.outputstorage[offsk + i];
                        }
                        analysis.outputstorage[offs0] = v;
                    }
                }
            }
            else
            {

                //
                // Non-unit D, LDLT decomposition
                //
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + u2smap[k] * trowstride;
                    for (j = 0; j <= uwidth - 1; j++)
                    {
                        targetcol = u2smap[j];
                        offsj = offsu + j * urowstride;
                        offsk = offsu + k * urowstride;
                        offs0 = targetrow + targetcol;
                        v = analysis.outputstorage[offs0];
                        for (i = 0; i <= urank - 1; i++)
                        {
                            v = v - analysis.outputstorage[offsj + i] * diagd[offsd + i] * analysis.outputstorage[offsk + i];
                        }
                        analysis.outputstorage[offs0] = v;
                    }
                }
            }
            apserv.nipoolrecycle(analysis.nintegerpool, ref u2smap, _params);
        }


        /*************************************************************************
        Factorizes target supernode, returns True on success, False on failure.

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        private static bool factorizesupernode(spcholanalysis analysis,
            int sidx,
            xparams _params)
        {
            bool result = new bool();
            int i = 0;
            int j = 0;
            int k = 0;
            int cols0 = 0;
            int cols1 = 0;
            int offss = 0;
            int blocksize = 0;
            int offdiagsize = 0;
            int sstride = 0;
            double v = 0;
            double vs = 0;
            double possignvraw = 0;
            bool controlpivot = new bool();
            bool controloverflow = new bool();

            cols0 = analysis.supercolrange[sidx];
            cols1 = analysis.supercolrange[sidx + 1];
            offss = analysis.rowoffsets[sidx];
            blocksize = cols1 - cols0;
            offdiagsize = analysis.superrowridx[sidx + 1] - analysis.superrowridx[sidx];
            sstride = analysis.rowstrides[sidx];
            controlpivot = analysis.modtype == 1 && (double)(analysis.modparam0) > (double)(0);
            controloverflow = analysis.modtype == 1 && (double)(analysis.modparam1) > (double)(0);
            if (analysis.unitd)
            {

                //
                // Classic Cholesky
                //
                for (j = 0; j <= blocksize - 1; j++)
                {

                    //
                    // Compute J-th column
                    //
                    vs = 0;
                    for (k = j; k <= blocksize + offdiagsize - 1; k++)
                    {
                        v = analysis.outputstorage[offss + k * sstride + j];
                        for (i = 0; i <= j - 1; i++)
                        {
                            v = v - analysis.outputstorage[offss + k * sstride + i] * analysis.outputstorage[offss + j * sstride + i];
                        }
                        analysis.outputstorage[offss + k * sstride + j] = v;
                        vs = vs + Math.Abs(v);
                    }
                    if (controloverflow && vs > analysis.modparam1)
                    {

                        //
                        // Possible failure due to accumulation of numerical errors
                        //
                        result = false;
                        return result;
                    }

                    //
                    // Handle pivot element
                    //
                    v = analysis.outputstorage[offss + j * sstride + j];
                    if (controlpivot && v <= analysis.modparam0)
                    {

                        //
                        // Basic modified Cholesky
                        //
                        v = Math.Sqrt(analysis.modparam0);
                        analysis.diagd[cols0 + j] = 1.0;
                        analysis.outputstorage[offss + j * sstride + j] = v;
                        v = 1 / v;
                        for (k = j + 1; k <= blocksize + offdiagsize - 1; k++)
                        {
                            analysis.outputstorage[offss + k * sstride + j] = v * analysis.outputstorage[offss + k * sstride + j];
                        }
                    }
                    else
                    {

                        //
                        // Default case
                        //
                        if (v <= 0)
                        {
                            result = false;
                            return result;
                        }
                        analysis.diagd[cols0 + j] = 1.0;
                        v = 1 / Math.Sqrt(v);
                        for (k = j; k <= blocksize + offdiagsize - 1; k++)
                        {
                            analysis.outputstorage[offss + k * sstride + j] = v * analysis.outputstorage[offss + k * sstride + j];
                        }
                    }
                }
            }
            else
            {

                //
                // LDLT with diagonal D
                //
                for (j = 0; j <= blocksize - 1; j++)
                {

                    //
                    // Compute J-th column
                    //
                    vs = 0;
                    for (k = j; k <= blocksize + offdiagsize - 1; k++)
                    {
                        v = analysis.outputstorage[offss + k * sstride + j];
                        for (i = 0; i <= j - 1; i++)
                        {
                            v = v - analysis.outputstorage[offss + k * sstride + i] * analysis.diagd[cols0 + i] * analysis.outputstorage[offss + j * sstride + i];
                        }
                        analysis.outputstorage[offss + k * sstride + j] = v;
                        vs = vs + Math.Abs(v);
                    }
                    if (controloverflow && vs > analysis.modparam1)
                    {

                        //
                        // Possible failure due to accumulation of numerical errors
                        //
                        result = false;
                        return result;
                    }

                    //
                    // Handle pivot element
                    //
                    possignvraw = apserv.possign(analysis.inputstorage[offss + j * sstride + j], _params);
                    v = analysis.outputstorage[offss + j * sstride + j];
                    if (controlpivot && v / possignvraw <= analysis.modparam0)
                    {

                        //
                        // Basic modified LDLT
                        //
                        v = possignvraw * analysis.modparam0;
                        analysis.diagd[cols0 + j] = v;
                        analysis.outputstorage[offss + j * sstride + j] = 1.0;
                        v = 1 / v;
                        for (k = j + 1; k <= blocksize + offdiagsize - 1; k++)
                        {
                            analysis.outputstorage[offss + k * sstride + j] = v * analysis.outputstorage[offss + k * sstride + j];
                        }
                    }
                    else
                    {

                        //
                        // Unmodified LDLT
                        //
                        if (v == 0)
                        {
                            result = false;
                            return result;
                        }
                        analysis.diagd[cols0 + j] = v;
                        v = 1 / v;
                        for (k = j; k <= blocksize + offdiagsize - 1; k++)
                        {
                            analysis.outputstorage[offss + k * sstride + j] = v * analysis.outputstorage[offss + k * sstride + j];
                        }
                    }
                }
            }
            result = true;
            return result;
        }


        /*************************************************************************
        This function returns recommended stride for given row size

          -- ALGLIB routine --
             20.10.2020
             Bochkanov Sergey
        *************************************************************************/
        private static int recommendedstridefor(int rowsize,
            xparams _params)
        {
            int result = 0;

            result = rowsize;
            if (rowsize == 3)
            {
                result = 4;
            }
            return result;
        }


        /*************************************************************************
        This function aligns position in array in order to  better  accommodate to
        SIMD specifics.

        NOTE: this function aligns position measured in double precision  numbers,
              not in bits or bytes. If you want to have 256-bit aligned  position,
              round Offs to nearest multiple of 4 that is not less than Offs.

          -- ALGLIB routine --
             20.10.2020
             Bochkanov Sergey
        *************************************************************************/
        private static int alignpositioninarray(int offs,
            xparams _params)
        {
            int result = 0;

            result = offs;
            if (offs % 4 != 0)
            {
                result = result + (4 - offs % 4);
            }
            return result;
        }


#if ALGLIB_NO_FAST_KERNELS
    /*************************************************************************
    Fast kernels for small supernodal updates: special 4x4x4x4 function.

    ! See comments on UpdateSupernode() for information  on generic supernodal
    ! updates, including notation used below.

    The generic update has following form:

        S := S - scatter(U*D*Uc')

    This specialized function performs 4x4x4x4 update, i.e.:
    * S is a tHeight*4 matrix
    * U is a uHeight*4 matrix
    * Uc' is a 4*4 matrix
    * scatter() scatters rows of U*Uc', but does not scatter columns (they are
      densely packed).
      
    Return value:
    * True if update was applied
    * False if kernel refused to perform an update.

      -- ALGLIB routine --
         20.09.2020
         Bochkanov Sergey
    *************************************************************************/
    private static bool updatekernel4444(double[] rowstorage,
        int offss,
        int sheight,
        int offsu,
        int uheight,
        double[] diagd,
        int offsd,
        int[] raw2smap,
        int[] superrowidx,
        int urbase,
        xparams _params)
    {
        bool result = new bool();
        int k = 0;
        int targetrow = 0;
        int offsk = 0;
        double d0 = 0;
        double d1 = 0;
        double d2 = 0;
        double d3 = 0;
        double u00 = 0;
        double u01 = 0;
        double u02 = 0;
        double u03 = 0;
        double u10 = 0;
        double u11 = 0;
        double u12 = 0;
        double u13 = 0;
        double u20 = 0;
        double u21 = 0;
        double u22 = 0;
        double u23 = 0;
        double u30 = 0;
        double u31 = 0;
        double u32 = 0;
        double u33 = 0;
        double uk0 = 0;
        double uk1 = 0;
        double uk2 = 0;
        double uk3 = 0;

        d0 = diagd[offsd+0];
        d1 = diagd[offsd+1];
        d2 = diagd[offsd+2];
        d3 = diagd[offsd+3];
        u00 = d0*rowstorage[offsu+0*4+0];
        u01 = d1*rowstorage[offsu+0*4+1];
        u02 = d2*rowstorage[offsu+0*4+2];
        u03 = d3*rowstorage[offsu+0*4+3];
        u10 = d0*rowstorage[offsu+1*4+0];
        u11 = d1*rowstorage[offsu+1*4+1];
        u12 = d2*rowstorage[offsu+1*4+2];
        u13 = d3*rowstorage[offsu+1*4+3];
        u20 = d0*rowstorage[offsu+2*4+0];
        u21 = d1*rowstorage[offsu+2*4+1];
        u22 = d2*rowstorage[offsu+2*4+2];
        u23 = d3*rowstorage[offsu+2*4+3];
        u30 = d0*rowstorage[offsu+3*4+0];
        u31 = d1*rowstorage[offsu+3*4+1];
        u32 = d2*rowstorage[offsu+3*4+2];
        u33 = d3*rowstorage[offsu+3*4+3];
        for(k=0; k<=uheight-1; k++)
        {
            targetrow = offss+raw2smap[superrowidx[urbase+k]]*4;
            offsk = offsu+k*4;
            uk0 = rowstorage[offsk+0];
            uk1 = rowstorage[offsk+1];
            uk2 = rowstorage[offsk+2];
            uk3 = rowstorage[offsk+3];
            rowstorage[targetrow+0] = rowstorage[targetrow+0]-u00*uk0-u01*uk1-u02*uk2-u03*uk3;
            rowstorage[targetrow+1] = rowstorage[targetrow+1]-u10*uk0-u11*uk1-u12*uk2-u13*uk3;
            rowstorage[targetrow+2] = rowstorage[targetrow+2]-u20*uk0-u21*uk1-u22*uk2-u23*uk3;
            rowstorage[targetrow+3] = rowstorage[targetrow+3]-u30*uk0-u31*uk1-u32*uk2-u33*uk3;
        }
        result = true;
        return result;
    }
#endif


#if ALGLIB_NO_FAST_KERNELS
    /*************************************************************************
    Fast kernels for small supernodal updates: special 4x4x4x4 function.

    ! See comments on UpdateSupernode() for information  on generic supernodal
    ! updates, including notation used below.

    The generic update has following form:

        S := S - scatter(U*D*Uc')

    This specialized function performs AxBxCx4 update, i.e.:
    * S is a tHeight*A matrix with row stride equal to 4 (usually it means that
      it has 3 or 4 columns)
    * U is a uHeight*B matrix
    * Uc' is a B*C matrix, with C<=A
    * scatter() scatters rows and columns of U*Uc'
      
    Return value:
    * True if update was applied
    * False if kernel refused to perform an update (quick exit for unsupported
      combinations of input sizes)

      -- ALGLIB routine --
         20.09.2020
         Bochkanov Sergey
    *************************************************************************/
    private static bool updatekernelabc4(double[] rowstorage,
        int offss,
        int twidth,
        int offsu,
        int uheight,
        int urank,
        int urowstride,
        int uwidth,
        double[] diagd,
        int offsd,
        int[] raw2smap,
        int[] superrowidx,
        int urbase,
        xparams _params)
    {
        bool result = new bool();
        int k = 0;
        int targetrow = 0;
        int targetcol = 0;
        int offsk = 0;
        double d0 = 0;
        double d1 = 0;
        double d2 = 0;
        double d3 = 0;
        double u00 = 0;
        double u01 = 0;
        double u02 = 0;
        double u03 = 0;
        double u10 = 0;
        double u11 = 0;
        double u12 = 0;
        double u13 = 0;
        double u20 = 0;
        double u21 = 0;
        double u22 = 0;
        double u23 = 0;
        double u30 = 0;
        double u31 = 0;
        double u32 = 0;
        double u33 = 0;
        double uk0 = 0;
        double uk1 = 0;
        double uk2 = 0;
        double uk3 = 0;
        int srccol0 = 0;
        int srccol1 = 0;
        int srccol2 = 0;
        int srccol3 = 0;

        
        //
        // Filter out unsupported combinations (ones that are too sparse for the non-SIMD code)
        //
        result = false;
        if( twidth<3 || twidth>4 )
        {
            return result;
        }
        if( uwidth<3 || uwidth>4 )
        {
            return result;
        }
        if( urank>4 )
        {
            return result;
        }
        
        //
        // Determine source columns for target columns, -1 if target column
        // is not updated.
        //
        srccol0 = -1;
        srccol1 = -1;
        srccol2 = -1;
        srccol3 = -1;
        for(k=0; k<=uwidth-1; k++)
        {
            targetcol = raw2smap[superrowidx[urbase+k]];
            if( targetcol==0 )
            {
                srccol0 = k;
            }
            if( targetcol==1 )
            {
                srccol1 = k;
            }
            if( targetcol==2 )
            {
                srccol2 = k;
            }
            if( targetcol==3 )
            {
                srccol3 = k;
            }
        }
        
        //
        // Load update matrix into aligned/rearranged 4x4 storage
        //
        d0 = 0;
        d1 = 0;
        d2 = 0;
        d3 = 0;
        u00 = 0;
        u01 = 0;
        u02 = 0;
        u03 = 0;
        u10 = 0;
        u11 = 0;
        u12 = 0;
        u13 = 0;
        u20 = 0;
        u21 = 0;
        u22 = 0;
        u23 = 0;
        u30 = 0;
        u31 = 0;
        u32 = 0;
        u33 = 0;
        if( urank>=1 )
        {
            d0 = diagd[offsd+0];
        }
        if( urank>=2 )
        {
            d1 = diagd[offsd+1];
        }
        if( urank>=3 )
        {
            d2 = diagd[offsd+2];
        }
        if( urank>=4 )
        {
            d3 = diagd[offsd+3];
        }
        if( srccol0>=0 )
        {
            if( urank>=1 )
            {
                u00 = d0*rowstorage[offsu+srccol0*urowstride+0];
            }
            if( urank>=2 )
            {
                u01 = d1*rowstorage[offsu+srccol0*urowstride+1];
            }
            if( urank>=3 )
            {
                u02 = d2*rowstorage[offsu+srccol0*urowstride+2];
            }
            if( urank>=4 )
            {
                u03 = d3*rowstorage[offsu+srccol0*urowstride+3];
            }
        }
        if( srccol1>=0 )
        {
            if( urank>=1 )
            {
                u10 = d0*rowstorage[offsu+srccol1*urowstride+0];
            }
            if( urank>=2 )
            {
                u11 = d1*rowstorage[offsu+srccol1*urowstride+1];
            }
            if( urank>=3 )
            {
                u12 = d2*rowstorage[offsu+srccol1*urowstride+2];
            }
            if( urank>=4 )
            {
                u13 = d3*rowstorage[offsu+srccol1*urowstride+3];
            }
        }
        if( srccol2>=0 )
        {
            if( urank>=1 )
            {
                u20 = d0*rowstorage[offsu+srccol2*urowstride+0];
            }
            if( urank>=2 )
            {
                u21 = d1*rowstorage[offsu+srccol2*urowstride+1];
            }
            if( urank>=3 )
            {
                u22 = d2*rowstorage[offsu+srccol2*urowstride+2];
            }
            if( urank>=4 )
            {
                u23 = d3*rowstorage[offsu+srccol2*urowstride+3];
            }
        }
        if( srccol3>=0 )
        {
            if( urank>=1 )
            {
                u30 = d0*rowstorage[offsu+srccol3*urowstride+0];
            }
            if( urank>=2 )
            {
                u31 = d1*rowstorage[offsu+srccol3*urowstride+1];
            }
            if( urank>=3 )
            {
                u32 = d2*rowstorage[offsu+srccol3*urowstride+2];
            }
            if( urank>=4 )
            {
                u33 = d3*rowstorage[offsu+srccol3*urowstride+3];
            }
        }
        
        //
        // Run update
        //
        if( urank==1 )
        {
            for(k=0; k<=uheight-1; k++)
            {
                targetrow = offss+raw2smap[superrowidx[urbase+k]]*4;
                offsk = offsu+k*urowstride;
                uk0 = rowstorage[offsk+0];
                rowstorage[targetrow+0] = rowstorage[targetrow+0]-u00*uk0;
                rowstorage[targetrow+1] = rowstorage[targetrow+1]-u10*uk0;
                rowstorage[targetrow+2] = rowstorage[targetrow+2]-u20*uk0;
                rowstorage[targetrow+3] = rowstorage[targetrow+3]-u30*uk0;
            }
        }
        if( urank==2 )
        {
            for(k=0; k<=uheight-1; k++)
            {
                targetrow = offss+raw2smap[superrowidx[urbase+k]]*4;
                offsk = offsu+k*urowstride;
                uk0 = rowstorage[offsk+0];
                uk1 = rowstorage[offsk+1];
                rowstorage[targetrow+0] = rowstorage[targetrow+0]-u00*uk0-u01*uk1;
                rowstorage[targetrow+1] = rowstorage[targetrow+1]-u10*uk0-u11*uk1;
                rowstorage[targetrow+2] = rowstorage[targetrow+2]-u20*uk0-u21*uk1;
                rowstorage[targetrow+3] = rowstorage[targetrow+3]-u30*uk0-u31*uk1;
            }
        }
        if( urank==3 )
        {
            for(k=0; k<=uheight-1; k++)
            {
                targetrow = offss+raw2smap[superrowidx[urbase+k]]*4;
                offsk = offsu+k*urowstride;
                uk0 = rowstorage[offsk+0];
                uk1 = rowstorage[offsk+1];
                uk2 = rowstorage[offsk+2];
                rowstorage[targetrow+0] = rowstorage[targetrow+0]-u00*uk0-u01*uk1-u02*uk2;
                rowstorage[targetrow+1] = rowstorage[targetrow+1]-u10*uk0-u11*uk1-u12*uk2;
                rowstorage[targetrow+2] = rowstorage[targetrow+2]-u20*uk0-u21*uk1-u22*uk2;
                rowstorage[targetrow+3] = rowstorage[targetrow+3]-u30*uk0-u31*uk1-u32*uk2;
            }
        }
        if( urank==4 )
        {
            for(k=0; k<=uheight-1; k++)
            {
                targetrow = offss+raw2smap[superrowidx[urbase+k]]*4;
                offsk = offsu+k*urowstride;
                uk0 = rowstorage[offsk+0];
                uk1 = rowstorage[offsk+1];
                uk2 = rowstorage[offsk+2];
                uk3 = rowstorage[offsk+3];
                rowstorage[targetrow+0] = rowstorage[targetrow+0]-u00*uk0-u01*uk1-u02*uk2-u03*uk3;
                rowstorage[targetrow+1] = rowstorage[targetrow+1]-u10*uk0-u11*uk1-u12*uk2-u13*uk3;
                rowstorage[targetrow+2] = rowstorage[targetrow+2]-u20*uk0-u21*uk1-u22*uk2-u23*uk3;
                rowstorage[targetrow+3] = rowstorage[targetrow+3]-u30*uk0-u31*uk1-u32*uk2-u33*uk3;
            }
        }
        result = true;
        return result;
    }
#endif


        /*************************************************************************
        Fast kernels for small supernodal updates: special rank-1 function.

        ! See comments on UpdateSupernode() for information  on generic supernodal
        ! updates, including notation used below.

        The generic update has following form:

            S := S - scatter(U*D*Uc')

        This specialized function performs rank-1 update, i.e.:
        * S is a tHeight*A matrix, with A<=4
        * U is a uHeight*1 matrix with unit stride
        * Uc' is a 1*B matrix, with B<=A
        * scatter() scatters rows and columns of U*Uc'

        Return value:
        * True if update was applied
        * False if kernel refused to perform an update (quick exit for unsupported
          combinations of input sizes)

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        private static bool updatekernelrank1(double[] rowstorage,
            int offss,
            int twidth,
            int trowstride,
            int offsu,
            int uheight,
            int uwidth,
            double[] diagd,
            int offsd,
            int[] raw2smap,
            int[] superrowidx,
            int urbase,
            xparams _params)
        {
            bool result = new bool();
            int k = 0;
            int targetrow = 0;
            double d0 = 0;
            double u00 = 0;
            double u10 = 0;
            double u20 = 0;
            double u30 = 0;
            double uk = 0;
            int col0 = 0;
            int col1 = 0;
            int col2 = 0;
            int col3 = 0;


            //
            // Filter out unsupported combinations (ones that are too sparse for the non-SIMD code)
            //
            result = false;
            if (twidth > 4)
            {
                return result;
            }
            if (uwidth > 4)
            {
                return result;
            }

            //
            // Determine target columns, load update matrix
            //
            d0 = diagd[offsd];
            col0 = 0;
            col1 = 0;
            col2 = 0;
            col3 = 0;
            u00 = 0;
            u10 = 0;
            u20 = 0;
            u30 = 0;
            if (uwidth >= 1)
            {
                col0 = raw2smap[superrowidx[urbase + 0]];
                u00 = d0 * rowstorage[offsu + 0];
            }
            if (uwidth >= 2)
            {
                col1 = raw2smap[superrowidx[urbase + 1]];
                u10 = d0 * rowstorage[offsu + 1];
            }
            if (uwidth >= 3)
            {
                col2 = raw2smap[superrowidx[urbase + 2]];
                u20 = d0 * rowstorage[offsu + 2];
            }
            if (uwidth >= 4)
            {
                col3 = raw2smap[superrowidx[urbase + 3]];
                u30 = d0 * rowstorage[offsu + 3];
            }

            //
            // Run update
            //
            if (uwidth == 1)
            {
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + raw2smap[superrowidx[urbase + k]] * trowstride;
                    uk = rowstorage[offsu + k];
                    rowstorage[targetrow + col0] = rowstorage[targetrow + col0] - u00 * uk;
                }
            }
            if (uwidth == 2)
            {
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + raw2smap[superrowidx[urbase + k]] * trowstride;
                    uk = rowstorage[offsu + k];
                    rowstorage[targetrow + col0] = rowstorage[targetrow + col0] - u00 * uk;
                    rowstorage[targetrow + col1] = rowstorage[targetrow + col1] - u10 * uk;
                }
            }
            if (uwidth == 3)
            {
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + raw2smap[superrowidx[urbase + k]] * trowstride;
                    uk = rowstorage[offsu + k];
                    rowstorage[targetrow + col0] = rowstorage[targetrow + col0] - u00 * uk;
                    rowstorage[targetrow + col1] = rowstorage[targetrow + col1] - u10 * uk;
                    rowstorage[targetrow + col2] = rowstorage[targetrow + col2] - u20 * uk;
                }
            }
            if (uwidth == 4)
            {
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + raw2smap[superrowidx[urbase + k]] * trowstride;
                    uk = rowstorage[offsu + k];
                    rowstorage[targetrow + col0] = rowstorage[targetrow + col0] - u00 * uk;
                    rowstorage[targetrow + col1] = rowstorage[targetrow + col1] - u10 * uk;
                    rowstorage[targetrow + col2] = rowstorage[targetrow + col2] - u20 * uk;
                    rowstorage[targetrow + col3] = rowstorage[targetrow + col3] - u30 * uk;
                }
            }
            result = true;
            return result;
        }


        /*************************************************************************
        Fast kernels for small supernodal updates: special rank-2 function.

        ! See comments on UpdateSupernode() for information  on generic supernodal
        ! updates, including notation used below.

        The generic update has following form:

            S := S - scatter(U*D*Uc')

        This specialized function performs rank-2 update, i.e.:
        * S is a tHeight*A matrix, with A<=4
        * U is a uHeight*2 matrix with row stride equal to 2
        * Uc' is a 2*B matrix, with B<=A
        * scatter() scatters rows and columns of U*Uc

        Return value:
        * True if update was applied
        * False if kernel refused to perform an update (quick exit for unsupported
          combinations of input sizes)

          -- ALGLIB routine --
             20.09.2020
             Bochkanov Sergey
        *************************************************************************/
        private static bool updatekernelrank2(double[] rowstorage,
            int offss,
            int twidth,
            int trowstride,
            int offsu,
            int uheight,
            int uwidth,
            double[] diagd,
            int offsd,
            int[] raw2smap,
            int[] superrowidx,
            int urbase,
            xparams _params)
        {
            bool result = new bool();
            int k = 0;
            int targetrow = 0;
            double d0 = 0;
            double d1 = 0;
            double u00 = 0;
            double u10 = 0;
            double u20 = 0;
            double u30 = 0;
            double u01 = 0;
            double u11 = 0;
            double u21 = 0;
            double u31 = 0;
            double uk0 = 0;
            double uk1 = 0;
            int col0 = 0;
            int col1 = 0;
            int col2 = 0;
            int col3 = 0;


            //
            // Filter out unsupported combinations (ones that are too sparse for the non-SIMD code)
            //
            result = false;
            if (twidth > 4)
            {
                return result;
            }
            if (uwidth > 4)
            {
                return result;
            }

            //
            // Determine target columns, load update matrix
            //
            d0 = diagd[offsd];
            d1 = diagd[offsd + 1];
            col0 = 0;
            col1 = 0;
            col2 = 0;
            col3 = 0;
            u00 = 0;
            u01 = 0;
            u10 = 0;
            u11 = 0;
            u20 = 0;
            u21 = 0;
            u30 = 0;
            u31 = 0;
            if (uwidth >= 1)
            {
                col0 = raw2smap[superrowidx[urbase + 0]];
                u00 = d0 * rowstorage[offsu + 0];
                u01 = d1 * rowstorage[offsu + 1];
            }
            if (uwidth >= 2)
            {
                col1 = raw2smap[superrowidx[urbase + 1]];
                u10 = d0 * rowstorage[offsu + 1 * 2 + 0];
                u11 = d1 * rowstorage[offsu + 1 * 2 + 1];
            }
            if (uwidth >= 3)
            {
                col2 = raw2smap[superrowidx[urbase + 2]];
                u20 = d0 * rowstorage[offsu + 2 * 2 + 0];
                u21 = d1 * rowstorage[offsu + 2 * 2 + 1];
            }
            if (uwidth >= 4)
            {
                col3 = raw2smap[superrowidx[urbase + 3]];
                u30 = d0 * rowstorage[offsu + 3 * 2 + 0];
                u31 = d1 * rowstorage[offsu + 3 * 2 + 1];
            }

            //
            // Run update
            //
            if (uwidth == 1)
            {
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + raw2smap[superrowidx[urbase + k]] * trowstride;
                    uk0 = rowstorage[offsu + 2 * k + 0];
                    uk1 = rowstorage[offsu + 2 * k + 1];
                    rowstorage[targetrow + col0] = rowstorage[targetrow + col0] - u00 * uk0 - u01 * uk1;
                }
            }
            if (uwidth == 2)
            {
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + raw2smap[superrowidx[urbase + k]] * trowstride;
                    uk0 = rowstorage[offsu + 2 * k + 0];
                    uk1 = rowstorage[offsu + 2 * k + 1];
                    rowstorage[targetrow + col0] = rowstorage[targetrow + col0] - u00 * uk0 - u01 * uk1;
                    rowstorage[targetrow + col1] = rowstorage[targetrow + col1] - u10 * uk0 - u11 * uk1;
                }
            }
            if (uwidth == 3)
            {
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + raw2smap[superrowidx[urbase + k]] * trowstride;
                    uk0 = rowstorage[offsu + 2 * k + 0];
                    uk1 = rowstorage[offsu + 2 * k + 1];
                    rowstorage[targetrow + col0] = rowstorage[targetrow + col0] - u00 * uk0 - u01 * uk1;
                    rowstorage[targetrow + col1] = rowstorage[targetrow + col1] - u10 * uk0 - u11 * uk1;
                    rowstorage[targetrow + col2] = rowstorage[targetrow + col2] - u20 * uk0 - u21 * uk1;
                }
            }
            if (uwidth == 4)
            {
                for (k = 0; k <= uheight - 1; k++)
                {
                    targetrow = offss + raw2smap[superrowidx[urbase + k]] * trowstride;
                    uk0 = rowstorage[offsu + 2 * k + 0];
                    uk1 = rowstorage[offsu + 2 * k + 1];
                    rowstorage[targetrow + col0] = rowstorage[targetrow + col0] - u00 * uk0 - u01 * uk1;
                    rowstorage[targetrow + col1] = rowstorage[targetrow + col1] - u10 * uk0 - u11 * uk1;
                    rowstorage[targetrow + col2] = rowstorage[targetrow + col2] - u20 * uk0 - u21 * uk1;
                    rowstorage[targetrow + col3] = rowstorage[targetrow + col3] - u30 * uk0 - u31 * uk1;
                }
            }
            result = true;
            return result;
        }


        /*************************************************************************
        Debug checks for sparsity structure

          -- ALGLIB routine --
             22.08.2021
             Bochkanov Sergey
        *************************************************************************/
        private static void slowdebugchecks(sparse.sparsematrix a,
            int[] fillinperm,
            int n,
            int tail,
            sparse.sparsematrix referencetaila,
            xparams _params)
        {
            int i = 0;
            int j = 0;
            sparse.sparsematrix perma = new sparse.sparsematrix();
            double[,] densea = new double[0, 0];

            sparse.sparsesymmpermtblbuf(a, false, fillinperm, perma, _params);
            densea = new double[n, n];
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= i; j++)
                {
                    if (!sparse.sparseexists(perma, i, j, _params))
                    {
                        densea[i, j] = 0;
                        continue;
                    }
                    if (i == j)
                    {
                        densea[i, j] = 1;
                    }
                    else
                    {
                        densea[i, j] = 0.01 * (Math.Cos(i + 1) + 1.23 * Math.Sin(j + 1)) / n;
                    }
                }
            }
            ap.assert(dbgmatrixcholesky2(densea, 0, n - tail, false, _params), "densechol failed");
            ablas.rmatrixrighttrsm(tail, n - tail, densea, 0, 0, false, false, 1, densea, n - tail, 0, _params);
            ablas.rmatrixsyrk(tail, n - tail, -1.0, densea, n - tail, 0, 0, 1.0, densea, n - tail, n - tail, false, _params);
            for (i = n - tail; i <= n - 1; i++)
            {
                for (j = n - tail; j <= i; j++)
                {
                    ap.assert(!((double)(densea[i, j]) == (double)(0) && sparse.sparseexists(referencetaila, i - (n - tail), j - (n - tail), _params)), "SPSymmAnalyze: structure check 1 failed");
                    ap.assert(!((double)(densea[i, j]) != (double)(0) && !sparse.sparseexists(referencetaila, i - (n - tail), j - (n - tail), _params)), "SPSymmAnalyze: structure check 2 failed");
                }
            }
        }


        /*************************************************************************
        Dense Cholesky driver for internal integrity checks

          -- ALGLIB routine --
             22.08.2021
             Bochkanov Sergey
        *************************************************************************/
        private static bool dbgmatrixcholesky2(double[,] aaa,
            int offs,
            int n,
            bool isupper,
            xparams _params)
        {
            bool result = new bool();
            int i = 0;
            int j = 0;
            double ajj = 0;
            double v = 0;
            double r = 0;
            double[] tmp = new double[0];
            int i_ = 0;
            int i1_ = 0;

            tmp = new double[2 * n];
            result = true;
            if (n < 0)
            {
                result = false;
                return result;
            }

            //
            // Quick return if possible
            //
            if (n == 0)
            {
                return result;
            }
            if (isupper)
            {

                //
                // Compute the Cholesky factorization A = U'*U.
                //
                for (j = 0; j <= n - 1; j++)
                {

                    //
                    // Compute U(J,J) and test for non-positive-definiteness.
                    //
                    v = 0.0;
                    for (i_ = offs; i_ <= offs + j - 1; i_++)
                    {
                        v += aaa[i_, offs + j] * aaa[i_, offs + j];
                    }
                    ajj = aaa[offs + j, offs + j] - v;
                    if ((double)(ajj) <= (double)(0))
                    {
                        aaa[offs + j, offs + j] = ajj;
                        result = false;
                        return result;
                    }
                    ajj = Math.Sqrt(ajj);
                    aaa[offs + j, offs + j] = ajj;

                    //
                    // Compute elements J+1:N-1 of row J.
                    //
                    if (j < n - 1)
                    {
                        if (j > 0)
                        {
                            i1_ = (offs) - (0);
                            for (i_ = 0; i_ <= j - 1; i_++)
                            {
                                tmp[i_] = -aaa[i_ + i1_, offs + j];
                            }
                            ablas.rmatrixmv(n - j - 1, j, aaa, offs, offs + j + 1, 1, tmp, 0, tmp, n, _params);
                            i1_ = (n) - (offs + j + 1);
                            for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                            {
                                aaa[offs + j, i_] = aaa[offs + j, i_] + tmp[i_ + i1_];
                            }
                        }
                        r = 1 / ajj;
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            aaa[offs + j, i_] = r * aaa[offs + j, i_];
                        }
                    }
                }
            }
            else
            {

                //
                // Compute the Cholesky factorization A = L*L'.
                //
                for (j = 0; j <= n - 1; j++)
                {

                    //
                    // Compute L(J+1,J+1) and test for non-positive-definiteness.
                    //
                    v = 0.0;
                    for (i_ = offs; i_ <= offs + j - 1; i_++)
                    {
                        v += aaa[offs + j, i_] * aaa[offs + j, i_];
                    }
                    ajj = aaa[offs + j, offs + j] - v;
                    if ((double)(ajj) <= (double)(0))
                    {
                        aaa[offs + j, offs + j] = ajj;
                        result = false;
                        return result;
                    }
                    ajj = Math.Sqrt(ajj);
                    aaa[offs + j, offs + j] = ajj;

                    //
                    // Compute elements J+1:N of column J.
                    //
                    if (j < n - 1)
                    {
                        r = 1 / ajj;
                        if (j > 0)
                        {
                            i1_ = (offs) - (0);
                            for (i_ = 0; i_ <= j - 1; i_++)
                            {
                                tmp[i_] = aaa[offs + j, i_ + i1_];
                            }
                            ablas.rmatrixmv(n - j - 1, j, aaa, offs + j + 1, offs, 0, tmp, 0, tmp, n, _params);
                            for (i = 0; i <= n - j - 2; i++)
                            {
                                aaa[offs + j + 1 + i, offs + j] = (aaa[offs + j + 1 + i, offs + j] - tmp[n + i]) * r;
                            }
                        }
                        else
                        {
                            for (i = 0; i <= n - j - 2; i++)
                            {
                                aaa[offs + j + 1 + i, offs + j] = aaa[offs + j + 1 + i, offs + j] * r;
                            }
                        }
                    }
                }
            }
            return result;
        }


}
