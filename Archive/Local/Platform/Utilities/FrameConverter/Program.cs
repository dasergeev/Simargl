//using Apeiron.Platform.Platform.IO;
using Apeiron.Frames;
using Apeiron.IO;
using Apeiron.Platform.Utilities;
using System.Text.RegularExpressions;

const string welcomeText = "Конвертер файлов.\n\n\n\r";

// Выводим строку в центре экрана.
Console.Clear();
int centerX = (Console.WindowWidth / 2) - (welcomeText.Length / 2);
Console.SetCursorPosition(centerX, 2);
Console.ForegroundColor = ConsoleColor.Blue;
Console.Write(welcomeText);
Console.ResetColor();

Console.WriteLine("Поиск файлов для конвертации в текущем рабочем каталоге и всех подкаталогах.\n\r");

// Список файлов.
List<FileProp> filesInSourcePath = new();

try
{
    ////Паттерн выборки исполняемых файлов.
    var searchPattern = new Regex(@"$(?<=\.(exe))", RegexOptions.IgnoreCase);

    // Получение списка файлов.
    filesInSourcePath = Directory.EnumerateFiles(".", "*", SearchOption.AllDirectories)
        .Where(f => !searchPattern.IsMatch(f))
        .Select((path) =>
            new FileProp
            {
                FilePath = PathBuilder.Normalize(path),
                FileName = Path.GetFileName(path),
            })
        .ToList();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Ошибка получения файлов исходных файлов для конвертации! - {ex}");
    Console.ResetColor();
}

Console.WriteLine("Для запуска конвертера нажмите любую клавишу.");
Console.ReadKey();

// Если есть исходные файлы для конвертации.
if (filesInSourcePath.Count > 0)
{
    // Цикл по файлам.
    foreach (var itemFile in filesInSourcePath)
    {
        const string firstMessage = "Исходный файл - ";
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("{0,-25}{1}", firstMessage, itemFile.FilePath);
        Console.ResetColor();

        List<int> channelLenght = new();

        StringBuilder csvHeaderName = new();
        StringBuilder csvHeaderSampling = new();

        try
        {
            // Создание фрейма.
            Frame frame = new(itemFile.FilePath);

            if (frame.Format != StorageFormat.TestLab)
            {
                throw new FormatException("Ошибка формата исходного файла.");
            }

            // Создание средства записи в файл.
            using StreamWriter writer = new(Path.Combine(".", itemFile.FileName) + ".csv", false, Encoding.UTF8);

            csvHeaderName.Append("Имя канала" + ";");
            csvHeaderSampling.Append("Частота дискретизации" + ";");

            // Формирование заголовка с названиями каналов и частотой дискретизации.
            foreach (var itemChannel in frame.Channels)
            {
                channelLenght.Add(itemChannel.Length);

                csvHeaderName.Append(itemChannel.Name).Append(';');
                csvHeaderSampling.Append(itemChannel.Sampling).Append(';');
            }

            writer.WriteLine(csvHeaderName.ToString());
            writer.WriteLine(csvHeaderSampling.ToString());

            for (int i = 0; i < channelLenght.Max(); i++)
            {
                // Запись N П/П.
                writer.Write((i + 1).ToString() + ";");

                // Цикл по всем каналам.
                foreach (var itemChannel in frame.Channels)
                {
                    if (i < itemChannel.Length)
                    {
                        writer.Write(itemChannel.Items[i]);
                    }

                    writer.Write(";");
                }

                writer.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Сконвертированный файл - {Path.Combine(itemFile.FileName)}" + ".csv");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка при обработки файла - {itemFile.FileName} \n - {ex}");
            Console.ResetColor();
        }
    }

    Console.WriteLine("\nДля выхода нажмите любую клавишу.");
    Console.ReadKey();
}

//foreach (var file in new DirectoryInfo("").GetFiles("*", SearchOption.AllDirectories))
//{
//    try
//    {
//        Frame frame = new(file.FullName);

//        if (frame.Format != StorageFormat.TestLab)
//        {
//            throw new Exception();
//        }
//    }
//    catch (Exception)
//    {

//    }
//}