using Apeiron.Frames;
using Apeiron.Platform.Heap;
using Excel = Microsoft.Office.Interop.Excel;

namespace Ape90ExpressAnalysis;

/// <summary>
/// Представляет анализатор кадров.
/// </summary>
internal static class Analyzer
{
    //  Каталог с исходными данными.
    private static readonly DirectoryInfo _SourceDirectory = new(@"C:\Data\Source");

    //  Каталог с исходными данными.
    private static readonly DirectoryInfo _TargetDirectory = new(@"C:\Data\Target");

    //  Каталог с исходными данными.
    private static readonly DirectoryInfo _CompactDirectory = new(@"C:\Data\Compact");

    //  Каталог, содержащий петли.
    private static readonly DirectoryInfo _LoopsDirectory = new(@"C:\Data\Loops");

    //  Каталог, содержащий петли.
    private static readonly DirectoryInfo _SourceCsvDirectory = new(@"C:\Data\SourceCsv");

    //  Количество дней.
    private const int _Days = 7;

    //  Количество секунд в кадре.
    private const int _Seconds = 24 * 3600;

    //  Минимальное перемещение.
    private const double _MinMove = 5;

    //  Минимальное количество точек в петле.
    private const int _MinLoopPoints = 10;

    /// <summary>
    /// Выполняет нормализацию исходных кандов.
    /// </summary>
    public static void Normalization()
    {
        //  Начальное время.
        DateTime beginTime = DateTime.Parse("24.12.2021");

        //  Имена основных каналов.
        string[] mainNames = { "P6_1", "P6_2", "L6_1", "L6_2" };

        //  Имена вспомогательных каналов.
        string[] supportNames = { "V_GPS" };

        //  Перебор всех дней.
        Parallel.For(0, _Days, index =>
        {
            //  Начальное время.
            DateTime time = beginTime.AddDays(index);

            //  Каталог.
            DirectoryInfo directory = new(Path.Combine(_SourceDirectory.FullName, $"{time:yyyy-MM-dd}"));

            //  Создание длинного кадра.
            Frame targetFrame = new();

            //  Добавление основных каналов.
            foreach (string name in mainNames)
            {
                //  Добавление основного канала.
                targetFrame.Channels.Add(new(name, string.Empty, 1, 1, _Seconds));
            }

            //  Добавление вспомогательных каналов.
            foreach (string name in supportNames)
            {
                //  Добавление вспомогательного канала.
                targetFrame.Channels.Add(new(name, string.Empty, 1, 1, _Seconds));
            }

            //  Получение информации о файлах.
            var files = directory.GetFiles()
                .Select(file => new
                {
                    Path = file.FullName,
                    Name = file.Name[..^file.Extension.Length],
                    IsValid = int.TryParse(file.Extension[1..], out int number),
                    Index = number - 1,
                    Time = time.AddMinutes(number - 1),
                })
                .OrderByDescending(file => file.Index);

            //  Перебор всех файлов.
            Parallel.ForEach(files, file =>
            {
                //  Открытие кадра.
                Frame sourceFrame = new(file.Path);

                //  Перебор основных каналов.
                foreach (string name in mainNames)
                {
                    //  Получение исходного канала.
                    Channel sourceChannel = sourceFrame.Channels[name];

                    //  Получение целевого канала.
                    Channel targetChannel = targetFrame.Channels[name];

                    //  Перебор фрагментов исходного канала.
                    for (int i = 0; i < 60; i++)
                    {
                        //  Запись значения в целевой канал.
                        targetChannel[file.Index * 60 + i] = sourceChannel.Vector.Subvector(i * 1200, 1200).Average();
                    }
                }

                //  Перебор вспомогательных каналов.
                foreach (string name in supportNames)
                {
                    //  Получение исходного канала.
                    Channel sourceChannel = sourceFrame.Channels[name];

                    //  Получение целевого канала.
                    Channel targetChannel = targetFrame.Channels[name];

                    //  Перебор фрагментов исходного канала.
                    for (int i = 0; i < 60; i++)
                    {
                        //  Запись значения в целевой канал.
                        targetChannel[file.Index * 60 + i] = sourceChannel[i];
                    }
                }

                //  Вывод информации о файле.
                Console.WriteLine(file.Path);
            });

            //  Получение канала основного перемещения.
            Channel moveChannel = targetFrame.Channels["L6_2"];

            //  Получение канала основной силы.
            Channel forceChannel = targetFrame.Channels["P6_2"];

            //  Создание канала-индикатора смещения нуля.
            Channel isZero = new("IsZero", string.Empty, 1, 1, _Seconds);

            //  Создание канала работы.
            Channel work = new("Work", string.Empty, 1, 1, _Seconds);

            //  Создание канала времени.
            Channel timeChannel = new("Time", string.Empty, 1, 1, _Seconds);

            //  Создание канала изменения перемещения.
            Channel dMove = new("dMove", string.Empty, 1, 1, _Seconds);

            //  Смещение канала перемещений.
            moveChannel.Vector.Move(65);

            //  Добавление канала-индикатора смещения нуля.
            targetFrame.Channels.Add(isZero);

            //  Добавление канала работы.
            targetFrame.Channels.Add(work);

            //  Добавление канала времени.
            targetFrame.Channels.Add(timeChannel);

            //  Добавление канала изменения перемещения.
            targetFrame.Channels.Add(dMove);

            //  Ноль силы.
            double zeroForce = 0;
            int zeroCount = 0;

            //  Значение работы и времени.
            double workValue = 0;
            double timeValue = 0;

            //  Обрезка значения.
            for (int i = 0; i < moveChannel.Length; i++)
            {
                if (Math.Abs(moveChannel[i]) < 2)
                {
                    isZero[i] = 1;
                }

                if (moveChannel[i] > 0)
                {
                    moveChannel[i] = 0;
                    zeroForce += forceChannel[i];
                    ++zeroCount;
                }
            }

            if (zeroCount > 0)
            {
                zeroForce /= zeroCount;
            }

            //  Смещение канала силы.
            forceChannel.Vector.Move(-zeroForce);

            //  Вывод информации о файле.
            Console.WriteLine($"zeroForce: {zeroForce}");

            //  Инвертирование канала перемещения.
            moveChannel.Vector.Scale(-1);

            //  Перебор значений силы.
            for (int i = 1; i < forceChannel.Length; i++)
            {
                if (forceChannel[i] != -zeroForce && forceChannel[i - 1] != -zeroForce)
                {
                    timeValue += 1;
                    workValue += (moveChannel[i] - moveChannel[i - 1]) * Math.Abs(forceChannel[i]);
                }

                timeChannel[i] = timeValue;
                work[i] = workValue;
            }

            //  Перебор значений перемещений.
            for (int i = 2; i < forceChannel.Length; i++)
            {
                double val0 = moveChannel[i - 2];
                double val1 = moveChannel[i - 1];
                double val2 = moveChannel[i];

                if (val0 > 0 && val1 > 0 && val2 > 0)
                {
                    if ((val0 < val1 && val1 > val2) || (val0 > val1 && val1 < val2))
                    {
                        dMove[i - 1] = val1 - 0.5 * (val0 + val2);
                    }
                }
            }

            for (int i = 0; i < forceChannel.Length; i++)
            {
                if (forceChannel[i] == -zeroForce)
                {
                    forceChannel[i] = 0;
                }
            }

            //  Сохранение длинного кадра.
            targetFrame.Save(Path.Combine(_TargetDirectory.FullName, $"Vp000_0 {time:yyyy-MM-dd}.{index + 1:0000}"), StorageFormat.TestLab);

            //  Перебор всех файлов.
            Parallel.ForEach(files, file =>
            {
                ////  Средство записи в файл.
                //StreamWriter? writer = null;

                try
                {
                    //  Открытие кадра.
                    Frame sourceFrame = new(file.Path);

                    //  Получение канала силы.
                    Channel force = sourceFrame.Channels["P6_2"];

                    //  Получение канала перемещения.
                    Channel move = sourceFrame.Channels["L6_2"];

                    //  Получение канала скорости.
                    Channel speed = sourceFrame.Channels["V_GPS"];

                    //  Определение времени начала.
                    time = file.Time;

                    //  Перебор значений.
                    for (int i = 0; i < force.Length; i++)
                    {
                        //  Определение значений.
                        double forceValue = force[i];
                        double moveValue = move[i];
                        double speedValue = speed[i / 1200];
                        //time = time.AddSeconds(1 / 1200.0);

                        //  Проверка значений.
                        if (forceValue != 0 && moveValue != 0 && speedValue != 0)
                        {
                            //  Корректировка значений.
                            moveValue += 65;
                            forceValue -= zeroForce;

                            ////  Проверка средства записи.
                            //if (writer is null)
                            //{
                            //    //  Создание средства записи.
                            //    writer = new(Path.Combine(_SourceCsvDirectory.FullName, $"{time:yyyy-MM-dd} {file.Index:0000}.csv"));

                            //    //  Запись заголовка.
                            //    writer.WriteLine("Time;Force;Move;Speed");
                            //}

                            ////  Запись значений в файл.
                            //writer.WriteLine($"{time.AddSeconds(i / 1200.0):dd.MM.yyyy HH:mm:ss.ffff};{forceValue};{moveValue};{speedValue}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    ////  Закрытие средства записи.
                    //writer?.Dispose();
                }
            });
        });
    }

    /// <summary>
    /// Сжатие кадров.
    /// </summary>
    public static void Compactification()
    {
        //  Получение списка файлов.
        IOrderedEnumerable<FileInfo> files = _TargetDirectory.GetFiles().OrderBy(f => f.Extension);

        //  Создание компактного кадра.
        Frame compactFrame = new();

        //  Создание средства записи в текстовый файл.
        using StreamWriter writer = new(Path.Combine(_CompactDirectory.FullName, "Output.txt"));

        //  Создание каналов силы, перемещения и скорости.
        Channel compactForce = new("Force", string.Empty, 1, 1, _Seconds * _Days);
        Channel compactMove = new("Move", string.Empty, 1, 1, _Seconds * _Days);
        Channel compactSpeed = new("Speed", string.Empty, 1, 1, _Seconds * _Days);

        //  Добавление каналов в кадр.
        compactFrame.Channels.AddRange(new Channel[] { compactForce, compactMove, compactSpeed });

        //  Индекс в компактном кадре.
        int compactIndex = 0;

        //  Перебор фалов.
        foreach (FileInfo file in files)
        {
            //  Открытие кадра.
            Frame frame = new(file.FullName);

            //  Получение каналов.
            Channel force = frame.Channels["P6_2"];
            Channel move = frame.Channels["L6_2"];
            Channel speed = frame.Channels["V_GPS"];

            //  Перебор значений.
            for (int i = 0; i < force.Length; i++)
            {
                //  Проверка значения силы.
                if (force[i] != 0)
                {
                    //  Запись в компактный кадр.
                    compactForce[compactIndex] = force[i];
                    compactMove[compactIndex] = move[i];
                    compactSpeed[compactIndex] = speed[i];

                    //  Запись в текстовый файл.
                    writer.WriteLine($"{compactIndex}\t{force[i]}\t{move[i]}\t{speed[i]}");

                    //  Корректировка индекса компактного кадра.
                    ++compactIndex;
                }
            }

            //  Вывод в консоль.
            Console.WriteLine(file.FullName);
        }

        //  Подрезка каналов.
        compactForce.Length = compactIndex;
        compactMove.Length = compactIndex;
        compactSpeed.Length = compactIndex;

        //  Сохранение компактного кадра.
        compactFrame.Save(Path.Combine(_CompactDirectory.FullName, "Vp000_0.0001"), StorageFormat.TestLab);
    }

    private readonly struct LoopPoint
    {
        public double Force { get; init; }
        public double Move { get; init; }
    }

    /// <summary>
    /// Выполняет построение петель.
    /// </summary>
    public static void BuildingLoops()
    {
        //  Создание приложения Excel.
        Excel.Application application = new()
        {
            Visible = true
        };

        //  Блок для очистки ресурсов.
        try
        {
            //  Создание рабочей книги Excel.
            Excel.Workbook book = application.Workbooks.Add(Path.Combine(new FileInfo(Environment.ProcessPath!).Directory!.FullName , "Loops.xltx"));

            //  Сохранение книги.
            book.SaveAs2(Path.Combine(_LoopsDirectory.FullName, $"Loops.xlsx"));

            //  Получение шаблона.
            Excel.Worksheet template = book.Sheets["Template"];

            ////  Очистка каталога.
            //_LoopsDirectory.Delete();
            //_LoopsDirectory.Create();

            //  Открытие компактного кадра.
            Frame compactFrame = new(Path.Combine(_CompactDirectory.FullName, "Vp000_0.0001"));

            //  Получение каналов силы и перемещения.
            Channel compactForce = compactFrame.Channels["Force"];
            Channel compactMove = compactFrame.Channels["Move"];

            //  Петля.
            List<LoopPoint> loop = new();

            //  Индекс петли.
            int loopIndex = 0;

            //  Перебор точек.
            for (int i = 0; i < compactForce.Length; i++)
            {
                //  Определение точки.
                LoopPoint point = new() { Force = compactForce[i], Move = compactMove[i] };

                //  Проверка перемещения.
                if (point.Move < _MinMove)
                {
                    //  Проверка количества точек в петле.
                    if (loop.Count >= _MinLoopPoints)
                    {
                        //  Создание листа.
                        template.Copy(After: book.Sheets[book.Sheets.Count]);
                        Excel.Worksheet sheet = book.Sheets[book.Sheets.Count];
                        sheet.Name = $"L{loopIndex:000}";

                        //  Индекс строки Excel.
                        int row = 2;

                        //  Создание средства записи в текстовый файл.
                        using StreamWriter writer = new(Path.Combine(_LoopsDirectory.FullName, $"Loop{loopIndex:0000}.csv"));

                        //  Запись в текстовый файл.
                        //writer.WriteLine("\"Move\",\"Force\"");
                        writer.WriteLine("Move;Force");

                        //  Перебор точек в петле.
                        foreach (LoopPoint item in loop)
                        {
                            //  Запись в текстовый файл.
                            //writer.WriteLine($"\"{item.Move}\",\"{item.Force}\"");
                            writer.WriteLine($"{item.Move};{item.Force}");

                            //  Запись в Excel.
                            sheet.Cells[row, 1] = item.Move;
                            sheet.Cells[row, 2] = item.Force;

                            //  Переход на новую строку.
                            ++row;
                        }

                        //  Вывод в консоль.
                        Console.WriteLine($"Loop: {loopIndex}");

                        //  Увеличение индекса петли.
                        ++loopIndex;

                        //  Сохранение книги.
                        book.Save();
                    }

                    //  Очистка петли.
                    loop.Clear();
                }
                else
                {
                    //  Добавление точки в петлю.
                    loop.Add(point);
                }
            }

            //  Сохранение книги.
            book.SaveAs2(Path.Combine(_LoopsDirectory.FullName, $"Loops.xlsx"));

            //  Закрытие книги.
            book.Close();
        }
        finally
        {
            //  Завершение работы приложения Excel.
            application.Quit();
        }
    }
}
