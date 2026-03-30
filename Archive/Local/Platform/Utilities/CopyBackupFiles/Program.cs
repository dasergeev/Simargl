using CopyBackupFiles;
using System.Text;

// Константы
const string localPath = @"\\10.69.16.236\Backup";
const string remotePath = @"\\10.69.16.250\NTO_Files\Platform\Archive\Databases\SQLBase";
const string welcomeText = "Копирование файлов Backup.\n\n\n\r";

// Выводим строку в центре экрана.
Console.Clear();
int centerX = (Console.WindowWidth / 2) - (welcomeText.Length / 2);
Console.SetCursorPosition(centerX, 2);
Console.ForegroundColor = ConsoleColor.Blue;
Console.Write(welcomeText);
Console.ResetColor();

Console.Write($"Время запуска - {DateTime.Now}\n\r");
Console.Write($"Локальная директория - {localPath}\n\r");
Console.Write($"Директория на файловом сервере - {remotePath}\n\n\r");

// Создаём Mail Agent для отправки почты.
MailAgent mailAgent = new();

// Текст письма для отправки.
StringBuilder letterBody = new("<h3>Резерное копирование баз SQL на файловый сервер</h3><br>");

// Получаем список файлов для копированя в удалённую папку.
ICollection<string> files = SearchFiles(localPath);

Console.WriteLine("Найдены следующие файлы в локальной директории\n\r");

letterBody.AppendLine($"Время запуска - {DateTime.Now}<br><br>");
letterBody.AppendLine($"<b>Найдены следующие файлы в локальной директории ( {localPath} ):</b><br>");
foreach (var item in files)
{
    Console.WriteLine(item);
    letterBody.AppendLine(item);
    letterBody.AppendLine("<br>");
}

// Копирование файлов в удалённый каталог.
CopyFiles(files, localPath, remotePath);

// Отправка письма
mailAgent.SendMail($"SQL Base Copy Service - {DateTime.Now}", letterBody.ToString());



// Ищет файлы для копирования в локальной папке.
ICollection<string> SearchFiles(string localPath)
{
    if (localPath is null)
        throw new ArgumentNullException(nameof(localPath), "Передан пустой путь к локальной директории");

    try
    {
        //  Получение всех фалов в указанном каталоге.
        ICollection<string> files = Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
            .Select(x => x)
            .ToList();

        return files;
    }
    catch (Exception ex)
    {
        letterBody.Append(ex.ToString());

        // Отправка письма
        mailAgent.SendMail($"FAIL!!! SQL Base Copy Service - {DateTime.Now}", letterBody.ToString());

        throw new IOException($"Ошибка получения списка файлов в локальной директории - {ex}");
    }
}


// Копирование файлов в удалённую директорию.
void CopyFiles(ICollection<string> files, string localPath, string remotePath)
{
    if (files is null)
        throw new ArgumentNullException(nameof(files), "Передан пустой список файлов для копирования");

    if (localPath is null)
        throw new ArgumentNullException(nameof(localPath), "Передан пустой путь к локальной директории");

    if (remotePath is null)
        throw new ArgumentNullException(nameof(remotePath), "Передан пустой путь к удалённой директории");

    if (files.Count > 0)
    {
        try
        {
            const string message = "Список файлов скопированных в удалённую директорию";
            Console.WriteLine($"\n\r{message}\n\r");
            letterBody.AppendLine($"<br><b>{message} ( {remotePath} ):</b><br>");

            ICollection<string> directories = Directory.GetDirectories(localPath, "*", SearchOption.AllDirectories);
            if (directories.Count > 0)
            {
                //Создать идентичное дерево каталогов
                foreach (string dirPath in directories)
                {
                    // Получаем новый путь к каталогу.
                    string newDirectory = dirPath.Replace(localPath, remotePath);
                    // Проверяем и создаём каталог.
                    if (!Directory.Exists(newDirectory))
                        Directory.CreateDirectory(newDirectory);
                }
            }

            //Скопировать все файлы. И перезаписать(если такие существуют)
            foreach (string newPath in files)
            {
                string newFullFileName = newPath.Replace(localPath, remotePath);
                if (!File.Exists(newFullFileName))
                {
                    File.Copy(newPath, newFullFileName, false);
                    Console.WriteLine(newPath);
                    letterBody.AppendLine(newPath);
                    letterBody.AppendLine("<br>");
                }
            }

            Console.WriteLine($"Копирование завершено - {DateTime.Now}");
            letterBody.AppendLine($"<br><br>Время завершения - {DateTime.Now}");
        }
        catch (Exception ex)
        {
            letterBody.Append(ex.ToString());

            // Отправка письма
            mailAgent.SendMail($"FAIL!!! SQL Base Copy Service - {DateTime.Now}", letterBody.ToString());

            throw new IOException($"Ошибка создания папки или файла - {ex}");
        }       
    }
}