using System;

namespace Simargl.Algorithms.Raw;

/********************************************************************
ALGLIB object, parent class for all user-visible objects  managed  by
ALGLIB.

Methods:
    _deallocate()       deallocation:
                        * in managed ALGLIB it does nothing
                        * in native ALGLIB it clears  dynamic  memory
                          being  hold  by  object  and  sets internal
                          reference to null.
    make_copy()         creates deep copy of the object.
                        Works in both managed and native versions  of
                        ALGLIB.
********************************************************************/

/// <summary>
/// 
/// </summary>
public abstract class alglibobject :
    IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public virtual void _deallocate() { }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract alglibobject make_copy();

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        _deallocate();
    }
}
