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

public class spline3d
{
    /*************************************************************************
    3-dimensional spline inteprolant
    *************************************************************************/
    public class spline3dinterpolant : apobject
    {
        public int k;
        public int stype;
        public int n;
        public int m;
        public int l;
        public int d;
        public double[] x;
        public double[] y;
        public double[] z;
        public double[] f;
        public spline3dinterpolant()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            y = new double[0];
            z = new double[0];
            f = new double[0];
        }
        public override apobject make_copy()
        {
            spline3dinterpolant _result = new spline3dinterpolant();
            _result.k = k;
            _result.stype = stype;
            _result.n = n;
            _result.m = m;
            _result.l = l;
            _result.d = d;
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.z = (double[])z.Clone();
            _result.f = (double[])f.Clone();
            return _result;
        }
    };




    /*************************************************************************
    This subroutine calculates the value of the trilinear or tricubic spline at
    the given point (X,Y,Z).

    INPUT PARAMETERS:
        C   -   coefficients table.
                Built by BuildBilinearSpline or BuildBicubicSpline.
        X, Y,
        Z   -   point

    Result:
        S(x,y,z)

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double spline3dcalc(spline3dinterpolant c,
        double x,
        double y,
        double z,
        xparams _params)
    {
        double result = 0;
        double v = 0;
        double vx = 0;
        double vy = 0;
        double vxy = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline3DCalc: incorrect C (incorrect parameter C.SType)");
        ap.assert((math.isfinite(x) && math.isfinite(y)) && math.isfinite(z), "Spline3DCalc: X=NaN/Infinite, Y=NaN/Infinite or Z=NaN/Infinite");
        if (c.d != 1)
        {
            result = 0;
            return result;
        }
        spline3ddiff(c, x, y, z, ref v, ref vx, ref vy, ref vxy, _params);
        result = v;
        return result;
    }


    /*************************************************************************
    This subroutine performs linear transformation of the spline argument.

    INPUT PARAMETERS:
        C       -   spline interpolant
        AX, BX  -   transformation coefficients: x = A*u + B
        AY, BY  -   transformation coefficients: y = A*v + B
        AZ, BZ  -   transformation coefficients: z = A*w + B
        
    OUTPUT PARAMETERS:
        C   -   transformed spline

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dlintransxyz(spline3dinterpolant c,
        double ax,
        double bx,
        double ay,
        double by,
        double az,
        double bz,
        xparams _params)
    {
        double[] x = new double[0];
        double[] y = new double[0];
        double[] z = new double[0];
        double[] f = new double[0];
        double[] v = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int di = 0;
        int i_ = 0;

        ap.assert(c.stype == -3 || c.stype == -1, "Spline3DLinTransXYZ: incorrect C (incorrect parameter C.SType)");
        x = new double[c.n];
        y = new double[c.m];
        z = new double[c.l];
        f = new double[c.m * c.n * c.l * c.d];
        for (j = 0; j <= c.n - 1; j++)
        {
            x[j] = c.x[j];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            y[i] = c.y[i];
        }
        for (i = 0; i <= c.l - 1; i++)
        {
            z[i] = c.z[i];
        }

        //
        // Handle different combinations of zero/nonzero AX/AY/AZ
        //
        if (((double)(ax) != (double)(0) && (double)(ay) != (double)(0)) && (double)(az) != (double)(0))
        {
            for (i_ = 0; i_ <= c.m * c.n * c.l * c.d - 1; i_++)
            {
                f[i_] = c.f[i_];
            }
        }
        if (((double)(ax) == (double)(0) && (double)(ay) != (double)(0)) && (double)(az) != (double)(0))
        {
            for (i = 0; i <= c.m - 1; i++)
            {
                for (j = 0; j <= c.l - 1; j++)
                {
                    spline3dcalcv(c, bx, y[i], z[j], ref v, _params);
                    for (k = 0; k <= c.n - 1; k++)
                    {
                        for (di = 0; di <= c.d - 1; di++)
                        {
                            f[c.d * (c.n * (c.m * j + i) + k) + di] = v[di];
                        }
                    }
                }
            }
            ax = 1;
            bx = 0;
        }
        if (((double)(ax) != (double)(0) && (double)(ay) == (double)(0)) && (double)(az) != (double)(0))
        {
            for (i = 0; i <= c.n - 1; i++)
            {
                for (j = 0; j <= c.l - 1; j++)
                {
                    spline3dcalcv(c, x[i], by, z[j], ref v, _params);
                    for (k = 0; k <= c.m - 1; k++)
                    {
                        for (di = 0; di <= c.d - 1; di++)
                        {
                            f[c.d * (c.n * (c.m * j + k) + i) + di] = v[di];
                        }
                    }
                }
            }
            ay = 1;
            by = 0;
        }
        if (((double)(ax) != (double)(0) && (double)(ay) != (double)(0)) && (double)(az) == (double)(0))
        {
            for (i = 0; i <= c.n - 1; i++)
            {
                for (j = 0; j <= c.m - 1; j++)
                {
                    spline3dcalcv(c, x[i], y[j], bz, ref v, _params);
                    for (k = 0; k <= c.l - 1; k++)
                    {
                        for (di = 0; di <= c.d - 1; di++)
                        {
                            f[c.d * (c.n * (c.m * k + j) + i) + di] = v[di];
                        }
                    }
                }
            }
            az = 1;
            bz = 0;
        }
        if (((double)(ax) == (double)(0) && (double)(ay) == (double)(0)) && (double)(az) != (double)(0))
        {
            for (i = 0; i <= c.l - 1; i++)
            {
                spline3dcalcv(c, bx, by, z[i], ref v, _params);
                for (k = 0; k <= c.m - 1; k++)
                {
                    for (j = 0; j <= c.n - 1; j++)
                    {
                        for (di = 0; di <= c.d - 1; di++)
                        {
                            f[c.d * (c.n * (c.m * i + k) + j) + di] = v[di];
                        }
                    }
                }
            }
            ax = 1;
            bx = 0;
            ay = 1;
            by = 0;
        }
        if (((double)(ax) == (double)(0) && (double)(ay) != (double)(0)) && (double)(az) == (double)(0))
        {
            for (i = 0; i <= c.m - 1; i++)
            {
                spline3dcalcv(c, bx, y[i], bz, ref v, _params);
                for (k = 0; k <= c.l - 1; k++)
                {
                    for (j = 0; j <= c.n - 1; j++)
                    {
                        for (di = 0; di <= c.d - 1; di++)
                        {
                            f[c.d * (c.n * (c.m * k + i) + j) + di] = v[di];
                        }
                    }
                }
            }
            ax = 1;
            bx = 0;
            az = 1;
            bz = 0;
        }
        if (((double)(ax) != (double)(0) && (double)(ay) == (double)(0)) && (double)(az) == (double)(0))
        {
            for (i = 0; i <= c.n - 1; i++)
            {
                spline3dcalcv(c, x[i], by, bz, ref v, _params);
                for (k = 0; k <= c.l - 1; k++)
                {
                    for (j = 0; j <= c.m - 1; j++)
                    {
                        for (di = 0; di <= c.d - 1; di++)
                        {
                            f[c.d * (c.n * (c.m * k + j) + i) + di] = v[di];
                        }
                    }
                }
            }
            ay = 1;
            by = 0;
            az = 1;
            bz = 0;
        }
        if (((double)(ax) == (double)(0) && (double)(ay) == (double)(0)) && (double)(az) == (double)(0))
        {
            spline3dcalcv(c, bx, by, bz, ref v, _params);
            for (k = 0; k <= c.l - 1; k++)
            {
                for (j = 0; j <= c.m - 1; j++)
                {
                    for (i = 0; i <= c.n - 1; i++)
                    {
                        for (di = 0; di <= c.d - 1; di++)
                        {
                            f[c.d * (c.n * (c.m * k + j) + i) + di] = v[di];
                        }
                    }
                }
            }
            ax = 1;
            bx = 0;
            ay = 1;
            by = 0;
            az = 1;
            bz = 0;
        }

        //
        // General case: AX<>0, AY<>0, AZ<>0
        // Unpack, scale and pack again.
        //
        for (i = 0; i <= c.n - 1; i++)
        {
            x[i] = (x[i] - bx) / ax;
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            y[i] = (y[i] - by) / ay;
        }
        for (i = 0; i <= c.l - 1; i++)
        {
            z[i] = (z[i] - bz) / az;
        }
        if (c.stype == -1)
        {
            spline3dbuildtrilinearvbuf(x, c.n, y, c.m, z, c.l, f, c.d, c, _params);
        }
    }


    /*************************************************************************
    This subroutine performs linear transformation of the spline.

    INPUT PARAMETERS:
        C   -   spline interpolant.
        A, B-   transformation coefficients: S2(x,y) = A*S(x,y,z) + B
        
    OUTPUT PARAMETERS:
        C   -   transformed spline

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dlintransf(spline3dinterpolant c,
        double a,
        double b,
        xparams _params)
    {
        double[] x = new double[0];
        double[] y = new double[0];
        double[] z = new double[0];
        double[] f = new double[0];
        int i = 0;
        int j = 0;

        ap.assert(c.stype == -3 || c.stype == -1, "Spline3DLinTransF: incorrect C (incorrect parameter C.SType)");
        x = new double[c.n];
        y = new double[c.m];
        z = new double[c.l];
        f = new double[c.m * c.n * c.l * c.d];
        for (j = 0; j <= c.n - 1; j++)
        {
            x[j] = c.x[j];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            y[i] = c.y[i];
        }
        for (i = 0; i <= c.l - 1; i++)
        {
            z[i] = c.z[i];
        }
        for (i = 0; i <= c.m * c.n * c.l * c.d - 1; i++)
        {
            f[i] = a * c.f[i] + b;
        }
        if (c.stype == -1)
        {
            spline3dbuildtrilinearvbuf(x, c.n, y, c.m, z, c.l, f, c.d, c, _params);
        }
    }


    /*************************************************************************
    This subroutine makes the copy of the spline model.

    INPUT PARAMETERS:
        C   -   spline interpolant

    OUTPUT PARAMETERS:
        CC  -   spline copy

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dcopy(spline3dinterpolant c,
        spline3dinterpolant cc,
        xparams _params)
    {
        int tblsize = 0;
        int i_ = 0;

        ap.assert(c.k == 1 || c.k == 3, "Spline3DCopy: incorrect C (incorrect parameter C.K)");
        cc.k = c.k;
        cc.n = c.n;
        cc.m = c.m;
        cc.l = c.l;
        cc.d = c.d;
        tblsize = c.n * c.m * c.l * c.d;
        cc.stype = c.stype;
        cc.x = new double[cc.n];
        cc.y = new double[cc.m];
        cc.z = new double[cc.l];
        cc.f = new double[tblsize];
        for (i_ = 0; i_ <= cc.n - 1; i_++)
        {
            cc.x[i_] = c.x[i_];
        }
        for (i_ = 0; i_ <= cc.m - 1; i_++)
        {
            cc.y[i_] = c.y[i_];
        }
        for (i_ = 0; i_ <= cc.l - 1; i_++)
        {
            cc.z[i_] = c.z[i_];
        }
        for (i_ = 0; i_ <= tblsize - 1; i_++)
        {
            cc.f[i_] = c.f[i_];
        }
    }


    /*************************************************************************
    Trilinear spline resampling

    INPUT PARAMETERS:
        A           -   array[0..OldXCount*OldYCount*OldZCount-1], function
                        values at the old grid, :
                            A[0]        x=0,y=0,z=0
                            A[1]        x=1,y=0,z=0
                            A[..]       ...
                            A[..]       x=oldxcount-1,y=0,z=0
                            A[..]       x=0,y=1,z=0
                            A[..]       ...
                            ...
        OldZCount   -   old Z-count, OldZCount>1
        OldYCount   -   old Y-count, OldYCount>1
        OldXCount   -   old X-count, OldXCount>1
        NewZCount   -   new Z-count, NewZCount>1
        NewYCount   -   new Y-count, NewYCount>1
        NewXCount   -   new X-count, NewXCount>1

    OUTPUT PARAMETERS:
        B           -   array[0..NewXCount*NewYCount*NewZCount-1], function
                        values at the new grid:
                            B[0]        x=0,y=0,z=0
                            B[1]        x=1,y=0,z=0
                            B[..]       ...
                            B[..]       x=newxcount-1,y=0,z=0
                            B[..]       x=0,y=1,z=0
                            B[..]       ...
                            ...

      -- ALGLIB routine --
         26.04.2012
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dresampletrilinear(double[] a,
        int oldzcount,
        int oldycount,
        int oldxcount,
        int newzcount,
        int newycount,
        int newxcount,
        ref double[] b,
        xparams _params)
    {
        double xd = 0;
        double yd = 0;
        double zd = 0;
        double c0 = 0;
        double c1 = 0;
        double c2 = 0;
        double c3 = 0;
        int ix = 0;
        int iy = 0;
        int iz = 0;
        int i = 0;
        int j = 0;
        int k = 0;

        b = new double[0];

        ap.assert((oldycount > 1 && oldzcount > 1) && oldxcount > 1, "Spline3DResampleTrilinear: length/width/height less than 1");
        ap.assert((newycount > 1 && newzcount > 1) && newxcount > 1, "Spline3DResampleTrilinear: length/width/height less than 1");
        ap.assert(ap.len(a) >= oldycount * oldzcount * oldxcount, "Spline3DResampleTrilinear: length/width/height less than 1");
        b = new double[newxcount * newycount * newzcount];
        for (i = 0; i <= newxcount - 1; i++)
        {
            for (j = 0; j <= newycount - 1; j++)
            {
                for (k = 0; k <= newzcount - 1; k++)
                {
                    ix = i * (oldxcount - 1) / (newxcount - 1);
                    if (ix == oldxcount - 1)
                    {
                        ix = oldxcount - 2;
                    }
                    xd = (double)(i * (oldxcount - 1)) / (double)(newxcount - 1) - ix;
                    iy = j * (oldycount - 1) / (newycount - 1);
                    if (iy == oldycount - 1)
                    {
                        iy = oldycount - 2;
                    }
                    yd = (double)(j * (oldycount - 1)) / (double)(newycount - 1) - iy;
                    iz = k * (oldzcount - 1) / (newzcount - 1);
                    if (iz == oldzcount - 1)
                    {
                        iz = oldzcount - 2;
                    }
                    zd = (double)(k * (oldzcount - 1)) / (double)(newzcount - 1) - iz;
                    c0 = a[oldxcount * (oldycount * iz + iy) + ix] * (1 - xd) + a[oldxcount * (oldycount * iz + iy) + (ix + 1)] * xd;
                    c1 = a[oldxcount * (oldycount * iz + (iy + 1)) + ix] * (1 - xd) + a[oldxcount * (oldycount * iz + (iy + 1)) + (ix + 1)] * xd;
                    c2 = a[oldxcount * (oldycount * (iz + 1) + iy) + ix] * (1 - xd) + a[oldxcount * (oldycount * (iz + 1) + iy) + (ix + 1)] * xd;
                    c3 = a[oldxcount * (oldycount * (iz + 1) + (iy + 1)) + ix] * (1 - xd) + a[oldxcount * (oldycount * (iz + 1) + (iy + 1)) + (ix + 1)] * xd;
                    c0 = c0 * (1 - yd) + c1 * yd;
                    c1 = c2 * (1 - yd) + c3 * yd;
                    b[newxcount * (newycount * k + j) + i] = c0 * (1 - zd) + c1 * zd;
                }
            }
        }
    }


    /*************************************************************************
    This subroutine builds trilinear vector-valued spline.

    INPUT PARAMETERS:
        X   -   spline abscissas,  array[0..N-1]
        Y   -   spline ordinates,  array[0..M-1]
        Z   -   spline applicates, array[0..L-1] 
        F   -   function values, array[0..M*N*L*D-1]:
                * first D elements store D values at (X[0],Y[0],Z[0])
                * next D elements store D values at (X[1],Y[0],Z[0])
                * next D elements store D values at (X[2],Y[0],Z[0])
                * ...
                * next D elements store D values at (X[0],Y[1],Z[0])
                * next D elements store D values at (X[1],Y[1],Z[0])
                * next D elements store D values at (X[2],Y[1],Z[0])
                * ...
                * next D elements store D values at (X[0],Y[0],Z[1])
                * next D elements store D values at (X[1],Y[0],Z[1])
                * next D elements store D values at (X[2],Y[0],Z[1])
                * ...
                * general form - D function values at (X[i],Y[j]) are stored
                  at F[D*(N*(M*K+J)+I)...D*(N*(M*K+J)+I)+D-1].
        M,N,
        L   -   grid size, M>=2, N>=2, L>=2
        D   -   vector dimension, D>=1

    OUTPUT PARAMETERS:
        C   -   spline interpolant

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dbuildtrilinearv(double[] x,
        int n,
        double[] y,
        int m,
        double[] z,
        int l,
        double[] f,
        int d,
        spline3dinterpolant c,
        xparams _params)
    {
        spline3dbuildtrilinearvbuf(x, n, y, m, z, l, f, d, c, _params);
    }


    /*************************************************************************
    This subroutine builds trilinear vector-valued spline.

    Buffered  version   of   Spline3DBuildTrilinearV()  which  reuses   memory
    previously allocated in C as much as possible.

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dbuildtrilinearvbuf(double[] x,
        int n,
        double[] y,
        int m,
        double[] z,
        int l,
        double[] f,
        int d,
        spline3dinterpolant c,
        xparams _params)
    {
        double t = 0;
        int tblsize = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int i0 = 0;
        int j0 = 0;

        ap.assert(m >= 2, "Spline3DBuildTrilinearV: M<2");
        ap.assert(n >= 2, "Spline3DBuildTrilinearV: N<2");
        ap.assert(l >= 2, "Spline3DBuildTrilinearV: L<2");
        ap.assert(d >= 1, "Spline3DBuildTrilinearV: D<1");
        ap.assert((ap.len(x) >= n && ap.len(y) >= m) && ap.len(z) >= l, "Spline3DBuildTrilinearV: length of X, Y or Z is too short (Length(X/Y/Z)<N/M/L)");
        ap.assert((apserv.isfinitevector(x, n, _params) && apserv.isfinitevector(y, m, _params)) && apserv.isfinitevector(z, l, _params), "Spline3DBuildTrilinearV: X, Y or Z contains NaN or Infinite value");
        tblsize = n * m * l * d;
        ap.assert(ap.len(f) >= tblsize, "Spline3DBuildTrilinearV: length of F is too short (Length(F)<N*M*L*D)");
        ap.assert(apserv.isfinitevector(f, tblsize, _params), "Spline3DBuildTrilinearV: F contains NaN or Infinite value");

        //
        // Fill interpolant
        //
        c.k = 1;
        c.n = n;
        c.m = m;
        c.l = l;
        c.d = d;
        c.stype = -1;
        c.x = new double[c.n];
        c.y = new double[c.m];
        c.z = new double[c.l];
        c.f = new double[tblsize];
        for (i = 0; i <= c.n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            c.y[i] = y[i];
        }
        for (i = 0; i <= c.l - 1; i++)
        {
            c.z[i] = z[i];
        }
        for (i = 0; i <= tblsize - 1; i++)
        {
            c.f[i] = f[i];
        }

        //
        // Sort points:
        //  * sort x;
        //  * sort y;
        //  * sort z.
        //
        for (j = 0; j <= c.n - 1; j++)
        {
            k = j;
            for (i = j + 1; i <= c.n - 1; i++)
            {
                if ((double)(c.x[i]) < (double)(c.x[k]))
                {
                    k = i;
                }
            }
            if (k != j)
            {
                for (i = 0; i <= c.m - 1; i++)
                {
                    for (j0 = 0; j0 <= c.l - 1; j0++)
                    {
                        for (i0 = 0; i0 <= c.d - 1; i0++)
                        {
                            t = c.f[c.d * (c.n * (c.m * j0 + i) + j) + i0];
                            c.f[c.d * (c.n * (c.m * j0 + i) + j) + i0] = c.f[c.d * (c.n * (c.m * j0 + i) + k) + i0];
                            c.f[c.d * (c.n * (c.m * j0 + i) + k) + i0] = t;
                        }
                    }
                }
                t = c.x[j];
                c.x[j] = c.x[k];
                c.x[k] = t;
            }
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            k = i;
            for (j = i + 1; j <= c.m - 1; j++)
            {
                if ((double)(c.y[j]) < (double)(c.y[k]))
                {
                    k = j;
                }
            }
            if (k != i)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    for (j0 = 0; j0 <= c.l - 1; j0++)
                    {
                        for (i0 = 0; i0 <= c.d - 1; i0++)
                        {
                            t = c.f[c.d * (c.n * (c.m * j0 + i) + j) + i0];
                            c.f[c.d * (c.n * (c.m * j0 + i) + j) + i0] = c.f[c.d * (c.n * (c.m * j0 + k) + j) + i0];
                            c.f[c.d * (c.n * (c.m * j0 + k) + j) + i0] = t;
                        }
                    }
                }
                t = c.y[i];
                c.y[i] = c.y[k];
                c.y[k] = t;
            }
        }
        for (k = 0; k <= c.l - 1; k++)
        {
            i = k;
            for (j = i + 1; j <= c.l - 1; j++)
            {
                if ((double)(c.z[j]) < (double)(c.z[i]))
                {
                    i = j;
                }
            }
            if (i != k)
            {
                for (j = 0; j <= c.m - 1; j++)
                {
                    for (j0 = 0; j0 <= c.n - 1; j0++)
                    {
                        for (i0 = 0; i0 <= c.d - 1; i0++)
                        {
                            t = c.f[c.d * (c.n * (c.m * k + j) + j0) + i0];
                            c.f[c.d * (c.n * (c.m * k + j) + j0) + i0] = c.f[c.d * (c.n * (c.m * i + j) + j0) + i0];
                            c.f[c.d * (c.n * (c.m * i + j) + j0) + i0] = t;
                        }
                    }
                }
                t = c.z[k];
                c.z[k] = c.z[i];
                c.z[i] = t;
            }
        }
    }


    /*************************************************************************
    This subroutine calculates bilinear or bicubic vector-valued spline at the
    given point (X,Y,Z).

    INPUT PARAMETERS:
        C   -   spline interpolant.
        X, Y,
        Z   -   point
        F   -   output buffer, possibly preallocated array. In case array size
                is large enough to store result, it is not reallocated.  Array
                which is too short will be reallocated

    OUTPUT PARAMETERS:
        F   -   array[D] (or larger) which stores function values

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dcalcvbuf(spline3dinterpolant c,
        double x,
        double y,
        double z,
        ref double[] f,
        xparams _params)
    {
        double xd = 0;
        double yd = 0;
        double zd = 0;
        double c0 = 0;
        double c1 = 0;
        double c2 = 0;
        double c3 = 0;
        int ix = 0;
        int iy = 0;
        int iz = 0;
        int l = 0;
        int r = 0;
        int h = 0;
        int i = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline3DCalcVBuf: incorrect C (incorrect parameter C.SType)");
        ap.assert((math.isfinite(x) && math.isfinite(y)) && math.isfinite(z), "Spline3DCalcVBuf: X, Y or Z contains NaN/Infinite");
        apserv.rvectorsetlengthatleast(ref f, c.d, _params);

        //
        // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        ix = l;

        //
        // Binary search in the [ y[0], ..., y[n-2] ] (y[n-1] is not included)
        //
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        iy = l;

        //
        // Binary search in the [ z[0], ..., z[n-2] ] (z[n-1] is not included)
        //
        l = 0;
        r = c.l - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.z[h]) >= (double)(z))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        iz = l;
        xd = (x - c.x[ix]) / (c.x[ix + 1] - c.x[ix]);
        yd = (y - c.y[iy]) / (c.y[iy + 1] - c.y[iy]);
        zd = (z - c.z[iz]) / (c.z[iz + 1] - c.z[iz]);
        for (i = 0; i <= c.d - 1; i++)
        {

            //
            // Trilinear interpolation
            //
            if (c.stype == -1)
            {
                c0 = c.f[c.d * (c.n * (c.m * iz + iy) + ix) + i] * (1 - xd) + c.f[c.d * (c.n * (c.m * iz + iy) + (ix + 1)) + i] * xd;
                c1 = c.f[c.d * (c.n * (c.m * iz + (iy + 1)) + ix) + i] * (1 - xd) + c.f[c.d * (c.n * (c.m * iz + (iy + 1)) + (ix + 1)) + i] * xd;
                c2 = c.f[c.d * (c.n * (c.m * (iz + 1) + iy) + ix) + i] * (1 - xd) + c.f[c.d * (c.n * (c.m * (iz + 1) + iy) + (ix + 1)) + i] * xd;
                c3 = c.f[c.d * (c.n * (c.m * (iz + 1) + (iy + 1)) + ix) + i] * (1 - xd) + c.f[c.d * (c.n * (c.m * (iz + 1) + (iy + 1)) + (ix + 1)) + i] * xd;
                c0 = c0 * (1 - yd) + c1 * yd;
                c1 = c2 * (1 - yd) + c3 * yd;
                f[i] = c0 * (1 - zd) + c1 * zd;
            }
        }
    }


    /*************************************************************************
    This subroutine calculates trilinear or tricubic vector-valued spline at the
    given point (X,Y,Z).

    INPUT PARAMETERS:
        C   -   spline interpolant.
        X, Y,
        Z   -   point

    OUTPUT PARAMETERS:
        F   -   array[D] which stores function values.  F is out-parameter and
                it  is  reallocated  after  call to this function. In case you
                want  to    reuse  previously  allocated  F,   you   may   use
                Spline2DCalcVBuf(),  which  reallocates  F only when it is too
                small.

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dcalcv(spline3dinterpolant c,
        double x,
        double y,
        double z,
        ref double[] f,
        xparams _params)
    {
        f = new double[0];

        ap.assert(c.stype == -1 || c.stype == -3, "Spline3DCalcV: incorrect C (incorrect parameter C.SType)");
        ap.assert((math.isfinite(x) && math.isfinite(y)) && math.isfinite(z), "Spline3DCalcV: X=NaN/Infinite, Y=NaN/Infinite or Z=NaN/Infinite");
        f = new double[c.d];
        spline3dcalcvbuf(c, x, y, z, ref f, _params);
    }


    /*************************************************************************
    This subroutine unpacks tri-dimensional spline into the coefficients table

    INPUT PARAMETERS:
        C   -   spline interpolant.

    Result:
        N   -   grid size (X)
        M   -   grid size (Y)
        L   -   grid size (Z)
        D   -   number of components
        SType-  spline type. Currently, only one spline type is supported:
                trilinear spline, as indicated by SType=1.
        Tbl -   spline coefficients: [0..(N-1)*(M-1)*(L-1)*D-1, 0..13].
                For T=0..D-1 (component index), I = 0...N-2 (x index),
                J=0..M-2 (y index), K=0..L-2 (z index):
                    Q := T + I*D + J*D*(N-1) + K*D*(N-1)*(M-1),
                    
                    Q-th row stores decomposition for T-th component of the
                    vector-valued function
                    
                    Tbl[Q,0] = X[i]
                    Tbl[Q,1] = X[i+1]
                    Tbl[Q,2] = Y[j]
                    Tbl[Q,3] = Y[j+1]
                    Tbl[Q,4] = Z[k]
                    Tbl[Q,5] = Z[k+1]
                    
                    Tbl[Q,6] = C000
                    Tbl[Q,7] = C100
                    Tbl[Q,8] = C010
                    Tbl[Q,9] = C110
                    Tbl[Q,10]= C001
                    Tbl[Q,11]= C101
                    Tbl[Q,12]= C011
                    Tbl[Q,13]= C111
                On each grid square spline is equals to:
                    S(x) = SUM(c[i,j,k]*(x^i)*(y^j)*(z^k), i=0..1, j=0..1, k=0..1)
                    t = x-x[j]
                    u = y-y[i]
                    v = z-z[k]
                
                NOTE: format of Tbl is given for SType=1. Future versions of
                      ALGLIB can use different formats for different values of
                      SType.

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline3dunpackv(spline3dinterpolant c,
        ref int n,
        ref int m,
        ref int l,
        ref int d,
        ref int stype,
        ref double[,] tbl,
        xparams _params)
    {
        int p = 0;
        int ci = 0;
        int cj = 0;
        int ck = 0;
        double du = 0;
        double dv = 0;
        double dw = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int di = 0;
        int i0 = 0;

        n = 0;
        m = 0;
        l = 0;
        d = 0;
        stype = 0;
        tbl = new double[0, 0];

        ap.assert(c.stype == -1, "Spline3DUnpackV: incorrect C (incorrect parameter C.SType)");
        n = c.n;
        m = c.m;
        l = c.l;
        d = c.d;
        stype = Math.Abs(c.stype);
        tbl = new double[(n - 1) * (m - 1) * (l - 1) * d, 14];

        //
        // Fill
        //
        for (i = 0; i <= n - 2; i++)
        {
            for (j = 0; j <= m - 2; j++)
            {
                for (k = 0; k <= l - 2; k++)
                {
                    for (di = 0; di <= d - 1; di++)
                    {
                        p = d * ((n - 1) * ((m - 1) * k + j) + i) + di;
                        tbl[p, 0] = c.x[i];
                        tbl[p, 1] = c.x[i + 1];
                        tbl[p, 2] = c.y[j];
                        tbl[p, 3] = c.y[j + 1];
                        tbl[p, 4] = c.z[k];
                        tbl[p, 5] = c.z[k + 1];
                        du = 1 / (tbl[p, 1] - tbl[p, 0]);
                        dv = 1 / (tbl[p, 3] - tbl[p, 2]);
                        dw = 1 / (tbl[p, 5] - tbl[p, 4]);

                        //
                        // Trilinear interpolation
                        //
                        if (c.stype == -1)
                        {
                            for (i0 = 6; i0 <= 13; i0++)
                            {
                                tbl[p, i0] = 0;
                            }
                            tbl[p, 6 + 2 * (2 * 0 + 0) + 0] = c.f[d * (n * (m * k + j) + i) + di];
                            tbl[p, 6 + 2 * (2 * 0 + 0) + 1] = c.f[d * (n * (m * k + j) + (i + 1)) + di] - c.f[d * (n * (m * k + j) + i) + di];
                            tbl[p, 6 + 2 * (2 * 0 + 1) + 0] = c.f[d * (n * (m * k + (j + 1)) + i) + di] - c.f[d * (n * (m * k + j) + i) + di];
                            tbl[p, 6 + 2 * (2 * 0 + 1) + 1] = c.f[d * (n * (m * k + (j + 1)) + (i + 1)) + di] - c.f[d * (n * (m * k + (j + 1)) + i) + di] - c.f[d * (n * (m * k + j) + (i + 1)) + di] + c.f[d * (n * (m * k + j) + i) + di];
                            tbl[p, 6 + 2 * (2 * 1 + 0) + 0] = c.f[d * (n * (m * (k + 1) + j) + i) + di] - c.f[d * (n * (m * k + j) + i) + di];
                            tbl[p, 6 + 2 * (2 * 1 + 0) + 1] = c.f[d * (n * (m * (k + 1) + j) + (i + 1)) + di] - c.f[d * (n * (m * (k + 1) + j) + i) + di] - c.f[d * (n * (m * k + j) + (i + 1)) + di] + c.f[d * (n * (m * k + j) + i) + di];
                            tbl[p, 6 + 2 * (2 * 1 + 1) + 0] = c.f[d * (n * (m * (k + 1) + (j + 1)) + i) + di] - c.f[d * (n * (m * (k + 1) + j) + i) + di] - c.f[d * (n * (m * k + (j + 1)) + i) + di] + c.f[d * (n * (m * k + j) + i) + di];
                            tbl[p, 6 + 2 * (2 * 1 + 1) + 1] = c.f[d * (n * (m * (k + 1) + (j + 1)) + (i + 1)) + di] - c.f[d * (n * (m * (k + 1) + (j + 1)) + i) + di] - c.f[d * (n * (m * (k + 1) + j) + (i + 1)) + di] + c.f[d * (n * (m * (k + 1) + j) + i) + di] - c.f[d * (n * (m * k + (j + 1)) + (i + 1)) + di] + c.f[d * (n * (m * k + (j + 1)) + i) + di] + c.f[d * (n * (m * k + j) + (i + 1)) + di] - c.f[d * (n * (m * k + j) + i) + di];
                        }

                        //
                        // Rescale Cij
                        //
                        for (ci = 0; ci <= 1; ci++)
                        {
                            for (cj = 0; cj <= 1; cj++)
                            {
                                for (ck = 0; ck <= 1; ck++)
                                {
                                    tbl[p, 6 + 2 * (2 * ck + cj) + ci] = tbl[p, 6 + 2 * (2 * ck + cj) + ci] * Math.Pow(du, ci) * Math.Pow(dv, cj) * Math.Pow(dw, ck);
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    /*************************************************************************
    This subroutine calculates the value of the trilinear(or tricubic;possible
    will be later) spline  at the given point X(and its derivatives; possible
    will be later).

    INPUT PARAMETERS:
        C       -   spline interpolant.
        X, Y, Z -   point

    OUTPUT PARAMETERS:
        F   -   S(x,y,z)
        FX  -   dS(x,y,z)/dX
        FY  -   dS(x,y,z)/dY
        FXY -   d2S(x,y,z)/dXdY

      -- ALGLIB PROJECT --
         Copyright 26.04.2012 by Bochkanov Sergey
    *************************************************************************/
    private static void spline3ddiff(spline3dinterpolant c,
        double x,
        double y,
        double z,
        ref double f,
        ref double fx,
        ref double fy,
        ref double fxy,
        xparams _params)
    {
        double xd = 0;
        double yd = 0;
        double zd = 0;
        double c0 = 0;
        double c1 = 0;
        double c2 = 0;
        double c3 = 0;
        int ix = 0;
        int iy = 0;
        int iz = 0;
        int l = 0;
        int r = 0;
        int h = 0;

        f = 0;
        fx = 0;
        fy = 0;
        fxy = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline3DDiff: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline3DDiff: X or Y contains NaN or Infinite value");

        //
        // Prepare F, dF/dX, dF/dY, d2F/dXdY
        //
        f = 0;
        fx = 0;
        fy = 0;
        fxy = 0;
        if (c.d != 1)
        {
            return;
        }

        //
        // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        ix = l;

        //
        // Binary search in the [ y[0], ..., y[n-2] ] (y[n-1] is not included)
        //
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        iy = l;

        //
        // Binary search in the [ z[0], ..., z[n-2] ] (z[n-1] is not included)
        //
        l = 0;
        r = c.l - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.z[h]) >= (double)(z))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        iz = l;
        xd = (x - c.x[ix]) / (c.x[ix + 1] - c.x[ix]);
        yd = (y - c.y[iy]) / (c.y[iy + 1] - c.y[iy]);
        zd = (z - c.z[iz]) / (c.z[iz + 1] - c.z[iz]);

        //
        // Trilinear interpolation
        //
        if (c.stype == -1)
        {
            c0 = c.f[c.n * (c.m * iz + iy) + ix] * (1 - xd) + c.f[c.n * (c.m * iz + iy) + (ix + 1)] * xd;
            c1 = c.f[c.n * (c.m * iz + (iy + 1)) + ix] * (1 - xd) + c.f[c.n * (c.m * iz + (iy + 1)) + (ix + 1)] * xd;
            c2 = c.f[c.n * (c.m * (iz + 1) + iy) + ix] * (1 - xd) + c.f[c.n * (c.m * (iz + 1) + iy) + (ix + 1)] * xd;
            c3 = c.f[c.n * (c.m * (iz + 1) + (iy + 1)) + ix] * (1 - xd) + c.f[c.n * (c.m * (iz + 1) + (iy + 1)) + (ix + 1)] * xd;
            c0 = c0 * (1 - yd) + c1 * yd;
            c1 = c2 * (1 - yd) + c3 * yd;
            f = c0 * (1 - zd) + c1 * zd;
        }
    }


}
