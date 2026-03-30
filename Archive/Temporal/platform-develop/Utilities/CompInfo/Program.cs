using CompInfo;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

const string welcomeText = "Информация о компьютере.\n\n\n\r";

// Выводим строку в центре экрана.
Console.Clear();
int centerX = (Console.WindowWidth / 2) - (welcomeText.Length / 2);
Console.SetCursorPosition(centerX, 2);
Console.ForegroundColor = ConsoleColor.Blue;
Console.Write(welcomeText);
Console.ResetColor();

//  Проверка платформы.
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    Console.WriteLine("Используемая операционная система - Windows\n");

    // Получение информации об операционной системе.
    Console.WriteLine(CrossPlatformCompInfo.GetOSInfo());

    // Получение MAC адреса сетевой карты.
    Console.WriteLine(CrossPlatformCompInfo.GetMacAddressFromDefaultConnection());

    Console.WriteLine("\n");
    if (CrossPlatformCompInfo.GetAllIpAddress() is IEnumerable<IPAddress> addresses)
    {
        foreach (var item in addresses)
        {
            Console.WriteLine($"{item}");
        }
    }

    Console.WriteLine("\n");

    OutputResult("Процессор:", GetHardwareInfo("Win32_Processor", "Name"));
    OutputResult("Процессор:", GetHardwareInfo("Win32_Processor", "ProcessorId"));
    OutputResult("Видеокарта:", GetHardwareInfo("Win32_VideoController ", "Name"));
    OutputResult("Bios:", GetHardwareInfo("Win32_BIOS", "Manufacturer"));

    Console.WriteLine(WindowsCompInfo.GetWindowsInstallationDateTime().ToString());

    //Console.WriteLine(CrossPlatformCompInfo.GetMacAddress());

    Console.WriteLine("\n");

    //Console.WriteLine(GetUUID());


    Console.WriteLine("\n");

    ShowSystemInfoFromWMI();
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    Console.WriteLine("Используемая операционная система - Linux\n");

    Console.WriteLine(CrossPlatformCompInfo.GetOSInfo());

    // Получение MAC адреса сетевой карты.
    Console.WriteLine(CrossPlatformCompInfo.GetMacAddressFromDefaultConnection());

    Console.WriteLine("\n");
    if (CrossPlatformCompInfo.GetAllIpAddress() is IEnumerable<IPAddress> addresses)
    {
        foreach (var item in addresses)
        {
            Console.WriteLine($"{item}");
        }
    }

    Console.WriteLine("\n");


    Console.WriteLine(LinuxCompInfo.GetProcessorInfo());

    Console.WriteLine(LinuxCompInfo.GetLineInfoFromFile(LinuxCompInfo.UUID));
    Console.WriteLine(LinuxCompInfo.GetLineInfoFromFile(LinuxCompInfo.ProductName));
    Console.WriteLine(LinuxCompInfo.GetLineInfoFromFile(LinuxCompInfo.ProductSerial));
}


static List<string> GetHardwareInfo(string WIN32_Class, string ClassItemField)
{
    List<string> result = new();

    ManagementObjectSearcher searcher = new("SELECT * FROM " + WIN32_Class);

    try
    {
        foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
        {
            if (obj[ClassItemField].ToString() is string text)
            {
                result.Add(text.Trim());
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    return result;
}


static void OutputResult(string info, List<string> result)
{
    if (info.Length > 0)
        Console.WriteLine(info);

    if (result.Count > 0)
    {
        for (int i = 0; i < result.Count; ++i)
            Console.WriteLine(result[i]);
    }
}



static void ShowSystemInfoFromWMI()
{
    StringBuilder systemInfo = new();
    ManagementClass manageClass = new("Win32_ComputerSystemProduct");
    //ManagementClass manageClass = new ManagementClass("Win32_Processor");
    //ManagementClass manageClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
    
    // Получаем все экземпляры класса
    ManagementObjectCollection manageObjects = manageClass.GetInstances();
    
    // Получаем набор свойств класса
    PropertyDataCollection properties = manageClass.Properties;
    
    foreach (ManagementObject obj in manageObjects.Cast<ManagementObject>())
    {
        foreach (PropertyData property in properties)
        {
            try
            {
                if (obj.Properties[property.Name].Value is null)
                    continue;

                systemInfo.AppendLine(property.Name + ":  " +
                                obj.Properties[property.Name].Value.ToString());
            }
            catch (NullReferenceException)
            {
                continue;
            }
        }
        systemInfo.AppendLine();
    }
    Console.WriteLine(systemInfo);
}