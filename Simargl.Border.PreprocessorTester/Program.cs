using Simargl.Border.Processing;

namespace Simargl.Border.PreprocessorTester;

/// <summary>
/// Предоставляет точку входа.
/// </summary>
public static class Program
{
    /// <summary>
    /// Постоянная, определяющая путь к исходному кадру.
    /// </summary>
    public const string SourcePath = "E:\\Temporal\\Simargl.Border.PreprocessorTester\\0x0000000000000004";

    /// <summary>
    /// Постоянная, определяющая путь к целевому кадру.
    /// </summary>
    public const string TargetPath = "E:\\Temporal\\Simargl.Border.PreprocessorTester\\Target";

    /// <summary>
    /// Точка входа.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая приложение.
    /// </returns>
    public static async Task Main()
    {
        //  Вывод информации в консоль.
        Console.WriteLine("Начало тестирования.");
        Console.WriteLine();

        //  Создание источника токена отмены.
        using CancellationTokenSource cancellationTokenSource = new();

        //  Получение токена отмены.
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        //  Блок перехвата всех исключений.
        try
        {
            //  Создание устройства обработки.
            Processor processor = await Processor.CreateAsync(cancellationToken).ConfigureAwait(false);

            //  Создание устройства предобработки.
            Preprocessor preprocessor = new(processor, SourcePath, TargetPath);

            //  Выполнение предобработки.
            await preprocessor.InvokeAsync(cancellationToken).ConfigureAwait(false);

            //  Вывод результатов.
            Console.WriteLine();
            Console.WriteLine($"Найдено осей: {preprocessor.PressureMap.AxesCount}");
            Console.WriteLine($"Фиксаций количества осей: {preprocessor.PressureMap.AxesCommits}");
            Console.WriteLine($"Достоверность количества осей: {preprocessor.PressureMap.IsReliable}");
            Console.WriteLine();

            //  Перебор осей.
            foreach (Axis axis in preprocessor.Axes)
            {
                Console.WriteLine();
                Console.WriteLine($"Ось №{axis.Index + 1}:");
                foreach (AxisInteraction? i in axis.Interactions)
                {
                    if (i is not null)
                    {
                        Console.WriteLine($"  {i.Section:00}");
                        Console.WriteLine($"    Section = {i.Section:00}");
                        Console.WriteLine($"    Time = {i.Time:0.00}");
                        Console.WriteLine($"    Speed = {i.Speed:0.0}");


                        Console.WriteLine($"    Length = {i.Length}");
                        Console.WriteLine($"    SpeedSum = {i.SpeedSum:0.0}");
                        Console.WriteLine($"    SpeedSquaresSum = {i.SpeedSquaresSum:0.0}");
                        Console.WriteLine($"    SpeedAverage = {i.SpeedAverage:0.0}");
                        Console.WriteLine($"    SpeedDeviation = {i.SpeedDeviation:0.0}");
                        Console.WriteLine($"    LeftSum = {i.LeftSum:0.0}");
                        Console.WriteLine($"    LeftSquaresSum = {i.LeftSquaresSum:0.0}");
                        Console.WriteLine($"    LeftAverage = {i.LeftAverage:0.0}");
                        Console.WriteLine($"    LeftDeviation = {i.LeftDeviation:0.0}");
                        Console.WriteLine($"    LeftMax = {i.LeftMax:0.0}");
                        Console.WriteLine($"    RightSum = {i.RightSum:0.0}");
                        Console.WriteLine($"    RightSquaresSum = {i.RightSquaresSum:0.0}");
                        Console.WriteLine($"    RightMax = {i.RightMax:0.0}");
                        Console.WriteLine($"    RightAverage = {i.RightAverage:0.0}");
                        Console.WriteLine($"    RightDeviation = {i.RightDeviation:0.0}");
                     }
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            //  Вывод информации в консоль.
            Console.WriteLine();
            Console.WriteLine("Произошла ошибка:");
            Console.WriteLine(ex);
        }

        //  Вывод информации в консоль.
        Console.WriteLine();
        Console.WriteLine("Завершение тестирования.");
    }
}
