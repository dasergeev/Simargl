using Microsoft.EntityFrameworkCore;
using Simargl.Projects.Oriole.Oriole01Storage;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;

namespace Simargl.Projects.Oriole.Oriole01Utilities;

partial class Program
{
    private static async Task HourDirectoriesSealAsync(DateTime time, CancellationToken cancellationToken)
    {
        //  Подключение к базе данных.
        using Oriole01StorageContext context = new();

        //  Получение часовых каталогов.
        HourDirectoryData[] hourDirectories = await context.HourDirectories
            .Where(x => x.State == HourDirectoryState.Registered && x.Timestamp < time.Ticks)
            .ToArrayAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор часовых каталогов.
        foreach (HourDirectoryData hourDirectory in hourDirectories)
        {
            //  Запечатывание каталога.
            hourDirectory.State = HourDirectoryState.Parsed;
        }

        //  Сохранение изменений.
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}