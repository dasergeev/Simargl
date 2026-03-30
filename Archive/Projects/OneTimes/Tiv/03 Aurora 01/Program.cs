using Simargl.Frames;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Simargl.Projects.OneTimes.Tiv.Aurora01;

/// <summary>
/// Представляет приложение.
/// </summary>
public sealed partial class Program
{
    /// <summary>
    /// Представляет точку входа в приложение.
    /// </summary>
    public static async Task Main()
    {
        //  Вывод информации в консоль.
        Console.WriteLine("Начало работы.");

        //  Создание источника токена отмены.
        using CancellationTokenSource cancellationTokenSource = new();

        //  Получение токена отмены.
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        //  Блок перехвата всех исключений.
        try
        {
            ////  Сборка кадров.
            //await CollectAsync(cancellationToken).ConfigureAwait(false);

            //  Восстановление перемещений.
            await RestoringMovementsAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Вывод информации в консоль.
            Console.WriteLine();
            Console.WriteLine($"Произошла ошибка: {ex}");
            Console.WriteLine();
        }

        //  Вывод информации в консоль.
        Console.WriteLine("Завершение работы.");
    }
}
