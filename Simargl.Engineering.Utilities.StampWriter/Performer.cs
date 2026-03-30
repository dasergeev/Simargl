using System.IO;
using System.Runtime.InteropServices;

namespace Simargl.Engineering.Utilities.StampWriter;

/// <summary>
/// Представляет исполнителя.
/// </summary>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
public sealed class Performer(CancellationToken cancellationToken) :
    IDisposable
{
    /// <summary>
    /// Поле для хранения приложения.
    /// </summary>
    private dynamic? _Application = null;

    /// <summary>
    /// Поле для хранения стека объектов.
    /// </summary>
    private readonly Stack<dynamic> _Stack = [];

    /// <summary>
    /// Выполняет основную работу.
    /// </summary>
    public void Invoke()
    {
        //  Вывод сообщения в журнал.
        Log("Начало работы");

        //  Создание приложения.
        _Application = Get("Kompas.Application.7");

        //  Установка видимости приложения.
        _Application.Visible = true;

        //  Установка для блокировки сообщений.
        _Application.HideMessage = 1;

        //  Получение коллекции документов.
        dynamic documents = _Application.Documents;

        //  Корневой путь.
        //const string rootPath = "Y:\\МПТ\\999 Транзит\\5213.40.01.000-2 Установка элементов крепления\\";
        //const string rootPath = "C:\\КД\\";
        const string rootPath = "C:\\KD2\\";
        //
        //const string rootPath = "Y:\\МПТ\\200 КО\\112 ВЭИП\\113 АРВ Харитонов\\124 КД_PDF_CDW_SPW_5213_ (на 09.09.25)\\5213.00.00.000\\";

        //  Перебор расширений.
        foreach (string extension in new string[] { "*.cdw", "*.spw" })
        {
            //  Перебор файлов.
            foreach (FileInfo file in new DirectoryInfo(rootPath)
                .GetFiles(extension, SearchOption.AllDirectories))
            {
                //  Проверка токена отмены.
                cancellationToken.ThrowIfCancellationRequested();

                //  Блок перехвата всех исключений.
                try
                {
                    //  Работа с файлом.
                    write(file);
                }
                catch (Exception ex)
                {
                    //  Вывод в журнал.
                    Log($"Ошибка при работе с файлом \"{file.FullName}\": {ex}");
                }
            }
        }

        //  Записывает данные в файл.
        void write(FileInfo file)
        {
            //  Открытие документа.
            dynamic document = Get(() => documents.Open(file.FullName, true, false));
            //dynamic document = Get(() => documents.OpenEx(file.FullName, true, false, Type.Missing));


            //  !!! ПЕРЕСТРОЕНИЕ !!!

            try
            {
                //  Перестроение документа.
                document.RebuildDocument();
            }
            catch
            {

                try
                {
                    //  Закрытие документа.
                    document.Close(1);
                }
                catch { }

                //  Открытие документа.
                document = Get(() => documents.Open(file.FullName, true, false));
            }

            //  Получение листов оформления.
            dynamic sheets = Get(() => document.LayoutSheets);

            //  Получение первого листа.
            dynamic sheet = Get(() => sheets.Item(0));

            //  Получение штампа.
            dynamic stamp = Get(() => sheet.Stamp);

            //  Заполнение штампа.
            stamp.Text(140).Str = "";  //  Номер версии
            stamp.Text(160).Str = "ЦНРФ/3-2025";  //  ок

            stamp.Text(170).Str = "Подписано электронно";  //  ок
            //                                               //stamp.Text(170).TextLine(0).TextItem.SizeFactor = 0.75;

            //stamp.Text(170).Str = "Подписано";
            //stamp.Text(170).Add().Str = "электронно";


            stamp.Text(180).Str = "18.08.2025";

            //stamp.Text(110).Str = "Разработал";
            stamp.Text(120).Str = "Подписано электронно";  //  ок
            stamp.Text(130).Str = "17.08.2025"; //  Разработал

            stamp.Text(121).Str = "Подписано электронно";  //  ок
            stamp.Text(131).Str = "17.08.2025"; //  Разработал

            stamp.Text(124).Str = "Подписано электронно";  //  ок
            stamp.Text(134).Str = "17.08.2025"; //  Разработал

            stamp.Text(115).Str = "";
            stamp.Text(125).Str = "";
            stamp.Text(135).Str = ""; //  Утв.

            stamp.Text(40).Str = "О";

            //stamp.Text(111).Str = "Проверил"; stamp.Text(131).Str = "Пров"; //  Проверил
            //stamp.Text(112).Str = "Т.контр."; stamp.Text(132).Str = "Т.к"; //  Т.контр.
            //stamp.Text(114).Str = "Н.контр."; stamp.Text(134).Str = "Н.к"; //  Н.контр.

            //  Обновление штампа.
            stamp.Update();

            //  Сохранение документа.
            document.Save();

            //  Получение пути для сохранения в pdf.
            string path = string.Concat(file.FullName.AsSpan(0, file.FullName.Length - file.Extension.Length), ".pdf");

            //  Сохранение в формате pdf.
            document.SaveAs(path);

            //  Закрытие документа.
            document.Close(1);

            //  Вывод сообщения в журнал.
            Log(file.FullName);
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            Thread.Sleep(100);
        }
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Закрытие приложения.
            _Application?.Quit();
        }
        catch { }

        //  Блок перехвата всех исключений.
        try
        {
            //  Извлечение объектов из стека.
            while (_Stack.TryPop(out dynamic? obj))
            {
                //  Проверка объекта.
                if (obj is not null)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Освобождение объекта.
                        Marshal.ReleaseComObject(obj);
                    }
                    catch { }
                }
            }
        }
        catch { }

        //  Блок перехвата всех исключений.
        try
        {
            //  Сборка мусора.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        catch { }
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    public static Heart Heart => ((App)Application.Current).Heart;

    /// <summary>
    /// Добавляет сообщение в очередь.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо добавить в очередь.
    /// </param>
    public void Log(string message)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Вывод сообщения в журнал.
        Heart.Log(message);
    }

    /// <summary>
    /// Получает объект по програмному идентификатору.
    /// </summary>
    /// <param name="guid">
    /// Уникальный идентификатор.
    /// </param>
    /// <returns>
    /// Полученный объект.
    /// </returns>
    private dynamic Get(Guid guid)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Получение типа.
        Type type = Type.GetTypeFromCLSID(guid) ??
            throw new InvalidOperationException($"Не удалось найти тип с идентификатором \"{guid}\"");

        //  Получение объекта.
        return Get(() => Activator.CreateInstance(type) ??
            throw new InvalidOperationException($"Не удалось создать объект типа с идентификатором \"{guid}\""));
    }

    /// <summary>
    /// Получает объект по програмному идентификатору.
    /// </summary>
    /// <param name="progID">
    /// Програмный идентификатор.
    /// </param>
    /// <returns>
    /// Полученный объект.
    /// </returns>
    private dynamic Get(string progID)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Получение типа.
        Type type = Type.GetTypeFromProgID(progID) ??
            throw new InvalidOperationException($"Не удалось найти тип \"{progID}\"");

        //  Получение объекта.
        return Get(() => Activator.CreateInstance(type) ??
            throw new InvalidOperationException($"Не удалось создать объект типа \"{progID}\""));
    }

    /// <summary>
    /// Получает объект.
    /// </summary>
    /// <param name="action">
    /// Метод получающий объект.
    /// </param>
    /// <returns>
    /// Полученный объект.
    /// </returns>
    private dynamic Get(Func<dynamic?> action)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Получение объекта.
        dynamic? obj = action();

        //  Прикрепление объекта.
        return Attach(obj);
    }

    /// <summary>
    /// Прикрепляет объект.
    /// </summary>
    /// <param name="obj">
    /// Прикрепляемый объект.
    /// </param>
    /// <returns>
    /// Прикреплённый объект.
    /// </returns>
    private dynamic Attach(dynamic? obj)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Проверка объекта.
        if (obj is null)
        {
            //  Выброс исключения.
            throw new ArgumentNullException(nameof(obj));
        }
        else
        {
            //  Проверка объекта.
            if (Marshal.IsComObject(obj))
            {
                //  Добавление в стек.
                _Stack.Push(obj);

                //  Проверка токена отмены.
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Освобождение объекта.
                        Marshal.ReleaseComObject(obj);
                    }
                    catch { }
                }
            }

            //  Возврат проверенного объекта.
            return obj;
        }
    }
}

static class Rot
{
    [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
    private static extern int CLSIDFromProgID(string progID, out Guid clsid);

    [DllImport("oleaut32.dll")]
    private static extern int GetActiveObject(ref Guid rclsid, IntPtr reserved,
        [MarshalAs(UnmanagedType.Interface)] out object ppunk);

    public static dynamic FromProgID(string progID)
    {
        Guid clsid;
        if (CLSIDFromProgID(progID, out clsid) != 0) throw new Exception("Нет ProgID");
        object obj;
        if (GetActiveObject(ref clsid, IntPtr.Zero, out obj) != 0) throw new Exception("Объект не найден в ROT");
        return obj;
    }
}