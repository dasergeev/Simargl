global using Apeiron.Platform.Utilities.Generation.Attributes;
using Apeiron.Platform.Utilities.Generation;
using System.Runtime.CompilerServices;

//  Блок перехвата всех исключений для вывода информации в консоль.
try
{
    //  Получение пути к корневому каталогу для генерации файлов.
    string rootPath = getRootPath();

    //  Вывод информации о корневом каталоге для генерации файлов.
    Console.WriteLine($"Корневой каталог для генерации файлов: \"{rootPath}\"");

    //  Создание генератора кода.
    Generator generator = new(rootPath);

    //  Выполнение работы генератора.
    generator.Invoke();

    //Tester.Invoke();
}
catch (Exception ex)
{
    //  Вывод информации об исключении в консоль.
    Console.WriteLine(ex.ToString());
}

//  Возвращает путь к корневому каталогу для генерации файлов.
static string getRootPath()
{
    //  Получение информации о текущем файле.
    FileInfo fileInfo = new(getCurrentCsFile());

    //  Получение ифнормации о каталоге, содержащим текущий файл.
    DirectoryInfo? directoryInfo = fileInfo.Directory;

    //  Проверка ссылки на каталог.
    if (directoryInfo is not null)
    {
        //  Переход к родительскому каталогу.
        directoryInfo = directoryInfo.Parent;

        //  Проверка ссылки на каталог.
        if (directoryInfo is not null)
        {
            //  Переход к родительскому каталогу.
            directoryInfo = directoryInfo.Parent;

            //  Проверка ссылки на каталог.
            if (directoryInfo is not null)
            {
                //  Формирование пути к корневому каталогу.
                return Path.Combine(
                    directoryInfo.FullName,
                    "Databases",
                    "CentralDatabase",
                    "Generated");
            }
        }
    }

    //  Генерация исключения.
    throw new IOException("Не удалось определить корневой каталог для генерации файлов.");

    //  Вспомогательная функция для получения информации о текущем файле.
    static string getCurrentCsFile([CallerFilePath] string filePath = "")
    {
        //  Возврат текущего файла.
        return filePath;
    }
}
