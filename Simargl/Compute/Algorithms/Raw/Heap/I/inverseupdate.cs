#pragma warning disable CS1591

namespace Simargl.Algorithms.Raw;

public class inverseupdate
{
    /*************************************************************************
    Inverse matrix update by the Sherman-Morrison formula

    The algorithm updates matrix A^-1 when adding a number to an element
    of matrix A.

    Input parameters:
        InvA    -   inverse of matrix A.
                    Array whose indexes range within [0..N-1, 0..N-1].
        N       -   size of matrix A.
        UpdRow  -   row where the element to be updated is stored.
        UpdColumn - column where the element to be updated is stored.
        UpdVal  -   a number to be added to the element.


    Output parameters:
        InvA    -   inverse of modified matrix A.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixinvupdatesimple(double[,] inva,
        int n,
        int updrow,
        int updcolumn,
        double updval,
        xparams _params)
    {
        double[] t1 = new double[0];
        double[] t2 = new double[0];
        int i = 0;
        double lambdav = 0;
        double vt = 0;
        int i_ = 0;

        ap.assert(updrow >= 0 && updrow < n, "RMatrixInvUpdateSimple: incorrect UpdRow!");
        ap.assert(updcolumn >= 0 && updcolumn < n, "RMatrixInvUpdateSimple: incorrect UpdColumn!");
        t1 = new double[n - 1 + 1];
        t2 = new double[n - 1 + 1];

        //
        // T1 = InvA * U
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            t1[i_] = inva[i_, updrow];
        }

        //
        // T2 = v*InvA
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            t2[i_] = inva[updcolumn, i_];
        }

        //
        // Lambda = v * InvA * U
        //
        lambdav = updval * inva[updcolumn, updrow];

        //
        // InvA = InvA - correction
        //
        for (i = 0; i <= n - 1; i++)
        {
            vt = updval * t1[i];
            vt = vt / (1 + lambdav);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                inva[i, i_] = inva[i, i_] - vt * t2[i_];
            }
        }
    }


    /*************************************************************************
    Inverse matrix update by the Sherman-Morrison formula

    The algorithm updates matrix A^-1 when adding a vector to a row
    of matrix A.

    Input parameters:
        InvA    -   inverse of matrix A.
                    Array whose indexes range within [0..N-1, 0..N-1].
        N       -   size of matrix A.
        UpdRow  -   the row of A whose vector V was added.
                    0 <= Row <= N-1
        V       -   the vector to be added to a row.
                    Array whose index ranges within [0..N-1].

    Output parameters:
        InvA    -   inverse of modified matrix A.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixinvupdaterow(double[,] inva,
        int n,
        int updrow,
        double[] v,
        xparams _params)
    {
        double[] t1 = new double[0];
        double[] t2 = new double[0];
        int i = 0;
        int j = 0;
        double lambdav = 0;
        double vt = 0;
        int i_ = 0;

        t1 = new double[n - 1 + 1];
        t2 = new double[n - 1 + 1];

        //
        // T1 = InvA * U
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            t1[i_] = inva[i_, updrow];
        }

        //
        // T2 = v*InvA
        // Lambda = v * InvA * U
        //
        for (j = 0; j <= n - 1; j++)
        {
            vt = 0.0;
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                vt += v[i_] * inva[i_, j];
            }
            t2[j] = vt;
        }
        lambdav = t2[updrow];

        //
        // InvA = InvA - correction
        //
        for (i = 0; i <= n - 1; i++)
        {
            vt = t1[i] / (1 + lambdav);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                inva[i, i_] = inva[i, i_] - vt * t2[i_];
            }
        }
    }


    /*************************************************************************
    Inverse matrix update by the Sherman-Morrison formula

    The algorithm updates matrix A^-1 when adding a vector to a column
    of matrix A.

    Input parameters:
        InvA        -   inverse of matrix A.
                        Array whose indexes range within [0..N-1, 0..N-1].
        N           -   size of matrix A.
        UpdColumn   -   the column of A whose vector U was added.
                        0 <= UpdColumn <= N-1
        U           -   the vector to be added to a column.
                        Array whose index ranges within [0..N-1].

    Output parameters:
        InvA        -   inverse of modified matrix A.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixinvupdatecolumn(double[,] inva,
        int n,
        int updcolumn,
        double[] u,
        xparams _params)
    {
        double[] t1 = new double[0];
        double[] t2 = new double[0];
        int i = 0;
        double lambdav = 0;
        double vt = 0;
        int i_ = 0;

        t1 = new double[n - 1 + 1];
        t2 = new double[n - 1 + 1];

        //
        // T1 = InvA * U
        // Lambda = v * InvA * U
        //
        for (i = 0; i <= n - 1; i++)
        {
            vt = 0.0;
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                vt += inva[i, i_] * u[i_];
            }
            t1[i] = vt;
        }
        lambdav = t1[updcolumn];

        //
        // T2 = v*InvA
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            t2[i_] = inva[updcolumn, i_];
        }

        //
        // InvA = InvA - correction
        //
        for (i = 0; i <= n - 1; i++)
        {
            vt = t1[i] / (1 + lambdav);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                inva[i, i_] = inva[i, i_] - vt * t2[i_];
            }
        }
    }


    /*************************************************************************
    Inverse matrix update by the Sherman-Morrison formula

    The algorithm computes the inverse of matrix A+u*v' by using the given matrix
    A^-1 and the vectors u and v.

    Input parameters:
        InvA    -   inverse of matrix A.
                    Array whose indexes range within [0..N-1, 0..N-1].
        N       -   size of matrix A.
        U       -   the vector modifying the matrix.
                    Array whose index ranges within [0..N-1].
        V       -   the vector modifying the matrix.
                    Array whose index ranges within [0..N-1].

    Output parameters:
        InvA - inverse of matrix A + u*v'.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixinvupdateuv(double[,] inva,
        int n,
        double[] u,
        double[] v,
        xparams _params)
    {
        double[] t1 = new double[0];
        double[] t2 = new double[0];
        int i = 0;
        int j = 0;
        double lambdav = 0;
        double vt = 0;
        int i_ = 0;

        t1 = new double[n - 1 + 1];
        t2 = new double[n - 1 + 1];

        //
        // T1 = InvA * U
        // Lambda = v * T1
        //
        for (i = 0; i <= n - 1; i++)
        {
            vt = 0.0;
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                vt += inva[i, i_] * u[i_];
            }
            t1[i] = vt;
        }
        lambdav = 0.0;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            lambdav += v[i_] * t1[i_];
        }

        //
        // T2 = v*InvA
        //
        for (j = 0; j <= n - 1; j++)
        {
            vt = 0.0;
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                vt += v[i_] * inva[i_, j];
            }
            t2[j] = vt;
        }

        //
        // InvA = InvA - correction
        //
        for (i = 0; i <= n - 1; i++)
        {
            vt = t1[i] / (1 + lambdav);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                inva[i, i_] = inva[i, i_] - vt * t2[i_];
            }
        }
    }


}
