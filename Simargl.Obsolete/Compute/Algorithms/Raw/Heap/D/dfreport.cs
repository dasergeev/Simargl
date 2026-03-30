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
Decision forest training report.

=== training/oob errors ==================================================

Following fields store training set errors:
* relclserror           -   fraction of misclassified cases, [0,1]
* avgce                 -   average cross-entropy in bits per symbol
* rmserror              -   root-mean-square error
* avgerror              -   average error
* avgrelerror           -   average relative error

Out-of-bag estimates are stored in fields with same names, but "oob" prefix.

For classification problems:
* RMS, AVG and AVGREL errors are calculated for posterior probabilities

For regression problems:
* RELCLS and AVGCE errors are zero

=== variable importance ==================================================

Following fields are used to store variable importance information:

* topvars               -   variables ordered from the most  important  to
                            less  important  ones  (according  to  current
                            choice of importance raiting).
                            For example, topvars[0] contains index of  the
                            most important variable, and topvars[0:2]  are
                            indexes of 3 most important ones and so on.

* varimportances        -   array[nvars], ratings (the  larger,  the  more
                            important the variable  is,  always  in  [0,1]
                            range).
                            By default, filled  by  zeros  (no  importance
                            ratings are  provided  unless  you  explicitly
                            request them).
                            Zero rating means that variable is not important,
                            however you will rarely encounter such a thing,
                            in many cases  unimportant  variables  produce
                            nearly-zero (but nonzero) ratings.

Variable importance report must be EXPLICITLY requested by calling:
* dfbuildersetimportancegini() function, if you need out-of-bag Gini-based
  importance rating also known as MDI  (fast to  calculate,  resistant  to
  overfitting  issues,   but   has   some   bias  towards  continuous  and
  high-cardinality categorical variables)
* dfbuildersetimportancetrngini() function, if you need training set Gini-
  -based importance rating (what other packages typically report).
* dfbuildersetimportancepermutation() function, if you  need  permutation-
  based importance rating also known as MDA (slower to calculate, but less
  biased)
* dfbuildersetimportancenone() function,  if  you  do  not  need  importance
  ratings - ratings will be zero, topvars[] will be [0,1,2,...]

Different importance ratings (Gini or permutation) produce  non-comparable
values. Although in all cases rating values lie in [0,1] range, there  are
exist differences:
* informally speaking, Gini importance rating tends to divide "unit amount
  of importance"  between  several  important  variables, i.e. it produces
  estimates which roughly sum to 1.0 (or less than 1.0, if your  task  can
  not be solved exactly). If all variables  are  equally  important,  they
  will have same rating,  roughly  1/NVars,  even  if  every  variable  is
  critically important.
* from the other side, permutation importance tells us what percentage  of
  the model predictive power will be ruined  by  permuting  this  specific
  variable. It does not produce estimates which  sum  to  one.  Critically
  important variable will have rating close  to  1.0,  and  you  may  have
  multiple variables with such a rating.

More information on variable importance ratings can be found  in  comments
on the dfbuildersetimportancegini() and dfbuildersetimportancepermutation()
functions.
*************************************************************************/
public class dfreport : alglibobject
{
    //
    // Public declarations
    //
    public double relclserror { get { return _innerobj.relclserror; } set { _innerobj.relclserror = value; } }
    public double avgce { get { return _innerobj.avgce; } set { _innerobj.avgce = value; } }
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double avgrelerror { get { return _innerobj.avgrelerror; } set { _innerobj.avgrelerror = value; } }
    public double oobrelclserror { get { return _innerobj.oobrelclserror; } set { _innerobj.oobrelclserror = value; } }
    public double oobavgce { get { return _innerobj.oobavgce; } set { _innerobj.oobavgce = value; } }
    public double oobrmserror { get { return _innerobj.oobrmserror; } set { _innerobj.oobrmserror = value; } }
    public double oobavgerror { get { return _innerobj.oobavgerror; } set { _innerobj.oobavgerror = value; } }
    public double oobavgrelerror { get { return _innerobj.oobavgrelerror; } set { _innerobj.oobavgrelerror = value; } }
    public int[] topvars { get { return _innerobj.topvars; } set { _innerobj.topvars = value; } }
    public double[] varimportances { get { return _innerobj.varimportances; } set { _innerobj.varimportances = value; } }

    public dfreport()
    {
        _innerobj = new dforest.dfreport();
    }

    public override alglibobject make_copy()
    {
        return new dfreport((dforest.dfreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private dforest.dfreport _innerobj;
    public dforest.dfreport innerobj { get { return _innerobj; } }
    public dfreport(dforest.dfreport obj)
    {
        _innerobj = obj;
    }
}

