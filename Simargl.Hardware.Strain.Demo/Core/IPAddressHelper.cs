using System.Net;

namespace Simargl.Hardware.Strain.Demo.Core;

/// <summary>
/// Предоставляет вспомогательные методы для <see cref="IPAddress"/>.
/// </summary>
public static class IPAddressHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ip1"></param>
    /// <param name="ip2"></param>
    /// <returns></returns>
    public static bool Equals(IPAddress ip1, IPAddress ip2)
    {
        if (ip1 == null || ip2 == null)
            return false;

        byte[] bytes1 = ip1.GetAddressBytes();
        byte[] bytes2 = ip2.GetAddressBytes();

        if (bytes1.Length != bytes2.Length)
            return false;

        for (int i = 0; i < bytes1.Length; i++)
        {
            if (bytes1[i] != bytes2[i])
                return false;
        }

        return true;
    }
}
