using System.Diagnostics;
using System.Text;

namespace CompInfo;

/// <summary>
/// Класс для получения информации компьютера с установленной ОС Linux.
/// </summary>
public static class LinuxCompInfo
{
    public const string UUID = @"/sys/class/dmi/id/product_uuid";
    public const string ProductName = @"/sys/class/dmi/id/product_name";
    public const string ProductSerial = @"/sys/class/dmi/id/product_serial";
   

    /// <summary>
    /// Получение информации из файлов в ОС Linux.
    /// </summary>
    /// <returns>Считанный параметр в строковом формате.</returns>
    public static string? GetLineInfoFromFile(string parameter)
    {
        if (!string.IsNullOrWhiteSpace(parameter))
        {
            string? line;

            try
            {
                using StreamReader stream = new(parameter);
                {
                    line = stream.ReadLine();
                }

                return line;
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Получение информации о процессоре.
    /// </summary>
    /// <returns>Информация о процессоре в строковом формате.</returns>
    public static string GetProcessorInfo()
    {
        StringBuilder processorInfo = new();

        // Получение информации о версии процессора.
        processorInfo.AppendLine("dmidecode -t processor | grep -E Version | sed 's/.*: //' | head -n 1".Bash());
        // Получение информации об идентификаторе процессора.
        processorInfo.AppendLine("dmidecode -t processor | grep -E ID | sed 's/.*: //' | head -n 1".Bash());
        // Получение информации о дате установки системы.
        processorInfo.AppendLine("ls -clt / | tail -n 1 | awk '{ print $7, $6, $8 }'".Bash());

        return processorInfo.ToString();
    }

    /// <summary>
    /// Метод расширения для выполнения команды Linux в Bash.
    /// </summary>
    /// <param name="cmd">Команда для выполнения в формате строки.</param>
    /// <returns>Строка с результатом выполнения команды.</returns>
    public static string Bash(this string cmd)
    {
        string result = string.Empty;

        try
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            using (Process process = new())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                process.Start();
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit(1500);
                process.Kill();
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка выполнения shell команды: {cmd}", ex);
        }
        return result;
    }
}
