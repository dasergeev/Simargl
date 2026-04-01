#pragma warning disable CS1591

namespace Simargl.Algorithms.Raw;


/*************************************************************************
This structure is used to store  OptGuard  report,  i.e.  report  on   the
properties of the nonlinear function being optimized with ALGLIB.

After you tell your optimizer to activate OptGuard  this technology starts
to silently monitor function values and gradients/Jacobians  being  passed
all around during your optimization session. Depending on specific set  of
checks enabled OptGuard may perform additional function evaluations  (say,
about 3*N evaluations if you want to check analytic gradient for errors).

Upon discovering that something strange happens  (function  values  and/or
gradient components change too sharply and/or unexpectedly) OptGuard  sets
one of the "suspicion  flags" (without interrupting optimization session).
After optimization is done, you can examine OptGuard report.

Following report fields can be set:
* nonc0suspected
* nonc1suspected
* badgradsuspected


=== WHAT CAN BE DETECTED WITH OptGuard INTEGRITY CHECKER =================

Following  types  of  errors  in your target function (constraints) can be
caught:
a) discontinuous functions ("non-C0" part of the report)
b) functions with discontinuous derivative ("non-C1" part of the report)
c) errors in the analytic gradient provided by user

These types of errors result in optimizer  stopping  well  before reaching
solution (most often - right after encountering discontinuity).

Type A errors are usually  coding  errors  during  implementation  of  the
target function. Most "normal" problems involve continuous functions,  and
anyway you can't reliably optimize discontinuous function.

Type B errors are either coding errors or (in case code itself is correct)
evidence of the fact  that  your  problem  is  an  "incorrect"  one.  Most
optimizers (except for ones provided by MINNS subpackage) do  not  support
nonsmooth problems.

Type C errors are coding errors which often prevent optimizer from  making
even one step  or result in optimizing stopping  too  early,  as  soon  as
actual descent direction becomes too different from one suggested by user-
supplied gradient.


=== WHAT IS REPORTED =====================================================

Following set of report fields deals with discontinuous  target functions,
ones not belonging to C0 continuity class:

* nonc0suspected - is a flag which is set upon discovering some indication
  of the discontinuity. If this flag is false, the rest of "non-C0" fields
  should be ignored
* nonc0fidx - is an index of the function (0 for  target  function,  1  or
  higher for nonlinear constraints) which is suspected of being "non-C0"
* nonc0lipshitzc - a Lipchitz constant for a function which was  suspected
  of being non-continuous.
* nonc0test0positive -  set  to  indicate  specific  test  which  detected
  continuity violation (test #0)

Following set of report fields deals with discontinuous gradient/Jacobian,
i.e. with functions violating C1 continuity:

* nonc1suspected - is a flag which is set upon discovering some indication
  of the discontinuity. If this flag is false, the rest of "non-C1" fields
  should be ignored
* nonc1fidx - is an index of the function (0 for  target  function,  1  or
  higher for nonlinear constraints) which is suspected of being "non-C1"
* nonc1lipshitzc - a Lipchitz constant for a function gradient  which  was
  suspected of being non-smooth.
* nonc1test0positive -  set  to  indicate  specific  test  which  detected
  continuity violation (test #0)
* nonc1test1positive -  set  to  indicate  specific  test  which  detected
  continuity violation (test #1)

Following set of report fields deals with errors in the gradient:
* badgradsuspected - is a flad which is set upon discovering an  error  in
  the analytic gradient supplied by user
* badgradfidx - index  of   the  function  with bad gradient (0 for target
  function, 1 or higher for nonlinear constraints)
* badgradvidx - index of the variable
* badgradxbase - location where Jacobian is tested
* following  matrices  store  user-supplied  Jacobian  and  its  numerical
  differentiation version (which is assumed to be  free  from  the  coding
  errors), both of them computed near the initial point:
  * badgraduser, an array[K,N], analytic Jacobian supplied by user
  * badgradnum,  an array[K,N], numeric  Jacobian computed by ALGLIB
  Here K is a total number of  nonlinear  functions  (target  +  nonlinear
  constraints), N is a variable number.
  The  element  of  badgraduser[] with index [badgradfidx,badgradvidx]  is
  assumed to be wrong.

More detailed error log can  be  obtained  from  optimizer  by  explicitly
requesting reports for tests C0.0, C1.0, C1.1.

  -- ALGLIB --
     Copyright 19.11.2018 by Bochkanov Sergey
*************************************************************************/
public class optguardreport : alglibobject
{
    //
    // Public declarations
    //
    public bool nonc0suspected { get { return _innerobj.nonc0suspected; } set { _innerobj.nonc0suspected = value; } }
    public bool nonc0test0positive { get { return _innerobj.nonc0test0positive; } set { _innerobj.nonc0test0positive = value; } }
    public int nonc0fidx { get { return _innerobj.nonc0fidx; } set { _innerobj.nonc0fidx = value; } }
    public double nonc0lipschitzc { get { return _innerobj.nonc0lipschitzc; } set { _innerobj.nonc0lipschitzc = value; } }
    public bool nonc1suspected { get { return _innerobj.nonc1suspected; } set { _innerobj.nonc1suspected = value; } }
    public bool nonc1test0positive { get { return _innerobj.nonc1test0positive; } set { _innerobj.nonc1test0positive = value; } }
    public bool nonc1test1positive { get { return _innerobj.nonc1test1positive; } set { _innerobj.nonc1test1positive = value; } }
    public int nonc1fidx { get { return _innerobj.nonc1fidx; } set { _innerobj.nonc1fidx = value; } }
    public double nonc1lipschitzc { get { return _innerobj.nonc1lipschitzc; } set { _innerobj.nonc1lipschitzc = value; } }
    public bool badgradsuspected { get { return _innerobj.badgradsuspected; } set { _innerobj.badgradsuspected = value; } }
    public int badgradfidx { get { return _innerobj.badgradfidx; } set { _innerobj.badgradfidx = value; } }
    public int badgradvidx { get { return _innerobj.badgradvidx; } set { _innerobj.badgradvidx = value; } }
    public double[] badgradxbase { get { return _innerobj.badgradxbase; } set { _innerobj.badgradxbase = value; } }
    public double[,] badgraduser { get { return _innerobj.badgraduser; } set { _innerobj.badgraduser = value; } }
    public double[,] badgradnum { get { return _innerobj.badgradnum; } set { _innerobj.badgradnum = value; } }

    public optguardreport()
    {
        _innerobj = new optguardapi.optguardreport();
    }

    public override alglibobject make_copy()
    {
        return new optguardreport((optguardapi.optguardreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private optguardapi.optguardreport _innerobj;
    public optguardapi.optguardreport innerobj { get { return _innerobj; } }
    public optguardreport(optguardapi.optguardreport obj)
    {
        _innerobj = obj;
    }
}
