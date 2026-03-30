namespace Simargl.Projects.Oriole.Oriole01Utilities;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Начало работы.");

        CancellationToken cancellationToken = default;

        //  Рекордер: 2025-02-07-08
        //  Рут: 2025-02-07-00
        //  Сканер: 2025-02-06-07

        //  Запечатать часовые каталоги.
        await HourDirectoriesSealAsync(DateTime.Parse("05.02.2025"), cancellationToken).ConfigureAwait(false);


        Console.WriteLine("Конец работы.");

    }
}
