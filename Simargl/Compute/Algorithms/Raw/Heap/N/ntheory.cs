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

public class ntheory
{
    public static void findprimitiverootandinverse(int n,
        ref int proot,
        ref int invproot,
        xparams _params)
    {
        int candroot = 0;
        int phin = 0;
        int q = 0;
        int f = 0;
        bool allnonone = new bool();
        int x = 0;
        int lastx = 0;
        int y = 0;
        int lasty = 0;
        int a = 0;
        int b = 0;
        int t = 0;
        int n2 = 0;

        proot = 0;
        invproot = 0;

        ap.assert(n >= 3, "FindPrimitiveRootAndInverse: N<3");
        proot = 0;
        invproot = 0;

        //
        // check that N is prime
        //
        ap.assert(isprime(n, _params), "FindPrimitiveRoot: N is not prime");

        //
        // Because N is prime, Euler totient function is equal to N-1
        //
        phin = n - 1;

        //
        // Test different values of PRoot - from 2 to N-1.
        // One of these values MUST be primitive root.
        //
        // For testing we use algorithm from Wiki (Primitive root modulo n):
        // * compute phi(N)
        // * determine the different prime factors of phi(N), say p1, ..., pk
        // * for every element m of Zn*, compute m^(phi(N)/pi) mod N for i=1..k
        //   using a fast algorithm for modular exponentiation.
        // * a number m for which these k results are all different from 1 is a
        //   primitive root.
        //
        for (candroot = 2; candroot <= n - 1; candroot++)
        {

            //
            // We have current candidate root in CandRoot.
            //
            // Scan different prime factors of PhiN. Here:
            // * F is a current candidate factor
            // * Q is a current quotient - amount which was left after dividing PhiN
            //   by all previous factors
            //
            // For each factor, perform test mentioned above.
            //
            q = phin;
            f = 2;
            allnonone = true;
            while (q > 1)
            {
                if (q % f == 0)
                {
                    t = modexp(candroot, phin / f, n, _params);
                    if (t == 1)
                    {
                        allnonone = false;
                        break;
                    }
                    while (q % f == 0)
                    {
                        q = q / f;
                    }
                }
                f = f + 1;
            }
            if (allnonone)
            {
                proot = candroot;
                break;
            }
        }
        ap.assert(proot >= 2, "FindPrimitiveRoot: internal error (root not found)");

        //
        // Use extended Euclidean algorithm to find multiplicative inverse of primitive root
        //
        x = 0;
        lastx = 1;
        y = 1;
        lasty = 0;
        a = proot;
        b = n;
        while (b != 0)
        {
            q = a / b;
            t = a % b;
            a = b;
            b = t;
            t = lastx - q * x;
            lastx = x;
            x = t;
            t = lasty - q * y;
            lasty = y;
            y = t;
        }
        while (lastx < 0)
        {
            lastx = lastx + n;
        }
        invproot = lastx;

        //
        // Check that it is safe to perform multiplication modulo N.
        // Check results for consistency.
        //
        n2 = (n - 1) * (n - 1);
        ap.assert(n2 / (n - 1) == n - 1, "FindPrimitiveRoot: internal error");
        ap.assert(proot * invproot / proot == invproot, "FindPrimitiveRoot: internal error");
        ap.assert(proot * invproot / invproot == proot, "FindPrimitiveRoot: internal error");
        ap.assert(proot * invproot % n == 1, "FindPrimitiveRoot: internal error");
    }


    private static bool isprime(int n,
        xparams _params)
    {
        bool result = new bool();
        int p = 0;

        result = false;
        p = 2;
        while (p * p <= n)
        {
            if (n % p == 0)
            {
                return result;
            }
            p = p + 1;
        }
        result = true;
        return result;
    }


    private static int modmul(int a,
        int b,
        int n,
        xparams _params)
    {
        int result = 0;
        int t = 0;
        double ra = 0;
        double rb = 0;

        ap.assert(a >= 0 && a < n, "ModMul: A<0 or A>=N");
        ap.assert(b >= 0 && b < n, "ModMul: B<0 or B>=N");

        //
        // Base cases
        //
        ra = a;
        rb = b;
        if (b == 0 || a == 0)
        {
            result = 0;
            return result;
        }
        if (b == 1 || a == 1)
        {
            result = a * b;
            return result;
        }
        if ((double)(ra * rb) == (double)(a * b))
        {
            result = a * b % n;
            return result;
        }

        //
        // Non-base cases
        //
        if (b % 2 == 0)
        {

            //
            // A*B = (A*(B/2)) * 2
            //
            // Product T=A*(B/2) is calculated recursively, product T*2 is
            // calculated as follows:
            // * result:=T-N
            // * result:=result+T
            // * if result<0 then result:=result+N
            //
            // In case integer result overflows, we generate exception
            //
            t = modmul(a, b / 2, n, _params);
            result = t - n;
            result = result + t;
            if (result < 0)
            {
                result = result + n;
            }
        }
        else
        {

            //
            // A*B = (A*(B div 2)) * 2 + A
            //
            // Product T=A*(B/2) is calculated recursively, product T*2 is
            // calculated as follows:
            // * result:=T-N
            // * result:=result+T
            // * if result<0 then result:=result+N
            //
            // In case integer result overflows, we generate exception
            //
            t = modmul(a, b / 2, n, _params);
            result = t - n;
            result = result + t;
            if (result < 0)
            {
                result = result + n;
            }
            result = result - n;
            result = result + a;
            if (result < 0)
            {
                result = result + n;
            }
        }
        return result;
    }


    private static int modexp(int a,
        int b,
        int n,
        xparams _params)
    {
        int result = 0;
        int t = 0;

        ap.assert(a >= 0 && a < n, "ModExp: A<0 or A>=N");
        ap.assert(b >= 0, "ModExp: B<0");

        //
        // Base cases
        //
        if (b == 0)
        {
            result = 1;
            return result;
        }
        if (b == 1)
        {
            result = a;
            return result;
        }

        //
        // Non-base cases
        //
        if (b % 2 == 0)
        {
            t = modmul(a, a, n, _params);
            result = modexp(t, b / 2, n, _params);
        }
        else
        {
            t = modmul(a, a, n, _params);
            result = modexp(t, b / 2, n, _params);
            result = modmul(result, a, n, _params);
        }
        return result;
    }


}
