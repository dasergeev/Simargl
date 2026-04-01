using System;

namespace Simargl.Algorithms.Raw;

/// <summary>
/// 
/// </summary>
public class alglibexception :
    Exception
{
    /// <summary>
    /// 
    /// </summary>
    public string msg;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public alglibexception(string s)
    {
        msg = s;
    }
}
