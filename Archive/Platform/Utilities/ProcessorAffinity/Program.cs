// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

//  Перебор всех процессов.
foreach (Process process in Process.GetProcesses())
{
    try
    {
        if (process.ProcessName == "sqlservr")
        {
            process.ProcessorAffinity = new IntPtr(0x3FC);
        }
        else
        {
            process.ProcessorAffinity = new IntPtr(0x3);
        }

        Console.WriteLine($"{process.ProcessName}: {process.ProcessorAffinity}");
    }
    catch
    {
        Console.WriteLine($"{process.ProcessName}: -");
    }
}
