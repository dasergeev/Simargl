using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace CompInfo;

/// <summary>
/// Класс для получения информации компьютера с установленной ОС Windows.
/// </summary>
public class WindowsCompInfo
{
    /// <summary>
    /// Возвращает дату и время установки системы. 
    /// </summary>
    /// <returns>Дата и время уставноки системы. При возникновении ошибок возращает минимальную дату.</returns>
    public static DateTime GetWindowsInstallationDateTime()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                string? date = key?.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion")?.GetValue("InstallDate")?.ToString();               

                DateTime dateDef = new(1970, 1, 1, 0, 0, 0, kind: DateTimeKind.Utc);
                
                if (!string.IsNullOrEmpty(date))
                    return TimeZoneInfo.ConvertTime(dateDef.AddSeconds(Convert.ToInt64(date)), TimeZoneInfo.Local);
                else
                    return DateTime.MinValue;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        return DateTime.MinValue;
    }

}
