using Microsoft.EntityFrameworkCore;
using Simargl.Trials.Aurora.Aurora01.Storage;
using Simargl.Trials.Aurora.Aurora01.Storage.Entities;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Simargl.Trials.Aurora.Aurora01.TrackBuilder;

/// <summary>
/// Предоставляет точку входа.
/// </summary>
internal partial class Program
{
    /// <summary>
    /// Асинхронная точка входа приложения.
    /// </summary>
    /// <param name="args">
    /// Параметры командной строки.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу приложения.
    /// </returns>
    static async Task Main(string[] args)
    {
        //  Для анализатора.
        _ = args;
        await Task.CompletedTask.ConfigureAwait(false);
        CancellationToken cancellationToken = CancellationToken.None;

        //  Вывод информации в консоль.
        Console.WriteLine("Начало работы.");

        //  Блок перехвата всех исключений.
        try
        {
            //  Подключение к базе данных.
            await using Aurora01StorageContext context = new();

            //  Получение всех данных.
            NmeaData[] nmeas = await context.Nmeas
                .OrderBy(x => x.Timestamp)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Связывание данных.
            await RelationAsync(nmeas, cancellationToken).ConfigureAwait(false);


            //  Сохранение изменений.
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Вывод информации в консоль.
            Console.WriteLine();
            Console.WriteLine(ex.ToString());
            Console.WriteLine();
        }

        //  Вывод информации в консоль.
        Console.WriteLine("Завершение работы.");
    }
}
