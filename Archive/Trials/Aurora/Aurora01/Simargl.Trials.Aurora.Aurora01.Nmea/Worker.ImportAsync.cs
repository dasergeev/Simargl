using System.IO;

namespace Simargl.Trials.Aurora.Aurora01.Nmea;

partial class Worker
{
    /// <summary>
    /// Асинхронно выполняет импорт данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// ЗадачаЮ выполняющая импорт данных.
    /// </returns>
    private static async Task ImportAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка флага.
        if (!NmeaTunnings.IsImport) return;

        //  Вывод в консоль.
        Console.WriteLine("Импорт файлов.");
        Console.WriteLine("  Начало импорта файлов.");

        //  Получение списка каталогов.
        DirectoryInfo[] directories = new DirectoryInfo(NmeaTunnings.RawDataPath).GetDirectories("*", SearchOption.TopDirectoryOnly);

        //  Перебор каталогов.
        await Parallel.ForEachAsync(
            directories,
            cancellationToken,
            async delegate (DirectoryInfo directory, CancellationToken cancellationToken)
            {
                //  Получение списка фалов.
                FileInfo[] files = directory.GetFiles("*.nmea", SearchOption.AllDirectories);

                //  Перебор файлов.
                foreach (FileInfo file in files)
                {
                    //  Проверка токена отмены.
                    await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Копирование файла.
                        File.Copy(file.FullName,
                            Path.Combine(NmeaTunnings.NmeaPath, file.Name));
                    }
                    catch { }
                }

                //  Вывод информации в консоль.
                Console.WriteLine($"    {directory.Name}.");
            }).ConfigureAwait(false);

        //  Вывод в консоль.
        Console.WriteLine("  Завершение импорта файлов.");
    }
}
