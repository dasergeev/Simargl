using System;
using static Simargl.Algorithms.Raw.RawAlgorithms;

#pragma warning disable CS3006
#pragma warning disable CS8625
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


/********************************************************************
internal functions
********************************************************************/
public class ap
{
    public static int len<T>(T[] a)
    { return a.Length; }

    public static int rows<T>(T[,] a)
    { return a.GetLength(0); }
    public static int cols<T>(T[,] a)
    { return a.GetLength(1); }
    public static void swap<T>(ref T a, ref T b)
    {
        T t = a;
        a = b;
        b = t;
    }

    public static void assert(bool cond, string s)
    {
        if (!cond)
        {
            if (trace_mode != TRACE_MODE.NONE)
                trace("---!!! CRITICAL ERROR !!!--- exception with message '" + s + "' was generated\n");
            throw new alglibexception(s);
        }
    }

    public static void assert(bool cond)
    {
        assert(cond, "ALGLIB: assertion failed");
    }

    /****************************************************************
    Error tracking for unit testing purposes; utility functions.
    ****************************************************************/
    public static string sef_xdesc = "";

    public static void seterrorflag(ref bool flag, bool cond, string xdesc)
    {
        if (cond)
        {
            flag = true;
            sef_xdesc = xdesc;
        }
    }

    /****************************************************************
    returns dps (digits-of-precision) value corresponding to threshold.
    dps(0.9)  = dps(0.5)  = dps(0.1) = 0
    dps(0.09) = dps(0.05) = dps(0.01) = 1
    and so on
    ****************************************************************/
    public static int threshold2dps(double threshold)
    {
        int result = 0;
        double t;
        for (result = 0, t = 1; t / 10 > threshold * (1 + 1E-10); result++, t /= 10) ;
        return result;
    }

    /****************************************************************
    prints formatted complex
    ****************************************************************/
    public static string format(complex a, int _dps)
    {
        int dps = Math.Abs(_dps);
        string fmt = _dps >= 0 ? "F" : "E";
        string fmtx = String.Format("{{0:" + fmt + "{0}}}", dps);
        string fmty = String.Format("{{0:" + fmt + "{0}}}", dps);
        string result = String.Format(fmtx, a.x) + (a.y >= 0 ? "+" : "-") + String.Format(fmty, Math.Abs(a.y)) + "i";
        result = result.Replace(',', '.');
        return result;
    }

    /****************************************************************
    prints formatted array
    ****************************************************************/
    public static string format(bool[] a)
    {
        string[] result = new string[len(a)];
        int i;
        for (i = 0; i < len(a); i++)
            if (a[i])
                result[i] = "true";
            else
                result[i] = "false";
        return "{" + String.Join(",", result) + "}";
    }

    /****************************************************************
    prints formatted array
    ****************************************************************/
    public static string format(int[] a)
    {
        string[] result = new string[len(a)];
        int i;
        for (i = 0; i < len(a); i++)
            result[i] = a[i].ToString();
        return "{" + String.Join(",", result) + "}";
    }

    /****************************************************************
    prints formatted array
    ****************************************************************/
    public static string format(double[] a, int _dps)
    {
        int dps = Math.Abs(_dps);
        string sfmt = _dps >= 0 ? "F" : "E";
        string fmt = String.Format("{{0:" + sfmt + "{0}}}", dps);
        string[] result = new string[len(a)];
        int i;
        for (i = 0; i < len(a); i++)
        {
            result[i] = String.Format(fmt, a[i]);
            result[i] = result[i].Replace(',', '.');
        }
        return "{" + String.Join(",", result) + "}";
    }

    /****************************************************************
    prints formatted array
    ****************************************************************/
    public static string format(complex[] a, int _dps)
    {
        int dps = Math.Abs(_dps);
        string fmt = _dps >= 0 ? "F" : "E";
        string fmtx = String.Format("{{0:" + fmt + "{0}}}", dps);
        string fmty = String.Format("{{0:" + fmt + "{0}}}", dps);
        string[] result = new string[len(a)];
        int i;
        for (i = 0; i < len(a); i++)
        {
            result[i] = String.Format(fmtx, a[i].x) + (a[i].y >= 0 ? "+" : "-") + String.Format(fmty, Math.Abs(a[i].y)) + "i";
            result[i] = result[i].Replace(',', '.');
        }
        return "{" + String.Join(",", result) + "}";
    }

    /****************************************************************
    prints formatted matrix
    ****************************************************************/
    public static string format(bool[,] a)
    {
        int i, j, m, n;
        n = cols(a);
        m = rows(a);
        bool[] line = new bool[n];
        string[] result = new string[m];
        for (i = 0; i < m; i++)
        {
            for (j = 0; j < n; j++)
                line[j] = a[i, j];
            result[i] = format(line);
        }
        return "{" + String.Join(",", result) + "}";
    }

    /****************************************************************
    prints formatted matrix
    ****************************************************************/
    public static string format(int[,] a)
    {
        int i, j, m, n;
        n = cols(a);
        m = rows(a);
        int[] line = new int[n];
        string[] result = new string[m];
        for (i = 0; i < m; i++)
        {
            for (j = 0; j < n; j++)
                line[j] = a[i, j];
            result[i] = format(line);
        }
        return "{" + String.Join(",", result) + "}";
    }

    /****************************************************************
    prints formatted matrix
    ****************************************************************/
    public static string format(double[,] a, int dps)
    {
        int i, j, m, n;
        n = cols(a);
        m = rows(a);
        double[] line = new double[n];
        string[] result = new string[m];
        for (i = 0; i < m; i++)
        {
            for (j = 0; j < n; j++)
                line[j] = a[i, j];
            result[i] = format(line, dps);
        }
        return "{" + String.Join(",", result) + "}";
    }

    /****************************************************************
    prints formatted matrix
    ****************************************************************/
    public static string format(complex[,] a, int dps)
    {
        int i, j, m, n;
        n = cols(a);
        m = rows(a);
        complex[] line = new complex[n];
        string[] result = new string[m];
        for (i = 0; i < m; i++)
        {
            for (j = 0; j < n; j++)
                line[j] = a[i, j];
            result[i] = format(line, dps);
        }
        return "{" + String.Join(",", result) + "}";
    }

    /********************************************************************
    Tracing and logging
    ********************************************************************/
    enum TRACE_MODE { NONE, FILE };
    private static TRACE_MODE trace_mode = TRACE_MODE.NONE;
    private static string trace_tags = "";
    private static string trace_filename = "";

    public static void trace_file(string tags, string filename)
    {
        trace_mode = TRACE_MODE.FILE;
        trace_tags = "," + tags.ToLower() + ",";
        trace_filename = filename;
        trace("####################################################################################################\n");
        trace("# TRACING ENABLED: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
        trace("# TRACE TAGS:      '" + tags + "'\n");
        trace("####################################################################################################\n");
    }

    public static void trace_disable()
    {
        trace_mode = TRACE_MODE.NONE;
        trace_tags = "";
    }

    public static bool istraceenabled(string tag, xparams _params)
    {
        // trace disabled
        if (trace_mode == TRACE_MODE.NONE)
            return false;

        // contains tag (followed by comma, which means exact match)
        if (trace_tags.Contains("," + tag.ToLower() + ","))
            return true;

        // contains tag (followed by dot, which means match with child)
        if (trace_tags.Contains("," + tag.ToLower() + "."))
            return true;

        // nothing
        return false;
    }

    public static void trace(string s)
    {
        if (trace_mode == TRACE_MODE.NONE)
            return;
        if (trace_mode == TRACE_MODE.FILE)
        {
            System.IO.File.AppendAllText(trace_filename, s);
            return;
        }
    }

    /********************************************************************
    array of objects and related functions
    ********************************************************************/
    public class objarray : apobject
    {
        /* lock object which protects array */
        private smp.ae_lock array_lock;

        /* elements count */
        private int cnt;

        /* storage size */
        private int capacity;

        /* whether capacity can be automatically increased or not */
        private bool fixed_capacity;

        /* pointers to objects */
        private apobject[] arr;

        public objarray()
        {
            init();
        }

        public override void init()
        {
            cnt = 0;
            capacity = 0;
            fixed_capacity = false;
            arr = null;
            smp.ae_init_lock(ref array_lock);
        }

        public override apobject make_copy()
        {
            int i;
            objarray result = new objarray();
            result.cnt = cnt;
            result.capacity = capacity;
            result.fixed_capacity = fixed_capacity;
            AE_CRITICAL_ASSERT(cnt <= capacity);
            if (result.capacity > 0)
            {
                result.arr = new apobject[result.capacity];
                for (i = 0; i < result.cnt; i++)
                    result.arr[i] = arr[i].make_copy();
            }
            return result;
        }


        /************************************************************************
        This function clears dynamic objects array.

        After call to this function all objects managed by array are destroyed and
        their memory is freed. Array capacity does not change.

        NOTE: this function is thread-unsafe.
        ************************************************************************/
        public void clear()
        {
            int i;
            for (i = 0; i < cnt; i++)
                arr[i] = null;
            cnt = 0;
        }

        /************************************************************************
        Internal function which modifies array capacity, ignoring fixed_capacity
        flag.
        ************************************************************************/
        void _set_capacity(int new_capacity)
        {
            int i;

            /* integrity checks */
            ap.assert(cnt <= new_capacity, "objarray._set_capacity: new capacity is less than present size");

            /* quick exit */
            if (cnt == new_capacity)
                return;

            /* increase capacity */
            capacity = new_capacity;

            /* allocate new memory, copy data */
            apobject[] new_arr = new apobject[new_capacity];
            for (i = 0; i < cnt; i++)
                new_arr[i] = arr[i];
            arr = new_arr;
        }

        /************************************************************************
        This function sets array into special fixed capacity  mode  which  allows
        concurrent appends, writes and reads to be performed.

        new_capacity        new capacity, must be at least equal to current length.

        On output:
        * array capacity increased to new_capacity exactly
        * all present elements are retained
        * if array size already exceeds new_capacity, an exception is generated
        ************************************************************************/
        public void set_fixed_capacity(int new_capacity)
        {
            ap.assert(cnt <= new_capacity, "objarray.set_fixed_capacity: new capacity is less than present size");
            _set_capacity(new_capacity);
            fixed_capacity = true;
        }

        /************************************************************************
        get length
        ************************************************************************/
        public int getlength()
        {
            return cnt;
        }

        /************************************************************************
        This function retrieves element from the array and assigns it to PTR.

        arr                 array.
        idx                 element index
        ptr                 assign target

        On output:
        * pointer with index idx is assigned to PTR
        * out-of-bounds access will result in exception being generated
        ************************************************************************/
        public void get<T>(int idx, ref T ptr) where T : apobject
        {
            if (idx < 0 || idx >= cnt)
                ap.assert(false, "ObjArray: out of bounds read access was performed");
            ptr = (T)arr[idx];
        }


        /************************************************************************
        This function atomically appends object  to arr, increasing array  length
        by 1 and returns index of the element being added.

        arr                 array.
        ptr                 object reference

        Notes:
        * if array has fixed capacity and its size is already at  its  limit,  an
          exception will be generated
        * ptr can be null

        This function is partially thread-safe:
        * parallel threads can concurrently append elements using this function
        * for fixed-capacity arrays it is possible to combine appends with reads,
          e.g. to use ae_obj_array_get()
        ************************************************************************/
        public int append(apobject ptr)
        {
            int result;

            /* initial integrity checks */
            ap.assert(!fixed_capacity || cnt < capacity, "objarray.append: unable to append, all capacity is used up");

            /* get primary lock */
            smp.ae_acquire_lock(array_lock);
            try
            {
                /* reallocate if needed */
                if (cnt == capacity)
                {
                    /* one more integrity check */
                    AE_CRITICAL_ASSERT(!fixed_capacity);

                    /* increase capacity */
                    _set_capacity(2 * capacity + 8);
                }

                /* append ptr */
                arr[cnt] = ptr;

                /* issue memory fence (necessary for correct ae_obj_array_get_length) and increase array size */
                System.Threading.Thread.MemoryBarrier();
                result = cnt;
                cnt = result + 1;
            }
            finally
            {
                /* release primary lock */
                smp.ae_release_lock(array_lock);
            }

            /* done */
            return result;
        }

        /************************************************************************
        This function sets idx-th element of array to ptr.

        Notes:
        * array size must be  at  least  idx+1,  an  exception will be generated
          otherwise
        * ptr can be null
        * this function does NOT change array size and capacity

        This function is partially thread-safe: it is  safe  as  long  as  array
        capacity is not changed by concurrently called functions.

        idx                 element index
        ptr                 object

        ************************************************************************/
        public void rewrite(int idx, apobject ptr)
        {
            /* initial integrity checks */
            if (idx < 0 || idx >= cnt)
                ap.assert(false, "objarray.rewrite: out of bounds idx");
            arr[idx] = ptr;
        }
    }

};
