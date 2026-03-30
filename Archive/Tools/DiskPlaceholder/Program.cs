namespace DiskPlaceholder
{
    internal class Program
    {
        static async Task Main()
        {
            const long fileSize = 1024 * 1024;
            var drivers = DriveInfo.GetDrives();
            await Parallel.ForEachAsync(drivers,
                async delegate (DriveInfo driver, CancellationToken cancellationToken)
                {
                    Console.WriteLine($"{driver.Name}:");
                    try
                    {
                        Random random = new(unchecked((int)DateTime.Now.Ticks));
                        string directory = Path.Combine(driver.Name, "RawRecords");
                        Directory.CreateDirectory(directory);

                        while (driver.AvailableFreeSpace > fileSize)
                        {
                            string path = Path.Combine(directory, $"{DateTime.Now.AddYears(-1).Ticks}.data");
                            byte[] buffer = new byte[fileSize];
                            random.NextBytes(buffer);
                            File.WriteAllBytes(path, buffer);
                        }

                        await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{driver.Name}: {ex}");
                    }
                });


            await Task.CompletedTask;

            Console.WriteLine("Заполнение завершено.");
        }
    }
}
