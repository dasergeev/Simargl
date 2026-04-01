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


/*************************************************************************
RBF far field expansion kernels
*************************************************************************/
public partial class rbfv3farfields
{
    /*************************************************************************
    Fast kernel for biharmonic panel with NY=1

    INPUT PARAMETERS:
        D0, D1, D2      -   evaluation point minus (Panel.C0,Panel.C1,Panel.C2)

    OUTPUT PARAMETERS:
        F               -   model value
        InvPowRPPlus1   -   1/(R^(P+1))

      -- ALGLIB --
         Copyright 19.11.2022 by Sergey Bochkanov
    *************************************************************************/
#if ALGLIB_USE_SIMD
    private static unsafe bool try_bhpaneleval1fastkernel(double d0,
        double d1,
        double d2,
        int panelp,
        double* pnma,
        double* pnmb,
        double* pmmcdiag,
        double* ynma,
        double* tblrmodmn,
        out double f,
        out double invpowrpplus1)
    {
        f = 0;
        invpowrpplus1 = 0;
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        //#if !ALGLIB_NO_FMA
        //if( Fma.IsSupported )
        //{
        //    return true;
        //}
        //#endif // no-fma
        if( Avx2.IsSupported )
        {
            int n;
            double r, r2, r01, invr, sintheta, costheta;
            complex expiphi, expiphi2, expiphi3, expiphi4;
            int jj;
            double sml = 1.0E-100;
            
            f = 0.0;
            invpowrpplus1 = 0.0;
            
            
            /*
             *Convert to spherical polar coordinates.
             *
             * NOTE: we make sure that R is non-zero by adding extremely small perturbation
             */
            r2 = d0*d0+d1*d1+d2*d2+sml;
            r = System.Math.Sqrt(r2);
            r01 = System.Math.Sqrt(d0*d0+d1*d1+sml);
            costheta = d2/r;
            sintheta = r01/r;
            expiphi.x = d0/r01;
            expiphi.y = d1/r01;
            invr = 1.0/r;
            
            /*
             * prepare precomputed quantities
             */
            double powsintheta2 = sintheta*sintheta;
            double powsintheta3 = powsintheta2*sintheta;
            double powsintheta4 = powsintheta2*powsintheta2;
            expiphi2.x = expiphi.x*expiphi.x-expiphi.y*expiphi.y;
            expiphi2.y = 2*expiphi.x*expiphi.y;
            expiphi3.x = expiphi2.x*expiphi.x-expiphi2.y*expiphi.y;
            expiphi3.y = expiphi2.x*expiphi.y+expiphi.x*expiphi2.y;
            expiphi4.x = expiphi2.x*expiphi2.x-expiphi2.y*expiphi2.y;
            expiphi4.y = 2*expiphi2.x*expiphi2.y;
            
            /*
             * Compute far field expansion for a cluster of basis functions f=r
             *
             * NOTE: the original paper by Beatson et al. uses f=r as the basis function,
             *       whilst ALGLIB uses f=-r due to conditional positive definiteness requirement.
             *       We will perform conversion later.
             */
            Intrinsics.Vector256<double> v_costheta      = Intrinsics.Vector256.Create(costheta);
            Intrinsics.Vector256<double> v_r2            = Intrinsics.Vector256.Create(r2);
            Intrinsics.Vector256<double> v_f             = Intrinsics.Vector256<double>.Zero;
            Intrinsics.Vector256<double> v_invr          = Intrinsics.Vector256.Create(invr);
            Intrinsics.Vector256<double> v_powsinthetaj  = Intrinsics.Vector256.Create(1.0, sintheta, powsintheta2, powsintheta3);
            Intrinsics.Vector256<double> v_powsintheta4  = Intrinsics.Vector256.Create(powsintheta4);
            Intrinsics.Vector256<double> v_expijphix     = Intrinsics.Vector256.Create(1.0, expiphi.x, expiphi2.x, expiphi3.x);
            Intrinsics.Vector256<double> v_expijphiy     = Intrinsics.Vector256.Create(0.0, expiphi.y, expiphi2.y, expiphi3.y);
            Intrinsics.Vector256<double> v_expi4phix     = Intrinsics.Vector256.Create(expiphi4.x);
            Intrinsics.Vector256<double> v_expi4phiy     = Intrinsics.Vector256.Create(expiphi4.y);
            for(jj=0; jj<4; jj++)
            {
                Intrinsics.Vector256<double> pnm_cur = Intrinsics.Vector256<double>.Zero, pnm_prev = Intrinsics.Vector256<double>.Zero, pnm_new;
                Intrinsics.Vector256<double> v_powrminusj1 = Intrinsics.Vector256.Create(invr);
                for(n=0; n<jj*4; n++)
                    v_powrminusj1 = Avx2.Multiply(v_powrminusj1, v_invr);
                for(n=jj*4; n<16; n++)
                {
                    int j0=jj*4;
                    int j1=j0+4;
                    
                    
                    pnm_new = Avx2.Multiply(v_powsinthetaj, Avx2.LoadVector256(pmmcdiag+n*16+j0));
                    pnm_new = Avx2.Add(pnm_new, Avx2.Multiply(v_costheta,Avx2.Multiply(pnm_cur,Avx2.LoadVector256(pnma+n*16+j0))));
                    pnm_new = Avx2.Add(pnm_new, Avx2.Multiply(pnm_prev,Avx2.LoadVector256(pnmb+n*16+j0)));
                    pnm_prev = pnm_cur;
                    pnm_cur  = pnm_new;
                    
                    Intrinsics.Vector256<double> v_tmp = Avx2.Multiply(pnm_cur, Avx2.LoadVector256(ynma+n*16+j0));
                    Intrinsics.Vector256<double> v_sphericalx = Avx2.Multiply(v_tmp, v_expijphix);
                    Intrinsics.Vector256<double> v_sphericaly = Avx2.Multiply(v_tmp, v_expijphiy);
                    
                    Intrinsics.Vector256<double> v_summnx = Avx2.Add(Avx2.Multiply(v_r2,Avx2.LoadVector256(tblrmodmn+n*64+j0+32)),Avx2.LoadVector256(tblrmodmn+n*64+j0));
                    Intrinsics.Vector256<double> v_summny = Avx2.Add(Avx2.Multiply(v_r2,Avx2.LoadVector256(tblrmodmn+n*64+j0+48)),Avx2.LoadVector256(tblrmodmn+n*64+j0+16));
                    
                    Intrinsics.Vector256<double> v_z = Avx2.Subtract(Avx2.Multiply(v_sphericalx,v_summnx),Avx2.Multiply(v_sphericaly,v_summny));
                    
                    v_f = Avx2.Add(v_f, Avx2.Multiply(v_powrminusj1, v_z));
                    v_powrminusj1 = Avx2.Multiply(v_powrminusj1, v_invr);
                }
                Intrinsics.Vector256<double> v_expijphix_new = Avx2.Subtract(Avx2.Multiply(v_expijphix,v_expi4phix),Avx2.Multiply(v_expijphiy,v_expi4phiy));
                Intrinsics.Vector256<double> v_expijphiy_new = Avx2.Add(Avx2.Multiply(v_expijphix,v_expi4phiy),Avx2.Multiply(v_expijphiy,v_expi4phix));
                v_powsinthetaj = Avx2.Multiply(v_powsinthetaj, v_powsintheta4);
                v_expijphix = v_expijphix_new;
                v_expijphiy = v_expijphiy_new;
            }
            
            double *ttt = stackalloc double[4];
            Avx2.Store(ttt, v_f);
            for(int k=0; k<4; k++)
                f += ttt[k];
            
            double r4 = r2*r2;
            double r8 = r4*r4;
            double r16 = r8*r8;
            invpowrpplus1 = 1/r16;

            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif
    private static bool bhpaneleval1fastkernel(double d0,
        double d1,
        double d2,
        int panelp,
        double[] pnma,
        double[] pnmb,
        double[] pmmcdiag,
        double[] ynma,
        double[] tblrmodmn,
        ref double f,
        ref double invpowrpplus1,
        xparams _params)
    {
#if ALGLIB_USE_SIMD
        unsafe
        {
            fixed(double* p_pnma=pnma, p_pnmb = pnmb, p_pmmcdiag=pmmcdiag, p_ynma=ynma, p_tblrmodmn=tblrmodmn)
            {
                if( try_bhpaneleval1fastkernel(d0, d1, d2, panelp, p_pnma, p_pnmb,p_pmmcdiag, p_ynma, p_tblrmodmn, out f, out invpowrpplus1) )
                    return true;
            }
        }
#endif

        //
        // No fast kernel
        //
        return false;
    }


    /*************************************************************************
    Fast kernel for biharmonic panel with NY=1

    INPUT PARAMETERS:
        D0, D1, D2      -   evaluation point minus (Panel.C0,Panel.C1,Panel.C2)

    OUTPUT PARAMETERS:
        F               -   model value
        InvPowRPPlus1   -   1/(R^(P+1))

      -- ALGLIB --
         Copyright 19.11.2022 by Sergey Bochkanov
    *************************************************************************/
#if ALGLIB_USE_SIMD
    private static unsafe bool try_bhpanelevalfastkernel(double d0,
        double d1,
        double d2,
        int ny,
        int panelp,
        double* pnma,
        double* pnmb,
        double* pmmcdiag,
        double* ynma,
        double* tblrmodmn,
        double* f,
        out double invpowrpplus1)
    {
        invpowrpplus1 = 0;
#if !ALGLIB_NO_SSE2
#if !ALGLIB_NO_AVX2
        //#if !ALGLIB_NO_FMA
        //if( Fma.IsSupported )
        //{
        //    return true;
        //}
        //#endif // no-fma
        if( Avx2.IsSupported )
        {
            int n;
            double r, r2, r01, invr, sintheta, costheta;
            complex expiphi, expiphi2, expiphi3, expiphi4;
            int jj;
            double sml = 1.0E-100;
            

            /*
             * Precomputed buffer which is enough for NY up to 16
             */
            if( ny>16 )
                return false;
            Intrinsics.Vector256<double>* v_f = stackalloc Intrinsics.Vector256<double>[ny];
            for(int k=0; k<ny; k++)
            {
                v_f[k] = Intrinsics.Vector256<double>.Zero;
                f[k] = 0.0;
            }
            invpowrpplus1 = 0.0;
            
            
            /*
             *Convert to spherical polar coordinates.
             *
             * NOTE: we make sure that R is non-zero by adding extremely small perturbation
             */
            r2 = d0*d0+d1*d1+d2*d2+sml;
            r = System.Math.Sqrt(r2);
            r01 = System.Math.Sqrt(d0*d0+d1*d1+sml);
            costheta = d2/r;
            sintheta = r01/r;
            expiphi.x = d0/r01;
            expiphi.y = d1/r01;
            invr = 1.0/r;
            
            /*
             * prepare precomputed quantities
             */
            double powsintheta2 = sintheta*sintheta;
            double powsintheta3 = powsintheta2*sintheta;
            double powsintheta4 = powsintheta2*powsintheta2;
            expiphi2.x = expiphi.x*expiphi.x-expiphi.y*expiphi.y;
            expiphi2.y = 2*expiphi.x*expiphi.y;
            expiphi3.x = expiphi2.x*expiphi.x-expiphi2.y*expiphi.y;
            expiphi3.y = expiphi2.x*expiphi.y+expiphi.x*expiphi2.y;
            expiphi4.x = expiphi2.x*expiphi2.x-expiphi2.y*expiphi2.y;
            expiphi4.y = 2*expiphi2.x*expiphi2.y;
            
            /*
             * Compute far field expansion for a cluster of basis functions f=r
             *
             * NOTE: the original paper by Beatson et al. uses f=r as the basis function,
             *       whilst ALGLIB uses f=-r due to conditional positive definiteness requirement.
             *       We will perform conversion later.
             */
            Intrinsics.Vector256<double> v_costheta      = Intrinsics.Vector256.Create(costheta);
            Intrinsics.Vector256<double> v_r2            = Intrinsics.Vector256.Create(r2);
            Intrinsics.Vector256<double> v_invr          = Intrinsics.Vector256.Create(invr);
            Intrinsics.Vector256<double> v_powsinthetaj  = Intrinsics.Vector256.Create(1.0, sintheta, powsintheta2, powsintheta3);
            Intrinsics.Vector256<double> v_powsintheta4  = Intrinsics.Vector256.Create(powsintheta4);
            Intrinsics.Vector256<double> v_expijphix     = Intrinsics.Vector256.Create(1.0, expiphi.x, expiphi2.x, expiphi3.x);
            Intrinsics.Vector256<double> v_expijphiy     = Intrinsics.Vector256.Create(0.0, expiphi.y, expiphi2.y, expiphi3.y);
            Intrinsics.Vector256<double> v_expi4phix     = Intrinsics.Vector256.Create(expiphi4.x);
            Intrinsics.Vector256<double> v_expi4phiy     = Intrinsics.Vector256.Create(expiphi4.y);
            for(jj=0; jj<4; jj++)
            {
                Intrinsics.Vector256<double> pnm_cur = Intrinsics.Vector256<double>.Zero, pnm_prev = Intrinsics.Vector256<double>.Zero, pnm_new;
                Intrinsics.Vector256<double> v_powrminusj1 = Intrinsics.Vector256.Create(invr);
                for(n=0; n<jj*4; n++)
                    v_powrminusj1 = Avx2.Multiply(v_powrminusj1, v_invr);
                for(n=jj*4; n<16; n++)
                {
                    int j0=jj*4;
                    int j1=j0+4;
                    
                    
                    pnm_new = Avx2.Multiply(v_powsinthetaj, Avx2.LoadVector256(pmmcdiag+n*16+j0));
                    pnm_new = Avx2.Add(pnm_new, Avx2.Multiply(v_costheta,Avx2.Multiply(pnm_cur,Avx2.LoadVector256(pnma+n*16+j0))));
                    pnm_new = Avx2.Add(pnm_new, Avx2.Multiply(pnm_prev,Avx2.LoadVector256(pnmb+n*16+j0)));
                    pnm_prev = pnm_cur;
                    pnm_cur  = pnm_new;
                    
                    Intrinsics.Vector256<double> v_tmp = Avx2.Multiply(pnm_cur, Avx2.LoadVector256(ynma+n*16+j0));
                    Intrinsics.Vector256<double> v_sphericalx = Avx2.Multiply(v_tmp, v_expijphix);
                    Intrinsics.Vector256<double> v_sphericaly = Avx2.Multiply(v_tmp, v_expijphiy);
                    
                    double *p_rmodmn = tblrmodmn+n*64+j0;
                    for(int k=0; k<ny; k++)
                    {
                        Intrinsics.Vector256<double> v_summnx = Avx2.Add(Avx2.Multiply(v_r2,Avx2.LoadVector256(p_rmodmn+32)),Avx2.LoadVector256(p_rmodmn));
                        Intrinsics.Vector256<double> v_summny = Avx2.Add(Avx2.Multiply(v_r2,Avx2.LoadVector256(p_rmodmn+48)),Avx2.LoadVector256(p_rmodmn+16));
                        Intrinsics.Vector256<double> v_z = Avx2.Subtract(Avx2.Multiply(v_sphericalx,v_summnx),Avx2.Multiply(v_sphericaly,v_summny));
                        v_f[k] = Avx2.Add(v_f[k], Avx2.Multiply(v_powrminusj1, v_z));
                        p_rmodmn += 1024;
                    }
                    v_powrminusj1 = Avx2.Multiply(v_powrminusj1, v_invr);
                }
                Intrinsics.Vector256<double> v_expijphix_new = Avx2.Subtract(Avx2.Multiply(v_expijphix,v_expi4phix),Avx2.Multiply(v_expijphiy,v_expi4phiy));
                Intrinsics.Vector256<double> v_expijphiy_new = Avx2.Add(Avx2.Multiply(v_expijphix,v_expi4phiy),Avx2.Multiply(v_expijphiy,v_expi4phix));
                v_powsinthetaj = Avx2.Multiply(v_powsinthetaj, v_powsintheta4);
                v_expijphix = v_expijphix_new;
                v_expijphiy = v_expijphiy_new;
            }
            
            double *ttt = stackalloc double[4];
            for(int t=0; t<ny; t++)
            {
                Avx2.Store(ttt, v_f[t]);
                for(int k=0; k<4; k++)
                    f[t] += ttt[k];
            }
            
            double r4 = r2*r2;
            double r8 = r4*r4;
            double r16 = r8*r8;
            invpowrpplus1 = 1/r16;

            return true;
        }
#endif // no-avx2
#endif // no-sse2
        return false;
    }
#endif
    private static bool bhpanelevalfastkernel(double d0,
        double d1,
        double d2,
        int ny,
        int panelp,
        double[] pnma,
        double[] pnmb,
        double[] pmmcdiag,
        double[] ynma,
        double[] tblrmodmn,
        double[] f,
        ref double invpowrpplus1,
        xparams _params)
    {
#if ALGLIB_USE_SIMD
        unsafe
        {
            fixed(double* p_pnma=pnma, p_pnmb = pnmb, p_pmmcdiag=pmmcdiag, p_ynma=ynma, p_tblrmodmn=tblrmodmn, p_f=f)
            {
                if( try_bhpanelevalfastkernel(d0, d1, d2, ny, panelp, p_pnma, p_pnmb,p_pmmcdiag, p_ynma, p_tblrmodmn, p_f, out invpowrpplus1) )
                    return true;
            }
        }
#endif

        //
        // No fast kernel
        //
        return false;
    }



    /*************************************************************************
    Biharmonic evaluator and its global temporaries
    *************************************************************************/
    public class biharmonicevaluator : apobject
    {
        public int maxp;
        public int precomputedcount;
        public double[] tdoublefactorial;
        public double[] tfactorial;
        public double[] tsqrtfactorial;
        public double[] tpowminus1;
        public complex[] tpowi;
        public complex[] tpowminusi;
        public double[] ynma;
        public double[] pnma;
        public double[] pnmb;
        public double[] pmmc;
        public double[] pmmcdiag;
        public double[] mnma;
        public double[] nnma;
        public complex[] inma;
        public biharmonicevaluator()
        {
            init();
        }
        public override void init()
        {
            tdoublefactorial = new double[0];
            tfactorial = new double[0];
            tsqrtfactorial = new double[0];
            tpowminus1 = new double[0];
            tpowi = new complex[0];
            tpowminusi = new complex[0];
            ynma = new double[0];
            pnma = new double[0];
            pnmb = new double[0];
            pmmc = new double[0];
            pmmcdiag = new double[0];
            mnma = new double[0];
            nnma = new double[0];
            inma = new complex[0];
        }
        public override apobject make_copy()
        {
            biharmonicevaluator _result = new biharmonicevaluator();
            _result.maxp = maxp;
            _result.precomputedcount = precomputedcount;
            _result.tdoublefactorial = (double[])tdoublefactorial.Clone();
            _result.tfactorial = (double[])tfactorial.Clone();
            _result.tsqrtfactorial = (double[])tsqrtfactorial.Clone();
            _result.tpowminus1 = (double[])tpowminus1.Clone();
            _result.tpowi = (complex[])tpowi.Clone();
            _result.tpowminusi = (complex[])tpowminusi.Clone();
            _result.ynma = (double[])ynma.Clone();
            _result.pnma = (double[])pnma.Clone();
            _result.pnmb = (double[])pnmb.Clone();
            _result.pmmc = (double[])pmmc.Clone();
            _result.pmmcdiag = (double[])pmmcdiag.Clone();
            _result.mnma = (double[])mnma.Clone();
            _result.nnma = (double[])nnma.Clone();
            _result.inma = (complex[])inma.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Panel (a set of closely located points) for a biharmonic evaluator
    containing aggregated far field expansion functions N_ij and M_ij.

    * C0, C1, C2 contain evaluation center
    * RMax contains radius of the points cloud
    * UseAtDistance contains minimum distance which allows to compute model
      value with required precision
    * NY contains 'output' dimensions count
    * P contains functions count
    * MaxSumAbs contains maximum of column-wise sums of coefficient magnitudes
    * tblN                  contains NY*PxP table of expansion functions N_nm,
                            with tblN[K*P*P+I*P+J] being N_nm(n=I,m=J) for K-th
                            output of the vector-valued model
    * tblM                  contains PxP table of expansion functions M_nm,
                            with tblM[K*P*P+I*P+J] being M_nm(n=I,m=J) for K-th
                            output of the vector-valued model
    * tblModN, tblModM      contain tables N and M with additional precomputed
                            multiplier merged in (which accelerates panel
                            evaluation)
    * tblRModMN             contains tblModM and tblModN in a packed format:
                            * first P elements are  Real(tblModM[0..P-1])
                            *  next P elements are Image(tblModM[0..P-1])
                            *  next P elements are  Real(tblModN[0..P-1])
                            *  next P elements are Image(tblModN[0..P-1])
                            * next 4*P elements store subsequent P elements of
                              tblModM and tblModN and so on
    *************************************************************************/
    public class biharmonicpanel : apobject
    {
        public double c0;
        public double c1;
        public double c2;
        public double rmax;
        public double useatdistance;
        public int ny;
        public int p;
        public int sizen;
        public int sizem;
        public int stride;
        public int sizeinner;
        public complex[] tbln;
        public complex[] tblm;
        public complex[] tblmodn;
        public complex[] tblmodm;
        public double[] tblpowrmax;
        public double[] tblrmodmn;
        public double maxsumabs;
        public complex[] funcsphericaly;
        public double[] tpowr;
        public biharmonicpanel()
        {
            init();
        }
        public override void init()
        {
            tbln = new complex[0];
            tblm = new complex[0];
            tblmodn = new complex[0];
            tblmodm = new complex[0];
            tblpowrmax = new double[0];
            tblrmodmn = new double[0];
            funcsphericaly = new complex[0];
            tpowr = new double[0];
        }
        public override apobject make_copy()
        {
            biharmonicpanel _result = new biharmonicpanel();
            _result.c0 = c0;
            _result.c1 = c1;
            _result.c2 = c2;
            _result.rmax = rmax;
            _result.useatdistance = useatdistance;
            _result.ny = ny;
            _result.p = p;
            _result.sizen = sizen;
            _result.sizem = sizem;
            _result.stride = stride;
            _result.sizeinner = sizeinner;
            _result.tbln = (complex[])tbln.Clone();
            _result.tblm = (complex[])tblm.Clone();
            _result.tblmodn = (complex[])tblmodn.Clone();
            _result.tblmodm = (complex[])tblmodm.Clone();
            _result.tblpowrmax = (double[])tblpowrmax.Clone();
            _result.tblrmodmn = (double[])tblrmodmn.Clone();
            _result.maxsumabs = maxsumabs;
            _result.funcsphericaly = (complex[])funcsphericaly.Clone();
            _result.tpowr = (double[])tpowr.Clone();
            return _result;
        }
    };




    /*************************************************************************
    Initialize precomputed table for a biharmonic evaluator

      -- ALGLIB --
         Copyright 26.08.2022 by Sergey Bochkanov
    *************************************************************************/
    public static void biharmonicevaluatorinit(biharmonicevaluator eval,
        int maxp,
        xparams _params)
    {
        int i = 0;
        int n = 0;
        int m = 0;
        complex cplxi = 0;
        complex cplxminusi = 0;

        ap.assert(maxp >= 2, "BiharmonicEvaluatorInit: MaxP<2");
        eval.maxp = maxp;

        //
        // Precompute some often used values
        //
        // NOTE: we use SetLength() instead of rAllocV() in order to enforce strict length
        //       of the precomputed tables which results in better bounds checking during
        //       the running time.
        //
        eval.precomputedcount = 2 * maxp + 3;
        eval.tpowminus1 = new double[eval.precomputedcount];
        eval.tpowminusi = new complex[eval.precomputedcount];
        eval.tpowi = new complex[eval.precomputedcount];
        cplxi.x = 0;
        cplxi.y = 1;
        cplxminusi.x = 0;
        cplxminusi.y = -1;
        eval.tpowminus1[0] = 1;
        eval.tpowminusi[0] = 1;
        eval.tpowi[0] = 1;
        for (i = 1; i <= eval.precomputedcount - 1; i++)
        {
            eval.tpowminus1[i] = eval.tpowminus1[i - 1] * -1;
            eval.tpowminusi[i] = eval.tpowminusi[i - 1] * cplxminusi;
            eval.tpowi[i] = eval.tpowi[i - 1] * cplxi;
        }
        eval.tfactorial = new double[eval.precomputedcount];
        eval.tsqrtfactorial = new double[eval.precomputedcount];
        eval.tfactorial[0] = 1;
        for (i = 1; i <= eval.precomputedcount - 1; i++)
        {
            eval.tfactorial[i] = i * eval.tfactorial[i - 1];
        }
        for (i = 0; i <= eval.precomputedcount - 1; i++)
        {
            eval.tsqrtfactorial[i] = Math.Sqrt(eval.tfactorial[i]);
        }
        eval.tdoublefactorial = new double[eval.precomputedcount];
        ap.assert(eval.precomputedcount >= 2, "BiharmonicEvaluatorInit: integrity check 8446 failed");
        eval.tdoublefactorial[0] = 1;
        eval.tdoublefactorial[1] = 1;
        for (i = 2; i <= eval.precomputedcount - 1; i++)
        {
            eval.tdoublefactorial[i] = i * eval.tdoublefactorial[i - 2];
        }

        //
        // Precompute coefficients for the associated Legendre recurrence relation
        //
        //   P[n+1,m] = P[n,m]*CosTheta*(2*n-1)/(N-M) - P[n-1,m]*(N+M-1)/(N-M)  (for n>m)
        //            = P[n,m]*CosTheta*PnmA[n+1,m] + P[n-1,m]*PnmB[n+1,m]      (for n>m)
        //
        ablasf.rsetallocv((maxp + 1) * (maxp + 1), 0.0, ref eval.pnma, _params);
        ablasf.rsetallocv((maxp + 1) * (maxp + 1), 0.0, ref eval.pnmb, _params);
        for (n = 0; n <= maxp; n++)
        {
            for (m = 0; m <= n - 1; m++)
            {
                eval.pnma[n * (maxp + 1) + m] = (double)(2 * n - 1) / (double)(n - m);
                eval.pnmb[n * (maxp + 1) + m] = -((double)(n + m - 1) / (double)(n - m));
            }
        }

        //
        // Precompute coefficient used during computation of initial values of the
        // associated Legendre recurrence
        //
        ablasf.rsetallocv(maxp + 1, 0.0, ref eval.pmmc, _params);
        ablasf.rsetallocv((maxp + 1) * (maxp + 1), 0.0, ref eval.pmmcdiag, _params);
        for (m = 0; m <= maxp; m++)
        {
            eval.pmmc[m] = eval.tpowminus1[m] * eval.tdoublefactorial[Math.Max(2 * m - 1, 0)];
            eval.pmmcdiag[m * (maxp + 1) + m] = eval.pmmc[m];
        }

        //
        // Precompute coefficient YnmA used during computation of the spherical harmonic Ynm
        //
        ablasf.rsetallocv((maxp + 1) * (maxp + 1), 0.0, ref eval.ynma, _params);
        for (n = 0; n <= maxp; n++)
        {
            for (m = 0; m <= n; m++)
            {
                eval.ynma[n * (maxp + 1) + m] = eval.tpowminus1[m] * eval.tsqrtfactorial[n - m] / eval.tsqrtfactorial[n + m];
            }
        }

        //
        // Precompute coefficient InmA used during computation of the inner function Inm
        //
        ablasf.csetallocv((maxp + 1) * (maxp + 1), 0.0, ref eval.inma, _params);
        for (n = 0; n <= maxp; n++)
        {
            for (m = 0; m <= n; m++)
            {
                eval.inma[n * (maxp + 1) + m] = eval.tpowminusi[m] * (eval.tpowminus1[n] / (eval.tsqrtfactorial[n + m] * eval.tsqrtfactorial[n - m]));
            }
        }

        //
        // Precompute coefficients MnmA and NnmA used during computation of expansion functions Mnm and Nnm
        //
        ablasf.rsetallocv(maxp + 1, 0.0, ref eval.mnma, _params);
        ablasf.rsetallocv(maxp + 1, 0.0, ref eval.nnma, _params);
        for (n = 0; n <= maxp; n++)
        {
            eval.nnma[n] = -(eval.tpowminus1[n] / (2 * n - 1));
            if (n <= maxp - 2)
            {
                eval.mnma[n] = eval.tpowminus1[n] / (2 * n + 3);
            }
        }
    }


    /*************************************************************************
    Build a panel with biharmonic far field expansions. The  function  assumes
    that we work with 3D data. Lower dimensional data can be zero-padded. Data
    with higher dimensionality is NOT supported by biharmonic code.

    IMPORTANT: this function computes far field expansion,  but  it  does  NOT
               compute error bounds. By default, far field distance is set  to
               some extremely big number.
               You should explicitly set desired far  field  tolerance  (which
               leads to automatic computation of the UseAtDistance  field)  by
               calling bhPanelSetPrec().

    INPUT PARAMETERS:
        Panel           -   panel to be initialized. Previously allocated
                            memory is reused as much as possible.
        XW              -   array[?,3+NY]:
                            * 3 first columns are X,Y,Z coordinates
                            * subsequent NY columns are basis function coefficients
        XIdx0, XIdx1    -   defines row range [XIdx0,XIdx1) of XW to process,
                            XIdx1-XIdx0 rows are processed, the rest is ignored
        NY              -   NY>=1, output values count
        Eval            -   precomputed table

      -- ALGLIB --
         Copyright 26.08.2022 by Sergey Bochkanov
    *************************************************************************/
    public static void bhpanelinit(biharmonicpanel panel,
        double[,] xw,
        int xidx0,
        int xidx1,
        int ny,
        biharmonicevaluator eval,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int d = 0;
        int n = 0;
        int offs = 0;
        int doffs = 0;
        double x0 = 0;
        double x1 = 0;
        double x2 = 0;
        double r = 0;
        double r2 = 0;
        double r01 = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double vnij = 0;
        double vmij = 0;
        double costheta = 0;
        double sintheta = 0;
        double powsinthetaj = 0;
        double pnmprev = 0;
        double pnm = 0;
        double pnmnew = 0;
        complex inma = 0;
        complex expiminusphi = 0;
        complex expiminusjphi = 0;
        complex sphericaly = 0;
        complex innernm = 0;
        complex nnm = 0;
        complex mnm = 0;
        complex fmult = 0;
        int stride2 = 0;

        ap.assert(xidx1 - xidx0 >= 1, "bhPanelInit: XIdx1<=XIdx0");

        //
        // Allocate space
        //
        panel.ny = ny;
        panel.p = eval.maxp;
        panel.stride = eval.maxp + 1;
        panel.sizeinner = eval.maxp + 1;
        panel.sizen = eval.maxp + 1;
        panel.sizem = eval.maxp - 1;
        panel.useatdistance = 0.001 * Math.Sqrt(math.maxrealnumber);
        ablasf.csetallocv(ny * panel.stride * panel.stride, 0.0, ref panel.tbln, _params);
        ablasf.csetallocv(ny * panel.stride * panel.stride, 0.0, ref panel.tblm, _params);
        ablasf.csetallocv(ny * panel.stride * panel.stride, 0.0, ref panel.tblmodn, _params);
        ablasf.csetallocv(ny * panel.stride * panel.stride, 0.0, ref panel.tblmodm, _params);
        ablasf.rsetallocv(ny * 4 * panel.stride * panel.stride, 0.0, ref panel.tblrmodmn, _params);
        stride2 = panel.stride * panel.stride;

        //
        // Compute center, SubAbs and RMax
        //
        panel.maxsumabs = 0;
        panel.c0 = 0;
        panel.c1 = 0;
        panel.c2 = 0;
        for (k = xidx0; k <= xidx1 - 1; k++)
        {
            panel.c0 = panel.c0 + xw[k, 0];
            panel.c1 = panel.c1 + xw[k, 1];
            panel.c2 = panel.c2 + xw[k, 2];
        }
        panel.c0 = panel.c0 / (xidx1 - xidx0);
        panel.c1 = panel.c1 / (xidx1 - xidx0);
        panel.c2 = panel.c2 / (xidx1 - xidx0);
        panel.rmax = math.machineepsilon;
        for (k = xidx0; k <= xidx1 - 1; k++)
        {
            v0 = xw[k, 0] - panel.c0;
            v1 = xw[k, 1] - panel.c1;
            v2 = xw[k, 2] - panel.c2;
            panel.rmax = Math.Max(panel.rmax, Math.Sqrt(v0 * v0 + v1 * v1 + v2 * v2));
        }
        for (d = 0; d <= ny - 1; d++)
        {
            v = 0;
            for (k = xidx0; k <= xidx1 - 1; k++)
            {
                v = v + Math.Abs(xw[k, 3 + d]);
            }
            panel.maxsumabs = Math.Max(panel.maxsumabs, v);
        }

        //
        // Precompute powers of RMax up to MaxP+1
        //
        ablasf.rallocv(eval.maxp + 2, ref panel.tblpowrmax, _params);
        panel.tblpowrmax[0] = 1;
        for (i = 1; i <= eval.maxp + 1; i++)
        {
            panel.tblpowrmax[i] = panel.tblpowrmax[i - 1] * panel.rmax;
        }

        //
        // Fill tables N and M
        //
        ablasf.rallocv(panel.sizeinner, ref panel.tpowr, _params);
        for (k = xidx0; k <= xidx1 - 1; k++)
        {

            //
            // Prepare table of spherical harmonics for point K (to be used later to compute
            // inner functions I_nm and expansion functions N_nm and M_nm).
            //
            x0 = xw[k, 0] - panel.c0;
            x1 = xw[k, 1] - panel.c1;
            x2 = xw[k, 2] - panel.c2;
            r2 = x0 * x0 + x1 * x1 + x2 * x2 + math.minrealnumber;
            r = Math.Sqrt(r2);
            r01 = Math.Sqrt(x0 * x0 + x1 * x1 + math.minrealnumber);
            costheta = x2 / r;
            sintheta = r01 / r;
            expiminusphi.x = x0 / r01;
            expiminusphi.y = -(x1 / r01);
            panel.tpowr[0] = 1;
            for (i = 1; i <= panel.sizeinner - 1; i++)
            {
                panel.tpowr[i] = panel.tpowr[i - 1] * r;
            }

            //
            // Compute table of associated Legrengre polynomials, with
            //
            //     P_nm(N=I,M=-J,X)
            //
            // being an associated Legendre polynomial, as defined in "Numerical
            // recipes in C" and Wikipedia.
            //
            // It is important that the section 3 of 'Fast evaluation of polyharmonic
            // splines in three dimensions' by R.K. Beatson, M.J.D. Powell and A.M. Tan
            // uses different formulation of P_nm. We will account for the difference
            // during the computation of the spherical harmonics.
            //
            powsinthetaj = 1.0;
            expiminusjphi.x = 1.0;
            expiminusjphi.y = 0.0;
            for (j = 0; j <= panel.stride - 1; j++)
            {

                //
                // Prepare recursion for associated Legendre polynomials
                //
                pnmprev = 0;
                pnm = powsinthetaj * eval.pmmc[j];

                //
                // Perform recursion on N
                //
                for (n = j; n <= panel.stride - 1; n++)
                {
                    offs = n * panel.stride + j;

                    //
                    // Compute table of associated Legrengre polynomials with
                    //
                    //     P_nm(N=I,M=-J,X)
                    //
                    // being an associated Legendre polynomial, as defined in "Numerical
                    // recipes in C" and Wikipedia.
                    //
                    // It is important that the section 3 of 'Fast evaluation of polyharmonic
                    // splines in three dimensions' by R.K. Beatson, M.J.D. Powell and A.M. Tan
                    // uses different formulation of P_nm. We will account for the difference
                    // during the computation of the spherical harmonics.
                    //
                    if (n > j)
                    {

                        //
                        // Recursion on N
                        //
                        pnmnew = pnm * costheta * eval.pnma[offs] + pnmprev * eval.pnmb[offs];
                        pnmprev = pnm;
                        pnm = pnmnew;
                    }

                    //
                    // Compute table of spherical harmonics Y_nm(N=I,M=-J) with
                    //
                    //     Y_nm(N,M) = E_m*sqrt((n-m)!/(n+m)!)*P_nm(cos(theta))*exp(i*m*phi)
                    //     E_m = m>0 ? pow(-1,m) : 1
                    //     (because we always compute Y_nm for m<=0, E_m is always 1)
                    //
                    // being a spherical harmonic, as defined in the equation (18) of 'Fast evaluation of polyharmonic
                    // splines in three dimensions' by R.K. Beatson, M.J.D. Powell and A.M. Tan.
                    //
                    // Here P_nm is an associated Legendre polynomial as defined by Beatson et al. However, Pnm variable
                    // stores values of associated Legendre polynomials as defined by Wikipedia. Below we perform conversion
                    // between one format and another one:
                    //
                    //     P_nm(Beatson)  = P_nm(Wiki)*(-1)^(-m)*(n+m)!/(n-m)!
                    //     Y_nm(n=N,m=-J) = E_m*sqrt((n-m)!/(n+m)!)*P_nm(Beatson)*exp(i*m*phi)
                    //                    = E_m*sqrt((n-m)!/(n+m)!)*P_nm(Wiki)*(-1)^(-m)*(n+m)!/(n-m)!*exp(i*m*phi)
                    //                    = [E_m*(-1)^(-m)*sqrt((n+m)!/(n-m)!)*P_nm(Wiki)]*exp(i*m*phi)
                    //                    = A_ynm*P_nm(Wiki)*exp(i*m*phi)
                    //
                    v = pnm * eval.ynma[offs];
                    sphericaly.x = v * expiminusjphi.x;
                    sphericaly.y = v * expiminusjphi.y;

                    //
                    // Compute inner functions table where
                    //
                    //     InnerNM = I_nm(n=I,m=-J),
                    //
                    // with
                    //
                    //     I_nm(n,m) = pow(i,|m|)*pow(-1,n)*pow(r,n)/sqrt((n-m)!(n+m)!)*Y_nm(theta,phi)
                    //               = I_ynm*r^n*Y_nm(theta,phi)
                    //
                    // being an inner function, as defined in equation (20) of 'Fast evaluation of polyharmonic splines in three dimensions'
                    // by R.K. Beatson, M.J.D. Powell and A.M. Tan 1.
                    //
                    v = panel.tpowr[n];
                    inma = eval.inma[offs];
                    innernm.x = v * (inma.x * sphericaly.x - inma.y * sphericaly.y);
                    innernm.y = v * (inma.x * sphericaly.y + inma.y * sphericaly.x);

                    //
                    // Update expansion functions N_nm and M_nm with harmonics coming from the point #K
                    //
                    // NOTE: precomputed coefficient MnmA[] takes care of the fact that we require
                    //       Mnm for n,m>P-2 to be zero. It is exactly zero at the corresponding positions,
                    //       so we may proceed without conditional operators.
                    //
                    for (d = 0; d <= ny - 1; d++)
                    {
                        doffs = offs + d * stride2;
                        vnij = xw[k, 3 + d] * eval.nnma[n];
                        nnm = panel.tbln[doffs];
                        panel.tbln[doffs].x = nnm.x + vnij * innernm.x;
                        panel.tbln[doffs].y = nnm.y + vnij * innernm.y;
                        vmij = xw[k, 3 + d] * panel.tpowr[2] * eval.mnma[n];
                        mnm = panel.tblm[doffs];
                        panel.tblm[doffs].x = mnm.x + vmij * innernm.x;
                        panel.tblm[doffs].y = mnm.y + vmij * innernm.y;
                    }
                }

                //
                // Prepare for the next iteration
                //
                powsinthetaj = powsinthetaj * sintheta;
                v0 = expiminusjphi.x * expiminusphi.x - expiminusjphi.y * expiminusphi.y;
                v1 = expiminusjphi.x * expiminusphi.y + expiminusjphi.y * expiminusphi.x;
                expiminusjphi.x = v0;
                expiminusjphi.y = v1;
            }
        }

        //
        // Compute modified N_nm and M_nm by multiplying original values by sqrt((n-m)!(n+m)!)*i^(-m),
        // with additional multiplication by 2 for J<>0
        //
        // This scaling factor is a part of the outer function O_nm which is computed during the model
        // evaluation, and it does NOT depends on the trial point X. So, merging it with N_nm/M_nm
        // saves us a lot of computational effort.
        //
        for (i = 0; i <= panel.p; i++)
        {
            for (j = 0; j <= i; j++)
            {
                v = eval.tsqrtfactorial[i + j] * eval.tsqrtfactorial[i - j];
                if (j != 0)
                {
                    v = v * 2;
                }
                fmult = eval.tpowi[j];
                fmult.x = fmult.x * v;
                fmult.y = fmult.y * v;
                if (i < panel.sizen)
                {
                    for (d = 0; d <= ny - 1; d++)
                    {
                        nnm = panel.tbln[d * stride2 + i * panel.stride + j];
                        panel.tblmodn[d * stride2 + i * panel.stride + j].x = nnm.x * fmult.x - nnm.y * fmult.y;
                        panel.tblmodn[d * stride2 + i * panel.stride + j].y = nnm.x * fmult.y + nnm.y * fmult.x;
                    }
                }
                if (i < panel.sizem)
                {
                    for (d = 0; d <= ny - 1; d++)
                    {
                        mnm = panel.tblm[d * stride2 + i * panel.stride + j];
                        panel.tblmodm[d * stride2 + i * panel.stride + j].x = mnm.x * fmult.x - mnm.y * fmult.y;
                        panel.tblmodm[d * stride2 + i * panel.stride + j].y = mnm.x * fmult.y + mnm.y * fmult.x;
                    }
                }
            }
        }

        //
        // Convert tblModN and tblModN into packed storage
        //
        offs = 0;
        doffs = 0;
        for (i = 0; i <= (panel.p + 1) * ny - 1; i++)
        {
            for (j = 0; j <= panel.p; j++)
            {
                panel.tblrmodmn[doffs + j] = panel.tblmodm[offs + j].x;
            }
            doffs = doffs + panel.stride;
            for (j = 0; j <= panel.p; j++)
            {
                panel.tblrmodmn[doffs + j] = panel.tblmodm[offs + j].y;
            }
            doffs = doffs + panel.stride;
            for (j = 0; j <= panel.p; j++)
            {
                panel.tblrmodmn[doffs + j] = panel.tblmodn[offs + j].x;
            }
            doffs = doffs + panel.stride;
            for (j = 0; j <= panel.p; j++)
            {
                panel.tblrmodmn[doffs + j] = panel.tblmodn[offs + j].y;
            }
            doffs = doffs + panel.stride;
            offs = offs + panel.stride;
        }

        //
        // Default UseAtDistance, means that far field is not used
        //
        panel.useatdistance = 1.0E50 + 1.0E6 * panel.rmax;
    }


    /*************************************************************************
    This function sets far field distance depending on desired accuracy.

    INPUT PARAMETERS:
        Panel           -   panel with valid far field expansion
        Tol             -   desired tolerance

      -- ALGLIB --
         Copyright 20.11.2022 by Sergey Bochkanov
    *************************************************************************/
    public static void bhpanelsetprec(biharmonicpanel panel,
        double tol,
        xparams _params)
    {
        double errbnd = 0;
        double rcand = 0;

        ap.assert(math.isfinite(tol) && (double)(tol) > (double)(0), "bhPanelSetPrec: Tol<=0 or infinite");
        rcand = panel.rmax;
        do
        {
            rcand = 1.05 * rcand + math.machineepsilon;
            errbnd = panel.maxsumabs * rcand * ((double)2 / (double)(2 * panel.p + 1)) * Math.Pow(panel.rmax / rcand, panel.p + 1) / (1 - panel.rmax / rcand);
        }
        while ((double)(errbnd) >= (double)(tol));
        panel.useatdistance = rcand;
    }


    /*************************************************************************
    Tries evaluating model using the far field expansion stored in the panel,
    special case for NY=1

    INPUT PARAMETERS:
        Panel           -   panel
        Eval            -   precomputed table
        X0, X1, X2      -   evaluation point
        NeedErrBnd      -   whether error bound is needed or not

    OUTPUT PARAMETERS:
        F               -   model value
        ErrBnd          -   upper bound on the far field expansion error, if
                            requested. Zero otherwise.

      -- ALGLIB --
         Copyright 26.08.2022 by Sergey Bochkanov
    *************************************************************************/
    public static void bhpaneleval1(biharmonicpanel panel,
        biharmonicevaluator eval,
        double x0,
        double x1,
        double x2,
        ref double f,
        bool neederrbnd,
        ref double errbnd,
        xparams _params)
    {
        int j = 0;
        int n = 0;
        complex vsummn = 0;
        double v = 0;
        double r = 0;
        double r2 = 0;
        double r01 = 0;
        double v0 = 0;
        double v1 = 0;
        double invr = 0;
        double invpowrmplus1 = 0;
        double invpowrnplus1 = 0;
        double invpowrpplus1 = 0;
        double pnmnew = 0;
        double pnm = 0;
        double pnmprev = 0;
        double sintheta = 0;
        double powsinthetaj = 0;
        double costheta = 0;
        complex expiphi = 0;
        complex expijphi = 0;
        complex sphericaly = 0;
        int offs = 0;

        f = 0;
        errbnd = 0;

        ap.assert(panel.ny == 1, "RBF3EVAL1: NY>1");

        //
        // Center evaluation point
        //
        x0 = x0 - panel.c0;
        x1 = x1 - panel.c1;
        x2 = x2 - panel.c2;
        r2 = x0 * x0 + x1 * x1 + x2 * x2 + math.minrealnumber;
        r = Math.Sqrt(r2);

        //
        // Try to use fast kernel.
        // If fast kernel returns False, use reference implementation below.
        //
        if (!bhpaneleval1fastkernel(x0, x1, x2, panel.p, eval.pnma, eval.pnmb, eval.pmmcdiag, eval.ynma, panel.tblrmodmn, ref f, ref invpowrpplus1, _params))
        {

            //
            // No fast kernel.
            //
            // Convert to spherical polar coordinates.
            //
            // NOTE: we make sure that R is non-zero by adding extremely small perturbation
            //
            r01 = Math.Sqrt(x0 * x0 + x1 * x1 + math.minrealnumber);
            costheta = x2 / r;
            sintheta = r01 / r;
            expiphi.x = x0 / r01;
            expiphi.y = x1 / r01;

            //
            // Compute far field expansion for a cluster of basis functions f=r
            //
            // NOTE: the original paper by Beatson et al. uses f=r as the basis function,
            //       whilst ALGLIB uses f=-r due to conditional positive definiteness requirement.
            //       We will perform conversion later.
            //
            powsinthetaj = 1.0;
            f = 0;
            invr = 1 / r;
            invpowrmplus1 = invr;
            expijphi.x = 1.0;
            expijphi.y = 0.0;
            for (j = 0; j <= panel.p; j++)
            {
                invpowrnplus1 = invpowrmplus1;

                //
                // Prepare recursion for associated Legendre polynomials
                //
                pnmprev = 0;
                pnm = powsinthetaj * eval.pmmc[j];

                //
                //
                //
                for (n = j; n <= panel.p; n++)
                {
                    offs = n * panel.stride + j;

                    //
                    // Compute table of associated Legrengre polynomials with
                    //
                    //     P_nm(N=I,M=-J,X)
                    //
                    // being an associated Legendre polynomial, as defined in "Numerical
                    // recipes in C" and Wikipedia.
                    //
                    // It is important that the section 3 of 'Fast evaluation of polyharmonic
                    // splines in three dimensions' by R.K. Beatson, M.J.D. Powell and A.M. Tan
                    // uses different formulation of P_nm. We will account for the difference
                    // during the computation of the spherical harmonics.
                    //
                    if (n > j)
                    {

                        //
                        // Recursion on N
                        //
                        pnmnew = pnm * costheta * eval.pnma[offs] + pnmprev * eval.pnmb[offs];
                        pnmprev = pnm;
                        pnm = pnmnew;
                    }

                    //
                    // Compute table of spherical harmonics where
                    //
                    //     funcSphericalY[I*N+J] = Y_nm(N=I,M=-J),
                    //
                    // with
                    //
                    //     Y_nm(N,M) = E_m*sqrt((n-m)!/(n+m)!)*P_nm(cos(theta))*exp(i*m*phi)
                    //     E_m = m>0 ? pow(-1,m) : 1
                    //     (because we always compute Y_nm for m<=0, E_m is always 1)
                    //
                    // being a spherical harmonic, as defined in the equation (18) of 'Fast evaluation of polyharmonic
                    // splines in three dimensions' by R.K. Beatson, M.J.D. Powell and A.M. Tan.
                    //
                    // Here P_nm is an associated Legendre polynomial as defined by Beatson et al. However, the Pnm
                    // variable stores values of associated Legendre polynomials as defined by Wikipedia. Below we perform conversion
                    // between one format and another one:
                    //
                    //     P_nm(Beatson) = P_nm(Wiki)*(-1)^(-m)*(n+m)!/(n-m)!
                    //     Y_nm(N,M)     = E_m*sqrt((n-m)!/(n+m)!)*P_nm(Beatson)*exp(i*m*phi)
                    //                   = E_m*sqrt((n-m)!/(n+m)!)*P_nm(Wiki)*(-1)^(-m)*(n+m)!/(n-m)!*exp(i*m*phi)
                    //                   = [E_m*(-1)^(-m)*sqrt((n+m)!/(n-m)!)*P_nm(Wiki)]*exp(i*m*phi)
                    //                   = YnmA[n,m]*P_nm(Wiki)*exp(i*m*phi)
                    //
                    v = pnm * eval.ynma[offs];
                    sphericaly.x = v * expijphi.x;
                    sphericaly.y = v * expijphi.y;

                    //
                    // Compute outer function for n=N, m=1..N
                    // Update result with O_mn*(M_mn + R^2*N_mn).
                    //
                    // The most straighforward implementation of the loop below should look like as follows:
                    //
                    //     O_nm  = [sqrt((n-m)!(n+m)!)*i^m]*Y_nm/R^(n+1)
                    //     RES  += 2*RealPart[(R^2*N_nm+M_nm)*O_nm]
                    //
                    // However, we may save a lot of computational effort by moving [sqrt((n-m)!(n+m)!)*i^m]
                    // multiplier to the left part of the product, i.e. by merging it with N_nm and M_nm
                    // and producing MODIFIED expansions NMod and MMod. Because computing this multiplier
                    // involves three lookups into precomputed tables and one complex product, it may
                    // save us a lot of time.
                    //
                    vsummn.x = r2 * panel.tblmodn[offs].x + panel.tblmodm[offs].x;
                    vsummn.y = r2 * panel.tblmodn[offs].y + panel.tblmodm[offs].y;
                    f = f + invpowrnplus1 * (vsummn.x * sphericaly.x - vsummn.y * sphericaly.y);
                    invpowrnplus1 = invpowrnplus1 * invr;
                }

                //
                // Prepare for the next iteration
                //
                powsinthetaj = powsinthetaj * sintheta;
                invpowrmplus1 = invpowrmplus1 * invr;
                v0 = expijphi.x * expiphi.x - expijphi.y * expiphi.y;
                v1 = expijphi.x * expiphi.y + expijphi.y * expiphi.x;
                expijphi.x = v0;
                expijphi.y = v1;
            }
            invpowrpplus1 = r * invpowrmplus1;
        }

        //
        // Convert from f=r to f=-r
        //
        f = -f;

        //
        // Compute error bound
        //
        errbnd = 0.0;
        if (neederrbnd)
        {
            errbnd = panel.maxsumabs * r2 * 2 * panel.tblpowrmax[panel.p + 1] * invpowrpplus1 / ((2 * panel.p + 1) * (r - panel.rmax));
            errbnd = errbnd + 100 * math.machineepsilon * (panel.maxsumabs * r + Math.Abs(f));
        }
    }


    /*************************************************************************
    Tries evaluating model using the far field expansion stored in the panel,
    general case for NY>=1

    INPUT PARAMETERS:
        Panel           -   panel
        Eval            -   precomputed table
        X0, X1, X2      -   evaluation point
        NeedErrBnd      -   whether error bound is needed or not

    OUTPUT PARAMETERS:
        F               -   model value
        ErrBnd          -   upper bound on the far field expansion error, if
                            requested. Zero otherwise.

      -- ALGLIB --
         Copyright 10.11.2022 by Sergey Bochkanov
    *************************************************************************/
    public static void bhpaneleval(biharmonicpanel panel,
        biharmonicevaluator eval,
        double x0,
        double x1,
        double x2,
        ref double[] f,
        bool neederrbnd,
        ref double errbnd,
        xparams _params)
    {
        int j = 0;
        int k = 0;
        int n = 0;
        int ny = 0;
        int stride2 = 0;
        complex vsummn = 0;
        double v = 0;
        double r = 0;
        double r2 = 0;
        double r01 = 0;
        double v0 = 0;
        double v1 = 0;
        double invr = 0;
        double invpowrmplus1 = 0;
        double invpowrnplus1 = 0;
        double pnmnew = 0;
        double pnm = 0;
        double pnmprev = 0;
        double sintheta = 0;
        double powsinthetaj = 0;
        double invpowrpplus1 = 0;
        double costheta = 0;
        complex expiphi = 0;
        complex expijphi = 0;
        complex sphericaly = 0;
        int offs = 0;
        int offsk = 0;
        double af = 0;

        errbnd = 0;

        ny = panel.ny;
        if (ap.len(f) < ny)
        {
            f = new double[ny];
        }

        //
        // Center and convert to spherical polar coordinates.
        //
        // NOTE: we make sure that R is non-zero by adding extremely small perturbation
        //
        x0 = x0 - panel.c0;
        x1 = x1 - panel.c1;
        x2 = x2 - panel.c2;
        r2 = x0 * x0 + x1 * x1 + x2 * x2 + math.minrealnumber;
        r = Math.Sqrt(r2);
        r01 = Math.Sqrt(x0 * x0 + x1 * x1 + math.minrealnumber);
        costheta = x2 / r;
        sintheta = r01 / r;
        expiphi.x = x0 / r01;
        expiphi.y = x1 / r01;

        //
        // Try to use fast kernel.
        // If fast kernel returns False, use reference implementation below.
        //
        if (!bhpanelevalfastkernel(x0, x1, x2, ny, panel.p, eval.pnma, eval.pnmb, eval.pmmcdiag, eval.ynma, panel.tblrmodmn, f, ref invpowrpplus1, _params))
        {

            //
            // No fast kernel.
            //
            // Compute far field expansion for a cluster of basis functions f=r
            //
            // NOTE: the original paper by Beatson et al. uses f=r as the basis function,
            //       whilst ALGLIB uses f=-r due to conditional positive definiteness requirement.
            //       We will perform conversion later.
            //
            powsinthetaj = 1.0;
            for (k = 0; k <= ny - 1; k++)
            {
                f[k] = 0;
            }
            invr = 1 / r;
            invpowrmplus1 = invr;
            expijphi.x = 1.0;
            expijphi.y = 0.0;
            stride2 = panel.stride * panel.stride;
            for (j = 0; j <= panel.p; j++)
            {
                invpowrnplus1 = invpowrmplus1;

                //
                // Prepare recursion for associated Legendre polynomials
                //
                pnmprev = 0;
                pnm = powsinthetaj * eval.pmmc[j];

                //
                //
                //
                for (n = j; n <= panel.p; n++)
                {
                    offs = n * panel.stride + j;

                    //
                    // Compute table of associated Legrengre polynomials with
                    //
                    //     P_nm(N=I,M=-J,X)
                    //
                    // being an associated Legendre polynomial, as defined in "Numerical
                    // recipes in C" and Wikipedia.
                    //
                    // It is important that the section 3 of 'Fast evaluation of polyharmonic
                    // splines in three dimensions' by R.K. Beatson, M.J.D. Powell and A.M. Tan
                    // uses different formulation of P_nm. We will account for the difference
                    // during the computation of the spherical harmonics.
                    //
                    if (n > j)
                    {

                        //
                        // Recursion on N
                        //
                        pnmnew = pnm * costheta * eval.pnma[offs] + pnmprev * eval.pnmb[offs];
                        pnmprev = pnm;
                        pnm = pnmnew;
                    }

                    //
                    // Compute table of spherical harmonics where
                    //
                    //     funcSphericalY[I*N+J] = Y_nm(N=I,M=-J),
                    //
                    // with
                    //
                    //     Y_nm(N,M) = E_m*sqrt((n-m)!/(n+m)!)*P_nm(cos(theta))*exp(i*m*phi)
                    //     E_m = m>0 ? pow(-1,m) : 1
                    //     (because we always compute Y_nm for m<=0, E_m is always 1)
                    //
                    // being a spherical harmonic, as defined in the equation (18) of 'Fast evaluation of polyharmonic
                    // splines in three dimensions' by R.K. Beatson, M.J.D. Powell and A.M. Tan.
                    //
                    // Here P_nm is an associated Legendre polynomial as defined by Beatson et al. However, the Pnm
                    // variable stores values of associated Legendre polynomials as defined by Wikipedia. Below we perform conversion
                    // between one format and another one:
                    //
                    //     P_nm(Beatson) = P_nm(Wiki)*(-1)^(-m)*(n+m)!/(n-m)!
                    //     Y_nm(N,M)     = E_m*sqrt((n-m)!/(n+m)!)*P_nm(Beatson)*exp(i*m*phi)
                    //                   = E_m*sqrt((n-m)!/(n+m)!)*P_nm(Wiki)*(-1)^(-m)*(n+m)!/(n-m)!*exp(i*m*phi)
                    //                   = [E_m*(-1)^(-m)*sqrt((n+m)!/(n-m)!)*P_nm(Wiki)]*exp(i*m*phi)
                    //                   = YnmA[n,m]*P_nm(Wiki)*exp(i*m*phi)
                    //
                    v = pnm * eval.ynma[offs];
                    sphericaly.x = v * expijphi.x;
                    sphericaly.y = v * expijphi.y;

                    //
                    // Compute outer function for n=N, m=1..N
                    // Update result with O_mn*(M_mn + R^2*N_mn).
                    //
                    // The most straighforward implementation of the loop below should look like as follows:
                    //
                    //     O_nm  = [sqrt((n-m)!(n+m)!)*i^m]*Y_nm/R^(n+1)
                    //     RES  += 2*RealPart[(R^2*N_nm+M_nm)*O_nm]
                    //
                    // However, we may save a lot of computational effort by moving [sqrt((n-m)!(n+m)!)*i^m]
                    // multiplier to the left part of the product, i.e. by merging it with N_nm and M_nm
                    // and producing MODIFIED expansions NMod and MMod. Because computing this multiplier
                    // involves three lookups into precomputed tables and one complex product, it may
                    // save us a lot of time.
                    //
                    offsk = offs;
                    for (k = 0; k <= ny - 1; k++)
                    {
                        vsummn.x = r2 * panel.tblmodn[offsk].x + panel.tblmodm[offsk].x;
                        vsummn.y = r2 * panel.tblmodn[offsk].y + panel.tblmodm[offsk].y;
                        f[k] = f[k] + invpowrnplus1 * (vsummn.x * sphericaly.x - vsummn.y * sphericaly.y);
                        offsk = offsk + stride2;
                    }
                    invpowrnplus1 = invpowrnplus1 * invr;
                }

                //
                // Prepare for the next iteration
                //
                powsinthetaj = powsinthetaj * sintheta;
                invpowrmplus1 = invpowrmplus1 * invr;
                v0 = expijphi.x * expiphi.x - expijphi.y * expiphi.y;
                v1 = expijphi.x * expiphi.y + expijphi.y * expiphi.x;
                expijphi.x = v0;
                expijphi.y = v1;
            }
            invpowrpplus1 = r * invpowrmplus1;
        }

        //
        // Convert from f=r to f=-r
        //
        for (k = 0; k <= ny - 1; k++)
        {
            f[k] = -f[k];
        }

        //
        // Compute error bound, if needed
        //
        errbnd = 0.0;
        if (neederrbnd)
        {
            af = 0;
            for (k = 0; k <= ny - 1; k++)
            {
                af = Math.Max(af, Math.Abs(f[k]));
            }
            errbnd = panel.maxsumabs * r2 * 2 * panel.tblpowrmax[panel.p + 1] * invpowrpplus1 / ((2 * panel.p + 1) * (r - panel.rmax));
            errbnd = errbnd + 100 * math.machineepsilon * (panel.maxsumabs * r + af);
        }
    }


#if ALGLIB_NO_FAST_KERNELS
    /*************************************************************************
    Fast kernel for biharmonic panel with NY=1

    INPUT PARAMETERS:
        D0, D1, D2      -   evaluation point minus (Panel.C0,Panel.C1,Panel.C2)

    OUTPUT PARAMETERS:
        F               -   model value
        InvPowRPPlus1   -   1/(R^(P+1))

      -- ALGLIB --
         Copyright 26.08.2022 by Sergey Bochkanov
    *************************************************************************/
    private static bool bhpaneleval1fastkernel(double d0,
        double d1,
        double d2,
        int panelp,
        double[] pnma,
        double[] pnmb,
        double[] pmmcdiag,
        double[] ynma,
        double[] tblrmodmn,
        ref double f,
        ref double invpowrpplus1,
        xparams _params)
    {
        bool result = new bool();

        f = 0;
        invpowrpplus1 = 0;

        f = 0;
        invpowrpplus1 = 0;
        result = false;
        return result;
    }
#endif


#if ALGLIB_NO_FAST_KERNELS
    /*************************************************************************
    Fast kernel for biharmonic panel with general NY

    INPUT PARAMETERS:
        D0, D1, D2      -   evaluation point minus (Panel.C0,Panel.C1,Panel.C2)

    OUTPUT PARAMETERS:
        F               -   model value
        InvPowRPPlus1   -   1/(R^(P+1))

      -- ALGLIB --
         Copyright 26.08.2022 by Sergey Bochkanov
    *************************************************************************/
    private static bool bhpanelevalfastkernel(double d0,
        double d1,
        double d2,
        int ny,
        int panelp,
        double[] pnma,
        double[] pnmb,
        double[] pmmcdiag,
        double[] ynma,
        double[] tblrmodmn,
        double[] f,
        ref double invpowrpplus1,
        xparams _params)
    {
        bool result = new bool();

        invpowrpplus1 = 0;

        invpowrpplus1 = 0;
        result = false;
        return result;
    }
#endif

}
