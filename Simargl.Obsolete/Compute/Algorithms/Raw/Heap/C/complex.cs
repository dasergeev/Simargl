#pragma warning disable CS8765
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


/********************************************************************
Class defining a complex number with double precision.
********************************************************************/
public struct complex
{
    public double x;
    public double y;

    public complex(double _x)
    {
        x = _x;
        y = 0;
    }
    public complex(double _x, double _y)
    {
        x = _x;
        y = _y;
    }
    public static implicit operator complex(double _x)
    {
        return new complex(_x);
    }
    public static bool operator ==(complex lhs, complex rhs)
    {
        return ((double)lhs.x == (double)rhs.x) & ((double)lhs.y == (double)rhs.y);
    }
    public static bool operator !=(complex lhs, complex rhs)
    {
        return ((double)lhs.x != (double)rhs.x) | ((double)lhs.y != (double)rhs.y);
    }
    public static complex operator +(complex lhs)
    {
        return lhs;
    }
    public static complex operator -(complex lhs)
    {
        return new complex(-lhs.x, -lhs.y);
    }
    public static complex operator +(complex lhs, complex rhs)
    {
        return new complex(lhs.x + rhs.x, lhs.y + rhs.y);
    }
    public static complex operator -(complex lhs, complex rhs)
    {
        return new complex(lhs.x - rhs.x, lhs.y - rhs.y);
    }
    public static complex operator *(complex lhs, complex rhs)
    {
        return new complex(lhs.x * rhs.x - lhs.y * rhs.y, lhs.x * rhs.y + lhs.y * rhs.x);
    }
    public static complex operator /(complex lhs, complex rhs)
    {
        complex result;
        double e;
        double f;
        if (System.Math.Abs(rhs.y) < System.Math.Abs(rhs.x))
        {
            e = rhs.y / rhs.x;
            f = rhs.x + rhs.y * e;
            result.x = (lhs.x + lhs.y * e) / f;
            result.y = (lhs.y - lhs.x * e) / f;
        }
        else
        {
            e = rhs.x / rhs.y;
            f = rhs.y + rhs.x * e;
            result.x = (lhs.y + lhs.x * e) / f;
            result.y = (-lhs.x + lhs.y * e) / f;
        }
        return result;
    }
    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }
    public override bool Equals(object obj)
    {
        if (obj is byte)
            return Equals(new complex((byte)obj));
        if (obj is sbyte)
            return Equals(new complex((sbyte)obj));
        if (obj is short)
            return Equals(new complex((short)obj));
        if (obj is ushort)
            return Equals(new complex((ushort)obj));
        if (obj is int)
            return Equals(new complex((int)obj));
        if (obj is uint)
            return Equals(new complex((uint)obj));
        if (obj is long)
            return Equals(new complex((long)obj));
        if (obj is ulong)
            return Equals(new complex((ulong)obj));
        if (obj is float)
            return Equals(new complex((float)obj));
        if (obj is double)
            return Equals(new complex((double)obj));
        if (obj is decimal)
            return Equals(new complex((double)(decimal)obj));
        return base.Equals(obj);
    }
}
