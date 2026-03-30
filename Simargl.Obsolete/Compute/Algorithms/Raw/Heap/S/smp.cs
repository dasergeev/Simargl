#pragma warning disable CS3026
#pragma warning disable CS8625
#pragma warning disable CS8618
#pragma warning disable CS8600
#pragma warning disable CS8602
#pragma warning disable CS8601
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


/*
 * Parts of smp class which are shared with GPL version of ALGLIB
 */
public partial class smp
{
    public const int AE_LOCK_CYCLES = 512;
    public const int AE_LOCK_TESTS_BEFORE_YIELD = 16;

    /*
     * This variable is used to perform spin-wait loops in a platform-independent manner
     * (loops which should work same way on Mono and Microsoft NET). You SHOULD NEVER
     * change this field - it must be zero during all program life.
     */
    public static volatile int never_change_it = 0;

    /*************************************************************************
    Lock.

    This class provides lightweight spin lock
    *************************************************************************/
    public class ae_lock
    {
        public volatile int is_locked;
    }

    /********************************************************************
    Shared pool: data structure used to provide thread-safe access to pool
    of temporary variables.
    ********************************************************************/
    public class sharedpoolentry
    {
        public apobject obj;
        public sharedpoolentry next_entry;
    }
    public class shared_pool : apobject
    {
        /* lock object which protects pool */
        public ae_lock pool_lock;

        /* seed object (used to create new instances of temporaries) */
        public volatile apobject seed_object;

        /*
         * list of recycled OBJECTS:
         * 1. entries in this list store pointers to recycled objects
         * 2. every time we retrieve object, we retrieve first entry from this list,
         *    move it to recycled_entries and return its obj field to caller/
         */
        public volatile sharedpoolentry recycled_objects;

        /* 
         * list of recycled ENTRIES:
         * 1. this list holds entries which are not used to store recycled objects;
         *    every time recycled object is retrieved, its entry is moved to this list.
         * 2. every time object is recycled, we try to fetch entry for him from this list
         *    before allocating it with malloc()
         */
        public volatile sharedpoolentry recycled_entries;

        /* enumeration pointer, points to current recycled object*/
        public volatile sharedpoolentry enumeration_counter;

        /* constructor */
        public shared_pool()
        {
            ae_init_lock(ref pool_lock);
        }

        /* initializer - creation of empty pool */
        public override void init()
        {
            seed_object = null;
            recycled_objects = null;
            recycled_entries = null;
            enumeration_counter = null;
        }

        /* copy constructor (it is NOT thread-safe) */
        public override apobject make_copy()
        {
            sharedpoolentry ptr, buf;
            shared_pool result = new shared_pool();

            /* create lock */
            ae_init_lock(ref result.pool_lock);

            /* copy seed object */
            if (seed_object != null)
                result.seed_object = seed_object.make_copy();

            /*
             * copy recycled objects:
             * 1. copy to temporary list (objects are inserted to beginning, order is reversed)
             * 2. copy temporary list to output list (order is restored back to normal)
             */
            buf = null;
            for (ptr = recycled_objects; ptr != null; ptr = ptr.next_entry)
            {
                sharedpoolentry tmp = new sharedpoolentry();
                tmp.obj = ptr.obj.make_copy();
                tmp.next_entry = buf;
                buf = tmp;
            }
            result.recycled_objects = null;
            for (ptr = buf; ptr != null;)
            {
                sharedpoolentry next_ptr = ptr.next_entry;
                ptr.next_entry = result.recycled_objects;
                result.recycled_objects = ptr;
                ptr = next_ptr;
            }

            /* recycled entries are not copied because they do not store any information */
            result.recycled_entries = null;

            /* enumeration counter is reset on copying */
            result.enumeration_counter = null;

            return result;
        }
    }


    /************************************************************************
    This function performs given number of spin-wait iterations
    ************************************************************************/
    public static void ae_spin_wait(int cnt)
    {
        /*
         * these strange operations with ae_never_change_it are necessary to
         * prevent compiler optimization of the loop.
         */
        int i;

        /* very unlikely because no one will wait for such amount of cycles */
        if (cnt > 0x12345678)
            never_change_it = cnt % 10;

        /* spin wait, test condition which will never be true */
        for (i = 0; i < cnt; i++)
            if (never_change_it > 0)
                never_change_it--;
    }


    /************************************************************************
    This function causes the calling thread to relinquish the CPU. The thread
    is moved to the end of the queue and some other thread gets to run.
    ************************************************************************/
    public static void ae_yield()
    {
        System.Threading.Thread.Sleep(0);
    }

    /************************************************************************
    This function initializes ae_lock structure and sets lock in a free mode.
    ************************************************************************/
    public static void ae_init_lock(ref ae_lock obj)
    {
        obj = new ae_lock();
        obj.is_locked = 0;
    }


    /************************************************************************
    This function acquires lock. In case lock is busy, we perform several
    iterations inside tight loop before trying again.
    ************************************************************************/
    public static void ae_acquire_lock(ae_lock obj)
    {
        int cnt = 0;
        for (; ; )
        {
            if (System.Threading.Interlocked.CompareExchange(ref obj.is_locked, 1, 0) == 0)
                return;
            ae_spin_wait(AE_LOCK_CYCLES);
            cnt++;
            if (cnt % AE_LOCK_TESTS_BEFORE_YIELD == 0)
                ae_yield();
        }
    }


    /************************************************************************
    This function releases lock.
    ************************************************************************/
    public static void ae_release_lock(ae_lock obj)
    {
        System.Threading.Interlocked.Exchange(ref obj.is_locked, 0);
    }


    /************************************************************************
    This function frees ae_lock structure.
    ************************************************************************/
    public static void ae_free_lock(ref ae_lock obj)
    {
        obj = null;
    }


    /************************************************************************
    This function returns True, if internal seed object was set.  It  returns
    False for un-seeded pool.

    dst                 destination pool (initialized by constructor function)

    NOTE: this function is NOT thread-safe. It does not acquire pool lock, so
          you should NOT call it when lock can be used by another thread.
    ************************************************************************/
    public static bool ae_shared_pool_is_initialized(shared_pool dst)
    {
        return dst.seed_object != null;
    }


    /************************************************************************
    This function sets internal seed object. All objects owned by the pool
    (current seed object, recycled objects) are automatically freed.

    dst                 destination pool (initialized by constructor function)
    seed_object         new seed object

    NOTE: this function is NOT thread-safe. It does not acquire pool lock, so
          you should NOT call it when lock can be used by another thread.
    ************************************************************************/
    public static void ae_shared_pool_set_seed(shared_pool dst, apobject seed_object)
    {
        dst.seed_object = seed_object.make_copy();
        dst.recycled_objects = null;
        dst.enumeration_counter = null;
    }


    /************************************************************************
    This  function  retrieves  a  copy  of  the seed object from the pool and
    stores it to target variable.

    pool                pool
    obj                 target variable
    
    NOTE: this function IS thread-safe.  It  acquires  pool  lock  during its
          operation and can be used simultaneously from several threads.
    ************************************************************************/
    public static void ae_shared_pool_retrieve<T>(shared_pool pool, ref T obj) where T : apobject
    {
        apobject new_obj;

        /* assert that pool was seeded */
        ap.assert(pool.seed_object != null, "ALGLIB: shared pool is not seeded, PoolRetrieve() failed");

        /* acquire lock */
        ae_acquire_lock(pool.pool_lock);

        /* try to reuse recycled objects */
        if (pool.recycled_objects != null)
        {
            /* retrieve entry/object from list of recycled objects */
            sharedpoolentry result = pool.recycled_objects;
            pool.recycled_objects = pool.recycled_objects.next_entry;
            new_obj = result.obj;
            result.obj = null;

            /* move entry to list of recycled entries */
            result.next_entry = pool.recycled_entries;
            pool.recycled_entries = result;

            /* release lock */
            ae_release_lock(pool.pool_lock);

            /* assign object to smart pointer */
            obj = (T)new_obj;

            return;
        }

        /*
         * release lock; we do not need it anymore because
         * copy constructor does not modify source variable.
         */
        ae_release_lock(pool.pool_lock);

        /* create new object from seed */
        new_obj = pool.seed_object.make_copy();

        /* assign object to pointer and return */
        obj = (T)new_obj;
    }


    /************************************************************************
    This  function  recycles object owned by the source variable by moving it
    to internal storage of the shared pool.

    Source  variable  must  own  the  object,  i.e.  be  the only place where
    reference  to  object  is  stored.  After  call  to  this function source
    variable becomes NULL.

    pool                pool
    obj                 source variable

    NOTE: this function IS thread-safe.  It  acquires  pool  lock  during its
          operation and can be used simultaneously from several threads.
    ************************************************************************/
    public static void ae_shared_pool_recycle<T>(shared_pool pool, ref T obj) where T : apobject
    {
        sharedpoolentry new_entry;

        /* assert that pool was seeded */
        ap.assert(pool.seed_object != null, "ALGLIB: shared pool is not seeded, PoolRecycle() failed");

        /* assert that pointer non-null */
        ap.assert(obj != null, "ALGLIB: obj in ae_shared_pool_recycle() is NULL");

        /* acquire lock */
        ae_acquire_lock(pool.pool_lock);

        /* acquire shared pool entry (reuse one from recycled_entries or malloc new one) */
        if (pool.recycled_entries != null)
        {
            /* reuse previously allocated entry */
            new_entry = pool.recycled_entries;
            pool.recycled_entries = new_entry.next_entry;
        }
        else
        {
            /*
             * Allocate memory for new entry.
             *
             * NOTE: we release pool lock during allocation because new() may raise
             *       exception and we do not want our pool to be left in the locked state.
             */
            ae_release_lock(pool.pool_lock);
            new_entry = new sharedpoolentry();
            ae_acquire_lock(pool.pool_lock);
        }

        /* add object to the list of recycled objects */
        new_entry.obj = obj;
        new_entry.next_entry = pool.recycled_objects;
        pool.recycled_objects = new_entry;

        /* release lock object */
        ae_release_lock(pool.pool_lock);

        /* release source pointer */
        obj = null;
    }


    /************************************************************************
    This function clears internal list of  recycled  objects,  but  does  not
    change seed object managed by the pool.

    pool                pool

    NOTE: this function is thread-safe.
    ************************************************************************/
    public static void ae_shared_pool_clear_recycled(shared_pool pool)
    {
        /* acquire lock */
        ae_acquire_lock(pool.pool_lock);

        /* drop recycled objects list */
        pool.recycled_objects = null;

        /* release lock object */
        ae_release_lock(pool.pool_lock);
    }


    /************************************************************************
    This function allows to enumerate recycled elements of the  shared  pool.
    It stores reference to the first recycled object in the smart pointer.

    IMPORTANT:
    * in case target variable owns non-NULL value, it is rewritten
    * recycled object IS NOT removed from pool
    * target variable DOES NOT become owner of the new value; you can use
      reference to recycled object, but you do not own it.
    * this function IS NOT thread-safe
    * you SHOULD NOT modify shared pool during enumeration (although you  can
      modify state of the objects retrieved from pool)
    * in case there is no recycled objects in the pool, NULL is stored to obj
    * in case pool is not seeded, NULL is stored to obj

    pool                pool
    obj                 reference
    ************************************************************************/
    public static void ae_shared_pool_first_recycled<T>(shared_pool pool, ref T obj) where T : apobject
    {
        /* modify internal enumeration counter */
        pool.enumeration_counter = pool.recycled_objects;

        /* exit on empty list */
        if (pool.enumeration_counter == null)
        {
            obj = null;
            return;
        }

        /* assign object to smart pointer */
        obj = (T)pool.enumeration_counter.obj;
    }


    /************************************************************************
    This function allows to enumerate recycled elements of the  shared  pool.
    It stores pointer to the next recycled object in the smart pointer.

    IMPORTANT:
    * in case target variable owns non-NULL value, it is rewritten
    * recycled object IS NOT removed from pool
    * target pointer DOES NOT become owner of the new value
    * this function IS NOT thread-safe
    * you SHOULD NOT modify shared pool during enumeration (although you  can
      modify state of the objects retrieved from pool)
    * in case there is no recycled objects left in the pool, NULL is stored.
    * in case pool is not seeded, NULL is stored.

    pool                pool
    obj                 target variable
    ************************************************************************/
    public static void ae_shared_pool_next_recycled<T>(shared_pool pool, ref T obj) where T : apobject
    {
        /* exit on end of list */
        if (pool.enumeration_counter == null)
        {
            obj = null;
            return;
        }

        /* modify internal enumeration counter */
        pool.enumeration_counter = pool.enumeration_counter.next_entry;

        /* exit on empty list */
        if (pool.enumeration_counter == null)
        {
            obj = null;
            return;
        }

        /* assign object to smart pointer */
        obj = (T)pool.enumeration_counter.obj;
    }


    /************************************************************************
    This function clears internal list of recycled objects and  seed  object.
    However, pool still can be used (after initialization with another seed).

    pool                pool
    state               ALGLIB environment state

    NOTE: this function is NOT thread-safe. It does not acquire pool lock, so
          you should NOT call it when lock can be used by another thread.
    ************************************************************************/
    public static void ae_shared_pool_reset(shared_pool pool)
    {
        pool.seed_object = null;
        pool.recycled_objects = null;
        pool.enumeration_counter = null;
    }

    public static int cores_count = 1;
    public static volatile int cores_to_use = 1;
    public static bool isparallelcontext()
    {
        return false;
    }

}
