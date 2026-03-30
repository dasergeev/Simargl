using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace CompInfo;

public class CrossPlatformCompInfo
{
    /// <summary>
    /// Получение информации об операционной системе.
    /// </summary>
    /// <returns>Возвращает информацию об операционной системе.</returns>
    public static string GetOSInfo()
    {
        // Получение объекта операционной системы из окружения.
        OperatingSystem os = Environment.OSVersion;        
        StringBuilder osInfo = new();
        
        // Версия платформы ОС.
        osInfo.AppendLine(os.Platform.ToString());

        string osName = string.Empty;

        if (OperatingSystem.IsAndroid())    osName = "Android";
        if (OperatingSystem.IsLinux())      osName = "Linux";
        if (OperatingSystem.IsWindows())    osName = "Windows";
        if (OperatingSystem.IsIOS())        osName = "IOS";
        if (OperatingSystem.IsFreeBSD())    osName = "FreeBSD";
        if (OperatingSystem.IsBrowser())    osName = "Browser";
        if (OperatingSystem.IsMacOS())      osName = "MacOS";
        if (OperatingSystem.IsMacCatalyst()) osName = "MacCatalyst";
        if (OperatingSystem.IsTvOS())       osName = "TvOS";
        if (OperatingSystem.IsWatchOS())    osName = "WatchOS";

        osInfo.AppendLine(osName);

        // Информация о разрядности ОС.
        if (Environment.Is64BitOperatingSystem)
            osInfo.AppendLine("64bit");
        else
            osInfo.AppendLine("32bit");

        // Информация о названии хоста.
        osInfo.AppendLine(Environment.MachineName);

        // Информация о пользователе.
        osInfo.AppendLine(Environment.UserName);

        // Время последней загрузки системы.
        osInfo.AppendLine(DateTime.Now.AddMilliseconds(-Environment.TickCount64).ToString());

        // Установленная версия CLR.
        osInfo.AppendLine(Environment.Version.ToString());

        return osInfo.ToString();
    }

    /// <summary>
    /// Получение Mac адреса сетевой карты, которая включена и у которой настроен шлюз по умолчанию.
    /// </summary>
    /// <returns>Если найден MAC адрес соответсвующий условиям, то возращается строка с адресом, иначе null.</returns>
    public static string? GetMacAddressFromDefaultConnection()
    {
        PhysicalAddress? interfaceAddress;

        try
        {
            // Получение физического адреса. Проверка на активность сетевой карты и установки на данном интерфейсе шлюза по умолчанию.
            interfaceAddress = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up
                    && n.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    && n.GetIPProperties().GatewayAddresses.Count > 0)
                .OrderByDescending(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Select(n => n.GetPhysicalAddress())
                .FirstOrDefault();
        }
        catch (NetworkInformationException)
        {
            return null;
        }

        if (interfaceAddress is not null)
            return interfaceAddress.ToString();

        return null;
    }

    /// <summary>
    /// Получение всех IPv4 адресов
    /// </summary>
    /// <returns>Коллекция IPv4 адресов устройства или null если не получилось определить.</returns>
    public static IEnumerable<IPAddress>? GetAllIpAddress()
    {
        IEnumerable<IPAddress> iPv4;

        try
        {
            iPv4 = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(i => i.GetIPProperties().UnicastAddresses)
                .SelectMany(u => u)
                .Where(u => u.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(i => i.Address)
                .ToArray();
        }
        catch (NetworkInformationException)
        {
            return null;
        }

        return iPv4;
    }
}
