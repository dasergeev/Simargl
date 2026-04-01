//using Simargl.Platform.Journals;
//using System;
//using Simargl.Designing;
//using System.IO;
//using Simargl.Support;
//using System.Threading.Tasks;
//using System.Threading;
//using static Simargl.Designing.Verify;

//namespace Simargl.Platform.Transmitters;

///// <summary>
///// Представляет класс передатчика, конечной точкой которого является жесткий диск.
///// </summary>
//internal class FileTransmitter : ITransmitter
//{
//    /// <summary>
//    /// Представляет интерфейс логирования.
//    /// </summary>
//    private readonly Journal _Journal;

//    /// <summary>
//    /// Представляет путь до корневого каталога.
//    /// </summary>
//    internal string RootPath { get; init; }

//    /// <summary>
//    /// Представляет имя папки передатчика.
//    /// </summary>
//    internal string FolderName { get; init; }

//    /// <summary>
//    /// Представляет имя рассширения передатчика.
//    /// </summary>
//    internal string ExtensionName { get; init; }

//    /// <summary>
//    /// Инициализирует экземпляр класса.
//    /// </summary>
//    /// <param name="journal">
//    /// Журнал.
//    /// </param>
//    /// <param name="rootPath">
//    /// Корневой каталог.
//    /// </param>
//    /// <param name="folderName">
//    /// Папка назначения.
//    /// </param>
//    /// <param name="extensionName">
//    /// Имя расширения.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="journal"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="rootPath"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="folderName"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="extensionName"/> передана пустая ссылка.
//    /// </exception>
//    internal FileTransmitter(Journal journal, string rootPath, string folderName,string extensionName)
//    {
//        //  Установка интерфейса логирования.
//        _Journal = IsNotNull(journal, nameof(journal));

//        //  Проверка и сохранения корневого пути
//        RootPath = IsNotNull(rootPath,nameof(rootPath));

//        //  Проверка и сохранение целевой папки.
//        FolderName = IsNotNull(folderName, nameof(folderName));

//        //  Проверка и сохранение расширения
//        ExtensionName = IsNotNull(extensionName, nameof(extensionName));
//    }

//    /// <summary>
//    /// Представляет функцию сохранения массива в файл.
//    /// </summary>
//    /// <param name="data">
//    /// Массив
//    /// </param>
//    public void Send(byte[] data)
//    {
//        try
//        {
//            //  Создание потока файла.
//            using FileStream stream = new(GetFilePath(), FileMode.Append);

//            //  Запись данных в файл.
//            stream.Write(data, 0, data.Length);

//            //  Выгрузка на диск 
//            stream.Flush();

//            //  Закрытие файла 
//            stream.Close();
//        }
//        catch (Exception ex)
//        {
//            //  Проверка исключения.
//            if(ex.IsSystem())
//            {
//                //  Выброс исключения.
//                throw;
//            }

//            //  Логирование исключения.
//            _ = Task.Run(async()=> await _Journal.LogErrorAsync($"FileTransmitter:{ex.Message}", default).ConfigureAwait(false));
//        }
//    }

//    /// <summary>
//    /// Представляет функцию сохранения массива в файл асинхронно.
//    /// </summary>
//    /// <param name="data">
//    /// Массив
//    /// </param>
//    /// <param name="token">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача.
//    /// </returns>
//    public async Task SendAsync(byte[] data, CancellationToken token)
//    {
//        try
//        {
//            //  Создание потока файла.
//            using FileStream stream = new(GetFilePath(), FileMode.Append);

//            //  Запись данных в файл.
//            await stream.WriteAsync(data, token).ConfigureAwait(false);

//            //  Выгрузка на диск.
//            await stream.FlushAsync(token).ConfigureAwait(false);

//            //  Закрытие файла 
//            stream.Close();

//        }
//        catch(Exception ex)
//        {

//            //  Проверка исключения.
//            if (ex.IsSystem())
//            {
//                //  Выброс исключения.
//                throw;
//            }
//            //  Логирование исключения.
//            await _Journal.LogErrorAsync($"FileTransmitter:{ex.Message}", token).ConfigureAwait(false);

//        }
//    }


//    /// <summary>
//    /// Представляет функцию создания директории, если она отсутствует.
//    /// </summary>
//    /// <param name="path"></param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="path"/> переданна пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="path"/> переданна пустая строка.
//    /// </exception>
//    private static string CreateFolderIfNotExist(string path)
//    {
//        //  Проверка на пустую ссылку и пустую строку.
//        IsNotNull(path, nameof(path));
//        Verify.IsNotEmpty(path, nameof(path));

//        //  Перехват не системных исключений.
//        safeNotSystem(delegate
//        {
//            //  Проверка каталога.
//            if (!Directory.Exists(path))
//            {
//                //  Создание каталога.
//                Directory.CreateDirectory(path);
//            }
//        });

//        static void safeNotSystem(Action action)
//        {
//            //  Проверка ссылки на действие.
//            action = IsNotNull(action, nameof(action));

//            //  Блок перехвата несистемных исключений.
//            try
//            {
//                //  Выполнение действия.
//                action();
//            }
//            catch (Exception ex)
//            {
//                //  Проверка системного исключения.
//                if (ex.IsSystem())
//                {
//                    //  Повторный выброс исключения.
//                    throw;
//                }
//            }
//        }

//        return path;
//    }

//    /// <summary>
//    /// Представляет функцию получения пути файла.
//    /// </summary>
//    /// <returns>
//    /// Путь к файлу.
//    /// </returns>
//    private string GetFilePath()
//    {
//        //  Получение пути к корневому каталогу.
//        string path = CreateFolderIfNotExist(RootPath);

//        //  Получение времени.
//        var time = DateTime.Now;

//        //  Получение пути к каталогу по времени.
//        path = CreateFolderIfNotExist(Path.Combine(path, $"{time:yyyy-MM-dd-HH}"));

//        //  Получение пути к каталогу датчика.
//        path = CreateFolderIfNotExist(Path.Combine(path, $"{FolderName}"));

//        //  Получение имени файла.
//        path = Path.Combine(path, $"{time:yyyy-MM-dd-HH-mm}.{ExtensionName}");

//        //  Возврат значения.
//        return path;
//    }
//}
