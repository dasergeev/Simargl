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
Sparse matrix structure.

You should use ALGLIB functions to work with sparse matrix. Never  try  to
access its fields directly!

NOTES ON THE SPARSE STORAGE FORMATS

Sparse matrices can be stored using several formats:
* Hash-Table representation
* Compressed Row Storage (CRS)
* Skyline matrix storage (SKS)

Each of the formats has benefits and drawbacks:
* Hash-table is good for dynamic operations (insertion of new elements),
  but does not support linear algebra operations
* CRS is good for operations like matrix-vector or matrix-matrix products,
  but its initialization is less convenient - you have to tell row   sizes
  at the initialization, and you have to fill  matrix  only  row  by  row,
  from left to right.
* SKS is a special format which is used to store triangular  factors  from
  Cholesky factorization. It does not support  dynamic  modification,  and
  support for linear algebra operations is very limited.

Tables below outline information about these two formats:

    OPERATIONS WITH MATRIX      HASH        CRS         SKS
    creation                    +           +           +
    SparseGet                   +           +           +
    SparseExists                +           +           +
    SparseRewriteExisting       +           +           +
    SparseSet                   +           +           +
    SparseAdd                   +
    SparseGetRow                            +           +
    SparseGetCompressedRow                  +           +
    sparse-dense linear algebra             +           +
*************************************************************************/
public class sparsematrix : alglibobject
{
    //
    // Public declarations
    //

    public sparsematrix()
    {
        _innerobj = new sparse.sparsematrix();
    }

    public override alglibobject make_copy()
    {
        return new sparsematrix((sparse.sparsematrix)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private sparse.sparsematrix _innerobj;
    public sparse.sparsematrix innerobj { get { return _innerobj; } }
    public sparsematrix(sparse.sparsematrix obj)
    {
        _innerobj = obj;
    }
}
